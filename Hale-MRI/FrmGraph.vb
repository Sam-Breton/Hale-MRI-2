Imports System.ComponentModel
Imports LibDatabase
Imports LibDatabase.Contexts
Imports LibDatabase.Models
Imports Microsoft.EntityFrameworkCore
Imports Microsoft.Extensions.DependencyInjection
Imports Windows.ApplicationModel.Appointments
Imports Windows.ApplicationModel.Contacts
Imports Windows.Devices.Geolocation

Public Class FrmGraph
#Region "Members"
    Private mServiceProvider As IServiceProvider
    Private mDatabase As HaleMRIContext
    Private mJob As Job
    Private mJobDetails As JobDetail
    Private mTolerance As Tolerance
    Private mMasterSource As BindingSource
    Private mNavigator As RecordNavigationBar
    Public HomeSet As Boolean
    Private mHardware As WorkstationEncoders
    Private mGraph As DisplayControl ''' member that holds the currently displayed graph
#End Region
#Region "Constructors"
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
            Return DirectCast(JobDetailsBindingSource.Current, JobDetail)
        End Get
    End Property

    'Public ReadOnly Property Database As HaleMRIContext
    '    Get
    '        Return mDatabase
    '    End Get
    'End Property
    Public Property Hardware As WorkstationEncoders ' We need to pass back to Measurements so that the encoders don't disconnect when the form is closed
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
                If mJob.LeExclusion Is Nothing Then mJob.LeExclusion = 0
                If mJob.TeExclusion Is Nothing Then mJob.TeExclusion = 0
                ShowJobInfo()
            End If
        End Set
    End Property

    Public Property SelectedTolerance As String
        Get
            Return mTolerance.ToleranceClass
        End Get
        Set(value As String)
            If Me.Database IsNot Nothing Then mTolerance = GetToleranceTable(Database, value)
            If mJobDetails IsNot Nothing Then
                mJobDetails.ToleranceClass = value
                If Me.Database IsNot Nothing Then Database.SaveChanges()
                'ShowJobDetailsInfo()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Loads only the given JobDetail and its Cell, Extreme and RadiusMeasurements.
    ''' </summary>
    ''' <returns></returns>
    Public Property JobDetails As JobDetail ''' the setter for this isn't being called need to look into why and resolve to continue testing the form
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
    Public Property Graph As DisplayControl
        Get
            Return mGraph
        End Get
        Set(value As DisplayControl)
            GraphChangeBladeAdjust(value)
            GraphChangeRadiiAdjust(value)
            mGraph = value
            ''' add code for adding selected blades and sselected radii to mgraph''' blades by sector and sectors by blade only need one radius
        End Set
    End Property
    Public Property AllowProgressivePitch As Boolean
        Get
            Return ChkAllowProgressivePitch.Checked
        End Get
        Set(value As Boolean)
            ChkAllowProgressivePitch.Checked = value
        End Set
    End Property
#End Region
#Region "Private Interface"
    Private Sub BindDataSources()
        Dim tolerances = New BindingList(Of Tolerance)(Database.Tolerances.Local.ToList())
        ComboTolerance.DataSource = tolerances
        ComboTolerance.DisplayMember = "ToleranceClass"
        ComboTolerance.ValueMember = "ToleranceClass"
        ComboTolerance.SelectedItem = tolerances.FirstOrDefault(Function(t) t.ToleranceClass = If(mJobDetails?.ToleranceClass, "I"))
        EmployeesBindingSource.DataSource = New BindingList(Of Employee)(Database.Employees.Local.ToList())
        ClassBindingSource.DataSource = New BindingList(Of Tolerance)(Database.Tolerances.Local.ToList())
        MeasurementTypesBindingSource.DataSource = Database.MeasurementTypes.Local.ToList()
    End Sub

    Private Sub ShowJobInfo()
        ShowJobDetailsInfo()
    End Sub
    Private Sub ShowJobDetailsInfo()
        ComboTolerance.SelectedItem = Database.Tolerances.FirstOrDefault(Function(t) t.ToleranceClass = If(JobDetails.ToleranceClass, "I"))
        ShowPitchBasis()
        ManageBladeMenuStrip()
        ManageRadiusMenuStrip()
        GraphChangeBladeAdjust(Graph)
        GraphChangeRadiiAdjust(Graph)
        Graph.Data = JobDetails
    End Sub
    Private Sub GraphChangeBladeAdjust(value As DisplayControl)
        ''' transfer blades to graph after changing
        If value Is Nothing Then Exit Sub
        Dim newBlades As New List(Of String)
        For Each item As ToolStripMenuItem In BladesToolStripMenuItem.DropDownItems
            If item.Checked Then
                newBlades.Add(item.Text.Substring(6)) ''' add only the bladenumber to the blade list maybe look for a better way to seperate this
            End If
        Next
        If value.Name = "ChartBladesBySector" Then
            Dim chart As ChartBladesbySector = DirectCast(value, ChartBladesbySector)
            chart.Blades.Clear()
            chart.Blades = newBlades
        ElseIf value.Name = "ChartSectorsByBlade" Then
            Dim chart As ChartSectorsbyBlade = DirectCast(value, ChartSectorsbyBlade)
            chart.Blades.Clear()
            chart.Blades = newBlades
        ElseIf value.Name = "ChartPosition" Then
            Dim chart As ChartPosition = DirectCast(value, ChartPosition)
            chart.Blades.Clear()
            chart.Blades = newBlades
        ElseIf value.Name = "ChartSummary" Then
            Dim chart As ChartSummary = DirectCast(value, ChartSummary) 'ChartSummary)
            chart.Blades.Clear()
            chart.Blades = newBlades
        ElseIf value.Name = "ChartExpandedSections" Then
            Dim chart As ChartBladesbySector = DirectCast(value, ChartBladesbySector)
            chart.Blades.Clear()
            chart.Blades = newBlades
        End If
    End Sub
    Private Sub GraphChangeRadiiAdjust(value As DisplayControl)
        ''' transfer Radii to graph after changing
        If value Is Nothing Then Exit Sub
        Dim newRadii As New List(Of String)
        For Each item As ToolStripMenuItem In RadiiToolStripMenuItem.DropDownItems
            If item.Checked Then
                newRadii.Add(item.Text.Substring(7)) ''' add only the Radius number to the Radii list maybe look for a better way to seperate this
            End If
        Next
        If value.Name = "ChartBladesbySector" Then
            Dim chart As ChartBladesbySector = DirectCast(value, ChartBladesbySector)
            chart.Radius = Double.Parse(newRadii(0))
        ElseIf value.Name = "ChartSectorsbyBlade" Then
            Dim chart As ChartSectorsbyBlade = DirectCast(value, ChartSectorsbyBlade)
            chart.Radius = Double.Parse(newRadii(0))
        ElseIf value.Name = "ChartPosition" Then
            Dim chart As ChartPosition = DirectCast(value, ChartPosition)
            chart.Radii.Clear()
            chart.Radii = newRadii
        ElseIf value.Name = "ChartSummary" Then
            Dim chart As ChartSummary = DirectCast(value, ChartSummary)
            chart.Radii.Clear()
            chart.Radii = newRadii
        ElseIf value.Name = "ChartExpandedSections" Then
            Dim chart As ChartBladesbySector = DirectCast(value, ChartBladesbySector)
            'chart.Radii.Clear()
            'chart.Radii = newRadii
        End If
    End Sub
    Private Sub ManageBladeMenuStrip()
        If BladesToolStripMenuItem.DropDownItems.Count = Job?.PropellerBlades Then Exit Sub
        BladesToolStripMenuItem.DropDownItems.Clear()
        Dim x As Integer = 1
        If Job IsNot Nothing Then
            For x = 1 To Job?.PropellerBlades
                Dim bladeMenuItem As New ToolStripMenuItem With {
                    .Name = $"Blade{x}ToolStripMenuItem",
                    .CheckOnClick = True,
                    .Checked = False,
                    .Text = "Blade " + x.ToString()} '''need to change blade nad radius all and clear to buttons outside the menustrip
                AddHandler bladeMenuItem.CheckedChanged, AddressOf BladeCheckedChanged
                BladesToolStripMenuItem.DropDownItems.Add(bladeMenuItem)
            Next
        End If
    End Sub
    Private Sub ManageRadiusMenuStrip()
        If RadiiToolStripMenuItem.DropDownItems.Count = Job?.PropellerBlades Then Exit Sub
        RadiiToolStripMenuItem.DropDownItems.Clear()
        If JobDetails IsNot Nothing Then
            For Each rm As RadiusMeasurement In JobDetails?.RadiusMeasurements.Where(Function(r) r.BladeId = 1).ToList()
                Dim radiusMenuItem As New ToolStripMenuItem With {
                    .Name = $"Radius{Math.Round(rm.Radius.Value)}ToolStripMenuItem",
                    .CheckOnClick = True,
                    .Checked = False,
                    .Text = "Radius " + Math.Round(rm.Radius.Value).ToString()}
                AddHandler radiusMenuItem.CheckedChanged, AddressOf RadiusCheckedChanged
                RadiiToolStripMenuItem.DropDownItems.Add(radiusMenuItem)
            Next
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

    Private Sub ShowPitchBasis()
        Select Case ComboBasis.Text
            Case "Mean"
                If JobDetails?.WheelPitch.HasValue Then
                    tBoxBasis.Text = JobDetails.WheelPitch.Value.ToString("F2")
                Else
                    tBoxBasis.Text = Job?.MarkedPitch.ToString()
                End If
            Case "Marked"
                tBoxBasis.Text = Job?.MarkedPitch.ToString()
            Case "Desired"
                tBoxBasis.Text = Job?.DesiredPitch.ToString()
            Case Else
                Return
        End Select
    End Sub

    Protected Overrides Sub OnDataSyncNotification(entityType As Type, primaryKey As Object)
        Dim tolerances = New BindingList(Of Tolerance)(Database.Tolerances.ToList())
        ComboTolerance.DataSource = tolerances
        ComboTolerance.DisplayMember = "ToleranceClass"
        ComboTolerance.ValueMember = "ToleranceClass"
        ComboTolerance.SelectedItem = tolerances.FirstOrDefault(Function(t) t.ToleranceClass = If(mJobDetails?.ToleranceClass, "I"))
        ComboBasis.DataSource = New List(Of String) From {"Mean", "Marked", "Desired"}
    End Sub
#End Region
#Region "Event Handlers"
    Private Sub FrmGraph_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DataGridJobDetails.AutoGenerateColumns = False
        ComboBasis.DataSource = New List(Of String) From {"Mean", "Marked", "Desired"}
        Dim tolerances = New BindingList(Of Tolerance)(Database.Tolerances.ToList())
        ComboTolerance.DataSource = tolerances
        ComboTolerance.DisplayMember = "ToleranceClass"
        ComboTolerance.ValueMember = "ToleranceClass"
        ComboTolerance.SelectedItem = tolerances.FirstOrDefault(Function(t) t.ToleranceClass = If(mJobDetails?.ToleranceClass, "I"))

        Me.WindowState = FormWindowState.Maximized
        EmployeesBindingSource.DataSource = New BindingList(Of Employee)(Database.Employees.ToList())
        ClassBindingSource.DataSource = New BindingList(Of Tolerance)(Database.Tolerances.ToList())
        MeasurementTypesBindingSource.DataSource = Database.MeasurementTypes.ToList()

        ' Initialize the Navigator
        If Me.Database IsNot Nothing Then BindDataSources()
        Navigator = RecordNavigationBar1
        If Me.Database IsNot Nothing Then Navigator.Database = Me.Database
        If Me.ServiceProvider IsNot Nothing Then Navigator.ServiceProvider = Me.ServiceProvider
        'Navigator = RecordNavigationBar1
        'Navigator.Database = mDatabase
        'Navigator.ServiceProvider = mServiceProvider
        Navigator.BoundControls = New List(Of Control) From {DataGridJobDetails}
        RecordNavigationBar1.MasterSource = JobDetailsBindingSource
        Select Case My.Settings.GraphSelectedChart
            Case "BladesbySector"
                CmdBladesbySectorRadio.Checked = True
            Case "SectorsByBlade"
                CmdSectorsbyBladeRadio.Checked = True
            Case "Position"
                CmdPositionRadio.Checked = True
            Case "Summary"
                CmdSummaryRadio.Checked = True
            Case "ExpandedSections"
                CmdExpSectionRadio.Checked = True
        End Select
        ShowPitchBasis()
    End Sub

    Private Sub CmdAllBlades_Click(sender As Object, e As EventArgs) Handles CmdAllBlades.Click
        '' need to green light all blades when checked and remove any unselected blades when unchecked
        For Each item As ToolStripMenuItem In BladesToolStripMenuItem.DropDownItems
            item.Checked = True
        Next
    End Sub
    Private Sub CmdClearBlades_Click(sender As Object, e As EventArgs) Handles CmdClearBlades.Click
        '' need to remove all blades from graph when clicked(set all drop down Items to unchecked)
        For Each item As ToolStripMenuItem In BladesToolStripMenuItem.DropDownItems
            item.Checked = False
        Next
    End Sub
    Private Sub BladeCheckedChanged(sender As Object, e As EventArgs)
        '' add blade designation to graph when checked and remove when unchecked
        GraphChangeBladeAdjust(Graph)
    End Sub
    Private Sub CmdAllRadii_Click(sender As Object, e As EventArgs) Handles CmdAllRadii.Click
        '' need to green light all radii when checked and remove any unselected radii when unchecked
        For Each item As ToolStripMenuItem In RadiiToolStripMenuItem.DropDownItems
            item.Checked = True
        Next
    End Sub
    Private Sub CmdClearRadii_Click(sender As Object, e As EventArgs) Handles CmdClearRadii.Click
        '' need to remove all radii from graph when clicked(set all drop down Items to unchecked)
        For Each item As ToolStripMenuItem In RadiiToolStripMenuItem.DropDownItems
            item.Checked = False
        Next
    End Sub
    Private Sub RadiusCheckedChanged(sender As Object, e As EventArgs)
        '' need to add radius designation to graph when checked and remove when unchecked
        GraphChangeRadiiAdjust(Graph)
    End Sub

    Private Sub ComboTolerance_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboTolerance.SelectedIndexChanged
        SelectedTolerance = DirectCast(ComboTolerance.SelectedItem, Tolerance).ToleranceClass
        DataGridJobDetails.Refresh()
        If Graph IsNot Nothing Then
            Graph.TolClass = GetToleranceTable(Database, SelectedTolerance)
        End If
    End Sub

    Private Sub CmdPositionRadio_CheckedChanged(sender As Object, e As EventArgs) Handles CmdPositionRadio.CheckedChanged
        ''' add code for swapping displayed graph to position(angle by local height)
        If CmdPositionRadio.Checked = True Then
            GraphPanel.Controls.Clear()
            Dim newGraph = New ChartPosition With {
                .Dock = DockStyle.Fill,
                .Name = "ChartPosition",
                .Basis = ComboBasis.Text,
                .Precision = 2,
                .TolClass = GetToleranceTable(Database, SelectedTolerance),
                .Font = New Font(Me.Font.FontFamily, 14)}
            GraphPanel.Controls.Add(newGraph)
            Graph = newGraph
            Graph.Data = JobDetails
        End If
    End Sub

    Private Sub CmdSectorsByBladeRadio_CheckedChanged(sender As Object, e As EventArgs) Handles CmdSectorsbyBladeRadio.CheckedChanged
        ''' add code for swapping displayed graph to sectors by blade
        If JobDetails Is Nothing Then Exit Sub
        If CmdSectorsbyBladeRadio.Checked = True Then
            GraphPanel.Controls.Clear()
            Dim newGraph = New ChartSectorsbyBlade With {
                .Dock = DockStyle.Fill,
                .Name = "ChartSectorsbyBlade",
                .Basis = ComboBasis.Text,
                .Precision = 2,
                .TolClass = GetToleranceTable(Database, SelectedTolerance),
                .Font = New Font(Me.Font.FontFamily, 14)}
            GraphPanel.Controls.Add(newGraph)
            Graph = newGraph
            Graph.Data = JobDetails
        End If
    End Sub

    Private Sub CmdSummaryRadio_CheckedChanged(sender As Object, e As EventArgs) Handles CmdSummaryRadio.CheckedChanged
        ''' add code for swapping displayed graph to summary(average pitch of RadiusMeasurements)
        'If JobDetails Is Nothing Then Exit Sub
        If CmdSummaryRadio.Checked = True Then
            GraphPanel.Controls.Clear()
            Dim newGraph = New ChartSummary With {
                .Dock = DockStyle.Fill,
                .Name = "ChartSummary",
                .Basis = ComboBasis.Text,
                .Precision = 2,
                .DefaultSize = GraphPanel.Size,
                .TolClass = GetToleranceTable(Database, SelectedTolerance),
                .Font = New Font(Me.Font.FontFamily, 14)}
            GraphPanel.Controls.Add(newGraph)
            Graph = newGraph
            Graph.Data = JobDetails
        End If
    End Sub

    Private Sub CmdBladesBySectorRadio_CheckedChanged(sender As Object, e As EventArgs) Handles CmdBladesbySectorRadio.CheckedChanged
        ''' add code for swapping displayed graph to Blades by sector
        If CmdBladesbySectorRadio.Checked = True Then
            GraphPanel.Controls.Clear()
            Dim newGraph = New ChartBladesbySector With {
                .Dock = DockStyle.Fill,
                .Name = "ChartBladesbySector",
                .Basis = ComboBasis.Text,
                .Precision = 2,
                .TolClass = GetToleranceTable(Database, SelectedTolerance),
                .Font = New Font(Me.Font.FontFamily, 14)}
            GraphPanel.Controls.Add(newGraph)
            Graph = newGraph
            Graph.Data = JobDetails
        End If
    End Sub

    Private Sub ChkAllowProgressivePitch_CheckedChanged(sender As Object, e As EventArgs) Handles ChkAllowProgressivePitch.CheckedChanged
        If Graph.Name = "ChartSummary" Then
            Dim chart As ChartSummary = DirectCast(Graph, ChartSummary)
            chart.AllowProgressivePitch = AllowProgressivePitch
            Graph.Basis = ComboBasis.Text
        End If
    End Sub

    Private Sub CmdMeasureForm_Click(sender As Object, e As EventArgs) Handles CmdMeasureForm.Click
        Dim frm As FrmMeasurements = DirectCast(ShowForm(Of FrmMeasurements)(Me.ScopeFactory, Me.User), FrmMeasurements)

        frm.Hardware = Hardware
        frm.AllowProgressivePitch = AllowProgressivePitch
        frm.Job = Current.Job
    End Sub

    Private Sub CmdComparisonForm_Click(sender As Object, e As EventArgs) Handles CmdComparisonForm.Click
        Dim frm As FrmComparison = DirectCast(ShowForm(Of FrmComparison)(Me.ScopeFactory, Me.User), FrmComparison)

        frm.JobDetailsBindingSource.DataSource = Current
        frm.Hardware = Hardware
    End Sub
    Private Sub JobDetailsBindingSource_CurrentChanged(sender As Object, e As EventArgs) Handles JobDetailsBindingSource.CurrentChanged
        If mJobDetails IsNot Current Then
            mJobDetails = Current
            If JobDetails IsNot Nothing Then
                ShowJobDetailsInfo()
            End If
        End If
    End Sub

    Private Sub CmdInspectForm_Click(sender As Object, e As EventArgs) Handles CmdInspectForm.Click
        Dim frm As FrmInspection = DirectCast(ShowForm(Of FrmInspection)(Me.ScopeFactory, Me.User), FrmInspection)

        frm.JobDetails = Current
        frm.JobDetailsBindingSource.DataSource = Current
        frm.Hardware = Hardware
        'frm.Show()
    End Sub

    Private Sub ComboBasis_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboBasis.SelectionChangeCommitted
        ShowPitchBasis()
    End Sub
#End Region
End Class