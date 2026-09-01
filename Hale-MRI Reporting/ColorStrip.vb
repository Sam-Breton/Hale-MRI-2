Public Class ColorStrip
    Inherits Control

    Private mColors As Color() = {Color.Red, Color.Green, Color.Blue}
    Private mColorPickerForm As FrmColorStripColorPicker

    Public Property Colors As Color()
        Get
            Return mColors
        End Get
        Set(value As Color())
            If value IsNot Nothing AndAlso value.Length > 0 Then
                mColors = value
            Else
                mColors = {Color.Transparent}
            End If
            Invalidate() ' Redraw control when colors change.
        End Set
    End Property

    Public Sub New()
        ' Reduce flicker
        InitializeComponent()
        Me.SetStyle(ControlStyles.AllPaintingInWmPaint Or
                    ControlStyles.UserPaint Or
                    ControlStyles.OptimizedDoubleBuffer, True)
    End Sub

    Private Sub ColorPickerShow()
        mColorPickerForm = New FrmColorStripColorPicker()
        mColorPickerForm.Colors = mColors
        If mColorPickerForm.ShowDialog() = DialogResult.OK Then
            Me.Colors = mColorPickerForm.Colors
        End If
        mColorPickerForm.Dispose()
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)

        If mColors Is Nothing OrElse mColors.Length = 0 Then Return

        Dim rectWidth As Integer = Math.Max(1, Width \ mColors.Length)
        Dim rectHeight As Integer = Height

        For i As Integer = 0 To mColors.Length - 1
            Dim x As Integer = i * rectWidth
            Dim r As New Rectangle(x, 0, rectWidth, rectHeight)

            Using b As New SolidBrush(mColors(i))
                e.Graphics.FillRectangle(b, r)
            End Using

            ' Optional: draw a thin border around each color block
            Using p As New Pen(Color.FromArgb(50, Color.Black))
                e.Graphics.DrawRectangle(p, r)
            End Using
        Next
    End Sub

    Private Sub ComboColorPicker_SelectionChangeCommitted(sender As Object, e As EventArgs)
        Try
            Dim combo As ComboColorPicker = DirectCast(sender, ComboColorPicker)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ColorStrip_DoubleClick(sender As Object, e As MouseEventArgs) Handles Me.DoubleClick
        Try
            ColorPickerShow()
        Catch ex As Exception

        End Try
    End Sub
End Class
