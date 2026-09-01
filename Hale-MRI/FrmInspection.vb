Imports System.ComponentModel
Imports LibDatabase.BindingSourceExtensions
Imports LibDatabase
Imports LibDatabase.Contexts
Imports LibDatabase.Models
Imports Microsoft.EntityFrameworkCore
Imports Microsoft.EntityFrameworkCore.Metadata.Internal
Imports Microsoft.Extensions.DependencyInjection

Public Class FrmInspection
    Inherits FrmDatabaseForm

#Region "Private Members"
    Private mJobDetails As JobDetail                            ' The current JobDetail record
    Private mJob As Job                                         ' The Job the current JobDetail record belongs to.
    Private mMasterSource As BindingSource = Nothing            ' The form's "master" BindingSource.
    Private mNavigator As RecordNavigationBar = Nothing         ' The form's RecordNavigationBar.
    Private mHardware As WorkstationEncoders         ' Hardware member used to hold encoders so that they aren't reset or changed when forms change
    Private mTolerance As String
    Public HomeSet As Boolean = False
    Private ReadOnly mDatabase As HaleMRIContext
    Private ReadOnly mServiceProvider As IServiceProvider   ' The current database ServiceProvider reference.
#End Region
#Region "Constructers"
    ' Visual Studio Designer uses this.
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
    ''' Returns the currently selected JobDetail,
    ''' or Nothing if there is no selected record.
    ''' </summary>
    Public ReadOnly Property Current As JobDetail
        Get
            Return JobDetailsBindingSource.Current(Of JobDetail)
        End Get

        'Public ReadOnly Property Database As HaleMRIContext
        '    Get
        '        Return mDatabase
        '    End Get
        'End Property
    End Property
    ''' <summary>
    ''' Gets/sets the encoder hardware used by the form.
    ''' </summary>
    ''' <returns></returns>
    Public Property Hardware As WorkstationEncoders
        Get
            Return mHardware
        End Get
        Set(value As WorkstationEncoders)
            mHardware = value
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
                If Me.Database IsNot Nothing Then JobDetailsBindingSource.DataSource = GetMeasurementData(mJob)
                ShowJobInfo()
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
            If mJobDetails IsNot Nothing Then
                If Me.Database IsNot Nothing Then JobDetailsBindingSource.DataSource = GetMeasurementData(mJobDetails)
                ShowJobDetailsInfo()
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
#End Region
#Region "Private Interface"
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
    Private Sub ShowJobInfo()
        ShowJobDetailsInfo()
    End Sub
    Private Sub ShowJobDetailsInfo()
        ChordlengthTableUpdate()
        ShowTolerances(MinimumsApply, AllowProgressivePitch)
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
    Private Sub ChordlengthTableUpdate()
        If JobDetails Is Nothing Then Exit Sub
        Dim dTable As DataTable = New DataTable
        Dim colRadius As DataColumn = dTable.Columns.Add("Blade", GetType(Integer))
        Dim dRow As DataRow
        For x As Integer = 1 To Job?.PropellerBlades
            dRow = dTable.Rows.Add(x)
        Next
        dGridChordLengths.DataSource = dTable
        dTable.PrimaryKey = New DataColumn() {colRadius}
        For Each row As DataRow In dTable.Rows
            For Each rm As RadiusMeasurement In JobDetails?.RadiusMeasurements.Where(Function(r) r.BladeId = row.Item("Blade")).ToList()
                Dim radius As String = Math.Round(CType(rm.Radius, Double)).ToString("F2")
                dRow = If(dTable.Rows.Find(rm.BladeId), dTable.Rows.Add(rm.BladeId))
                colRadius = If(dTable.Columns(radius), dTable.Columns.Add(radius))
                Dim ChordLength As Double = GetChordLength(rm.CellMeasurements, Job?.PropellerDiameter, Math.Round(rm.Radius.Value))
                dRow.Item(colRadius) = Math.Round(ChordLength, 2).ToString("F2")
            Next
        Next
    End Sub

    Protected Overrides Sub OnDataSyncNotification(entityType As Type, primaryKey As Object)
        ' Load any required data from the database into the LocalView.
    End Sub
#Region "Tolerance"
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
        If mJobDetails Is Nothing Or ChkISO484.Checked = False Then Exit Sub
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
#End Region
#End Region
#Region "Event Handler"
    ' Sam, please see TODO comments in FrmInspectPopUp. I commented out all the Sub New()
    ' overloads and added only the two needed at design-time and the DI at run-time. I
    ' recommend you add Public Subs that take the required parameters and call them
    ' immediately after the ShowForm() calls below.
    Private Sub CmdPrintClassS_Click(sender As Object, e As EventArgs) Handles CmdPrintClassS.Click
        ShowForm(Of FrmInspectPopUp)(Me.ScopeFactory, Me.User)
        'Dim inspect As New FrmInspectPopUp(JobDetails, GetToleranceTable(Database, "S"), Job?.DesiredPitch, AllowProgressivePitch, MinimumsApply)
        'inspect.Show()
    End Sub
    Private Sub CmdPrintClassI_Click(sender As Object, e As EventArgs) Handles CmdPrintClassI.Click
        ShowForm(Of FrmInspectPopUp)(Me.ScopeFactory, Me.User)
        'Dim inspect As New FrmInspectPopUp(JobDetails, GetToleranceTable(Database, "I"), Job?.DesiredPitch, AllowProgressivePitch, MinimumsApply)
        'inspect.Show()
    End Sub
    Private Sub CmdPrintClassII_Click(sender As Object, e As EventArgs) Handles CmdPrintClassII.Click
        ShowForm(Of FrmInspectPopUp)(Me.ScopeFactory, Me.User)
        'Dim inspect As New FrmInspectPopUp(JobDetails, GetToleranceTable(Database, "II"), Job?.DesiredPitch, AllowProgressivePitch, MinimumsApply)
        'inspect.Show()
    End Sub
    Private Sub CmdPrintClassIII_Click(sender As Object, e As EventArgs) Handles CmdPrintClassIII.Click
        ShowForm(Of FrmInspectPopUp)(Me.ScopeFactory, Me.User)
        'Dim inspect As New FrmInspectPopUp(JobDetails, GetToleranceTable(Database, "III"), Job?.DesiredPitch, AllowProgressivePitch, MinimumsApply)
        'inspect.Show()
    End Sub
    Private Sub CmdPrintClassCustom_Click(sender As Object, e As EventArgs) Handles CmdPrintClassCustom.Click

    End Sub
    Private Sub ChkLocalPitch_CheckedChanged(sender As Object, e As EventArgs) Handles ChkLocalPitch.CheckedChanged
        If ChkLocalPitch.Checked Then
            ChkLocalPitch.ForeColor = Color.Black
            ShowTolerances(MinimumsApply, AllowProgressivePitch)
        Else
            ChkLocalPitch.ForeColor = Color.DimGray
            LabTolLPS.ForeColor = Color.DimGray
            LabTolLPI.ForeColor = Color.DimGray
            LabTolLPII.ForeColor = Color.DimGray
            LabTolLPC.ForeColor = Color.DimGray
        End If
    End Sub
    Private Sub ChkMeanPitchRadius_CheckedChanged(sender As Object, e As EventArgs) Handles ChkMeanPitchRadius.CheckedChanged
        If ChkMeanPitchRadius.Checked Then
            ChkMeanPitchRadius.ForeColor = Color.Black
            ShowTolerances(MinimumsApply, AllowProgressivePitch)
        Else
            ChkMeanPitchRadius.ForeColor = Color.DimGray
            LabTolMPRS.ForeColor = Color.DimGray
            LabTolMPRI.ForeColor = Color.DimGray
            LabTolMPRII.ForeColor = Color.DimGray
            LabTolMPRIII.ForeColor = Color.DimGray
            LabTolMPRC.ForeColor = Color.DimGray
        End If
    End Sub
    Private Sub ChkMeanPitchBlade_CheckedChanged(sender As Object, e As EventArgs) Handles ChkMeanPitchBlade.CheckedChanged
        If ChkMeanPitchBlade.Checked Then
            ChkMeanPitchBlade.ForeColor = Color.Black
            ShowTolerances(MinimumsApply, AllowProgressivePitch)
        Else
            ChkMeanPitchBlade.ForeColor = Color.DimGray
            LabTolMPBS.ForeColor = Color.DimGray
            LabTolMPBI.ForeColor = Color.DimGray
            LabTolMPBII.ForeColor = Color.Black
            LabTolMPBIII.ForeColor = Color.Black
            LabTolMPBC.ForeColor = Color.DimGray
        End If
    End Sub
    Private Sub ChkMeanPitchPropeller_CheckedChanged(sender As Object, e As EventArgs) Handles ChkMeanPitchPropeller.CheckedChanged
        If ChkMeanPitchPropeller.Checked Then
            ChkMeanPitchPropeller.ForeColor = Color.Black
            ShowTolerances(MinimumsApply, AllowProgressivePitch)
        Else
            ChkMeanPitchPropeller.ForeColor = Color.DimGray
            LabTolMPPS.ForeColor = Color.DimGray
            LabTolMPPI.ForeColor = Color.DimGray
            LabTolMPPII.ForeColor = Color.DimGray
            LabTolMPPIII.ForeColor = Color.DimGray
            LabTolMPPC.ForeColor = Color.DimGray
        End If
    End Sub
    Private Sub ChkAngularDeviation_CheckedChanged(sender As Object, e As EventArgs) Handles ChkAngularDeviation.CheckedChanged
        If ChkAngularDeviation.Checked Then
            ChkAngularDeviation.ForeColor = Color.Black
            ShowTolerances(MinimumsApply, AllowProgressivePitch)
        Else
            ChkAngularDeviation.ForeColor = Color.DimGray
            LabTolADS.ForeColor = Color.DimGray
            LabTolADI.ForeColor = Color.DimGray
            LabTolADII.ForeColor = Color.DimGray
            LabTolADIII.ForeColor = Color.DimGray
            LabTolADC.ForeColor = Color.Black
        End If
    End Sub
    Private Sub ChkAxialPosition_CheckedChanged(sender As Object, e As EventArgs) Handles ChkAxialPosition.CheckedChanged
        If ChkAxialPosition.Checked Then
            ChkAxialPosition.ForeColor = Color.Black
            ShowTolerances(MinimumsApply, AllowProgressivePitch)
        Else
            ChkAxialPosition.ForeColor = Color.DimGray
            LabTolAPS.ForeColor = Color.DimGray
            LabTolAPI.ForeColor = Color.DimGray
            LabTolAPII.ForeColor = Color.DimGray
            LabTolAPIII.ForeColor = Color.DimGray
            LabTolAPC.ForeColor = Color.DimGray
        End If
    End Sub
    Private Sub CmdComparisonForm_Click(sender As Object, e As EventArgs) Handles CmdComparisonForm.Click
        If Current IsNot Nothing Then
            Dim frm As FrmComparison = DirectCast(ShowForm(Of FrmComparison)(Me.ScopeFactory, Me.User), FrmComparison)

            frm.JobDetailsBindingSource.DataSource = Current
            frm.Hardware = Hardware
        End If
    End Sub
    Private Sub CmdGraphForm_Click(sender As Object, e As EventArgs) Handles CmdGraphForm.Click
        Dim frm As FrmGraph = DirectCast(ShowForm(Of FrmGraph)(Me.ScopeFactory, Me.User), FrmGraph)

        frm.JobDetails = Current
        frm.HomeSet = HomeSet
        frm.JobDetailsBindingSource.DataSource = Current
        frm.Hardware = Hardware
    End Sub
    Private Sub CmdInspectForm_Click(sender As Object, e As EventArgs) Handles CmdInspectForm.Click
        'Dim frm As FrmInspection = DirectCast(ShowForm(Of FrmInspection)(Me.ScopeFactory, Me.User), FrmInspection)

        'frm.JobDetails = Current
        ''frm.mHomeSet = HomeSet
        ''frm.HomeRefresh()
        'frm.Hardware = Hardware
    End Sub
    Private Sub FrmInspection_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DataGridJobDetails.AutoGenerateColumns = False

        Me.WindowState = FormWindowState.Maximized
        EmployeesBindingSource.DataSource = New BindingList(Of Employee)(Database.Employees.ToList())
        ClassBindingSource.DataSource = New BindingList(Of Tolerance)(Database.Tolerances.ToList())
        MeasurementTypesBindingSource.DataSource = Database.MeasurementTypes.ToList()

        ' Initialize the Navigator
        Navigator = RecordNavigationBar1
        Navigator.Database = mDatabase
        Navigator.ServiceProvider = mServiceProvider
        Navigator.BoundControls = New List(Of Control) From {DataGridJobDetails}
        RecordNavigationBar1.MasterSource = JobDetailsBindingSource
    End Sub
    Private Sub ChkISO484_CheckedChanged(sender As Object, e As EventArgs) Handles ChkISO484.CheckedChanged
        ShowTolerances(MinimumsApply, AllowProgressivePitch)
    End Sub
#End Region
End Class