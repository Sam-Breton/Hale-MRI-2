Imports System.ComponentModel

Public Class ComboFormBorderStylePicker
    Inherits ComboBox

    Private Const kBorderPenWidth As Single = 1.0!
    Private Const kItemHeightDefault As Integer = 18
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

        ' Bind all items in the FormBorderStyle enumeration to the control's data source.
        Me.DataSource = [Enum].GetValues(GetType(FormBorderStyle))
    End Sub

    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    <Browsable(False)>
    Public Property BorderStyle As FormBorderStyle
        Get
            If Me.SelectedItem IsNot Nothing Then
                Return CType(Me.SelectedItem, FormBorderStyle)
            End If
            Return FormBorderStyle.None
        End Get
        Set(value As FormBorderStyle)
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

    Protected Overrides Sub OnDrawItem(ByVal e As DrawItemEventArgs)
        ' Avoid rendering if the index is invalid (e.g., during initialization)
        If e.Index < 0 Then Return

        ' Draw the standard system background and focus rectangles.
        e.DrawBackground()
        e.DrawFocusRectangle()

        Dim g As Graphics = e.Graphics
        Dim itemStyle As FormBorderStyle = CType(Me.Items(e.Index), FormBorderStyle)

        ' 2. Define bounds for the preview thumbnail and the text description.
        Dim iconRect As New Rectangle(e.Bounds.X + kPreviewRectangleXOffset, e.Bounds.Y + kPreviewRectangleYOffset, kPreviewRectangleWidth, kPreviewRectangleHeight)
        Dim textRect As New Rectangle(e.Bounds.X + kTextRectangleXOffset, e.Bounds.Y, e.Bounds.Width + kTextRectangleWidthOffset, e.Bounds.Height)

        ' 3. Draw a unique graphical representation based on the FormBorderStyle.
        Using borderPen As New Pen(Color.Gray, kBorderPenWidth)
            Using titleBrush As New SolidBrush(Color.LightBlue)
                Select Case itemStyle
                    Case FormBorderStyle.None
                        ' Draw a dashed rectangle to signify no actual border.
                        borderPen.DashStyle = Drawing2D.DashStyle.Dash
                        g.DrawRectangle(borderPen, iconRect)

                    Case FormBorderStyle.FixedSingle, FormBorderStyle.Fixed3D, FormBorderStyle.FixedDialog
                        ' Draw a solid window border with a standard title bar accent.
                        g.DrawRectangle(Pens.Black, iconRect)
                        g.FillRectangle(titleBrush, New Rectangle(iconRect.X + 1, iconRect.Y + 1, iconRect.Width - 1, 3))

                    Case FormBorderStyle.Sizable
                        ' Draw a thick/double border to signify resizability.
                        g.DrawRectangle(Pens.Black, iconRect)
                        Dim innerRect As New Rectangle(iconRect.X + 1, iconRect.Y + 1, iconRect.Width - 2, iconRect.Height - 2)
                        g.DrawRectangle(borderPen, innerRect)
                        g.FillRectangle(titleBrush, New Rectangle(iconRect.X + 2, iconRect.Y + 2, iconRect.Width - 3, 3))

                    Case FormBorderStyle.FixedToolWindow, FormBorderStyle.SizableToolWindow
                        ' Draw a window shell with a thinner utility title bar accent.
                        g.DrawRectangle(Pens.Black, iconRect)
                        g.FillRectangle(Brushes.SlateGray, New Rectangle(iconRect.X + 1, iconRect.Y + 1, iconRect.Width - 1, 2))
                End Select
            End Using
        End Using

        ' 4. Draw the actual item string text next to your graphic.
        Dim itemText As String = itemStyle.ToString()
        Using textBrush As New SolidBrush(e.ForeColor)
            Dim sf As New StringFormat() With {.LineAlignment = StringAlignment.Center}
            g.DrawString(itemText, e.Font, textBrush, New RectangleF(textRect.X, textRect.Y, textRect.Width, textRect.Height), sf)
        End Using
    End Sub
End Class
