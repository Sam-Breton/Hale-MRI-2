Imports System.ComponentModel

Public Class ComboContentAlignmentPicker
    Inherits ComboBox

    Private Const kColorBlendCount As Integer = 3
    Private Const kItemHeightDefault As Integer = 18
    Private Const kPenWidthDefault As Single = 1.0!
    Private Const kPreviewRectOffsetBottom As Integer = 3
    Private Const kPreviewRectOffsetHeight As Integer = -6
    Private Const kPreviewRectOffsetTop As Integer = 3
    Private Const kPreviewRectOffsetX As Integer = 4
    Private Const kPreviewRectOffsetY As Integer = 3
    Private Const kPreviewRectWidthDefault As Integer = 30
    Private Const kTextRectOffsetRight As Integer = 8

    Public Sub New()
        MyBase.New()
        InitializeComponent()

        Me.DropDownStyle = ComboBoxStyle.DropDownList
        Me.DrawMode = DrawMode.OwnerDrawFixed
        Me.ItemHeight = kItemHeightDefault

        ' CRITICAL: Only populate data when the program is actually running
        If Not Me.DesignMode AndAlso System.ComponentModel.LicenseManager.UsageMode = System.ComponentModel.LicenseUsageMode.Runtime Then
            Me.DataSource = [Enum].GetValues(GetType(ContentAlignment))
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
    Public Property Alignment As ContentAlignment
        Get
            If Me.SelectedItem IsNot Nothing Then
                Return CType(Me.SelectedItem, ContentAlignment)
            End If
            Return ContentAlignment.MiddleCenter
        End Get
        Set(value As ContentAlignment)
            If Me.DataSource IsNot Nothing Then
                Me.SelectedItem = value
            End If
        End Set
    End Property

    Protected Overrides Sub OnDrawItem(e As DrawItemEventArgs)
        If e.Index < 0 Then Return

        Dim g As Graphics = e.Graphics
        Dim alignment As ContentAlignment = CType(Me.Items(e.Index), ContentAlignment)

        ' Render native Windows selection background or standard item background
        e.DrawBackground()

        ' Setup boundaries for the 3x3 layout preview matrix block
        Dim previewRect As New Rectangle(e.Bounds.X + kPreviewRectOffsetX, e.Bounds.Y + kPreviewRectOffsetY, kPreviewRectWidthDefault,
                                         e.Bounds.Height + kPreviewRectOffsetHeight)

        If previewRect.Width > 0 AndAlso previewRect.Height > 0 Then
            DrawAlignmentGrid(g, previewRect, e.ForeColor, alignment)
        End If

        ' Render text label next to the preview matrix block
        Using textBrush As New SolidBrush(e.ForeColor)
            Dim textX As Integer = previewRect.Right + kTextRectOffsetRight
            Dim textY As Integer = e.Bounds.Y + ((e.Bounds.Height - g.MeasureString(alignment.ToString(), Me.Font).Height) / 2)
            g.DrawString(alignment.ToString(), Me.Font, textBrush, textX, textY)
        End Using

        ' Draws standard Windows dotted focus indicators if needed
        e.DrawFocusRectangle()
    End Sub

    ' Renders a miniature 3x3 grid box with a filled dot marking the alignment state
    Private Sub DrawAlignmentGrid(g As Graphics, rect As Rectangle, foregroundColor As Color, align As ContentAlignment)
        ' Draw boundary outline box
        Using p As New Pen(foregroundColor, kPenWidthDefault)
            g.DrawRectangle(p, rect)
        End Using

        ' Size of the internal alignment indicator dot
        Dim dotSize As Integer = 4
        Dim dotX As Integer = rect.X + (rect.Width \ 2) - (dotSize \ 2)
        Dim dotY As Integer = rect.Y + (rect.Height \ 2) - (dotSize \ 2)

        ' Calculate exact X coordinate based on Left/Center/Right flags
        If (align And (ContentAlignment.TopLeft Or ContentAlignment.MiddleLeft Or ContentAlignment.BottomLeft)) <> 0 Then
            dotX = rect.X + kPreviewRectOffsetTop
        ElseIf (align And (ContentAlignment.TopRight Or ContentAlignment.MiddleRight Or ContentAlignment.BottomRight)) <> 0 Then
            dotX = rect.Right - dotSize - kPreviewRectOffsetTop
        End If

        ' Calculate exact Y coordinate based on Top/Middle/Bottom flags
        If (align And (ContentAlignment.TopLeft Or ContentAlignment.TopCenter Or ContentAlignment.TopRight)) <> 0 Then
            dotY = rect.Y + kPreviewRectOffsetBottom
        ElseIf (align And (ContentAlignment.BottomLeft Or ContentAlignment.BottomCenter Or ContentAlignment.BottomRight)) <> 0 Then
            dotY = rect.Bottom - dotSize - kPreviewRectOffsetBottom
        End If

        ' Fill the target alignment dot indicator
        Using b As New SolidBrush(foregroundColor)
            g.FillRectangle(b, dotX, dotY, dotSize, dotSize)
        End Using
    End Sub
End Class

