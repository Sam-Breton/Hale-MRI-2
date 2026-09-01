Imports System.Windows.Forms.DataVisualization.Charting
Imports LibDatabase.Models

Public Class ChartSectorsbyBlade
    ''' Displays the Average pitch of each Tolerance Sector of a given Radius of all blades - Bar Graph
    Inherits DisplayControl

#Region "Types and Constants"
    ' Chart configuration constants.
    Private Const kChartTitle As String = "Sectors By Blade"
    Private Const kSeriesName As String = "Blade"
    Private Const kLeadingEdgeLabel As String = "LE"
    Private Const kTrailingEdgeLabel As String = "TE"
    Private Const kYAxisTitle As String = "Segment Pitch"
    Private Const kYAxisMaxFactor As Double = 1.15#
    Private Const kYAxisMinFactor As Double = 0.75#
#End Region

#Region "Private Members"
    Private mBlades As New List(Of String)
    Private mRadius As Double? = Nothing
#End Region
#Region "Constructors"
    ''' <summary>
    ''' Creates a new ReportHeader object.
    ''' </summary>
    Public Sub New()
        MyBase.New()
        InitializeComponent()
    End Sub
#End Region
#Region "Public Interface"
#Region "Client Properties"
    Public Property Blades As List(Of String)
        Get
            If mBlades Is Nothing Then
                mBlades = New List(Of String)
            End If
            If mBlades.Count = 0 And BladeCount IsNot Nothing Then
                Dim x As Integer
                For x = 1 To BladeCount
                    mBlades.Add(x.ToString())
                Next
            End If
            Return mBlades
        End Get
        Set(value As List(Of String))
            mBlades = value
        End Set
    End Property
    Public Overrides Property Basis As String
        Get
            Return MyBase.Basis
        End Get
        Set(value As String)
            MyBase.Basis = value
            DisplayInitialize()
            BasisSet(value, Me.BasisPitch, Me.Radius)
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
    ''' Determines which radius is being measured against each other
    ''' </summary>
    ''' <returns>Double?</returns>
    Public Property Radius As Double?
        Get
            If mRadius Is Nothing Then
                mRadius = 50.0
            End If
            Return mRadius
        End Get
        Set(value As Double?)
            mRadius = value
            DisplayInitialize()
            BasisSet(Me.Basis, Me.BasisPitch, value)
            DataShow()
        End Set
    End Property
#End Region
#Region "Computated Properties"
    Private ReadOnly Property BasisPitch As Double?
        Get
            Dim result As Double?
            If Me.Basis = "Marked" Then
                result = Me.JobDetails?.Job?.MarkedPitch
            ElseIf Me.Basis = "Desired" Then
                result = Me.JobDetails?.Job?.DesiredPitch
            ElseIf Me.Basis = "Progressive" Then
                result = Me.JobDetails?.WheelPitch
            ElseIf Me.Basis = "Design" Then
                result = 0
            Else
                result = Me.JobDetails?.WheelPitch
            End If

            Return result
        End Get
    End Property

    Public ReadOnly Property BladeCount As Short?
        Get
            Return Me.JobDetails?.Job?.PropellerBlades
        End Get
    End Property

    Private ReadOnly Property JobDetails As JobDetail
        Get
            Return DirectCast(Me.Data, JobDetail)
        End Get
    End Property

    Private ReadOnly Property LEExclusion As Double?
        Get
            Return Me.JobDetails?.Job?.LeExclusion
        End Get
    End Property

    Private ReadOnly Property LocalPitchSectors As Integer?
        Get
            Return Me.TolClass?.LocalPitchSectors
        End Get
    End Property

    Private ReadOnly Property PropellerDiameter As Double?
        Get
            Return Me.JobDetails?.Job?.PropellerDiameter
        End Get
    End Property

    Private ReadOnly Property RadiusMeasurements As List(Of RadiusMeasurement)
        Get
            Return Me.JobDetails?.RadiusMeasurements
        End Get
    End Property

    Private ReadOnly Property TEExclusion As Double?
        Get
            Return Me.JobDetails?.Job?.TeExclusion
        End Get
    End Property

    Private ReadOnly Property ToleranceClass As String
        Get
            Return If(Me.TolClass?.ToleranceClass, String.Empty)
        End Get
    End Property
#End Region
#End Region
#Region "Private Interface"
    Protected Overrides Sub DisplayInitialize()
        ' Initialize the chart titles, legends and axes.
        If Chart1 IsNot Nothing Then
            With Chart1
                .Annotations.Clear()
                .Series.Clear()
                .Titles.Clear()
                .Titles.Add(New Title With {
                    .Name = "ChartTitle",
                    .Text = kChartTitle,
                    .Font = New Font("Segoe UI", 11.0F, FontStyle.Bold),
                    .Alignment = ContentAlignment.TopCenter
                 })
                .Legends("Legend1").Docking = Docking.Top
            End With

            With Chart1.ChartAreas("ChartArea1")
                .AxisY.Title = kYAxisTitle
                .AxisY.TitleFont = New Font("Segoe UI", 10.0F, FontStyle.Bold)
            End With

            ' Initialize font scaling.
            MyBase.DisplayInitialize()
        End If
    End Sub
    Private Sub BasisSet(basisValue As String, pitchValue As Double?, radiusValue As Double?)
        If basisValue IsNot Nothing AndAlso pitchValue IsNot Nothing AndAlso radiusValue IsNot Nothing Then
            Dim cArea As ChartArea = Chart1.ChartAreas("ChartArea1")
            cArea.AxisY.Minimum = Math.Round(pitchValue.Value * kYAxisMinFactor)
            cArea.AxisY.Maximum = Math.Round(pitchValue.Value * kYAxisMaxFactor)
            cArea.AxisY.Interval = Math.Round((cArea.AxisY.Maximum - cArea.AxisY.Minimum) / 9)
            cArea.AxisY.MinorTickMark.Enabled = True
            cArea.AxisY.MinorTickMark.Interval = Math.Round(cArea.AxisY.Interval / 5)
            Chart1.Legends("Legend1").Title = $"Radius {radiusValue} - Compare to {basisValue} - Minimums Apply"
        End If
    End Sub
    Protected Overrides Sub DataShow()
        If mDisplayInitialized Then
            ' Clear any current display data and reset the chart title.
            Chart1.Series.Clear()
            Dim chartTitle As Title = Chart1.Titles("ChartTitle")
            ' Make sure we have all required data.
            If Me.BladeCount IsNot Nothing AndAlso
                Me.RadiusMeasurements IsNot Nothing AndAlso
                Me.LocalPitchSectors IsNot Nothing AndAlso
                Me.Radius IsNot Nothing Then
                'Update the chart title according to the current TolClass.
                chartTitle.Text = $"{kChartTitle} - {ChartTitleGet(Me.TolClass)}" '''set up multi column display
                Chart1.Legends("Legend1").Title = $"Radius {Radius.Value} - Compare to {Basis} - Minimums Apply"
                For x As Integer = 1 To BladeCount
                    Dim seriesPitch As New Series() With {
                        .Name = $"{kSeriesName}{x}",
                        .ChartType = SeriesChartType.Column,
                        .ChartArea = "ChartArea1",
                        .IsXValueIndexed = True,
                        .Color = GraphColorArray(x - 1)}
                    Chart1.Series.Add(seriesPitch)
                    For y As Integer = 1 To LocalPitchSectors
#Disable Warning BC42324 ' Using the iteration variable in a lambda expression may have unexpected results
                        Dim bladeData As RadiusMeasurement = Me.RadiusMeasurements.Where(Function(r) r.BladeId = x And Math.Round(r.Radius.Value) = Math.Round(Radius.Value)).FirstOrDefault()
#Enable Warning BC42324 ' Using the iteration variable in a lambda expression may have unexpected results
                        Dim localpitch As Double = GetLocalPitch(bladeData.CellMeasurements, Me.LocalPitchSectors, y, Me.PropellerDiameter, Me.Radius, Me.TEExclusion, Me.LEExclusion)
                        Dim poin = seriesPitch.Points.AddXY(y, Math.Round(localpitch, If(Precision, 2)))
                        If y = 1 And y = LocalPitchSectors Then
                            seriesPitch.Points(poin).AxisLabel = "Local Pitch"
                        ElseIf y = 1 And y <> LocalPitchSectors Then
                            seriesPitch.Points(poin).AxisLabel = kLeadingEdgeLabel
                        ElseIf y <> 1 And y = LocalPitchSectors Then
                            seriesPitch.Points(poin).AxisLabel = kTrailingEdgeLabel
                        Else
                            seriesPitch.Points(poin).AxisLabel = y.ToString()
                        End If
                        seriesPitch.Points(poin).Color = GraphColorArray(x - 1)
                    Next
                Next
            End If
            MyBase.DataShow()
        End If

    End Sub
    'Protected Overrides Sub ShowData()
    'End Sub
    Private Function ChartTitleGet(tolClassValue As Tolerance) As String
        Dim titleText As String = String.Empty

        If tolClassValue IsNot Nothing Then
            'Dim title As Title = Chart1.Titles("TopTitle")
            If tolClassValue?.ToleranceClass = "C" Then
                titleText = "Local Pitch Custom Class"
            Else
                titleText = "Local Pitch ISO 484 " + tolClassValue?.ToleranceClass
            End If
        End If

        Return titleText
    End Function
#End Region
End Class