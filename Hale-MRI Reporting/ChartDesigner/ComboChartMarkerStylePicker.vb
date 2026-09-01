Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms.DataVisualization.Charting

Public Class ComboChartMarkerStylePicker
    Inherits ComboBox

    Private Const kItemHeightDefault As Integer = 21
    Private Const kPenWidthDefault As Single = 1.0!
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
            Me.DataSource = [Enum].GetValues(GetType(MarkerStyle))
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
    Public Property MarkerStyle As MarkerStyle
        Get
            If Me.SelectedItem IsNot Nothing Then
                Return CType(Me.SelectedItem, MarkerStyle)
            End If
            Return MarkerStyle.None
        End Get
        Set(value As MarkerStyle)
            If Me.DataSource IsNot Nothing Then
                Me.SelectedItem = value
            End If
        End Set
    End Property

    Protected Overrides Sub OnDrawItem(e As DrawItemEventArgs)
        ' Ensure we have a valid item to draw
        If e.Index < 0 Then Return

        ' Draw background and focus rectangle
        e.DrawBackground()

        ' Get the current MarkerStyle value
        Dim currentStyle As MarkerStyle = CType(Me.Items(e.Index), MarkerStyle)

        ' Set up drawing boundaries
        Dim iconRect As New Rectangle(e.Bounds.X + 4, e.Bounds.Y + 3, 16, 16)
        Dim textRect As New Rectangle(e.Bounds.X + 26, e.Bounds.Y, e.Bounds.Width - 26, e.Bounds.Height)

        ' Use the item text color based on selection state
        Dim textBrush As New SolidBrush(e.ForeColor)
        Dim markerBrush As New SolidBrush(If((e.State And DrawItemState.Selected) = DrawItemState.Selected, Color.White, Color.DarkBlue))
        Dim markerPen As New Pen(markerBrush, 2)

        ' Configure smooth rendering for shapes
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias

        ' Draw the appropriate preview shape based on MarkerStyle
        Select Case currentStyle
            Case MarkerStyle.Circle
                e.Graphics.FillEllipse(markerBrush, iconRect)

            Case MarkerStyle.Square
                e.Graphics.FillRectangle(markerBrush, iconRect)

            Case MarkerStyle.Diamond
                Dim points() As Point = {
                    New Point(iconRect.Left + 8, iconRect.Top),
                    New Point(iconRect.Right, iconRect.Top + 8),
                    New Point(iconRect.Left + 8, iconRect.Bottom),
                    New Point(iconRect.Left, iconRect.Top + 8)
                }
                e.Graphics.FillPolygon(markerBrush, points)

            Case MarkerStyle.Triangle
                Dim points() As Point = {
                    New Point(iconRect.Left + 8, iconRect.Top),
                    New Point(iconRect.Right, iconRect.Bottom),
                    New Point(iconRect.Left, iconRect.Bottom)
                }
                e.Graphics.FillPolygon(markerBrush, points)

            Case MarkerStyle.Cross
                e.Graphics.DrawLine(markerPen, iconRect.Left, iconRect.Top + 8, iconRect.Right, iconRect.Top + 8)
                e.Graphics.DrawLine(markerPen, iconRect.Left + 8, iconRect.Top, iconRect.Left + 8, iconRect.Bottom)

            Case MarkerStyle.Star4
                e.Graphics.DrawLine(markerPen, iconRect.Left, iconRect.Top + 8, iconRect.Right, iconRect.Top + 8)
                e.Graphics.DrawLine(markerPen, iconRect.Left + 8, iconRect.Top, iconRect.Left + 8, iconRect.Bottom)
                ' Minor diagonal cross to form a 4-point star look or coordinate points
                e.Graphics.DrawRectangle(markerPen, iconRect.Left + 6, iconRect.Top + 6, 4, 4)

            Case MarkerStyle.Star5, MarkerStyle.Star6, MarkerStyle.Star10
                ' Simplified representation for complex stars using a generic asterisk pattern
                e.Graphics.DrawLine(markerPen, iconRect.Left + 8, iconRect.Top, iconRect.Left + 8, iconRect.Bottom)
                e.Graphics.DrawLine(markerPen, iconRect.Left, iconRect.Top + 4, iconRect.Right, iconRect.Bottom - 4)
                e.Graphics.DrawLine(markerPen, iconRect.Left, iconRect.Bottom - 4, iconRect.Right, iconRect.Top + 4)

            Case MarkerStyle.None
                ' Draw a light gray boundary dash or leave empty to mean "No Marker"
                Using neutralPen As New Pen(Color.Gray, 1) With {.DashStyle = DashStyle.Dash}
                    e.Graphics.DrawRectangle(neutralPen, iconRect)
                End Using
        End Select

        ' Draw the text label next to the graphic
        Using sf As New StringFormat()
            sf.LineAlignment = StringAlignment.Center
            e.Graphics.DrawString(CurrentStyle.ToString(), e.Font, textBrush, textRect, sf)
        End Using

        ' Clean up resources
        textBrush.Dispose()
        markerBrush.Dispose()
        markerPen.Dispose()

        e.DrawFocusRectangle()
    End Sub
End Class
