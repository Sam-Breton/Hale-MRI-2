Imports System.ComponentModel
Imports System.Text.RegularExpressions
Imports System.Windows.Forms.DataVisualization.Charting

Public Class ComboChartLegendTypePicker
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

        ' Setup rendering styles matching your framework pattern
        Me.DrawMode = DrawMode.OwnerDrawFixed
        Me.DropDownStyle = ComboBoxStyle.DropDownList
        Me.ItemHeight = kItemHeightDefault

        ' CRITICAL: Only populate data when the program is actually running
        If Not Me.DesignMode AndAlso LicenseManager.UsageMode = LicenseUsageMode.Runtime Then
            Me.DataSource = [Enum].GetValues(GetType(LegendStyle))
        End If
    End Sub

    ''' <summary>
    ''' Shadows the base DataSource to keep the Designer from auto-serializing code.
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
    ''' Shadows the base Items collection to prevent designer serialization.
    ''' </summary>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    <Browsable(False)>
    <Bindable(False)>
    Public Shadows ReadOnly Property Items As ObjectCollection
        Get
            Return MyBase.Items
        End Get
    End Property

    ''' <summary>
    ''' Strongly-typed helper property to easily extract the selected LegendStyle enum.
    ''' </summary>
    <Browsable(False)>
    Public Property LegendStyle As LegendStyle
        Get
            If Me.SelectedItem IsNot Nothing Then
                Return CType(Me.SelectedItem, LegendStyle)
            End If
            Return LegendStyle.Table ' Safe fallback default.
        End Get
        Set(value As LegendStyle)
            If Me.DataSource IsNot Nothing Then
                Me.SelectedItem = value
            End If
        End Set
    End Property

    Protected Overrides Sub OnDrawItem(e As DrawItemEventArgs)
        MyBase.OnDrawItem(e)

        ' Ensure index is valid before painting
        If e.Index < 0 Then Return

        ' 1. Grab the current enum value from our runtime array source
        Dim stylesArray As LegendStyle() = CType(Me.DataSource, LegendStyle())
        Dim currentStyle As LegendStyle = stylesArray(e.Index)

        ' 2. Split PascalCase into user-friendly words (e.g., "Column" stays "Column", but any compound names will split)
        Dim displayString As String = Regex.Replace(currentStyle.ToString(), "(\B[A-Z])", " $1")

        ' 3. Draw standard row background highlights
        e.DrawBackground()

        ' 4. Match the font color to the selection state
        Dim textColor As Color = If((e.State And DrawItemState.Selected) = DrawItemState.Selected,
                                    SystemColors.HighlightText,
                                    Me.ForeColor)

        ' 5. Draw the text inside a safely padded rectangle
        Using textBrush As New SolidBrush(textColor)
            Dim textBounds As New Rectangle(e.Bounds.X + 4, e.Bounds.Y, e.Bounds.Width - 4, e.Bounds.Height)
            Dim sf As New StringFormat() With {.LineAlignment = StringAlignment.Center}

            e.Graphics.DrawString(displayString, e.Font, textBrush, textBounds, sf)
        End Using

        ' 6. Paint focus cues
        e.DrawFocusRectangle()
    End Sub
End Class
