Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms.DataVisualization.Charting

Public Class ComboChartLegendSeparatorStylePicker
    Inherits ComboBox

    Private Const kItemHeightDefault As Integer = 18

    Public Sub New()
        MyBase.New()
        InitializeComponent()

        ' Configure required custom drawing settings
        Me.DrawMode = DrawMode.OwnerDrawFixed
        Me.DropDownStyle = ComboBoxStyle.DropDownList
        Me.ItemHeight = kItemHeightDefault

        ' Populate data exclusively at runtime to protect the designer canvas
        If Not Me.DesignMode AndAlso System.ComponentModel.LicenseManager.UsageMode = System.ComponentModel.LicenseUsageMode.Runtime Then
            Me.DataSource = [Enum].GetValues(GetType(LegendSeparatorStyle))
        End If
    End Sub

    ''' <summary>
    ''' Exposes the strongly-typed separator style property. Hidden from the property grid to prevent design-time conflicts.
    ''' </summary>
    <Browsable(False)>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property SeparatorStyle As LegendSeparatorStyle
        Get
            If Me.SelectedItem IsNot Nothing Then
                Return CType(Me.SelectedItem, LegendSeparatorStyle)
            End If
            Return LegendSeparatorStyle.None
        End Get
        Set(value As LegendSeparatorStyle)
            ' Guard block to ensure we don't apply values before DataSource binds at runtime
            If Me.DataSource IsNot Nothing Then
                If Me.DataSource IsNot Nothing Then
                    Me.SelectedItem = value
                End If
            End If
        End Set
    End Property

    ''' <summary>
    ''' Prevents Designer from adding code that throws run-time exception.
    ''' </summary>
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
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    <Browsable(False)>
    <Bindable(False)>
    Public Shadows ReadOnly Property Items As ObjectCollection
        Get
            Return MyBase.Items
        End Get
    End Property

    Protected Overrides Sub OnDrawItem(e As DrawItemEventArgs)
        ' Safeguard against early draw cycles or unpopulated items
        If e.Index < 0 OrElse Me.Items.Count = 0 OrElse e.Index >= Me.Items.Count Then Return

        e.DrawBackground()

        ' Safely extract the enum value
        Dim currentStyle As LegendSeparatorStyle = CType(Me.Items(e.Index), LegendSeparatorStyle)

        ' Determine text and line color based on whether the item is highlighted
        Dim elementColor As Color = If((e.State And DrawItemState.Selected) = DrawItemState.Selected,
                                        SystemColors.HighlightText,
                                        SystemColors.WindowText)

        ' Layout metrics: Use left half for text name, right half for visual line preview
        Dim textWidth As Integer = CInt(e.Bounds.Width * 0.45)
        Dim textBounds As New Rectangle(e.Bounds.Left + 4, e.Bounds.Top, textWidth - 4, e.Bounds.Height)
        Dim lineBounds As New Rectangle(e.Bounds.Left + textWidth + 5, e.Bounds.Top, e.Bounds.Width - textWidth - 10, e.Bounds.Height)

        ' 1. Draw the style name text
        Using textBrush As New SolidBrush(elementColor),
              sf As New StringFormat()
            sf.LineAlignment = StringAlignment.Center
            e.Graphics.DrawString(currentStyle.ToString(), e.Font, textBrush, textBounds, sf)
        End Using

        ' 2. Draw the graphical line preview based on the Enum choice
        Dim centerY As Integer = lineBounds.Top + (lineBounds.Height \ 2)

        Using linePen As New Pen(elementColor, 1)
            Select Case currentStyle
                Case LegendSeparatorStyle.None
                    ' Draw nothing graphic or safely skip

                Case LegendSeparatorStyle.Line
                    e.Graphics.DrawLine(linePen, lineBounds.Left, centerY, lineBounds.Right, centerY)

                Case LegendSeparatorStyle.ThickLine
                    linePen.Width = 3
                    e.Graphics.DrawLine(linePen, lineBounds.Left, centerY, lineBounds.Right, centerY)

                Case LegendSeparatorStyle.DoubleLine
                    ' Top line
                    e.Graphics.DrawLine(linePen, lineBounds.Left, centerY - 2, lineBounds.Right, centerY - 2)
                    ' Bottom line
                    e.Graphics.DrawLine(linePen, lineBounds.Left, centerY + 2, lineBounds.Right, centerY + 2)

                Case LegendSeparatorStyle.DashLine
                    linePen.DashStyle = DashStyle.Dash
                    e.Graphics.DrawLine(linePen, lineBounds.Left, centerY, lineBounds.Right, centerY)

                Case LegendSeparatorStyle.DotLine
                    linePen.DashStyle = DashStyle.Dot
                    e.Graphics.DrawLine(linePen, lineBounds.Left, centerY, lineBounds.Right, centerY)

                Case LegendSeparatorStyle.GradientLine, LegendSeparatorStyle.ThickGradientLine
                    ' Fallback to standard line pen configuration if width is 0 or less
                    If lineBounds.Width > 0 Then
                        Dim thickness As Integer = If(currentStyle = LegendSeparatorStyle.ThickGradientLine, 3, 1)

                        ' LinearGradientBrush that fades to transparent on both ends
                        Using gradBrush As New LinearGradientBrush(lineBounds, Color.Transparent, elementColor, 0.0F)
                            ' Define a 3-way color blend blend: Transparent -> Solid -> Transparent
                            Dim blend As New ColorBlend(3)
                            blend.Colors = New Color() {Color.Transparent, elementColor, Color.Transparent}
                            blend.Positions = New Single() {0.0F, 0.5F, 1.0F}
                            gradBrush.InterpolationColors = blend

                            Using gradPen As New Pen(gradBrush, thickness)
                                e.Graphics.DrawLine(gradPen, lineBounds.Left, centerY, lineBounds.Right, centerY)
                            End Using
                        End Using
                    End If
            End Select
        End Using

        e.DrawFocusRectangle()
    End Sub
End Class
