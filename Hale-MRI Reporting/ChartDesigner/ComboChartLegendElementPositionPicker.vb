Imports System.ComponentModel
Imports System.Windows.Forms.DataVisualization.Charting

Public Class ComboChartLegendElementPositionPicker
    Inherits ComboBox

    Private Const kItemHeightDefault As Integer = 22 ' Extra space for a clear, high-contrast layout map

    ' Internal structure to bind a human-friendly name to the actual ElementPosition data
    Private Class PositionItem
        Public Property Name As String
        Public Property Position As ElementPosition

        Public Sub New(displayName As String, pos As ElementPosition)
            Me.Name = displayName
            Me.Position = pos
        End Sub

        Public Overrides Function ToString() As String
            Return Me.Name
        End Function
    End Class

    Public Sub New()
        MyBase.New()
        InitializeComponent()

        ' Configure required custom drawing settings
        Me.DrawMode = DrawMode.OwnerDrawFixed
        Me.DropDownStyle = ComboBoxStyle.DropDownList
        Me.ItemHeight = kItemHeightDefault

        ' Populate data exclusively at runtime to protect the designer canvas
        If Not Me.DesignMode AndAlso System.ComponentModel.LicenseManager.UsageMode = System.ComponentModel.LicenseUsageMode.Runtime Then
            PopulatePositions()
        End If
    End Sub

    ''' <summary>
    ''' Exposes the strongly-typed ElementPosition property. Hidden from the property grid to prevent design-time conflicts.
    ''' </summary>
    <Browsable(False)>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property Position As ElementPosition
        Get
            If Me.SelectedItem IsNot Nothing Then
                Return CType(Me.SelectedItem, ElementPosition)
            End If
            Return New ElementPosition()
        End Get
        Set(value As ElementPosition)
            ' Guard block to ensure we don't apply values before DataSource binds at runtime
            If Me.DataSource IsNot Nothing Then
                ' Match based on coordinates since instances may differ
                For Each item As PositionItem In Me.Items
                    If item.Position.X = value.X AndAlso
                       item.Position.Y = value.Y AndAlso
                       item.Position.Width = value.Width AndAlso
                       item.Position.Height = value.Height Then
                        Me.SelectedItem = item
                        Exit Property
                    End If
                Next
                ' Fallback if a custom position is pushed programmatically
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

    ''' <summary>
    ''' Instantiates common layout strategies to use as the bound data source.
    ''' </summary>
    Private Sub PopulatePositions()
        Dim itemsList As New List(Of PositionItem)()

        ' 1. Auto Allocation (The standard chart default)
        Dim autoPos As New ElementPosition() ' Auto is signaled when FromRectangle is never explicitly set, or left default
        itemsList.Add(New PositionItem("Automatic", autoPos))

        ' 2. Common Layout Presets (Metrics represent percentages from 0 to 100)
        itemsList.Add(New PositionItem("Full Canvas", New ElementPosition(0, 0, 100, 100)))
        itemsList.Add(New PositionItem("Top Banner", New ElementPosition(0, 0, 100, 20)))
        itemsList.Add(New PositionItem("Bottom Banner", New ElementPosition(0, 80, 100, 20)))
        itemsList.Add(New PositionItem("Left Sidebar", New ElementPosition(0, 0, 25, 100)))
        itemsList.Add(New PositionItem("Right Sidebar", New ElementPosition(75, 0, 25, 100)))
        itemsList.Add(New PositionItem("Centered Content", New ElementPosition(15, 15, 70, 70)))

        Me.DataSource = itemsList
        Me.DisplayMember = "Name"
        Me.ValueMember = "Position"
    End Sub

    ''' <summary>
    ''' Graphically draws each placement option accompanied by a mini layout canvas map.
    ''' </summary>
    Protected Overrides Sub OnDrawItem(e As DrawItemEventArgs)
        ' Safeguard against early draw cycles or unpopulated items
        If e.Index < 0 OrElse Me.Items.Count = 0 OrElse e.Index >= Me.Items.Count Then Return

        e.DrawBackground()

        ' Safely extract our wrapper object
        Dim currentItem As PositionItem = CType(Me.Items(e.Index), PositionItem)
        Dim pos As ElementPosition = currentItem.Position

        ' Determine theme element colors based on hover highlight states
        Dim elementColor As Color = If((e.State And DrawItemState.Selected) = DrawItemState.Selected,
                                        SystemColors.HighlightText,
                                        SystemColors.WindowText)

        ' Layout metrics: text on the left, visual layout map on the right
        Dim textWidth As Integer = CInt(e.Bounds.Width * 0.55)
        Dim textBounds As New Rectangle(e.Bounds.Left + 4, e.Bounds.Top, textWidth - 4, e.Bounds.Height)

        ' Dimension for our miniature "Chart Container" box
        Dim mapWidth As Integer = 24
        Dim mapHeight As Integer = 18
        Dim mapX As Integer = e.Bounds.Left + textWidth + 10
        Dim mapY As Integer = e.Bounds.Top + ((e.Bounds.Height - mapHeight) \ 2)
        Dim containerBounds As New Rectangle(mapX, mapY, mapWidth, mapHeight)

        ' 1. Draw the strategy text name
        Using textBrush As New SolidBrush(elementColor),
              sf As New StringFormat()
            sf.LineAlignment = StringAlignment.Center
            e.Graphics.DrawString(currentItem.Name, e.Font, textBrush, textBounds, sf)
        End Using

        ' 2. Draw the miniature canvas map
        Using outerPen As New Pen(Color.FromArgb(120, elementColor), 1),
              innerPen As New Pen(elementColor, 1),
              fillBrush As New SolidBrush(Color.FromArgb(65, elementColor))

            ' Draw the simulated outer chart canvas bounding container (dashed line style)
            outerPen.DashStyle = Drawing2D.DashStyle.Dot
            e.Graphics.DrawRectangle(outerPen, containerBounds)

            ' If it's the "Automatic" layout configuration, render an abstract visual cue
            If pos.Auto Then
                ' Draw a small 'A' character inside the mini-map to symbolize automatic positioning
                Using fontBrush As New SolidBrush(Color.FromArgb(150, elementColor)),
                      centerSf As New StringFormat()
                    centerSf.Alignment = StringAlignment.Center
                    centerSf.LineAlignment = StringAlignment.Center
                    Using miniFont As New Font(e.Font.FontFamily, e.Font.Size - 2, FontStyle.Italic)
                        e.Graphics.DrawString("A", miniFont, fontBrush, containerBounds, centerSf)
                    End Using
                End Using
            Else
                ' Convert percentage coordinates (0-100) to pixel dimensions relative to our mini container box
                ' Ensure calculations are clamped cleanly inside the layout box borders
                Dim elementX As Integer = containerBounds.Left + CInt((pos.X / 100.0) * containerBounds.Width)
                Dim elementY As Integer = containerBounds.Top + CInt((pos.Y / 100.0) * containerBounds.Height)
                Dim elementW As Integer = CInt((pos.Width / 100.0) * containerBounds.Width)
                Dim elementH As Integer = CInt((pos.Height / 100.0) * containerBounds.Height)

                ' Constrain width/height minimum bounds to 1 pixel so they remain visually legible
                If elementW <= 0 Then elementW = 1
                If elementH <= 0 Then elementH = 1

                ' Handle border bounds overflowing calculations cleanly
                If elementX + elementW > containerBounds.Right Then elementW = containerBounds.Right - elementX
                If elementY + elementH > containerBounds.Bottom Then elementH = containerBounds.Bottom - elementY

                Dim innerElementBounds As New Rectangle(elementX, elementY, elementW, elementH)

                ' Paint the filled miniature block layout position
                e.Graphics.FillRectangle(fillBrush, innerElementBounds)
                e.Graphics.DrawRectangle(innerPen, innerElementBounds)
            End If
        End Using

        e.DrawFocusRectangle()
    End Sub
End Class
