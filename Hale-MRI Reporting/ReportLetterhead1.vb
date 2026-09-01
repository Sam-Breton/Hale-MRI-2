Public Class ReportLetterhead1
    Inherits UserControl
    Implements IZoomable

#Region "Public Interface"
#Region "IZoomable"
    Public Sub ApplyZoom(ByVal scale As SizeF) Implements IZoomable.ApplyZoom
        Dim newX As Integer = CInt(Math.Round(Me.BaseLocation.X * scale.Width))
        Dim newY As Integer = CInt(Math.Round(Me.BaseLocation.Y * scale.Height))
        Dim newW As Integer = CInt(Math.Round(Me.OriginalSize.Width * scale.Width))
        Dim newH As Integer = CInt(Math.Round(Me.OriginalSize.Height * scale.Height))

        ' 2. Apply the new Bounds
        Me.SetBounds(newX, newY, newW, newH)

        ' 3. Refresh the Non-Client area
        ' Even if they don't have a "Selection Border", calling this 
        ' ensures Windows synchronizes the window frame with the new size.
        ' &H37 = NOMOVE | NOSIZE | NOZORDER | NOACTIVATE | FRAMECHANGED
        NativeMethods.SetWindowPos(Me.Handle, IntPtr.Zero, 0, 0, 0, 0, &H37)
    End Sub

    Public Property BaseLocation As Point Implements IZoomable.BaseLocation

    Public Property BaseSize As Size Implements IZoomable.BaseSize

    Public Property OriginalSize As Size Implements IZoomable.OriginalSize

    Public ReadOnly Property ScaleSize As Boolean Implements IZoomable.ScaleSize
        Get
            Throw New NotImplementedException()
        End Get
    End Property
#End Region
#Region "Control Methods and Properties"
    Public ReadOnly Property CustomPanel As CustomPanel
        Get
            Return Me.CustomPanel1
        End Get
    End Property

    ''' <summary>
    ''' Draws a high-resolution bitmap for printing.
    ''' </summary>
    ''' <param name="g"></param>
    ''' <param name="targetBounds"></param>
    Public Sub DrawToGraphics(g As Graphics, targetBounds As Rectangle)
        ' 1. Use the ACTUAL DPI of the destination (Printer or Screen)
        Dim targetDpiX As Single = g.DpiX
        Dim targetDpiY As Single = g.DpiY

        ' 2. Calculate scale based on the difference from standard Screen (96 DPI)
        Dim scaleX As Single = targetDpiX / 96.0F
        Dim scaleY As Single = targetDpiY / 96.0F

        Dim bmpW As Integer = CInt(Math.Round(Me.Width * scaleX))
        Dim bmpH As Integer = CInt(Math.Round(Me.Height * scaleY))

        ' Prevent 0-sized bitmaps
        If bmpW <= 0 OrElse bmpH <= 0 Then Exit Sub
        Using bmp As New Bitmap(bmpW, bmpH)
            ' 1. Set the bitmap resolution to match the printer exactly
            bmp.SetResolution(targetDpiX, targetDpiY)

            ' 2. Ensures no gray "Control" color prints
            Using gBmp = Graphics.FromImage(bmp)
                gBmp.Clear(Color.White) ' Ensures no gray "Control" color prints
            End Using

            ' 3. Capture the control at the target's native resolution
            Me.DrawToBitmap(bmp, New Rectangle(0, 0, bmpW, bmpH))

            ' Optional: Ensures best quality if the printer is 1200+ DPI
            g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic

            ' 4. Draw the high-res capture into the page bounds
            ' Note: DrawImage handles the scaling from the Bitmap's DPI to the Graphics' DPI
            g.DrawImage(bmp, targetBounds)
        End Using
    End Sub

    ''' <summary>
    ''' Wrapper for DrawToGraphics for GDI+ rendering.
    ''' </summary>
    ''' <param name="g"></param>
    ''' <param name="bounds"></param>
    Public Sub RenderTo(g As Graphics, bounds As Rectangle)
        ' 1. Draw the high-res raster capture of the WinForms controls
        Me.DrawToGraphics(g, bounds)

        ' 2. Draw vector overlays (Lines/Text) if needed
        ' We save the state and translate so manual drawing is relative to (0,0) of the control
        Dim state = g.Save()
        g.TranslateTransform(bounds.X, bounds.Y)

        ' Manual GDI+ drawing (Always native resolution)
        ' g.DrawString("Overlay", Me.Font, Brushes.Black, 0, 0)

        g.Restore(state)
    End Sub

    Public ReadOnly Property PictureBox As PictureBox
        Get
            Return Me.PictureLetterhead
        End Get
    End Property

    Public ReadOnly Property ContextMenuItem(ByVal name As String) As ToolStripMenuItem
        Get
            Return DirectCast(Me.ContextMenuStrip.Items(name), ToolStripMenuItem)
        End Get
    End Property

    ''' <summary>
    ''' The size of the gap between the bottom of the ReportLetterhead and any other Controls.
    ''' </summary>
    ''' <returns></returns>
    Public Property VerticalSeparation As Integer = 20

    Private Sub PictureLetterhead_LoadCompleted(sender As Object, e As System.ComponentModel.AsyncCompletedEventArgs) Handles PictureLetterhead.LoadCompleted
        If PictureLetterhead.Image Is Nothing Then
            'Me.CustomPanel.BorderStyle = BorderStyle.None
            Me.CustomPanel.DashStyle = Drawing2D.DashStyle.Dash
        Else
            'Me.CustomPanel.BorderStyle = BorderStyle.None
            Me.CustomPanel.DashStyle = Drawing2D.DashStyle.Custom
            Me.CustomPanel.DashPattern = {1.0F, 10.0F}
        End If
    End Sub
#End Region
#End Region
End Class
