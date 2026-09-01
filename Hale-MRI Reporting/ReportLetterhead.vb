Imports System.ComponentModel
Imports LibGlobals

Public Class ReportLetterhead
    Inherits PictureBox

#Region "Types and Constants"
    ''' <summary>
    ''' Event to signal a BorderStyle change.
    ''' </summary>
    Public Event BorderStyleChanged As EventHandler

    ''' <summary>
    ''' Event to signal an Image/ImageLocation change.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Public Event ImageChanged(sender As Object, e As EventArgs)

    Private Const kVerticalSeparationDefault As Integer = 20
#End Region
#Region "Private Members"
    Private mSelectedBorderStyle As BorderStyle = BorderStyle.None
    Private mSelectedSizeMode As PictureBoxSizeMode = PictureBoxSizeMode.Normal
#End Region
#Region "Constructors"
    ''' <summary>
    '''  Default constructor.
    ''' </summary>
    Public Sub New()
        InitializeComponent()
    End Sub
#End Region
#Region "Public Interface"
    ''' <summary>
    ''' The default font for any Controls.
    ''' </summary>
    ''' <returns></returns>
    <Browsable(True)>
    <Category("Settings")>
    <Description("The default font for any Controls.")>
    Public Property BaseFont As Font

    Public Property BaseLocation As Point = Point.Empty

    Public Property BaseSize As Size = Size.Empty

    Public ReadOnly Property ContextMenuItem(ByVal name As String) As ToolStripMenuItem
        Get
            Return If(Me.ContextMenuStrip IsNot Nothing, Me.ContextMenuStrip.Items.Find(name, True).FirstOrDefault(), Nothing)
        End Get
    End Property

    ''' <summary>
    ''' Sets the BorderStyle and raises the BorderStyleChanged event.
    ''' </summary>
    ''' <returns></returns>
    Public Shadows Property BorderStyle As BorderStyle
        Get
            Return MyBase.BorderStyle
        End Get
        Set(value As BorderStyle)
            If MyBase.BorderStyle <> value Then
                MyBase.BorderStyle = value
                mSelectedBorderStyle = value
                ' Raise the event whenever the value actually changes.
                RaiseEvent BorderStyleChanged(Me, EventArgs.Empty)
            End If
        End Set
    End Property

    ''' <summary>
    ''' Draws a high-resolution bitmap for screen rendering and printing.
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

    Public Shadows Property Image() As Image
        Get
            Return MyBase.Image
        End Get
        Set(ByVal value As Image)
            ' Only trigger if the image is actually different
            If MyBase.Image IsNot value Then
                MyBase.Image = If(value, Me.InitialImage) ' Avoid null reference issues in DrawToGraphics
                ' Raise the custom event
                RaiseEvent ImageChanged(Me, EventArgs.Empty)
            End If
        End Set
    End Property

    Public Shadows Property ImageLocation() As String
        Get
            Return MyBase.ImageLocation
        End Get
        Set(ByVal value As String)
            ' Trigger the event only if the path actually changes
            If MyBase.ImageLocation <> value Then
                MyBase.ImageLocation = value
                ' Note: MyBase.ImageLocation internally updates the Image property 
                ' when the load is complete, but triggering here notifies the UI
                ' that a change has been initiated.
                RaiseEvent ImageChanged(Me, EventArgs.Empty)
            End If
        End Set
    End Property

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

    ''' <summary>
    ''' The size of the gap between the bottom of the ReportLetterhead and any other Controls.
    ''' </summary>
    ''' <returns></returns>
    <Browsable(True)>
    <Category("Settings")>
    <Description("The size of the gap between the bottom of the ReportLetterhead and any other Controls.")>
    Public Property VerticalSeparation As Integer = kVerticalSeparationDefault
#End Region
#Region "Event Handlers"
    Protected Overrides Sub OnVisibleChanged(e As EventArgs)
        MyBase.OnVisibleChanged(e)
        If Me.Visible Then
            If Me.Image Is Nothing Then
                ' If no image is selected, show the placeholder image and border so the user knows it's there. 
                Me.BorderStyle = BorderStyle.FixedSingle
                Me.Image = Nothing
                Me.SizeMode = PictureBoxSizeMode.CenterImage
            Else
                ' If an image is already selected, apply the selected settings.
                Me.BorderStyle = mSelectedBorderStyle
                Me.SizeMode = mSelectedSizeMode
            End If
        End If
    End Sub
#End Region
End Class
