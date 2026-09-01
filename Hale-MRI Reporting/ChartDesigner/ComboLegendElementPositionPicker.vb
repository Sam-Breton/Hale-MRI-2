Imports System.ComponentModel
Imports System.Windows.Forms.DataVisualization.Charting

Public Class ComboLegendElementPositionPicker
    Inherits ComboBox

    Private Const kBorderPenWidth As Single = 1.0!
    Private Const kDashPenWidth As Single = 1.0!
    Private Const kItemHeightDefault As Integer = 21
    Private Const kPreviewRectangleHeight As Integer = 12
    Private Const kPreviewRectangleWidth As Integer = 16
    Private Const kPreviewRectangleXOffset As Integer = 4
    Private Const kPreviewRectangleYOffset As Integer = 3
    Private Const kPreviewTextOffset As Integer = 8
    Private Const kTextRectangleXOffset As Integer = 26
    Private Const kTextRectangleWidthOffset As Integer = -26

    Public Sub New()
        MyBase.New()
        InitializeComponent()

        ' Set required properties for custom drawing.
        Me.DrawMode = DrawMode.OwnerDrawFixed
        Me.DropDownStyle = ComboBoxStyle.DropDownList
        Me.ItemHeight = kItemHeightDefault

        ' Populate standard preset coordinates for standard chart element layout configurations.
        Dim presets As New List(Of ElementPosition)()
        presets.Add(New ElementPosition() With {.Auto = True})  ' Automatic layout sizing
        presets.Add(New ElementPosition(0, 0, 30, 15))          ' Top Left
        presets.Add(New ElementPosition(70, 0, 30, 15))         ' Top Right
        presets.Add(New ElementPosition(0, 85, 30, 15))         ' Bottom Left
        presets.Add(New ElementPosition(70, 85, 30, 15))        ' Bottom Right
        presets.Add(New ElementPosition(35, 35, 30, 30))        ' Centered

        Me.DataSource = presets
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

    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    <Browsable(False)>
    <Bindable(False)>
    Public Shadows ReadOnly Property Items As ObjectCollection
        Get
            Return MyBase.Items
        End Get
    End Property

    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    <Browsable(False)>
    Public Property ElementPosition As ElementPosition
        Get
            If Me.SelectedItem IsNot Nothing Then
                Return CType(Me.SelectedItem, ElementPosition)
            End If
            Return Nothing
        End Get
        Set(value As ElementPosition)
            If Me.DataSource IsNot Nothing AndAlso value IsNot Nothing Then
                Dim list As List(Of ElementPosition) = CType(Me.DataSource, List(Of ElementPosition))
                Dim foundItem As ElementPosition = Nothing

                ' Value-match by coordinate equality since incoming instances vary
                For Each item As ElementPosition In list
                    If item.Auto = value.Auto AndAlso
                       item.X = value.X AndAlso
                       item.Y = value.Y AndAlso
                       item.Width = value.Width AndAlso
                       item.Height = value.Height Then
                        foundItem = item
                        Exit For
                    End If
                Next

                ' Dynamic Support: If a unique programmatic position is applied, append it to the picker options
                If foundItem Is Nothing Then
                    list.Add(value)
                    Me.DataSource = Nothing
                    Me.DataSource = list
                    foundItem = value
                End If

                Me.SelectedItem = foundItem
            ElseIf value Is Nothing Then
                Me.SelectedIndex = 0
            End If
        End Set
    End Property

    Protected Overrides Sub OnDrawItem(ByVal e As DrawItemEventArgs)
        If e.Index < 0 Then Return

        ' Render systemic selection backgrounds and focus borders
        e.DrawBackground()
        e.DrawFocusRectangle()

        Dim pos As ElementPosition = CType(Me.Items(e.Index), ElementPosition)
        If pos Is Nothing Then Return

        ' Adjust brushes depending on item highlights to maintain readability
        Dim textColor As Color = If((e.State And DrawItemState.Selected) = DrawItemState.Selected, SystemColors.HighlightText, Me.ForeColor)
        Dim textBrush As New SolidBrush(textColor)
        Dim borderPen As New Pen(textColor, kBorderPenWidth)
        Dim elementBrush As New SolidBrush(Color.FromArgb(100, textColor)) ' Semi-transparent element area fill

        Try
            ' 1. Draw outer boundary box (representing full canvas space)
            Dim chartRect As New Rectangle(e.Bounds.X + kPreviewRectangleXOffset, e.Bounds.Y + kPreviewRectangleYOffset, kPreviewRectangleWidth, kPreviewRectangleHeight)
            e.Graphics.DrawRectangle(borderPen, chartRect)

            ' 2. Draw interior element representation
            If pos.Auto Then
                ' Draw a delicate crosshairs effect inside the preview boundaries for Auto layout mode
                Using dashPen As New Pen(Color.FromArgb(140, textColor), kDashPenWidth)
                    dashPen.DashStyle = Drawing2D.DashStyle.Dot
                    e.Graphics.DrawLine(dashPen, chartRect.X, chartRect.Y, chartRect.Right, chartRect.Bottom)
                    e.Graphics.DrawLine(dashPen, chartRect.Right, chartRect.Y, chartRect.X, chartRect.Bottom)
                End Using
            Else
                ' ElementPosition property floats map directly to relative layout scale percentages (0 to 100)
                Dim elemX As Single = chartRect.X + (pos.X / 100.0!) * chartRect.Width
                Dim elemY As Single = chartRect.Y + (pos.Y / 100.0!) * chartRect.Height
                Dim elemWidth As Single = (pos.Width / 100.0!) * chartRect.Width
                Dim elemHeight As Single = (pos.Height / 100.0!) * chartRect.Height

                ' Ensure bounding visibility even for razor-thin configurations
                If elemWidth < 1.0! Then elemWidth = 1.0!
                If elemHeight < 1.0! Then elemHeight = 1.0!

                Dim elemRect As New RectangleF(elemX, elemY, elemWidth, elemHeight)
                e.Graphics.FillRectangle(elementBrush, elemRect)
                e.Graphics.DrawRectangle(borderPen, elemRect.X, elemRect.Y, elemRect.Width, elemRect.Height)
            End If

            ' 3. Render description text alignment
            Dim textRect As New Rectangle(e.Bounds.X + kTextRectangleXOffset, e.Bounds.Y, e.Bounds.Width + kTextRectangleWidthOffset, e.Bounds.Height)
            Dim itemText As String = GetPositionText(pos)

            Using sf As New StringFormat()
                sf.LineAlignment = StringAlignment.Center
                e.Graphics.DrawString(itemText, e.Font, textBrush, textRect, sf)
            End Using

        Finally
            textBrush.Dispose()
            borderPen.Dispose()
            elementBrush.Dispose()
        End Try
    End Sub

    Private Function GetPositionText(ByVal pos As ElementPosition) As String
        If pos Is Nothing OrElse pos.Auto Then Return "Auto"

        ' Verify presets to show concise human names
        If pos.X = 0 AndAlso pos.Y = 0 AndAlso pos.Width = 30 AndAlso pos.Height = 15 Then Return "Top Left"
        If pos.X = 70 AndAlso pos.Y = 0 AndAlso pos.Width = 30 AndAlso pos.Height = 15 Then Return "Top Right"
        If pos.X = 0 AndAlso pos.Y = 85 AndAlso pos.Width = 30 AndAlso pos.Height = 15 Then Return "Bottom Left"
        If pos.X = 70 AndAlso pos.Y = 85 AndAlso pos.Width = 30 AndAlso pos.Height = 15 Then Return "Bottom Right"
        If pos.X = 35 AndAlso pos.Y = 35 AndAlso pos.Width = 30 AndAlso pos.Height = 30 Then Return "Center"

        ' Fallback output for clean, custom coordinates layout
        Return String.Format("Custom ({0:0}, {1:0}, {2:0}, {3:0})", pos.X, pos.Y, pos.Width, pos.Height)
    End Function
End Class
