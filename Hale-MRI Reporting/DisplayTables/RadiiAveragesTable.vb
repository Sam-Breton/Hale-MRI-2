Imports LibDatabase.Models

Public Class RadiiAveragesTable
    Inherits DisplayControl
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
    ''' <summary>
    ''' Data Used to plot chart
    ''' </summary>
    ''' <returns>JobDetail</returns>
    Public Property JobDetails As JobDetail = Nothing
    ''' <summary>
    ''' Tolerance Class for Chart1
    ''' </summary>
    ''' <returns>ToleranceClass</returns>
    'Public Property TolClass As Tolerance = Nothing
    ''' <summary>
    ''' Basis pitch style used for tolerance lines
    ''' </summary>
    ''' <returns>String</returns>
    'Public Property Basis As String = Nothing
    ''' <summary>
    ''' Allow progressive pitch for tolerance lines
    ''' </summary>
    ''' <returns>Boolean</returns>
    Public Property AllowProgressivePitch As Boolean = False
    ''' <summary>
    ''' Minimums Apply
    ''' </summary>
    ''' <returns>Boolean</returns>
    Public Property MinimumsApply As Boolean = False
    Public Property Designload As Boolean = False
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
            'BasisSet(value, Me.BasisPitch, Me.Radius)
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
            'TolClassSet(value)
            DataShow()
        End Set
    End Property
#End Region
#Region "Computed Properties"
    Public ReadOnly Property TEExclusion As Double
        Get
            Return JobDetails.Job.TeExclusion
        End Get
    End Property
    Public ReadOnly Property LEExclusion As Double
        Get
            Return JobDetails.Job.LeExclusion
        End Get
    End Property
    Public ReadOnly Property BladeCount As Integer
        Get
            Return JobDetails.Job.PropellerBlades
        End Get
    End Property
#End Region
#Region "Private Interface"
    Protected Overrides Sub DataShow()
        ' Remove magic numbers and strings, and define as constants.
        ' Add comments explaining what the code does and why.
        If JobDetails Is Nothing Or TolClass Is Nothing Then Return
        Dim radAvg As New List(Of Double)
        Dim bldAvg As Double
        For I As Integer = 1 To BladeCount + 2
            If I = BladeCount + 2 Then
                If Designload Then
                    'Load design radmeas here
                Else
                    Exit For ' exit because if no design load we dont have a column and it will cause exceptions
                End If
            ElseIf I = BladeCount + 1 Then
                'Get radii averages here
                For Each Doub As Double In radAvg
                    Dim avg As Double = Doub / BladeCount
                    Dim lab As New Label With {
                        .Text = avg.ToString("0.00"),
                        .Dock = DockStyle.Fill,
                        .TextAlign = ContentAlignment.MiddleCenter,
                        .Name = $"Mean{I}"}
                Next
            Else
#Disable Warning BC42324 ' Using the iteration variable in a lambda expression may have unexpected results
                Dim rms As List(Of RadiusMeasurement) = JobDetails.RadiusMeasurements.Where(Function(r) r.BladeId = I).OrderBy(Function(r) r.Radius).ToList()
#Enable Warning BC42324 ' Using the iteration variable in a lambda expression may have unexpected results
                For Each rm As RadiusMeasurement In rms
                    Dim pit As Double = GetRadiusMeasurementPitch(rm.CellMeasurements, TEExclusion, LEExclusion)
                    Dim lab As New Label With {
                        .Text = pit.ToString("0.00"),
                        .Dock = DockStyle.Fill,
                        .TextAlign = ContentAlignment.MiddleCenter,
                        .Name = $"B{I}R{Math.Round(rm.Radius.Value, 1)}"}
                    tLayoutRABase.Controls.Add(lab, I, rms.IndexOf(rm) + 1)
                    If I = 1 Then
                        radAvg.Add(pit)
                    Else
                        radAvg(I - 1) += pit
                    End If
                    bldAvg += pit
                    If rms.IndexOf(rm) = rms.Count - 1 Then
                        lab = New Label With {
                            .Text = bldAvg / tLayoutRABase.RowCount - 2,
                            .Dock = DockStyle.Fill,
                            .TextAlign = ContentAlignment.MiddleCenter,
                            .Name = "BldAvg" + I.ToString}
                        tLayoutRABase.Controls.Add(lab, I, rms.Count + 1)
                    End If
                Next
            End If
        Next
    End Sub

    Public Sub FormatTables() '''Add another row to contain blade averages and wheel pitch
        ' Instantiate and format all visual elements once, in DisplayIntialize()
        ' Remove magic numbers and strings, and define as constants.
        ' Add comments explaining whatg the code does and why.
        Dim meas As List(Of RadiusMeasurement) = JobDetails.RadiusMeasurements.Where(Function(r) r.BladeId = 1).ToList()
        With tLayoutRABase
            .ColumnCount = BladeCount + If(Designload, 4, 3)
            .ColumnStyles.Clear()
            .RowCount = meas.Count + 1
            .RowStyles.Clear()
            For I As Integer = 0 To .RowCount - 1
                .RowStyles.Add(New RowStyle(SizeType.AutoSize))
                If I = 0 Then
                    Dim lab As New Label With {
                        .Text = "r/R",
                        .Dock = DockStyle.Fill,
                        .TextAlign = ContentAlignment.MiddleCenter,
                        .Name = "r/R"}
                    .Controls.Add(lab, 0, I)
                ElseIf I = .RowCount - 1 Then
                    Dim lab As New Label With {
                        .Text = "Bld Avg.",
                        .Dock = DockStyle.Fill,
                        .TextAlign = ContentAlignment.MiddleCenter,
                        .Name = "BldAvg"}
                    .Controls.Add(lab, 0, 1)
                Else
                    Dim lab As New Label With {
                        .Text = meas(I - 1).Radius.Value.ToString("0.0"),
                        .Dock = DockStyle.Fill,
                        .TextAlign = ContentAlignment.MiddleCenter,
                        .Name = "Rad" & meas(I - 1).Radius.Value.ToString("0.0")}
                    .Controls.Add(lab, 0, I)
                End If
            Next
            For I As Integer = 1 To BladeCount
                .ColumnStyles.Add(New ColumnStyle(SizeType.AutoSize))
                Dim lab As New Label With {
                        .Text = "Bld " & I.ToString(),
                        .Dock = DockStyle.Fill,
                        .TextAlign = ContentAlignment.MiddleCenter,
                        .Name = "Blade" & I.ToString()}
                .Controls.Add(lab, I, 0)
            Next
            .ColumnStyles.Add(New ColumnStyle(SizeType.AutoSize))
            Dim lb As New Label With {
                .Text = "Mean",
                .Dock = DockStyle.Fill,
                .TextAlign = ContentAlignment.MiddleCenter,
                .Name = "Mean"}
            .Controls.Add(lb, BladeCount + 1, 0)
            If (Designload) Then
                .ColumnStyles.Add(New ColumnStyle(SizeType.AutoSize))
                Dim lab As New Label With {
                    .Text = "Design",
                    .Dock = DockStyle.Fill,
                    .TextAlign = ContentAlignment.MiddleCenter,
                    .Name = "Design"}
                .Controls.Add(lab, BladeCount + 2, 0)
            End If
        End With
    End Sub
#End Region
End Class
