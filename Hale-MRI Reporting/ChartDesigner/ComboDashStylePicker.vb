Imports System.ComponentModel
Imports System.Drawing.Drawing2D

Public Class ComboDashStylePicker
    Inherits ComboBox

    Private Const kItemHeightDefault As Integer = 18

    Public Sub New()
        MyBase.New()
        InitializeComponent()

        ' Configure the control for a custom list presentation.
        Me.DropDownStyle = ComboBoxStyle.DropDownList
        Me.DrawMode = DrawMode.OwnerDrawFixed
        Me.ItemHeight = kItemHeightDefault ' Expanded vertical bounds to fit custom line dash patterns comfortably.

        ' Automatically bind all available DashStyle enumeration values.
        Me.DataSource = [Enum].GetValues(GetType(DashStyle))
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

    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    <Browsable(False)>
    Public Property DashStyle As DashStyle
        Get
            If Me.SelectedItem IsNot Nothing Then
                Return CType(Me.SelectedItem, DashStyle)
            End If
            Return DashStyle.Solid
        End Get
        Set(value As DashStyle)
            If Me.DataSource IsNot Nothing Then
                Me.SelectedItem = value
            End If
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
        ' Safeguard against drawing out-of-bounds or empty structures
        If e.Index < 0 Then Return

        ' Render standard system backgrounds and focus visuals matching item states
        e.DrawBackground()
        e.DrawFocusRectangle()

        Dim g As Graphics = e.Graphics
        Dim currentStyle As DashStyle = CType(Me.Items(e.Index), DashStyle)

        ' Segment the bounds: a small left container for graphics and a right container for text labels
        Dim lineBounds As New Rectangle(e.Bounds.X + 6, e.Bounds.Y + 2, 40, e.Bounds.Height - 4)
        Dim textBounds As New Rectangle(e.Bounds.X + 54, e.Bounds.Y, e.Bounds.Width - 54, e.Bounds.Height)

        ' Apply anti-aliasing context variables cleanly to secure visual consistency
        Dim originalSmoothing As SmoothingMode = g.SmoothingMode
        g.SmoothingMode = SmoothingMode.AntiAlias

        ' Draw the custom line preview matching the current index's enum state
        Using linePen As New Pen(e.ForeColor, 2)
            Try
                If currentStyle = DashStyle.Custom Then
                    ' Assign a placeholder dash-dot pattern for Custom to demonstrate capability cleanly
                    linePen.DashStyle = DashStyle.Custom
                    linePen.DashPattern = New Single() {4.0F, 2.0F, 1.0F, 2.0F}
                Else
                    linePen.DashStyle = currentStyle
                End If

                ' Draw a horizontal line path exactly centered vertically inside the preview region
                Dim yMiddle As Integer = lineBounds.Y + (lineBounds.Height \ 2)
                g.DrawLine(linePen, lineBounds.Left, yMiddle, lineBounds.Right, yMiddle)
            Catch
                ' Graceful fallback drawing style to prevent control failure during designer glitches
                Dim yMiddle As Integer = lineBounds.Y + (lineBounds.Height \ 2)
                g.DrawLine(Pens.Gray, lineBounds.Left, yMiddle, lineBounds.Right, yMiddle)
            End Try
        End Using

        ' Revert changes made to global canvas graphics components safely
        g.SmoothingMode = originalSmoothing

        ' Draw the literal text descriptor string next to the vector line graphic
        Dim itemText As String = currentStyle.ToString()
        Using textBrush As New SolidBrush(e.ForeColor)
            Dim sf As New StringFormat() With {
                .LineAlignment = StringAlignment.Center,
                .Alignment = StringAlignment.Near
            }
            Dim targetRectF As New RectangleF(textBounds.X, textBounds.Y, textBounds.Width, textBounds.Height)
            g.DrawString(itemText, e.Font, textBrush, targetRectF, sf)
        End Using
    End Sub
End Class
