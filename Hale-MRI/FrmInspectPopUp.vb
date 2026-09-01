Imports LibDatabase
Imports LibDatabase.Contexts
Imports LibDatabase.Models
Imports Microsoft.Extensions.DependencyInjection

Public Class FrmInspectPopUp
    Inherits FrmDatabaseForm
    'Dim Database As HaleMRIContext

    ' TODO: We need a different way of passing parameters to this class
    ' instead the Sub New() overloads. All the app's data-bound forms
    ' use Dependency Injection (DI) to load, which the DI Container determines
    ' on startup (Sub Main()) and provides any services required by the form.
    '
    ' I recommend changing the Sub New() overload parameters to properties,
    ' as they are in other forms, which can be provided by the client. The
    ' Sub New() overloads can also be replaced by Public Subs that take the
    ' required parameters and but the form into the proper state.

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

    Public Sub Open(mJobDetails As JobDetail, TolClass As Tolerance, Basis As String, AllowProgressivePitch As Boolean, MinimumsApply As Boolean)
        Dim mLocalPitchtable As New LocalPitchTable With {
            .Name = "InspectLP",
            .Dock = DockStyle.Fill,
            .TolClass = TolClass,
            .Basis = Basis,
            .AllowProgressivePitch = AllowProgressivePitch,
            .MinimumsApply = MinimumsApply
        }
        Me.Controls.Add(mLocalPitchtable)
        mLocalPitchtable.Data = mJobDetails
        Me.Size = mLocalPitchtable.NeededSize
        AddHandler mLocalPitchtable.KeyDown, AddressOf FrmInspectPopUp_KeyDown
    End Sub

    Public Sub Open(mJobDetails As JobDetail, TolClass As Tolerance, ReferenceBlade As Integer, ReferenceRadius As Double, ReferencePoint As String, typ As String)
        Dim neededsize As New Size(Screen.PrimaryScreen.WorkingArea.Width * 0.75, Screen.PrimaryScreen.WorkingArea.Height * 0.8)
        Me.SuspendLayout()
        Me.Size = neededsize
        Select Case typ
            Case "BladeHeight"
                Dim mChartBladeHeight As New ChartBladeHeight With {
                    .Dock = DockStyle.Fill,
                     .Name = "InspectChartBH",
                  .DefaultSize = neededsize,
                   .TolClass = TolClass,
                    .ReferenceBlade = ReferenceBlade,
                     .ReferenceRadius = ReferenceRadius,
                    .ReferencePoint = ReferencePoint}
                Me.Controls.Add(mChartBladeHeight)
                mChartBladeHeight.Data = mJobDetails
                AddHandler mChartBladeHeight.KeyDown, AddressOf FrmInspectPopUp_KeyDown
            Case "AngularPosition"
                Dim mChartAngularPosition As New ChartAngularPosition With {
        .Dock = DockStyle.Fill,
        .Name = "InspectChartAP",
        .DefaultSize = neededsize,
        .TolClass = TolClass,
        .ReferenceBlade = ReferenceBlade,
        .ReferenceRadius = ReferenceRadius,
        .ReferencePoint = ReferencePoint}
                Me.Controls.Add(mChartAngularPosition)
                mChartAngularPosition.Data = mJobDetails
                AddHandler mChartAngularPosition.KeyDown, AddressOf FrmInspectPopUp_KeyDown
        End Select
        Me.ResumeLayout()
    End Sub

    Private Sub Open(data As HaleMRIContext, mJobDetails As JobDetail, TolClass As Tolerance, Basis As String, App As Boolean, MinimumsApply As Boolean, AngDev As Boolean)

        Dim mChartPlot As New ChartPlot With {
        .Dock = DockStyle.Fill,
        .Name = "InspectChartPlot",
        .Visible = False}
        Dim tlayout As New TableLayoutPanel With {
            .Dock = DockStyle.Fill,
            .ColumnCount = 2,
            .RowCount = 15,
            .Name = "TableLayoout"}
        Me.Controls.Add(tlayout)
        Dim lab As Label = New Label With {
            .Text = "Tolerance",
            .Dock = DockStyle.Fill,
            .TextAlign = ContentAlignment.BottomLeft,
            .Font = Me.Font,
            .Visible = True}
        tlayout.Controls.Add(lab, 0, 2)
        Dim CBox As ComboBox = New ComboBox With {
            .Dock = DockStyle.Top,
            .Name = "CBoxTolerance",
            .Visible = True}
        ' Add any initialization after the InitializeComponent() call.
        tlayout.Controls.Add(CBox, 0, 3)
        lab = New Label With {
            .Text = "BasisPitch",
            .Dock = DockStyle.Fill,
            .TextAlign = ContentAlignment.BottomLeft,
            .Font = Me.Font,
            .Visible = True}
        tlayout.Controls.Add(lab, 0, 0)
        CBox = New ComboBox With {
            .Dock = DockStyle.Top,
            .Name = "CBoxBasis",
            .Visible = True}
        tlayout.Controls.Add(CBox, 0, 1)
        lab = New Label With {
            .Text = "Basis",
            .Dock = DockStyle.Fill,
            .TextAlign = ContentAlignment.BottomLeft,
            .Font = Me.Font,
            .Visible = True}
        tlayout.Controls.Add(lab, 0, 4)
        Dim tBox As New TextBox With {
            .Dock = DockStyle.Top,
            .Name = "tBoxBasis",
            .Visible = True}
        tlayout.Controls.Add(tBox, 0, 5)
        Dim check As New CheckBox With {
            .Dock = DockStyle.Fill,
            .Name = "checkAD",
            .Text = "Angular Deviation",
            .Visible = True}
        tlayout.Controls.Add(check, 0, 6)
        tlayout.Controls.Add(mChartPlot, 1, 0)
        mChartPlot.Visible = True
        tlayout.SetRowSpan(mChartPlot, 15)
        mChartPlot.Data = mJobDetails
        mChartPlot.TolClass = TolClass
        mChartPlot.Basis = Basis
        mChartPlot.AllowProgressivePitch = App
        mChartPlot.MinimumsApply = MinimumsApply
        mChartPlot.AngDeviation = AngDev
    End Sub
    Private Sub FrmInspectPopUp_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Close()
        End If
    End Sub
End Class