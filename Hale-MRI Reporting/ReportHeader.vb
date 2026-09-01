Imports System.Collections.ObjectModel
Imports System.Collections.Specialized
Imports System.ComponentModel
Imports LibDatabase.Models
Imports LibDatabase.BindingSourceExtensions
Imports LibDatabase.Constants
Imports LibGlobals

Public Class ReportHeader
    Inherits UserControl
    ' TODO: the control needs a light dashed border to give visual cues 
    ' as to its bounds to the user at runtime. See CustomPanel.
#Region "Types and Constants"
    Private Const kFontZoomMax As Single = 3.0F             ' Max font scaling factor.
    Private Const kFontZoomMin As Single = 0.33F            ' Min font scaling factor.
    Private Const kVerticalSeparationDefault As Integer = 0 ' Default bottom edge vertical spacing between this and any other controls.

    ''' <summary>
    ''' Event to signal a BorderStyle change.
    ''' </summary>
    Public Event BorderStyleChanged As EventHandler

    ''' <summary>
    ''' Event to signal a DataSource change.
    ''' </summary>
    Public Event DataSourceChanged As EventHandler

    ''' <summary>
    ''' Type that aggregates controls that display a value along with a Label.
    ''' LabelControls are linked to ValueControls whose Name matches the 
    ''' Label's Tag. 
    ''' </summary>
    Public Class HeaderControl
        ''' <summary>
        ''' The Label that display's our Name.
        ''' </summary>
        ''' <returns></returns>
        Public Property LabelControl As Label

        ''' <summary>
        ''' The HeaderControl's Label used by the program.
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property Name As String
            Get
                Return LabelControl?.Text
            End Get
        End Property

        ''' <summary>
        ''' The HeaderControl's Tag used by the database.
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property Value As String
            Get
                Return ValueControl?.Tag.ToString()
            End Get
        End Property

        ''' <summary>
        ''' The Control that displays a value.
        ''' </summary>
        ''' <returns></returns>
        Public Property ValueControl As Control
        ''' <summary>
        ''' The size of the gap between the bottom of the ReportLetterhead and any other Controls.
        ''' </summary>
        ''' <returns></returns>
        <Browsable(True)>
        <Category("Settings")>
        <Description("The size of the gap between the bottom of the ReportLetterhead and any other Controls.")>
        Public Property VerticalSeparation As Integer = kVerticalSeparationDefault

        ''' <summary>
        ''' Indicates whether the HeaderControl is currently visible.
        ''' </summary>
        ''' <returns></returns>
        Public Property Visible As Boolean
            Get
                Return Me.ValueControl.Visible
            End Get
            Set(value As Boolean)
                If Me.ValueControl IsNot Nothing Then Me.ValueControl.Visible = value
                If Me.LabelControl IsNot Nothing Then Me.LabelControl.Visible = value
            End Set
        End Property
    End Class
#End Region
#Region "Private Members"
    Private mHeaderControls As New List(Of HeaderControl)
    Private mJobDetails As JobDetail = Nothing
    Private WithEvents mVisibleControls As New ObservableCollection(Of HeaderControl)
#End Region
#Region "Constructors"
    ''' <summary>
    ''' Default constructor.
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
    <Description("The defualt font for any Controls.")>
    Public Property BaseFont As Font

    Public Property BaseLocation As Point = Point.Empty

    Public Property BaseSize As Size = Size.Empty
#End Region
#Region "Header Methods and Properties"
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
                ' Raise the event whenever the value actually changes.
                RaiseEvent BorderStyleChanged(Me, EventArgs.Empty)
            End If
        End Set
    End Property

    ''' <summary>
    ''' The ReportHeader's list of visible items.
    ''' </summary>
    ''' <returns></returns>
    Public Property DataSource As BindingList(Of HeaderView)
        Get
            Return JobDetailsBindingSource.DataSource
        End Get
        Set(value As BindingList(Of HeaderView))
            Dim currentId = If(JobDetailsBindingSource.Current(Of HeaderView)?.Id, kNoCurrentRecord)
            Dim valueId = If(value?.FirstOrDefault()?.Id, kNoCurrentRecord)
            If valueId <> currentId Then
                JobDetailsBindingSource.DataSource = If(value, New BindingList(Of HeaderView))
                RaiseEvent DataSourceChanged(Me, EventArgs.Empty)
            End If
        End Set
    End Property

    ''' <summary>
    ''' Returns our ContextMenuStrip item whose name matches the given name. 
    ''' </summary>
    ''' <param name="name"></param>
    ''' <returns>ToolStripMenuItem</returns>
    Public ReadOnly Property ContextMenuItem(ByVal name As String) As ToolStripMenuItem
        Get
            Return If(Me.ContextMenuStrip IsNot Nothing, Me.ContextMenuStrip.Items.Find(name, True).FirstOrDefault(), Nothing)
        End Get
    End Property

    ''' <summary>
    ''' List of all available HeaderControls.
    ''' </summary>
    ''' <returns>List(Of HeaderControl)</returns>
    Public ReadOnly Property HeaderControls As List(Of HeaderControl)
        Get
            Return mHeaderControls
        End Get
    End Property

    ''' <summary>
    ''' Returns the HeaderControl whose Name property matches the given name.
    ''' </summary>
    ''' <param name="name"></param>
    ''' <returns>HeaderControl</returns>
    Public ReadOnly Property Item(name As String) As HeaderControl
        Get
            Return mHeaderControls.FirstOrDefault(Function(hc) hc.Name = name)
        End Get
    End Property

    Public Property JobDetails As JobDetail
        Get
            Return mJobDetails
        End Get
        Set(value As JobDetail)
            mJobDetails = value
        End Set
    End Property
    ''' <summary>
    ''' Returns the HeaderControl whose Value property matches the given name.
    ''' </summary>
    ''' <param name="name"></param>
    ''' <returns>HeaderControl</returns>
    Public ReadOnly Property Value(name As String) As HeaderControl
        Get
            Return mHeaderControls.FirstOrDefault(Function(hc) hc.Value = name)
        End Get
    End Property


    ''' <summary>
    ''' The collection of currently visible HeaderControls.
    ''' </summary>
    ''' <returns>ObservableCollection(Of HeaderControl)</returns>
    Public ReadOnly Property VisibleControls As ObservableCollection(Of HeaderControl)
        Get
            Return mVisibleControls
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

    ''' <summary>
    ''' The size of the gap between the bottom of the ReportLetterhead and any other Controls.
    ''' </summary>
    ''' <returns></returns>
    <Browsable(True)>
    <Category("Settings")>
    <Description("The size of the gap between the bottom of the ReportLetterhead and any other Controls.")>
    Public Property VerticalSeparation As Integer = kVerticalSeparationDefault

    ''' <summary>
    ''' Semi-colon delimited string of visible HeaderControls by Value property.
    ''' </summary>
    ''' <returns>String</returns>
    Public Property VisibleItems As String
        Get
            Dim items As List(Of String) = Me.VisibleControls.Select(Function(hc) hc.Value).ToList()
            Return String.Join(";"c, items)
        End Get
        Set(value As String)
            'Me.SuspendLayout()
            Try
                Me.VisibleControls.Clear()
                Dim items As List(Of String) = value.Split(";"c, StringSplitOptions.RemoveEmptyEntries).ToList()
                For Each item As String In items
                    Dim hc As HeaderControl = Me.Value(item)
                    If hc IsNot Nothing Then
                        Me.VisibleControls.Add(hc)
                    End If
                Next
            Finally
                'Me.ResumeLayout(False)
            End Try
        End Set
    End Property

#End Region
#Region "Private Interface"
    Private Sub FontSet()
        ' Scale control font sizes according to the current Header size.
        If Me.BaseSize.Height > 0 Then
            Dim scaleFactor As Single = Math.Round(Math.Clamp(Me.ClientRectangle.Height / Me.BaseSize.Height, kFontZoomMin, kFontZoomMax), 2)

            Me.SuspendLayout() ' TODO: Some glitching on ReportLoad()
            Try
                For Each ctrl As Control In Me.PanelHeaderLayout.Controls
                    Dim newFontSize As Single = ctrl.Font.Size * scaleFactor
                    ctrl.Font = New Font(ctrl.Font.FontFamily, newFontSize, ctrl.Font.Style)
                Next
            Finally
                Me.ResumeLayout(False) ' TODO: Some glitching on ReportLoad()
            End Try
        End If
    End Sub

    Private Sub DataSourceSet(bs As BindingSource)

    End Sub

    'Private Property DesignDimensions As Dimensions

    Private Sub InitializeControls()
        ' Initializes our list of HeaderControls by iterating through
        ' Labels whose Tag property is the Name of its
        ' associated value Control.
        For Each lab As Label In Me.PanelHeaderLayout.Controls.OfType(Of Label)()
            Dim ctrl As Control = Me.PanelHeaderLayout.Controls(lab.Tag.ToString())
            If ctrl IsNot Nothing Then
                Dim hc As New HeaderControl() With {.LabelControl = lab, .ValueControl = ctrl}
                Me.HeaderControls.Add(hc)
            End If
        Next
        Me.VisibleControls.Clear()
    End Sub

    Private Sub VisibleControlAdded(hc As HeaderControl)
        hc.Visible = True
        'Debug.WriteLine($"VisibleControlAdded: {hc.LabelControl.Name} ({hc.LabelControl.Visible}) {hc.LabelControl.Font} {hc.ValueControl.Name} ({hc.ValueControl.Visible}) {hc.ValueControl.Font}")
    End Sub

    Private Sub VisibleControlRemoved(hc As HeaderControl)
        hc.Visible = False
        'Debug.WriteLine($"VisibleControlRemoved: {hc.LabelControl.Name} ({hc.LabelControl.Visible}) {hc.ValueControl.Name} ({hc.ValueControl.Visible})")
    End Sub

    Protected Overrides Sub OnHandleCreated(e As EventArgs)
        MyBase.OnHandleCreated(e)
        If Not Me.DesignMode Then InitializeControls()
    End Sub

    Protected Overrides Sub OnSizeChanged(e As EventArgs)
        MyBase.OnSizeChanged(e)
        If Not Me.DesignMode Then FontSet()
    End Sub
#End Region
#Region "Event Handlers"
    Protected Overrides Sub OnVisibleChanged(e As EventArgs)
        MyBase.OnVisibleChanged(e)
    End Sub

    Private Sub VisibleControls_CollectionChanged(sender As Object, e As NotifyCollectionChangedEventArgs) Handles mVisibleControls.CollectionChanged
        ' Adds/removes HeaderControls to/from the collection of VisibleControls.
        Select Case e.Action
            Case NotifyCollectionChangedAction.Add
                If e.NewItems IsNot Nothing Then
                    For Each hc As HeaderControl In e.NewItems
                        VisibleControlAdded(hc)
                    Next
                End If
            Case NotifyCollectionChangedAction.Remove
                If e.OldItems IsNot Nothing Then
                    For Each hc As HeaderControl In e.OldItems
                        VisibleControlRemoved(hc)
                    Next
                End If
            Case NotifyCollectionChangedAction.Replace
                If e.OldItems IsNot Nothing Then
                    For Each hc As HeaderControl In e.OldItems
                        VisibleControlRemoved(hc)
                    Next
                End If
                If e.NewItems IsNot Nothing Then
                    For Each hc As HeaderControl In e.NewItems
                        VisibleControlAdded(hc)
                    Next
                End If
            Case NotifyCollectionChangedAction.Reset
                For Each hc As HeaderControl In Me.HeaderControls
                    VisibleControlRemoved(hc)
                Next
        End Select
    End Sub
#End Region
End Class
