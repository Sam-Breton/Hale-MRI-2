Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms.DataVisualization.Charting

Public Class ComboChartGradientPicker
    Inherits ComboBox

    Private Const kColorBlendCount As Integer = 3
    Private Const kItemHeightDefault As Integer = 18
    Private Const kPenWidthDefault As Single = 1.0!
    Private Const kPreviewRectOffsetHeight As Integer = -6
    Private Const kPreviewRectOffsetX As Integer = 4
    Private Const kPreviewRectOffsetY As Integer = 3
    Private Const kPreviewRectWidthDefault As Integer = 30
    Private Const kTextRectOffsetRight As Integer = 8

    Public Sub New()
        MyBase.New()
        InitializeComponent()

        ' Set CombBox properties for owner-drawn items.  
        Me.DropDownStyle = ComboBoxStyle.DropDownList
        Me.DrawMode = DrawMode.OwnerDrawFixed
        Me.ItemHeight = kItemHeightDefault

        ' CRITICAL: Only populate data when the program is actually running
        If Not Me.DesignMode AndAlso System.ComponentModel.LicenseManager.UsageMode = System.ComponentModel.LicenseUsageMode.Runtime Then
            Me.DataSource = [Enum].GetValues(GetType(GradientStyle))
        End If
    End Sub

    <Browsable(False)>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property GradientStyle As GradientStyle
        Get
            If Me.SelectedItem IsNot Nothing Then
                Return CType(Me.SelectedItem, GradientStyle)
            End If
            Return GradientStyle.None
        End Get
        Set(value As GradientStyle)
            If Me.DataSource IsNot Nothing Then
                Me.SelectedItem = value
            End If
        End Set
    End Property

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


    Protected Overrides Sub OnDrawItem(e As DrawItemEventArgs)
        If e.Index < 0 Then Return

        Dim g As Graphics = e.Graphics
        Dim style As GradientStyle = CType(Me.Items(e.Index), GradientStyle)

        ' Draws standard Windows highlight or background depending on item state.
        e.DrawBackground()

        ' Calculate bounds for the diagram box.
        Dim previewRect As New Rectangle(e.Bounds.X + kPreviewRectOffsetX, e.Bounds.Y + kPreviewRectOffsetY, kPreviewRectWidthDefault,
                                         e.Bounds.Height + kPreviewRectOffsetHeight)

        ' Render the icon using the current theme's text foreground color.
        ' This ensures icons swap to white when highlighted/selected.
        If previewRect.Width > 0 AndAlso previewRect.Height > 0 Then
            DrawPatternIcon(g, previewRect, e.ForeColor, style)
        End If

        ' Render text label next to the diagram block.
        Using textBrush As New SolidBrush(e.ForeColor)
            Dim textX As Integer = previewRect.Right + kTextRectOffsetRight
            Dim textY As Integer = e.Bounds.Y + ((e.Bounds.Height - g.MeasureString(style.ToString(), Me.Font).Height) / 2)
            g.DrawString(style.ToString(), Me.Font, textBrush, textX, textY)
        End Using

        ' Draws native Windows dotted focus line if needed.
        e.DrawFocusRectangle()
    End Sub

    ' Creates clean, monochromatic pattern icons matching System Colors.
    Private Sub DrawPatternIcon(g As Graphics, rect As Rectangle, foregroundColor As Color, style As GradientStyle)
        Dim cStart As Color = foregroundColor
        Dim cEnd As Color = Color.Transparent

        ' Draw bounding outline for the preview box using current text color.
        Using p As New Pen(foregroundColor, kPenWidthDefault)
            g.DrawRectangle(p, rect)
        End Using

        Select Case style
            Case GradientStyle.None
                ' Leave empty or draw a simple diagonal line indicating "No Gradient".
                Using p As New Pen(foregroundColor, kPenWidthDefault)
                    p.DashStyle = DashStyle.Dash
                    g.DrawLine(p, rect.Left, rect.Top, rect.Right, rect.Bottom)
                End Using

            Case GradientStyle.LeftRight
                Using b As New LinearGradientBrush(rect, cStart, cEnd, LinearGradientMode.Horizontal)
                    g.FillRectangle(b, rect)
                End Using

            Case GradientStyle.TopBottom
                Using b As New LinearGradientBrush(rect, cStart, cEnd, LinearGradientMode.Vertical)
                    g.FillRectangle(b, rect)
                End Using

            Case GradientStyle.DiagonalLeft
                Using b As New LinearGradientBrush(rect, cStart, cEnd, LinearGradientMode.ForwardDiagonal)
                    g.FillRectangle(b, rect)
                End Using

            Case GradientStyle.DiagonalRight
                Using b As New LinearGradientBrush(rect, cStart, cEnd, LinearGradientMode.BackwardDiagonal)
                    g.FillRectangle(b, rect)
                End Using

            Case GradientStyle.Center
                Using path As New GraphicsPath()
                    path.AddRectangle(rect)
                    Using b As New PathGradientBrush(path)
                        b.CenterColor = cStart
                        b.SurroundColors = New Color() {cEnd}
                        g.FillRectangle(b, rect)
                    End Using
                End Using

            Case GradientStyle.HorizontalCenter
                Using b As New LinearGradientBrush(rect, cEnd, cEnd, LinearGradientMode.Horizontal)
                    Dim cb As New ColorBlend(kColorBlendCount)
                    cb.Colors = New Color() {cEnd, cStart, cEnd}
                    cb.Positions = New Single() {0.0F, 0.5F, 1.0F}
                    b.InterpolationColors = cb
                    g.FillRectangle(b, rect)
                End Using

            Case GradientStyle.VerticalCenter
                Using b As New LinearGradientBrush(rect, cEnd, cEnd, LinearGradientMode.Vertical)
                    Dim cb As New ColorBlend(kColorBlendCount)
                    cb.Colors = New Color() {cEnd, cStart, cEnd}
                    cb.Positions = New Single() {0.0F, 0.5F, 1.0F}
                    b.InterpolationColors = cb
                    g.FillRectangle(b, rect)
                End Using
        End Select
    End Sub

End Class
