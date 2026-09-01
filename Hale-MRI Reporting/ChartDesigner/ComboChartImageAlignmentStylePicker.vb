Imports System.ComponentModel
Imports System.Windows.Forms.DataVisualization.Charting

Public Class ComboChartImageAlignmentStylePicker
    Inherits ComboBox

    Private Const kItemHeightDefault As Integer = 18
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
            Me.DataSource = [Enum].GetValues(GetType(ChartImageAlignmentStyle))
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
    Public Property AlignmentStyle As ChartImageAlignmentStyle
        Get
            If Me.SelectedItem IsNot Nothing Then
                Return CType(Me.SelectedItem, ChartImageAlignmentStyle)
            End If
            Return MarkerStyle.None
        End Get
        Set(value As ChartImageAlignmentStyle)
            If Me.DataSource IsNot Nothing Then
                Me.SelectedItem = value
            End If
        End Set
    End Property

    Protected Overrides Sub OnDrawItem(e As DrawItemEventArgs)
        ' Prevent crashes during design time or if the list is empty
        If e.Index < 0 Then Return

        ' 1. Draw the background and focus rectangle
        e.DrawBackground()

        ' Get the current alignment style enum value
        Dim style As ChartImageAlignmentStyle = CType(Me.Items(e.Index), ChartImageAlignmentStyle)
        Dim g As Graphics = e.Graphics

        ' 2. Define geometry for the preview rectangle
        Dim previewSize As Integer = e.Bounds.Height - 6
        Dim previewRect As New Rectangle(e.Bounds.X + 4, e.Bounds.Y + 3, previewSize, previewSize)

        ' Draw the preview box container boundaries
        Using borderPen As New Pen(If((e.State And DrawItemState.Selected) = DrawItemState.Selected, Color.White, Color.Gray))
            g.DrawRectangle(borderPen, previewRect)
        End Using

        ' 3. Calculate and draw the alignment indicator inside the preview box
        Dim dotSize As Integer = 4
        Dim dotRect As New Rectangle(0, 0, dotSize, dotSize)

        ' Map the enum value to geometric coordinates inside the preview boundary
        Select Case style
            Case ChartImageAlignmentStyle.TopLeft
                dotRect.Location = New Point(previewRect.Left + 1, previewRect.Top + 1)
            Case ChartImageAlignmentStyle.Top
                dotRect.Location = New Point(previewRect.Left + (previewSize \ 2) - (dotSize \ 2), previewRect.Top + 1)
            Case ChartImageAlignmentStyle.TopRight
                dotRect.Location = New Point(previewRect.Right - dotSize - 1, previewRect.Top + 1)
            Case ChartImageAlignmentStyle.Left
                dotRect.Location = New Point(previewRect.Left + 1, previewRect.Top + (previewSize \ 2) - (dotSize \ 2))
            Case ChartImageAlignmentStyle.Center
                dotRect.Location = New Point(previewRect.Left + (previewSize \ 2) - (dotSize \ 2), previewRect.Top + (previewSize \ 2) - (dotSize \ 2))
            Case ChartImageAlignmentStyle.Right
                dotRect.Location = New Point(previewRect.Right - dotSize - 1, previewRect.Top + (previewSize \ 2) - (dotSize \ 2))
            Case ChartImageAlignmentStyle.BottomLeft
                dotRect.Location = New Point(previewRect.Left + 1, previewRect.Bottom - dotSize - 1)
            Case ChartImageAlignmentStyle.Bottom
                dotRect.Location = New Point(previewRect.Left + (previewSize \ 2) - (dotSize \ 2), previewRect.Bottom - dotSize - 1)
            Case ChartImageAlignmentStyle.BottomRight
                dotRect.Location = New Point(previewRect.Right - dotSize - 1, previewRect.Bottom - dotSize - 1)
        End Select

        ' Render the alignment indicator indicator
        Dim brushColor As Color = If((e.State And DrawItemState.Selected) = DrawItemState.Selected, Color.White, Color.Black)
        Using indicatorBrush As New SolidBrush(brushColor)
            g.FillRectangle(indicatorBrush, dotRect)
        End Using

        ' 4. Draw the item text text layout
        Dim textX As Integer = previewRect.Right + 6
        Dim textRect As New Rectangle(textX, e.Bounds.Y, e.Bounds.Width - textX, e.Bounds.Height)

        Using textBrush As New SolidBrush(e.ForeColor)
            Dim sf As New StringFormat() With {.LineAlignment = StringAlignment.Center}
            g.DrawString(style.ToString(), e.Font, textBrush, textRect, sf)
        End Using

        ' Draw the focus rectangle boundary overlay
        e.DrawFocusRectangle()
    End Sub
End Class
