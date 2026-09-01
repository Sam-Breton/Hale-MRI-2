Imports LibDatabase.Models

Public Class ChordLengthTable
    Inherits DisplayControl
#Region "Types and Constants"
    Private Const kTableTitle As String = "Chord Length - Diameter - Track  :  Inches"
#End Region
#Region "Constructors"
    ''' <summary>
    ''' Default constructor.
    ''' </summary>
    Public Sub New()
        MyBase.New()
        InitializeComponent()
    End Sub
#End Region
#Region "Client Properties"
    Public ReadOnly Property Prec As String
        Get
            If Precision Is Nothing Then
                Return "F2"
            ElseIf Precision = 3 Then
                Return "F3"
            ElseIf Precision = 2 Then
                Return "F2"
            Else
                Return "F2"
            End If
        End Get
    End Property
    Public Overrides Property Basis As String
        Get
            Return MyBase.Basis
        End Get
        Set(value As String)
            MyBase.Basis = value
            DataShow()
        End Set
    End Property
    Public Overrides Property Precision As Integer?
        Get
            Return MyBase.Precision
        End Get
        Set(value As Integer?)
            MyBase.Precision = value
            DataShow()
        End Set
    End Property
    ''' <summary>
    ''' Loaded Progression Measurements for making tolerance and reference lines
    ''' </summary>
    ''' <returns>Tolerance</returns>
    Public Overrides Property TolClass As Tolerance
        Get
            Return MyBase.TolClass
        End Get
        Set(value As Tolerance)
            MyBase.TolClass = value
            DataShow()
        End Set
    End Property
    ''' <summary>
    ''' Minimums Apply
    ''' </summary>
    ''' <returns>Boolean</returns>
    Public Property MinimumsApply As Boolean = True
    Public Overrides Property Data As Object
        Get
            Return MyBase.Data
        End Get
        Set(value As Object)
            MyBase.Data = value
            DataShow()
        End Set
    End Property
#End Region
#Region "Computed Properties"
    '''<summary>
    '''JobDetail containing data used for table.
    ''' </summary>
    ''' <Returns>JobDetail</Returns>
    Private ReadOnly Property JobDetails As JobDetail
        Get
            Return DirectCast(Me.Data, JobDetail)
        End Get
    End Property
    Private ReadOnly Property Blades As Integer?
        Get
            Return JobDetails?.Job?.PropellerBlades
        End Get
    End Property
    Private ReadOnly Property TeExclusion As Double?
        Get
            Return JobDetails?.Job?.TeExclusion
        End Get
    End Property
    Private ReadOnly Property LeExclusion As Double?
        Get
            Return JobDetails?.Job?.LeExclusion
        End Get
    End Property
    Private ReadOnly Property Diameter As Double?
        Get
            Return JobDetails?.Job?.PropellerDiameter
        End Get
    End Property
    Private ReadOnly Property RadiusMeasurements As List(Of RadiusMeasurement)
        Get
            Return JobDetails?.RadiusMeasurements
        End Get
    End Property
#End Region
#Region "Private Interface"
    Protected Overrides Sub DisplayInitialize()
        LabTitle.Text = kTableTitle ''' + Sys when system string is added
        With TLayoutCLBase
            .ColumnCount = Blades + 2
            .ColumnStyles.Clear()
            Dim lab As Label
            'Please use lowercase letters for loop variables.
            For i As Integer = 0 To .ColumnCount - 1
                .ColumnStyles.Add(New ColumnStyle(SizeType.AutoSize))
                If i = 0 Then
                    lab = New Label With {
                        .Text = "Blade",
                        .Dock = DockStyle.Fill,
                        .TextAlign = ContentAlignment.MiddleCenter,
                        .Name = "LabBlade"}
                    .Controls.Add(lab, 0, 0)
                ElseIf i = .ColumnCount - 1 Then
                    lab = New Label With {
                        .Text = "Allow",
                        .Dock = DockStyle.Fill,
                        .TextAlign = ContentAlignment.MiddleCenter,
                        .Name = "LabAllow"}
                    .Controls.Add(lab, i, 0)
                Else
                    lab = New Label With {
                        .Text = "Blade " + i.ToString(),
                        .Dock = DockStyle.Fill,
                        .TextAlign = ContentAlignment.MiddleCenter,
                        .Name = "LabBlade" + i.ToString()}
                    .Controls.Add(lab, i, 0)
                End If
            Next
            Dim meas As List(Of RadiusMeasurement) = RadiusMeasurements.Where(Function(r) r.BladeId = 1).ToList() '''' need to add labels and finish formatting
            .RowCount = meas.Count + 1
            .RowStyles.Clear()
            For I As Integer = 1 To .RowCount - 1
                .RowStyles.Add(New RowStyle(SizeType.AutoSize))
                lab = New Label With {
                    .Text = Math.Round(meas(I - 1).Radius.Value).ToString(),
                    .Dock = DockStyle.Fill,
                    .TextAlign = ContentAlignment.MiddleCenter,
                    .Name = "LabRadius" + I.ToString()}
                .Controls.Add(lab, 0, I)
                lab = New Label With {
                    .Text = "+/- 3%",
                    .Dock = DockStyle.Fill,
                    .TextAlign = ContentAlignment.MiddleCenter,
                    .Name = "LabAllow" + I.ToString()}
                .Controls.Add(lab, .ColumnCount - 1, I)
            Next
        End With
        MyBase.DisplayInitialize()
    End Sub
    Protected Overrides Sub DataShow()
        If JobDetails Is Nothing Then
            If LabTitle IsNot Nothing Then
                LabTitle.Text = kTableTitle
            End If
            Return
            End If
            With TLayoutCLBase
            For i As Integer = 1 To Blades
#Disable Warning BC42324 ' Using the iteration variable in a lambda expression may have unexpected results
                Dim meas As List(Of RadiusMeasurement) = RadiusMeasurements.Where(Function(r) r.BladeId = i).ToList()
#Enable Warning BC42324 ' Using the iteration variable in a lambda expression may have unexpected results
                For Each rm As RadiusMeasurement In meas
                    Dim cl As Double = GetChordLength(rm.CellMeasurements, Diameter, Math.Round(rm.Radius.Value))
                    Dim lab As New Label With {
                        .Text = cl.ToString("0.00"),
                        .Dock = DockStyle.Fill,
                        .TextAlign = ContentAlignment.MiddleCenter,
                        .Name = $"LabCL{i}_{Math.Round(rm.Radius.Value)}"}
                    .Controls.Add(lab, i, meas.IndexOf(rm) + 1)
                Next
            Next
        End With
    End Sub
#End Region
End Class
