Imports System.ComponentModel
Imports System.Windows.Forms.DataVisualization.Charting
Imports LibDatabase.Models

Public Class ChartBladeAverage
    Inherits DisplayControl

#Region "Types and Constants"
    ' Chart configuration constants.
    Private Const kChartTitle As String = "Blade Averages"
    Private Const kSeriesName As String = "Pitch"
    Private kStripBorderColor As Color = Color.Black
    Private kStripColorUnder As Color = Color.Red
    Private kStripColorOver As Color = Color.Blue
    Private Const kXAxisInterval As Double = 1.0#
    Private Const kXAxisMinimum As Double = 0#
    Private Const kXAxisTitle As String = "Blade"
    Private Const kYAxisFactor As Double = 1.2#
    Private Const kYAxisInterval As Double = 1.0#
    Private Const kYAxisMajorTickInterval As Double = 5.0#
    Private Const kYAxisMinorTickInterval As Double = 1.0#
    Private Const kStripBorderWidth As Integer = 2
    Private Const kStripLineWidth As Double = 0.01#
    Private Const kYAxisMinimum As Double = 0#
#End Region
#Region "Private Members"
    Private mItems As String
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
#Region "Public Interface"
#Region "Computed Properties"
    <Browsable(False)>
    Private ReadOnly Property BasisPitch As Double?
        Get
            If JobDetails Is Nothing Then
                Return 0
            End If
            Select Case Basis
                Case "Marked"
                    Return JobDetails?.Job?.MarkedPitch
                Case "Desired"
                    Return JobDetails?.Job?.DesiredPitch
                Case "Design"
                    Return 0 ' need to set up loading designs for comparison
                Case Else ' "Mean"
                    Return JobDetails?.WheelPitch
            End Select
        End Get
    End Property

    ''' <summary>
    ''' Propeller blade count.
    ''' </summary>
    ''' <returns>Integer?</returns>
    <Browsable(False)>
    Public ReadOnly Property BladeCount As Short?
        Get
            Return Me.JobDetails?.Job?.PropellerBlades
        End Get
    End Property

    ''' <summary>
    ''' Provides chart metadata.
    ''' </summary>
    ''' <returns>JobDetail</returns>
    <Browsable(False)>
    Private ReadOnly Property JobDetails As JobDetail
        Get
            Return DirectCast(Me.Data, JobDetail)
        End Get
    End Property
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

    <Browsable(False)>
    Private ReadOnly Property LEExclusion As Double?
        Get
            Return Me.JobDetails?.Job?.LeExclusion
        End Get
    End Property

    ''' <summary>
    ''' Blade radius measurements.
    ''' </summary>
    ''' <returns></returns>
    <Browsable(False)>
    Private ReadOnly Property RadiusMeasurements As List(Of RadiusMeasurement)
        Get
            ' TODO: Check with Hale if this is correct.
            Return Me.JobDetails?.RadiusMeasurements
        End Get
    End Property

    Private ReadOnly Property TEExclusion As Double?
        Get
            Return Me.JobDetails?.Job?.TeExclusion
        End Get
    End Property
#End Region
#End Region
#Region "Private Interface"
    Protected Overrides Sub DataGet()
        ' Get any data required by this chart.
        Me.TolClass = If(Me.TolClass, Me.JobDetails?.ToleranceClassNavigation)
        Me.Basis = If(Me.Basis, "Marked")
        MyBase.DataGet()
    End Sub

    Protected Overrides Sub DataShow()
        If mDisplayInitialized Then
            ' Clear any displayed data and reset the chart title.
            Chart1.Series.Clear()
            ' Ensure required properties are set.
            If Me.JobDetails IsNot Nothing AndAlso
                Me.TolClass IsNot Nothing AndAlso
                Me.Basis IsNot Nothing AndAlso
                Me.RadiusMeasurements IsNot Nothing Then

                ' Create a new Series.
                Dim cArea As ChartArea = Chart1.ChartAreas("ChartArea1")
                Dim seriesPitch As New Series() With {
                    .Name = kSeriesName,
                    .ChartType = SeriesChartType.Bar,
                    .ChartArea = cArea.Name
                }

                ' Plot each blade's average pitch.
                For x As Integer = 1 To Me.BladeCount
                    Dim b As Integer = x
                    Dim avgpitch As Double = 0
                    Dim pitchcount As Integer = 0
                    For Each rm As RadiusMeasurement In Me.RadiusMeasurements.Where(Function(r) r.BladeId = b)
                        'avgpitch += GetRadiusMeasurementPitch(rm.CellMeasurements.ToList(), Me.TEExclusion.Value, Me.LEExclusion.Value)
                        avgpitch += GetRadiusMeasurementPitch(rm.CellMeasurements, Me.TEExclusion.Value, Me.LEExclusion.Value)
                        pitchcount += 1
                    Next
                    If pitchcount > 0 Then
                        avgpitch /= pitchcount
                    End If
                    Dim pointind As Integer = seriesPitch.Points.AddXY(b, avgpitch)
                    seriesPitch.Points(pointind).Color = GraphColorArray(b - 1)
                Next

                ' Set Y-axis limits and tolerance lines based on the selected basis and tolerance class.
                With cArea.AxisY
                    .Maximum = Me.BasisPitch * kYAxisFactor
                    .MajorGrid.Interval = .Maximum
                    .StripLines(0).IntervalOffset = Me.BasisPitch - (Me.BasisPitch * (TolClass.MeanPitchPerBladePercent / 100))
                    .StripLines(0).Text = (Me.BasisPitch - (Me.BasisPitch * (TolClass.MeanPitchPerBladePercent / 100))).ToString()
                    .StripLines(1).IntervalOffset = Me.BasisPitch + (Me.BasisPitch * (TolClass.MeanPitchPerBladePercent / 100))
                    .StripLines(1).Text = (Me.BasisPitch + (Me.BasisPitch * (TolClass.MeanPitchPerBladePercent / 100))).ToString()
                End With

                ' Add the series to the chart.
                Chart1.Series.Add(seriesPitch)
            End If
        End If
        MyBase.DataShow()
    End Sub

    Protected Overrides Sub DisplayInitialize()
        ' Initialize the chart titles and axes.
        With Chart1
            .Annotations.Clear()
            .ChartAreas.Clear()
            .Legends.Clear()
            .Series.Clear()
            .Titles.Clear()
            .ChartAreas.Add("ChartArea1")
            .Titles.Add(New Title With {
                .Name = "ChartTitle",
                .Text = kChartTitle,
                .Font = New Font("Segoe UI", 11.0F, FontStyle.Bold),
                .Alignment = ContentAlignment.TopCenter
             })
        End With

        With Chart1.ChartAreas("ChartArea1")
            .AxisX.Interval = kXAxisInterval
            .AxisX.IsMarginVisible = True
            .AxisX.Minimum = kXAxisMinimum
            .AxisX.Title = kXAxisTitle
            .AxisX.TitleFont = New Font("Segoe UI", 10, FontStyle.Bold)
            .AxisX2.Enabled = AxisEnabled.False
            .AxisY.Interval = kYAxisInterval
            .AxisY.Minimum = kYAxisMinimum
            .AxisY.MinorTickMark.Enabled = True
            .AxisY.MinorTickMark.Interval = kYAxisMinorTickInterval
            .AxisY.MajorTickMark.Enabled = True
            .AxisY.MajorTickMark.Interval = kYAxisMajorTickInterval
            .AxisY.MajorGrid.Enabled = True
            .AxisY.StripLines.Add(New StripLine With {
                .StripWidth = kStripLineWidth,
                .BorderColor = kStripBorderColor,
                .BorderWidth = kStripBorderWidth,
                .TextOrientation = TextOrientation.Horizontal,
                .TextLineAlignment = StringAlignment.Near,
                .ForeColor = kStripColorUnder
            })
            .AxisY.StripLines.Add(New StripLine With {
                .StripWidth = kStripLineWidth,
                .BorderColor = kStripBorderColor,
                .BorderWidth = kStripBorderWidth,
                .TextOrientation = TextOrientation.Horizontal,
                .TextLineAlignment = StringAlignment.Far,
                .ForeColor = kStripColorOver
            })
            .AxisY.TitleFont = New Font("Segoe UI", 10, FontStyle.Bold)
            .AxisY2.Enabled = AxisEnabled.False
        End With

        ' Initial font scaling.
        MyBase.DisplayInitialize()
    End Sub
#End Region
End Class
