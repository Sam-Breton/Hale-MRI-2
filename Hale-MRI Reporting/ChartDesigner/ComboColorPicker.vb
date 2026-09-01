Imports System.ComponentModel

Public Class ComboColorPicker
    Inherits ComboBox

    Public Enum ColorList
        None = 0
        [System] = 1
        Web = 2
        Custom = 3
        All = 4
    End Enum

    Private Const kColorRectangleWidth As Integer = 30
    Private Const kColorRectangleHeightOffset As Integer = -4
    Private Const kColorRectangleXOffset As Integer = 4
    Private Const kColorRectangleYOffset As Integer = 2
    Private Const kColorTextOffset As Integer = 8

    Private mColorList As ColorList = ColorList.None

    Public Sub New()
        MyBase.New()

        ' Set required properties for custom drawing.
        Me.DrawMode = DrawMode.OwnerDrawFixed
        Me.DropDownStyle = ComboBoxStyle.DropDownList
        Me.ValueMember = "ColorValue"
        Me.DisplayMember = "DisplayName"
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

    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    <Browsable(False)>
    Public Property Color As Color
        Get
            ' Cast SelectedItem to the wrapper class, then return its internal ColorValue.
            If Me.SelectedItem IsNot Nothing Then
                Return CType(Me.SelectedItem, CustomColor).ColorValue
            End If
            Return Color.Transparent
        End Get
        Set(value As Color)
            SelectColor(value)
        End Set
    End Property

    <Browsable(True)>
    <Category("Appearance")>
    <Description("The colors that will populate the dropdown list.")>
    Public Property Colors As ColorList
        Get
            Return mColorList
        End Get
        Set(value As ColorList)
            PopulateColors(value)
            mColorList = value
        End Set
    End Property

    Public Sub InsertColor(ByVal customColor As CustomColor)
        Me.Items.Insert(0, customColor)
    End Sub

    Public Sub InsertColor(ByVal knownColor As Color)
        Me.Items.Insert(0, New CustomColor(knownColor.Name, knownColor))
    End Sub

    Public Sub InsertColors(ByVal customColors As List(Of CustomColor))
        ' Inserts the given colors at the top of the dropdown list.
        For Each clr As CustomColor In customColors
            Me.InsertColor(clr)
        Next
    End Sub

    Public Sub InsertColors(ByVal knownColors As List(Of Color))
        ' Inserts the given colors at the top of the dropdown list.
        For Each clr As Color In knownColors
            Me.InsertColor(clr)
        Next
    End Sub

    Public Sub SelectColor(ByVal clr As Color)
        If Me.Items.Count > 0 Then
            Dim foundIndex As Integer = -1

            ' Search through the list of items for the given color.
            For i As Integer = 0 To Me.Items.Count - 1
                ' Cast to the wrapper object instead of Color
                Dim item As CustomColor = DirectCast(Me.Items(i), CustomColor)

                ' Compare the internal ARGB value
                If item.ColorValue.ToArgb() = clr.ToArgb() Then
                    foundIndex = i
                    Exit For
                End If
            Next

            ' If found, select the item.
            If foundIndex <> -1 Then
                Me.SelectedIndex = foundIndex
            End If
        End If
    End Sub

    Public Sub SelectIndex(ByVal idx As Integer)
        Me.SelectedIndex = idx
    End Sub

    Private Sub PopulateColors(ByVal filter As ColorList)
        Me.Items.Clear()

        If filter = ColorList.System OrElse filter = ColorList.Web OrElse filter = ColorList.All Then
            ' Extract all named colors available in .NET Framework.
            For Each colorName As String In [Enum].GetNames(GetType(KnownColor))
                Dim knownColor As Color = Color.FromName(colorName)

                If knownColor <> Color.Empty Then
                    Select Case filter
                        Case ColorList.System
                            If knownColor.IsSystemColor Then Me.Items.Add(New CustomColor(colorName, knownColor))
                        Case ColorList.Web
                            If Not knownColor.IsSystemColor Then Me.Items.Add(New CustomColor(colorName, knownColor))
                        Case ColorList.All
                            Me.Items.Add(New CustomColor(colorName, knownColor))
                        Case Else
                    End Select
                End If
            Next
        End If

        If filter = ColorList.Custom OrElse filter = ColorList.All Then
            For Each colorName As String In [Enum].GetNames(GetType(CustomColors))
                Dim customColor As CustomColor = CustomColor.FromName(colorName)

                If customColor IsNot Nothing AndAlso customColor.ColorValue <> Color.Empty Then Me.Items.Add(customColor)
            Next
        End If
    End Sub

    Protected Overrides Sub OnDrawItem(e As DrawItemEventArgs)
        If e.Index < 0 Then Return

        ' Grab the wrapper item instead of a direct Color struct
        Dim item As CustomColor = CType(Me.Items(e.Index), CustomColor)
        Dim itemColor As Color = item.ColorValue
        Dim itemText As String = item.DisplayName

        ' FIX: This line paints the row selection highlight on hover and fixes click processing!
        e.DrawBackground()

        ' --- Your exact rectangle drawing code remains unchanged ---
        Dim rectWidth As Integer = kColorRectangleWidth
        Dim rectHeight As Integer = e.Bounds.Height + kColorRectangleHeightOffset
        Dim colorRect As New Rectangle(e.Bounds.X + kColorRectangleXOffset, e.Bounds.Y + kColorRectangleYOffset, rectWidth, rectHeight)

        Using brush As New SolidBrush(itemColor)
            e.Graphics.FillRectangle(brush, colorRect)
        End Using
        e.Graphics.DrawRectangle(Pens.Black, colorRect)

        ' Set formatting options for text rendering.
        Dim textBrush As Brush
        If (e.State And DrawItemState.Selected) = DrawItemState.Selected Then
            ' Use default highlight text color (White) if selected.
            textBrush = SystemBrushes.HighlightText
        Else
            ' FIX: SystemBrushes.WindowText represents standard dark ComboBox list text
            textBrush = SystemBrushes.WindowText
        End If

        ' Set where the color text label begins.
        Dim textX As Integer = colorRect.Right + kColorTextOffset
        Dim textY As Integer = e.Bounds.Y + ((e.Bounds.Height - Me.Font.Height) \ 2)

        ' Render the color text label.
        e.Graphics.DrawString(itemText, Me.Font, textBrush, textX, textY)

        ' Draw focus rectangle if needed.
        e.DrawFocusRectangle()
    End Sub
End Class
