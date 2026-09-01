Imports System.Windows.Forms.DataVisualization.Charting
Imports LibDatabase.Models

Public Class ChartPlot
    Inherits DisplayControl
    Private mAngDeviation As Boolean
    Private mMinimumsApply As Boolean
    Private mAllowProgressivePitch As Boolean
    Private mCustBasis As Double
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
    ''' <summary>
    ''' Data Used to plot chart
    ''' </summary>
    ''' <returns>JobDetail</returns>
    Public ReadOnly Property JobDetails As JobDetail
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
            'BasisSet(value, Me.BasisPitch, Me.Radius)
            'DataShow()
        End Set
    End Property
    Public Overrides Property Precision As Integer?
        Get
            Return MyBase.Precision
        End Get
        Set(value As Integer?)
            MyBase.Precision = value
            'DataShow()
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
    Public Property AngDeviation As Boolean
        Get
            Return mAngDeviation
        End Get
        Set(value As Boolean)
            mAngDeviation = value
            DataShow()
        End Set
    End Property
    Public Property MinimumsApply As Boolean
        Get
            Return mMinimumsApply
        End Get
        Set(value As Boolean)
            mMinimumsApply = value
            DataShow()
        End Set
    End Property
    Public Property AllowProgressivePitch As Boolean
        Get
            Return mAllowProgressivePitch
        End Get
        Set(value As Boolean)
            mAllowProgressivePitch = value
            DataShow()
        End Set
    End Property
    Public Property CustBasis As Double
        Get
            Return mCustBasis
        End Get
        Set(value As Double)
            mCustBasis = value
        End Set
    End Property
    Public Property BackCol As Color
        Get
            Return Chart1.BackColor
        End Get
        Set(value As Color)
            Chart1.BackColor = value
        End Set
    End Property
    Private ReadOnly Property BladeCount As Short?
        Get
            Return Me.JobDetails?.Job?.PropellerBlades
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
#Region "Computated Properties"
    Private ReadOnly Property BasisPitch As Double
        Get
            If JobDetails Is Nothing Then Return 0
            If Basis = "Marked" Then
                Return JobDetails.Job.MarkedPitch
            ElseIf Basis = "Desired" Then
                Return JobDetails.Job.DesiredPitch
            ElseIf Basis = "Progressive" Then
                Return JobDetails.WheelPitch
            ElseIf Basis = "Custom" Then
                Return CustBasis
            Else
                Basis = "Mean"
                Return JobDetails.WheelPitch
            End If
        End Get
    End Property
#End Region
#End Region
#Region "Private Interface"
    Protected Overrides Sub DisplayInitialize()
        ' Clear any existing chart areas and series.
        Chart1.ChartAreas.Clear()
        Chart1.Series.Clear()
        Chart1.Titles.Clear()
        Chart1.Legends.Clear()

        ' Add a ChartArea and Title for the point graph
        Dim chartArea1 As New ChartArea()
        chartArea1.AxisX.MajorGrid.Enabled = False
        chartArea1.AxisY.MajorGrid.Enabled = False
        chartArea1.AxisX.LabelStyle.Enabled = False
        chartArea1.AxisY.LabelStyle.Enabled = False
        chartArea1.AxisX.MajorTickMark.Enabled = False
        chartArea1.AxisY.MajorTickMark.Enabled = False
        chartArea1.AxisX.LineWidth = 0
        chartArea1.AxisY.LineWidth = 0
        chartArea1.Position = New ElementPosition(0, 0, 100, 100)
        chartArea1.InnerPlotPosition = New ElementPosition(0, 0, 100, 100)
        chartArea1.BackColor = Color.Transparent
        chartArea1.BackImageWrapMode = ChartImageWrapMode.Scaled
        Chart1.ChartAreas.Add(chartArea1)

        ' The chart axes min/max values are the greatest radius value,
        ' this way the arcs always start at the outside of the chart area.
        chartArea1.AxisX.Maximum = kBladePlotAxesMax
        chartArea1.AxisX.Minimum = -chartArea1.AxisX.Maximum
        chartArea1.AxisY.Maximum = chartArea1.AxisX.Maximum
        chartArea1.AxisY.Minimum = -chartArea1.AxisY.Maximum
        ' Each RadiusMeasurement is a new Series of Points that circumscribes an arc
        ' having a radius equal to RadiusMeasurement.Radius. 
        MyBase.DisplayInitialize()
    End Sub
    Protected Overrides Sub DataShow()
        If JobDetails Is Nothing Or TolClass Is Nothing Or Basis = "" Then Return
        Chart1.Series.Clear()
        ' Get a list of RadiusMeasurements for this JobDetail.
        Dim radiusMeasurements As List(Of RadiusMeasurement) =
            JobDetails?.RadiusMeasurements _
            .OrderBy(Function(b) b.BladeId) _
            .ThenBy(Function(r) CType(r.Radius, Double)) _
            .ToList()
        If radiusMeasurements.Count = 0 Then Return
        Dim x As Integer
        For x = 1 To BladeCount '''create points at the angle of the mid point of each blade to label which blade is which
            Dim midangfound As Boolean = False
            Dim midang As Double = 0
            Dim sr As New Series With {
                    .ChartType = SeriesChartType.Point,
                    .MarkerSize = 20,
                    .MarkerStyle = MarkerStyle.Star10,
                    .MarkerColor = GraphColorArray(x - 1),
                    .Name = "BladeLab" + x.ToString(),
                    .Label = x.ToString(),
                    .LabelForeColor = Color.Black}
            If JobDetails?.RadiusMeasurements.Contains(JobDetails?.RadiusMeasurements.FirstOrDefault(Function(r) r.BladeId = x)) Then
                Dim rad As RadiusMeasurement = JobDetails?.RadiusMeasurements.Where(Function(r) r.BladeId = x).FirstOrDefault()
                Dim mid As Double = GetChordMidAngle(rad.CellMeasurements)
                Dim bladelabpoint = PolarToCartesian(25, mid)
                sr.Points.AddXY(bladelabpoint.x, bladelabpoint.y)
                Chart1.Series.Add(sr)
            End If
            For Each rm As RadiusMeasurement In radiusMeasurements.Where(Function(r) r.BladeId = x).ToList()
                If AngDeviation Then ''' If displaying Angular Deviation find an applicable Radii between 65 and 75 and plot the Mid Chord Line through it
                    If midangfound = False Then
                        If rm.Radius >= 65 And rm.Radius <= 75 Then
                            midangfound = True
                            midang = GetChordMidAngle(rm.CellMeasurements)
                            Dim ser As New Series With {
                                .ChartType = SeriesChartType.Line,
                                .Name = "MidAngBlade" + x.ToString(),
                                .Color = Color.Black,
                                .BorderWidth = 3
                            }
                            Dim midangcoordslow = PolarToCartesian(25, midang)
                            Dim midangcoordshigh = PolarToCartesian(100, midang)
                            ser.Points.AddXY(midangcoordslow.x, midangcoordslow.y)
                            ser.Points.AddXY(midangcoordshigh.x, midangcoordshigh.y)
                            Chart1.Series.Add(ser)
                        End If
                    End If
                End If
                Dim s As New Series With {
                    .ChartType = SeriesChartType.Line,
                    .MarkerStyle = MarkerStyle.Circle,
                    .MarkerSize = 5,
                    .BorderWidth = 5
                }
                Dim cellMeasurements As List(Of CellMeasurement) = rm.CellMeasurements.ToList()
                Dim tolPitch As Double = BasisPitch
                Dim arcColors As New List(Of ToleranceColor)

                Dim sector As Integer = 1
                For sector = 1 To TolClass.LocalPitchSectors ''' populate arcColors with a list of tolerance colors
                    If AllowProgressivePitch Then
                        tolPitch = 0
                        For Each rad As RadiusMeasurement In JobDetails.RadiusMeasurements.Where(Function(r) Math.Round(r.Radius.Value) = Math.Round(rm.Radius.Value)).ToList()
                            tolPitch += GetLocalPitch(cellMeasurements, TolClass.LocalPitchSectors, sector, PropellerDiameter, rad.Radius, TEExclusion, LEExclusion)
                        Next
                        tolPitch /= BladeCount
                        arcColors.Add(CheckLocalPitchTolerance(TolClass, GetLocalPitch(cellMeasurements, TolClass.LocalPitchSectors, sector, PropellerDiameter, rm.Radius, TEExclusion, LEExclusion), tolPitch, MinimumsApply))
                    Else
                        arcColors.Add(CheckLocalPitchTolerance(TolClass, GetLocalPitch(cellMeasurements, TolClass.LocalPitchSectors, sector, PropellerDiameter, rm.Radius, TEExclusion, LEExclusion), BasisPitch, MinimumsApply))
                    End If
                Next
                Dim cellPerSector As Integer = (Math.Floor(cellMeasurements.Count / TolClass.LocalPitchSectors))
                ''' Plot points in the correct position with the correct color based on arcColor and Tolerance Class
                For i As Integer = 1 To cellMeasurements.Count - 1
                    Dim currentSector As Integer = Math.Truncate(i / cellPerSector)
                    Dim cmCurrent As CellMeasurement = cellMeasurements(i)
                    Dim cmPrevious As CellMeasurement = cellMeasurements(i - 1)
                    Dim angle As Double = (cmCurrent?.Angle + cmPrevious?.Angle) / 2
                    Dim coordinates = PolarToCartesian(rm.Radius, angle)
                    Dim p As Integer = s.Points.AddXY(coordinates.x, coordinates.y) ' Need a mathematical formula based on data in the dB or functions in MRIMath module x,y=f(a,b) ???
                    Dim pointcolor As ToleranceColor = arcColors(Math.Min(currentSector, arcColors.Count - 1))
                    s.Points(p).Color = ToColor(pointcolor)
                Next
                Chart1.Series.Add(s)
            Next
        Next
        MyBase.DataShow()
    End Sub
#End Region
End Class
