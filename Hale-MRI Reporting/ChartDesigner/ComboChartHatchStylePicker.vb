Imports System.ComponentModel
Imports System.Drawing.Drawing2D

Public Class ComboChartHatchStylePicker
    Inherits ComboBox

    Private Const kItemHeightDefault As Integer = 18
    Private Const kPreviewRectOffsetHeight As Integer = -6
    Private Const kPreviewRectOffsetX As Integer = 4
    Private Const kPreviewRectOffsetY As Integer = 3
    Private Const kPreviewRectWidthDefault As Integer = 35

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        Me.DropDownStyle = ComboBoxStyle.DropDownList
        Me.DrawMode = DrawMode.OwnerDrawFixed
        Me.ItemHeight = kItemHeightDefault

        ' CRITICAL: Only populate data when the program is actually running
        If Not Me.DesignMode AndAlso System.ComponentModel.LicenseManager.UsageMode = System.ComponentModel.LicenseUsageMode.Runtime Then
            Me.DataSource = [Enum].GetValues(GetType(HatchStyle))
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
    Public Property HatchStyle As HatchStyle
        Get
            If Me.SelectedItem IsNot Nothing Then
                Return CType(Me.SelectedItem, HatchStyle)
            End If
            Return HatchStyle.Min
        End Get
        Set(value As HatchStyle)
            If Me.DataSource IsNot Nothing Then
                Me.SelectedItem = value
            End If
        End Set
    End Property

    Protected Overrides Sub OnDrawItem(e As DrawItemEventArgs)
        If e.Index < 0 Then Return

        Dim g As Graphics = e.Graphics
        Dim style As HatchStyle = CType(Me.Items(e.Index), HatchStyle)

        ' Render native Windows background or selection highlighted state
        e.DrawBackground()

        ' Setup boundaries for the texture swatch preview area
        Dim previewRect As New Rectangle(e.Bounds.X + kPreviewRectOffsetX, e.Bounds.Y + kPreviewRectOffsetY,
                                         kPreviewRectWidthDefault, e.Bounds.Height + kPreviewRectOffsetHeight)

        If previewRect.Width > 0 AndAlso previewRect.Height > 0 Then
            ' Fill pattern using foreground color on a transparent background
            Using brush As New HatchBrush(style, e.ForeColor, Color.Transparent)
                g.FillRectangle(brush, previewRect)
            End Using

            ' Draw a native border outline around the swatch block matching current forecolor
            Using p As New Pen(e.ForeColor, 1)
                g.DrawRectangle(p, previewRect)
            End Using
        End If

        ' Render the enum string name text next to the swatch preview
        Using textBrush As New SolidBrush(e.ForeColor)
            Dim textX As Integer = previewRect.Right + 8
            Dim textY As Integer = e.Bounds.Y + ((e.Bounds.Height - g.MeasureString(style.ToString(), Me.Font).Height) / 2)
            g.DrawString(style.ToString(), Me.Font, textBrush, textX, textY)
        End Using

        ' Draws standard Windows dotted focus indicators if the control has focus
        e.DrawFocusRectangle()
    End Sub
End Class
