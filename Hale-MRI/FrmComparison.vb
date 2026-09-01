Imports System.ComponentModel
Imports Hale_MRI.My
Imports Hale_MRI.RecordNavigationBar
Imports LibDatabase
Imports LibDatabase.Constants
Imports LibDatabase.Contexts
Imports LibDatabase.Models
Imports Microsoft.EntityFrameworkCore
Imports Microsoft.Extensions.DependencyInjection
Public Class FrmComparison
    Inherits FrmDatabaseForm

#Region "Private Members"
    Private mJobDetails As JobDetail                            ' The current JobDetail record
    Private mJob As Job                                         ' The Job the current JobDetail record belongs to.
    Private mMasterSource As BindingSource = Nothing            ' The form's "master" BindingSource.
    Private mNavigator As RecordNavigationBar = Nothing         ' The form's RecordNavigationBar.
    Private mProgRadius As List(Of RadiusMeasurement) = Nothing ' this is the progression data for the radius measurements, which is used to show the progression of the measurements over time in the charts, and is passed to the ChartCompLine control so it can be displayed when the user clicks on a point in the chart
    Private mProgNewPitch As Double
    Private mProgOldPitch As Double
    Private mProgLoaded As Boolean = False
    Private mCharts As New List(Of ChartCompLine)
    Private mHardware As WorkstationEncoders
    Private ReadOnly mDatabase As HaleMRIContext
    Private ReadOnly mServiceProvider As IServiceProvider
    Public mUser As Employee
    Public HomeSet As Boolean = False
    Private ProgScreen As Boolean = False
    Private ProgManager As UCProgressionManager
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
            Return JobDetailsBindingSource.Current
        End Get
    End Property

    ''' <summary>
    ''' Finds the given JobDetail and, if found, makes it the current record.
    ''' </summary>
    ''' <param name="item">The JobDetail to find.</param>
    ''' <returns>The found JobDetail, or Nothing if not found.</returns>
    Public Function Find(item As JobDetail) As JobDetail
        Dim result As JobDetail = Nothing
        Dim pos As Integer = MasterSource.Find("Id", item.Id) 'BindingSourceFind(MasterSource, item)
        If pos <> kNoCurrentRecord Then
            MasterSource.Position = pos
            result = Current
        End If
        Return result
    End Function
    'Public ReadOnly Property Database As HaleMRIContext
    '    Get
    '        Return mDatabase
    '    End Get
    'End Property
    Public Property JobDetails As JobDetail
        Get
            Return mJobDetails
        End Get
        Set(value As JobDetail)
            mJobDetails = value
            mJob = mJobDetails?.Job
            If mJobDetails IsNot Nothing Then
                If Me.Database IsNot Nothing Then JobDetailsBindingSource.DataSource = GetMeasurementData(mJobDetails)
                JobChanged()
            End If
        End Set
    End Property
    Public Property Job As Job
        Get
            Return mJob
        End Get
        Set(value As Job)
            mJob = value
            If mJob IsNot Nothing Then
                If Me.Database IsNot Nothing Then JobDetailsBindingSource.DataSource = GetMeasurementData(mJob)
                JobChanged()
            End If
        End Set
    End Property
    Public Property Hardware As WorkstationEncoders ' We need to pass back to Measurements so that the encoders don't disconnect when the form is closed
        Get
            Return mHardware
        End Get
        Set(value As WorkstationEncoders)
            mHardware = value
        End Set
    End Property
    Public Property CenterRef As Boolean
        Get
            Return My.Settings.CompCenterRef 'ChkCenterRef.Checked
        End Get
        Set(value As Boolean)
            'ChkCenterRef.Checked = value
            My.Settings.CompCenterRef = value
            My.Settings.Save()
        End Set
    End Property
    Public Property GraphEntireScan As Boolean
        Get
            Return ChkGraphEntireScan.Checked
        End Get
        Set(value As Boolean)
            'ChkGraphEntireScan.Checked = value
            My.Settings.CompGraphEntireScan = value
            My.Settings.Save()
        End Set
    End Property
    Public Property ShowTrack As Boolean
        Get
            Return My.Settings.CompShowTrack 'ChkShowTrack.Checked
        End Get
        Set(value As Boolean)
            My.Settings.CompShowTrack = value
            My.Settings.Save()
        End Set
    End Property
    Public Property ExamineOneBlade As Boolean
        Get
            Return My.Settings.CompOneBlade
        End Get
        Set(value As Boolean)
            My.Settings.CompOneBlade = value
            My.Settings.Save()
        End Set
    End Property
    Public Property Spline As Boolean
        Get
            Return My.Settings.CompSpline
        End Get
        Set(value As Boolean)
            My.Settings.CompSpline = value
            My.Settings.Save()
        End Set
    End Property
    Public Property ComparisonFont As Integer
        Get
            Return TrackFont.Value
        End Get
        Set(value As Integer)
            TrackFont.Value = value
            My.Settings.Save()
        End Set
    End Property
    Public Property KeepForComp As Boolean
        Get
            Return My.Settings.CompKeepForComp
        End Get
        Set(value As Boolean)
            My.Settings.CompKeepForComp = value
            My.Settings.Save()
        End Set
    End Property
    Public Property Sections As Integer
        Get
            Return My.Settings.CompSections
        End Get
        Set(value As Integer)
            My.Settings.CompSections = value
            My.Settings.Save()
        End Set
    End Property

#End Region
#Region "Private Interface"
    Private Sub BindDataSources()
        EmployeeBindingSource.DataSource = Me.Database.Employees.Local.ToBindingList()
        ToleranceBindingSource.DataSource = Me.Database.Tolerances.Local.ToBindingList()
        MeasurementTypesBindingSource.DataSource = Me.Database.MeasurementTypes.Local.ToBindingList()
        Dim bsReferenceBlades As New BindingList(Of Integer)
        If mJob IsNot Nothing Then
            For i As Integer = 1 To mJob.PropellerBlades
                bsReferenceBlades.Add(i)
            Next
            ComboTrackRefBlade.DataSource = bsReferenceBlades
            ComboTrackRefBlade.SelectedIndex = 0
        End If
        TxtRefPitch.Text = If(Current?.Job?.DesiredPitch.HasValue, Current.Job.DesiredPitch.Value.ToString(), "")
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
            If mNavigator IsNot Nothing Then mNavigator.Database = Me.Database
        End Set
    End Property
    Private Function ReferenceRadiiGet(ByVal blade As Integer) As List(Of Double)
        ' Returns a list of reference radii for the given blade.
        Dim radii As New List(Of Double)
        If mJobDetails?.RadiusMeasurements IsNot Nothing Then
            For Each rm As RadiusMeasurement In mJobDetails.RadiusMeasurements
                If rm.BladeId = blade Then radii.Add(Math.Round(CType(rm.Radius, Double)).ToString("F2"))
            Next
        End If
        Return radii
    End Function
    Private Sub JobChanged()
        If JobDetails Is Nothing Then Return
        TxtRefPitch.Text = JobDetails.Job.DesiredPitch.Value.ToString()
        ComboRadiusorBlade.DataSource = Nothing
        Dim bsReferenceBlades As New BindingList(Of Integer)
        For i As Integer = 1 To JobDetails.Job.PropellerBlades
            bsReferenceBlades.Add(i)
        Next
        ComboTrackRefBlade.DataSource = bsReferenceBlades
        ComboTrackRefBlade.SelectedIndex = 0
        If ChkExamineoneBlade.Checked Then
            Dim bsdatablades As New BindingList(Of Integer)
            For i As Integer = 1 To JobDetails.Job.PropellerBlades
                bsdatablades.Add(i)
            Next
            ComboRadiusorBlade.DataSource = bsdatablades
            ComboRadiusorBlade.SelectedIndex = 0
            LabRadiusorBlade.Text = "Blade: " + ComboRadiusorBlade.SelectedText
        Else
            ComboRadiusorBlade.DataSource = ReferenceRadiiGet(ComboTrackRefBlade.SelectedIndex + 1)
            ComboRadiusorBlade.SelectedIndex = 0
            LabRadiusorBlade.Text = "Radius: " + ComboRadiusorBlade.SelectedText
        End If
        UpdateChartsFull()
    End Sub
    Private Sub UpdateChartsFull()
        If Current Is Nothing Then Return
        If ComboRadiusorBlade.SelectedItem Is Nothing Then Return
        For Each chart As ChartCompLine In mCharts
            If TLayoutCompCharts.Contains(chart) Then TLayoutCompCharts.Controls.Remove(chart)
        Next
        mCharts.Clear() ' something is breaking when Clearing controls from the Layout Table-System.ArgumentOutOfRangeException
        Dim i As Integer = 0
        If ExamineOneBlade Then
            TLayoutCompCharts.RowCount = Current.RadiusMeasurements.Where(Function(r) r.BladeId = ComboRadiusorBlade.SelectedIndex + 1).Count()
            TLayoutCompCharts.Height = TLayoutCompCharts.RowCount * 200
            For Each rm As RadiusMeasurement In Current.RadiusMeasurements.Where(Function(r) r.BladeId = ComboRadiusorBlade.SelectedIndex + 1).OrderBy(Function(r) r.Radius).ToList()
                Dim graph As New ChartCompLine With {
                    .AxesScaling = CBoxAxesScaling.SelectedValue,
                    .RefPitch = TxtRefPitch.Text,
                    .CenterRef = CenterRef,
                    .EntireScan = GraphEntireScan,
                    .ShowTrack = ShowTrack,
                    .Spline = Spline,
                    .Sections = Sections,
                    .Dock = DockStyle.Fill,
                    .Margin = New Padding(2, 2, 2, 2),
                    .ChartBlade = rm.BladeId,
                    .ChartRadius = Math.Round(rm.Radius.Value),
                    .TolClass = GetToleranceTable(Me.Database, JobDetails.ToleranceClass),
                    .ProgNewPitch = mProgNewPitch,
                    .ProgOldPitch = mProgOldPitch,
                    .ProgRads = mProgRadius,
                    .TrackBlade = ComboTrackRefBlade.SelectedIndex + 1,
                    .Font = New Font(Me.Font.FontFamily, TrackFont.Value, FontStyle.Bold),
                    .DefaultSize = New Drawing.Size(TLayoutCompCharts.Width - 4, 200)
                }
                TLayoutCompCharts.Controls.Add(graph, 0, i)
                mCharts.Add(graph)
                i += 1
                graph.Data = JobDetails
            Next
        Else
            TLayoutCompCharts.RowCount = JobDetails.Job.PropellerBlades
            TLayoutCompCharts.Height = TLayoutCompCharts.RowCount * 200
            For Each rm As RadiusMeasurement In Current.RadiusMeasurements.Where(Function(r) Math.Round(r.Radius.Value).ToString = ComboRadiusorBlade.SelectedItem.ToString()).OrderBy(Function(r) r.BladeId).ToList()
                Dim graph As New ChartCompLine With {
                    .AxesScaling = CBoxAxesScaling.SelectedValue,
                    .RefPitch = TxtRefPitch.Text,
                    .CenterRef = CenterRef,
                    .EntireScan = GraphEntireScan,
                    .ShowTrack = ShowTrack,
                    .Spline = Spline,
                    .Sections = Sections,
                    .Dock = DockStyle.Fill,
                    .Margin = New Padding(2, 2, 2, 2),
                    .ChartBlade = rm.BladeId,
                    .ChartRadius = Math.Round(rm.Radius.Value),
                    .ProgNewPitch = mProgNewPitch,
                    .ProgOldPitch = mProgOldPitch,
                    .ProgRads = mProgRadius,
                    .TrackBlade = ComboTrackRefBlade.SelectedIndex + 1,
                    .Font = New Font(Me.Font.FontFamily, TrackFont.Value, FontStyle.Bold),
                    .DefaultSize = New Drawing.Size(TLayoutCompCharts.Width - 4, 200)
                }
                TLayoutCompCharts.Controls.Add(graph, 0, i)
                mCharts.Add(graph)
                i += 1
                graph.Data = JobDetails
            Next
        End If
        TLayoutCompCharts.RowStyles.Clear()
        For Each chart As ChartCompLine In mCharts
            TLayoutCompCharts.RowStyles.Add(New RowStyle(SizeType.Absolute, 200))
        Next
    End Sub
    Private Sub UpdateCharts() ' this is called when the user changes a setting that affects the chart display but not the data, so we don't need to remake the charts, just update the properties and refresh them
        For Each Chart As ChartCompLine In mCharts
            Chart.AxesScaling = CBoxAxesScaling.SelectedValue
            Chart.RefPitch = TxtRefPitch.Text
            Chart.CenterRef = CenterRef
            Chart.EntireScan = GraphEntireScan
            Chart.ShowTrack = ShowTrack
            Chart.Spline = Spline
            Chart.Sections = Sections
            Chart.TrackBlade = ComboTrackRefBlade.SelectedIndex + 1
            Chart.Data = JobDetails
            Chart.Font = New Font(Me.Font.FontFamily, TrackFont.Value, FontStyle.Bold)
        Next
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
            Me.Database.JobDetails _
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
            Me.Database.JobDetails _
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
    Private Sub UpdateRms(blade As Integer)
        ' called when the user changes the selected blade when Examining one blade
        Dim rms As List(Of RadiusMeasurement) = Current?.RadiusMeasurements?.Where(Function(r) r.BladeId = blade).OrderBy(Function(r) r.Radius).ToList()
        If mCharts.Count <> rms.Count Then Return ' end the function here because chart creation is managed by UpdateChartsFull and shouldn't be any where else
        Dim x As Integer
        For x = 0 To rms.Count - 1
            mCharts(x).ChartBlade = blade
            mCharts(x).ChartRadius = Math.Round(rms(x).Radius.Value)
        Next
    End Sub
    Private Sub UpdateRms(Rad As Double)
        'called when the user changes the selected radius when not examining one blade
        Dim rms As List(Of RadiusMeasurement) = Current.RadiusMeasurements.Where(Function(r) Math.Round(r.Radius.Value) = Math.Round(Rad)).OrderBy(Function(r) r.BladeId).ToList()
        If mCharts.Count <> rms.Count Then Return
        Dim x As Integer
        For x = 0 To rms.Count - 1
            mCharts(x).ChartBlade = rms(x).BladeId
            mCharts(x).ChartRadius = Rad
        Next
    End Sub
    Private Sub InsertProgs()
        If Not mProgLoaded Then Return
        For Each chart As ChartCompLine In mCharts
            chart.ProgNewPitch = mProgNewPitch
            chart.ProgOldPitch = mProgOldPitch
            chart.ProgRads = mProgRadius
        Next
    End Sub

    Protected Overrides Sub OnDataSyncNotification(entityType As Type, primaryKey As Object)
        ' TODO: Load any entities this form manages from the database into the LocalView so they're current.
        ' BindingSource.ResetBindings(False)
    End Sub

#End Region
#Region "Event Handlers"
    Private Sub Navigator_NavigationEvent(sender As Object, e As NavigationEventArgs)
        ' Handles Navigator events so we can update our controls accordingly.
        Select Case e.EventName
            Case "AddNew"
                ' Disable PanelMeasurements when the user is adding a new JobDetails record.
            Case "Delete"
                ' put msg box here that says can't delete record on this form
            Case "Editing"
            Case "FilterOff"
            Case "FilterOn"
            Case "Find"
            Case "GotoFirst", "GotoNext", "GotoPrev"
            Case "GotoLast"
            Case "Save"
                ' Refresh any open database forms affected by our changes and enable PanelMeasurements.
                'RefreshAll()
                'MyBase.Refresh()
            Case "Undo"
                ' Enable the PanelMeasurements when the user has cancelled the JobDetails record changes.
                If Me.Current IsNot Nothing Then
                End If
            Case Else
        End Select
    End Sub
    Private Sub JobDetailsBindingSource_CurrentChanged(sender As Object, e As EventArgs) Handles JobDetailsBindingSource.CurrentChanged
        If mJobDetails IsNot Current Then
            mJobDetails = Current
            JobChanged()
        End If
    End Sub
    Private Sub FrmComparison_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Load event handler code here
        Me.WindowState = FormWindowState.Maximized

        If Me.Database IsNot Nothing Then BindDataSources()
        EmployeeBindingSource.DataSource = New BindingList(Of Employee)(Database.Employees.ToList())
        ToleranceBindingSource.DataSource = New BindingList(Of Tolerance)(Database.Tolerances.ToList())
        MeasurementTypesBindingSource.DataSource = Database.MeasurementTypes.ToList()

        Navigator = RecordNavigationBar1
        Navigator.BoundControls = New List(Of Control) From {DataGridJobDetails}
        MasterSource = JobDetailsBindingSource
        Sections = 10
        'Dim bsReferenceBlades As New BindingList(Of Integer)
        'If mJob IsNot Nothing Then
        '    For i As Integer = 1 To mJob.PropellerBlades
        '        bsReferenceBlades.Add(i)
        '    Next
        '    ComboTrackRefBlade.DataSource = bsReferenceBlades
        '    ComboTrackRefBlade.SelectedIndex = 0
        'End If
        'TxtRefPitch.Text = If(Current?.Job?.DesiredPitch.HasValue, Current.Job.DesiredPitch.Value.ToString(), "")
        Dim axes As New List(Of Double) From {0.01, 0.02, 0.05, 0.1, 0.2, 0.5, 1, 2, 5, 10}
        CBoxAxesScaling.DataSource = axes
        CBoxAxesScaling.SelectedIndex = My.Settings.CompAxesScaling
        ChkExamineoneBlade.Checked = My.Settings.CompOneBlade
        TLayoutCompCharts.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
        ChkCenterRef.Checked = My.Settings.CompCenterRef
        ChkGraphEntireScan.Checked = My.Settings.CompGraphEntireScan
        ChkSpline.Checked = My.Settings.CompSpline
        ChkKeepforComp.Checked = My.Settings.CompKeepForComp
        TrackSegments.Value = My.Settings.CompSections
        TrackSegments.Value = Sections
        ComboTrackRefBlade.SelectedIndex = My.Settings.CompReferenceBlade
        ComboRadiusorBlade.SelectedIndex = My.Settings.CompBladeorRad
        ChkShowTrack.Checked = My.Settings.CompShowTrack
        If Me.Database IsNot Nothing Then UpdateChartsFull()
    End Sub

    Private Sub ChkCenterRef_CheckedChanged(sender As Object, e As EventArgs) Handles ChkCenterRef.CheckedChanged
        CenterRef = ChkCenterRef.Checked
        UpdateCharts()
    End Sub

    Private Sub ChkKeepforComp_CheckedChanged(sender As Object, e As EventArgs) Handles ChkKeepforComp.CheckedChanged
        KeepForComp = ChkKeepforComp.Checked
    End Sub

    Private Sub ChkGraphEntireScan_CheckedChanged(sender As Object, e As EventArgs) Handles ChkGraphEntireScan.CheckedChanged
        GraphEntireScan = ChkGraphEntireScan.Checked
        UpdateCharts()
    End Sub

    Private Sub ChkShowTrack_CheckedChanged(sender As Object, e As EventArgs) Handles ChkShowTrack.CheckedChanged
        ShowTrack = ChkShowTrack.Checked
        UpdateCharts()
    End Sub

    Private Sub ChkExamineoneBlade_CheckedChanged(sender As Object, e As EventArgs) Handles ChkExamineoneBlade.CheckedChanged
        If JobDetails Is Nothing Then Return
        'need to make this run UpdateChartsFull or enforce the correct number of Charts are added to mCharts so that all Radii are shown
        ExamineOneBlade = ChkExamineoneBlade.Checked
        If ChkExamineoneBlade.Checked Then
            ComboRadiusorBlade.DataSource = Nothing
            Dim bsdatablades As New BindingList(Of Integer)
            For i As Integer = 1 To JobDetails.Job.PropellerBlades
                bsdatablades.Add(i)
            Next
            ComboRadiusorBlade.DataSource = bsdatablades
            ComboRadiusorBlade.SelectedIndex = 0
            LabRadiusorBlade.Text = "Blade: " + ComboRadiusorBlade.SelectedItem.ToString()
        Else
            ComboRadiusorBlade.DataSource = Nothing
            ComboRadiusorBlade.DataSource = ReferenceRadiiGet(ComboTrackRefBlade.SelectedIndex + 1)
            ComboRadiusorBlade.SelectedIndex = 0
            LabRadiusorBlade.Text = "Radius: " + ComboRadiusorBlade.SelectedItem.ToString()
        End If
        UpdateChartsFull()
    End Sub

    Private Sub ChkSpline_CheckedChanged(sender As Object, e As EventArgs) Handles ChkSpline.CheckedChanged
        Spline = ChkSpline.Checked
        UpdateCharts()
    End Sub

    Private Sub TrackFont_ValueChanged(sender As Object, e As EventArgs) Handles TrackFont.ValueChanged
        Dim tfont As New Font(Me.Font.FontFamily, TrackFont.Value, FontStyle.Bold)
        For Each chart As ChartCompLine In mCharts
            chart.Font = tfont
        Next
        UpdateCharts()
    End Sub

    Private Sub TrackSegments_ValueChanged(sender As Object, e As EventArgs) Handles TrackSegments.ValueChanged
        LabSegments.Text = "Segments: " & TrackSegments.Value.ToString()
        Sections = TrackSegments.Value
        UpdateCharts()
    End Sub

    Private Sub CmdMeasure_Click(sender As Object, e As EventArgs) Handles CmdMeasure.Click
        Dim frm As FrmComparison = DirectCast(ShowForm(Of FrmComparison)(Me.ScopeFactory, Me.User), FrmComparison)
        'frm.mHomeSet = HomeSet
        'frm.HomeRefresh()
        frm.Hardware = Hardware
        frm.Job = Current.Job
    End Sub

    Private Sub CmdComparison_Click(sender As Object, e As EventArgs) Handles CmdComparison.Click

    End Sub

    Private Sub CBoxAxesScaling_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBoxAxesScaling.SelectionChangeCommitted
        Settings.CompAxesScaling = CBoxAxesScaling.SelectedIndex
        My.Settings.Save()
        UpdateCharts()
    End Sub

    Private Sub ComboTrackRefBlade_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboTrackRefBlade.SelectedIndexChanged
        LabTrackRefBlade.Text = "Track Ref Blade: " + ComboTrackRefBlade.SelectedItem.ToString()
        UpdateCharts()
    End Sub

    Private Sub ComboRadiusorBlade_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboRadiusorBlade.SelectedIndexChanged
        If sender IsNot ComboRadiusorBlade Then Return
        If ComboRadiusorBlade.DataSource Is Nothing Then Return
        Dim wasc = sender.ToString()
        If ExamineOneBlade Then
            UpdateRms(ComboRadiusorBlade.SelectedIndex + 1)
        Else
            UpdateRms(Double.Parse(ComboRadiusorBlade.SelectedItem.ToString()))
        End If
        UpdateCharts()
    End Sub

    Private Sub CmdSelectProgression_Click(sender As Object, e As EventArgs) Handles CmdSelectProgression.Click
        'need to make this replace the TlayoutCompCharts with a custom control that handles management of the Progressions
        'If ProgScreen Then
        '    mProgRadius = ProgManager.BladeProgs
        '    InsertProgs()
        '    ProgManager.Dispose()
        '    ProgManager = Nothing
        '    UpdateChartsFull()
        'Else
        '    For Each chart As ChartCompLine In mCharts
        '        If TLayoutCompCharts.Contains(chart) Then TLayoutCompCharts.Controls.Remove(chart)
        '    Next
        '    TLayoutCompCharts.RowCount = 1
        '    TLayoutCompCharts.Dock = DockStyle.Fill
        '    ProgManager = New UCProgressionManager(mServiceProvider) With {
        '        .Margin = New Padding(25, 50, 25, 50),
        '        .CompCurrent = Current,
        '        .Sections = Sections,
        '        .Database = Database
        '    }

        '    TLayoutCompCharts.Controls.Add(ProgManager, 0, 0)
        '    ProgManager.Dock = DockStyle.Fill
        '    ProgManager.Refresh()
        'End If
    End Sub

    Private Sub CmdInspect_Click(sender As Object, e As EventArgs) Handles CmdInspect.Click
        Dim frm As FrmInspection = DirectCast(ShowForm(Of FrmInspection)(Me.ScopeFactory, Me.User), FrmInspection)

        frm.JobDetails = Current
        frm.JobDetailsBindingSource.DataSource = Current
        frm.Hardware = Hardware
    End Sub

    Private Sub TxtRefPitch_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtRefPitch.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Enter) Then
            UpdateCharts()
        End If
    End Sub
#End Region
End Class