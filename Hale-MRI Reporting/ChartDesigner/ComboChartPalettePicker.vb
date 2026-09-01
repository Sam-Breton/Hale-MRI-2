Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms.DataVisualization.Charting

Public Class ComboChartPalettePicker
    Inherits ComboBox

    Private Const kItemHeightDefault As Integer = 18

    Public Sub New()
        MyBase.New()
        InitializeComponent()

        Me.DropDownStyle = ComboBoxStyle.DropDownList
        Me.DrawMode = DrawMode.OwnerDrawFixed
        Me.ItemHeight = kItemHeightDefault

        ' CRITICAL: Only populate data when the program is actually running
        If Not Me.DesignMode AndAlso System.ComponentModel.LicenseManager.UsageMode = System.ComponentModel.LicenseUsageMode.Runtime Then
            Me.DataSource = [Enum].GetValues(GetType(ChartColorPalette))
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


    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    <Browsable(False)>
    Public Property ColorPalette As ChartColorPalette
        Get
            If Me.SelectedItem IsNot Nothing Then
                Return CType(Me.SelectedItem, ChartColorPalette)
            End If
            Return ChartColorPalette.None
        End Get
        Set(value As ChartColorPalette)
            If Me.DataSource IsNot Nothing Then
                Me.SelectedItem = value
            End If
        End Set
    End Property

    Protected Overrides Sub OnDrawItem(e As DrawItemEventArgs)
        If e.Index < 0 Then Return

        Dim g As Graphics = e.Graphics
        Dim palette As ChartColorPalette = CType(Me.Items(e.Index), ChartColorPalette)

        ' Draws native Windows backgrounds or highlighted selection blocks correctly
        e.DrawBackground()

        ' Setup bounds for the color swatch preview area (holds 4 colors)
        Dim previewRect As New Rectangle(e.Bounds.X + 4, e.Bounds.Y + 3, 40, e.Bounds.Height - 6)

        If previewRect.Width > 0 AndAlso previewRect.Height > 0 Then
            ' Get MS Chart's native colors for this specific palette style
            Dim colors As Color() = GetPaletteColors(palette)

            ' Draw the tiny color blocks
            DrawPaletteSwatches(g, previewRect, colors)

            ' Draw a native border outline around the swatch block matching current forecolor
            Using p As New Pen(e.ForeColor, 1)
                g.DrawRectangle(p, previewRect)
            End Using
        End If

        ' Render the enum label text next to the swatch preview
        Using textBrush As New SolidBrush(e.ForeColor)
            Dim textX As Integer = previewRect.Right + 8
            Dim textY As Integer = e.Bounds.Y + ((e.Bounds.Height - g.MeasureString(palette.ToString(), Me.Font).Height) / 2)
            g.DrawString(palette.ToString(), Me.Font, textBrush, textX, textY)
        End Using

        ' Draws standard Windows dotted focus indicators if the control has focus
        e.DrawFocusRectangle()
    End Sub

    ' Draws a horizontal line of multi-color swatches representing the palette theme
    Private Sub DrawPaletteSwatches(g As Graphics, rect As Rectangle, colors As Color())
        Dim swatchCount As Integer = colors.Length
        Dim swatchWidth As Single = CSng(rect.Width) / swatchCount

        For i As Integer = 0 To swatchCount - 1
            Dim startX As Single = rect.X + (i * swatchWidth)
            ' Ensure the last swatch fills any fractional floating-point pixels completely
            Dim currentWidth As Single = If(i = swatchCount - 1, rect.Right - startX, swatchWidth)

            Dim swatchRect As New RectangleF(startX, rect.Y, currentWidth, rect.Height)
            Using b As New SolidBrush(colors(i))
                g.FillRectangle(b, swatchRect)
            End Using
        Next
    End Sub

    ' Hardcoded color presets matching the System.Windows.Forms.DataVisualization.Charting internal engine
    Private Function GetPaletteColors(palette As ChartColorPalette) As Color()
        Select Case palette
            Case ChartColorPalette.Bright
                Return New Color() {Color.Green, Color.Blue, Color.Purple, Color.Lime}
            Case ChartColorPalette.Grayscale
                Return New Color() {Color.DarkGray, Color.Gray, Color.LightGray, Color.Silver}
            Case ChartColorPalette.Excel
                Return New Color() {Color.FromArgb(153, 153, 255), Color.FromArgb(153, 51, 102), Color.FromArgb(255, 255, 204), Color.FromArgb(204, 255, 255)}
            Case ChartColorPalette.Light
                Return New Color() {Color.Lavender, Color.LightBlue, Color.LightCyan, Color.LightGreen}
            Case ChartColorPalette.Pastel
                Return New Color() {Color.SkyBlue, Color.LightPink, Color.LightYellow, Color.LightGreen}
            Case ChartColorPalette.EarthTones
                Return New Color() {Color.Maroon, Color.Sienna, Color.SandyBrown, Color.BurlyWood}
            Case ChartColorPalette.SemiTransparent
                Return New Color() {Color.FromArgb(128, Color.Gold), Color.FromArgb(128, Color.Red), Color.FromArgb(128, Color.Blue), Color.FromArgb(128, Color.Green)}
            Case ChartColorPalette.Berry
                Return New Color() {Color.DarkMagenta, Color.Magenta, Color.MediumVioletRed, Color.Orchid}
            Case ChartColorPalette.Chocolate
                Return New Color() {Color.SaddleBrown, Color.Sienna, Color.Chocolate, Color.Peru}
            Case ChartColorPalette.Fire
                Return New Color() {Color.Red, Color.DarkOrange, Color.Orange, Color.Yellow}
            Case ChartColorPalette.SeaGreen
                Return New Color() {Color.DarkGreen, Color.SeaGreen, Color.MediumSeaGreen, Color.LightSeaGreen}
            Case ChartColorPalette.BrightPastel
                Return New Color() {Color.FromArgb(65, 140, 240), Color.FromArgb(252, 180, 65), Color.FromArgb(117, 112, 112), Color.FromArgb(242, 117, 43)}
            Case Else ' None, Custom, or fallback
                ' Draw a fallback grayscale split or window system-based accent array
                Return New Color() {SystemColors.ControlDark, SystemColors.ControlDarkDark, SystemColors.ControlLight, SystemColors.ControlLightLight}
        End Select
    End Function
End Class
