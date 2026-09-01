Imports System.ComponentModel
Imports System.Windows.Forms.DataVisualization.Charting

Public Class ComboChartDashStylePicker
    Inherits ComboBox

    Private Const kItemHeightDefault As Integer = 18
    Private Const kPenWidthDefault As Single = 2.0!
    Private Const kPreviewRectOffsetHeight As Integer = -6
    Private Const kPreviewRectOffsetX As Integer = 4
    Private Const kPreviewRectOffsetY As Integer = 3
    Private Const kPreviewRectWidthDefault As Integer = 40

    Public Sub New()
        MyBase.New()
        InitializeComponent()

        Me.DropDownStyle = ComboBoxStyle.DropDownList
        Me.DrawMode = DrawMode.OwnerDrawFixed
        Me.ItemHeight = kItemHeightDefault

        ' CRITICAL: Only populate data when the program is actually running
        If Not Me.DesignMode AndAlso System.ComponentModel.LicenseManager.UsageMode = System.ComponentModel.LicenseUsageMode.Runtime Then
            Me.DataSource = [Enum].GetValues(GetType(ChartDashStyle))
        End If
    End Sub

    <Browsable(False)>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property DashStyle As ChartDashStyle
        Get
            If Me.SelectedItem IsNot Nothing Then
                Return CType(Me.SelectedItem, ChartDashStyle)
            End If
            Return ChartDashStyle.NotSet
        End Get
        Set(value As ChartDashStyle)
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
        Dim style As ChartDashStyle = CType(Me.Items(e.Index), ChartDashStyle)

        ' Draws standard Windows highlight or background depending on item state
        e.DrawBackground()

        ' Calculate bounds for the pattern line preview area
        Dim previewRect As New Rectangle(e.Bounds.X + kPreviewRectOffsetX, e.Bounds.Y + kPreviewRectOffsetY,
                                         kPreviewRectWidthDefault, e.Bounds.Height + kPreviewRectOffsetHeight)

        ' Render the line preview using the current theme's text foreground color
        If previewRect.Width > 0 AndAlso previewRect.Height > 0 Then
            DrawDashStylePreview(g, previewRect, e.ForeColor, style)
        End If

        ' Render text label next to the line preview area
        Using textBrush As New SolidBrush(e.ForeColor)
            Dim textX As Integer = previewRect.Right + 8
            Dim textY As Integer = e.Bounds.Y + ((e.Bounds.Height - g.MeasureString(style.ToString(), Me.Font).Height) / 2)
            g.DrawString(style.ToString(), Me.Font, textBrush, textX, textY)
        End Using

        ' Draws native Windows dotted focus line if needed
        e.DrawFocusRectangle()
    End Sub

    ' Maps ChartDashStyle to GDI+ Pen DashStyles and renders a preview line
    Private Sub DrawDashStylePreview(g As Graphics, rect As Rectangle, foregroundColor As Color, style As ChartDashStyle)
        ' Calculate the vertical center point of the preview rectangle
        Dim midY As Integer = rect.Y + (rect.Height \ 2)

        Select Case style
            Case ChartDashStyle.NotSet
                ' Draw a faint crossed line or leave blank to show it is unset
                Using p As New Pen(Color.FromArgb(128, foregroundColor), 1)
                    p.DashStyle = Drawing2D.DashStyle.Dot
                    g.DrawLine(p, rect.Left, rect.Top + 2, rect.Right, rect.Bottom - 2)
                End Using

            Case ChartDashStyle.Dash
                Using p As New Pen(foregroundColor, kPenWidthDefault)
                    p.DashStyle = Drawing2D.DashStyle.Dash
                    g.DrawLine(p, rect.Left, midY, rect.Right, midY)
                End Using

            Case ChartDashStyle.DashDot
                Using p As New Pen(foregroundColor, kPenWidthDefault)
                    p.DashStyle = Drawing2D.DashStyle.DashDot
                    g.DrawLine(p, rect.Left, midY, rect.Right, midY)
                End Using

            Case ChartDashStyle.DashDotDot
                Using p As New Pen(foregroundColor, kPenWidthDefault)
                    p.DashStyle = Drawing2D.DashStyle.DashDotDot
                    g.DrawLine(p, rect.Left, midY, rect.Right, midY)
                End Using

            Case ChartDashStyle.Dot
                Using p As New Pen(foregroundColor, kPenWidthDefault)
                    p.DashStyle = Drawing2D.DashStyle.Dot
                    g.DrawLine(p, rect.Left, midY, rect.Right, midY)
                End Using

            Case ChartDashStyle.Solid
                Using p As New Pen(foregroundColor, kPenWidthDefault)
                    p.DashStyle = Drawing2D.DashStyle.Solid
                    g.DrawLine(p, rect.Left, midY, rect.Right, midY)
                End Using
        End Select
    End Sub
End Class
