Imports System.ComponentModel
Imports System.IO
Imports System.Numerics
Imports System.Windows.Forms.DataVisualization.Charting
Imports Hale_MRI_Reporting.ComboChartAnnotationTypePicker
Imports LibGlobals.Strings
Imports LibGlobals
Imports Microsoft.VisualBasic.FileIO

Public Class FrmChartDesigner
#Region "Types and Constants"
    Private Class SeriesData
        Public XData() As Object
        Public YData() As Object

        Public Sub New()
            XData = Array.Empty(Of Object)()
            YData = Array.Empty(Of Object)()
        End Sub

        Public Sub New(ByVal xData() As Object, ByVal yData() As Object)
            Me.XData = xData
            Me.YData = yData
        End Sub
    End Class
#End Region
#Region "Private Members"
    Private WithEvents mAnnotations As New BindingList(Of String)()
    Private WithEvents mAxes As New BindingList(Of String)()
    Private WithEvents mChartAreas As New BindingList(Of String)()
    Private WithEvents mCharts As New BindingList(Of String)()
    Private WithEvents mChartList As New List(Of Chart)()
    Private WithEvents mLegends As New BindingList(Of String)()
    Private WithEvents mSeries As New BindingList(Of String)()
    Private WithEvents mTitles As New BindingList(Of String)()
    Private mOriginalCharts As New Dictionary(Of Chart, MemoryStream)()
    Private mSelectedAnnotation As Annotation = Nothing
    Private mSelectedAxis As Axis = Nothing
    Private mSelectedChart As Chart = Nothing
    Private mSelectedChartArea As ChartArea = Nothing
    Private mSelectedLegend As Legend = Nothing
    Private mSelectedSeries As Series = Nothing
    Private mSelectedTitle As Title = Nothing
    Private mUserInput As Boolean = False
#End Region
#Region "Public Interface"
    Public Property Charts As List(Of Chart)
        Get
            Return mChartList
        End Get
        Set(value As List(Of Chart))
            mChartList = value
            ChartsSaveOriginal(value)
            ChartsUpdateFromList(value)
        End Set
    End Property

    Public Property SelectedAnnotation As Annotation
        Get
            Return mSelectedAnnotation
        End Get
        Set(value As Annotation)
            mSelectedAnnotation = value
            AnnotationSelect(value)
        End Set
    End Property

    Public Property SelectedAxis As Axis
        Get
            Return mSelectedAxis
        End Get
        Set(value As Axis)
            mSelectedAxis = value
            AxisSelect(value)
        End Set
    End Property

    Public Property SelectedChart As Chart
        Get
            Return mSelectedChart
        End Get
        Set(value As Chart)
            mSelectedChart = value
            ChartSelect(value)
        End Set
    End Property

    Public Property SelectedChartArea As ChartArea
        Get
            Return mSelectedChartArea
        End Get
        Set(value As ChartArea)
            mSelectedChartArea = value
            ChartAreaSelect(value)
        End Set
    End Property

    Public Property SelectedLegend As Legend
        Get
            Return mSelectedLegend
        End Get
        Set(value As Legend)
            mSelectedLegend = value
            LegendSelect(value)
        End Set
    End Property

    Public Property SelectedSeries As Series
        Get
            Return mSelectedSeries
        End Get
        Set(value As Series)
            mSelectedSeries = value
            SeriesSelect(value)
        End Set
    End Property

    Public Property SelectedTitle As Title
        Get
            Return mSelectedTitle
        End Get
        Set(value As Title)
            mSelectedTitle = value
            TitleSelect(value)
        End Set
    End Property
#End Region
#Region "Private Interface"
    Private Function AnnotationsAddNew(ByVal annotationType As ChartAnnotationType, ByVal name As String, Optional ByVal chart As Chart = Nothing) As Annotation
        Dim newAnnotation As Annotation = Nothing
        Select Case annotationType
            Case ChartAnnotationType.Arrow
                newAnnotation = New ArrowAnnotation() With {.Name = name}
            Case ChartAnnotationType.VerticalLine
                newAnnotation = New VerticalLineAnnotation() With {.Name = name}
            Case ChartAnnotationType.Line
                newAnnotation = New LineAnnotation() With {.Name = name}
            Case ChartAnnotationType.HorizontalLine
                newAnnotation = New HorizontalLineAnnotation() With {.Name = name}
            Case ChartAnnotationType.Polyline
                newAnnotation = New PolylineAnnotation() With {.Name = name}
            Case ChartAnnotationType.Polygon
                newAnnotation = New PolygonAnnotation() With {.Name = name}
            Case ChartAnnotationType.Callout
                newAnnotation = New CalloutAnnotation() With {.Name = name}
            Case ChartAnnotationType.Ellipse
                newAnnotation = New EllipseAnnotation() With {.Name = name}
            Case ChartAnnotationType.Rectangle
                newAnnotation = New RectangleAnnotation() With {.Name = name}
            Case ChartAnnotationType.Text
                newAnnotation = New TextAnnotation() With {.Name = name}
            Case Else
        End Select

        If chart IsNot Nothing Then chart.Annotations.Add(newAnnotation)

        Return newAnnotation
    End Function

    Private Sub AnnotationPropertiesHide()
        ComboAnnotationsList.SelectedIndex = -1
        ComboAnnotationsAnnotationType.SelectedIndex = -1
        ComboAnnotationsAlignment.SelectedIndex = -1
        ComboAnnotationsBackColor.SelectedIndex = -1
        ComboAnnotationsBackGradientStyle.SelectedIndex = -1
        ComboAnnotationsBackHatchStyle.SelectedIndex = -1
        ComboAnnotationsForeColor.SelectedIndex = -1
        NumericAnnotationsHeight.Value = 0
        ComboAnnotationsLineColor.SelectedIndex = -1
        ComboAnnotationsLineDashStyle.SelectedIndex = -1
        NumericAnnotationsLineWidth.Value = 0
        TxtAnnotationsRight.Text = ""
        ComboAnnotationsShadowColor.SelectedIndex = -1
        NumericAnnotationsShadowOffset.Value = 0
        TxtAnnotationsX.Text = ""
        TxtAnnotationsY.Text = ""

        ComboAnnotationsList.Enabled = False
        'Do not disable ComboAnnotationsAnnotationType so the user can select and add new annotations. 
        ComboAnnotationsAlignment.Enabled = False
        ComboAnnotationsBackColor.Enabled = False
        ComboAnnotationsBackGradientStyle.Enabled = False
        ComboAnnotationsBackHatchStyle.Enabled = False
        CmdAnnotationsFont.Enabled = False
        ComboAnnotationsForeColor.Enabled = False
        NumericAnnotationsHeight.Enabled = False
        ComboAnnotationsLineColor.Enabled = False
        ComboAnnotationsLineDashStyle.Enabled = False
        NumericAnnotationsLineWidth.Enabled = False
        TxtAnnotationsRight.Enabled = False
        CmdAnnotationsRemove.Enabled = False
        ComboAnnotationsShadowColor.Enabled = False
        NumericAnnotationsShadowOffset.Enabled = False
        TxtAnnotationsX.Enabled = False
        TxtAnnotationsY.Enabled = False
    End Sub

    Private Sub AnnotationPropertiesShow(ByVal annotation As Annotation)
        ComboAnnotationsList.Enabled = True
        ComboAnnotationsAnnotationType.Enabled = True
        ComboAnnotationsAlignment.Enabled = True
        ComboAnnotationsBackColor.Enabled = True
        ComboAnnotationsBackGradientStyle.Enabled = True
        ComboAnnotationsBackHatchStyle.Enabled = True
        CmdAnnotationsFont.Enabled = True
        ComboAnnotationsForeColor.Enabled = True
        NumericAnnotationsHeight.Enabled = True
        ComboAnnotationsLineColor.Enabled = True
        ComboAnnotationsLineDashStyle.Enabled = True
        NumericAnnotationsLineWidth.Enabled = True
        TxtAnnotationsRight.Enabled = True
        CmdAnnotationsRemove.Enabled = True
        ComboAnnotationsShadowColor.Enabled = True
        NumericAnnotationsShadowOffset.Enabled = True
        TxtAnnotationsX.Enabled = True
        TxtAnnotationsY.Enabled = True

        ComboAnnotationsList.SelectedItem = annotation.Name
        ComboAnnotationsAnnotationType.AnnotationType = AnnotationTypeGet(annotation)
        ComboAnnotationsAlignment.Alignment = annotation.Alignment
        ComboAnnotationsBackColor.Color = annotation.BackColor
        ComboAnnotationsBackGradientStyle.GradientStyle = annotation.BackGradientStyle
        ComboAnnotationsBackHatchStyle.HatchStyle = annotation.BackHatchStyle
        ComboAnnotationsForeColor.Color = annotation.ForeColor
        NumericAnnotationsHeight.Value = If(Not Double.IsNaN(annotation.Height), annotation.Height, 0)
        ComboAnnotationsLineColor.Color = annotation.LineColor
        ComboAnnotationsLineDashStyle.DashStyle = annotation.LineDashStyle
        NumericAnnotationsLineWidth.Value = annotation.LineWidth
        TxtAnnotationsRight.Text = If(Not Double.IsNaN(annotation.Right), annotation.Right, 0).ToString()
        ComboAnnotationsShadowColor.Color = annotation.ShadowColor
        NumericAnnotationsShadowOffset.Value = annotation.ShadowOffset
        TxtAnnotationsX.Text = If(Not Double.IsNaN(annotation.X), annotation.X, 0).ToString()
        TxtAnnotationsY.Text = If(Not Double.IsNaN(annotation.Y), annotation.Y, 0).ToString()
    End Sub

    Private Sub AnnotationSelect(ByVal annotation As Annotation)
        If annotation IsNot Nothing Then
            AnnotationPropertiesShow(annotation)
        Else
            AnnotationPropertiesHide()
        End If
    End Sub

    Public Function AnnotationTypeGet(ann As Annotation) As ChartAnnotationType
        If ann Is Nothing Then Return Nothing 'ChartAnnotationType.Unknown

        Select Case ann.GetType()
            Case GetType(TextAnnotation)
                Return ChartAnnotationType.Text
            Case GetType(LineAnnotation)
                Return ChartAnnotationType.Line
            Case GetType(RectangleAnnotation)
                Return ChartAnnotationType.Rectangle
            Case GetType(EllipseAnnotation)
                Return ChartAnnotationType.Ellipse
            Case GetType(ArrowAnnotation)
                Return ChartAnnotationType.Arrow
            Case GetType(CalloutAnnotation)
                Return ChartAnnotationType.Callout
            Case GetType(ImageAnnotation)
                Return ChartAnnotationType.Image
            Case GetType(PolylineAnnotation)
                Return ChartAnnotationType.Polyline
            Case GetType(PolygonAnnotation)
                Return ChartAnnotationType.Polygon
            Case Else
                Return Nothing 'ChartAnnotationType.Unknown
        End Select
    End Function

    Private Sub AnnotationsUpdateFromChart(ByVal chart As Chart)
        mAnnotations.RaiseListChangedEvents = False  ' Pause events during bulk update.
        mAnnotations.Clear()
        If chart IsNot Nothing Then
            For Each annotation As Annotation In chart.Annotations
                mAnnotations.Add(annotation.Name)
            Next
        End If
        mAnnotations.RaiseListChangedEvents = True   ' Resume events.
        mAnnotations.ResetBindings()                 ' Trigger a single "Reset" event.
    End Sub

    Private Sub AxisPropertiesHide()
        TxtAxesChartArea.Text = ""
        ComboAxesAxis.SelectedIndex = -1
        TxtAxesScalingAndMathMinimum.Text = ""
        TxtAxesScalingAndMathMaximum.Text = ""
        TxtAxesScalingAndMathInterval.Text = ""
        TxtAxesScalingAndMathCrossing.Text = ""
        ChkAxesScalingAndMathIsLogarithmic.Checked = False
        ChkAxesScalingAndMathIsReversed.Checked = False
        ChkAxesMajorGridEnabled.Checked = False
        ComboAxesMajorGridDashStyle.SelectedIndex = -1
        ComboAxesMajorGridLineColor.SelectedIndex = -1
        ComboAxesMajorTickMarkStyle.SelectedIndex = -1
        ChkAxesMinorGridEnabled.Checked = False
        ComboAxesMinorGridDashStyle.SelectedIndex = -1
        ComboAxesMinorGridLineColor.SelectedIndex = -1
        ComboAxesMinorTickMarkStyle.SelectedIndex = -1
        ComboAxesAxisLineColor.SelectedIndex = -1
        ComboAxesAxisLineDashStyle.SelectedIndex = -1
        NumericAxesAxisLineWidth.Value = 0
        NumericAxesAutoFitMaxFontSize.Value = 0
        NumericAxesAutoFitMinFontSize.Value = 0
        TxtAxesLabelsAngle.Text = ""
        ComboAxesLabelsForeColor.SelectedIndex = -1
        TxtAxesLabelsFormat.Text = ""
        ComboAxesTitleAlignment.SelectedIndex = -1
        ComboAxesTitleForeColor.SelectedIndex = -1
        TxtAxesTitleText.Text = ""
        ComboAxesTitleTextOrientation.SelectedIndex = -1
        ChkAxesInterlacingIsInterlaced.Checked = False

        TxtAxesChartArea.Enabled = False
        ComboAxesAxis.Enabled = False
        TxtAxesScalingAndMathMinimum.Enabled = False
        TxtAxesScalingAndMathMaximum.Enabled = False
        TxtAxesScalingAndMathInterval.Enabled = False
        TxtAxesScalingAndMathCrossing.Enabled = False
        ChkAxesScalingAndMathIsLogarithmic.Enabled = False
        ChkAxesScalingAndMathIsReversed.Enabled = False
        ChkAxesMajorGridEnabled.Enabled = False
        ComboAxesMajorGridDashStyle.Enabled = False
        ComboAxesMajorGridLineColor.Enabled = False
        ComboAxesMajorTickMarkStyle.Enabled = False
        ChkAxesMinorGridEnabled.Enabled = False
        ComboAxesMinorGridDashStyle.Enabled = False
        ComboAxesMinorGridLineColor.Enabled = False
        ComboAxesMinorTickMarkStyle.Enabled = False
        ComboAxesAxisLineColor.Enabled = False
        ComboAxesAxisLineDashStyle.Enabled = False
        NumericAxesAxisLineWidth.Enabled = False
        NumericAxesAutoFitMaxFontSize.Enabled = False
        NumericAxesAutoFitMinFontSize.Enabled = False
        CmdAxesLabelsFont.Enabled = False
        TxtAxesLabelsAngle.Enabled = False
        ComboAxesLabelsForeColor.Enabled = False
        TxtAxesLabelsFormat.Enabled = False
        ComboAxesTitleAlignment.Enabled = False
        ComboAxesTitleForeColor.Enabled = False
        TxtAxesTitleText.Enabled = False
        ComboAxesTitleTextOrientation.Enabled = False
        ChkAxesInterlacingIsInterlaced.Enabled = False
    End Sub

    Private Sub AxisPropertiesShow(ByVal axis As Axis)
        TxtAxesChartAreaAxis.Enabled = True
        ComboAxesAxis.Enabled = True
        TxtAxesScalingAndMathMinimum.Enabled = True
        TxtAxesScalingAndMathMaximum.Enabled = True
        TxtAxesScalingAndMathInterval.Enabled = True
        TxtAxesScalingAndMathCrossing.Enabled = True
        ChkAxesScalingAndMathIsLogarithmic.Enabled = True
        ChkAxesScalingAndMathIsReversed.Enabled = True
        ChkAxesMajorGridEnabled.Enabled = True
        ComboAxesMajorGridDashStyle.Enabled = True
        ComboAxesMajorGridLineColor.Enabled = True
        ComboAxesMajorTickMarkStyle.Enabled = True
        ChkAxesMinorGridEnabled.Enabled = True
        ComboAxesMinorGridDashStyle.Enabled = True
        ComboAxesMinorGridLineColor.Enabled = True
        ComboAxesMinorTickMarkStyle.Enabled = True
        ComboAxesAxisLineColor.Enabled = True
        ComboAxesAxisLineDashStyle.Enabled = True
        NumericAxesAxisLineWidth.Enabled = True
        NumericAxesAutoFitMaxFontSize.Enabled = True
        NumericAxesAutoFitMinFontSize.Enabled = True
        CmdAxesLabelsFont.Enabled = True
        TxtAxesLabelsAngle.Enabled = True
        ComboAxesLabelsForeColor.Enabled = True
        TxtAxesLabelsFormat.Enabled = True
        ComboAxesAxisLineColor.Enabled = True
        ComboAxesTitleAlignment.Enabled = True
        ComboAxesTitleForeColor.Enabled = True
        TxtAxesTitleText.Enabled = True
        ComboAxesTitleTextOrientation.Enabled = True
        ChkAxesInterlacingIsInterlaced.Enabled = True

        TxtAxesChartAreaAxis.Text = axis.Name
        ComboAxesAxis.SelectedItem = axis.Name
        TxtAxesScalingAndMathMinimum.Text = axis.Maximum.ToString()
        TxtAxesScalingAndMathMaximum.Text = axis.Minimum.ToString()
        TxtAxesScalingAndMathInterval.Text = axis.Interval.ToString()
        TxtAxesScalingAndMathCrossing.Text = axis.Crossing.ToString()
        ChkAxesScalingAndMathIsLogarithmic.Checked = axis.IsLogarithmic
        ChkAxesScalingAndMathIsReversed.Checked = axis.IsReversed
        ChkAxesMajorGridEnabled.Checked = axis.MajorGrid.Enabled
        ComboAxesMajorGridDashStyle.DashStyle = axis.MajorGrid.LineDashStyle
        ComboAxesMajorGridLineColor.Color = axis.MajorGrid.LineColor
        ComboAxesMajorTickMarkStyle.TickMarkStyle = axis.MajorTickMark.LineDashStyle
        ChkAxesMinorGridEnabled.Checked = axis.MinorGrid.Enabled
        ComboAxesMinorGridDashStyle.DashStyle = axis.MinorGrid.LineDashStyle
        ComboAxesMinorGridLineColor.Color = axis.MinorGrid.LineColor
        ComboAxesMinorTickMarkStyle.TickMarkStyle = axis.MinorTickMark.LineDashStyle
        ComboAxesAxisLineColor.Color = axis.LineColor
        ComboAxesAxisLineDashStyle.DashStyle = axis.LineDashStyle
        NumericAxesAxisLineWidth.Value = axis.LineWidth
        NumericAxesAutoFitMaxFontSize.Value = axis.LabelAutoFitMaxFontSize
        NumericAxesAutoFitMinFontSize.Value = axis.LabelAutoFitMinFontSize
        TxtAxesLabelsAngle.Text = axis.LabelStyle.Angle.ToString()
        ComboAxesLabelsForeColor.Color = axis.LabelStyle.ForeColor
        TxtAxesLabelsFormat.Text = axis.LabelStyle.Format
        ComboAxesAxisLineColor.Color = axis.LineColor
        ComboAxesTitleAlignment.Alignment = axis.TitleAlignment
        ComboAxesTitleForeColor.Color = axis.TitleForeColor
        TxtAxesTitleText.Text = axis.Title
        ComboAxesTitleTextOrientation.OrientationName = axis.TextOrientation
        ChkAxesInterlacingIsInterlaced.Checked = axis.IsInterlaced
    End Sub

    Private Sub AxisSelect(ByVal axis As Axis)
        If axis IsNot Nothing Then
            AxisPropertiesShow(axis)
        Else
            AxisPropertiesHide()
        End If
    End Sub

    Private Sub AxesUpdateFromChartArea(ByVal chartArea As ChartArea)
        mAxes.RaiseListChangedEvents = False  ' Pause events during bulk update.
        mAxes.Clear()
        If chartArea IsNot Nothing Then
            For Each axis As Axis In chartArea.Axes
                mAxes.Add(axis.Name)
            Next
        End If
        mAxes.RaiseListChangedEvents = True   ' Resume events.
        mAxes.ResetBindings()                 ' Trigger a single "Reset" event.
    End Sub

    Private Sub ChartAreaPropertiesHide()
        ComboChartAreaAlignmentOrientation.SelectedIndex = -1
        ComboChartAreaAlignmentStyles.SelectedIndex = -1
        ComboChartArea3dStyle.SelectedIndex = -1
        ComboChartAreaBackColor.SelectedIndex = -1
        ComboChartAreaBackGradientStyle.SelectedIndex = -1
        ComboChartAreaBackHatchStyle.SelectedIndex = -1
        TxtChartAreaBackImage.Text = ""
        ComboChartAreaBorderColor.SelectedIndex = -1
        ComboChartAreaBorderDashStyle.SelectedIndex = -1
        NumericChartAreaBorderWidth.Value = 0
        ComboChartAreaShadowColor.SelectedIndex = -1
        NumericChartAreaShadowOffset.Value = 0

        CmdChartAreaBackImage.Enabled = False
        CmdChartAreasRemove.Enabled = False
        ComboChartAreaAlignmentOrientation.Enabled = False
        ComboChartAreaAlignmentStyles.Enabled = False
        ComboChartArea3dStyle.Enabled = False
        ComboChartAreaBackColor.Enabled = False
        ComboChartAreaBackGradientStyle.Enabled = False
        ComboChartAreaBackHatchStyle.Enabled = False
        TxtChartAreaBackImage.Enabled = False
        ComboChartAreaBorderColor.Enabled = False
        ComboChartAreaBorderDashStyle.Enabled = False
        NumericChartAreaBorderWidth.Enabled = False
        ComboChartAreaShadowColor.Enabled = False
        NumericChartAreaShadowOffset.Enabled = False

        AxisPropertiesHide()
    End Sub

    Private Sub ChartAreaPropertiesShow(ByVal chartArea As ChartArea)
        CmdChartAreaBackImage.Enabled = True
        CmdChartAreasRemove.Enabled = True
        ComboChartAreaAlignmentOrientation.Enabled = True
        ComboChartAreaAlignmentStyles.Enabled = True
        ComboChartArea3dStyle.Enabled = True
        ComboChartAreaBackColor.Enabled = True
        ComboChartAreaBackGradientStyle.Enabled = True
        ComboChartAreaBackHatchStyle.Enabled = True
        TxtChartAreaBackImage.Enabled = True
        ComboChartAreaBorderColor.Enabled = True
        ComboChartAreaBorderDashStyle.Enabled = True
        NumericChartAreaBorderWidth.Enabled = True
        ComboChartAreaShadowColor.Enabled = True
        NumericChartAreaShadowOffset.Enabled = True

        ComboChartAreaAlignmentOrientation.Orientation = chartArea.AlignmentOrientation
        ComboChartAreaAlignmentStyles.AlignmentStyle = chartArea.AlignmentStyle
        ComboChartArea3dStyle.Style = chartArea.Area3DStyle
        ComboChartAreaBackColor.Color = chartArea.BackColor
        ComboChartAreaBackGradientStyle.GradientStyle = chartArea.BackGradientStyle
        ComboChartAreaBackHatchStyle.HatchStyle = chartArea.BackHatchStyle
        TxtChartAreaBackImage.Text = chartArea.BackImage
        ComboChartAreaBorderColor.Color = chartArea.BorderColor
        ComboChartAreaBorderDashStyle.DashStyle = chartArea.BorderDashStyle
        NumericChartAreaBorderWidth.Value = chartArea.BorderWidth
        ComboChartAreaShadowColor.Color = chartArea.ShadowColor
        NumericChartAreaShadowOffset.Value = chartArea.ShadowOffset

        TxtAxesChartArea.Enabled = True
        TxtAxesChartArea.Text = chartArea.Name
        If SelectedAxis IsNot Nothing Then AxisPropertiesShow(SelectedAxis)
    End Sub

    Private Sub ChartAreaSelect(ByVal chartArea As ChartArea)
        If chartArea IsNot Nothing Then
            AxesUpdateFromChartArea(chartArea)
            ChartAreaPropertiesShow(chartArea)
        Else
            ChartAreaPropertiesHide()
        End If
    End Sub

    Private Sub ChartAreasUpdateFromChart(ByVal chart As Chart)
        mChartAreas.RaiseListChangedEvents = False  ' Pause events during bulk update.
        mChartAreas.Clear()
        If chart IsNot Nothing Then
            For Each chartArea As ChartArea In chart.ChartAreas
                mChartAreas.Add(chartArea.Name)
            Next
        End If
        mChartAreas.RaiseListChangedEvents = True   ' Resume events.
        mChartAreas.ResetBindings()                 ' Trigger a single "Reset" event.
    End Sub

    Private Sub ChartPropertiesHide()
        ComboChartAntiAliasing.SelectedIndex = -1
        ComboChartTextAntiAliasingQuality.SelectedIndex = -1
        ComboChartBackColor.SelectedIndex = -1
        ComboChartBackGradientStyle.SelectedIndex = -1
        TxtChartBackImage.Text = ""
        ComboChartBackImageTransparentColor.SelectedIndex = -1
        ComboChartBackImageAlignmentStyle.SelectedIndex = -1
        ComboChartBorderColor.SelectedIndex = -1
        ComboChartBorderDashStyle.SelectedIndex = -1
        ComboChartBorderlineColor.SelectedIndex = -1
        ComboChartBorderlineDashStyle.SelectedIndex = -1
        NumericChartBorderlineWidth.Value = 0
        ComboChartBorderSkinStyle.SelectedIndex = -1
        ComboChartBorderSkinBackColor.SelectedIndex = -1
        ComboChartBorderSkinBackSecondaryColor.SelectedIndex = -1
        ComboChartBorderSkinPageColor.SelectedIndex = -1
        ComboChartBorderSkinBorderColor.SelectedIndex = -1
        TxtChartFontHeight.Text = ""
        ComboChartForeColor.SelectedIndex = -1
        ComboChartColorPalette.SelectedIndex = -1
        TxtChartName.Text = ""
        ChkChartIsSoftShadows.Checked = False
        ComboChartColorPalette.SelectedIndex = -1
        ColorStripChartPaletteCustomColors.Colors = Nothing
        TxtChartText.Text = ""

        ComboChartAntiAliasing.Enabled = False
        ComboChartTextAntiAliasingQuality.Enabled = False
        ComboChartBackColor.Enabled = False
        ComboChartBackGradientStyle.Enabled = False
        TxtChartBackImage.Enabled = False
        CmdChartBackImage.Enabled = False
        ComboChartBackImageTransparentColor.Enabled = False
        ComboChartBackImageAlignmentStyle.Enabled = False
        ComboChartBorderColor.Enabled = False
        ComboChartBorderDashStyle.Enabled = False
        ComboChartBorderlineColor.Enabled = False
        ComboChartBorderlineDashStyle.Enabled = False
        NumericChartBorderlineWidth.Enabled = False
        NumericChartBorderlineWidth.Value = 0
        ComboChartBorderSkinStyle.Enabled = False
        ComboChartBorderSkinBackColor.Enabled = False
        ComboChartBorderSkinBackSecondaryColor.Enabled = False
        ComboChartBorderSkinPageColor.Enabled = False
        ComboChartBorderSkinBorderColor.Enabled = False
        CmdChartFont.Enabled = False
        TxtChartFontHeight.Enabled = False
        ComboChartForeColor.Enabled = False
        TxtChartName.Enabled = False
        ChkChartIsSoftShadows.Enabled = False
        ComboChartColorPalette.Enabled = False
        ColorStripChartPaletteCustomColors.Enabled = False
        TxtChartText.Enabled = False
    End Sub

    Private Sub ChartPropertiesShow(ByVal chart As Chart)
        ComboChartAntiAliasing.Enabled = True
        ComboChartTextAntiAliasingQuality.Enabled = True
        ComboChartBackColor.Enabled = True
        ComboChartBackGradientStyle.Enabled = True
        TxtChartBackImage.Enabled = True
        CmdChartBackImage.Enabled = True
        ComboChartBackImageTransparentColor.Enabled = True
        ComboChartBackImageAlignmentStyle.Enabled = True
        ComboChartBorderColor.Enabled = True
        ComboChartBorderDashStyle.Enabled = True
        ComboChartBorderlineColor.Enabled = True
        ComboChartBorderlineDashStyle.Enabled = True
        NumericChartBorderlineWidth.Enabled = True
        NumericChartBorderlineWidth.Value = 0
        ComboChartBorderSkinStyle.Enabled = True
        ComboChartBorderSkinBackColor.Enabled = True
        ComboChartBorderSkinBackSecondaryColor.Enabled = True
        ComboChartBorderSkinPageColor.Enabled = True
        ComboChartBorderSkinBorderColor.Enabled = True
        CmdChartFont.Enabled = True
        TxtChartFontHeight.Enabled = True
        ComboChartForeColor.Enabled = True
        TxtChartName.Enabled = True
        ChkChartIsSoftShadows.Enabled = True
        ComboChartColorPalette.Enabled = True
        ColorStripChartPaletteCustomColors.Enabled = True
        TxtChartText.Enabled = True

        ComboChartAntiAliasing.AntiAliasingStyle = chart.AntiAliasing
        ComboChartTextAntiAliasingQuality.AntiAliasingQuality = chart.TextAntiAliasingQuality
        ComboChartBackColor.Color = chart.BackColor
        ComboChartBackGradientStyle.GradientStyle = chart.BackGradientStyle
        TxtChartBackImage.Text = chart.BackImage
        ComboChartBackImageTransparentColor.Color = chart.BackImageTransparentColor
        ComboChartBackImageAlignmentStyle.AlignmentStyle = chart.BackImageAlignment
        ComboChartBorderColor.Color = chart.BorderColor
        ComboChartBorderDashStyle.DashStyle = chart.BorderlineDashStyle
        ComboChartBorderlineColor.Color = chart.BorderlineColor
        ComboChartBorderlineDashStyle.DashStyle = chart.BorderlineDashStyle
        NumericChartBorderlineWidth.Value = chart.BorderlineWidth
        ComboChartBorderSkinBackColor.Color = chart.BorderSkin.BackColor
        ComboChartBorderSkinBorderColor.Color = chart.BorderSkin.BorderColor
        ComboChartBorderSkinBackSecondaryColor.Color = chart.BorderSkin.BackSecondaryColor
        ComboChartBorderSkinPageColor.Color = chart.BorderSkin.PageColor
        ComboChartBorderSkinStyle.SkinStyle = chart.BorderSkin.SkinStyle
        TxtChartFontHeight.Text = chart.Font.Height.ToString()
        ComboChartForeColor.Color = chart.ForeColor
        ChkChartIsSoftShadows.Checked = chart.IsSoftShadows
        ComboChartColorPalette.ColorPalette = chart.Palette
        ColorStripChartPaletteCustomColors.Colors = chart.PaletteCustomColors
        TxtChartText.Text = chart.Text
    End Sub

    Private Sub ChartsRestoreOriginal()
        For Each kvp As KeyValuePair(Of Chart, MemoryStream) In mOriginalCharts
            Dim targetChart As Chart = kvp.Key
            Dim backupStream As MemoryStream = kvp.Value

            ' Reset stream position to the beginning
            backupStream.Seek(0, SeekOrigin.Begin)

            ' FIX: Use the Serializer property to load the chart state back
            targetChart.Serializer.Load(backupStream)
        Next
    End Sub

    Private Sub ChartsSaveOriginal(ByVal charts As List(Of Chart))
        For Each ms In mOriginalCharts.Values
            ms.Dispose()
        Next
        mOriginalCharts.Clear()

        For Each originalChart As Chart In charts
            Dim ms As New MemoryStream()

            ' FIX: Use the Serializer property to save the chart state
            originalChart.Serializer.Save(ms)

            ' Add to dictionary for easy retrieval later
            mOriginalCharts.Add(originalChart, ms)
        Next
    End Sub

    Private Sub ChartSelect(ByVal chart As Chart)
        If chart IsNot Nothing AndAlso Me.Charts IsNot Nothing Then
            TitlesUpdateFromChart(chart)
            ChartAreasUpdateFromChart(chart)
            SeriesUpdateFromChart(chart)
            LegendsUpdateFromChart(chart)
            AnnotationsUpdateFromChart(chart)
            ChartPropertiesShow(chart)
            SelectedTitle = If(Me.ComboTitlesList.SelectedIndex <> -1, chart.Titles(Me.ComboTitlesList.SelectedIndex), Nothing)
            SelectedChartArea = If(Me.ComboChartAreasList.SelectedIndex <> -1, chart.ChartAreas(Me.ComboChartAreasList.SelectedIndex), Nothing)
            SelectedSeries = If(Me.ComboSeriesList.SelectedIndex <> -1, chart.Series(Me.ComboSeriesList.SelectedIndex), Nothing)
            SelectedLegend = If(Me.ComboLegendsList.SelectedIndex <> -1, chart.Legends(Me.ComboChartAreasList.SelectedIndex), Nothing)
            SelectedAnnotation = If(Me.ComboAnnotationsList.SelectedIndex <> -1, chart.Annotations(Me.ComboAnnotationsList.SelectedIndex), Nothing)
        Else
            ChartPropertiesHide()
        End If
    End Sub

    Private Sub ChartsUpdateFromList(ByVal charts As List(Of Chart))
        mCharts.RaiseListChangedEvents = False  ' Pause events during bulk update.
        mCharts.Clear()
        If charts IsNot Nothing Then
            For Each chart As Chart In charts
                mCharts.Add(chart.Text)
            Next
        End If
        mCharts.RaiseListChangedEvents = True   ' Resume events.
        mCharts.ResetBindings()                 ' Trigger a single "Reset" event.
    End Sub

    Private Sub DataPropertiesHide()
        TxtAxesSeries.Text = ""
        ComboAxesXAxisType.SelectedIndex = -1
        TxtAxesXValueMember.Text = ""
        ComboAxesXValueType.SelectedIndex = -1
        ComboAxesYAxisType.SelectedIndex = -1
        TxtAxesYValueMember.Text = ""
        ComboAxesYValueType.SelectedIndex = -1
        NumericAxesYValuesPerPoint.Value = 0

        TxtAxesSeries.Enabled = False
        ComboAxesXAxisType.Enabled = False
        TxtAxesXValueMember.Enabled = False
        ComboAxesXValueType.Enabled = False
        ComboAxesYAxisType.Enabled = False
        TxtAxesYValueMember.Enabled = False
        ComboAxesYValueType.Enabled = False
        NumericAxesYValuesPerPoint.Enabled = False
    End Sub

    Private Sub ColorPickersInitialize(ByVal controls As Control.ControlCollection)
        For Each ctrl As Control In controls
            If TypeOf ctrl Is ComboColorPicker Then
                DirectCast(ctrl, ComboColorPicker).Colors = ComboColorPicker.ColorList.All
                DirectCast(ctrl, ComboColorPicker).InsertColor(Color.Transparent)
            End If
            If ctrl.HasChildren Then
                ColorPickersInitialize(ctrl.Controls)
            End If
        Next
    End Sub

    Private Sub DataPropertiesShow(ByVal series As Series)
        TxtAxesSeries.Enabled = True
        ComboAxesXAxisType.Enabled = True
        TxtAxesXValueMember.Enabled = True
        ComboAxesXValueType.Enabled = True
        ComboAxesYAxisType.Enabled = True
        TxtAxesYValueMember.Enabled = True
        ComboAxesYValueType.Enabled = True
        NumericAxesYValuesPerPoint.Enabled = True

        TxtAxesSeries.Text = series.Name
        ComboAxesXAxisType.SelectedItem = series.XAxisType
        TxtAxesXValueMember.Text = series.XValueMember
        ComboAxesXValueType.SelectedItem = series.XValueType
        ComboAxesYAxisType.SelectedItem = series.YAxisType
        TxtAxesYValueMember.Text = series.YValueMembers
        ComboAxesYValueType.SelectedItem = series.YValueType
        NumericAxesYValuesPerPoint.Value = series.YValuesPerPoint
    End Sub

    Private Function IsFieldQuotedInOriginalLine(rawLine As String, fieldIndex As Integer) As Boolean
        ' Split by commas crudely just to peek at the original characters
        Dim parts() As String = rawLine.Split(","c)
        If fieldIndex < parts.Length Then
            Dim cleanPart As String = parts(fieldIndex).Trim()
            Return cleanPart.StartsWith("""") AndAlso cleanPart.EndsWith("""")
        End If
        Return False
    End Function

    Private Sub LabelMarkerPropertiesHide()
        TxtLabelsMarkersSeries.Text = ""
        TxtLabelsMarkersLabel.Text = ""
        NumericLabelsMarkersLabelAngle.Value = 0
        ComboLabelsMarkersLabelBackColor.SelectedIndex = -1
        ComboLabelsMarkersLabelBorderColor.SelectedIndex = -1
        ComboLabelsMarkersLabelBorderDashStyle.SelectedIndex = -1
        NumericLabelsMarkersLabelBorderWidth.Value = 0
        ComboLabelsMarkersMarkerBorderColor.SelectedIndex = -1
        NumericLabelsMarkersMarkerBorderWidth.Value = 0
        ComboLabelsMarkersMarkerColor.SelectedIndex = -1
        TxtLabelsMarkersMarkerImage.Text = ""
        NumericLabelsMarkersMarkerSize.Value = 0
        NumericLabelsMarkersMarkerStep.Value = 0
        ComboLabelsMarkersMarkerStyle.SelectedIndex = -1

        CmdLabelsMarkersMarkerImage.Enabled = False
        TxtLabelsMarkersLabel.Enabled = False
        TxtLabelsMarkersSeries.Enabled = False
        NumericLabelsMarkersLabelAngle.Enabled = False
        ComboLabelsMarkersLabelBackColor.Enabled = False
        ComboLabelsMarkersLabelBorderColor.Enabled = False
        ComboLabelsMarkersLabelBorderDashStyle.Enabled = False
        NumericLabelsMarkersLabelBorderWidth.Enabled = False
        ComboLabelsMarkersMarkerBorderColor.Enabled = False
        NumericLabelsMarkersMarkerBorderWidth.Enabled = False
        ComboLabelsMarkersMarkerColor.Enabled = False
        TxtLabelsMarkersMarkerImage.Enabled = False
        NumericLabelsMarkersMarkerSize.Enabled = False
        NumericLabelsMarkersMarkerStep.Enabled = False
        ComboLabelsMarkersMarkerStyle.Enabled = False
    End Sub

    Private Sub LabelMarkerPropertiesShow(ByVal series As Series)
        CmdLabelsMarkersMarkerImage.Enabled = True
        TxtLabelsMarkersLabel.Enabled = True
        TxtLabelsMarkersSeries.Enabled = True
        NumericLabelsMarkersLabelAngle.Enabled = True
        ComboLabelsMarkersLabelBackColor.Enabled = True
        ComboLabelsMarkersLabelBorderColor.Enabled = True
        ComboLabelsMarkersLabelBorderDashStyle.Enabled = True
        NumericLabelsMarkersLabelBorderWidth.Enabled = True
        ComboLabelsMarkersMarkerBorderColor.Enabled = True
        NumericLabelsMarkersMarkerBorderWidth.Enabled = True
        ComboLabelsMarkersMarkerColor.Enabled = True
        TxtLabelsMarkersMarkerImage.Enabled = True
        NumericLabelsMarkersMarkerSize.Enabled = True
        NumericLabelsMarkersMarkerStep.Enabled = True
        ComboLabelsMarkersMarkerStyle.Enabled = True

        TxtLabelsMarkersSeries.Text = series.Name
        TxtLabelsMarkersLabel.Text = series.Label
        NumericLabelsMarkersLabelAngle.Value = series.LabelAngle
        ComboLabelsMarkersLabelBackColor.Color = series.LabelBackColor
        ComboLabelsMarkersLabelBorderColor.Color = series.MarkerBorderColor
        ComboLabelsMarkersLabelBorderDashStyle.DashStyle = series.LabelBorderDashStyle
        NumericLabelsMarkersLabelBorderWidth.Value = series.LabelBorderWidth
        ComboLabelsMarkersMarkerBorderColor.Color = series.MarkerBorderColor
        NumericLabelsMarkersMarkerBorderWidth.Value = series.MarkerBorderWidth
        ComboLabelsMarkersMarkerColor.Color = series.MarkerColor
        TxtLabelsMarkersMarkerImage.Text = series.MarkerImage
        NumericLabelsMarkersMarkerSize.Value = series.MarkerSize
        NumericLabelsMarkersMarkerStep.Value = series.MarkerStep
        ComboLabelsMarkersMarkerStyle.SelectedIndex = series.MarkerStyle
    End Sub

    Private Sub LegendPropertiesHide()
        ComboLegendsList.SelectedIndex = -1
        ComboLegendStringAlignment.SelectedIndex = -1
        NumericLegendsAutoFitMinFontSize.Value = 0
        TxtLegendsBackImage.Text = ""
        ComboLegendsBorderColor.SelectedIndex = -1
        ComboLegendsBorderDashStyle.SelectedIndex = -1
        NumericLegendsBorderWidth.Value = 0
        ComboLegendsForeColor.SelectedIndex = -1
        ComboLegendsHeaderSeparator.SelectedIndex = -1
        ChkLegendsIsEquallySpacedItems.Checked = False
        ChkLegendsIsTextAutoFit.Checked = False
        ComboLegendsItemColumnSeparator.SelectedIndex = -1
        ComboLegendsItemColumnSepColor.SelectedIndex = -1
        NumericLegendsItemColumnSpacing.Value = 0
        TxtLegend.Text = ""
        TxtLegendsMaximumAutoSize.Text = ""
        ComboLegendsPosition.SelectedIndex = -1
        ComboLegendsShadowColor.SelectedIndex = -1
        NumericLegendsShadowOffset.Value = 0
        ComboLegendsTableStyle.SelectedIndex = -1
        NumericLegendsTextWrapThreshold.Value = 0
        TxtLegendsTitle.Text = ""
        ComboLegendsTitleAlignment.SelectedIndex = -1
        ComboLegendsTitleBackColor.SelectedIndex = -1
        ComboLegendsTitleForeColor.SelectedIndex = -1
        ComboLegendsTitleSeparator.SelectedIndex = -1
        ComboLegendsTitleSeparatorColor.SelectedIndex = -1

        ComboLegendsList.Enabled = False
        CmdLegendsRemove.Enabled = False
        ComboLegendStringAlignment.Enabled = False
        NumericLegendsAutoFitMinFontSize.Enabled = False
        CmdLegendsBackImage.Enabled = False
        TxtLegendsBackImage.Enabled = False
        ComboLegendsBorderColor.Enabled = False
        ComboLegendsBorderDashStyle.Enabled = False
        NumericLegendsBorderWidth.Enabled = False
        CmdLegendsFont.Enabled = False
        ComboLegendsForeColor.Enabled = False
        ComboLegendsHeaderSeparator.Enabled = False
        ChkLegendsIsEquallySpacedItems.Enabled = False
        ChkLegendsIsTextAutoFit.Enabled = False
        ComboLegendsItemColumnSeparator.Enabled = False
        ComboLegendsItemColumnSepColor.Enabled = False
        NumericLegendsItemColumnSpacing.Enabled = False
        TxtLegend.Enabled = False
        TxtLegendsMaximumAutoSize.Enabled = False
        ComboLegendsPosition.Enabled = False
        ComboLegendsShadowColor.Enabled = False
        NumericLegendsShadowOffset.Enabled = False
        ComboLegendsTableStyle.Enabled = False
        NumericLegendsTextWrapThreshold.Enabled = False
        TxtLegendsTitle.Enabled = False
        ComboLegendsTitleAlignment.Enabled = False
        ComboLegendsTitleBackColor.Enabled = False
        CmdLegendsTitleFont.Enabled = False
        ComboLegendsTitleForeColor.Enabled = False
        ComboLegendsTitleSeparator.Enabled = False
        ComboLegendsTitleSeparatorColor.Enabled = False
    End Sub

    Private Sub LegendPropertiesShow(ByVal legend As Legend)
        ComboLegendsList.Enabled = True
        CmdLegendsRemove.Enabled = True
        ComboLegendStringAlignment.Enabled = True
        NumericLegendsAutoFitMinFontSize.Enabled = True
        CmdLegendsBackImage.Enabled = True
        TxtLegendsBackImage.Enabled = True
        ComboLegendsBorderColor.Enabled = True
        ComboLegendsBorderDashStyle.Enabled = True
        NumericLegendsBorderWidth.Enabled = True
        CmdLegendsFont.Enabled = True
        ComboLegendsForeColor.Enabled = True
        ComboLegendsHeaderSeparator.Enabled = True
        ChkLegendsIsEquallySpacedItems.Enabled = True
        ChkLegendsIsTextAutoFit.Enabled = True
        ComboLegendsItemColumnSeparator.Enabled = True
        ComboLegendsItemColumnSepColor.Enabled = True
        NumericLegendsItemColumnSpacing.Enabled = True
        TxtLegend.Enabled = True
        TxtLegendsMaximumAutoSize.Enabled = True
        ComboLegendsPosition.Enabled = True
        ComboLegendsShadowColor.Enabled = True
        NumericLegendsShadowOffset.Enabled = True
        ComboLegendsTableStyle.Enabled = True
        NumericLegendsTextWrapThreshold.Enabled = True
        TxtLegendsTitle.Enabled = True
        ComboLegendsTitleAlignment.Enabled = True
        ComboLegendsTitleBackColor.Enabled = True
        CmdLegendsTitleFont.Enabled = True
        ComboLegendsTitleForeColor.Enabled = True
        ComboLegendsTitleSeparator.Enabled = True
        ComboLegendsTitleSeparatorColor.Enabled = True

        ComboLegendsList.SelectedItem = legend.Name
        ComboLegendStringAlignment.Alignment = legend.Alignment
        NumericLegendsAutoFitMinFontSize.Value = legend.AutoFitMinFontSize
        TxtLegendsBackImage.Text = legend.BackImage
        ComboLegendsBorderColor.Color = legend.BorderColor
        ComboLegendsBorderDashStyle.DashStyle = legend.BorderDashStyle
        NumericLegendsBorderWidth.Value = legend.BorderWidth
        ComboLegendsForeColor.Color = legend.ForeColor
        ComboLegendsHeaderSeparator.SeparatorStyle = legend.HeaderSeparator
        ChkLegendsIsEquallySpacedItems.Checked = legend.IsEquallySpacedItems
        ChkLegendsIsTextAutoFit.Checked = legend.IsTextAutoFit
        ComboLegendsItemColumnSeparator.SeparatorStyle = legend.ItemColumnSeparator
        ComboLegendsItemColumnSepColor.Color = legend.ItemColumnSeparatorColor
        NumericLegendsItemColumnSpacing.Value = legend.ItemColumnSpacing
        TxtLegend.Text = legend.Name
        TxtLegendsMaximumAutoSize.Text = legend.MaximumAutoSize.ToString()
        ComboLegendsPosition.SelectedItem = legend.Position
        ComboLegendsShadowColor.Color = legend.ShadowColor
        NumericLegendsShadowOffset.Value = legend.ShadowOffset
        ComboLegendsTableStyle.TableStyle = legend.TableStyle
        NumericLegendsTextWrapThreshold.Value = legend.TextWrapThreshold
        TxtLegendsTitle.Text = legend.Title
        ComboLegendsTitleAlignment.Alignment = legend.TitleAlignment
        ComboLegendsTitleBackColor.Color = legend.TitleBackColor
        ComboLegendsTitleForeColor.Color = legend.TitleForeColor
        ComboLegendsTitleSeparator.SeparatorStyle = legend.HeaderSeparator
        ComboLegendsTitleSeparatorColor.Color = legend.TitleSeparatorColor
    End Sub

    Private Sub LegendSelect(ByVal legend As Legend)
        If legend IsNot Nothing Then
            LegendPropertiesShow(legend)
        Else
            LegendPropertiesHide()
        End If
    End Sub

    Private Sub LegendsUpdateFromChart(ByVal chart As Chart)
        mLegends.RaiseListChangedEvents = False  ' Pause events during bulk update.
        mLegends.Clear()
        If chart IsNot Nothing Then
            For Each legend As Legend In chart.Legends
                mLegends.Add(legend.Name)
            Next
        End If
        mLegends.RaiseListChangedEvents = True   ' Resume events.
        mLegends.ResetBindings()                 ' Trigger a single "Reset" event.
    End Sub

    Function ReadCsvLineToArray(ByVal line As String) As Object()
        Using reader As New StringReader(line)
            Using parser As New TextFieldParser(reader)
                parser.TextFieldType = FieldType.Delimited
                parser.SetDelimiters(",")
                parser.HasFieldsEnclosedInQuotes = True ' This handles the quote stripping automatically

                Dim fields() As String = parser.ReadFields()
                If fields Is Nothing Then Return Array.Empty(Of Object)()

                ' 2. Initialize our target Object array to match the CSV column count
                Dim targetArray(fields.Length - 1) As Object

                ' 3. Process each field based on your exact strict requirements
                For i As Integer = 0 To fields.Length - 1
                    Dim rawValue As String = fields(i)

                    ' Check if the field originally had quotes in the raw CSV string.
                    ' TextFieldParser strips them, so if it WAS quoted, it's explicitly a String.
                    If IsFieldQuotedInOriginalLine(line, i) Then
                        targetArray(i) = rawValue ' Keep as String
                    Else
                        ' If it had no quotes, check if it can cleanly convert to a number
                        Dim intResult As Integer
                        Dim doubleResult As Double

                        If Integer.TryParse(rawValue, intResult) Then
                            targetArray(i) = intResult ' Store as Integer
                        ElseIf Double.TryParse(rawValue, doubleResult) Then
                            targetArray(i) = doubleResult ' Store as Double
                        Else
                            ' Fallback: If it had no quotes but isn't a number, it breaks your requirement.
                            ' You can throw an error here, or gracefully handle it as a String.
                            targetArray(i) = rawValue
                        End If
                    End If
                Next

                Return targetArray
            End Using
        End Using
    End Function

    Private Function SeriesDataRead(ByVal filePath As String) As SeriesData
        Dim data As SeriesData = Nothing
        Dim lines() As String = File.ReadAllLines(filePath)

        If lines.Length > 0 Then
            data = New SeriesData() With {.XData = ReadCsvLineToArray(lines(0))}
        End If

        If lines.Length > 1 Then
            data.YData = ReadCsvLineToArray(lines(1))
        End If

        If data.XData.Length > 0 AndAlso data.YData.Length > 0 Then
            If data.XData.Length <> data.XData.Length Then
                Throw New InvalidDataException("X data and Y data rows must have the same number of elements.")
            End If
        End If

        Return data
    End Function

    Private Sub SeriesDataShow(Of TX, TY As INumber(Of TY))(ByVal series As Series, ByVal xData() As TX, ByVal yData() As TY)
        series.Points.Clear()

        ' Cast both arrays to IEnumerable for MS Chart compatibility.
        Dim bindableX As IEnumerable = CType(xData, IEnumerable)
        Dim bindableY As IEnumerable = CType(yData, IEnumerable)

        ' Bind both axes simultaneously.
        series.Points.DataBindXY(bindableX, bindableY)
    End Sub

    Private Sub SeriesDataShow(Of TY As INumber(Of TY))(ByVal series As Series, ByVal xData() As String, ByVal yData() As TY)
        series.Points.Clear()

        ' Cast both arrays to IEnumerable for MS Chart compatibility.
        Dim bindableX As IEnumerable = CType(xData, IEnumerable)
        Dim bindableY As IEnumerable = CType(yData, IEnumerable)

        ' Bind both axes simultaneously.
        series.Points.DataBindXY(bindableX, bindableY)
    End Sub

    Private Sub SeriesDataShow(ByVal series As Series, ByVal data As SeriesData)
        If data IsNot Nothing Then
            If data.XData IsNot Nothing AndAlso data.XData.Length > 0 Then
                If TypeOf data.XData(0) Is Integer OrElse TypeOf data.XData(0) Is Single OrElse TypeOf data.XData(0) Is Double Then
                    SeriesDataShow(series,
                    data.XData.Select(
                            Function(obj)
                                If obj IsNot Nothing AndAlso obj.IsNumericType() Then
                                    Return Convert.ToDouble(obj)
                                Else
                                    Return 0.0 ' Fallback value for strings/Nothing
                                End If
                            End Function
                        ).ToArray(),
                    data.YData.Select(
                        Function(obj)
                            If obj IsNot Nothing AndAlso TypeOf obj Is Integer OrElse TypeOf obj Is Single OrElse TypeOf obj Is Double Then
                                Return Convert.ToDouble(obj)
                            Else
                                Return 0.0 ' Fallback value for strings/Nothing
                            End If
                        End Function
                    ).ToArray())
                Else
                    SeriesDataShow(series,
                    data.XData.Select(
                        Function(obj) If(obj IsNot Nothing, obj.ToString(), "")
                        ).ToArray(),
                    data.YData.Select(
                        Function(obj)
                            If obj IsNot Nothing AndAlso TypeOf obj Is Integer OrElse TypeOf obj Is Single OrElse TypeOf obj Is Double Then
                                Return Convert.ToDouble(obj)
                            Else
                                Return 0.0 ' Fallback value for strings/Nothing
                            End If
                        End Function
                    ).ToArray())
                End If
            End If
        End If
    End Sub

    Private Sub SeriesPropertiesHide()
        ComboSeriesChartArea.SelectedIndex = -1
        ComboSeriesChartType.SelectedIndex = -1
        TxtSeriesAxisLabel.Text = ""
        ComboSeriesBackHatchStyle.SelectedIndex = -1
        TxtSeriesBackImage.Text = ""
        ComboSeriesBorderColor.SelectedIndex = -1
        ComboSeriesBorderDashStyle.SelectedIndex = -1
        NumericSeriesBorderWidth.Value = 0
        ComboSeriesColor.SelectedIndex = -1
        ChkSeriesIsValueShownAsLabel.Checked = False
        ChkSeriesIsVisibleInLegend.Checked = False
        ChkSeriesIsXValueIndexed.Checked = False
        ComboSeriesColorPalette.SelectedIndex = -1
        ComboSeriesShadowColor.SelectedIndex = -1
        NumericSeriesShadowOffset.Value = 0
        ChkSmartLabelsEnabled.Checked = False

        CmdSeriesBackImage.Enabled = False
        CmdSeriesDataSource.Enabled = False
        CmdSeriesRemove.Enabled = False
        ComboSeriesChartArea.Enabled = False
        ComboSeriesChartType.Enabled = False
        CmdSeriesDataSource.Enabled = False
        TxtSeriesDataSource.Enabled = False
        TxtSeriesAxisLabel.Enabled = False
        ComboSeriesBackHatchStyle.Enabled = False
        TxtSeriesBackImage.Enabled = False
        ComboSeriesBorderColor.Enabled = False
        ComboSeriesBorderDashStyle.Enabled = False
        NumericSeriesBorderWidth.Enabled = False
        ComboSeriesColor.Enabled = False
        ChkSeriesIsValueShownAsLabel.Enabled = False
        ChkSeriesIsVisibleInLegend.Enabled = False
        ChkSeriesIsXValueIndexed.Enabled = False
        ComboSeriesColorPalette.Enabled = False
        ComboSeriesShadowColor.Enabled = False
        NumericSeriesShadowOffset.Enabled = False
        ChkSmartLabelsEnabled.Enabled = False
    End Sub

    Private Sub SeriesPropertiesShow(ByVal series As Series)
        CmdSeriesBackImage.Enabled = True
        CmdSeriesDataSource.Enabled = True
        CmdSeriesRemove.Enabled = True
        ComboSeriesChartArea.Enabled = True
        ComboSeriesChartType.Enabled = True
        CmdSeriesDataSource.Enabled = True
        TxtSeriesDataSource.Enabled = True
        TxtSeriesAxisLabel.Enabled = True
        ComboSeriesBackHatchStyle.Enabled = True
        TxtSeriesBackImage.Enabled = True
        ComboSeriesBorderColor.Enabled = True
        ComboSeriesBorderDashStyle.Enabled = True
        NumericSeriesBorderWidth.Enabled = True
        ComboSeriesColor.Enabled = True
        ChkSeriesIsValueShownAsLabel.Enabled = True
        ChkSeriesIsVisibleInLegend.Enabled = True
        ChkSeriesIsXValueIndexed.Enabled = True
        ComboSeriesColorPalette.Enabled = True
        ComboSeriesShadowColor.Enabled = True
        NumericSeriesShadowOffset.Enabled = True
        ChkSmartLabelsEnabled.Enabled = True

        ComboSeriesChartArea.SelectedItem = series.ChartArea
        ComboSeriesChartType.ChartType = series.ChartType
        TxtSeriesAxisLabel.Text = series.AxisLabel
        ComboSeriesBackHatchStyle.HatchStyle = series.BackHatchStyle
        TxtSeriesBackImage.Text = series.BackImage
        ComboSeriesBorderColor.Color = series.BorderColor
        ComboSeriesBorderDashStyle.DashStyle = series.BorderDashStyle
        NumericSeriesBorderWidth.Value = series.BorderWidth
        ComboSeriesColor.Color = series.Color
        ChkSeriesIsValueShownAsLabel.Checked = series.IsValueShownAsLabel
        ChkSeriesIsVisibleInLegend.Checked = series.IsVisibleInLegend
        ChkSeriesIsXValueIndexed.Checked = series.IsXValueIndexed
        ComboSeriesColorPalette.ColorPalette = series.Palette
        ComboSeriesShadowColor.Color = series.ShadowColor
        NumericSeriesShadowOffset.Value = series.ShadowOffset
        ChkSmartLabelsEnabled.Checked = series.SmartLabelStyle.Enabled
    End Sub

    Private Sub SeriesSelect(ByVal series As Series)
        If series IsNot Nothing Then
            SeriesPropertiesShow(series)
            LabelMarkerPropertiesShow(series)
            DataPropertiesShow(series)
        Else
            SeriesPropertiesHide()
            LabelMarkerPropertiesHide()
            DataPropertiesHide()
        End If
    End Sub

    Private Sub SeriesUpdateFromChart(ByVal chart As Chart)
        mSeries.RaiseListChangedEvents = False  ' Pause events during bulk update.
        mSeries.Clear()
        If chart IsNot Nothing Then
            For Each series As Series In chart.Series
                mSeries.Add(series.Name)
            Next
        End If
        mSeries.RaiseListChangedEvents = True   ' Resume events.
        mSeries.ResetBindings()                 ' Trigger a single "Reset" event.
    End Sub

    Private Sub TitlePropertiesHide()
        ComboTitlesList.SelectedIndex = -1
        ChkTitleVisible.Checked = False
        ComboTitleBackColor.SelectedIndex = -1
        ComboTitleBackGradientStyle.SelectedIndex = -1
        ComboTitleBackHatchStyle.SelectedIndex = -1
        TxtTitleBackImage.Text = ""
        ComboTitleBackImageTransparentColor.SelectedIndex = -1
        ComboTitleBackImageAlignmentStyle.SelectedIndex = -1
        ComboTitleBorderColor.SelectedIndex = -1
        ComboTitleBorderDashStyle.SelectedIndex = -1
        NumericTitleBorderWidth.Value = 0
        ComboTitleContentAlignment.SelectedIndex = -1
        ComboTitleForeColor.SelectedIndex = -1
        ComboTitleShadowColor.SelectedIndex = -1
        NumericTitleShadowOffset.Value = 0
        TxtTitleText.Text = ""
        ComboTitleTextOrientation.SelectedIndex = -1
        ComboTitleTextStyle.SelectedIndex = -1

        ComboTitlesList.Enabled = False
        CmdTitlesRemove.Enabled = False
        CmdTitleBackImage.Enabled = False
        ComboTitleBackColor.Enabled = False
        ComboTitleBackGradientStyle.Enabled = False
        ComboTitleBackHatchStyle.Enabled = False
        TxtTitleBackImage.Enabled = False
        ComboTitleBackImageTransparentColor.Enabled = False
        ComboTitleBackImageAlignmentStyle.Enabled = False
        ComboTitleBorderColor.Enabled = False
        ComboTitleBorderDashStyle.Enabled = False
        NumericTitleBorderWidth.Enabled = False
        ComboTitleContentAlignment.Enabled = False
        CmdTitleFont.Enabled = False
        ComboTitleForeColor.Enabled = False
        ComboTitleShadowColor.Enabled = False
        NumericTitleShadowOffset.Enabled = False
        TxtTitleText.Enabled = False
        ComboTitleTextOrientation.Enabled = False
        ComboTitleTextStyle.Enabled = False
        ChkTitleVisible.Enabled = False
    End Sub

    Private Sub TitlePropertiesShow(ByVal title As Title)
        ComboTitlesList.Enabled = True
        CmdTitlesRemove.Enabled = True
        CmdTitleBackImage.Enabled = True
        ComboTitleBackColor.Enabled = True
        ComboTitleBackGradientStyle.Enabled = True
        ComboTitleBackHatchStyle.Enabled = True
        TxtTitleBackImage.Enabled = True
        ComboTitleBackImageTransparentColor.Enabled = False
        ComboTitleBackImageAlignmentStyle.Enabled = False
        ComboTitleBorderColor.Enabled = True
        ComboTitleBorderDashStyle.Enabled = True
        NumericTitleBorderWidth.Enabled = True
        ComboTitleContentAlignment.Enabled = True
        CmdTitleFont.Enabled = True
        ComboTitleForeColor.Enabled = True
        ComboTitleShadowColor.Enabled = True
        NumericTitleShadowOffset.Enabled = True
        TxtTitleText.Enabled = True
        ComboTitleTextOrientation.Enabled = True
        ComboTitleTextStyle.Enabled = True
        ChkTitleVisible.Enabled = True

        ComboTitlesList.SelectedItem = title.Name
        ChkTitleVisible.Checked = title.Visible
        ComboTitleBackColor.Color = title.BackColor
        ComboTitleBackGradientStyle.GradientStyle = title.BackGradientStyle
        ComboTitleBackHatchStyle.HatchStyle = title.BackHatchStyle
        TxtTitleBackImage.Text = title.BackImage
        ComboTitleBackImageTransparentColor.Color = title.BackImageTransparentColor
        ComboTitleBackImageAlignmentStyle.AlignmentStyle = title.BackImageAlignment
        ComboTitleBorderColor.Color = title.BorderColor
        ComboTitleBorderDashStyle.DashStyle = title.BorderDashStyle
        NumericTitleBorderWidth.Value = title.BorderWidth
        ComboTitleContentAlignment.Alignment = title.Alignment
        ComboTitleForeColor.Color = title.ForeColor
        ComboTitleShadowColor.Color = title.ShadowColor
        NumericTitleShadowOffset.Value = title.ShadowOffset
        TxtTitleText.Text = title.Text
        ComboTitleTextOrientation.OrientationName = title.TextOrientation.ToString()
        ComboTitleTextStyle.TextStyle = title.TextStyle
    End Sub

    Private Sub TitleSelect(ByVal title As Title)
        If title IsNot Nothing Then
            TitlePropertiesShow(title)
        Else
            TitlePropertiesHide()
        End If
    End Sub

    Private Sub TitlesUpdateFromChart(ByVal chart As Chart)
        mTitles.RaiseListChangedEvents = False  ' Pause events during bulk update.
        mTitles.Clear()
        If chart IsNot Nothing Then
            For Each title As Title In chart.Titles
                mTitles.Add(title.Text)
            Next
        End If
        mTitles.RaiseListChangedEvents = True   ' Resume events.
        mTitles.ResetBindings()                 ' Trigger a single "Reset" event.
    End Sub
#End Region
#Region "Event Handlers"
#Region "Controls"
    Private Sub Chk_CheckedChanged(sender As Object, e As EventArgs) Handles ChkAxesInterlacingIsInterlaced.CheckedChanged
        Try
            Dim chk As CheckBox = DirectCast(sender, CheckBox)

            Select Case chk.Name
                Case "ChkAxesInterlacingIsInterlaced"
                    If chk.Checked Then
                        ComboAxesInterlacingInterlacedColor.Enabled = True
                        ComboAxesInterlacingInterlacedColor.Color = SelectedAxis.InterlacedColor
                    Else
                        ComboAxesInterlacingInterlacedColor.SelectedIndex = -1
                        ComboAxesInterlacingInterlacedColor.Enabled = False
                    End If
                Case Else
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Chk_UserCheckedChanged(sender As Object, e As EventArgs) Handles ChkTitleVisible.CheckedChanged,
        ChkAxesInterlacingIsInterlaced.CheckedChanged,
        ChkSeriesIsValueShownAsLabel.CheckedChanged, ChkSeriesIsVisibleInLegend.CheckedChanged, ChkSeriesIsXValueIndexed.CheckedChanged, ChkSmartLabelsEnabled.CheckedChanged,
        ChkLegendsIsEquallySpacedItems.CheckedChanged, ChkLegendsIsTextAutoFit.CheckedChanged,
        ChkAxesScalingAndMathIsLogarithmic.CheckedChanged, ChkAxesScalingAndMathIsReversed.CheckedChanged, ChkAxesMajorGridEnabled.CheckedChanged, ChkAxesMinorGridEnabled.CheckedChanged
        Try
            If Not mUserInput Then Return

            Dim chk = DirectCast(sender, CheckBox)

            Select Case chk.Name
                Case "ChkChartIsSoftShadows"
                    SelectedChart.IsSoftShadows = chk.Checked
                Case "ChkTitleVisible"
                    SelectedTitle.Visible = chk.Checked
                Case "ChkAxesInterlacingIsInterlaced"
                    SelectedAxis.IsInterlaced = chk.Checked
                Case "ChkSeriesIsValueShownAsLabel"
                    SelectedSeries.IsValueShownAsLabel = chk.Checked
                Case "ChkSeriesIsVisibleInLegend"
                    SelectedSeries.IsVisibleInLegend = chk.Checked
                Case "ChkSeriesIsXValueIndexed"
                    SelectedSeries.IsXValueIndexed = chk.Checked
                Case "ChkLegendsIsEquallySpacedItems"
                    SelectedLegend.IsEquallySpacedItems = chk.Checked
                Case "ChkLegendsIsTextAutoFit"
                    SelectedLegend.IsTextAutoFit = chk.Checked
                Case "ChkSmartLabelsEnabled"
                    SelectedSeries.SmartLabelStyle.Enabled = chk.Checked
                Case "ChkAxesScalingAndMathIsLogarithmic"
                    SelectedAxis.IsLogarithmic = chk.Checked
                Case "ChkAxesScalingAndMathIsReversed"
                    SelectedAxis.IsReversed = chk.Checked
                Case "ChkAxesMajorGridEnabled"
                    SelectedAxis.MajorGrid.Enabled = chk.Checked
                Case "ChkAxesMinorGridEnabled"
                    SelectedAxis.MinorGrid.Enabled = chk.Checked
                Case Else
            End Select

            mUserInput = False
        Catch ex As Exception
#If DEBUG Then
            Debug.WriteLine($"{sender}: {ex.Message}")
#Else
            FileLogger.LogException(ex)
#End If
        End Try
    End Sub

    Private Sub Chk_MouseDown(sender As Object, e As MouseEventArgs) Handles ChkTitleVisible.MouseDown,
        ChkAxesInterlacingIsInterlaced.MouseDown,
        ChkSeriesIsValueShownAsLabel.MouseDown, ChkSeriesIsVisibleInLegend.MouseDown, ChkSeriesIsXValueIndexed.MouseDown, ChkSmartLabelsEnabled.MouseDown,
        ChkLegendsIsEquallySpacedItems.MouseDown, ChkLegendsIsTextAutoFit.MouseDown,
        ChkAxesScalingAndMathIsLogarithmic.MouseDown, ChkAxesScalingAndMathIsReversed.MouseDown, ChkAxesMajorGridEnabled.MouseDown, ChkAxesMinorGridEnabled.MouseDown
        '
        ' We only want user-initiated check changes to take effect.
        ' Check for {MouseButtons.Left} and set flag. This routine handles all CheckBoxes.
        '
        If e.Button = MouseButtons.Left Then mUserInput = True
    End Sub

    Private Sub CmdCancel_Click(sender As Object, e As EventArgs) Handles CmdCancel.Click
        ChartsRestoreOriginal()
        Me.Close()
    End Sub

    Private Sub CmdFileOpen_Click(sender As Object, e As EventArgs) Handles CmdChartBackImage.Click,
        CmdTitleBackImage.Click,
        CmdChartAreaBackImage.Click,
        CmdSeriesDataSource.Click, CmdSeriesBackImage.Click,
        CmdLabelsMarkersMarkerImage.Click,
        CmdLegendsBackImage.Click
        Const kImageFilesFilter As String = "Image Files (*.png;*.jpg;*.jpeg;*.gif;*.bmp)|*.png;*.jpg;*.jpeg;*.gif;*.bmp|All Files (*.*)|*.*"
        Const kDataFilesFilter As String = "CSV Files (*.csv)|*.csv|Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
        Try
            Dim cmd As Button = DirectCast(sender, Button)
            Dim dlg As New OpenFileDialog() With {.Title = "Select File"}
            Dim folderMyPictures As String = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
            Dim folderMyDocuments As String = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            Dim txt As TextBox = Nothing

            Select Case cmd.Name
                Case "CmdChartBackImage"
                    dlg.InitialDirectory = folderMyPictures
                    dlg.Filter = kImageFilesFilter
                Case "CmdTitleBackImage"
                    dlg.InitialDirectory = folderMyPictures
                    dlg.Filter = kImageFilesFilter
                Case "CmdChartAreaBackImage"
                    dlg.InitialDirectory = folderMyPictures
                    dlg.Filter = kImageFilesFilter
                Case "CmdSeriesDataSource"
                    dlg.InitialDirectory = folderMyDocuments
                    dlg.Filter = kDataFilesFilter
                Case "CmdSeriesBackImage"
                    dlg.InitialDirectory = folderMyPictures
                    dlg.Filter = kImageFilesFilter
                Case "CmdLabelsMarkersMarkerImage"
                    dlg.InitialDirectory = folderMyPictures
                    dlg.Filter = kImageFilesFilter
                Case "CmdLegendsBackImage"
                    dlg.InitialDirectory = folderMyPictures
                    dlg.Filter = kImageFilesFilter
                Case Else
            End Select

            dlg.FilterIndex = 1

            If dlg.ShowDialog() = DialogResult.OK Then
                Select Case cmd.Name
                    Case "CmdChartBackImage"
                        SelectedChart.BackImage = dlg.FileName
                        txt = TxtChartBackImage
                    Case "CmdTitleBackImage"
                        SelectedTitle.BackImage = dlg.FileName
                        txt = TxtTitleBackImage
                    Case "CmdChartAreaBackImage"
                        SelectedChartArea.BackImage = dlg.FileName
                        txt = TxtChartAreaBackImage
                    Case "CmdSeriesDataSource"
                        Dim data As SeriesData = SeriesDataRead(dlg.FileName)

                        SeriesDataShow(SelectedSeries, data)
                        txt = TxtSeriesDataSource
                    Case "CmdSeriesBackImage"
                        SelectedSeries.BackImage = dlg.FileName
                        txt = TxtSeriesBackImage
                    Case "CmdLabelsMarkersMarkerImage"
                        SelectedSeries.MarkerImage = dlg.FileName
                        txt = TxtLabelsMarkersMarkerImage
                    Case "CmdLegendsBackImage"
                        SelectedLegend.BackImage = dlg.FileName
                        txt = TxtLegendsBackImage
                    Case Else
                End Select

                If txt IsNot Nothing Then txt.Text = dlg.FileName
            End If
        Catch ex As Exception
#If DEBUG Then
            Debug.WriteLine($"{sender}: {ex.Message}")
#Else
            FileLogger.LogException(ex)
#End If
        End Try
    End Sub

    Private Sub CmdFont_Click(sender As Object, e As EventArgs) Handles CmdChartFont.Click,
        CmdTitleFont.Click,
        CmdAxesLabelsFont.Click,
        CmdLegendsFont.Click, CmdAxesTitleFont.Click,
        CmdAnnotationsFont.Click
        Try
            Dim cmd = DirectCast(sender, Button)
            Dim dlg As New FontDialog With {
                .ShowColor = True
            }

            Select Case cmd.Name
                Case "CmdChartFont"
                    dlg.Font = SelectedChart.Font
                    dlg.Color = SelectedChart.ForeColor
                Case "CmdTitleFont"
                    dlg.Font = SelectedTitle.Font
                    dlg.Color = SelectedTitle.ForeColor
                Case "CmdLegendsFont"
                    dlg.Font = SelectedLegend.Font
                    dlg.Color = SelectedLegend.ForeColor
                Case "CmdAnnotationsFont"
                    dlg.Font = SelectedAnnotation.Font
                    dlg.Color = SelectedAnnotation.ForeColor
                Case "CmdAxesLabelsFont"
                    dlg.Font = SelectedAxis.LabelStyle.Font
                    dlg.Color = SelectedAxis.LabelStyle.ForeColor
                Case "CmdAxesTitleFont"
                    dlg.Font = SelectedAxis.TitleFont
                    dlg.Color = SelectedAxis.TitleForeColor
                Case Else
            End Select

            If dlg.ShowDialog = DialogResult.OK Then
                Select Case cmd.Name
                    Case "CmdChartFont"
                        SelectedChart.Font = dlg.Font
                        SelectedChart.ForeColor = dlg.Color
                        TxtChartFontHeight.Text = SelectedChart.Font.Height.ToString()
                        ComboChartForeColor.Color = dlg.Color
                    Case "CmdTitleFont"
                        SelectedTitle.Font = dlg.Font
                        SelectedTitle.ForeColor = dlg.Color
                        ComboTitleForeColor.Color = dlg.Color
                    Case "CmdLegendsFont"
                        SelectedLegend.Font = dlg.Font
                        SelectedLegend.ForeColor = dlg.Color
                        ComboLegendsForeColor.Color = dlg.Color
                    Case "CmdAnnotationsFont"
                        SelectedAnnotation.Font = dlg.Font
                        SelectedAnnotation.ForeColor = dlg.Color
                        ComboAnnotationsForeColor.Color = dlg.Color
                    Case "CmdAxesLabelsFont"
                        SelectedAxis.LabelStyle.Font = dlg.Font
                        SelectedAxis.LabelStyle.ForeColor = dlg.Color
                        ComboAxesLabelsForeColor.Color = dlg.Color
                    Case "CmdAxesTitleFont"
                        SelectedAxis.TitleFont = dlg.Font
                        SelectedAxis.TitleForeColor = dlg.Color
                        CmdAxesTitleFont.BackColor = dlg.Color
                    Case Else
                End Select
            End If
        Catch ex As Exception
#If DEBUG Then
            Debug.WriteLine($"{sender}: {ex.Message}")
#Else
            FileLogger.LogException(ex)
#End If
        End Try
    End Sub

    Private Sub CmdItemAddNew_Click(sender As Object, e As EventArgs) Handles CmdTitlesAddNew.Click,
        CmdChartAreasAddNew.Click,
        CmdSeriesAddNew.Click,
        CmdLegendsAddNew.Click,
        CmdAnnotationsAddNew.Click
        Try
            Dim cmd As Button = DirectCast(sender, Button)
            Dim txtInput As String = InputBox("Enter the item name:")

            If String.IsNullOrEmpty(txtInput) Then Return

            Select Case cmd.Name
                Case "CmdTitlesAddNew"
                    SelectedChart.Titles.Add(txtInput)
                    TitlesUpdateFromChart(SelectedChart)
                    ComboTitlesList.FindStringExact(txtInput)
                    SelectedTitle = SelectedChart.Titles(ComboTitlesList.SelectedIndex)
                Case "CmdChartAreasAddNew"
                    SelectedChart.ChartAreas.Add(txtInput)
                    ChartAreasUpdateFromChart(SelectedChart)
                    ComboChartAreasList.FindStringExact(txtInput)
                    SelectedChartArea = SelectedChart.ChartAreas(ComboChartAreasList.SelectedIndex)
                Case "CmdSeriesAddNew"
                    Dim series As Series = SelectedChart.Series.Add(txtInput)

                    series.ChartArea = SelectedChartArea.Name
                    SeriesUpdateFromChart(SelectedChart)
                    ComboSeriesList.FindStringExact(txtInput)
                    SelectedSeries = SelectedChart.Series(ComboSeriesList.SelectedIndex)
                Case "CmdLegendsAddNew"
                    SelectedChart.Legends.Add(txtInput)
                    LegendsUpdateFromChart(SelectedChart)
                    ComboLegendsList.FindStringExact(txtInput)
                    SelectedLegend = SelectedChart.Legends(ComboLegendsList.SelectedIndex)
                Case "CmdAnnotationsAddNew"
                    Dim unused = AnnotationsAddNew(ComboAnnotationsAnnotationType.SelectedItem, txtInput, SelectedChart)
                    AnnotationsUpdateFromChart(SelectedChart)
                    ComboAnnotationsList.FindStringExact(txtInput)
                    SelectedAnnotation = SelectedChart.Annotations(ComboAnnotationsList.SelectedIndex)
                Case Else
            End Select
        Catch ex As Exception
#If DEBUG Then
            Debug.WriteLine($"{sender}: {ex.Message}")
#Else
            FileLogger.LogException(ex)
#End If
        End Try
    End Sub

    Private Sub CmdItemRemove_Click(sender As Object, e As EventArgs) Handles CmdTitlesRemove.Click,
        CmdChartAreasRemove.Click,
        CmdSeriesRemove.Click,
        CmdLegendsRemove.Click,
        CmdAnnotationsRemove.Click
        Try
            Dim cmd As Button = DirectCast(sender, Button)
            Dim prompt As String = ""
            Select Case cmd.Name
                Case "CmdTitlesRemove"
                    prompt = String.Format(STR_PROMPT_DELETE, "title", ComboTitlesList.SelectedItem)
                Case "CmdChartAreasRemove"
                    prompt = String.Format(STR_PROMPT_REMOVE, "chart area", ComboChartAreasList.SelectedItem)
                Case "CmdSeriesRemove"
                    prompt = String.Format(STR_PROMPT_REMOVE, "series", ComboSeriesList.SelectedItem)
                Case "CmdLegendsRemove"
                    prompt = String.Format(STR_PROMPT_REMOVE, "legend", ComboLegendsList.SelectedItem)
                Case "CmdAnnotationsRemove"
                    prompt = String.Format(STR_PROMPT_REMOVE, "annotation", ComboAnnotationsList.SelectedItem)
                Case Else
            End Select

            Dim result As DialogResult = MessageBox.Show(
                prompt,
                "Remove Item from List",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            )
            If result = DialogResult.Yes Then
                Select Case cmd.Name
                    Case "CmdTitlesRemove"
                        SelectedChart.Titles.Remove(SelectedTitle)
                        TitlesUpdateFromChart(SelectedChart)
                        SelectedTitle = If(Me.ComboTitlesList.SelectedIndex <> -1, SelectedChart.Titles(Me.ComboTitlesList.SelectedIndex), Nothing)
                    Case "CmdChartAreasRemove"
                        SelectedChart.ChartAreas.Remove(SelectedChartArea)
                        ChartAreasUpdateFromChart(SelectedChart)
                        SelectedChartArea = ComboChartAreasList.SelectedItem
                        SelectedChartArea = If(Me.ComboChartAreasList.SelectedIndex <> -1, SelectedChart.ChartAreas(Me.ComboChartAreasList.SelectedIndex), Nothing)
                    Case "CmdSeriesRemove"
                        SelectedChart.Series.Remove(SelectedSeries)
                        SeriesUpdateFromChart(SelectedChart)
                        SelectedSeries = If(Me.ComboSeriesList.SelectedIndex <> -1, SelectedChart.Series(Me.ComboSeriesList.SelectedIndex), Nothing)
                    Case "CmdLegendsRemove"
                        SelectedChart.Legends.Remove(SelectedLegend)
                        LegendsUpdateFromChart(SelectedChart)
                        SelectedLegend = If(Me.ComboLegendsList.SelectedIndex <> -1, SelectedChart.Legends(Me.ComboLegendsList.SelectedIndex), Nothing)
                    Case "CmdAnnotationsRemove"
                        SelectedChart.Annotations.Remove(SelectedAnnotation)
                        AnnotationsUpdateFromChart(SelectedChart)
                        SelectedAnnotation = If(Me.ComboAnnotationsList.SelectedIndex <> -1, SelectedChart.Annotations(Me.ComboAnnotationsList.SelectedIndex), Nothing)
                    Case Else
                End Select
            End If
        Catch ex As Exception
#If DEBUG Then
            Debug.WriteLine($"{sender}: {ex.Message}")
#Else
            FileLogger.LogException(ex)
#End If
        End Try
    End Sub

    Private Sub CmdOK_Click(sender As Object, e As EventArgs) Handles CmdOK.Click
        Me.Close()
    End Sub

    Private Sub ComboAreaAlignmentOrientation_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboChartAreaAlignmentOrientation.SelectionChangeCommitted
        Try
            Dim combo As ComboChartAreaAlignmentOrientationsPicker = DirectCast(sender, ComboChartAreaAlignmentOrientationsPicker)

            Select Case combo.Name
                Case "ComboChartAreaAlignmentOrientation"
                    SelectedChartArea.AlignmentOrientation = combo.Orientation
                Case Else
            End Select
        Catch ex As Exception
#If DEBUG Then
            Debug.WriteLine($"{sender}: {ex.Message}")
#Else
            FileLogger.LogException(ex)
#End If
        End Try
    End Sub

    Private Sub ComboBackGradientStyle_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboChartBackGradientStyle.SelectionChangeCommitted, ComboChartBorderSkinBackGradientStyle.SelectionChangeCommitted,
        ComboTitleBackGradientStyle.SelectionChangeCommitted,
        ComboChartAreaBackGradientStyle.SelectionChangeCommitted,
        ComboAnnotationsBackGradientStyle.SelectionChangeCommitted
        Try
            Dim combo As ComboChartGradientPicker = DirectCast(sender, ComboChartGradientPicker)

            Select Case combo.Name
                Case "ComboChartBackGradientStyle"
                    SelectedChart.BackGradientStyle = combo.GradientStyle
                Case "ComboChartBorderSkinBackGradientStyle"
                    SelectedChart.BorderSkin.BackGradientStyle = combo.GradientStyle
                Case "ComboTitleBackGradientStyle"
                    SelectedTitle.BackGradientStyle = combo.GradientStyle
                Case "ComboChartAreaBackGradientStyle"
                    SelectedChartArea.BackGradientStyle = combo.GradientStyle
                Case "ComboAnnotationsBackGradientStyle"
                    SelectedAnnotation.BackGradientStyle = combo.GradientStyle
                Case Else
            End Select
        Catch ex As Exception
#If DEBUG Then
            Debug.WriteLine($"{sender}: {ex.Message}")
#Else
            FileLogger.LogException(ex)
#End If
        End Try
    End Sub

    Private Sub ComboBackHatchStyle_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboChartBorderSkinBackHatchStyle.SelectionChangeCommitted,
        ComboTitleBackHatchStyle.SelectionChangeCommitted,
        ComboChartAreaBackHatchStyle.SelectionChangeCommitted,
        ComboSeriesBackHatchStyle.SelectionChangeCommitted,
        ComboAnnotationsBackHatchStyle.SelectionChangeCommitted
        Try
            Dim combo As ComboChartHatchStylePicker = DirectCast(sender, ComboChartHatchStylePicker)

            Select Case combo.Name
                Case "ComboChartBorderSkinBackHatchStyle"
                    SelectedChart.BorderSkin.BackHatchStyle = combo.HatchStyle
                Case "ComboTitleBackHatchStyle"
                    SelectedTitle.BackHatchStyle = combo.HatchStyle
                Case "ComboChartAreaBackHatchStyle"
                    SelectedChartArea.BackHatchStyle = combo.HatchStyle
                Case "ComboSeriesBackHatchStyle"
                    SelectedSeries.BackHatchStyle = combo.HatchStyle
                Case "ComboAnnotationsHatchStylePicker"
                    SelectedAnnotation.BackHatchStyle = combo.HatchStyle
                Case Else
            End Select
        Catch ex As Exception
#If DEBUG Then
            Debug.WriteLine($"{sender}: {ex.Message}")
#Else
            FileLogger.LogException(ex)
#End If
        End Try
    End Sub

    Private Sub ComboContentAlignment_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboTitleContentAlignment.SelectionChangeCommitted,
        ComboAnnotationsAlignment.SelectionChangeCommitted
        Try
            Dim combo As ComboContentAlignmentPicker = DirectCast(sender, ComboContentAlignmentPicker)

            Select Case combo.Name
                Case "ComboTitleContentAlignment"
                    SelectedTitle.Alignment = combo.Alignment
                Case "ComboAnnotationsAlignment"
                    SelectedAnnotation.Alignment = combo.Alignment
                Case Else
            End Select
        Catch ex As Exception
#If DEBUG Then
            Debug.WriteLine($"{sender}: {ex.Message}")
#Else
            FileLogger.LogException(ex)
#End If
        End Try
    End Sub

    Private Sub ComboDashStyle_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboChartBorderDashStyle.SelectionChangeCommitted, ComboChartBorderlineDashStyle.SelectionChangeCommitted,
        ComboTitleBorderDashStyle.SelectionChangeCommitted,
        ComboChartAreaBorderDashStyle.SelectionChangeCommitted,
        ComboAxesMajorGridDashStyle.SelectionChangeCommitted, ComboAxesMinorGridDashStyle.SelectionChangeCommitted, ComboAxesAxisLineDashStyle.SelectionChangeCommitted,
        ComboSeriesBorderDashStyle.SelectionChangeCommitted,
        ComboLabelsMarkersLabelBorderDashStyle.SelectionChangeCommitted,
        ComboLegendsBorderDashStyle.SelectionChangeCommitted
        Try
            Dim combo = DirectCast(sender, ComboChartDashStylePicker)

            Select Case combo.Name
                Case "ComboChartBorderDashStyle"
                    SelectedChart.BorderlineDashStyle = combo.DashStyle
                Case "ComboChartBorderlineDashStyle"
                    SelectedChart.BorderlineDashStyle = combo.DashStyle
                Case "ComboTitleBorderDashStyle"
                    SelectedTitle.BorderDashStyle = combo.DashStyle
                Case "ComboChartAreaBorderDashStyle"
                    SelectedChartArea.BorderDashStyle = combo.DashStyle
                Case "ComboSeriesBorderDashStyle"
                    SelectedSeries.BorderDashStyle = combo.DashStyle
                Case "ComboLabelsMarkersLabelBorderDashStyle"
                    SelectedSeries.LabelBorderDashStyle = combo.DashStyle
                Case "ComboLegendsBorderDashStyle"
                    SelectedLegend.BorderDashStyle = combo.DashStyle
                Case "ComboAxesMajorGridDashStyle"
                    SelectedAxis.MajorGrid.LineDashStyle = combo.DashStyle
                Case "ComboAxesMinorGridDashStyle"
                    SelectedAxis.MinorGrid.LineDashStyle = combo.DashStyle
                Case Else
            End Select
        Catch ex As Exception
#If DEBUG Then
            Debug.WriteLine($"{sender}: {ex.Message}")
#Else
            FileLogger.LogException(ex)
#End If
        End Try
    End Sub

    Private Sub ComboChart_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboChartAntiAliasing.SelectionChangeCommitted, ComboChartTextAntiAliasingQuality.SelectionChangeCommitted
        Try
            Dim combo As ComboBox = DirectCast(sender, ComboBox)

            Select Case combo.Name
                Case "ComboChartAntiAliasing"
                    SelectedChart.AntiAliasing = CType(DirectCast(combo, ComboChartAntiAliasingPicker).AntiAliasingStyle, AntiAliasingStyles)
                Case "ComboChartTextAntiAliasingQuality"
                    SelectedChart.TextAntiAliasingQuality = CType(DirectCast(combo, ComboChartTextAntiAliasingQualityPicker).AntiAliasingQuality, TextAntiAliasingQuality)
                Case Else
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ComboColor_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboChartBackColor.SelectionChangeCommitted, ComboChartBackImageTransparentColor.SelectionChangeCommitted, ComboChartBorderColor.SelectionChangeCommitted, ComboChartBorderlineColor.SelectionChangeCommitted, ComboChartBorderSkinBorderColor.SelectionChangeCommitted, ComboChartBorderSkinPageColor.SelectionChangeCommitted, ComboChartBorderSkinBackColor.SelectionChangeCommitted, ComboChartBorderSkinBackSecondaryColor.SelectionChangeCommitted, ComboChartForeColor.SelectionChangeCommitted,
        ComboChartAreaBackColor.SelectionChangeCommitted, ComboChartAreaBorderColor.SelectionChangeCommitted, ComboChartAreaShadowColor.SelectionChangeCommitted,
        ComboAxesAxisLineColor.SelectionChangeCommitted, ComboAxesTitleForeColor.SelectionChangeCommitted, ComboAxesLabelsForeColor.SelectionChangeCommitted, ComboAxesInterlacingInterlacedColor.SelectionChangeCommitted,
        ComboSeriesBorderColor.SelectionChangeCommitted, ComboSeriesColor.SelectionChangeCommitted, ComboSeriesShadowColor.SelectionChangeCommitted,
        ComboLabelsMarkersLabelBackColor.SelectionChangeCommitted, ComboLabelsMarkersLabelBorderColor.SelectionChangeCommitted, ComboLabelsMarkersMarkerBorderColor.SelectionChangeCommitted, ComboLabelsMarkersMarkerColor.SelectionChangeCommitted,
        ComboLegendsBorderColor.SelectionChangeCommitted, ComboLegendsForeColor.SelectionChangeCommitted, ComboLegendsHeaderSeparatorColor.SelectionChangeCommitted, ComboLegendsItemColumnSepColor.SelectionChangeCommitted, ComboLegendsShadowColor.SelectionChangeCommitted, ComboLegendsTitleBackColor.SelectionChangeCommitted, ComboLegendsTitleForeColor.SelectionChangeCommitted, ComboLegendsTitleSeparatorColor.SelectionChangeCommitted,
        ComboAnnotationsBackColor.SelectionChangeCommitted, ComboAnnotationsForeColor.SelectionChangeCommitted, ComboAnnotationsLineColor.SelectionChangeCommitted, ComboAnnotationsShadowColor.SelectionChangeCommitted, ComboAxesMajorGridLineColor.SelectionChangeCommitted, ComboAxesMinorGridLineColor.SelectionChangeCommitted
        Try
            Dim combo = DirectCast(sender, ComboColorPicker)

            Select Case combo.Name
                Case "ComboChartBackColor"
                    SelectedChart.BackColor = combo.Color
                Case "ComboChartBackImageTransparentColor"
                    SelectedChart.BackImageTransparentColor = combo.Color
                Case "ComboChartBorderColor"
                    SelectedChart.BorderColor = combo.Color
                Case "ComboChartBorderlineColor"
                    SelectedChart.BorderlineColor = combo.Color
                Case "ComboChartBorderSkinBorderColor"
                    SelectedChart.BorderSkin.BorderColor = combo.Color
                Case "ComboChartBorderSkinPageColor"
                    SelectedChart.BorderSkin.PageColor = combo.Color
                Case "ComboChartBorderSkinBackColor"
                    SelectedChart.BorderSkin.BackColor = combo.Color
                Case "ComboChartBorderSkinBackSecondaryColor"
                    SelectedChart.BorderSkin.BackSecondaryColor = combo.Color
                Case "ComboChartForeColor"
                    SelectedChart.ForeColor = combo.Color
                Case "ComboTitleBackColor"
                    SelectedTitle.BackColor = combo.Color
                Case "ComboTitleBorderColor"
                    SelectedTitle.BorderColor = combo.Color
                Case "ComboTitleForeColor"
                    SelectedTitle.ForeColor = combo.Color
                Case "ComboTitleShadowColor"
                    SelectedTitle.ShadowColor = combo.Color
                Case "ComboChartAreaBackColor"
                    SelectedChartArea.BackColor = combo.Color
                Case "ComboChartAreaBorderColor"
                    SelectedChartArea.BorderColor = combo.Color
                Case "ComboChartAreaShadowColor"
                    SelectedChartArea.ShadowColor = combo.Color
                Case "ComboAxesAxisLineColor"
                    SelectedAxis.LineColor = combo.Color
                Case "ComboAxesMajorGridLineColor"
                    SelectedAxis.MajorGrid.LineColor = combo.Color
                Case "ComboAxesMinorGridLineColor"
                    SelectedAxis.MinorGrid.LineColor = combo.Color
                Case "ComboAxesLabelsForeColor"
                    SelectedAxis.LabelStyle.ForeColor = combo.Color
                Case "ComboAxesTitleForeColor"
                    SelectedAxis.TitleForeColor = combo.Color
                Case "ComboAxesInterlacingInterlacedColor"
                    SelectedAxis.InterlacedColor = combo.Color
                Case "ComboSeriesBorderColor"
                    SelectedSeries.BorderColor = combo.Color
                Case "ComboSeriesColor"
                    SelectedSeries.Color = combo.Color
                Case "ComboSeriesShadowColor"
                    SelectedSeries.ShadowColor = combo.Color
                Case "ComboLabelsMarkersLabelBackColor"
                    SelectedSeries.LabelBackColor = combo.Color
                Case "ComboLabelsMarkersLabelBorderColor"
                    SelectedSeries.LabelBorderColor = combo.Color
                Case "ComboLabelsMarkersMarkerBorderColor"
                    SelectedSeries.MarkerBorderColor = combo.Color
                Case "ComboLabelsMarkersMarkerColor"
                    SelectedSeries.MarkerColor = combo.Color
                Case "ComboLegendsBorderColor"
                    SelectedLegend.BorderColor = combo.Color
                Case "ComboLegendsForeColor"
                    SelectedLegend.ForeColor = combo.Color
                Case "ComboLegendsHeaderSeparatorColor"
                    SelectedLegend.HeaderSeparatorColor = combo.Color
                Case "ComboLegendsItemColumnSepColor"
                    SelectedLegend.ItemColumnSeparatorColor = combo.Color
                Case "ComboLegendsShadowColor"
                    SelectedLegend.ShadowColor = combo.Color
                Case "ComboLegendsTitleBackColor"
                    SelectedLegend.TitleBackColor = combo.Color
                Case "ComboLegendsTitleForeColor"
                    SelectedLegend.TitleForeColor = combo.Color
                Case "ComboLegendsTitleSeparatorColor"
                    SelectedLegend.TitleSeparatorColor = combo.Color
                Case "ComboAnnotationsBackColor"
                    SelectedAnnotation.BackColor = combo.Color
                Case "ComboAnnotationsForeColor"
                    SelectedAnnotation.ForeColor = combo.Color
                Case "ComboAnnotationsLineColor"
                    SelectedAnnotation.LineColor = combo.Color
                Case "ComboAnnotationsShadowColor"
                    SelectedAnnotation.ShadowColor = combo.Color
                Case Else
            End Select
        Catch ex As Exception
#If DEBUG Then
            Debug.WriteLine($"{sender}: {ex.Message}")
#Else
            FileLogger.LogException(ex)
#End If
        End Try
    End Sub

    Private Sub ComboElementPosition_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboLegendsPosition.SelectionChangeCommitted
        Try
            Dim combo As ComboLegendElementPositionPicker = DirectCast(sender, ComboLegendElementPositionPicker)

            Select Case combo.Name
                Case "ComboLegendsPosition"
                    SelectedLegend.Position = combo.ElementPosition
                Case Else
            End Select
        Catch ex As Exception
#If DEBUG Then
            Debug.WriteLine($"{sender}: {ex.Message}")
#Else
            FileLogger.LogException(ex)
#End If
        End Try
    End Sub

    Private Sub ComboItemType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboAnnotationsAnnotationType.SelectedIndexChanged,
        ComboLegendsLegendStyle.SelectedIndexChanged,
        ComboSeriesChartType.SelectedIndexChanged
        Try
            Dim combo As ComboBox = DirectCast(sender, ComboBox)

            Select Case combo.Name
                Case "ComboAnnotationsAnnotationType"
                    CmdAnnotationsAddNew.Enabled = (combo.SelectedIndex <> -1)
                Case "ComboLegendsLegendStyle"
                    CmdLegendsAddNew.Enabled = (combo.SelectedIndex <> -1)
                Case "ComboSeriesChartType"
                    CmdSeriesAddNew.Enabled = (combo.SelectedIndex <> -1)
                Case Else
            End Select
        Catch ex As Exception
#If DEBUG Then
            Debug.WriteLine($"{sender}: {ex.Message}")
#Else
            FileLogger.LogException(ex)
#End If
        End Try
    End Sub

    Private Sub ComboItemType_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboSeriesChartType.SelectionChangeCommitted, ComboLegendsLegendStyle.SelectionChangeCommitted, ComboAnnotationsAnnotationType.SelectionChangeCommitted
        Try
            Dim combo As ComboBox = DirectCast(sender, ComboBox)

            Select Case combo.Name
                Case "ComboSeriesChartType"
                    SelectedSeries.ChartType = CType(combo.SelectedItem, SeriesChartType)
                Case "ComboLegendsLegendStyle"
                    SelectedLegend.LegendStyle = CType(combo.SelectedItem, LegendStyle)
                Case "ComboAnnotationsAnnotationType"
                    If SelectedAnnotation IsNot Nothing Then SelectedAnnotation = AnnotationsAddNew(CType(combo.SelectedItem, ChartAnnotationType), SelectedAnnotation.Name)
                Case Else
            End Select
        Catch ex As Exception
#If DEBUG Then
            Debug.WriteLine($"{sender}: {ex.Message}")
#Else
            FileLogger.LogException(ex)
#End If
        End Try
    End Sub

    Private Sub ComboList_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboTitlesList.SelectionChangeCommitted,
        ComboChartAreasList.SelectionChangeCommitted,
        ComboAxesAxis.SelectionChangeCommitted,
        ComboSeriesList.SelectionChangeCommitted,
        ComboLegendsList.SelectionChangeCommitted,
        ComboAnnotationsList.SelectionChangeCommitted
        Try
            Dim combo As ComboBox = DirectCast(sender, ComboBox)

            Select Case combo.Name
                Case "ComboSeriesChartArea"
                    SelectedSeries.ChartArea = combo.SelectedItem
                Case "ComboTitlesList"
                    SelectedTitle = SelectedChart.Titles(combo.SelectedIndex)
                Case "ComboChartAreasList"
                    SelectedChartArea = SelectedChart.ChartAreas(combo.SelectedIndex)
                Case "ComboAxesAxis"
                    SelectedAxis = SelectedChartArea.Axes(combo.SelectedIndex)
                Case "ComboSeriesList"
                    SelectedSeries = SelectedChart.Series(combo.SelectedIndex)
                Case "ComboLegendsList"
                    SelectedLegend = SelectedChart.Legends(combo.SelectedIndex)
                Case "ComboAnnotationsList"
                    SelectedAnnotation = SelectedChart.Annotations(combo.SelectedIndex)
                Case Else
            End Select
        Catch ex As Exception
#If DEBUG Then
            Debug.WriteLine($"{sender}: {ex.Message}")
#Else
            FileLogger.LogException(ex)
#End If
        End Try
    End Sub

    Private Sub ComboList_Validating(sender As Object, e As CancelEventArgs) Handles ComboTitlesList.Validating, ComboSeriesChartArea.Validating, ComboChartAreasList.Validating, ComboSeriesList.Validating, ComboAnnotationsList.Validating, ComboLegendsList.Validating
        Try
            Dim combo As ComboBox = DirectCast(sender, ComboBox)
            Dim txtInput As String = combo.Text.Trim()

            If String.IsNullOrEmpty(txtInput) OrElse SelectedChart Is Nothing Then Return

            Dim index As Integer = combo.FindStringExact(txtInput)

            If index = -1 Then
                Dim result As DialogResult = MessageBox.Show(
                    $"'{txtInput}' not found in the list. Would you like to add it?",
                    "Add Item to List",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                )

                If result = DialogResult.Yes Then
                    Select Case combo.Name
                        Case "ComboTitlesList"
                            SelectedChart.Titles.Add(txtInput)
                            TitlesUpdateFromChart(SelectedChart)
                            combo.SelectedIndex = combo.FindStringExact(txtInput)
                            SelectedTitle = SelectedChart.Titles(combo.SelectedIndex)
                        Case "ComboChartAreasList"
                            SelectedChart.ChartAreas.Add(txtInput)
                            ChartAreasUpdateFromChart(SelectedChart)
                            combo.SelectedIndex = combo.FindStringExact(txtInput)
                            SelectedChartArea = SelectedChart.ChartAreas(combo.SelectedIndex)
                        Case "ComboSeriesList"
                            SelectedChart.Series.Add(txtInput)
                            SeriesUpdateFromChart(SelectedChart)
                            combo.SelectedIndex = combo.FindStringExact(txtInput)
                            SelectedSeries = SelectedChart.Series(combo.SelectedIndex)
                        Case "ComboLegendsList"
                            SelectedChart.Legends.Add(txtInput)
                            LegendsUpdateFromChart(SelectedChart)
                            combo.SelectedIndex = combo.FindStringExact(txtInput)
                            SelectedLegend = SelectedChart.Legends(combo.SelectedIndex)
                        Case "ComboAnnotationsList"
                            Dim newAnnotation As Annotation = AnnotationsAddNew(ComboAnnotationsAnnotationType.SelectedItem, txtInput, SelectedChart)
                            AnnotationsUpdateFromChart(SelectedChart)
                            combo.SelectedIndex = combo.FindStringExact(txtInput)
                            SelectedAnnotation = SelectedChart.Annotations(combo.SelectedIndex)
                        Case Else
                    End Select
                Else
                    combo.Text = ""
                End If
            End If
        Catch ex As Exception
#If DEBUG Then
            Debug.WriteLine($"{sender}: {ex.Message}")
#Else
            FileLogger.LogException(ex)
#End If
        End Try
    End Sub

    Private Sub ComboMarkerStyle_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboLabelsMarkersMarkerStyle.SelectionChangeCommitted
        Try
            Dim combo As ComboChartMarkerStylePicker = DirectCast(sender, ComboChartMarkerStylePicker)

            Select Case combo.Name
                Case "ComboLabelsMarkersMarkerStyle"
                    SelectedSeries.MarkerStyle = combo.MarkerStyle
                Case Else
            End Select
        Catch ex As Exception
#If DEBUG Then
            Debug.WriteLine($"{sender}: {ex.Message}")
#Else
            FileLogger.LogException(ex)
#End If
        End Try
    End Sub

    Private Sub ComboPalette_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboSeriesColorPalette.SelectionChangeCommitted
        Try
            Dim combo = DirectCast(sender, ComboChartPalettePicker)

            Select Case combo.Name
                Case "ComboChartColorPalette"
                    SelectedChart.Palette = combo.ColorPalette
                Case "ComboSeriesColorPalette"
                    SelectedSeries.Palette = combo.ColorPalette
                Case Else
            End Select
        Catch ex As Exception
#If DEBUG Then
            Debug.WriteLine($"{sender}: {ex.Message}")
#Else
            FileLogger.LogException(ex)
#End If
        End Try
    End Sub

    Private Sub ComboSeparatorStylePicker_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboLegendsHeaderSeparator.SelectionChangeCommitted, ComboLegendsItemColumnSeparator.SelectionChangeCommitted, ComboLegendsTitleSeparator.SelectionChangeCommitted
        Try
            Dim combo As ComboChartLegendSeparatorStylePicker = DirectCast(sender, ComboChartLegendSeparatorStylePicker)

            Select Case combo.Name
                Case "ComboLegendsHeaderSeparator"
                    SelectedLegend.HeaderSeparator = combo.SeparatorStyle
                Case "ComboLegendsItemColumnSeparator"
                    SelectedLegend.ItemColumnSeparator = combo.SeparatorStyle
                Case "ComboLegendsTitleSeparator"
                    SelectedLegend.TitleSeparator = combo.SeparatorStyle
                Case Else
            End Select
        Catch ex As Exception
#If DEBUG Then
            Debug.WriteLine($"{sender}: {ex.Message}")
#Else
            FileLogger.LogException(ex)
#End If
        End Try
    End Sub

    Private Sub ComboStringAlignment_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboAxesTitleAlignment.SelectionChangeCommitted,
        ComboLegendStringAlignment.SelectionChangeCommitted, ComboLegendsTitleAlignment.SelectionChangeCommitted
        Try
            Dim combo As ComboLegendStringAlignmentPicker = DirectCast(sender, ComboLegendStringAlignmentPicker)

            Select Case combo.Name
                Case "ComboAxesTitleAlignment"
                    SelectedAxis.TitleAlignment = combo.Alignment
                Case "ComboLegendStringAlignment"
                    SelectedLegend.Alignment = combo.Alignment
                Case "ComboLegendsTitleAlignment"
                    SelectedLegend.TitleAlignment = combo.Alignment
                Case Else
            End Select
        Catch ex As Exception
#If DEBUG Then
            Debug.WriteLine($"{sender}: {ex.Message}")
#Else
            FileLogger.LogException(ex)
#End If
        End Try
    End Sub

    Private Sub ComboTableStyle_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboLegendsTableStyle.SelectionChangeCommitted
        Try
            Dim combo As ComboChartLegendTableStylePicker = DirectCast(sender, ComboChartLegendTableStylePicker)

            Select Case combo.Name
                Case "ComboLegendsTableStyle"
                    SelectedLegend.TableStyle = combo.TableStyle
                Case Else
            End Select
        Catch ex As Exception
#If DEBUG Then
            Debug.WriteLine($"{sender}: {ex.Message}")
#Else
            FileLogger.LogException(ex)
#End If
        End Try
    End Sub

    Private Sub ComboTextOrientation_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboTitleTextOrientation.SelectionChangeCommitted,
        ComboAxesTitleTextOrientation.SelectionChangeCommitted
        Try
            Dim combo As ComboReportingTextOrientationPicker = DirectCast(sender, ComboReportingTextOrientationPicker)

            Select Case combo.Name
                Case "ComboTitleTextOrientation"
                    SelectedTitle.TextOrientation = combo.OrientationName
                Case "ComboAxesTitleTextOrientation"
                    SelectedAxis.TextOrientation = combo.OrientationName
                Case Else
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ComboTickMarkStyle_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboAxesMajorTickMarkStyle.SelectionChangeCommitted, ComboAxesMinorTickMarkStyle.SelectionChangeCommitted
        Try
            Dim combo As ComboChartTickMarkStylePicker = DirectCast(sender, ComboChartTickMarkStylePicker)

            Select Case combo.Name
                Case "ComboAxesMajorTickMarkStyle"
                    SelectedAxis.MajorTickMark.TickMarkStyle = combo.TickMarkStyle
                Case "ComboAxesMinorTickMarkStyle"
                    SelectedAxis.MinorTickMark.TickMarkStyle = combo.TickMarkStyle
                Case Else
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ListCharts_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            Dim myList As ListBox = DirectCast(sender, ListBox)

            ChartSelect(Me.Charts(myList.SelectedIndex))
        Catch ex As Exception
#If DEBUG Then
            Debug.WriteLine($"{sender}: {ex.Message}")
#Else
            FileLogger.LogException(ex)
#End If
        End Try
    End Sub

    Private Sub Numeric_KeyDown(sender As Object, e As KeyEventArgs) Handles NumericChartBorderlineWidth.KeyDown, NumericChartBorderSkinBorderWidth.KeyDown,
        NumericTitleBorderWidth.KeyDown, NumericTitleShadowOffset.KeyDown,
        NumericChartAreaBorderWidth.KeyDown, NumericChartAreaShadowOffset.KeyDown,
        NumericAxesAxisLineWidth.KeyDown,
        NumericSeriesBorderWidth.KeyDown, NumericSeriesShadowOffset.KeyDown,
        NumericLabelsMarkersLabelBorderWidth.KeyDown, NumericLabelsMarkersMarkerBorderWidth.KeyDown, NumericLabelsMarkersMarkerSize.KeyDown, NumericLabelsMarkersMarkerStep.KeyDown,
        NumericLegendsBorderWidth.KeyDown, NumericLegendsShadowOffset.KeyDown, NumericLegendsTextWrapThreshold.KeyDown, NumericLegendsItemColumnSpacing.KeyDown,
        NumericAnnotationsLineWidth.KeyDown, NumericAnnotationsShadowOffset.KeyDown
        '
        ' We only want user-initiated numeric value changes to take effect.
        ' Check for {ENTER} and set flag. This routine handles all NumericUpDowns.
        '
        If e.KeyCode = Keys.Enter Then
            ' Prevent the default beep sound.
            e.SuppressKeyPress = True

            mUserInput = True
        End If
    End Sub

    Private Sub NumericOffsetThresholdSize_ValueChanged(sender As Object, e As EventArgs) Handles NumericTitleShadowOffset.ValueChanged,
        NumericChartAreaShadowOffset.ValueChanged,
        NumericSeriesShadowOffset.ValueChanged,
        NumericLabelsMarkersMarkerSize.ValueChanged, NumericLabelsMarkersMarkerStep.ValueChanged,
        NumericLegendsShadowOffset.ValueChanged, NumericLegendsTextWrapThreshold.ValueChanged,
        NumericAnnotationsShadowOffset.ValueChanged
        Try
            Dim num As NumericUpDown = DirectCast(sender, NumericUpDown)

            If Control.MouseButtons = MouseButtons.Left OrElse mUserInput Then
                Select Case num.Name
                    Case "NumericTitleShadowOffset"
                        SelectedTitle.ShadowOffset = num.Value
                    Case "NumericChartAreaShadowOffset"
                        SelectedChartArea.ShadowOffset = num.Value
                    Case "NumericSeriesShadowOffset"
                        SelectedSeries.ShadowOffset = num.Value
                    Case "NumericLabelsMarkersMarkerSize"
                        SelectedSeries.MarkerSize = num.Value
                    Case "NumericLabelsMarkersMarkerStep"
                        SelectedSeries.MarkerStep = num.Value
                    Case "NumericLegendsShadowOffset"
                        SelectedLegend.ShadowOffset = num.Value
                    Case "NumericLegendsTextWrapThreshold"
                        SelectedLegend.TextWrapThreshold = num.Value
                    Case "NumericAnnotationsShadowOffset"
                        SelectedAnnotation.ShadowOffset = num.Value
                    Case Else
                End Select

                mUserInput = False
            End If
        Catch ex As Exception
#If DEBUG Then
            Debug.WriteLine($"{sender}: {ex.Message}")
#Else
            FileLogger.LogException(ex)
#End If
        End Try
    End Sub

    Private Sub NumericAutoFit_ValueChanged(sender As Object, e As EventArgs)
        Try
            Dim num = DirectCast(sender, NumericUpDown)

            Select Case num.Name
                Case "NumericAxesAutoFitMaxFontSize"
                    SelectedAxis.LabelAutoFitMaxFontSize = num.Value
                Case "NumericAxesAutoFitMinFontSize"
                    SelectedAxis.LabelAutoFitMinFontSize = num.Value
                Case Else
            End Select
        Catch ex As Exception
#If DEBUG Then
            Debug.WriteLine($"{sender}: {ex.Message}")
#Else
            FileLogger.LogException(ex)
#End If
        End Try
    End Sub

    Private Sub NumericHeightWidth_ValueChanged(sender As Object, e As EventArgs) Handles NumericChartBorderlineWidth.ValueChanged, NumericChartBorderSkinBorderWidth.ValueChanged,
        NumericTitleBorderWidth.ValueChanged,
        NumericChartAreaBorderWidth.ValueChanged,
        NumericAxesAxisLineWidth.ValueChanged,
        NumericSeriesBorderWidth.ValueChanged,
        NumericLabelsMarkersLabelBorderWidth.ValueChanged, NumericLabelsMarkersMarkerBorderWidth.ValueChanged, NumericLabelsMarkersLabelAngle.ValueChanged,
        NumericLegendsBorderWidth.ValueChanged, NumericLegendsItemColumnSpacing.ValueChanged,
        NumericAnnotationsLineWidth.ValueChanged
        Try
            Dim num As NumericUpDown = DirectCast(sender, NumericUpDown)

            If Control.MouseButtons = MouseButtons.Left OrElse mUserInput Then
                Select Case num.Name
                    Case "NumericChartBorderlineWidth"
                        SelectedChart.BorderlineWidth = num.Value
                    Case "NumericChartBorderSkinBorderWidth"
                        SelectedChart.BorderSkin.BorderWidth = num.Value
                    Case "NumericTitleBorderWidth"
                        SelectedTitle.BorderWidth = num.Value
                    Case "NumericChartAreaBorderWidth"
                        SelectedChart.BorderWidth = num.Value
                    Case "NumericAxesAxisLineWidth"
                        SelectedAxis.LineWidth = num.Value
                    Case "NumericSeriesBorderWidth"
                        SelectedSeries.BorderWidth = num.Value
                    Case "NumericLabelsMarkersLabelBorderWidth"
                        SelectedSeries.LabelBorderWidth = num.Value
                    Case "NumericLabelsMarkersMarkerBorderWidth"
                        SelectedSeries.MarkerBorderWidth = num.Value
                    Case "NumericLabelsMarkersLabelAngle"
                        SelectedSeries.LabelAngle = num.Value
                    Case "NumericLegendsBorderWidth"
                        SelectedLegend.BorderWidth = num.Value
                    Case "NumericLegendsItemColumnSpacing"
                        SelectedLegend.ItemColumnSpacing = num.Value
                    Case "NumericAnnotationsLineWidth"
                        SelectedAnnotation.LineWidth = num.Value
                    Case Else
                End Select

                mUserInput = False
            End If
        Catch ex As Exception
#If DEBUG Then
            Debug.WriteLine($"{sender}: {ex.Message}")
#Else
            FileLogger.LogException(ex)
#End If
        End Try
    End Sub

    Private Sub TxtAxes_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtAxesScalingAndMathMinimum.KeyDown, TxtAxesScalingAndMathMaximum.KeyDown, TxtAxesScalingAndMathCrossing.KeyDown, TxtAxesScalingAndMathInterval.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim txt = DirectCast(sender, TextBox)

                Select Case txt.Name
                    Case "TxtAxesScalingAndMathMinimum"
                        SelectedAxis.Minimum = Double.Parse(txt.Text)
                    Case "TxtAxesScalingAndMathMaximum"
                        SelectedAxis.Minimum = Double.Parse(txt.Text)
                    Case "TxtAxesScalingAndMathCrossing"
                        SelectedAxis.Crossing = Double.Parse(txt.Text)
                    Case "TxtAxesScalingAndMathInterval"
                        SelectedAxis.Interval = Double.Parse(txt.Text)
                    Case "TxtAxesLabelsAngle"
                        ' TODO: change this to a NumericUpDown.
                        SelectedAxis.LabelStyle.Angle = Integer.Parse(txt.Text)
                    Case "TxtAxesLabelsFormat"
                        SelectedAxis.LabelStyle.Format = txt.Text
                    Case Else
                End Select
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TxtBackImage_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtChartBackImage.KeyDown,
        TxtTitleBackImage.KeyDown,
        TxtChartAreaBackImage.KeyDown,
        TxtSeriesBackImage.KeyDown,
        TxtLegendsBackImage.KeyDown,
        TxtLabelsMarkersMarkerImage.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim txt As TextBox = DirectCast(sender, TextBox)

                Select Case txt.Name
                    Case "TxtChartBackImage"
                        SelectedChart.BackImage = txt.Text
                    Case "TxtTitleBackImage"
                        SelectedTitle.BackImage = txt.Text
                    Case "TxtChartAreaBackImage"
                        SelectedChartArea.BackImage = txt.Text
                    Case "TxtSeriesBackImage"
                        SelectedSeries.BackImage = txt.Text
                    Case "TxtLegendsBackImage"
                        SelectedLegend.BackImage = txt.Text
                    Case "TxtLabelsMarkersMarkerImage"
                        SelectedSeries.MarkerImage = txt.Text
                    Case Else
                End Select

                e.SuppressKeyPress = True
            End If
        Catch ex As Exception
#If DEBUG Then
            Debug.WriteLine($"{sender}: {ex.Message}")
#Else
            FileLogger.LogException(ex)
#End If
        End Try
    End Sub

    Private Sub TxtOther_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtChartName.KeyDown, TxtChartText.KeyDown,
        TxtLegendsMaximumAutoSize.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim txt As TextBox = DirectCast(sender, TextBox)

                Select Case txt.Name
                    Case "TxtChartName"
                        SelectedChart.Name = txt.Text
                    Case "TxtChartText"
                        SelectedChart.Text = txt.Text
                    Case "TxtLegendsMaximumAutoSize"
                        Dim value As Single
                        If Single.TryParse(txt.Text, value) Then
                            SelectedLegend.MaximumAutoSize = value
                        End If
                    Case Else
                End Select

                e.SuppressKeyPress = True
            End If
        Catch ex As Exception
#If DEBUG Then
            Debug.WriteLine($"{sender}: {ex.Message}")
#Else
            FileLogger.LogException(ex)
#End If
        End Try
    End Sub

    Private Sub TxtTitleText_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtTitleText.KeyDown,
        TxtSeriesAxisLabel.KeyDown,
        TxtLabelsMarkersLabel.KeyDown,
        TxtLegendsTitle.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim txt = DirectCast(sender, TextBox)

                Select Case txt.Name
                    Case "TxtChartText"
                        SelectedChart.Text = txt.Text
                    Case "TxtTitleText"
                        SelectedTitle.Text = txt.Text
                    Case "TxtAxesTitleText"
                        SelectedAxis.Title = txt.Text
                    Case "TxtSeriesAxisLabel"
                        SelectedSeries.AxisLabel = txt.Text
                    Case "TxtLabelsMarkersLabel"
                        SelectedSeries.Label = txt.Text
                    Case "TxtLegendsTitle"
                        SelectedLegend.Title = txt.Text
                    Case Else
                End Select

                e.SuppressKeyPress = True
            End If
        Catch ex As Exception

        End Try
    End Sub
#End Region
#Region "Form"
    Private Sub FrmChartDesigner_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ListCharts.DataSource = mCharts
        ComboTitlesList.DataSource = mTitles
        ComboChartAreasList.DataSource = mChartAreas
        ComboAxesAxis.DataSource = mAxes
        ComboSeriesChartArea.DataSource = mChartAreas
        ComboSeriesList.DataSource = mSeries
        ComboLegendsList.DataSource = mLegends
        ComboAnnotationsList.DataSource = mAnnotations

        For Each ctrl As Control In Me.Controls
            ColorPickersInitialize(Me.Controls)
        Next

        SelectedChart = If(Me.Charts IsNot Nothing AndAlso Me.ListCharts.SelectedIndex <> -1, Me.Charts(Me.ListCharts.SelectedIndex), Nothing)

        AddHandler Me.ListCharts.SelectedIndexChanged, AddressOf Me.ListCharts_SelectedIndexChanged
    End Sub
#End Region
#Region "Observable Collections"
    Private Sub Annotations_ListChanged(sender As Object, e As ListChangedEventArgs) Handles mAnnotations.ListChanged
        Try
            Select Case e.ListChangedType
                Case ListChangedType.ItemAdded

                Case ListChangedType.ItemDeleted

                Case ListChangedType.Reset

            End Select
        Catch ex As Exception
#If DEBUG Then
            Debug.WriteLine($"{sender}: {ex.Message}")
#Else
            FileLogger.LogException(ex)
#End If
        End Try
    End Sub

    Private Sub Axes_ListChanged(sender As Object, e As ListChangedEventArgs) Handles mAxes.ListChanged
        Try
            Select Case e.ListChangedType
                Case ListChangedType.ItemAdded

                Case ListChangedType.ItemDeleted

                Case ListChangedType.Reset
                    SelectedAxis = SelectedChartArea?.Axes(0)

            End Select
        Catch ex As Exception
#If DEBUG Then
            Debug.WriteLine($"{sender}: {ex.Message}")
#Else
            FileLogger.LogException(ex)
#End If
        End Try
    End Sub

    Private Sub ChartAreas_ListChanged(sender As Object, e As ListChangedEventArgs) Handles mChartAreas.ListChanged
        Try
            Select Case e.ListChangedType
                Case ListChangedType.ItemAdded

                Case ListChangedType.ItemDeleted

                Case ListChangedType.Reset

            End Select
        Catch ex As Exception
#If DEBUG Then
            Debug.WriteLine($"{sender}: {ex.Message}")
#Else
            FileLogger.LogException(ex)
#End If
        End Try
    End Sub

    Private Sub Charts_ListChanged(sender As Object, e As ListChangedEventArgs) Handles mCharts.ListChanged
        Try
            Select Case e.ListChangedType
                Case ListChangedType.ItemAdded

                Case ListChangedType.ItemDeleted

                Case ListChangedType.Reset

            End Select
        Catch ex As Exception
#If DEBUG Then
            Debug.WriteLine($"{sender}: {ex.Message}")
#Else
            FileLogger.LogException(ex)
#End If
        End Try
    End Sub

    Private Sub Legends_ListChanged(sender As Object, e As ListChangedEventArgs) Handles mLegends.ListChanged
        Try
            Select Case e.ListChangedType
                Case ListChangedType.ItemAdded

                Case ListChangedType.ItemDeleted

                Case ListChangedType.Reset

            End Select
        Catch ex As Exception
            Debug.WriteLine($"{sender}: {ex.Message}")
        End Try
    End Sub

    Private Sub Series_ListChanged(sender As Object, e As ListChangedEventArgs) Handles mSeries.ListChanged
        Try
            Select Case e.ListChangedType
                Case ListChangedType.ItemAdded

                Case ListChangedType.ItemDeleted

                Case ListChangedType.Reset

            End Select
        Catch ex As Exception
#If DEBUG Then
            Debug.WriteLine($"{sender}: {ex.Message}")
#Else
            FileLogger.LogException(ex)
#End If
        End Try
    End Sub

    Private Sub Titles_ListChanged(sender As Object, e As ListChangedEventArgs) Handles mTitles.ListChanged
        Try
            Select Case e.ListChangedType
                Case ListChangedType.ItemAdded

                Case ListChangedType.ItemDeleted

                Case ListChangedType.Reset

            End Select
        Catch ex As Exception
#If DEBUG Then
            Debug.WriteLine($"{sender}: {ex.Message}")
#Else
            FileLogger.LogException(ex)
#End If
        End Try
    End Sub

    Private Sub ComboImageAlignmentStyle_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboChartBackImageAlignmentStyle.SelectionChangeCommitted,
            ComboTitleBackImageAlignmentStyle.SelectionChangeCommitted
        Try
            Dim combo = DirectCast(sender, ComboChartImageAlignmentStylePicker)

            Select Case combo.Name
                Case "ComboChartBackImageAlignmentStyle"
                    SelectedChart.BackImageAlignment = combo.AlignmentStyle
                Case "ComboTitleBackImageAlignmentStyle"
                    SelectedTitle.BackImageAlignment = combo.AlignmentStyle
                Case Else
            End Select
        Catch ex As Exception
#If DEBUG Then
            Debug.WriteLine($"{sender}: {ex.Message}")
#Else
            FileLogger.LogException(ex)
#End If
        End Try
    End Sub
#End Region
#End Region
End Class