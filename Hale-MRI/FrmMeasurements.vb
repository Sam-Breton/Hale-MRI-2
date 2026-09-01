Imports System.ComponentModel
Imports System.Windows.Forms.DataVisualization.Charting
Imports Hale_MRI.EncoderStatusStrip
Imports Hale_MRI.RecordNavigationBar
Imports LibDatabase
Imports LibDatabase.BindingSourceExtensions
Imports LibDatabase.Contexts
Imports LibDatabase.Models
Imports LibEncoder
Imports LibGlobals
Imports Microsoft.EntityFrameworkCore
Imports Microsoft.Extensions.DependencyInjection

Public Class FrmMeasurements
    Inherits FrmDatabaseForm

#Region "Private Members"
    Private Const kMaxSamplesPerScan As Integer = 200           ' Maximum number of samples per scan (this is in My.Settings also).
    Private mJobDetails As JobDetail                            ' The current JobDetail record
    Private mJob As Job                                         ' The Job the current JobDetail record belongs to.
    Private mMasterSource As BindingSource = Nothing            ' The form's "master" BindingSource.
    Private mNavigator As RecordNavigationBar = Nothing         ' The form's RecordNavigationBar.
    Private mNewJobDetail As JobDetail = Nothing                ' The new JobDetail record being added.
    Private mRadiusPercent As New MovingAverage(2)              ' Keeps a moving average of RadiusPercent measurements during a scan.
    Private mRadiusMeasurement As RadiusMeasurement = Nothing   ' Stores the RadiusMeasurement to which CellMeasurements collected during a scan are assigned to. 
    Private mSampleCount As Integer                             ' Number of samples for the current scan.
    Private mSaveMeasurementsEnabled As Boolean = False         ' Flag indicating whether hardware measurements can be saved to the database.
    Private mScanIncrement As Double = 1.8                      ' The angle increment between samples in degrees(this will be recalculated on form load but this is the default value).
    Public mHomeSet As Boolean = False                          ' Whether the home position has been set for the current JobDetail.
    Private mLastScannedAngle As Double = Double.MaxValue       ' The last angle measurement saved during scanning (Used with mScanIncrement to determine when to save a new measurement).
    Private mTolerance As String = String.Empty
    Private mScannedPoints As Integer = 0
    Private mLeftOffset As Double = 0
    Private mHubOffset As Double = 0

#If NO_ENCODERS Then
    Private mCm As Integer = 0
    Private mEncoderData As List(Of RadiusMeasurement) = Nothing
    Private mRd As Integer = 0
#End If
#End Region
#Region "Constructors"
    Public Sub New()
        MyBase.New()
        InitializeComponent()
    End Sub

    ' DI Container uses this at runtime.
    Public Sub New(context As HaleMRIContext, serviceProvider As IServiceProvider, scopeFactory As IServiceScopeFactory)
        MyBase.New(context, serviceProvider, scopeFactory)
        InitializeComponent()
    End Sub
#End Region
#Region "Public Interface"
    ''' <summary>
    ''' Adds a new JobDetail for the given Job
    ''' </summary>
    ''' <param name="job"></param>
    Public Sub AddNew(ByVal job As Job)
        mNewJobDetail = New JobDetail With {
            .Job = job,
            .StartDate = Date.Now
        }
        MasterSource.AddNew()
    End Sub

    ''' <summary>
    ''' Returns the currently selected JobDetail,
    ''' or Nothing if there is no selected record.
    ''' </summary>
    Public ReadOnly Property Current As JobDetail
        Get
            Return JobDetailsBindingSource.Current(Of JobDetail)
        End Get
    End Property

    ''' <summary>
    ''' Finds the given JobDetail and, if found, makes it the current record.
    ''' </summary>
    ''' <param name="item">The JobDetail to find.</param>
    ''' <returns>The found JobDetail, or Nothing if not found.</returns>
    Public Function Find(item As JobDetail) As JobDetail
        Dim result As JobDetail = Nothing
        Dim pos As Integer = MasterSource.Find("Id", item.Id)
        If pos <> kNoCurrentRecord Then
            MasterSource.Position = pos
            result = Current
        End If
        Return result
    End Function

    Public Property LeftOffset() As Double
        Get
            Return mLeftOffset
        End Get
        Set(value As Double)
            mLeftOffset = value
            Hardware.Encoders.LeftProbeOffset = mLeftOffset
        End Set
    End Property
    Public Property HubOffset() As Double
        Get
            Return mHubOffset
        End Get
        Set(value As Double)
            mHubOffset = value
            Hardware.Encoders.HubOffset = mHubOffset
        End Set
    End Property
    ''' <summary>
    ''' Gets/sets the encoder hardware used by the form.
    ''' </summary>
    ''' <returns></returns>
    Public Property Hardware As WorkstationEncoders
        ' Property to get or set the EncoderHardware instance and Workstation calibration data
        ' This property sets the Hardware property of the EncoderStatusStrip1 control so
        ' that its UI updates accordingly.
        Get
            Return EncoderStatusStrip1.Hardware
        End Get
        Set(value As WorkstationEncoders)
            EncoderStatusStrip1.Hardware = value
            If EncoderStatusStrip1.Hardware IsNot Nothing Then
                If EncoderStatusStrip1.Hardware.Encoders IsNot Nothing Then
                    Try
                        If Not EncoderStatusStrip1.Hardware.Encoders.Initialized Then EncoderStatusStrip1.Initialize()
                    Catch ex As Exception
                        MsgBox(ex.Message, MsgBoxStyle.Critical, STR_TITLE_ENCODER_ERROR)
                    End Try
                End If
            End If
            FormEnable(EncoderStatusStrip1.Hardware, Me.Job)
        End Set
    End Property

    ''' <summary>
    ''' Loads all JobDetails and their Cell, Extreme and RadiusMeasurements
    ''' for the given Job.
    ''' </summary>
    ''' <returns></returns>
    Public Property Job As Job
        Get
            Return mJob
        End Get
        Set(value As Job)
            mJob = value
            If mJob IsNot Nothing Then
                Try
                    'If EncoderStatusStrip1?.Hardware?.Encoders IsNot Nothing Then
                    JobDetailsBindingSource.DataSource = GetMeasurementData(mJob)
                    If mJob.LeExclusion Is Nothing Then mJob.LeExclusion = 0
                    If mJob.TeExclusion Is Nothing Then mJob.TeExclusion = 0
                    If Job.PropellerRotation = "L" Then
                        EncoderStatusStrip1.Hardware.Encoders.SetForward(0, False)
                    Else
                        EncoderStatusStrip1.Hardware.Encoders.SetForward(0, True)
                    End If
                    'FormEnable(EncoderStatusStrip1.Hardware, Me.Job)
                    ShowJobInfo()
                    EncoderStatusStrip1.TimerOn = True
                    'Else
                    '    Throw New Exception($"Encoder initialization error")
                    'End If
                Catch ex As Exception
                    MessageBox.Show(String.Format(STR_ERR_ENCODERS, $"{ex.Message}"), STR_TITLE_ENCODER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Me.Close()
                End Try
            End If
        End Set
    End Property

    ''' <summary>
    ''' Loads only the given JobDetail and its Cell, Extreme and RadiusMeasurements.
    ''' </summary>
    ''' <returns></returns>
    Public Property JobDetails As JobDetail
        Get
            Return mJobDetails
        End Get
        Set(value As JobDetail)
            mJobDetails = value
            mJob = mJobDetails?.Job
            CmdHome.Enabled = True
            If mJobDetails IsNot Nothing Then
                JobDetailsBindingSource.DataSource = GetMeasurementData(mJobDetails)
                If mJob.LeExclusion Is Nothing Then mJob.LeExclusion = 0
                If mJob.TeExclusion Is Nothing Then mJob.TeExclusion = 0
                ShowJobInfo()
                If EncoderStatusStrip1?.Hardware?.Encoders IsNot Nothing Then
                    FormEnable(EncoderStatusStrip1.Hardware, Me.Job)
                End If
                'Try
                'If EncoderStatusStrip1?.Hardware?.Encoders IsNot Nothing Then
                '    JobDetailsBindingSource.DataSource = GetMeasurementData(mJob)
                '    If mJob.LeExclusion Is Nothing Then mJob.LeExclusion = 0
                '    If mJob.TeExclusion Is Nothing Then mJob.TeExclusion = 0
                '    If Job.PropellerRotation = "L" Then
                '        EncoderStatusStrip1.Hardware.Encoders.SetForward(0, False)
                '    Else
                '        EncoderStatusStrip1.Hardware.Encoders.SetForward(0, True)
                '    End If
                '    FormEnable(EncoderStatusStrip1.Hardware, Me.Job)
                '    ShowJobInfo()
                '    ShowJobDetailsInfo()
                '    Else
                '        Throw New Exception($"Encoder initialization error")
                '    End If
                'Catch ex As Exception
                '    MessageBox.Show(String.Format(STR_ERR_ENCODERS, $"{ex.Message}"), STR_TITLE_ENCODER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
                '    Me.Close()
                'End Try
            End If
        End Set
    End Property

    Public Property SelectedTolerance As String
        Get
            Return mJobDetails.ToleranceClass
        End Get
        Set(value As String)
            mTolerance = value
            If mJobDetails IsNot Nothing Then
                mJobDetails.ToleranceClass = value
                Database.SaveChanges()
                ShowJobDetailsInfo()
            End If
        End Set
    End Property

    Public Property HomeSet As Boolean ' revisit this to make it work consistently and leave the option to rehome during scanning process
        Get
            Return mHomeSet
        End Get
        Set(value As Boolean)
            mHomeSet = value
            If value = True Then
                If JobDetails.RadiusMeasurements.Count >= 1 Then
                    Dim result = MessageBox.Show("Setting Home Position for this job will remove scanned data for this Job Details.", "Set Home", MessageBoxButtons.OKCancel)
                    If result = DialogResult.OK Then
                        JobDetails.RadiusMeasurements.Clear()
                        Database.SaveChanges()
                        ShowJobDetailsInfo()
                        CmdHome.Text = "Re-Home"
                        ChkScan.Enabled = True
                        ChkScan.BackColor = Color.ForestGreen
                    Else
                        mHomeSet = False
                        CmdHome.Enabled = True
                    End If
                    Exit Property
                End If
                CmdHome.Text = "Re-Home"
                ChkScan.Enabled = True
                TxtStatus.Text = "Ready to Scan"
                ChkScan.BackColor = Color.ForestGreen
                ShowJobInfo()
            End If
        End Set
    End Property

    Public Property MinimumsApply As Boolean
        Get
            Return ChkMinimumsApply.Checked
        End Get
        Set(value As Boolean)
            ChkMinimumsApply.Checked = value
            ShowJobDetailsInfo()
        End Set
    End Property

    Public Property AllowProgressivePitch As Boolean
        Get
            Return ChkAllowProgressivePitch.Checked
        End Get
        Set(value As Boolean)
            ChkAllowProgressivePitch.Checked = value
            ShowJobDetailsInfo()
        End Set
    End Property

    Public Sub HomeRefresh()
        If mHomeSet Then
            ChkScan.Enabled = True
            TxtStatus.Text = "Ready to Scan"
            ChkScan.BackColor = Color.ForestGreen
        End If
    End Sub
#End Region
#Region "Private Interface"
    Private Sub BindDataSources()
        Dim tolerances = New BindingList(Of Tolerance)(Database.Tolerances.Local.ToList())
        ComboTolerance.DataSource = tolerances
        ComboTolerance.DisplayMember = "ToleranceClass"
        ComboTolerance.ValueMember = "ToleranceClass"
        ComboReferencePoint.DataSource = New List(Of String) From {"LE", "Mid", "TE"}
        ComboPitchBasis.DataSource = New List(Of String) From {"Mean", "Marked", "Desired"}

        EmployeesBindingSource.DataSource = New BindingList(Of Employee)(Database.Employees.ToList())
        ClassBindingSource.DataSource = New BindingList(Of Tolerance)(Database.Tolerances.ToList())
        MeasurementTypesBindingSource.DataSource = Database.MeasurementTypes.ToList()
    End Sub

    Private Function CreateNewJobDetail() As JobDetail
        Dim tol As String
        If mJobDetails IsNot Nothing Then
            tol = mJobDetails.ToleranceClass
        Else
            tol = "S"
        End If
        Return New JobDetail With {
            .Job = mJob,
            .StartDate = Date.Now,
            .ToleranceClass = tol
        }
    End Function

    Private Function DeleteConfirm() As Boolean
        Return (MessageBox.Show($"Delete job detail and all measurements from {JobDetails?.StartDate}?", STR_TITLE_DEFAULT, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = DialogResult.OK)
    End Function

    Private Sub DeleteJobDetail()
        JobDetailsBindingSource.Delete(Me.Database)
    End Sub

    Private Sub FormEnable(ByVal hardware As WorkstationEncoders, ByVal job As Job)
        ' We need to enable controls/functions based on the hardware status and given Job.
        If hardware IsNot Nothing Then
            If hardware.Encoders.Initialized Then
                EncoderStatusStrip1.TimerOn = True
                CmdHome.Enabled = True
            End If
            If job IsNot Nothing Then
                If job.PropellerRotation = "L" Then
                    EncoderStatusStrip1.Hardware.Encoders.SetForward(0, False)
                Else
                    EncoderStatusStrip1.Hardware.Encoders.SetForward(0, True)
                End If
            End If
            mSaveMeasurementsEnabled = True
        End If
    End Sub

    Private Sub FormSort(ByRef jobDetails As BindingList(Of JobDetail))
        For Each jd As JobDetail In jobDetails
            For Each rm As RadiusMeasurement In jd?.RadiusMeasurements
                rm.CellMeasurements = rm.CellMeasurements.OrderBy(Function(cm) cm.Id).ToList()
                rm.ExtremeMeasurements = rm.ExtremeMeasurements.OrderBy(Function(em) em.Id).ToList()
            Next
        Next
    End Sub

    Private Function GetMeasurementData(j As Object) As BindingList(Of JobDetail)
        Dim data As BindingList(Of JobDetail) = Nothing
        If TypeOf j Is Job Then
            data = New BindingList(Of JobDetail)(
            Database.JobDetails _
                .Where(Function(jd) jd.Job Is CType(j, Job)) _
                .Include(Function(jd) jd.Job) _
                .Include(Function(rm) rm.RadiusMeasurements) _
                .ThenInclude(Function(m) m.CellMeasurements) _
                .Include(Function(rm) rm.RadiusMeasurements) _
                .ThenInclude(Function(m) m.ExtremeMeasurements) _
                .OrderBy(Function(jd) jd.StartDate) _
                .AsSplitQuery().ToList()
            )
        ElseIf TypeOf j Is JobDetail Then
            data = New BindingList(Of JobDetail)(
            Database.JobDetails _
                .Where(Function(jd) jd Is CType(j, JobDetail)) _
                .Include(Function(jd) jd.Job) _
                .Include(Function(rm) rm.RadiusMeasurements) _
                .ThenInclude(Function(m) m.CellMeasurements) _
                .Include(Function(rm) rm.RadiusMeasurements) _
                .ThenInclude(Function(m) m.ExtremeMeasurements) _
                .OrderBy(Function(jd) jd.StartDate) _
                .AsSplitQuery().ToList()
            )
        End If
        FormSort(data)
        Return data
    End Function
    Private Sub MeasurementsGet()
        ' Calls encoder angle, depth and radius methods ONCE, and uses the returned
        ' values as required. This one doesn't save Measurements.
        With EncoderStatusStrip1
            Dim angle As Double = .Angle()
            Dim depth As Double = .Depth()
            Dim radius As IEncoderHardware.RadiusMeasurement = .Radius(Job.PropellerDiameter)
            Dim blade As Integer = GetBladeNumber(angle, Job.PropellerBlades)
            'add handlers for editting based on offsets
            TxtBlade.Text = blade
            TxtAngle.Text = Math.Round(angle, 2).ToString() + " °"
            TxtRadius.Text = Math.Round(radius.Value * 2, 2).ToString() + " In."
            TxtDepth.Text = Math.Round(depth, 2).ToString() + " In."
            TxtRadiusPercent.Text = Math.Round(radius.Percent * 100.0, 2).ToString() + " %"
            PlotVisualization(angle, radius.Percent * 100)
        End With
    End Sub
    Private Sub MeasurementsGet(lastAngle As Double)
        ' Calls encoder angle, depth and radius methods ONCE, and uses the returned
        ' values as required. Saves the measurements if the angle measurement 
        ' changes by more than some specified amount.
        ' Doesn't change the Blade Number textbox as it wouldn't change during scanning.
        With EncoderStatusStrip1
            Dim angle As Double = .Angle()
            Dim depth As Double = .Depth()
            Dim radius As IEncoderHardware.RadiusMeasurement = .Radius(Job.PropellerDiameter)
            If TxtBlade.Text = "1" And angle >= 180 Then
                angle -= 360.0 'this is a simple way to handle overscan when crossing 0 degrees on blade 1 - this will make the change in angle consistent when crossing 0 degrees
            End If
            TxtAngle.Text = Math.Round(angle, 2).ToString() + " °"
            TxtRadius.Text = Math.Round(radius.Value * 2, 2).ToString() + " In."
            TxtDepth.Text = Math.Round(depth, 2).ToString() + " In."
            TxtRadiusPercent.Text = Math.Round(radius.Percent * 100.0, 2).ToString() + " %"
            If (lastAngle - angle) > mScanIncrement Then
                MeasurementsSave(angle, depth, radius)
                mSampleCount += 1
            End If
        End With
    End Sub
    Private Sub MeasurementsSave(ByVal angle As Double, ByVal depth As Double, ByVal radius As IEncoderHardware.RadiusMeasurement)
        ' Updates the RadiusPercent moving average and saves the given angle and depth measurements.
        If TxtBlade.Text = "1" And angle > 180.0 Then 'this is a simple way to handle overscan when crossing 0 degrees on blade 1
            angle -= 360.0                                           ' this will make the change in angle consistent when crossing 0 degrees
        End If
        mRadiusPercent.Input(radius.Percent * 100)
        Dim cm As New CellMeasurement With {
            .RadiusMeasurement = mRadiusMeasurement,
            .Angle = angle,
            .Depth = depth
        }
        Database.CellMeasurements.Add(cm)
        mLastScannedAngle = angle
    End Sub
    Protected Overrides Sub OnDataSyncNotification(entityType As Type, primaryKey As Object)
        ' This event is raised by forms whenever changes are made to the database.
        ' Load any required data from the database into the LocalView.
        ' Reset any BindingSources effected.
    End Sub

    Private Sub PlotVisualization(angle As Double, radius As Double)
        If mJobDetails IsNot Nothing Then
            'If mJobDetails.Job.PropellerRotation = "L" Then
            angle = 360 - angle
            'End If
        End If
        Dim img As New NamedImage With {
            .Name = "PlotVisualization",
            .Image = New Bitmap(1600, 1600)}
        Using g As Graphics = Graphics.FromImage(img.Image)
            g.Clear(Color.Transparent)
            Dim pen As New Pen(Color.Black, 14)
            Dim halfheight As Double = 800
            Dim halfwidth As Double = 800
            Dim adjustedheight As Double = halfheight + (halfheight * Math.Sin(angle * Math.PI / 180.0))
            Dim adjustedwidth As Double = halfwidth + (halfwidth * Math.Cos(angle * Math.PI / 180.0))
            g.DrawLine(pen, New Point(halfwidth, halfheight), New Point(adjustedwidth, adjustedheight))
            Dim adjustedradius As Double = radius / 100
            Dim adjwidth As Double = halfwidth * adjustedradius
            Dim adjheight As Double = halfheight * adjustedradius
            Dim Ellipsewidth As Double = adjwidth * 2
            Dim Ellipseheight As Double = adjheight * 2
            g.DrawEllipse(pen, CType(halfwidth - adjwidth, Integer), CType(halfheight - adjheight, Integer), CType(Ellipsewidth, Integer), CType(Ellipseheight, Integer))
        End Using
        ChartPlot1.Chart1.Images.Clear()
        ChartPlot1.Chart1.Images.Add(img)
        If ChartPlot1.Chart1.ChartAreas.Count = 0 Then Return
        ChartPlot1.Chart1.ChartAreas(0).BackImage = "PlotVisualization"
    End Sub

    Protected Property MasterSource As BindingSource
        Get
            Return mMasterSource
        End Get
        Set(value As BindingSource)
            mMasterSource = value
            If Navigator IsNot Nothing Then Navigator.MasterSource = mMasterSource
        End Set
    End Property

    Private Property Navigator As RecordNavigationBar
        Get
            Return mNavigator
        End Get
        Set(value As RecordNavigationBar)
            mNavigator = value
            If mNavigator IsNot Nothing Then mNavigator.Database = Database
        End Set
    End Property
    Private Sub NewRadiusMeasurement()
        ' RadiusMeasurement is now parent (PK) of Cell and ExtremeMeasurements
        ' (FK). Clear the previous moving average and create a new
        ' RadiusMeasurement with .Radius = 0, which will be updated at
        ' the end of the scan.
        mRadiusPercent.Clear()
        mRadiusMeasurement = New RadiusMeasurement With {
            .JobDetails = Me.JobDetails,
            .Radius = 0.0,
            .LeCell = 0,
            .TeCell = 0
        }
        mLastScannedAngle = 1000.0 'ensure first measurement is always saved
        'mRadiusPercent.Input(Double.Parse(TxtRadiusPercent.Text))
    End Sub

    Private Function ReferenceRadiiGet(ByVal blade As Integer) As List(Of Double)
        ' Returns a list of reference radii for the given blade.
        Dim radii As New List(Of Double)
        If JobDetails?.RadiusMeasurements IsNot Nothing Then
            For Each rm As RadiusMeasurement In JobDetails.RadiusMeasurements
                If rm.BladeId = blade Then radii.Add(Math.Round(CType(rm.Radius, Double)))
            Next
        End If
        Return radii
    End Function

    Private Sub SaveRadiusMeasurement()
        ' Update and save the current RadiusMeasurement with the moving average
        ' we collected while scanning.
        If JobDetails.RadiusMeasurements.Where(Function(rm) rm.BladeId = Integer.Parse(TxtBlade.Text) And Math.Round(rm.Radius().Value) = Math.Round(mRadiusPercent.Output())).Any() Then
            Database.RadiusMeasurements.Remove(JobDetails.RadiusMeasurements.Where(Function(rm) rm.BladeId = Integer.Parse(TxtBlade.Text) And Math.Round(rm.Radius().Value) = Math.Round(mRadiusPercent.Output())).FirstOrDefault())
        End If
        If mSampleCount < 3 Then ' if less than 3 samples then we consider this a bad scan and we don't save the measurement
            JobDetails.RadiusMeasurements.Remove(mRadiusMeasurement)
            TxtStatus.Text = "Ready to Scan"
            ChkScan.Text = "Scan"
            Return
        End If
        mRadiusMeasurement.Radius = mRadiusPercent.Output()
        mRadiusMeasurement.BladeId = Integer.Parse(TxtBlade.Text)
        mRadiusMeasurement.TeCell = mSampleCount - 1
        Database.RadiusMeasurements.Add(mRadiusMeasurement)
        Database.SaveChanges()
        TxtStatus.Text = "Ready to Scan"
        ChkScan.Text = "Scan"
        ComboReferenceBlade.SelectedIndex = mRadiusMeasurement.BladeId.Value - 1
        ComboReferenceRadius.SelectedIndex = ComboReferenceRadius.DataSource.IndexOf(Math.Round(mRadiusMeasurement.Radius.Value))
    End Sub

    Private Sub ScanControlsEnabled(ByVal isScanning As Boolean)
    End Sub

    Private Property Scanning As Boolean
        Get
            Return ChkScan.Checked
        End Get
        Set(value As Boolean)
            If value Then
                NewRadiusMeasurement()
                mSampleCount = 0
                TxtStatus.Text = "Scanning..."
                ChkScan.Text = "Stop"
                ChkScan.BackColor = Color.Red
                TxtStatus.BackColor = Color.Red
            Else
                TxtStatus.Text = "Saving Measurements..."
                ChkScan.BackColor = Color.ForestGreen
                TxtStatus.BackColor = Color.ForestGreen
                SaveRadiusMeasurement()
                ShowBladePitch(True)
                ShowTolerances(MinimumsApply, AllowProgressivePitch)
                ShowBladePlot()
                'ShowTrack() dont need this because it is called when the selectedIndex of ComboRefBlade changes in SaveRadiusMeasurement()
            End If
            ScanControlsEnabled(value)
        End Set
    End Property

    Private Function ShowMeanPitchPropellerTolerance(minsapply As Boolean, app As Boolean, classes As List(Of Tolerance)) As Integer
        Dim passingClass = 0
        For Each tol As Tolerance In classes
            If passingClass < classes.IndexOf(tol) Then
                Return passingClass
            End If
            Dim pitch = mJobDetails.WheelPitch
            Dim meanPitch As ToleranceColor = CheckWheelPitch(tol, pitch, mJob.DesiredPitch, minsapply)
            If meanPitch <> ToleranceColor.Pass Then
                passingClass += 1
            End If
        Next
        Return passingClass
    End Function
    Private Function ShowAngularDeviationTolerance(classes As List(Of Tolerance), radius As Double) As Integer
        Dim passingClass As Integer = 0
        Dim largestDeviation As Double = 0.0

        Dim blade As Integer
        For blade = 1 To mJob?.PropellerBlades
            Dim rad As RadiusMeasurement
            Dim rad2 As RadiusMeasurement
            Dim nextBlade As Integer = blade + 1
            If blade = mJob.PropellerBlades Then
                nextBlade = 1
            End If
            If mJobDetails?.RadiusMeasurements.Where(Function(rm) rm.BladeId = blade).Where(Function(rm) Math.Round(rm.Radius.Value) = radius).Any() Then
                rad = mJobDetails?.RadiusMeasurements.Where(Function(rm) rm.BladeId = blade).Where(Function(rm) Math.Round(rm.Radius.Value) = radius).FirstOrDefault()
                rad2 = mJobDetails?.RadiusMeasurements.Where(Function(rm) rm.BladeId = nextBlade).Where(Function(rm) Math.Round(rm.Radius.Value) = radius).FirstOrDefault()
            Else ' if no radii at selected radius then no classes pass inspection
                Return 5
            End If
            Dim bladeMidAngle = GetChordMidAngle(rad.CellMeasurements) ' need to make all necessary checks to select a good radius measurement
            Dim nextBladeMidAngle = GetChordMidAngle(rad2.CellMeasurements)
            If rad2.BladeId = 1 Then
                nextBladeMidAngle += 360
            End If
            Dim CurrentDeviation As Double
            If bladeMidAngle - nextBladeMidAngle < 0 Then
                CurrentDeviation = nextBladeMidAngle - bladeMidAngle
                If CurrentDeviation < mJobDetails.Job.PropellerBlades / 360 Then
                    CurrentDeviation = (mJobDetails.Job.PropellerBlades / 360) - CurrentDeviation
                Else
                    CurrentDeviation -= (360 / mJobDetails.Job.PropellerBlades)
                End If
            Else
                CurrentDeviation = Math.Abs(bladeMidAngle - nextBladeMidAngle)
                If CurrentDeviation < mJobDetails.Job.PropellerBlades / 360 Then
                    CurrentDeviation = (mJobDetails.Job.PropellerBlades / 360) - CurrentDeviation
                Else
                    CurrentDeviation -= (360 / mJobDetails.Job.PropellerBlades)
                End If
            End If
            If largestDeviation < Math.Abs(CurrentDeviation) Then
                largestDeviation = CurrentDeviation
            End If
        Next
        For Each tol As Tolerance In classes
            If passingClass < classes.IndexOf(tol) Then
                Exit For
            End If
            Dim angDeviationCheck As ToleranceColor = CheckAngularDeviation(tol, mJob.PropellerBlades, largestDeviation, 360 / mJobDetails.Job.PropellerBlades)
            If angDeviationCheck <> ToleranceColor.Pass Then
                passingClass += 1
                Exit For
            End If
        Next
        TxtAngularDeviation.Text = Math.Round(Math.Abs(largestDeviation), 2).ToString("F2") + "°"
        Return passingClass
    End Function
    Private Function ShowAxialPositionTolerance(classes As List(Of Tolerance), radius As Double) As Integer
        Dim passingClass As Integer = 0
        Dim largestDeviation As Double = 0.0
        For Each tol As Tolerance In classes
            If passingClass < classes.IndexOf(tol) Then
                Return passingClass
            End If
            Dim blade As Integer
            For blade = 1 To mJob?.PropellerBlades
                Dim rad As RadiusMeasurement
                Dim rad2 As RadiusMeasurement
                Dim nextBlade As Integer = blade + 1
                If blade = mJob.PropellerBlades Then
                    nextBlade = 1
                End If
                If mJobDetails?.RadiusMeasurements.Where(Function(rm) rm.BladeId = blade).Where(Function(rm) Math.Round(rm.Radius.Value) = radius).Any() Then
                    rad = mJobDetails?.RadiusMeasurements.Where(Function(rm) rm.BladeId = blade).Where(Function(rm) Math.Round(rm.Radius.Value) = radius).FirstOrDefault()
                    rad2 = mJobDetails?.RadiusMeasurements.Where(Function(rm) rm.BladeId = nextBlade).Where(Function(rm) Math.Round(rm.Radius.Value) = radius).FirstOrDefault()
                Else ' if no radii at selected radius then no classes pass inspection
                    Return 5
                End If
                Dim bladeMidDepth = GetChordMidDepth(rad.CellMeasurements) ' need to make all necessary checks to select a good radius measurement
                Dim nextBladeMidDepth = GetChordMidDepth(rad2.CellMeasurements)
                If largestDeviation < Math.Abs(bladeMidDepth - nextBladeMidDepth) Then
                    largestDeviation = Math.Abs(bladeMidDepth - nextBladeMidDepth)
                End If
                Dim axialPosCheck As ToleranceColor = CheckAngularDeviation(tol, mJob.PropellerBlades, bladeMidDepth, nextBladeMidDepth)
                If axialPosCheck <> ToleranceColor.Pass Then
                    passingClass += 1
                    Exit For
                End If
            Next
        Next
        TxtAxialPosition.Text = Math.Round(largestDeviation, 2).ToString() + " In."
        Return passingClass
    End Function
    Private Sub ShowTolerances(mins As Boolean, app As Boolean)
        If mJobDetails Is Nothing Then
            Return
        End If
        Dim Classes As New List(Of Tolerance) From {GetToleranceTable(Database, "S"), GetToleranceTable(Database, "I"), GetToleranceTable(Database, "II"), GetToleranceTable(Database, "III"), GetToleranceTable(Database, "Custom")}
        ' Classes(0) = Class S Classes(1) = Class I Classes(2) = Class II Classes(3) = Class III Classes(4) = Custom
        If ChkLocalPitch.Checked Then
            Dim LocalPitchClass As Integer = ShowLocalPitchTolerance(JobDetails, mins, app, Classes) 'need to implement local pitch radius restrictions IE class S needs 5 radii
            Select Case LocalPitchClass
                Case 0
                    LabTolLPS.ForeColor = Color.Green
                    LabTolLPI.ForeColor = Color.Green
                    LabTolLPII.ForeColor = Color.Green
                    LabTolLPC.ForeColor = Color.Green
                Case 1
                    LabTolLPS.ForeColor = Color.Red
                    LabTolLPI.ForeColor = Color.Green
                    LabTolLPII.ForeColor = Color.Green
                    LabTolLPC.ForeColor = Color.Green
                Case 2
                    LabTolLPS.ForeColor = Color.Red
                    LabTolLPI.ForeColor = Color.Red
                    LabTolLPII.ForeColor = Color.Green
                    LabTolLPC.ForeColor = Color.Green
                Case 3
                    LabTolLPS.ForeColor = Color.Red
                    LabTolLPI.ForeColor = Color.Red
                    LabTolLPII.ForeColor = Color.Red
                    LabTolLPC.ForeColor = Color.Green
                Case Else
                    LabTolLPS.ForeColor = Color.Red
                    LabTolLPI.ForeColor = Color.Red
                    LabTolLPII.ForeColor = Color.Red
                    LabTolLPC.ForeColor = Color.Red
            End Select
        End If
        If ChkMeanPitchRadius.Checked Then
            Dim MeanPitchRadiusClass As Integer = ShowMeanPitchRadiusTolerance(mJobDetails, mins, app, Classes)
            Select Case MeanPitchRadiusClass
                Case 0
                    LabTolMPRS.ForeColor = Color.Green
                    LabTolMPRI.ForeColor = Color.Green
                    LabTolMPRII.ForeColor = Color.Green
                    LabTolMPRIII.ForeColor = Color.Green
                    LabTolMPRC.ForeColor = Color.Green
                Case 1
                    LabTolMPRS.ForeColor = Color.Red
                    LabTolMPRI.ForeColor = Color.Green
                    LabTolMPRII.ForeColor = Color.Green
                    LabTolMPRIII.ForeColor = Color.Green
                    LabTolMPRC.ForeColor = Color.Green
                Case 2
                    LabTolMPRS.ForeColor = Color.Red
                    LabTolMPRI.ForeColor = Color.Red
                    LabTolMPRII.ForeColor = Color.Green
                    LabTolMPRIII.ForeColor = Color.Green
                    LabTolMPRC.ForeColor = Color.Green
                Case 3
                    LabTolMPRS.ForeColor = Color.Red
                    LabTolMPRI.ForeColor = Color.Red
                    LabTolMPRII.ForeColor = Color.Red
                    LabTolMPRIII.ForeColor = Color.Green
                    LabTolMPRC.ForeColor = Color.Green
                Case 4
                    LabTolMPRS.ForeColor = Color.Red
                    LabTolMPRI.ForeColor = Color.Red
                    LabTolMPRII.ForeColor = Color.Red
                    LabTolMPRIII.ForeColor = Color.Red
                    LabTolMPRC.ForeColor = Color.Green
                Case 5
                    LabTolMPRS.ForeColor = Color.Red
                    LabTolMPRI.ForeColor = Color.Red
                    LabTolMPRII.ForeColor = Color.Red
                    LabTolMPRIII.ForeColor = Color.Red
                    LabTolMPRC.ForeColor = Color.Red
            End Select
        End If
        If ChkMeanPitchBlade.Checked Then
            Dim MeanPitchBladeClass As Integer = ShowMeanPitchBladeTolerance(mJobDetails, mins, app, Classes)
            Select Case MeanPitchBladeClass
                Case 0
                    LabTolMPBS.ForeColor = Color.Green
                    LabTolMPBI.ForeColor = Color.Green
                    LabTolMPBII.ForeColor = Color.Green
                    LabTolMPBIII.ForeColor = Color.Green
                    LabTolMPBC.ForeColor = Color.Green
                Case 1
                    LabTolMPBS.ForeColor = Color.Red
                    LabTolMPBI.ForeColor = Color.Green
                    LabTolMPBII.ForeColor = Color.Green
                    LabTolMPBIII.ForeColor = Color.Green
                    LabTolMPBC.ForeColor = Color.Green
                Case 2
                    LabTolMPBS.ForeColor = Color.Red
                    LabTolMPBI.ForeColor = Color.Red
                    LabTolMPBII.ForeColor = Color.Green
                    LabTolMPBIII.ForeColor = Color.Green
                    LabTolMPBC.ForeColor = Color.Green
                Case 3
                    LabTolMPBS.ForeColor = Color.Red
                    LabTolMPBI.ForeColor = Color.Red
                    LabTolMPBII.ForeColor = Color.Red
                    LabTolMPBIII.ForeColor = Color.Green
                    LabTolMPBC.ForeColor = Color.Green
                Case 4
                    LabTolMPBS.ForeColor = Color.Red
                    LabTolMPBI.ForeColor = Color.Red
                    LabTolMPBII.ForeColor = Color.Red
                    LabTolMPBIII.ForeColor = Color.Red
                    LabTolMPBC.ForeColor = Color.Green
                Case Else
                    LabTolMPBS.ForeColor = Color.Red
                    LabTolMPBI.ForeColor = Color.Red
                    LabTolMPBII.ForeColor = Color.Red
                    LabTolMPBIII.ForeColor = Color.Red
                    LabTolMPBC.ForeColor = Color.Red
            End Select
        End If
        If ChkMeanPitchPropeller.Checked Then
            Dim MeanPitchPropellerClass = ShowMeanPitchPropellerTolerance(mins, app, Classes)
            Select Case MeanPitchPropellerClass
                Case 0
                    LabTolMPPS.ForeColor = Color.Green
                    LabTolMPPI.ForeColor = Color.Green
                    LabTolMPPII.ForeColor = Color.Green
                    LabTolMPPIII.ForeColor = Color.Green
                    LabTolMPPC.ForeColor = Color.Green
                Case 1
                    LabTolMPPS.ForeColor = Color.Red
                    LabTolMPPI.ForeColor = Color.Green
                    LabTolMPPII.ForeColor = Color.Green
                    LabTolMPPIII.ForeColor = Color.Green
                    LabTolMPPC.ForeColor = Color.Green
                Case 2
                    LabTolMPPS.ForeColor = Color.Red
                    LabTolMPPI.ForeColor = Color.Red
                    LabTolMPPII.ForeColor = Color.Green
                    LabTolMPPIII.ForeColor = Color.Green
                    LabTolMPPC.ForeColor = Color.Green
                Case 3
                    LabTolMPPS.ForeColor = Color.Red
                    LabTolMPPI.ForeColor = Color.Red
                    LabTolMPPII.ForeColor = Color.Red
                    LabTolMPPIII.ForeColor = Color.Green
                    LabTolMPPC.ForeColor = Color.Green
                Case 4
                    LabTolMPPS.ForeColor = Color.Red
                    LabTolMPPI.ForeColor = Color.Red
                    LabTolMPPII.ForeColor = Color.Red
                    LabTolMPPIII.ForeColor = Color.Red
                    LabTolMPPC.ForeColor = Color.Green
                Case Else
                    LabTolMPPS.ForeColor = Color.Red
                    LabTolMPPI.ForeColor = Color.Red
                    LabTolMPPII.ForeColor = Color.Red
                    LabTolMPPIII.ForeColor = Color.Red
                    LabTolMPPC.ForeColor = Color.Red
            End Select
        End If
        If ChkAngularDeviation.Checked Then
            Dim AngularDeviationClass As Integer = ShowAngularDeviationTolerance(Classes, 70)
            Select Case AngularDeviationClass
                Case 0
                    LabTolADS.ForeColor = Color.Green
                    LabTolADI.ForeColor = Color.Green
                    LabTolADII.ForeColor = Color.Green
                    LabTolADIII.ForeColor = Color.Green
                    LabTolADC.ForeColor = Color.Green
                Case 1
                    LabTolADS.ForeColor = Color.Red
                    LabTolADI.ForeColor = Color.Green
                    LabTolADII.ForeColor = Color.Green
                    LabTolADIII.ForeColor = Color.Green
                    LabTolADC.ForeColor = Color.Green
                Case 2
                    LabTolADS.ForeColor = Color.Red
                    LabTolADI.ForeColor = Color.Red
                    LabTolADII.ForeColor = Color.Green
                    LabTolADIII.ForeColor = Color.Green
                    LabTolADC.ForeColor = Color.Green
                Case 3
                    LabTolADS.ForeColor = Color.Red
                    LabTolADI.ForeColor = Color.Red
                    LabTolADII.ForeColor = Color.Red
                    LabTolADIII.ForeColor = Color.Green
                    LabTolADC.ForeColor = Color.Green
                Case 4
                    LabTolADS.ForeColor = Color.Red
                    LabTolADI.ForeColor = Color.Red
                    LabTolADII.ForeColor = Color.Red
                    LabTolADIII.ForeColor = Color.Red
                    LabTolADC.ForeColor = Color.Green
                Case Else
                    LabTolADS.ForeColor = Color.Red
                    LabTolADI.ForeColor = Color.Red
                    LabTolADII.ForeColor = Color.Red
                    LabTolADIII.ForeColor = Color.Red
                    LabTolADC.ForeColor = Color.Red
            End Select
        End If
        If ChkAxialPosition.Checked Then
            Dim AxialPositionClass = ShowAxialPositionTolerance(Classes, 70)
            Select Case AxialPositionClass
                Case 0
                    LabTolAPS.ForeColor = Color.Green
                    LabTolAPI.ForeColor = Color.Green
                    LabTolAPII.ForeColor = Color.Green
                    LabTolAPIII.ForeColor = Color.Green
                    LabTolAPC.ForeColor = Color.Green
                Case 1
                    LabTolAPS.ForeColor = Color.Red
                    LabTolAPI.ForeColor = Color.Green
                    LabTolAPII.ForeColor = Color.Green
                    LabTolAPIII.ForeColor = Color.Green
                    LabTolAPC.ForeColor = Color.Green
                Case 2
                    LabTolAPS.ForeColor = Color.Red
                    LabTolAPI.ForeColor = Color.Red
                    LabTolAPII.ForeColor = Color.Green
                    LabTolAPIII.ForeColor = Color.Green
                    LabTolAPC.ForeColor = Color.Green
                Case 3
                    LabTolAPS.ForeColor = Color.Red
                    LabTolAPI.ForeColor = Color.Red
                    LabTolAPII.ForeColor = Color.Red
                    LabTolAPIII.ForeColor = Color.Green
                    LabTolAPC.ForeColor = Color.Green
                Case 4
                    LabTolAPS.ForeColor = Color.Red
                    LabTolAPI.ForeColor = Color.Red
                    LabTolAPII.ForeColor = Color.Red
                    LabTolAPIII.ForeColor = Color.Red
                    LabTolAPC.ForeColor = Color.Green
                Case Else
                    LabTolAPS.ForeColor = Color.Red
                    LabTolAPI.ForeColor = Color.Red
                    LabTolAPII.ForeColor = Color.Red
                    LabTolAPIII.ForeColor = Color.Red
                    LabTolAPC.ForeColor = Color.Red
            End Select
        End If
    End Sub
    Private Sub ShowBladePitch(show As Boolean)
        Dim dtBladePitch As New DataTable()
        If JobDetails Is Nothing Then
            Return
        End If
        GridBladePitch.DataSource = Nothing
        GridBladebyRadius.DataSource = Nothing
        Dim PitchBasis As Double
        If ComboPitchBasis.Text = "Mean" And JobDetails.WheelPitch IsNot Nothing Then
            PitchBasis = JobDetails.WheelPitch
        ElseIf ComboPitchBasis.Text = "Marked" Then
            PitchBasis = JobDetails.Job.MarkedPitch
        ElseIf ComboPitchBasis.Text = "Desired" Then
            PitchBasis = JobDetails.Job.DesiredPitch
        End If
        Dim ToleranceTable As Tolerance = GetToleranceTable(Database, If(JobDetails?.ToleranceClass, "D"))
        Dim TotalPitchWheel As Double = 0.0
        Dim dtBladePitchByRadius As New DataTable()
        Dim colRadius As DataColumn = dtBladePitchByRadius.Columns.Add("Blade", GetType(Integer))
        Dim colPitch As DataColumn = dtBladePitch.Columns.Add("Blade", GetType(Double))
        Dim rowRadiusBlade As DataRow
        Dim rowBladeBlade As DataRow
        Dim x As Integer
        For x = 1 To Job?.PropellerBlades
            rowRadiusBlade = dtBladePitchByRadius.Rows.Add(x)
            rowBladeBlade = dtBladePitch.Rows.Add(x)
        Next
        GridBladePitch.DataSource = dtBladePitch
        dtBladePitch.Columns.Add("Avg Pitch", GetType(String))
        GridBladebyRadius.DataSource = dtBladePitchByRadius
        dtBladePitch.PrimaryKey = New DataColumn() {colPitch}
        dtBladePitchByRadius.PrimaryKey = New DataColumn() {colRadius}
        For Each row As DataRow In dtBladePitchByRadius.Rows
            Dim totalPitch As Double = 0.0
            Dim pitchCount As Integer = 0 ' Condensed these for loops into one to increase speed
            For Each rm As RadiusMeasurement In JobDetails?.RadiusMeasurements.Where(Function(r) r.BladeId = row.Item("Blade")).ToList().OrderBy(Function(r) r.Radius)
                Dim radiusPercent As String = Math.Round(CType(rm.Radius, Double)).ToString(STR_PARAM_DECIMAL_PLACES)
                rowRadiusBlade = If(dtBladePitchByRadius.Rows.Find(rm.BladeId), dtBladePitchByRadius.Rows.Add(rm.BladeId))
                colRadius = If(dtBladePitchByRadius.Columns(radiusPercent), dtBladePitchByRadius.Columns.Add(radiusPercent, GetType(String)))
                'Dim pitch As Double = GetRadiusMeasurementPitch(rm.CellMeasurements.ToList(), Job.TeExclusion, Job.LeExclusion)
                Dim pitch As Double = GetRadiusMeasurementPitch(rm.CellMeasurements, Job.TeExclusion, Job.LeExclusion)
                '
                rowRadiusBlade.Item(colRadius) = Math.Round(pitch, 2).ToString("F2")
                Dim textAvgBladePitchColor As ToleranceColor = CheckBladeRadiusPitch(ToleranceTable, pitch, PitchBasis, MinimumsApply) ' Check tolerance and adjust text color
                GridBladebyRadius.Rows(dtBladePitchByRadius.Rows.IndexOf(row)).Cells(colRadius.Ordinal).Style.ForeColor = ToColor(textAvgBladePitchColor)
                totalPitch += pitch
                pitchCount += 1
            Next
            colPitch = If(dtBladePitch.Columns("Avg Pitch"), dtBladePitch.Columns.Add("Avg Pitch", GetType(String)))
            Dim avgPitch As Double = totalPitch / pitchCount
            TotalPitchWheel += avgPitch
            Dim bladePitchColor As ToleranceColor = CheckBladePitch(ToleranceTable, avgPitch, PitchBasis, MinimumsApply) ' Check tolerance and adjust text color
            dtBladePitch.Rows(row.Item("Blade") - 1).Item("Avg Pitch") = Math.Round(avgPitch, 3).ToString("F3")
            GridBladePitch.Rows(row.Item("Blade") - 1).Cells(1).Style.ForeColor = Tolerances.ToColor(bladePitchColor)
        Next
        mJobDetails.WheelPitch = TotalPitchWheel / mJob.PropellerBlades
        Dim textWheelPitchColor As ToleranceColor = CheckWheelPitch(ToleranceTable, mJobDetails.WheelPitch, PitchBasis, True)
        TxtWheelPitch.ForeColor = Tolerances.ToColor(textWheelPitchColor)
        TxtWheelPitch.Text = mJobDetails.WheelPitch.ToString()
        GridBladePitch.Columns(0).Visible = False
        TLayoutGrids.ColumnStyles(1).Width = GridBladePitch.Columns(1).Width + 3
        For Each Col As DataGridViewColumn In GridBladebyRadius.Columns
            Col.SortMode = DataGridViewColumnSortMode.NotSortable
        Next
        rowRadiusBlade = dtBladePitchByRadius.Rows(0)
        For Each rm As RadiusMeasurement In JobDetails?.RadiusMeasurements.Where(Function(r) r.BladeId = 1).ToList().OrderBy(Function(r) r.Radius)
            Dim radiusPercent As String = Math.Round(CType(rm.Radius, Double)).ToString(STR_PARAM_DECIMAL_PLACES)
            colRadius = If(dtBladePitchByRadius.Columns(radiusPercent), dtBladePitchByRadius.Columns.Add(radiusPercent, GetType(String)))
            'Dim pitch As Double = GetRadiusMeasurementPitch(rm.CellMeasurements.ToList(), Job.TeExclusion, Job.LeExclusion)
            Dim pitch As Double = GetRadiusMeasurementPitch(rm.CellMeasurements, Job.TeExclusion, Job.LeExclusion)
            Dim textAvgBladePitchColor As ToleranceColor = CheckBladeRadiusPitch(ToleranceTable, pitch, PitchBasis, MinimumsApply) ' Check tolerance and adjust text color
            GridBladebyRadius.Rows(0).Cells(colRadius.Ordinal).Style.ForeColor = ToColor(textAvgBladePitchColor)
        Next
    End Sub

    Private Sub ShowJobInfo()
        ' Show the current Customer, Vessel, Job and Propeller info.
        Dim bsReferenceBlades As New BindingList(Of Integer)
        For i As Integer = 1 To mJob.PropellerBlades
            bsReferenceBlades.Add(i)
        Next
        Dim strBlades As String = If(Job?.PropellerBlades IsNot Nothing, $"Blades = {Job?.PropellerBlades}", "")
        Dim strDiameter As String = If(Job?.PropellerDiameter IsNot Nothing, $"Dia = {Job?.PropellerDiameter}", "")
        Dim strBore As String = If(Job?.PropellerBore IsNot Nothing, $"Bore = {Job?.PropellerBore}", "")
        TxtJobNumber.Text = Job?.JobNumber.ToString()
        TxtCustomer.Text = Job?.Vessel?.Customer?.CustomerName
        TxtVessel.Text = Job?.Vessel?.VesselName
        TxtManufacturer.Text = If(Job?.PropellerManufacturer?.ManufacturerName, "")
        TxtStyle.Text = If(Job?.PropellerStyleNavigation?.Style1, "")
        TxtMaterial.Text = If(Job?.PropellerMaterialNavigation?.Material1, "")
        TxtBlades.Text = strBlades
        TxtDiameter.Text = strDiameter
        TxtBore.Text = strBore
        ComboReferenceBlade.DataSource = bsReferenceBlades
        ComboReferencePoint.SelectedItem = "LE"
        ComboReferenceRadius.DataSource = ReferenceRadiiGet(ComboReferenceBlade.SelectedValue)
        ComboPitchBasis.SelectedItem = "Marked"
        ComboTolerance.SelectedItem = GetToleranceTable(Database, JobDetails?.ToleranceClass)
        CmdHome.Visible = True
        ShowPitchBasis()
    End Sub

    Private Sub ShowJobDetailsInfo()
        ' Update any controls that consume data from the current JobDetail record.
        ComboReferenceRadius.DataSource = ReferenceRadiiGet(ComboReferenceBlade.SelectedValue)
        ShowBladePitch(True)
        ShowTrack()
        ShowBladePlot()
        ShowTolerances(MinimumsApply, AllowProgressivePitch)
    End Sub

    Private Sub ShowPitchBasis()
        Select Case ComboPitchBasis.Text
            Case "Mean"
                If TxtWheelPitch.Text <> "NaN" Then
                    TxtBasis.Text = TxtWheelPitch.Text
                Else
                    TxtBasis.Text = Job?.MarkedPitch.ToString()
                End If
            Case "Marked"
                TxtBasis.Text = Job?.MarkedPitch.ToString()
            Case "Desired"
                TxtBasis.Text = Job?.DesiredPitch.ToString()
            Case Else
                Return
        End Select
    End Sub

    Private Sub ShowTrack()
        If ComboReferenceRadius.Items.Count > 0 Then
            If ComboReferenceRadius.SelectedValue Is Nothing Then
                ComboReferenceRadius.SelectedIndex = 0
            End If
        Else
            ChartAngularPosition1.Data = Nothing
            ChartBladeHeight1.Data = Nothing
            Return
        End If

        ChartBladeHeight1.ReferenceBlade = ComboReferenceBlade.SelectedIndex + 1
        ChartBladeHeight1.ReferencePoint = ComboReferencePoint.SelectedValue
        ChartBladeHeight1.ReferenceRadius = ComboReferenceRadius.SelectedValue
        ChartBladeHeight1.Data = JobDetails

        ChartAngularPosition1.ReferenceBlade = ComboReferenceBlade.SelectedIndex + 1
        ChartAngularPosition1.ReferencePoint = ComboReferencePoint.SelectedValue
        ChartAngularPosition1.ReferenceRadius = ComboReferenceRadius.SelectedValue
        ChartAngularPosition1.Data = JobDetails

        ShowRake(ComboReferenceBlade.SelectedIndex + 1, ComboReferencePoint.Text)
    End Sub

    Private Sub ShowBladePlot() '' need to replace this with the display control Plot chart so as to reduce code in this file
        If JobDetails Is Nothing Then Return
        ChartPlot1.MinimumsApply = MinimumsApply
        ChartPlot1.AllowProgressivePitch = AllowProgressivePitch
        ChartPlot1.AngDeviation = ChkPlotAngularDeviation.Checked
        ChartPlot1.Basis = ComboPitchBasis.Text
        If TxtBasis.Text <> "" Then
            ChartPlot1.CustBasis = Double.Parse(TxtBasis.Text)
        Else
            ChartPlot1.CustBasis = 0
        End If
        ChartPlot1.TolClass = GetToleranceTable(Database, If(JobDetails?.ToleranceClass, "S"))
        ChartPlot1.Data = JobDetails
    End Sub

    Private Sub ShowRake(Blade As Integer, refpoint As String)
        If mJobDetails Is Nothing Then Return
        Dim innerRad As RadiusMeasurement = mJobDetails.RadiusMeasurements.Where(Function(r) r.BladeId = Blade).OrderBy(Function(r) r.Radius).FirstOrDefault()
        Dim outerRad As RadiusMeasurement = mJobDetails.RadiusMeasurements.Where(Function(r) r.BladeId = Blade).OrderBy(Function(r) r.Radius).LastOrDefault()
        If innerRad Is Nothing Or outerRad Is Nothing Then
            TxtRake.Text = "0.0 °"
            Return
        End If
        If outerRad.Radius.Value = innerRad.Radius.Value Then
            TxtRake.Text = "0.0 °"
            Return
        End If
        Dim innerDepth As Double
        Dim outerDepth As Double
        Dim Radius As Double = (mJobDetails.Job.PropellerDiameter / 2)
        Select Case refpoint
            Case "LE"
                innerDepth = innerRad.CellMeasurements.LastOrDefault().Depth.Value
                outerDepth = outerRad.CellMeasurements.LastOrDefault().Depth.Value
            Case "Mid"
                innerDepth = GetChordMidDepth(innerRad.CellMeasurements)
                outerDepth = GetChordMidDepth(outerRad.CellMeasurements)
            Case "TE"
                innerDepth = innerRad.CellMeasurements.FirstOrDefault().Depth.Value
                outerDepth = outerRad.CellMeasurements.FirstOrDefault().Depth.Value
            Case Else
                TxtRake.Text = "0.0 °"
                Return
        End Select
        If innerDepth <> 0 And outerDepth <> 0 Then
            Dim rise = innerDepth - outerDepth
            Dim run = (Radius * outerRad.Radius.Value / 100) - (Radius * innerRad.Radius.Value / 100)
            If run <> 0 Then
                Dim rake As Double = (180 / Math.PI) * Math.Atan(rise / run)
                TxtRake.Text = rake.ToString("F2")
                Return
            End If
        End If
    End Sub
    Private Sub SetFocus()
        If Not HomeSet Then
            CmdHome.Select()
        Else
            ChkScan.Select()
        End If
    End Sub
#End Region
#Region "Event Handlers"
    Private Sub ChkScan_CheckedChanged(sender As Object, e As EventArgs) Handles ChkScan.CheckedChanged
        Try
            Scanning = ChkScan.Checked
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, STR_TITLE_APPLICATION_ERROR)
        End Try
    End Sub

    Private Sub CmdHome_Click(sender As Object, e As EventArgs) Handles CmdHome.Click
        HomeSet = True
        If HomeSet = False Then
            Exit Sub
        End If
        SetFocus()
        Try
            EncoderStatusStrip1.ResetAll()
        Catch ex As Exception
            MessageBox.Show("Error homing encoders: " & ex.Message, STR_TITLE_APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CmdSetTip_Click(sender As Object, e As EventArgs) Handles CmdSetTip.Click

    End Sub

    Private Sub ComboPitchBasis_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboPitchBasis.SelectedIndexChanged
        ShowPitchBasis()
        SetFocus()
    End Sub
    Private Sub TxtBasis_TextChanged(sender As Object, e As EventArgs) Handles TxtBasis.TextChanged
        ShowJobDetailsInfo() '''need to make this allow the pitch basis text box allow custom to and return a actual value
    End Sub

    Private Sub ComboReferenceBlade_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboReferenceBlade.SelectedIndexChanged
        Dim selrad As Integer = ComboReferenceRadius.SelectedIndex
        ComboReferenceRadius.DataSource = ReferenceRadiiGet(ComboReferenceBlade.SelectedIndex + 1).Order().ToList()
        If ComboReferenceRadius.Items.Count = 0 Then Return
        If selrad <> 0 And selrad <> -1 And selrad <= ComboReferenceRadius.Items.Count Then
            ComboReferenceRadius.SelectedIndex = selrad
        ElseIf ComboReferenceBlade.Items.Count = 0 Then
            ComboReferenceBlade.SelectedIndex = Nothing
        Else
            ComboReferenceRadius.SelectedIndex = 0
        End If
        SetFocus()
    End Sub

    Private Sub ComboReferencePoint_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboReferencePoint.SelectedIndexChanged
        ShowTrack()
        SetFocus()
    End Sub

    Private Sub ComboReferenceRadius_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboReferenceRadius.SelectedIndexChanged
        ShowTrack()
        SetFocus()
    End Sub

    Private Sub ComboTolerance_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboTolerance.SelectedIndexChanged
        SelectedTolerance = DirectCast(ComboTolerance.SelectedItem, Tolerance).ToleranceClass
        DataGridJobDetails.Refresh()
        ShowJobDetailsInfo()
    End Sub

    Private Sub DataGridJobDetails_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles DataGridJobDetails.MouseDoubleClick
        If Current IsNot Nothing Then
            Dim frm As FrmReports = DirectCast(ShowForm(Of FrmReports)(Me.ScopeFactory, Me.User), FrmReports)

            frm.JobDetails = Current
        End If
    End Sub

    Private Sub Encoders_EncoderEvent(sender As Object, e As EncoderEventArgs)
        ' Handles EncoderStatusStrip events so we can update our controls accordingly.
        FormEnable(EncoderStatusStrip1.Hardware, Me.Job)
    End Sub

    Private Sub EncoderStatusStrip1_Load(sender As Object, e As EventArgs)
        mScanIncrement = EncoderStatusStrip1.Hardware.Workstation.ScanIncrement
    End Sub

    Protected Sub Form_Closing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        On Error Resume Next
        EncoderStatusStrip1.TimerOn = False
        DataGridJobDetails.EndEdit()
        DataGridJobDetails.DataSource = Nothing
    End Sub

    Private Sub Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' Initialize form controls. This method needs to initialize all form controls
            ' based on some predefined "states". For example: if no encoders are detected,
            ' they're not initialized or in an error state, then disable all controls that 
            ' can access the encoders. 
            DataGridJobDetails.AutoGenerateColumns = False
            Me.WindowState = FormWindowState.Maximized

            ' Retrieve required data and initialize the Navigator.
            If Me.Database IsNot Nothing Then BindDataSources()
            Navigator = RecordNavigationBar1
            If Me.Database IsNot Nothing Then Navigator.Database = Me.Database
            If Me.ServiceProvider IsNot Nothing Then Navigator.ServiceProvider = Me.ServiceProvider
            Navigator = RecordNavigationBar1
            Navigator.BoundControls = New List(Of Control) From {DataGridJobDetails}
            RecordNavigationBar1.MasterSource = JobDetailsBindingSource
            BindDataSources()

            Dim Offsets As New List(Of String) From {"0 In Hub", "1 In Hub", "2 In Hub", "100 mm Hub"}
            Dim Offsets2 As New List(Of String) From {"0 In Rad", "1 In Rad", "2 In Rad", "100 mm Rad"}

            ComboOffsetHub.DataSource = Offsets
            ComboOffsetnothub.DataSource = Offsets2

            ' EncoderStatusStrip1 handles the encoder hardware and its controls automatically. 
            ' It raises events notifying clients of anything relevant. These events can, for
            ' instance, be used to update this form's state and take periodic measurements.
            ' See Encoders_EncoderEvent() and ScanTimer_Tick() for examples.
            AddHandler EncoderStatusStrip1.Load, AddressOf EncoderStatusStrip1_Load
            AddHandler EncoderStatusStrip1.EncoderEvent, AddressOf Encoders_EncoderEvent
            AddHandler EncoderStatusStrip1.Timer.Tick, AddressOf ScanTimer_Tick
            AddHandler Navigator.NavigationEvent, AddressOf Navigator_NavigationEvent
            'Dim a As Boolean = Me.Hardware?.Encoders?.Initialized
            'EncoderStatusStrip1.TimerOn = Me.Hardware?.Encoders?.Initialized
            ChkMinimumsApply.Checked = True
            SetFocus()
            AddHandler ChartBladeHeight1.Chart1.MouseDoubleClick, AddressOf ChartBladeHeight1_DoubleClick
            AddHandler ChartAngularPosition1.Chart1.MouseDoubleClick, AddressOf ChartAngularPosition1_DoubleClick
            FormEnable(EncoderStatusStrip1.Hardware, Me.Job)
        Catch ex As Exception
            MessageBox.Show("Error loading measurements form: " & ex.Message & " " & ex.Source, STR_TITLE_APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub JobDetailsBindingSource_AddingNew(sender As Object, e As AddingNewEventArgs) Handles JobDetailsBindingSource.AddingNew
        Try
            Dim newJobDetail As JobDetail = If(mNewJobDetail, CreateNewJobDetail())
            e.NewObject = newJobDetail
            If newJobDetail.MeasurementType Is Nothing Then
                newJobDetail.MeasurementType = Database.MeasurementTypes.FirstOrDefault()
            End If
            newJobDetail.ToleranceClass = ComboTolerance.Text
            newJobDetail.PerformedBy = Me.User.Id
            newJobDetail.Job = Job
            Database.JobDetails.Add(newJobDetail)
        Catch ex As Exception
            MessageBox.Show("Error adding new job details record: " & ex.Message, STR_TITLE_APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub JobDetailsBindingSource_CurrentChanged(sender As Object, e As EventArgs) Handles JobDetailsBindingSource.CurrentChanged
        If mJobDetails IsNot Current Then
            mJobDetails = Current
            If JobDetails IsNot Nothing Then
                ShowJobDetailsInfo()
            End If
        End If
    End Sub

    Private Sub Navigator_NavigationEvent(sender As Object, e As NavigationEventArgs)
        ' Handles Navigator events so we can update our controls accordingly.
        Select Case e.EventName
            Case "AddNew"
                ' Disable PanelMeasurements when the user is adding a new JobDetails record.
                PanelMeasurements.Enabled = False
                PanelGrids.Enabled = False
                PanelPlot.Enabled = False
                PanelTrack.Enabled = False
                PanelLocalPitchDetails.Enabled = False
            Case "Delete"
                If DeleteConfirm() Then
                    DeleteJobDetail()
                    HomeSet = False
                    SetFocus()
                End If
            Case "Editing"
                ' Disable the PanelMeasurements when the user is editing the JobDetails record. 
                PanelMeasurements.Enabled = False
                PanelGrids.Enabled = False
                PanelPlot.Enabled = False
                PanelTrack.Enabled = False
                PanelLocalPitchDetails.Enabled = False
                Navigator.CmdSave.Select()
            Case "FilterOff"
            Case "FilterOn"
            Case "Find"
            Case "GotoFirst", "GotoNext", "GotoPrev"
                ShowJobDetailsInfo()
                ShowTrack()
            Case "GotoLast"
            Case "Save"
                ' Refresh any open database forms affected by our changes and enable PanelMeasurements.
                PanelMeasurements.Enabled = True
                PanelGrids.Enabled = True
                PanelPlot.Enabled = True
                PanelTrack.Enabled = True
                PanelLocalPitchDetails.Enabled = True
                SetFocus()
                Navigator.CmdRefresh.PerformClick()
            Case "Undo"
                ' Enable the PanelMeasurements when the user has cancelled the JobDetails record changes.
                If Me.Current IsNot Nothing Then
                    ShowJobDetailsInfo()
                    PanelMeasurements.Enabled = True
                    PanelGrids.Enabled = True
                    PanelPlot.Enabled = True
                    PanelTrack.Enabled = True
                    PanelLocalPitchDetails.Enabled = True
                End If
            Case Else
                PanelMeasurements.Enabled = True
                PanelGrids.Enabled = True
                PanelPlot.Enabled = True
                PanelTrack.Enabled = True
                PanelLocalPitchDetails.Enabled = True
        End Select
    End Sub

    Private Sub ScanTimer_Tick(sender As Object, e As EventArgs)
        Try
            If Scanning Then
                MeasurementsGet(mLastScannedAngle)
                If mSampleCount = kMaxSamplesPerScan Then Scanning = False
            Else
                MeasurementsGet()
            End If
        Catch ex As Exception
            Scanning = False
            MessageBox.Show("Error getting measurements from the encoders: " & ex.Message, STR_TITLE_APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub TxtCustomer_DoubleClick(sender As Object, e As EventArgs) Handles TxtCustomer.DoubleClick
        If Job IsNot Nothing Then
            Dim frm As FrmCustomers = DirectCast(ShowForm(Of FrmCustomers)(Me.ScopeFactory, User), FrmCustomers)

            frm.Find(Job.Vessel.Customer)
        End If
    End Sub

    Private Sub TxtJobNumber_DoubleClick(sender As Object, e As EventArgs) Handles TxtJobNumber.DoubleClick
        If Job IsNot Nothing Then
            Dim frm As FrmJobs2 = DirectCast(ShowForm(Of FrmJobs2)(Me.ScopeFactory, User), FrmJobs2)

            frm.Hardware = Hardware
            frm.Find(Me.Job)
        End If
    End Sub
    Private Sub TxtManufacturer_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles TxtManufacturer.MouseDoubleClick
        If Job IsNot Nothing Then
            Dim frm As FrmManufacturers = DirectCast(ShowForm(Of FrmManufacturers)(Me.ScopeFactory, User), FrmManufacturers)

            frm.Find(Job?.PropellerManufacturer)
        End If
    End Sub

    Private Sub TxtVessel_DoubleClick(sender As Object, e As EventArgs) Handles TxtVessel.DoubleClick
        If Job IsNot Nothing Then
            Dim frm As FrmVessels = DirectCast(ShowForm(Of FrmVessels)(Me.ScopeFactory, User), FrmVessels)

            frm.Find(Job?.Vessel)
        End If
    End Sub

    Private Sub Form1_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        EncoderStatusStrip1.TimerOn = True
    End Sub

    Private Sub Form1_Deactivate(sender As Object, e As EventArgs) Handles MyBase.Deactivate
        EncoderStatusStrip1.TimerOn = False
    End Sub

    Private Sub ChkLocalPitch_CheckedChanged(sender As Object, e As EventArgs) Handles ChkLocalPitch.CheckedChanged
        If ChkLocalPitch.Checked Then
            ChkLocalPitch.ForeColor = Color.White
            ShowTolerances(MinimumsApply, AllowProgressivePitch)
        Else
            ChkLocalPitch.ForeColor = Color.DimGray
            LabTolLPS.ForeColor = Color.DimGray
            LabTolLPI.ForeColor = Color.DimGray
            LabTolLPII.ForeColor = Color.DimGray
            LabTolLPC.ForeColor = Color.DimGray
        End If
        SetFocus()
    End Sub

    Private Sub ChkMeanPitchRadius_CheckedChanged(sender As Object, e As EventArgs) Handles ChkMeanPitchRadius.CheckedChanged
        If ChkMeanPitchRadius.Checked Then
            ChkMeanPitchRadius.ForeColor = Color.White
            ShowTolerances(MinimumsApply, AllowProgressivePitch)
        Else
            LabTolMPRS.ForeColor = Color.DimGray
            LabTolMPRI.ForeColor = Color.DimGray
            LabTolMPRII.ForeColor = Color.DimGray
            LabTolMPRIII.ForeColor = Color.DimGray
            LabTolMPRC.ForeColor = Color.DimGray
        End If
        SetFocus()
    End Sub

    Private Sub ChkMeanPitchBlade_CheckedChanged(sender As Object, e As EventArgs) Handles ChkMeanPitchBlade.CheckedChanged
        If ChkMeanPitchBlade.Checked Then
            ChkMeanPitchBlade.ForeColor = Color.White
            ShowTolerances(MinimumsApply, AllowProgressivePitch)
        Else
            LabTolMPBS.ForeColor = Color.DimGray
            LabTolMPBI.ForeColor = Color.DimGray
            LabTolMPBII.ForeColor = Color.Black
            LabTolMPBIII.ForeColor = Color.Black
            LabTolMPBC.ForeColor = Color.DimGray
        End If
        SetFocus()
    End Sub

    Private Sub ChkMeanPitchPropeller_CheckedChanged(sender As Object, e As EventArgs) Handles ChkMeanPitchPropeller.CheckedChanged
        If ChkMeanPitchPropeller.Checked Then
            ChkMeanPitchPropeller.ForeColor = Color.White
            ShowTolerances(MinimumsApply, AllowProgressivePitch)
        Else
            LabTolMPPS.ForeColor = Color.DimGray
            LabTolMPPI.ForeColor = Color.DimGray
            LabTolMPPII.ForeColor = Color.DimGray
            LabTolMPPIII.ForeColor = Color.DimGray
            LabTolMPPC.ForeColor = Color.DimGray
        End If
        SetFocus()
    End Sub

    Private Sub ChkAngularDeviation_CheckedChanged(sender As Object, e As EventArgs) Handles ChkAngularDeviation.CheckedChanged
        If ChkAngularDeviation.Checked Then
            ChkAngularDeviation.ForeColor = Color.White
            ShowTolerances(MinimumsApply, AllowProgressivePitch)
        Else
            LabTolADS.ForeColor = Color.DimGray
            LabTolADI.ForeColor = Color.DimGray
            LabTolADII.ForeColor = Color.DimGray
            LabTolADIII.ForeColor = Color.DimGray
            LabTolADC.ForeColor = Color.Black
        End If
        SetFocus()
    End Sub
    Private Sub ChkAxialPosition_CheckedChanged(sender As Object, e As EventArgs) Handles ChkAxialPosition.CheckedChanged
        If ChkAxialPosition.Checked Then
            ChkAxialPosition.ForeColor = Color.White
            ShowTolerances(MinimumsApply, AllowProgressivePitch)
        Else
            LabTolAPS.ForeColor = Color.DimGray
            LabTolAPI.ForeColor = Color.DimGray
            LabTolAPII.ForeColor = Color.DimGray
            LabTolAPIII.ForeColor = Color.DimGray
            LabTolAPC.ForeColor = Color.DimGray
        End If
        SetFocus()
    End Sub

    Private Sub CmdSetRef_Click(sender As Object, e As EventArgs) Handles CmdSetRef.Click
        If mJobDetails Is Nothing Then
            Return
        End If
        Dim refcell As New ReferenceCell
        mJobDetails.ReferenceCell = refcell
        Dim userInput As String = "Ref Blade " + TxtBlade.Text + " Radius " + TxtRadiusPercent.Text
        mJobDetails.ReferenceCell.ReferenceDescription = userInput
        mJobDetails.ReferenceCell.ReferenceRadius = Double.Parse(TxtRadius.Text.Remove(TxtRadius.Text.IndexOf(CType(" ", Char))))
        mJobDetails.ReferenceCell.ReferenceAngle = Double.Parse(TxtAngle.Text.Remove(TxtAngle.Text.IndexOf(CType(" ", Char))))
        mJobDetails.ReferenceCell.ReferenceDepth = Double.Parse(TxtDepth.Text.Remove(TxtDepth.Text.IndexOf(CType(" ", Char))))
        Database.SaveChanges()
        TxtStatus.Text = userInput
        SetFocus()
    End Sub

    Private Sub CmdGetRef_Click(sender As Object, e As EventArgs) Handles CmdGetRef.Click
        If mJobDetails Is Nothing Then
            Return
        End If
        Dim res As DialogResult = MessageBox.Show("This will set the encoder counts to the Reference Cell values. The reference point was recorded at " + mJobDetails.ReferenceCell.ReferenceDescription, "Reference Point", MessageBoxButtons.OKCancel)
        If res = DialogResult.Cancel Then
            Return
        End If
        'resetting counts is multiplying by calibrations
        Dim refRadius As Double = mJobDetails.ReferenceCell.ReferenceRadius
        Dim refAngle As Double = mJobDetails.ReferenceCell.ReferenceAngle
        Dim refDepth As Double = mJobDetails.ReferenceCell.ReferenceDepth

        If Math.Round(refRadius) <> Math.Round(Double.Parse(TxtRadius.Text.Remove(TxtRadius.Text.IndexOf(CType(" ", Char))))) Then
            Hardware.Encoders.SetEncoderCount(1, CInt(refRadius * Hardware.Encoders.RadiusCalibration))
        End If
        If Math.Round(refAngle) <> Math.Round(Double.Parse(TxtAngle.Text.Remove(TxtAngle.Text.IndexOf(CType(" ", Char))))) Then
            Hardware.Encoders.SetEncoderCount(0, CInt(refAngle * Hardware.Encoders.AngleCalibration))
        End If
        If Math.Round(refDepth) <> Math.Round(Double.Parse(TxtDepth.Text.Remove(TxtDepth.Text.IndexOf(CType(" ", Char))))) Then
            Hardware.Encoders.SetEncoderCount(2, CInt(refDepth * Hardware.Encoders.DepthCalibration))
        End If
        If Not mHomeSet Then
            mHomeSet = True
            CmdHome.Text = "Re-Home"
            ChkScan.BackColor = Color.ForestGreen
            ChkScan.Enabled = True
        End If
        TxtStatus.Text = "Ready to Scan"
        SetFocus()
    End Sub

    Private Sub CmdComparisonForm_Click(sender As Object, e As EventArgs) Handles CmdComparisonForm.Click
        If Current IsNot Nothing Then
            Dim frm As FrmComparison = DirectCast(ShowForm(Of FrmComparison)(Me.ScopeFactory, Me.User), FrmComparison)

            frm.JobDetailsBindingSource.DataSource = Current
            frm.Hardware = Hardware
        End If
    End Sub

    Private Sub FrmMeasurements_ResizeEnd(sender As Object, e As EventArgs) Handles MyBase.ResizeEnd, MyBase.Resize
        'ChartPlot1.Width = ChartPlot1.Height
        If ChartPlot1 IsNot Nothing Then
            Dim halfheight As Integer = (ChartPlot1.Width - ChartPlot1.Height) / 2
            ChartPlot1.Margin = New Padding(halfheight, 0, halfheight, 0)
        End If
    End Sub

    Private Sub FrmMeasurements_StyleChanged(sender As Object, e As EventArgs) Handles MyBase.StyleChanged
        'ChartPlot1.Width = ChartPlot1.Height
        If ChartPlot1 IsNot Nothing Then
            Dim halfheight As Integer = (ChartPlot1.Width - ChartPlot1.Height) / 2
            ChartPlot1.Margin = New Padding(halfheight, 0, halfheight, 0)
        End If
    End Sub

    Private Sub ChkPlotAngularDeviation_CheckedChanged(sender As Object, e As EventArgs) Handles ChkPlotAngularDeviation.CheckedChanged
        ShowBladePlot()
    End Sub

    Private Sub ChkMinimumsApply_CheckedChanged(sender As Object, e As EventArgs) Handles ChkMinimumsApply.CheckedChanged
        MinimumsApply = ChkMinimumsApply.Checked
    End Sub

    Private Sub CmdPrintClassS_Click(sender As Object, e As EventArgs) Handles CmdPrintClassS.Click
        Dim frm As FrmInspectPopUp = ShowForm(Of FrmInspectPopUp)(Me.ScopeFactory, Me.User)
        frm.Open(JobDetails, GetToleranceTable(Database, "S"), ComboPitchBasis.Text, AllowProgressivePitch, MinimumsApply)
    End Sub

    Private Sub CmdPrintClassI_Click(sender As Object, e As EventArgs) Handles CmdPrintClassI.Click
        Dim frm As FrmInspectPopUp = ShowForm(Of FrmInspectPopUp)(Me.ScopeFactory, Me.User)
        frm.Open(JobDetails, GetToleranceTable(Database, "I"), ComboPitchBasis.Text, AllowProgressivePitch, MinimumsApply)
    End Sub

    Private Sub CmdPrintClassII_Click(sender As Object, e As EventArgs) Handles CmdPrintClassII.Click
        Dim frm As FrmInspectPopUp = ShowForm(Of FrmInspectPopUp)(Me.ScopeFactory, Me.User)
        frm.Open(JobDetails, GetToleranceTable(Database, "II"), ComboPitchBasis.Text, AllowProgressivePitch, MinimumsApply)
    End Sub

    Private Sub CmdPrintClassIII_Click(sender As Object, e As EventArgs) Handles CmdPrintClassIII.Click
        Dim frm As FrmInspectPopUp = ShowForm(Of FrmInspectPopUp)(Me.ScopeFactory, Me.User)
        frm.Open(JobDetails, GetToleranceTable(Database, "III"), ComboPitchBasis.Text, AllowProgressivePitch, MinimumsApply)
    End Sub

    Private Sub CmdPrintClassCustom_Click(sender As Object, e As EventArgs) Handles CmdPrintClassCustom.Click

    End Sub

    Private Sub ChkAllowProgressivePitch_CheckedChanged(sender As Object, e As EventArgs) Handles ChkAllowProgressivePitch.CheckedChanged
        AllowProgressivePitch = ChkAllowProgressivePitch.Checked
        ShowBladePlot()
        ShowBladePitch(True)
        ShowTolerances(MinimumsApply, AllowProgressivePitch)
        SetFocus()
    End Sub

    Private Sub ComboOffsetHub_TextUpdate(sender As Object, e As EventArgs) Handles ComboOffsetHub.TextUpdate, ComboOffsetHub.TextChanged
        If ComboOffsetHub.Text = "" Or ComboOffsetHub.Text Is Nothing Then Return
        Dim OFF As Double = 0
        If Not Double.TryParse(ComboOffsetHub.Text, OFF) Then Return
        If OFF >= JobDetails.Job.PropellerDiameter Then
            OFF *= kMmToInch
        End If
        HubOffset = OFF
    End Sub

    Private Sub ComboOffsetnothub_TextUpdate(sender As Object, e As EventArgs) Handles ComboOffsetnothub.TextUpdate, ComboOffsetnothub.TextChanged
        If ComboOffsetnothub.Text = "" Or ComboOffsetnothub.Text Is Nothing Then Return
        Dim OFF As Double = 0
        If Not Double.TryParse(ComboOffsetnothub.Text, OFF) Then Return
        If OFF >= JobDetails.Job.PropellerDiameter Then
            OFF *= kMmToInch
        End If
        LeftOffset = OFF
    End Sub

    Private Sub FrmMeasurements_Enter(sender As Object, e As EventArgs) Handles MyBase.Enter
        SetFocus()
    End Sub

    Private Sub ChartBladeHeight1_DoubleClick(sender As Object, e As EventArgs) Handles ChartBladeHeight1.MouseDoubleClick
        Dim frm As FrmInspectPopUp = ShowForm(Of FrmInspectPopUp)(Me.ScopeFactory, Me.User)
        frm.Open(JobDetails, GetToleranceTable(Database, JobDetails.ToleranceClass), ChartBladeHeight1.BladeCount, ChartBladeHeight1.ReferenceRadius, ChartBladeHeight1.ReferencePoint, "BladeHeight")
    End Sub

    Private Sub ChartAngularPosition1_DoubleClick(Sender As Object, e As EventArgs) Handles ChartAngularPosition1.MouseDoubleClick
        Dim frm As FrmInspectPopUp = ShowForm(Of FrmInspectPopUp)(Me.ScopeFactory, Me.User)
        frm.Open(JobDetails, GetToleranceTable(Database, JobDetails.ToleranceClass), ChartAngularPosition1.BladeCount, ChartAngularPosition1.ReferenceRadius, ChartAngularPosition1.ReferencePoint, "AngularPosition")
    End Sub

    Private Sub ChartBladeHeight1_Load(sender As Object, e As EventArgs) Handles ChartBladeHeight1.Load
        ChartBladeHeight1.ContextMenuStrip.Enabled = False
    End Sub

    Private Sub CmdGraphForm_Click(sender As Object, e As EventArgs) Handles CmdGraphForm.Click
        Dim frm As FrmGraph = DirectCast(ShowForm(Of FrmGraph)(Me.ScopeFactory, Me.User), FrmGraph)
        frm.JobDetails = Current
        frm.JobDetailsBindingSource.DataSource = Current
        frm.ComboBasis.SelectedItem = ComboPitchBasis.SelectedItem
        frm.Hardware = Hardware
        frm.HomeSet = HomeSet
        frm.ChkAllowProgressivePitch.Checked = AllowProgressivePitch
    End Sub


    Private Sub CmdInspectForm_Click(sender As Object, e As EventArgs) Handles CmdInspectForm.Click
        Dim frm As FrmInspection = DirectCast(ShowForm(Of FrmInspection)(Me.ScopeFactory, Me.User), FrmInspection)

        frm.JobDetails = Current
        frm.JobDetailsBindingSource.DataSource = Current
        frm.Hardware = Hardware
        frm.ChkAllowProgressivePitch.Checked = AllowProgressivePitch
        frm.HomeSet = HomeSet
    End Sub
#End Region
End Class