Imports System.Windows.Forms.DataVisualization.Charting
Imports LibDatabase.Models
Imports Windows.Media.Capture

Public Class ChartPosition
    Inherits DisplayControl
    Private mItems As String
    Private mAllowProgressivePitch
#Region "Constants"
    Private Const kChartTitle As String = "Radial Section Position"
    Private Const kSeriesName As String = "Blade"
    Private Const kYAxisTitle As String = "Blade Height Position"
    Private Const kXAxisTitle As String = "Relative Angle"
    Private Const kAxisOverFactor As Double = 1.1#
    Private Const kAxisUnderFactor As Double = 0.9#
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
#Region "Private Members"
    Private mBlades As New List(Of String) ''' list to store integer(string) values of blades to be displayed
    Private mRadii As New List(Of String) ''' list to store rounded integer(string) values of radii to be displayed
#End Region
#Region "Public Interface"
    Public Property Blades As List(Of String)
        Get
            Return mBlades
        End Get
        Set(value As List(Of String))
            mBlades = value
            DataShow()
        End Set
    End Property
    Public Property Radii As List(Of String)
        Get
            Return mRadii
        End Get
        Set(value As List(Of String))
            mRadii = value
            DataShow()
        End Set
    End Property
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
            If mDisplayInitialized = True Then
                'BasisSet(Chart1.ChartAreas("Summary"))
                DataShow()
            End If
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
    Public Overrides Property Data As Object
        Get
            Return MyBase.Data
        End Get
        Set(value As Object)
            MyBase.Data = value
            If Blades Is Nothing Then
                Blades = New List(Of String)
            End If
            If Blades.Count = 0 And BladeCount IsNot Nothing Then
                Dim x As Integer
                For x = 1 To BladeCount
                    Blades.Add(x.ToString())
                Next
            End If
            If Radii Is Nothing Then
                Radii = New List(Of String)
            End If
            If Radii.Count = 0 And RadiusMeasurements IsNot Nothing Then
                For Each rm As RadiusMeasurement In RadiusMeasurements
                    Radii.Add(Math.Round(rm.Radius.Value).ToString())
                Next
            End If
            DataShow()
        End Set
    End Property
#End Region
#End Region
#Region "Computed Properties"
    ''' <summary>
    ''' Basis pitch value used for tolerance lines based on Basis property and JobDetails data
    ''' </summary>
    ''' <returns>Double</returns>
    Private ReadOnly Property BasisPitch As Double?
        Get
            If JobDetails Is Nothing Then
                Return 0
            End If
            Select Case Basis
                Case "Marked"
                    Return JobDetails.Job.MarkedPitch
                Case "Desired"
                    Return JobDetails.Job.DesiredPitch
                Case "Design"
                    Return 0 ' need to set  up loading designs for comparison
                Case Else ' "Mean"
                    Return JobDetails.WheelPitch
            End Select
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

    Private ReadOnly Property RadiusMeasurements As List(Of RadiusMeasurement)
        Get
            Return Me.JobDetails?.RadiusMeasurements
        End Get
    End Property

#End Region
#Region "Private Interface"
    Protected Overrides Sub DisplayInitialize()
        Chart1.ChartAreas.Clear()
        Chart1.Series.Clear()
        Chart1.Legends.Clear()

        Dim cArea As ChartArea = Chart1.ChartAreas.Add("ChartArea1")
        Dim ser As Series = Chart1.Series.Add("Bld Avg")
        If (Chart1.Titles.Count = 0) Then
            Chart1.Titles.Add("Title1")
        End If
        Chart1.Titles("Title1").Text = kChartTitle
        cArea.AxisY.Title = kYAxisTitle
        cArea.AxisX.Title = kXAxisTitle
        cArea.AxisX.MajorGrid.Enabled = True
        cArea.AxisX.MinorGrid.Enabled = False
        cArea.AxisX.MinorTickMark.Enabled = True
        cArea.AxisY.MajorGrid.Enabled = True
        cArea.AxisY.MinorGrid.Enabled = False
        cArea.AxisY.MajorTickMark.Enabled = True
        cArea.AxisY.MinorTickMark.Enabled = True
        Chart1.Annotations.Clear()
    End Sub
    Protected Overrides Sub DataShow()
        If JobDetails Is Nothing Then
            Exit Sub
        End If
        Chart1.Series.Clear()
        Dim smallestAngle As Double = 0
        Dim largestAngle As Double = 0
        Dim smallestHeight As Double = 0
        Dim largestHeight As Double = 0
        For Each bladeId As String In Blades
            Dim angleadjust As Double = 360 / BladeCount * (CInt(bladeId) - 1) ''' calulated difference in angle so that every blade will move the same amount of angle
            Dim bladedata As List(Of RadiusMeasurement) = RadiusMeasurements.Where(Function(r) r.BladeId = CInt(bladeId)).ToList()
            For Each rm As RadiusMeasurement In bladedata
                If Radii.Contains(Math.Round(rm.Radius.Value).ToString()) Then
                    Dim mid = Math.Floor(rm.CellMeasurements.Count / 2)
                    Dim ser As Series = Chart1.Series.Add($"Bld{bladeId}{Math.Round(rm.Radius.Value)}")
                    ser.ChartArea = "ChartArea1"
                    ser.ChartType = SeriesChartType.Line
                    ser.BorderWidth = 2
                    ser.Color = GraphColorArray(CInt(bladeId))
                    Dim x As Integer = 0
                    For Each cm As CellMeasurement In rm.CellMeasurements
                        If x = mid Then
                            Dim poin = ser.Points.AddXY(GetChordMidAngle(rm.CellMeasurements) - angleadjust, GetChordMidDepth(rm.CellMeasurements))
                            ser.Points(poin).MarkerStyle = MarkerStyle.Cross
                            ser.Points(poin).MarkerSize = 25
                            ser.Points(poin).MarkerBorderWidth = 2
                        End If
                        Dim newangle As Double = cm.Angle.Value - angleadjust
                        Dim newheight As Double = cm.Depth.Value
                        If smallestAngle > newangle Or smallestAngle = 0 Then ''' these checks find the bounds of the displayed data to set the Axis limits
                            smallestAngle = newangle                                    ''' they check for = 0 to ensure that the smallest bounds are set to a value
                        End If
                        If largestAngle < newangle Or largestAngle = 0 Then
                            largestAngle = newangle
                        End If
                        If smallestHeight > newheight Or smallestHeight = 0 Then
                            smallestHeight = newheight
                        End If
                        If largestHeight < newheight Or largestHeight = 0 Then
                            largestHeight = newheight
                        End If
                        ser.Points.AddXY(newangle, newheight)
                        x += 1
                    Next
                End If
            Next
        Next
        Dim carea = Chart1.ChartAreas("ChartArea1")
        carea.AxisX.Maximum = Math.Round(largestAngle * kAxisOverFactor, 2)
        carea.AxisX.Minimum = Math.Round(smallestAngle * kAxisUnderFactor, 2)
        carea.AxisY.Maximum = Math.Round(largestHeight * kAxisOverFactor, 2)
        carea.AxisY.Minimum = Math.Round(smallestHeight * kAxisUnderFactor, 2)
        MyBase.DataShow()
    End Sub
#End Region
End Class
