Imports System.ComponentModel
Imports System.Windows.Forms.DataVisualization.Charting

Public Class ComboChartLegendTableStylePicker
    Inherits ComboBox

    Private Const kItemHeightDefault As Integer = 21 ' Extra height for nice grid/cell box icon drawing

    Public Sub New()
        MyBase.New()
        InitializeComponent()

        ' Configure required custom drawing settings
        Me.DrawMode = DrawMode.OwnerDrawFixed
        Me.DropDownStyle = ComboBoxStyle.DropDownList
        Me.ItemHeight = kItemHeightDefault

        ' Populate data exclusively at runtime to protect the designer canvas
        If Not Me.DesignMode AndAlso System.ComponentModel.LicenseManager.UsageMode = System.ComponentModel.LicenseUsageMode.Runtime Then
            Me.DataSource = [Enum].GetValues(GetType(LegendTableStyle))
        End If
    End Sub

    ''' <summary>
    ''' Exposes the strongly-typed LegendTableStyle property. Hidden from the property grid to prevent design-time conflicts.
    ''' </summary>
    <Browsable(False)>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property TableStyle As LegendTableStyle
        Get
            If Me.SelectedItem IsNot Nothing Then
                Return CType(Me.SelectedItem, LegendTableStyle)
            End If
            Return LegendTableStyle.Auto
        End Get
        Set(value As LegendTableStyle)
            If Me.DataSource IsNot Nothing Then
                Me.SelectedItem = value
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
        Dim currentStyle As LegendTableStyle = CType(Me.Items(e.Index), LegendTableStyle)

        ' Determine theme element colors based on hover highlight states
        Dim elementColor As Color = If((e.State And DrawItemState.Selected) = DrawItemState.Selected,
                                        SystemColors.HighlightText,
                                        SystemColors.WindowText)

        ' Layout bounds calculation
        Dim textWidth As Integer = CInt(e.Bounds.Width * 0.45)
        Dim textBounds As New Rectangle(e.Bounds.Left + 4, e.Bounds.Top, textWidth - 4, e.Bounds.Height)

        ' Size and position of the visual preview icon centered vertically
        Dim iconWidth As Integer = 24
        Dim iconHeight As Integer = 16
        Dim iconX As Integer = e.Bounds.Left + textWidth + 10
        Dim iconY As Integer = e.Bounds.Top + ((e.Bounds.Height - iconHeight) \ 2)
        Dim iconBounds As New Rectangle(iconX, iconY, iconWidth, iconHeight)

        ' 1. Draw the style name text string
        Using textBrush As New SolidBrush(elementColor),
              sf As New StringFormat()
            sf.LineAlignment = StringAlignment.Center
            e.Graphics.DrawString(currentStyle.ToString(), e.Font, textBrush, textBounds, sf)
        End Using

        ' 2. Draw the table structure diagram
        Using pen As New Pen(elementColor, 1),
              fillBrush As New SolidBrush(Color.FromArgb(50, elementColor)) ' semi-transparent cell filling

            ' Draw outer main boundary of the mini table container
            e.Graphics.DrawRectangle(pen, iconBounds)

            Select Case currentStyle
                Case LegendTableStyle.Auto
                    ' Replicates an adaptive layout split (Balanced 2x2 grid representing native scaling)
                    Dim w As Integer = (iconBounds.Width - 6) \ 2
                    Dim h As Integer = (iconBounds.Height - 6) \ 2
                    Dim cells() As Rectangle = {
                        New Rectangle(iconBounds.Left + 2, iconBounds.Top + 2, w, h),
                        New Rectangle(iconBounds.Left + 4 + w, iconBounds.Top + 2, w, h),
                        New Rectangle(iconBounds.Left + 2, iconBounds.Top + 4 + h, w, h),
                        New Rectangle(iconBounds.Left + 4 + w, iconBounds.Top + 4 + h, w, h)
                    }
                    For Each rect In cells
                        e.Graphics.FillRectangle(fillBrush, rect)
                        e.Graphics.DrawRectangle(pen, rect)
                    Next

                Case LegendTableStyle.Wide
                    ' Replicates horizontal growth bias (3 wide horizontal rows stretching left to right)
                    Dim w As Integer = iconBounds.Width - 4
                    Dim h As Integer = (iconBounds.Height - 8) \ 3
                    Dim cells() As Rectangle = {
                        New Rectangle(iconBounds.Left + 2, iconBounds.Top + 2, w, h),
                        New Rectangle(iconBounds.Left + 2, iconBounds.Top + 4 + h, w, h),
                        New Rectangle(iconBounds.Left + 2, iconBounds.Top + 6 + (h * 2), w, h)
                    }
                    For Each rect In cells
                        e.Graphics.FillRectangle(fillBrush, rect)
                        e.Graphics.DrawRectangle(pen, rect)
                    Next

                Case LegendTableStyle.Tall
                    ' Replicates vertical growth bias (3 tall columnar segments stretching top to bottom)
                    Dim w As Integer = (iconBounds.Width - 8) \ 3
                    Dim h As Integer = iconBounds.Height - 4
                    Dim cells() As Rectangle = {
                        New Rectangle(iconBounds.Left + 2, iconBounds.Top + 2, w, h),
                        New Rectangle(iconBounds.Left + 4 + w, iconBounds.Top + 2, w, h),
                        New Rectangle(iconBounds.Left + 6 + (w * 2), iconBounds.Top + 2, w, h)
                    }
                    For Each rect In cells
                        e.Graphics.FillRectangle(fillBrush, rect)
                        e.Graphics.DrawRectangle(pen, rect)
                    Next
            End Select
        End Using

        e.DrawFocusRectangle()
    End Sub
End Class
