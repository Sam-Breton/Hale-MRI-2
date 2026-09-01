Imports System.ComponentModel
Imports System.Windows.Forms.DataVisualization.Charting

Public Class ComboSeriesAxisTypePicker
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

        ' Required for OnDrawItem to trigger
        Me.DrawMode = DrawMode.OwnerDrawFixed
        Me.DropDownStyle = ComboBoxStyle.DropDownList
        Me.ItemHeight = kItemHeightDefault

        ' CRITICAL: Only populate data when the program is actually running
        If Not Me.DesignMode AndAlso System.ComponentModel.LicenseManager.UsageMode = System.ComponentModel.LicenseUsageMode.Runtime Then
            Me.DataSource = [Enum].GetValues(GetType(AxisType))
        End If
    End Sub

    <Browsable(False)>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property AxisType As AxisType
        Get
            If Me.SelectedItem IsNot Nothing Then
                Return CType(Me.SelectedItem, AxisType)
            End If
            Return AxisType.Primary
        End Get
        Set(value As AxisType)
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
        ' Prevent errors during design time or empty states
        If e.Index < 0 OrElse Me.Items.Count = 0 Then Return

        ' Draw standard item background and focus rectangle
        e.DrawBackground()
        e.DrawFocusRectangle()

        Dim itemValue As AxisType = CType(Me.Items(e.Index), AxisType)
        Dim bounds As Rectangle = e.Bounds

        ' 1. Define preview area geometry (a small box on the left)
        Dim previewSize As Integer = bounds.Height - 6
        Dim previewRect As New Rectangle(bounds.X + 4, bounds.Y + 3, previewSize, previewSize)

        ' 2. Draw the graphical axis preview line
        Using g As Graphics = e.Graphics
            ' Draw preview background
            g.FillRectangle(Brushes.White, previewRect)
            g.DrawRectangle(Pens.Gray, previewRect)

            ' Select line color and layout based on axis type
            Using axisPen As New Pen(If(itemValue = AxisType.Primary, Color.Blue, Color.Red), 2)
                If itemValue = AxisType.Primary Then
                    ' Bottom/Left line style (Primary X/Y representation)
                    g.DrawLine(axisPen, previewRect.Left + 2, previewRect.Bottom - 3, previewRect.Right - 2, previewRect.Bottom - 3)
                Else
                    ' Top/Right line style (Secondary X2/Y2 representation)
                    g.DrawLine(axisPen, previewRect.Left + 2, previewRect.Top + 3, previewRect.Right - 2, previewRect.Top + 3)
                End If
            End Using

            ' 3. Draw text shifted to the right of the preview box
            Dim textX As Integer = previewRect.Right + 6
            Dim textRect As New Rectangle(textX, bounds.Y, bounds.Width - textX, bounds.Height)

            ' Keep text color readable during selection highlight
            Dim textColor As Color = If((e.State And DrawItemState.Selected) = DrawItemState.Selected,
                                        SystemColors.HighlightText, SystemColors.WindowText)

            Using textBrush As New SolidBrush(textColor)
                Using sf As New StringFormat() With {.LineAlignment = StringAlignment.Center}
                    g.DrawString(itemValue.ToString(), e.Font, textBrush, textRect, sf)
                End Using
            End Using
        End Using
    End Sub
End Class
