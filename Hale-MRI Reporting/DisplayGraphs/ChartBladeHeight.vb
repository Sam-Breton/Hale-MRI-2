Imports System.ComponentModel
Imports System.Windows.Forms.DataVisualization.Charting
Imports LibDatabase.Models

Public Class ChartBladeHeight
    Inherits DisplayControl
#Region "Types and Constants"
    ' Chart configuration constants.
    Private Const kChartTitle As String = "Blade Height"
    Private Const kSeriesName As String = "BladeHeight"
    Private Const kLabelBlade As String = "Blade"
    Private Const kLabelHeight As String = "Height"
    Private Const kYAxisTitle As String = "Blade Height"
    Private Const kHeightOffset As Double = 0.2#
    Private Const kYAxisMinimum As Double = 0#
    Private Const kYAxisMaximum As Double = kHeightOffset * 2
    Private Const kYAxisInterval As Double = 0.05#
#End Region
#Region "Constructors"
    ''' <summary>
    ''' Default constructor.
    ''' </summary>
    Public Sub New()
        MyBase.New()
        InitializeComponent()
        Me.ContextMenuStrip = ContextMenuStrip1
    End Sub
#End Region
#Region "Public Interface"
#Region "Client Properties"
    ''' <summary>
    ''' Propeller reference blade.
    ''' </summary>
    ''' <returns>Integer?</returns>
    <Browsable(False)>
    Public Property ReferenceBlade As Integer? = Nothing

    ''' <summary>
    ''' Blade reference point.
    ''' </summary>
    ''' <returns>String</returns>
    <Browsable(False)>
    Public Property ReferencePoint As String = Nothing

    ''' <summary>
    ''' Blade reference radius.
    ''' </summary>
    ''' <returns></returns>
    <Browsable(False)>
    Public Property ReferenceRadius As Double? = Nothing
#End Region
#Region "Computed Properties"
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

    Private ReadOnly Property JobDetails As JobDetail
        Get
            Return DirectCast(Me.Data, JobDetail)
        End Get
    End Property

    <Browsable(False)>
    Private ReadOnly Property RadiusMeasurements As List(Of RadiusMeasurement)
        Get
            ' Fixed ... needed to flip the Radius and BladeId checks. We want all radius measurements with the reference Radius, not all radius measurements for the reference blade.
            Return DirectCast(Me.Data, JobDetail)?.RadiusMeasurements.
                Where(Function(r) Math.Round(CType(r.Radius, Double)) = ReferenceRadius).
                OrderBy(Function(r) r.BladeId).ToList()
        End Get
    End Property

    ''' <summary>
    ''' Depth at reference radius and point.
    ''' </summary>
    ''' <returns>Double?</returns>
    <Browsable(False)>
    Private ReadOnly Property ReferenceDepth As Double?
        Get
            Return TrackGetDepth(ReferenceRadiusMeasurement, ReferencePoint)
        End Get
    End Property
    Public Overrides Property Precision As Integer?
    Public Overrides Property Basis As String
    Public Overrides Property TolClass As Tolerance
#End Region
#End Region
#Region "Private Interface"
    Protected Overrides Sub ContextMenuStripSet()
        ' Initialize the chart's specific ContextMenuStrip.
        If Me.ContextMenuStrip?.Enabled = False Or Me.ContextMenuStrip Is Nothing Then
            Me.ContextMenuStrip = Nothing
            Return
        End If
        Dim referenceBlades As ToolStripMenuItem = DirectCast(ContextMenuStrip1.Items("ReferenceBladeToolStripMenuItem"), ToolStripMenuItem)
        Dim referencePoints As ToolStripMenuItem = DirectCast(ContextMenuStrip1.Items("ReferencePointToolStripMenuItem"), ToolStripMenuItem)
        Dim referenceRadii As ToolStripMenuItem = DirectCast(ContextMenuStrip1.Items("ReferenceRadiusToolStripMenuItem"), ToolStripMenuItem)
        If Me.Data IsNot Nothing Then
            'Dim jd As JobDetail = DirectCast(Me.Data, JobDetail)
            ReferenceBladesSet(Me.BladeCount)

            Dim refBlade As ToolStripMenuItem = DirectCast(referenceBlades.DropDownItems(0), ToolStripMenuItem)
            refBlade.Checked = True
            ReferenceRadiiSet(refBlade.Text)

            Dim refRadius As ToolStripMenuItem = DirectCast(referenceRadii.DropDownItems(0), ToolStripMenuItem)
            Dim refPoint As ToolStripMenuItem = DirectCast(referencePoints.DropDownItems(0), ToolStripMenuItem)
            refRadius.Checked = True
            refPoint.Checked = True
            'Me.BladeCount = jd.Job.PropellerBlades
            Me.ReferenceBlade = refBlade.Text
            Me.ReferenceRadius = refRadius.Name
            Me.ReferencePoint = refPoint.Text
            Me.ReferenceRadius = refRadius.Text
        End If
        referenceBlades.Enabled = Me.Data IsNot Nothing
        referencePoints.Enabled = referenceBlades.Enabled
        referenceRadii.Enabled = referenceBlades.Enabled

        MyBase.ContextMenuStripSet()
    End Sub

    Protected Overrides Sub DataShow()
        If mDisplayInitialized Then
            ' Clear any displayed data and reset the chart title.
            Dim seriesHeight As Series = Chart1.Series(kSeriesName)
            Dim chartTitle As Title = Chart1.Titles("ChartTitle")
            seriesHeight.Points.Clear()
            chartTitle.Text = kChartTitle

            ' Ensure required properties are set.
            If BladeCount IsNot Nothing AndAlso
            RadiusMeasurements IsNot Nothing AndAlso
            ReferenceBlade IsNot Nothing AndAlso
            Not String.IsNullOrEmpty(ReferencePoint) AndAlso
            ReferenceRadius IsNot Nothing Then
                ' Update the chart title with the current display settings.
                chartTitle.Text = $"{kChartTitle} (Ref: Blade {ReferenceBlade} at {ReferenceRadius}%, {ReferencePoint})"

                For i As Integer = 1 To Me.BladeCount
                    Dim b As Short = i - 1
                    ' ... and plot each blade's data points.
                    If RadiusMeasurements.Count < i Then ''' stop an Index out of range error from occuring
                        MyBase.DataShow()
                        Exit Sub
                    End If
                    If RadiusMeasurements(b) IsNot Nothing Then
                        Dim bladeid As Integer = RadiusMeasurements(b).BladeId
                        Dim bladeDepth As Double = TrackGetDepth(RadiusMeasurements(b), ReferencePoint)
                        Dim bladeHeight As Double = (ReferenceDepth.Value - bladeDepth) + kHeightOffset
                        Dim bladeValue As String = (ReferenceDepth.Value - bladeDepth).ToString("F2")

                        Dim p As Integer = seriesHeight.Points.AddXY($"B{bladeid}=({bladeValue})", bladeHeight)
                        seriesHeight.Points(p).Color = GraphColorArray(bladeid - 1)
                    End If
                Next
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
            .AntiAliasing = AntiAliasingStyles.All
            .TextAntiAliasingQuality = TextAntiAliasingQuality.High
            .ChartAreas.Add("ChartArea1")
            .Titles.Add(New Title With {
                .Name = "ChartTitle",
                .Text = kChartTitle,
                .Font = New Font("Segoe UI", 11.0F, FontStyle.Bold),
                .Alignment = ContentAlignment.TopCenter
             })
        End With

        With Chart1.ChartAreas("ChartArea1")
            .Position.Auto = True
            .AxisY.IsLabelAutoFit = True
            .AxisY.Interval = kYAxisInterval
            .AxisY.Minimum = kYAxisMinimum
            .AxisY.Title = kYAxisTitle
            .AxisY.TitleFont = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        End With

        Chart1.Series.Add(New Series With {
            .Name = kSeriesName,
            .ChartType = SeriesChartType.Column,
            .XValueMember = kLabelBlade,
            .YValueMembers = kLabelHeight,
            .IsXValueIndexed = True,
            .IsVisibleInLegend = False
        })

        ' Initial font scaling.
        MyBase.DisplayInitialize()
    End Sub

    ''' <summary>
    ''' This property retrieves the RadiusMeasurement for a given blade ID 
    ''' from the Data property.
    ''' </summary>
    ''' <param name="blade"></param>
    ''' <returns>RadiusMeasurement</returns>
    'Private ReadOnly Property ReferenceBladeMeasurement(ByVal blade As Integer) As RadiusMeasurement
    '    Get
    '        Dim measurements As List(Of RadiusMeasurement) = Me.RadiusMeasurements
    '        Dim refBladeMeasurement As RadiusMeasurement = measurements?.FirstOrDefault(Function(r) Math.Round(CType(r.Radius, Double)) = ReferenceRadius And r.BladeId = blade)
    '        Return refBladeMeasurement
    '    End Get
    'End Property

    Private Sub ReferenceBladeChanged(ByVal bladeItem As ToolStripMenuItem)
        Dim bladeNumber As Short = bladeItem.Name
        Dim referenceBlades As ToolStripMenuItem = TryCast(ContextMenuStrip1.Items("ReferenceBladeToolStripMenuItem"), ToolStripMenuItem)

        ' Uncheck all other blade menu items.
        For Each item As ToolStripMenuItem In referenceBlades.DropDownItems
            item.Checked = (item.Name = bladeItem.Name)
        Next
        ReferenceRadiiSet(bladeNumber)
        Me.ReferenceBlade = bladeNumber
        ' Update the chart using the new reference blade.
        DataShow()
    End Sub

    Private Sub ReferenceBladesSet(ByVal bladeCount As Short)
        ' Populate the Reference Blade drop down list with the number of blades.
        Dim referenceBlades As ToolStripMenuItem = DirectCast(ContextMenuStrip1.Items("ReferenceBladeToolStripMenuItem"), ToolStripMenuItem)
        referenceBlades.DropDownItems.Clear()

        For i As Short = 1 To bladeCount
            Dim menuItem As New ToolStripMenuItem(i.ToString()) With {
                .CheckOnClick = True,
                .Name = i.ToString()
            }
            AddHandler menuItem.Click, AddressOf Me.ReferenceBlade_Clicked
            referenceBlades.DropDownItems.Add(menuItem)
        Next
    End Sub

    Private Sub ReferencePointChanged(pointItem As ToolStripMenuItem)
        Dim point As String = pointItem.Text
        ' Uncheck all other reference point menu items.
        Dim referencePoints As ToolStripMenuItem = TryCast(ContextMenuStrip1.Items("ReferencePointToolStripMenuItem"), ToolStripMenuItem)
        For Each item As ToolStripMenuItem In referencePoints.DropDownItems
            item.Checked = (item.Name = pointItem.Name)
        Next
        Me.ReferencePoint = point
        ' Update the chart using the new reference point.
        DataShow()
    End Sub

    Private Sub ReferenceRadiiSet(ByVal bladeNumber As Short)
        ' Populate the eference radius drop down list with the number of radii.
        Dim measurements As List(Of RadiusMeasurement) = DirectCast(Me.Data, JobDetail)?.RadiusMeasurements
        Dim referenceRadii As ToolStripMenuItem = DirectCast(ContextMenuStrip1.Items("ReferenceRadiusToolStripMenuItem"), ToolStripMenuItem)
        referenceRadii.DropDownItems.Clear()

        For Each rm As RadiusMeasurement In measurements
            If rm.BladeId = bladeNumber Then
                Dim menuItem As New ToolStripMenuItem(Math.Round(CType(rm.Radius, Double)).ToString()) With {
                    .CheckOnClick = True,
                    .Name = Math.Round(CType(rm.Radius, Double))
                }
                AddHandler menuItem.Click, AddressOf Me.ReferenceRadius_Clicked
                referenceRadii.DropDownItems.Add(menuItem)
            End If
        Next
    End Sub

    Private Sub ReferenceRadiusChanged(radiusItem As ToolStripMenuItem)
        Dim radius As Double = radiusItem.Name
        ' Uncheck all other reference radius menu items.
        Dim referenceRadii As ToolStripMenuItem = TryCast(ContextMenuStrip1.Items("ReferenceRadiusToolStripMenuItem"), ToolStripMenuItem)
        For Each item As ToolStripMenuItem In referenceRadii.DropDownItems
            item.Checked = (item.Name = radiusItem.Name)
        Next
        Me.ReferenceRadius = radius
        ' Update the chart with the new reference radius.
        DataShow()
    End Sub

    Private ReadOnly Property ReferenceRadiusMeasurement As RadiusMeasurement
        Get
            Return RadiusMeasurements?.FirstOrDefault(Function(r) Math.Round(CType(r.Radius, Double)) = ReferenceRadius And r.BladeId = ReferenceBlade.Value)
        End Get
    End Property
#End Region
#Region "Event Handlers"
    Private Sub ReferenceBlade_Clicked(sender As Object, e As EventArgs)
        ReferenceBladeChanged(DirectCast(sender, ToolStripMenuItem))
    End Sub

    Private Sub ReferenceRadius_Clicked(sender As Object, e As EventArgs)
        ReferenceRadiusChanged(DirectCast(sender, ToolStripMenuItem))
    End Sub

    Private Sub ReferencePointToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LEToolStripMenuItem.Click, MidToolStripMenuItem.Click, TEToolStripMenuItem.Click
        ReferencePointChanged(DirectCast(sender, ToolStripMenuItem))
    End Sub
#End Region
End Class
