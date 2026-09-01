Imports System.ComponentModel
Imports Hale_MRI_Reporting.ComboChartAnnotationTypePicker

Public Class ComboLegendStringAlignmentPicker
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

        ' Enable custom painting
        Me.DrawMode = DrawMode.OwnerDrawFixed
        Me.DropDownStyle = ComboBoxStyle.DropDownList
        Me.ItemHeight = kItemHeightDefault

        ' CRITICAL: Only populate data when the program is actually running
        If Not Me.DesignMode AndAlso System.ComponentModel.LicenseManager.UsageMode = System.ComponentModel.LicenseUsageMode.Runtime Then
            Me.DataSource = [Enum].GetValues(GetType(StringAlignment))
        End If
    End Sub

    ''' <summary>
    ''' A custom ComboBox providing a graphical preview for Chart.Legend StringAlignment.
    ''' Safe for the Visual Studio Form Designer.
    ''' </summary>
    <Browsable(False)>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property Alignment As StringAlignment
        Get
            If Me.SelectedItem IsNot Nothing Then
                Return CType(Me.SelectedItem, StringAlignment)
            End If
            Return StringAlignment.Near
        End Get
        Set(value As StringAlignment)
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
        ' Ensure we don't draw an empty or invalid index
        If e.Index < 0 OrElse Me.Items.Count = 0 OrElse e.Index >= Me.Items.Count Then Return

        ' Draw standard item background (handles selection highlights)
        e.DrawBackground()

        ' Safely grab the enum value from the array data source
        Dim alignment As StringAlignment = CType(Me.Items(e.Index), StringAlignment)

        Using sf As New StringFormat()
            sf.Alignment = alignment
            sf.LineAlignment = StringAlignment.Center

            ' Match text color to the current system selection state
            Dim textColor As Color = If((e.State And DrawItemState.Selected) = DrawItemState.Selected,
                                        SystemColors.HighlightText,
                                        SystemColors.WindowText)

            Using textBrush As New SolidBrush(textColor)
                Dim displayText As String = alignment.ToString()
                e.Graphics.DrawString(displayText, e.Font, textBrush, e.Bounds, sf)
            End Using
        End Using

        ' Draw focus rectangle if needed
        e.DrawFocusRectangle()
    End Sub
End Class
