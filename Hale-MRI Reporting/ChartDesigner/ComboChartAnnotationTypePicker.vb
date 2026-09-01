Imports System.ComponentModel

Public Class ComboChartAnnotationTypePicker
    Inherits ComboBox

    ' Enumerates valid Annotation types.
    Public Enum ChartAnnotationType
        Line
        VerticalLine
        HorizontalLine
        Polyline
        Polygon
        Rectangle
        Ellipse
        Arrow
        Text
        Image
        Callout
    End Enum

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

        ' Enable custom painting
        Me.DrawMode = DrawMode.OwnerDrawFixed
        Me.DropDownStyle = ComboBoxStyle.DropDownList
        Me.ItemHeight = kItemHeightDefault

        ' CRITICAL: Only populate data when the program is actually running
        If Not Me.DesignMode AndAlso System.ComponentModel.LicenseManager.UsageMode = System.ComponentModel.LicenseUsageMode.Runtime Then
            Me.DataSource = [Enum].GetValues(GetType(ChartAnnotationType))
        End If
    End Sub

    <Browsable(False)>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property AnnotationType As ChartAnnotationType
        Get
            If Me.SelectedItem IsNot Nothing Then
                Dim item As Object = Me.SelectedItem
                Dim at As ChartAnnotationType = CType(item, ChartAnnotationType)
                Return CType(Me.SelectedItem, ChartAnnotationType)
            End If
            Return ChartAnnotationType.Line ' Default fallback
        End Get
        Set(value As ChartAnnotationType)
            If Me.DataSource IsNot Nothing Then
                Me.SelectedItem = value
            End If
        End Set
    End Property

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

    Protected Overrides Sub OnDrawItem(e As DrawItemEventArgs)
        MyBase.OnDrawItem(e)

        ' Avoid drawing if index is invalid (e.g., empty list or designer rendering)
        If e.Index < 0 Then Return

        ' 1. Retrieve the raw enum item from the DataSource array
        Dim enumArray As ChartAnnotationType() = CType(Me.DataSource, ChartAnnotationType())
        Dim currentEnum As ChartAnnotationType = enumArray(e.Index)

        ' 2. Turn the enum into clean, user-friendly text
        Dim displayString As String = System.Text.RegularExpressions.Regex.Replace(currentEnum.ToString(), "(\B[A-Z])", " $1")

        ' 3. Paint the standard item background (handles hover highlights automatically)
        e.DrawBackground()

        ' 4. Determine text color based on item state (Selected hover vs normal state)
        Dim textColor As Color = If((e.State And DrawItemState.Selected) = DrawItemState.Selected,
                                    SystemColors.HighlightText,
                                    Me.ForeColor)

        ' 5. Draw the text cleanly inside the bounding rectangle
        Using textBrush As New SolidBrush(textColor)
            ' Add a small horizontal margin (e.g., 4 pixels) so text isn't smashed against the edge
            Dim textBounds As New Rectangle(e.Bounds.X + 4, e.Bounds.Y, e.Bounds.Width - 4, e.Bounds.Height)

            ' Center the text vertically inside the row allocation
            Dim sf As New StringFormat() With {.LineAlignment = StringAlignment.Center}

            e.Graphics.DrawString(displayString, e.Font, textBrush, textBounds, sf)
        End Using

        ' 6. Paint focus rectangle if appropriate
        e.DrawFocusRectangle()
    End Sub
End Class
