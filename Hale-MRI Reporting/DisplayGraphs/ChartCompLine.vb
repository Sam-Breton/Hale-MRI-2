Imports System.ComponentModel
Imports System.Reflection.Emit
Imports System.Runtime.CompilerServices
Imports System.Security.Cryptography.Xml
Imports System.Windows.Forms.DataVisualization.Charting
Imports LibDatabase.Models
Imports Microsoft.EntityFrameworkCore.Metadata.Internal
Imports Windows.Services.Maps.LocalSearch
Imports Windows.UI.Input.Inking

Public Class ChartCompLine
    Inherits DisplayControl

#Region "Types and Constants"
    ' Chart configuration constants.
    Private Const kChartTitle As String = "Comp Line"
    Private Const kSeriesName As String = "LPLineArea"
    Private Const kChartAreaHeight As Single = 100.0!
    Private Const kChartAreaWidth As Single = 100.0!
    Private Const kAxisXMajorTickMarkIntervalFactor As Double = 100.0#
    Private Const kAxisXMajorTickMarkIntervalOffset As Double = 5.0#
    Private Const kAxisXMajorGridIntervalOffset As Double = 5.0#
    Private Const kAxisXMajorGridIntervalFactor As Double = 100.0#
    Private Const kAxisXMinorTickMarkInterval As Double = 2.0#
    Private Const kAxisXMinimum As Double = -5.0#
    Private Const kAxisXMaximum As Double = 105.0#
    Private Const kAxisXInterval As Double = 10.0#
    Private Const kAxisXIntervalOffset As Double = 5.0#
    Private Const kAxisYMajorTickMarkIntervalFactor As Double = 4.0#
    Private Const kAxisYMinorTickMarkIntervalFactor As Double = 8.0#
    Private Const kAxisYIntervalFactor As Double = 4.0#
    Private Const kSectionsDefault As Integer = 10
#End Region
#Region "Private Members"
    Private mItems As String
    Private mAxesScaling As Double = 0
    Private mSpline As Boolean = False
    Dim mSections As Integer = kSectionsDefault
#End Region
#Region "Constructors"
    ''' <summary>
    ''' Default constructor.
    ''' </summary>
    Public Sub New()
        MyBase.New()
        InitializeComponent()
        'Me.ContextMenuStrip = ContextMenuStrip1    ' Uncomment if adding a context menu strip to the control, don't forget to change the name if applicable.
    End Sub
#End Region
#Region "Public Interface"
#Region "Client Properties"
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
    '''<summary>
    ''' The blade where the desired RadiusMeasurement is contained
    ''' </summary>
    ''' <returns>Integer</returns>
    Public Property ChartBlade As Integer = 1
    '''<summary>
    ''' The Radius where the desired chord(list of cellmeasurements) is
    ''' </summary>
    ''' <returns>Double</returns>
    Public Property ChartRadius As Double = 0
    ''' <summary>
    ''' Loaded Progression Measurements for making tolerance and reference lines
    ''' </summary>
    ''' <returns>List(Of CellMeasurement)</returns>
    Public Property Prog As ProgRadiusMeasurement = New ProgRadiusMeasurement()
    ''' <summary>
    ''' the blade that is being used as the Track Reference
    ''' </summary>
    ''' <returns>List(Of CellMeasurement)</returns>
    Public Property TrackBlade As Integer?
    ''' <summary>
    ''' Reference Pitch
    ''' </summary>
    ''' <returns>Double</returns>
    Public Property RefPitch As Double = 0
    ''' <summary>
    ''' Determines max and min Y axis scaling
    ''' </summary>
    ''' <returns>Double</returns>
    Public Property AxesScaling As Double
        Get
            Return mAxesScaling
        End Get
        Set(value As Double)
            With Chart1.ChartAreas("ChartArea1")
                .AxisY.Minimum = -value
                .AxisY.Maximum = value
                .AxisY.MajorTickMark.Interval = value / kAxisYMajorTickMarkIntervalFactor
                .AxisY.MinorTickMark.Interval = value / kAxisYMinorTickMarkIntervalFactor
                .AxisY.Interval = value / kAxisYIntervalFactor
            End With
            mAxesScaling = value
            DataShow()
        End Set
    End Property
    '''<summary>
    '''determines whether thegraph is in spline or line mode
    '''</summary>
    '''<returns>Boolean</returns>
    Public Property Spline As Boolean
        Get
            Return mSpline
        End Get
        Set(value As Boolean)
            If mDisplayInitialized Then
                For Each ser As Series In Chart1.Series
                    If value Then
                        ser.ChartType = SeriesChartType.Spline
                    Else
                        ser.ChartType = SeriesChartType.Line
                    End If
                Next
            End If
            mSpline = value
        End Set
    End Property
    '''<summary>
    '''Show Track determines whether the chart points are offset by a height value taken from TrackCellMeasurements
    '''</summary>
    '''<returns>Boolean</returns>
    Public Property ShowTrack As Boolean = False ''' requires full redraw so no get set
    '''<summary>
    ''' Determines whther LEExclusion and TEExclusion are used in line graph calulations
    '''</summary>
    '''<returns>Boolean</returns>
    Public Property EntireScan As Boolean = False ''' requires full redraw so no get set
    ''' <summary>
    ''' Center Reference for placement of points
    ''' </summary>
    ''' <returns>Boolean</returns>
    Public Property CenterRef As Boolean = False ''' requires full redraw so no get set
    '''<summary>
    '''Number of sections to graph
    ''' </summary>
    ''' <returns>Integer</returns>
    Public Property Sections As Integer
        Get
            Return mSections
        End Get
        Set(value As Integer)
            If mDisplayInitialized Then
                With Chart1.ChartAreas("ChartArea1").AxisX
                    .MajorTickMark.Interval = kAxisXMajorTickMarkIntervalFactor / value
                    .MajorGrid.Interval = kAxisXMajorGridIntervalFactor / value
                End With
                ChartCompLine_SectionsUpdate()
            End If
            mSections = value
        End Set
    End Property
    Public Property ProgNewPitch As Double
        Get
            Return Prog.NewPitch
        End Get
        Set(Value As Double)
            Prog.NewPitch = Value
        End Set
    End Property
    Public Property ProgOldPitch As Double
        Get
            Return Prog.OldPitch
        End Get
        Set(Value As Double)
            Prog.OldPitch = Value
        End Set
    End Property
    Public Property ProgRads As List(Of RadiusMeasurement)
        Get
            Return Prog.Rads
        End Get
        Set(Value As List(Of RadiusMeasurement))
            Prog.Rads = Value
        End Set
    End Property
#End Region
#Region "Computed Properties"
    ''' <summary>
    ''' Gets the single Radiusmeasurement used to plot the Local height series in the chart
    ''' </summary>
    ''' <returns>RadiusMeasurement</returns>
    Public ReadOnly Property RadiusMeasurement As RadiusMeasurement
        Get
            Return JobDetails?.RadiusMeasurements.Where(Function(r) r.BladeId = ChartBlade And Math.Round(r.Radius.Value) = ChartRadius).FirstOrDefault()
        End Get
    End Property
    Public ReadOnly Property TrackCellMeasurements As List(Of CellMeasurement)
        Get
            Return JobDetails?.RadiusMeasurements.Where(Function(r) r.BladeId = TrackBlade And Math.Round(r.Radius.Value) = ChartRadius).FirstOrDefault().CellMeasurements
        End Get
    End Property
    ''' <summary>
    '''  Gets the Radius of the RadiusMeasurement
    ''' </summary>
    ''' <returns>Double?</returns>
    Public ReadOnly Property Radius As Double?
        Get
            Return RadiusMeasurement?.Radius
        End Get
    End Property
    ''' <summary>
    ''' Gets the Cell Measurements of the RadiusMeasurement
    ''' </summary>
    ''' <returns>List(Of CellMeasurement)</returns>
    Public ReadOnly Property CellMeasurements As List(Of CellMeasurement)
        Get
            Return RadiusMeasurement.CellMeasurements
        End Get
    End Property
    Private ReadOnly Property PropellerDiameter As Double?
        Get
            Return Me.JobDetails?.Job?.PropellerDiameter
        End Get
    End Property
    Public ReadOnly Property BladeCount As Short?
        Get
            Return Me.JobDetails?.Job?.PropellerBlades
        End Get
    End Property
    ''' <summary>
    ''' Returns the Trailing Edge Exclusion value from the associated Job
    ''' </summary>
    ''' <returns>Double</returns>
    Private ReadOnly Property TEExclusion As Double?
        Get
            If EntireScan Then
                Return 0
            Else
                Return If(Me.JobDetails?.Job?.TeExclusion, 0)
            End If
        End Get
    End Property
    ''' <summary>
    ''' Returns the Leading Edge Exclusion value from the associated Job
    ''' </summary>
    ''' <returns>Double</returns>
    Private ReadOnly Property LEExclusion As Double?
        Get
            If EntireScan Then
                Return 0
            Else
                Return If(Me.JobDetails?.Job?.LeExclusion, 0)
            End If
        End Get
    End Property
    Private Property HeightAtRefPoint As Double = 0
#End Region
#End Region
#Region "Private Interface"'finish formatting of the Graphs and make sure that the data is being displayed correctly
    Protected Overrides Sub DataShow()
        If Not mDisplayInitialized Then Return
        If RadiusMeasurement Is Nothing OrElse
                RefPitch = 0 Then
            Return
        End If
        ''set variables and acquire initial reference heights for each point
        Dim refheights As List(Of Double) = GetRefHeightsStraight(CenterRef, EntireScan, RefPitch, RadiusMeasurement.CellMeasurements)
        Dim cArea As ChartArea = Chart1.ChartAreas("ChartArea1")
        Dim ser As Series = Chart1.Series("Local Height")
        ser.Points.Clear()
        ser.Color = GraphColorArray(ChartBlade - 1)
        ser.MarkerStyle = MarkerStyle.Circle
        ser.MarkerSize = 12
        ser.MarkerColor = GraphColorArray(ChartBlade - 1)
        ser.BorderWidth = 3
        ChartCompLine_Add_Ref()
        Dim newheights As New List(Of Double)
        For x = 0 To 20 ''edit each Ref height by the height of the Ref point
            newheights.Add(HeightAtRefPoint - refheights(x))
        Next
        Dim lpline As New List(Of Double)
        ''' This loop gets and adjusts the height of each point on the line graph then adds it to the LPLine series
        For x = 0 To 20
            Dim q As Integer = Math.Abs(20 - x) 'this q value is the inverse of x so that the points are graphed from TE to LE - scanned data is from LE to TE
            If q = 0 Then
                lpline.Add(GetLocalHeightStartSector(CellMeasurements, 20, 1, PropellerDiameter, Radius, TEExclusion, LEExclusion) - newheights(q))
            Else
                lpline.Add(GetLocalHeightEndSector(CellMeasurements, 20, q, PropellerDiameter, Radius, TEExclusion, LEExclusion) - newheights(q))
            End If
            ser.Points.AddXY(x * 5, lpline(x))
        Next
        ChartCompLine_SectionsUpdate()
        cArea.AxisY.Title = $"Bld {ChartBlade} Rad {Math.Round(Radius.Value)}"
        MyBase.DataShow()
    End Sub

    Protected Overrides Sub DisplayInitialize()
        'Initialize the chart titles And axes.
        With Chart1
            .Annotations.Clear()
            .Legends.Clear()
            .Series("Local Height").Points.Clear()
            .Series("Ref").Points.Clear()
            .Series("TolHigh").Points.Clear()
            .Series("TolLow").Points.Clear()
            .Titles.Clear()
            .AntiAliasing = AntiAliasingStyles.All
            .TextAntiAliasingQuality = TextAntiAliasingQuality.High
            .Series("Local Height").ChartArea = .ChartAreas("ChartArea1").Name
            .Series("Ref").ChartArea = .ChartAreas("ChartArea1").Name
            .Series("TolHigh").ChartArea = .ChartAreas("ChartArea1").Name
            .Series("TolLow").ChartArea = .ChartAreas("ChartArea1").Name
            '.Titles.Add(New Title With {
            '    .Name = "ChartTitle",
            '    .Text = kChartTitle,
            '    .Font = New Font("Segoe UI", 11.0F, FontStyle.Bold),
            '    .Alignment = ContentAlignment.TopCenter
            ' })
        End With

        With Chart1.ChartAreas("ChartArea1")
            .Position.Auto = False
            .Position.Height = kChartAreaHeight
            .Position.Width = kChartAreaWidth
            .IsSameFontSizeForAllAxes = True
            .AxisY.Minimum = -Me.AxesScaling
            .AxisY.Maximum = Me.AxesScaling
            .AxisY.MajorGrid.Enabled = False
            .AxisY.MinorGrid.Enabled = False
            .AxisY.MajorTickMark.TickMarkStyle = TickMarkStyle.AcrossAxis
            .AxisY.MajorTickMark.Interval = Me.AxesScaling / kAxisYMajorTickMarkIntervalFactor
            .AxisY.MajorTickMark.Enabled = True
            .AxisY.MinorTickMark.TickMarkStyle = TickMarkStyle.InsideArea
            .AxisY.MinorTickMark.Interval = Me.AxesScaling / kAxisYMinorTickMarkIntervalFactor
            .AxisY.MinorTickMark.Enabled = True
            .AxisY.IntervalAutoMode = IntervalAutoMode.FixedCount
            .AxisY.LabelAutoFitMaxFontSize = Font.Size
            .AxisY.LabelAutoFitMinFontSize = Font.Size - 2
            .AxisY.IsLabelAutoFit = True
            .AxisY.Interval = AxesScaling / kAxisYIntervalFactor
            .AxisY.TitleFont = Font

            .AxisX.Minimum = kAxisXMinimum
            .AxisX.Maximum = kAxisXMaximum
            .AxisX.MajorGrid.Enabled = True
            .AxisX.MajorTickMark.TickMarkStyle = TickMarkStyle.AcrossAxis
            .AxisX.MajorTickMark.Enabled = True
            .AxisX.MajorTickMark.Interval = kAxisXMajorTickMarkIntervalFactor / Me.Sections
            .AxisX.MajorTickMark.IntervalOffset = kAxisXMajorTickMarkIntervalOffset
            .AxisX.MajorGrid.IntervalOffset = kAxisXMajorGridIntervalOffset
            .AxisX.MajorGrid.Interval = kAxisXMajorGridIntervalFactor / Me.Sections
            .AxisX.MinorGrid.Enabled = False
            .AxisX.MinorTickMark.Enabled = True
            .AxisX.MinorTickMark.Interval = kAxisXMinorTickMarkInterval
            .AxisX.MinorTickMark.TickMarkStyle = TickMarkStyle.InsideArea
            .AxisX.IntervalAutoMode = IntervalAutoMode.FixedCount
            .AxisX.Interval = kAxisXInterval
            .AxisX.IntervalOffset = kAxisXIntervalOffset
            .AxisX.LabelAutoFitMaxFontSize = Font.Size
            .AxisX.LabelAutoFitMinFontSize = Font.Size - 2
            .AxisX.IsLabelAutoFit = True
            .AxisX.TitleFont = Font
        End With
        Dim cArea As ChartArea = Chart1.ChartAreas("ChartArea1")
        Dim str As String
        ''' Adds a TE and LE annotation with the Exclusion values if Not EntireScan
        If LEExclusion > 0 Then
            str = "- " + LEExclusion.ToString()
        Else
            str = ""
        End If
        Dim Txtano As New TextAnnotation With {
            .Font = Font,
            .AxisX = cArea.AxisX,
            .AxisY = cArea.AxisY,
            .X = -3,
            .Y = AxesScaling / 2,
            .AllowMoving = False,
            .Text = "LE" + str}
        Chart1.Annotations.Add(Txtano)
        If TEExclusion > 0 Then
            str = "- " + TEExclusion.ToString()
        Else
            str = ""
        End If
        Txtano = New TextAnnotation With {
            .Font = Font,
            .AxisX = cArea.AxisX,
            .AxisY = cArea.AxisY,
            .X = 97,
            .Y = AxesScaling / 2,
            .AllowMoving = False,
            .Text = "TE" + str}
        Chart1.Annotations.Add(Txtano)

        MyBase.DisplayInitialize()
    End Sub

    'Protected Overrides Sub ShowData()
    'End Sub

    Private Sub ChartCompLine_Add_Ref() ' need to edit this to use ProgRadiusMeasurement to change tolerance lines
        ''' add all potential Reference line series to the chart area
        Dim refser As Series = Chart1.Series("Ref")
        Dim tolhighser As Series = Chart1.Series("TolHigh")
        Dim tollowser As Series = Chart1.Series("TolLow")
        refser.Points.Clear()
        tolhighser.Points.Clear()
        tollowser.Points.Clear()
        ''' acquire reference heights for each point
        Dim refheights As List(Of Double) = GetRefHeightsStraight(CenterRef, EntireScan, RefPitch, CellMeasurements)
        Dim x As Integer
        If Prog.Rads Is Nothing Then 'all creation and management of reference and tolerance lines are handled here
            ''' If there isn't a design loaded the reference height is determined by the selected reference point and Track measurements
            ''' the high and low tolerance lines are disabled unless there is a design loaded as they are based on the design's Radius Measurement
            refser.Points.AddXY(0, 0)
            refser.Points.AddXY(100, 0)
            If ShowTrack = True Then
                If CenterRef Then
                    HeightAtRefPoint = GetLocalHeightEndSector(TrackCellMeasurements, 20, 10, PropellerDiameter, Radius, LEExclusion, TEExclusion) 'need to be able to pull ref points from tracked blade
                Else
                    HeightAtRefPoint = GetLocalHeightEndSector(TrackCellMeasurements, 20, 20, PropellerDiameter, Radius, TEExclusion, LEExclusion)
                End If
            Else
                If CenterRef Then
                    HeightAtRefPoint = GetLocalHeightEndSector(CellMeasurements, 20, 10, PropellerDiameter, Radius, TEExclusion, LEExclusion)
                Else
                    HeightAtRefPoint = GetLocalHeightEndSector(CellMeasurements, 20, 20, PropellerDiameter, Radius, TEExclusion, LEExclusion)
                End If
            End If
            tollowser.Enabled = False
            tolhighser.Enabled = False
        Else
            'need to add in lines to edit by change in height from different pitchs the new class should hold the actual pitch of the section in Old pitch and the 
            'desired pitch in New pitch. need to implement a correction for scaled pitch - edit during print or hold edited values or use a list of heights
            Dim tollisthigh As List(Of Double) = GetRefHeightsHighTol(CenterRef, EntireScan, Prog.NewPitch, TolClass, Prog.Rads.Where(Function(r) Math.Round(r.Radius.Value) = Math.Round(Radius.Value)))
            Dim tollistlow As List(Of Double) = GetRefHeightsLowTol(CenterRef, EntireScan, Prog.NewPitch, TolClass, Prog.Rads.Where(Function(r) Math.Round(r.Radius.Value) = Math.Round(Radius.Value)))
            For x = 0 To Sections
                Dim height As Double
                If x = 0 Then
                    height = GetLocalHeightStartSector(Prog.Rads.Where(Function(r) Math.Round(r.Radius.Value) = Math.Round(Radius.Value)), Sections, 1, PropellerDiameter, Radius, TEExclusion, LEExclusion)
                Else
                    height = GetLocalHeightEndSector(Prog.Rads.Where(Function(r) Math.Round(r.Radius.Value) = Math.Round(Radius.Value)), Sections, x, PropellerDiameter, Radius, TEExclusion, LEExclusion)
                End If
                height -= refheights(x)  'need to add a change in here that changes height based on center ref point and the ref height at that point
                refser.Points.AddXY(x * (100 / Sections), height)
                tolhighser.Points.AddXY(x * (100 / Sections), tollisthigh(x))
                tollowser.Points.AddXY(x * (100 / Sections), tollistlow(x))
            Next
            If ShowTrack = True Then
                If CenterRef Then
                    HeightAtRefPoint = GetLocalHeightEndSector(Prog.Rads.Where(Function(r) Math.Round(r.Radius.Value) = Math.Round(Radius.Value)), 20, 10, PropellerDiameter, Radius, TEExclusion, LEExclusion)
                Else
                    HeightAtRefPoint = GetLocalHeightStartSector(Prog.Rads.Where(Function(r) Math.Round(r.Radius.Value) = Math.Round(Radius.Value)), 20, 1, PropellerDiameter, Radius, TEExclusion, LEExclusion)
                End If
            Else
                If CenterRef Then
                    HeightAtRefPoint = GetLocalHeightEndSector(CellMeasurements, 20, 10, PropellerDiameter, Radius, TEExclusion, LEExclusion)
                Else
                    HeightAtRefPoint = GetLocalHeightStartSector(CellMeasurements, 20, 1, PropellerDiameter, Radius, TEExclusion, LEExclusion)
                End If
            End If
            tollowser.Enabled = True
            tolhighser.Enabled = True
        End If
    End Sub
    Private Sub ChartCompLine_SectionsUpdate()
        Dim cArea As ChartArea = Chart1.ChartAreas("ChartArea1")
        Chart1.Annotations.Clear()
        Dim str As String
        ''' Adds a TE and LE annotation with the Exclusion values if Not EntireScan
        If LEExclusion > 0 Then
            str = "- " + LEExclusion.ToString()
        Else
            str = ""
        End If
        Dim Txtano As New TextAnnotation With {
            .Font = Font,
            .AxisX = cArea.AxisX,
            .AxisY = cArea.AxisY,
            .X = -3,
            .Y = AxesScaling / 2,
            .AllowMoving = False,
            .Text = "LE" + str}
        Chart1.Annotations.Add(Txtano)
        If TEExclusion > 0 Then
            str = "- " + TEExclusion.ToString()
        Else
            str = ""
        End If
        Txtano = New TextAnnotation With {
            .Font = Font,
            .AxisX = cArea.AxisX,
            .AxisY = cArea.AxisY,
            .X = 97,
            .Y = AxesScaling / 2,
            .AllowMoving = False,
            .Text = "TE" + str}
        Chart1.Annotations.Add(Txtano)
        ''' bunch of math that finds the angle bounds of the sector excluding TE and LE Exclusion zones the EntireScan property is handled 
        ''' when LE and TE Exclusion properties are called so that additional checks don't need to be made here
        Dim sectionsHeightdiff As New List(Of Double)
        Dim StartAngle As Double = CellMeasurements.FirstOrDefault().Angle
        Dim EndAngle As Double = CellMeasurements.LastOrDefault().Angle
        Dim TotAngle As Double
        If EndAngle < 0 Then
            TotAngle = Math.Abs(StartAngle + 360) - (EndAngle + 360) ' handles negative end angles by making them positive before finding the difference
        Else
            TotAngle = Math.Abs(StartAngle - EndAngle)
        End If
        Dim cl As Double = GetChordLength(CellMeasurements, PropellerDiameter, Math.Round(Radius.Value))
        If cl <> 0 Then
            StartAngle -= (TotAngle * TEExclusion / cl)
            EndAngle += (TotAngle * LEExclusion / cl)
        End If
        If EndAngle < 0 Then
            TotAngle = Math.Abs(StartAngle + 360) - (EndAngle + 360)
        Else
            TotAngle = Math.Abs(StartAngle - EndAngle)
        End If
        ''' using the Total angle of the RadiusMeasurement, the number of sections and the Reference Pitch 
        ''' we can find the ideal height difference between each section to plot the height difference at each section
        ''' Delta height = (Delta Angle / 360) * Pitch
        Dim anglediffbetweenpoints As Double = TotAngle / Sections
        Dim heightdiffbetweenpoints As Double = (RefPitch * anglediffbetweenpoints) / 360
        For x = 0 To Sections
            Dim q = Sections - x
            If CenterRef Then
                sectionsHeightdiff.Add(HeightAtRefPoint - (heightdiffbetweenpoints * (q - (Sections / 2))))
            Else
                sectionsHeightdiff.Add(HeightAtRefPoint - (heightdiffbetweenpoints * q))
            End If
        Next
        Dim p As Double
        Dim tcol As Color
        '''this loop adds striplines at each sector with text showing local pitch of that sector and annotations showing the height at the end of each sector
        '''editting this to make annotations aswell because the sections property changes the x axis major grid to match the sections
        For x = 0 To Sections
            Dim q As Integer = Sections - x ''' inverse of x so that striplines and annotations are added from TE to LE
            Dim textAnnot As TextAnnotation
            If x <> 0 Then
                ''' start of Local pitch strip lines
                p = GetLocalPitch(CellMeasurements, Sections, x, PropellerDiameter, Radius, TEExclusion, LEExclusion)
                Dim why As ToleranceColor = CheckLocalPitchTolerance(TolClass, p, RefPitch, True)
                If why = ToleranceColor.Pass Then
                    tcol = Color.Black
                ElseIf why = ToleranceColor.High Then
                    tcol = Color.Red
                Else
                    tcol = ToColor(why)
                End If
                'Dim stripline As New StripLine With {
                '    .Interval = 100 / Sections,
                '    .IntervalOffset = 5,
                '    .StripWidth = 150}
                'cArea.AxisX.StripLines.Add(stripline)
                textAnnot = New TextAnnotation With {
                        .Font = Font,
                        .AxisX = cArea.AxisX,
                        .AxisY = cArea.AxisY,
                        .X = (q) * (100 / Sections),
                        .ForeColor = tcol,
                        .Text = Math.Round(p, 2).ToString("F2"),
                        .Y = AxesScaling}
                Chart1.Annotations.Add(textAnnot)
            End If
            ''' start of height difference annotations
            Dim Anon As TextAnnotation
            If x = 0 Then
                Dim heit As Double = GetLocalHeightEndSector(CellMeasurements, Sections, Sections, PropellerDiameter, Radius, TEExclusion, LEExclusion)
                heit -= sectionsHeightdiff(Sections)
                Anon = New TextAnnotation With {
                    .Font = Font,
                    .AxisX = cArea.AxisX,
                    .AxisY = cArea.AxisY,
                    .X = 0,
                    .Y = 0,
                    .AllowMoving = False,
                    .Text = Math.Round(heit, 3).ToString("F3")}
            ElseIf x = Sections Then
                Dim heit As Double = GetLocalHeightStartSector(CellMeasurements, Sections, 1, PropellerDiameter, Radius, TEExclusion, LEExclusion)
                heit -= sectionsHeightdiff(0)
                Anon = New TextAnnotation With {
                    .Font = Font,
                    .AxisX = cArea.AxisX,
                    .AxisY = cArea.AxisY,
                    .X = 95,
                    .Y = 0,
                    .AllowMoving = False,
                    .Text = Math.Round(heit, 3).ToString("F3")}
            Else
                Dim heit As Double = GetLocalHeightEndSector(CellMeasurements, Sections, q, PropellerDiameter, Radius, TEExclusion, LEExclusion)
                heit -= sectionsHeightdiff(q)
                Anon = New TextAnnotation With {
                    .Font = Font,
                    .AxisX = cArea.AxisX,
                    .AxisY = cArea.AxisY,
                    .X = (x) * (100 / Sections),
                    .Y = 0,
                    .AllowMoving = False,
                    .Text = Math.Round(heit, 3).ToString("F3")}
            End If
            Chart1.Annotations.Add(Anon)
        Next
    End Sub

    Private Sub ChartCompLine_FontChanged(sender As Object, e As EventArgs) Handles MyBase.FontChanged
        Dim cArea As ChartArea = Chart1.ChartAreas("ChartArea1")
        cArea.AxisX.LabelStyle.Font = Font
        cArea.AxisX.TitleFont = Font
        cArea.AxisY.LabelStyle.Font = Font
        cArea.AxisY.TitleFont = Font
    End Sub
#End Region
End Class
