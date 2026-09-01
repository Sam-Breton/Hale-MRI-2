Imports System.ComponentModel
Imports System.Windows.Forms.DataVisualization.Charting

Public Class ComboChartAreaAlignmentOrientationsPicker
    Inherits ComboBox

    Private Const kItemHeightDefault As Integer = 18
    Private Const kPenWidthDefault As Single = 1.0!
    Private Const kPenDrawOffsetBottom As Integer = -3
    Private Const kPenDrawOffsetBottom2 As Integer = -6
    Private Const kPenDrawOffsetMid As Integer = -3
    Private Const kPenDrawOffsetMid2 As Integer = 3
    Private Const kPenDrawOffsetTop As Integer = 3
    Private Const kPenDrawOffsetTop2 As Integer = 6
    Private Const kPreviewRectOffsetHeight As Integer = -6
    Private Const kPreviewRectOffsetX As Integer = 4
    Private Const kPreviewRectOffsetY As Integer = 3
    Private Const kPreviewRectWidthDefault As Integer = 30
    Private Const kTextBrushOffsetRight As Integer = 8

    Public Sub New()
        MyBase.New()
        InitializeComponent()

        Me.DropDownStyle = ComboBoxStyle.DropDownList
        Me.DrawMode = DrawMode.OwnerDrawFixed
        Me.ItemHeight = kItemHeightDefault

        ' CRITICAL: Only populate data when the program is actually running
        If Not Me.DesignMode AndAlso System.ComponentModel.LicenseManager.UsageMode = System.ComponentModel.LicenseUsageMode.Runtime Then
            Me.DataSource = [Enum].GetValues(GetType(AreaAlignmentOrientations))
        End If
    End Sub

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
    Public Property Orientation As AreaAlignmentOrientations
        Get
            If Me.SelectedItem IsNot Nothing Then
                Return CType(Me.SelectedItem, AreaAlignmentOrientations)
            End If
            Return AreaAlignmentOrientations.None
        End Get
        Set(value As AreaAlignmentOrientations)
            If Me.DataSource IsNot Nothing Then
                Me.SelectedItem = value
            End If
        End Set
    End Property

    Protected Overrides Sub OnDrawItem(e As DrawItemEventArgs)
        If e.Index < 0 Then Return

        Dim g As Graphics = e.Graphics
        Dim orientation As AreaAlignmentOrientations = CType(Me.Items(e.Index), AreaAlignmentOrientations)

        ' Render native Windows selection background or standard item background
        e.DrawBackground()

        ' Setup boundaries for the orientation schematic preview block
        Dim previewRect As New Rectangle(e.Bounds.X + 4, e.Bounds.Y + 3, 30, e.Bounds.Height - 6)

        If previewRect.Width > 0 AndAlso previewRect.Height > 0 Then
            ' Draw outer bounding container box matching the foreground theme color
            Using p As New Pen(e.ForeColor, 1)
                g.DrawRectangle(p, previewRect)
            End Using

            ' Draw direction layout indicators inside the icon frame
            DrawOrientationSchematic(g, previewRect, e.ForeColor, orientation)
        End If

        ' Render text label next to the preview block
        Using textBrush As New SolidBrush(e.ForeColor)
            Dim textX As Integer = previewRect.Right + 8
            Dim textY As Integer = e.Bounds.Y + ((e.Bounds.Height - g.MeasureString(orientation.ToString(), Me.Font).Height) / 2)
            g.DrawString(orientation.ToString(), Me.Font, textBrush, textX, textY)
        End Using

        ' Draws standard Windows dotted focus indicators if needed
        e.DrawFocusRectangle()
    End Sub

    ' Renders split chart area layout lines mimicking stacked/aligned panels
    Private Sub DrawOrientationSchematic(g As Graphics, rect As Rectangle, foregroundColor As Color, orientation As AreaAlignmentOrientations)
        Dim midX As Integer = rect.X + (rect.Width \ 2)
        Dim midY As Integer = rect.Y + (rect.Height \ 2)

        Using p As New Pen(foregroundColor, 1)
            Select Case orientation
                Case AreaAlignmentOrientations.None
                    ' Draw a light diagonal skip dash indicating no alignment rule is active
                    p.DashStyle = Drawing2D.DashStyle.Dot
                    g.DrawLine(p, rect.Left + 4, rect.Top + 4, rect.Right - 4, rect.Bottom - 4)

                Case AreaAlignmentOrientations.Vertical
                    ' Draws a horizontal split line to mimic top/bottom chart areas aligning vertically
                    g.DrawLine(p, rect.Left, midY, rect.Right, midY)
                    ' Draw small directional alignment anchors
                    Using b As New SolidBrush(foregroundColor)
                        g.FillPolygon(b, New Point() {New Point(midX, rect.Top + 2), New Point(midX - 3, rect.Top + 5), New Point(midX + 3, rect.Top + 5)})
                        g.FillPolygon(b, New Point() {New Point(midX, rect.Bottom - 2), New Point(midX - 3, rect.Bottom - 5), New Point(midX + 3, rect.Bottom - 5)})
                    End Using

                Case AreaAlignmentOrientations.Horizontal
                    ' Draws a vertical split line to mimic left/right chart areas aligning horizontally
                    g.DrawLine(p, midX, rect.Top, midX, rect.Bottom)
                    ' Draw small directional alignment anchors
                    Using b As New SolidBrush(foregroundColor)
                        g.FillPolygon(b, New Point() {New Point(rect.Left + 2, midY), New Point(rect.Left + 5, midY - 3), New Point(rect.Left + 5, midY + 3)})
                        g.FillPolygon(b, New Point() {New Point(rect.Right - 2, midY), New Point(rect.Right - 5, midY - 3), New Point(rect.Right - 5, midY + 3)})
                    End Using

                Case AreaAlignmentOrientations.All
                    ' Cross splitting grid indicating alignment tracking on all axes
                    g.DrawLine(p, rect.Left, midY, rect.Right, midY)
                    g.DrawLine(p, midX, rect.Top, midX, rect.Bottom)
            End Select
        End Using
    End Sub
End Class
