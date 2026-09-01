Imports System.Windows.Forms.DataVisualization.Charting
Imports LibDatabase.Models

Public Module Graphing
    Private Const kChartScaleHeightMin As Integer = 300

    Public GraphColorArray As Color() = {Color.Red, Color.Green, Color.Blue, Color.Purple, Color.Yellow, Color.Cyan, ColorTranslator.FromHtml("#c87f00"), ColorTranslator.FromHtml("#c8c880"), ColorTranslator.FromHtml("#c880c8"), ColorTranslator.FromHtml("#80ff00"), Color.Black}

    'Public Sub ChartAutoFit(
    '    ByVal area As ChartArea, ByVal legend As Legend,
    '    Optional ByVal areaFontMin As Integer = -1, Optional ByVal areaFontMax As Integer = -1,
    '    Optional ByVal legendFontMin As Integer = -1, Optional ByVal textWrapThresh As Integer = -1
    ')
    '    ' *** Chart Axes and Legends Auto-Scaling ***
    '    area.AxisX.IsLabelAutoFit = True
    '    area.AxisY.IsLabelAutoFit = True

    '    ' Configure the Auto-fit styles (allow shrinking and wrapping to be disabled).
    '    area.AxisX.LabelAutoFitStyle =
    '        LabelAutoFitStyles.IncreaseFont Or LabelAutoFitStyles.DecreaseFont Or
    '        LabelAutoFitStyles.StaggeredLabels

    '    ' Set limits for auto-sizing.
    '    If areaFontMax <> -1 Then area.AxisX.LabelAutoFitMaxFontSize = areaFontMax
    '    If areaFontMin <> -1 Then area.AxisX.LabelAutoFitMinFontSize = areaFontMin

    '    ' Enable Legend Auto-Fitting.
    '    legend.IsTextAutoFit = True

    '    ' Set the minimum font size for the legend text.
    '    If legendFontMin <> -1 Then legend.AutoFitMinFontSize = legendFontMin

    '    ' To prevent wrapping, adjust the text wrap threshold 
    '    ' (0 effectively prevents wrapping by setting a very high threshold)
    '    If textWrapThresh <> -1 Then legend.TextWrapThreshold = textWrapThresh
    'End Sub

    'Public Sub ChartScale(
    '    ByVal chart As Chart,
    '    ByVal areas As ChartAreaCollection,
    '    ByVal titles As TitleCollection,
    '    ByVal titleFonts As Font(),
    '    ByVal defaultSize As Size,
    '    ByVal zoomFactor As Single,
    '    Optional ByVal xAxisFonts As Font() = Nothing,
    '    Optional ByVal yAxisFonts As Font() = Nothing,
    '    Optional ByVal scaleHeightMin As Integer = kChartScaleHeightMin
    ')
    '    ' This is an overload that takes collections of areas and titles, applying the corresponding fonts in order.
    '    ' TODO: Needs work. It currently assumes the collections are in the correct order and of the same length, which is fragile.
    '    Dim ratioWidth As Single = CSng(chart.Width / defaultSize.Width)
    '    For i As Integer = 0 To titles.Count - 1
    '        Dim titleSize As Single = titleFonts(i).Size * Math.Max(ratioWidth, zoomFactor)
    '        titles(i).Font = New Font(titleFonts(i).FontFamily, titleSize, titleFonts(i).Style)

    '        If chart.Height < scaleHeightMin Then
    '            titles(i).Docking = Docking.Top
    '        End If
    '    Next
    '    If chart.Height < scaleHeightMin Then
    '        For Each area As ChartArea In areas
    '            area.Position.Auto = True
    '        Next
    '    End If

    '    Dim ratioHeight As Single = CSng(chart.Height / defaultSize.Height) * ratioWidth
    '    For i As Integer = 0 To areas.Count - 1
    '        If Not String.IsNullOrEmpty(areas(i).AxisY.Title) AndAlso yAxisFonts.Count > i Then
    '            Dim axisTitleSize As Single = yAxisFonts(i).Size * Math.Max(ratioHeight, zoomFactor)
    '            areas(i).AxisY.TitleFont = New Font(yAxisFonts(i).FontFamily, axisTitleSize, yAxisFonts(i).Style)
    '        End If

    '        If Not String.IsNullOrEmpty(areas(i).AxisX.Title) AndAlso xAxisFonts.Count > i Then
    '            Dim axisTitleSize As Single = xAxisFonts(i).Size * Math.Max(ratioHeight, zoomFactor)
    '            areas(i).AxisX.TitleFont = New Font(xAxisFonts(i).FontFamily, axisTitleSize, xAxisFonts(i).Style)
    '        End If
    '    Next
    'End Sub

    'Public Sub ChartScale(
    '    ByVal chart As Chart,
    '    ByVal area As ChartArea,
    '    ByVal title As Title,
    '    ByVal titleFont As Font,
    '    ByVal defaultSize As Size,
    '    ByVal zoomFactor As Single,
    '    Optional ByVal xAxisFont As Font = Nothing,
    '    Optional ByVal yAxisFont As Font = Nothing,
    '    Optional ByVal scaleHeightMin As Integer = kChartScaleHeightMin
    ')
    '    ' Calculate main scale based on the most constrained dimension (usually Width for titles)
    '    Dim ratioWidth As Single = CSng(chart.Width / defaultSize.Width)

    '    ' Apply the ratio but keep a "Floor" so it stays readable
    '    ' 0.7 is a good floor (e.g., 12pt -> 8.4pt)
    '    Dim mainTitleSize As Single = titleFont.Size * Math.Max(ratioWidth, zoomFactor)

    '    ' *** Apply to title ***.
    '    title.Font = New Font(titleFont.FontFamily, mainTitleSize, titleFont.Style)

    '    ' CRITICAL: For small sizes, disable docking and use position to prevent 
    '    ' the title from pushing the ChartArea into a tiny pancake.
    '    If chart.Height < scaleHeightMin Then
    '        title.Docking = Docking.Top
    '        area.Position.Auto = True
    '    End If

    '    ' *** Then to the axis titles, if any ***.
    '    Dim ratioHeight As Single = CSng(chart.Height / defaultSize.Height) * ratioWidth
    '    If Not String.IsNullOrEmpty(area.AxisY.Title) AndAlso yAxisFont IsNot Nothing Then
    '        Dim axisTitleSize As Single = yAxisFont.Size * Math.Max(ratioHeight, zoomFactor)
    '        area.AxisY.TitleFont = New Font(yAxisFont.FontFamily, axisTitleSize, yAxisFont.Style)
    '    End If
    '    If Not String.IsNullOrEmpty(area.AxisX.Title) AndAlso xAxisFont IsNot Nothing Then
    '        Dim axisTitleSize As Single = xAxisFont.Size * Math.Max(ratioHeight, zoomFactor)
    '        area.AxisX.TitleFont = New Font(xAxisFont.FontFamily, axisTitleSize, xAxisFont.Style)
    '    End If
    'End Sub

    'Public Function ChartCreateSeries(ByVal chart As Chart, ByVal name As String, ByVal xaxis As String, ByVal yaxis As String) As Series
    '    ' Returns a new Series added to the given Chart with the given axis labels.
    '    Dim newSeries As New Series With {
    '        .Name = name,
    '        .ChartType = SeriesChartType.Column,
    '        .XValueMember = xaxis,
    '        .YValueMembers = yaxis,
    '        .IsXValueIndexed = True,
    '        .IsVisibleInLegend = False
    '    }
    '    chart.Series.Clear()
    '    chart.Series.Add(newSeries)
    '    Return newSeries
    'End Function

    Public Sub ChartAddPoint(ByVal chart As Chart, ByVal series As Series, ByVal x As String, ByVal y As Double, isRefBlade As Boolean)
        Dim barColors As Color() = {Color.Red, Color.Blue, Color.Green, Color.Orange, Color.Purple} ' Teletubbies!
        Dim p As Integer = chart.Series(series.Name).Points.AddXY(x, y)
        chart.Series(series.Name).Points(p).Color = If(isRefBlade, Color.Black, barColors(p Mod barColors.Length))
    End Sub

    Public Sub StripLineScale(chart As Chart, axis As Axis, strip As StripLine, ByVal desiredFont As Font, ByVal minFontSize As Single)
        ' 1. Always force recalculation before attempting to read coordinates
        chart.ChartAreas(0).RecalculateAxesScale()

        ' 2. Safely resolve Axis bounds checking for Zoom states vs Unzoomed states
        Dim valMin As Double = If(Double.IsNaN(axis.ScaleView.ViewMinimum), axis.Minimum, axis.ScaleView.ViewMinimum)
        Dim valMax As Double = If(Double.IsNaN(axis.ScaleView.ViewMaximum), axis.Maximum, axis.ScaleView.ViewMaximum)

        ' Exit immediately if the chart has unresolved bounds or lacks data
        If Double.IsNaN(valMin) OrElse Double.IsNaN(valMax) OrElse (valMax = valMin) Then Exit Sub

        ' 3. Translate Data Scale Coordinates into Pixel Area Height
        Dim pixelMax As Double = axis.ValueToPixelPosition(valMax)
        Dim pixelMin As Double = axis.ValueToPixelPosition(valMin)
        Dim chartHeightPixels As Double = Math.Abs(pixelMax - pixelMin)

        ' Scaled constraint boundary (using 5% of total height to bypass the 0.01 width trick)
        Dim stripPixelHeight As Single = CSng(chartHeightPixels * 0.05F)

        ' If the entire chart window is too tiny to read text, preserve performance and exit
        If stripPixelHeight <= 5.0F Then Exit Sub

        ' 4. Dynamically evaluate the font bounding box against the strip height
        Using g As Graphics = chart.CreateGraphics()
            Dim sampleText As String = strip.Text
            Dim fontSize As Single = desiredFont.Size

            ' CRITICAL: Guarantee the absolute floor is never <= 0 to prevent ArgumentException
            minFontSize = Math.Max(minFontSize, 1.0F)

            Dim currentFont As New Font(desiredFont.FontFamily, fontSize, desiredFont.Style)
            Dim textSize As SizeF = g.MeasureString(sampleText, currentFont)

            ' LOOK-AHEAD FIX: Check the NEXT size step before constructing the Font object
            While textSize.Height > stripPixelHeight AndAlso (fontSize - 0.5F) >= minFontSize
                fontSize -= 0.5F
                currentFont.Dispose() ' Safely release old resource before allocating new one
                currentFont = New Font(desiredFont.FontFamily, fontSize, desiredFont.Style)
                textSize = g.MeasureString(sampleText, currentFont)
            End While

            ' 5. Safely apply the final processed font object
            strip.Font = currentFont
        End Using
    End Sub

    Public Function SeriesBladeHeight(ByVal rm As List(Of RadiusMeasurement), ByVal bladeCount As Integer, ByVal refBlade As Integer, ByVal refPoint As String, ByVal refRadius As String) As Series
        Const kHeightOffset As Double = 0.2 ' Adjust as needed to set zero height
        Dim s As New Series With {
            .ChartType = SeriesChartType.Column,
            .IsXValueIndexed = True,
            .IsVisibleInLegend = False
        }
        Dim innerRm As RadiusMeasurement = rm?.FirstOrDefault()
        Dim innerDepth As Double = TrackGetDepth(innerRm, refPoint)
        Dim outerRm As RadiusMeasurement = rm?.LastOrDefault()
        Dim outerDepth As Double = TrackGetDepth(outerRm, refPoint)
        Dim refRm As RadiusMeasurement = rm?.FirstOrDefault(Function(r) Math.Round(CType(r.Radius, Double)) = refRadius)
        Dim refDepth As Double = TrackGetDepth(refRm, refPoint)
        Dim refAngle As Double = TrackGetAngle(refRm, refPoint)
        For i As Integer = 1 To bladeCount
            Dim b As Integer = i
            Dim bladeRadius As RadiusMeasurement = rm?.FirstOrDefault(Function(r) r.BladeId = b)
            Dim bladeDepth As Double = TrackGetDepth(bladeRadius, refPoint)
            Dim bladeHeight As Double = Math.Abs(refDepth - bladeDepth) + kHeightOffset
            Dim p As Integer = s.Points.AddXY($"{b}", bladeHeight)
        Next
        Return s
    End Function

    Public Function TrackGetAngle(ByVal rm As RadiusMeasurement, ByVal point As String) As Double
        ' Returns the Angle CellMeasurement for the given RadiusMeasurement at the given point (LE, Mid or TE).
        Dim angle As Double = 0.0
        If rm IsNot Nothing AndAlso Not String.IsNullOrEmpty(point) Then
            Select Case point
                Case "LE"
                    angle = rm.CellMeasurements.FirstOrDefault()?.Angle
                Case "Mid"
                    angle = rm.CellMeasurements.ElementAt(rm.CellMeasurements.Count \ 2)?.Angle
                Case "TE"
                    angle = rm.CellMeasurements.LastOrDefault()?.Angle
                Case Else
            End Select
        End If
        Return angle
    End Function

    Public Function TrackGetDepth(ByVal rm As RadiusMeasurement, ByVal point As String) As Double
        ' Returns the Depth CellMeasurement for the given RadiusMeasurement at the given point (LE, Mid or TE).
        Dim depth As Double = 0.0
        If rm IsNot Nothing AndAlso Not String.IsNullOrEmpty(point) Then
            Select Case point
                Case "LE"
                    depth = rm.CellMeasurements.FirstOrDefault()?.Depth
                Case "Mid"
                    depth = rm.CellMeasurements.ElementAt(rm.CellMeasurements.Count \ 2)?.Depth
                Case "TE"
                    depth = rm.CellMeasurements.LastOrDefault()?.Depth
                Case Else
            End Select
        End If
        Return depth
    End Function
End Module
Public Class ProgRadiusMeasurement
    Public Sub New()
        Rads = New List(Of RadiusMeasurement)()
    End Sub
    Public Sub New(rad As List(Of RadiusMeasurement), np As Double, op As Double)
        Rads = rad
        NewPitch = np
        OldPitch = op
    End Sub
    Public Rads As List(Of RadiusMeasurement) 'think i need to change this to list of heights
    Public NewPitch As Double
    Public OldPitch As Double
End Class
