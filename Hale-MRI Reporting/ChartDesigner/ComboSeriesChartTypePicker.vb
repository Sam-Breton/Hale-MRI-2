Imports System.ComponentModel
Imports System.Windows.Forms.DataVisualization.Charting

Public Class ComboSeriesChartTypePicker
    Inherits ComboBox

    Public Sub New()
        MyBase.New()
        InitializeComponent()

        Me.DropDownStyle = ComboBoxStyle.DropDownList
        Me.DrawMode = DrawMode.OwnerDrawFixed
        Me.ItemHeight = 18

        ' CRITICAL: Only populate data when the program is actually running
        If Not Me.DesignMode AndAlso System.ComponentModel.LicenseManager.UsageMode = System.ComponentModel.LicenseUsageMode.Runtime Then
            Me.DataSource = [Enum].GetValues(GetType(SeriesChartType))
        End If
    End Sub

    ''' <summary>
    ''' Prevents Designer from adding code that throws run-time exception.
    ''' </summary>
    ''' <returns>Object</returns>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    <Browsable(False)>
    <Bindable(False)>
    Public Shadows Property DataSource As Object
        Get
            Return MyBase.DataSource
        End Get
        Set(value As Object)
            MyBase.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Prevents Designer from adding code that throws run-time exception.
    ''' </summary>
    ''' <returns>ObjectCollection</returns>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    <Browsable(False)>
    <Bindable(False)>
    Public Shadows ReadOnly Property Items As ObjectCollection
        Get
            Return MyBase.Items
        End Get
    End Property

    <Browsable(False)>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property ChartType As SeriesChartType
        Get
            If Me.SelectedItem IsNot Nothing Then
                Return CType(Me.SelectedItem, SeriesChartType)
            End If
            Return SeriesChartType.Column
        End Get
        Set(value As SeriesChartType)
            If Me.DataSource IsNot Nothing Then
                Me.SelectedItem = value
            End If
        End Set
    End Property

    Private Sub DrawChartTypeIcon(g As Graphics, rect As Rectangle, foregroundColor As Color, type As SeriesChartType)
        Using p As New Pen(foregroundColor, 1.5F), b As New SolidBrush(foregroundColor)
            Select Case type
                Case SeriesChartType.Line, SeriesChartType.Spline, SeriesChartType.StepLine
                    ' Draw a jagged or flowing graphic thread line
                    g.DrawLines(p, New Point() {
                        New Point(rect.X + 3, rect.Bottom - 4),
                        New Point(rect.X + 10, rect.Top + 5),
                        New Point(rect.X + 18, rect.Bottom - 10),
                        New Point(rect.Right - 3, rect.Top + 3)
                    })

                Case SeriesChartType.Column, SeriesChartType.StackedColumn, SeriesChartType.StackedColumn100
                    ' Draw small vertical bar items rising from the bottom
                    g.FillRectangle(b, rect.X + 4, rect.Bottom - 12, 4, 11)
                    g.FillRectangle(b, rect.X + 11, rect.Bottom - 7, 4, 6)
                    g.FillRectangle(b, rect.X + 18, rect.Bottom - 14, 4, 13)

                Case SeriesChartType.Bar, SeriesChartType.StackedBar, SeriesChartType.StackedBar100
                    ' Draw small horizontal bar items extending from the left edge
                    g.FillRectangle(b, rect.X + 1, rect.Top + 3, 13, 3)
                    g.FillRectangle(b, rect.X + 1, rect.Top + 8, 7, 3)
                    g.FillRectangle(b, rect.X + 1, rect.Top + 13, 19, 3)

                Case SeriesChartType.Area, SeriesChartType.StackedArea, SeriesChartType.SplineArea
                    ' Draw a filled mountain shape tracking data horizons
                    Dim pts As Point() = {
                        New Point(rect.X + 1, rect.Bottom - 1),
                        New Point(rect.X + 5, rect.Top + 6),
                        New Point(rect.X + 14, rect.Bottom - 8),
                        New Point(rect.Right - 1, rect.Top + 3),
                        New Point(rect.Right - 1, rect.Bottom - 1)
                    }
                    ' Render as semi-transparent fill to reveal theme background nuances cleanly
                    Using transBrush As New SolidBrush(Color.FromArgb(120, foregroundColor))
                        g.FillPolygon(transBrush, pts)
                    End Using
                    g.DrawLines(p, pts)

                Case SeriesChartType.Pie, SeriesChartType.Doughnut, SeriesChartType.Funnel, SeriesChartType.Pyramid
                    ' Render circular circular geometry representations
                    Dim radius As Integer = Math.Min(rect.Width, rect.Height) - 6
                    Dim cx As Integer = rect.X + ((rect.Width - radius) \ 2)
                    Dim cy As Integer = rect.Y + ((rect.Height - radius) \ 2)
                    g.DrawEllipse(p, cx, cy, radius, radius)
                    g.DrawLine(p, cx + (radius \ 2), cy + (radius \ 2), cx + radius, cy + (radius \ 2))
                    g.DrawLine(p, cx + (radius \ 2), cy + (radius \ 2), cx + (radius \ 4), cy + radius - 1)

                Case SeriesChartType.Point, SeriesChartType.FastPoint, SeriesChartType.Bubble
                    ' Scattered cluster points indicator array
                    g.FillEllipse(b, rect.X + 5, rect.Top + 4, 3, 3)
                    g.FillEllipse(b, rect.X + 18, rect.Top + 6, 4, 4)
                    g.FillEllipse(b, rect.X + 11, rect.Bottom - 7, 3, 3)
                    g.FillEllipse(b, rect.Right - 7, rect.Bottom - 5, 3, 3)

                Case Else
                    ' Fallback icon: Simple grid crosshairs for complex styles (Candlestick, Radar, Kagi, BoxPlot etc.)
                    p.DashStyle = Drawing2D.DashStyle.Dot
                    g.DrawLine(p, rect.Left + 2, rect.Top + 2, rect.Right - 2, rect.Bottom - 2)
                    g.DrawLine(p, rect.Left + 2, rect.Bottom - 2, rect.Right - 2, rect.Top + 2)
            End Select
        End Using
    End Sub

    Protected Overrides Sub OnDrawItem(e As DrawItemEventArgs)
        If e.Index < 0 Then Return

        Dim g As Graphics = e.Graphics
        Dim chartType As SeriesChartType = CType(Me.Items(e.Index), SeriesChartType)

        ' Render native Windows selection highlight or standard background colors
        e.DrawBackground()

        ' Setup boundaries for the miniature chart schematic preview block
        Dim previewRect As New Rectangle(e.Bounds.X + 4, e.Bounds.Y + 3, 30, e.Bounds.Height - 6)

        If previewRect.Width > 0 AndAlso previewRect.Height > 0 Then
            ' Draw outer structural layout border
            Using p As New Pen(e.ForeColor, 1)
                g.DrawRectangle(p, previewRect)
            End Using

            ' Render custom schematic shapes mimicking the target chart engine layout style
            DrawChartTypeIcon(g, previewRect, e.ForeColor, chartType)
        End If

        ' Render the enum label string next to the box
        Using textBrush As New SolidBrush(e.ForeColor)
            Dim textX As Integer = previewRect.Right + 8
            Dim textY As Integer = e.Bounds.Y + ((e.Bounds.Height - g.MeasureString(chartType.ToString(), Me.Font).Height) / 2)
            g.DrawString(chartType.ToString(), Me.Font, textBrush, textX, textY)
        End Using

        ' Draws standard native Windows dotted focus indicators
        e.DrawFocusRectangle()
    End Sub
End Class
