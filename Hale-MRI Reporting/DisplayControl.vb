Imports System.Collections.Specialized
Imports System.ComponentModel
Imports System.Drawing.Design
Imports System.Runtime.InteropServices
Imports System.Windows.Forms.DataVisualization.Charting
Imports LibDatabase.Models
Imports LibGlobals.NativeMethods

Public Class DisplayControl
    Implements IZoomable
    Implements ICloneable
    Implements IEquatable(Of DisplayControl)
    Implements IMessageFilter

#Region "Types and Constants"
    Public Enum BoundsChecks
        None = 0
        Left = &H1
        Right = &H2
        Top = &H4
        Bottom = &H8
        Width = &H16
        Height = &H32
    End Enum

    Public Enum DragTypes
        None = 0
        Move = 1
        Resize = 2
    End Enum

    Public Enum ResizePoints
        None = 0
        RightEdge = 1
        LeftEdge = 2
        TopEdge = 3
        BottomEdge = 4
        TopRightCorner = 5
        BottomRightCorner = 6
        BottomLeftCorner = 7
        TopLeftCorner = 8
    End Enum

    Private Const kControlBorderSizeDefault As Integer = 1
    Private Const kControlBorderSizeMin As Integer = 1
    Private kControlBorderColorDefault As Color = Color.Blue
    Private Const kControlBorderStyleDefault As ButtonBorderStyle = ButtonBorderStyle.Solid
    Private Const kControlDragEdgeSizeDefault As Integer = 5
    Private Const kControlDragEdgeSizeMin As Integer = 5
    Private Const kControlToolTipOffset As Integer = 10
    Private Const kScaleFactorMax As Single = 3.0!
    Private Const kScaleFactorMin As Single = 0.5!
#End Region
#Region "Private Members"
    Private mData As Object = Nothing
    Protected mDisplayInitialized As Boolean = False
    Private mDisplayName As String = Nothing
    Private mDragEdgeSize As Integer = kControlDragEdgeSizeDefault
    Private mDragOffset As Point = Point.Empty
    Private mDragType As DragTypes = DragTypes.None
    Private mEnabledControlNames As New StringCollection()
    Private ReadOnly mOriginalFontSizes As New Dictionary(Of Object, Single)()
    Private mMouseDown As Boolean = False
    Private mMouseEntered As Boolean = False
    Private mResizePoint As ResizePoints = ResizePoints.None
    Private mSelected As Boolean = False
    Private mSelectionBorderColor As Color = kControlBorderColorDefault
    Private mSelectionBorderSize As Integer = kControlBorderSizeDefault
    Private mSelectionBorderStyle As ButtonBorderStyle = kControlBorderStyleDefault
    Private mTipIsVisible As Boolean = False
    Private mToolTip As New ToolTip()
    Private mZoom As Single = 1.0F
#End Region
#Region "Constructors"
    Public Sub New()
        InitializeComponent()

        Me.SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or ControlStyles.OptimizedDoubleBuffer, True)
        ' If a derived controls has a custom ContextMenuStrip, assign it in its constructor before calling this constructor.
    End Sub

    Public Function Clone() As Object Implements ICloneable.Clone
        Dim dc As DisplayControl = DisplayControl.CreateInstance($"{Me.GetType().Namespace}.{Me.Name}")

        dc.SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or ControlStyles.OptimizedDoubleBuffer, True)
        dc.DisplayName = Me.DisplayName
        dc.DragEdgeSize = Me.DragEdgeSize
        dc.IsMovable = Me.IsMovable
        dc.IsSelectable = Me.IsSelectable
        dc.IsSizeable = Me.IsSizeable
        dc.LastPosition = Me.LastPosition
        dc.LastSize = Me.LastSize
        dc.Location = Me.Location
        dc.MaxSize = Me.MaxSize
        dc.MinSize = Me.MinSize
        dc.Name = Me.Name
        dc.Page = Me.Parent
        dc.Selected = Me.Selected
        dc.SelectionBorderColor = Me.SelectionBorderColor
        dc.SelectionBorderSize = Me.SelectionBorderSize
        dc.SelectionBorderStyle = Me.SelectionBorderStyle
        dc.Size = Me.Size

        Return dc
    End Function
#End Region
#Region "Public Interface"
#Region "Factory Methods"
    Public Shared Function CreateInstance(ByVal controlFullName As String) As DisplayControl
        Dim controlType As Type = GetControlType(controlFullName)
        If controlType IsNot Nothing Then
            Dim dc As DisplayControl = TryCast(Activator.CreateInstance(System.Type.GetType(controlFullName, False, True)), DisplayControl)

            If dc IsNot Nothing Then
                dc.Id = Guid.NewGuid() ' Stamp with a fresh identity.
                Return dc
            End If
        End If
        Return Nothing
    End Function

    Public Shared Function GetControlType(controlTypeName As String) As Type
        Return System.Type.GetType(controlTypeName, False, True)
    End Function
#End Region
#Region "IEquatable"
    Public Shared Operator =(a As DisplayControl, b As DisplayControl) As Boolean
        If ReferenceEquals(a, b) Then
            Return True
        ElseIf ReferenceEquals(a, Nothing) OrElse ReferenceEquals(b, Nothing) Then
            Return False
        Else
            Return a.Equals(b)
        End If
    End Operator

    Public Shared Operator <>(a As DisplayControl, b As DisplayControl) As Boolean
        Return Not (a = b)
    End Operator

    Public Overloads Function Equals(other As DisplayControl) As Boolean Implements IEquatable(Of DisplayControl).Equals
        If ReferenceEquals(other, Nothing) Then
            Return False
        End If
        ' Define equality based on the Name and Type properties
        Return Me.Name = other.Name AndAlso Me.Type = other.Type
    End Function

    Public Overrides Function Equals(other As Object) As Boolean
        ' Use the strongly-typed Equals method for the actual comparison.
        Dim pg As DisplayControl = TryCast(other, DisplayControl)
        If pg IsNot Nothing Then
            Return Me.Equals(pg)
        End If
        Return False
    End Function

    Public Overrides Function GetHashCode() As Integer
        ' Generate a hash code based on the same property used for equality.
        Dim hashName = If(Name?.GetHashCode(), 0)
        Dim hashType = If(Type?.GetHashCode(), 0)
        ' Use XOR to combine them
        Return hashName Xor hashType
    End Function
#End Region
#Region "IMessageFilter"
    Private Function IsUsableControl(hWmd As IntPtr) As Boolean
        ' Returns TRUE if EnabledControls list is not empty and control is not in
        ' the list, else FALSE. This allows developers to add additional controls
        ' that behave normally and do not participate in DisplayControl drag, drop,
        ' move and resize operations.
        Dim ctrl As Control = FromHandle(hWmd)

        If ctrl IsNot Nothing AndAlso mEnabledControlNames.Count > 0 Then
            Dim isUseable As Boolean = Not mEnabledControlNames.Contains(ctrl.Name)

            If isUseable Then Me.Cursor = Cursors.Default

            Return isUseable
        End If

        Return False
    End Function

    Public Function PreFilterMessage(ByRef m As Message) As Boolean Implements IMessageFilter.PreFilterMessage
        ' This method intercepts mouse messages for this DisplayControl and its children. It
        ' determines if a mouse event should be raised for the DisplayControl instead of the child control.
        ' This is to facilitate drag/drop and selection behavior for the DisplayControl without interference
        ' and, to enable certain child Controls to receive mouse events.

        ' 1. Calculate coordinate-based hover state (Independent of HWnd)
        Dim mousePos As Point = Me.PointToClient(Control.MousePosition)
        Dim inBounds As Boolean = Me.ClientRectangle.Contains(mousePos)

        ' Handle Enter/Leave based on actual geometry to avoid child-crossing flickers.
        If inBounds Then
            If Not mMouseEntered Then
                mMouseEntered = True
                OnControlMouseEnter(EventArgs.Empty)
            End If
        Else
            If mMouseEntered Then
                mMouseEntered = False
                OnControlMouseLeave(EventArgs.Empty)
            End If
        End If

        ' 2. Target Check: Only interfere if the message is for this control or its children
        ' This prevents the filter from affecting other controls/forms in the app.
        If Not IsChildOf(Me.Handle, m.HWnd) Then Return False

        If IsUsableControl(m.HWnd) Then
            ' If the message is for a child control that is in the EnabledControls list, ignore it
            ' because we wan't the control to handle mouse events.
            Return False
        End If

        ' 3. Identify if this is a Mouse Message.
        ' Range &H200 to &H20E covers Move, L/R/M Down, Up, DoubleClick, and Wheel
        Dim isMouseMsg As Boolean = (m.Msg >= &H200 AndAlso m.Msg <= &H20E) OrElse (m.Msg = WM_MOUSEHOVER)

        If isMouseMsg AndAlso inBounds Then
            ' Process the events for DisplayControl logic.
            Select Case m.Msg
                Case WM_LBUTTONDOWN, WM_RBUTTONDOWN
                    Dim btn = If(m.Msg = WM_LBUTTONDOWN, MouseButtons.Left, MouseButtons.Right)
                    OnControlMouseDown(New MouseEventArgs(btn, 1, mousePos.X, mousePos.Y, 0))

                Case WM_LBUTTONUP, WM_RBUTTONUP
                    Dim btn = If(m.Msg = WM_LBUTTONUP, MouseButtons.Left, MouseButtons.Right)
                    OnControlMouseUp(New MouseEventArgs(btn, 1, mousePos.X, mousePos.Y, 0))

                Case WM_MOUSEMOVE
                    ' 1. Hide the tooltip immediately if it's currently showing.
                    If mTipIsVisible Then
                        mToolTip.Hide(Me)
                        mTipIsVisible = False
                    Else
                        ' 2. RE-ARM: Since the hover message is "one-shot", 
                        ' we must request tracking again now that the mouse is moving again.
                        Dim tme As New TRACK_MOUSE_EVENT()
                        tme.cbSize = Marshal.SizeOf(tme)
                        tme.dwFlags = TME_HOVER Or TME_LEAVE ' Request both Hover and Leave.
                        tme.hwndTrack = m.HWnd
                        tme.dwHoverTime = 400
                        TrackMouseEvent(tme)
                    End If
                    OnControlMouseMove(New MouseEventArgs(MouseButtons.None, 0, mousePos.X, mousePos.Y, 0))

                Case WM_MOUSEHOVER
                    OnControlMouseHover(EventArgs.Empty)
            End Select

            ' SWALLOW: Since it's a mouse message and we're in bounds, return True.
            ' This ensures children never see the move/click/hover.
            Return True
        End If

        ' 4. Let everything else (OnPaint, OnResize, etc.) pass through normally.
        Return False
    End Function
#End Region
#Region "IZoomable"

    ''' <summary>
    ''' The DisplayControl's 1:1 scale location.
    ''' </summary>
    ''' <returns>Point</returns>
    <Browsable(False)>
    Public Property BaseLocation As Point = Point.Empty Implements IZoomable.BaseLocation

    ''' <summary>
    ''' The DisplayControl's 1:1 scale size.
    ''' </summary>
    ''' <returns>Size</returns>
    <Browsable(False)>
    Public Property BaseSize As Size = Size.Empty Implements IZoomable.BaseSize

    Protected Overridable Sub ZoomSet(ByVal factor As Single) Implements IZoomable.ZoomSet
        ' Set the DisplayControl's bounds according to the 'Base' 
        ' properties and the zoom factor.
        Me.SetBounds(
            CInt(Me.BaseLocation.X * factor),
            CInt(Me.BaseLocation.Y * factor),
            CInt(Me.BaseSize.Width * factor),
            CInt(Me.BaseSize.Height * factor)
        )
    End Sub

#End Region
#Region "Control Methods and Properties"
    <Browsable(False)>
    Public Overridable Property Basis As String = Nothing

    <Browsable(False)>
    Public ReadOnly Property BoundsCheck(ByVal newBounds As Rectangle) As BoundsChecks
        Get
            Return CheckBounds(newBounds)
        End Get
    End Property

    <Browsable(False)>
    Public Overridable Property Data As Object
        Get
            Return mData
        End Get
        Set(value As Object)
            mData = value
            DataGet()
            ContextMenuStripSet()
            If mDisplayInitialized Then DataShow()
        End Set
    End Property

    <Browsable(True)>
    <Category("Settings")>
    <Description("The control's size when first instance created.")>
    Public Shadows Property DefaultSize As Size

    <Browsable(True)>
    <Category("Settings")>
    <Description("The control's human-readable name.")>
    Public Property DisplayName As String
        Get
            Return If(String.IsNullOrEmpty(mDisplayName), Me.Name, mDisplayName)
        End Get
        Set(value As String)
            mDisplayName = value
        End Set
    End Property

    <Browsable(True)>
    <Category("Settings")>
    <DefaultValue(1)>
    <Description("The thickness of the draggable edge.")>
    Public Property DragEdgeSize As Integer
        Get
            Return mDragEdgeSize
        End Get
        Set(value As Integer)
            mDragEdgeSize = If(value < kControlDragEdgeSizeMin, kControlDragEdgeSizeMin, value)
        End Set
    End Property

    <Browsable(False)>
    Public ReadOnly Property DragOffset As Point
        Get
            Return mDragOffset
        End Get
    End Property

    <Browsable(False)>
    Public ReadOnly Property DragType As DragTypes
        Get
            Return DragTypeGet()
        End Get
    End Property

    <Category("Settings")>
    <Description("Select the names of controls that will respond to mouse events.")>
    <Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", GetType(UITypeEditor))>
    Public Property EnabledControls As StringCollection
        Get
            Return mEnabledControlNames
        End Get
        Set(value As StringCollection)
            mEnabledControlNames = value
        End Set
    End Property

    <Browsable(False)>
    Public Property Id As Guid

    <Browsable(True)>
    <Category("Settings")>
    <DefaultValue(False)>
    <Description("Indicates whether the control is selectable.")>
    Public Property IsSelectable As Boolean

    <Browsable(True)>
    <Category("Settings")>
    <DefaultValue(False)>
    <Description("Indicates whether the control is moveable.")>
    Public Property IsMovable As Boolean

    <Browsable(True)>
    <Category("Settings")>
    <DefaultValue(False)>
    <Description("Indicates whether the control is resizable.")>
    Public Property IsSizeable As Boolean

    <Browsable(False)>
    Public Property LastPosition As Point = New Point()

    <Browsable(False)>
    Public Property LastSize As Size = New Size()

    <Browsable(True)>
    <Category("Settings")>
    <Description("The control's maximum size.")>
    Public Property MaxSize As Size = New Size()

    <Browsable(True)>
    <Category("Settings")>
    <Description("The control's minimum size.")>
    Public Property MinSize As Size = New Size()

    <Browsable(False)>
    Public Property Page As DocumentPage

    <Browsable(False)>
    Public Overridable Property Precision As Integer? = Nothing

    <Browsable(False)>
    Public ReadOnly Property ResizePoint As ResizePoints
        Get
            Return mResizePoint
        End Get
    End Property

    <Browsable(False)>
    Public Property Selected As Boolean
        Get
            Return mSelected
        End Get
        Set(value As Boolean)
            SelectedSet(value)
            mSelected = value
            DrawCursor(Me.PointToClient(Control.MousePosition)) ' Update cursor immediately on selection change.
        End Set
    End Property

    <Browsable(True)>
    <Category("Settings")>
    <Description("The selection border color.")>
    Public Property SelectionBorderColor As Color
        Get
            Return mSelectionBorderColor
        End Get
        Set(value As Color)
            mSelectionBorderColor = value
            Me.Invalidate(True)
        End Set
    End Property

    <Browsable(True)>
    <Category("Settings")>
    <DefaultValue(1)>
    <Description("The selection border thickness.")>
    Public Property SelectionBorderSize As Integer
        Get
            Return mSelectionBorderSize
        End Get
        Set(value As Integer)
            mSelectionBorderSize = If(value < kControlBorderSizeMin, kControlBorderSizeMin, value)
            Me.Padding = New Padding(mSelectionBorderSize + 1) ' Ensure padding matches border size to prevent border overlap.
            Me.Invalidate(True)
        End Set
    End Property

    <Browsable(True)>
    <Category("Settings")>
    <DefaultValue(ButtonBorderStyle.Solid)>
    <Description("The selection border style.")>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)>
    Public Property SelectionBorderStyle As ButtonBorderStyle
        Get
            Return mSelectionBorderStyle
        End Get
        Set(value As ButtonBorderStyle)
            mSelectionBorderStyle = value
            Me.Invalidate(True)
        End Set
    End Property

    <Browsable(False)>
    Public Overridable Property TolClass As Tolerance = Nothing

    <Browsable(False)>
    Public ReadOnly Property Type As String
        Get
            Return Me.GetType().ToString()
        End Get
    End Property

    <Browsable(False)>
    Public ReadOnly Property ZoomFactor As SizeF
        Get
            Return New SizeF(Me.Width / Me.BaseSize.Width, Me.Height / Me.BaseSize.Height)
        End Get
    End Property

    <Browsable(False)>
    Public Property Zoom As Single
        Get
            Return mZoom
        End Get
        Set(value As Single)
            ZoomSet(value)
            mZoom = value
        End Set
    End Property

    <Browsable(False)>
    Public ReadOnly Property ZOrder As Integer
        Get
            If Me.Parent Is Nothing Then
                Return -1
            End If
            Return Me.Parent.Controls.GetChildIndex(Me)
        End Get
    End Property
#End Region
#End Region
#Region "Private Interface"
    Private Function CheckBounds(ByVal newBounds As Rectangle) As BoundsChecks
        ' Checks whether this DisplayControls's bounds are within its parent
        ' DocumentPage's bounds and returns a bitwise value (see BoundsChecks).
        Dim result As BoundsChecks = BoundsChecks.None
        Dim parentPage As DocumentPage = DirectCast(Me.Parent, DocumentPage)
        Dim pageBounds = New Rectangle(
            parentPage.ClientRectangle.Left,
            parentPage.VerticalLimit,
            parentPage.ClientRectangle.Width,
            parentPage.ClientRectangle.Height - parentPage.VerticalLimit
        )

        If newBounds.Width > pageBounds.Width Then result = result Or BoundsChecks.Width
        If newBounds.Height > pageBounds.Height Then result = result Or BoundsChecks.Height
        If newBounds.Left < pageBounds.Left Then result = result Or BoundsChecks.Left
        If newBounds.Right > pageBounds.Right Then result = result Or BoundsChecks.Right
        If newBounds.Top < pageBounds.Top Then result = result Or BoundsChecks.Top
        If newBounds.Bottom > pageBounds.Bottom Then result = result Or BoundsChecks.Bottom

        Return result
    End Function

    Private Sub ChildControlSet(ctrl As Control)
        ctrl.TabStop = False ' Prevent child controls from stealing focus.
        For Each child As Control In ctrl.Controls
            ChildControlSet(child)
        Next
    End Sub

    Private Function DragTypeGet() As DragTypes
        ' Returns the type of drag operation that will occur based on the
        ' DisplayControl's current Cursor.
        If Me.Selected Then
            Select Case Me.Cursor
                Case Cursors.Cross
                    Return DragTypes.Move
                Case Cursors.SizeNWSE, Cursors.SizeNESW, Cursors.SizeWE, Cursors.SizeNS
                    Return DragTypes.Resize
                Case Else
                    Return DragTypes.None
            End Select
        End If
        Return DragTypes.None
    End Function

    Private Sub DrawBorder(g As Graphics, ByVal rect As Rectangle)
        ' Draws the DisplayControl's selection border.
        If Me.Selected Then
            ControlPaint.DrawBorder(g, rect,
                Me.SelectionBorderColor, Me.SelectionBorderSize, Me.SelectionBorderStyle,
                Me.SelectionBorderColor, Me.SelectionBorderSize, Me.SelectionBorderStyle,
                Me.SelectionBorderColor, Me.SelectionBorderSize, Me.SelectionBorderStyle,
                Me.SelectionBorderColor, Me.SelectionBorderSize, Me.SelectionBorderStyle
            )
        End If
    End Sub

    Private Sub DrawCursor(e As Point)
        ' Draws the DisplayControl's drag cursor depending on the Selected state
        ' and mouse position.
        If Me.Selected Then
            If Me.IsSizeable Then
                Dim rect As Rectangle = Me.ClientRectangle
                If e.X >= rect.Right - Me.DragEdgeSize AndAlso e.Y >= rect.Bottom - Me.DragEdgeSize Then
                    Me.Cursor = Cursors.SizeNWSE ' Bottom-right corner
                    mResizePoint = ResizePoints.BottomRightCorner
                ElseIf e.X <= rect.Left + Me.DragEdgeSize AndAlso e.Y >= rect.Bottom - Me.DragEdgeSize Then
                    Me.Cursor = Cursors.SizeNESW ' Bottom-left corner
                    mResizePoint = ResizePoints.BottomLeftCorner
                ElseIf e.X >= rect.Right - Me.DragEdgeSize AndAlso e.Y <= rect.Top + Me.DragEdgeSize Then
                    Me.Cursor = Cursors.SizeNESW ' Top-right corner
                    mResizePoint = ResizePoints.TopRightCorner
                ElseIf e.X <= rect.Left + Me.DragEdgeSize AndAlso e.Y <= rect.Top + Me.DragEdgeSize Then
                    Me.Cursor = Cursors.SizeNWSE ' Top-left corner
                    mResizePoint = ResizePoints.TopLeftCorner
                ElseIf e.X >= rect.Right - Me.DragEdgeSize Then
                    Me.Cursor = Cursors.SizeWE ' Right edge
                    mResizePoint = ResizePoints.RightEdge
                ElseIf e.X <= rect.Left + Me.DragEdgeSize Then
                    Me.Cursor = Cursors.SizeWE ' Left edge
                    mResizePoint = ResizePoints.LeftEdge
                ElseIf e.Y >= rect.Bottom - Me.DragEdgeSize Then
                    Me.Cursor = Cursors.SizeNS ' Bottom edge
                    mResizePoint = ResizePoints.BottomEdge
                ElseIf e.Y <= rect.Top + Me.DragEdgeSize Then
                    Me.Cursor = Cursors.SizeNS ' Top edge
                    mResizePoint = ResizePoints.TopEdge
                ElseIf Me.IsMovable AndAlso mMouseDown Then
                    Me.Cursor = Cursors.Cross
                Else
                    Me.Cursor = Cursors.Default
                    mResizePoint = ResizePoints.None
                End If
            ElseIf Me.IsMovable AndAlso mMouseDown Then
                Me.Cursor = Cursors.Cross
                mResizePoint = ResizePoints.None
            Else
                Me.Cursor = Cursors.Default
                mResizePoint = ResizePoints.None
            End If
        Else
            Me.Cursor = Cursors.Default
            mResizePoint = ResizePoints.None
        End If
    End Sub

    Private Function IsChildOf(parentHandle As IntPtr, childHandle As IntPtr) As Boolean
        ' Returns TRUE if childHandle is a child of parentHandle, 
        ' else returns FALSE.
        If childHandle = parentHandle Then Return True
        Dim current = childHandle
        While current <> IntPtr.Zero
            If current = parentHandle Then Return True
            current = GetParent(current)
        End While
        Return False
    End Function
#End Region
#Region "Derived Control Overridable Methods"
    Protected Overridable Sub ContextMenuStripSet()

    End Sub

    Protected Overridable Sub DisplayInitialize()
        mOriginalFontSizes.Clear()
        ControlFontsGet(Me)
        mDisplayInitialized = True
    End Sub

    Protected Overridable Sub DataGet()

    End Sub

    Protected Overridable Sub DataShow()

    End Sub

    Private Sub SelectedSet(ByVal value As Boolean)
        Me.Invalidate(True) ' Trigger a repaint to show selection state.
    End Sub

#Region "New Font Scaling"
    Protected Overridable Sub ChartAxisScale(ByVal axis As Axis, ByVal scaleFactor As Single)
        If mOriginalFontSizes.ContainsKey(axis.TitleFont) Then
            axis.TitleFont = New Font(axis.TitleFont.FontFamily, mOriginalFontSizes(axis.TitleFont) * scaleFactor, axis.TitleFont.Style)
        End If

        If mOriginalFontSizes.ContainsKey(axis.LabelStyle.Font) Then
            axis.LabelStyle.Font = New Font(axis.LabelStyle.Font.FontFamily, mOriginalFontSizes(axis.LabelStyle.Font) * scaleFactor, axis.LabelStyle.Font.Style)
        End If
    End Sub

    Protected Overridable Sub ChartFontsGet(ByVal chart As Chart)
        ' Get Chart.Titles, .Legends, .ChartAreas and .Annotations and .StripLines original Font sizes.
        For Each t As Title In chart.Titles
            mOriginalFontSizes(t) = t.Font.Size
        Next

        For Each l As Legend In chart.Legends
            mOriginalFontSizes(l) = l.Font.Size
        Next

        For Each ca As ChartArea In chart.ChartAreas
            ' Axis Titles.
            mOriginalFontSizes(ca.AxisX.TitleFont) = ca.AxisX.TitleFont.Size
            mOriginalFontSizes(ca.AxisX.LabelStyle.Font) = ca.AxisX.LabelStyle.Font.Size
            mOriginalFontSizes(ca.AxisY.TitleFont) = ca.AxisY.TitleFont.Size
            mOriginalFontSizes(ca.AxisY.LabelStyle.Font) = ca.AxisY.LabelStyle.Font.Size

            ' StripLine Text.
            ChartStripLinesGet(ca.AxisX)
            ChartStripLinesGet(ca.AxisY)
            ChartStripLinesGet(ca.AxisX2)
            ChartStripLinesGet(ca.AxisY2)
        Next

        For Each anno In chart.Annotations
            If TypeOf anno Is TextAnnotation OrElse TypeOf anno Is CalloutAnnotation Then
                mOriginalFontSizes(anno) = anno.Font.Size
            End If
        Next
    End Sub

    Protected Overridable Sub ChartFontsScale(ByVal chart As Chart, ByVal scaleFactor As Single)
        ' Scale the Chart's fonts according to the scaleFactor.
        For Each t As Title In chart.Titles
            If mOriginalFontSizes.ContainsKey(t) Then t.Font = New Font(t.Font.FontFamily, mOriginalFontSizes(t) * scaleFactor, t.Font.Style)
        Next

        For Each l As Legend In chart.Legends
            If mOriginalFontSizes.ContainsKey(l) Then l.Font = New Font(l.Font.FontFamily, mOriginalFontSizes(l) * scaleFactor, l.Font.Style)
        Next

        For Each ca As ChartArea In chart.ChartAreas
            ChartAxisScale(ca.AxisX, scaleFactor)
            ChartAxisScale(ca.AxisY, scaleFactor)

            ChartStripLinesScale(ca.AxisX, scaleFactor)
            ChartStripLinesScale(ca.AxisY, scaleFactor)
            ChartStripLinesScale(ca.AxisX2, scaleFactor)
            ChartStripLinesScale(ca.AxisY2, scaleFactor)
        Next

        For Each anno In chart.Annotations
            If mOriginalFontSizes.ContainsKey(anno) Then
                anno.Font = New Font(anno.Font.FontFamily, mOriginalFontSizes(anno) * scaleFactor, anno.Font.Style)
            End If
        Next
    End Sub

    Protected Overridable Sub ChartStripLinesGet(ByVal axis As Axis)
        If axis IsNot Nothing AndAlso axis.StripLines IsNot Nothing Then
            For Each strip As StripLine In axis.StripLines
                ' Only store if a valid font object exists and text is being displayed.
                ' *** IMPORTANT: For dynamically created StripLines (in DataShow), the
                ' all of DisplayControl's visual elemebts must be re-intialized by 
                ' calling DisplayIntialize(). This isn't recommended as it may be slow.
                If strip.Font IsNot Nothing AndAlso Not String.IsNullOrEmpty(strip.Text) Then
                    mOriginalFontSizes(strip) = strip.Font.Size
                End If
            Next
        End If
    End Sub

    Protected Overridable Sub ChartStripLinesScale(ByVal axis As Axis, ByVal scaleFactor As Single)
        ' If your StripLine text is configured to draw inside the shaded strip area, shrinking the
        ' layout size might narrow the strip width beneath the font's footprint, clipping the text.
        ' If you find text vanishing, verify if your text orientation or alignment properties
        ' (TextAlignment or TextLineAlignment) can be set to pull the text to the edge of the chart
        ' canvas instead of pinning it strictly inside the band.
        If axis IsNot Nothing AndAlso axis.StripLines IsNot Nothing Then
            For Each strip As StripLine In axis.StripLines
                If mOriginalFontSizes.ContainsKey(strip) Then
                    strip.Font = New Font(strip.Font.FontFamily, mOriginalFontSizes(strip) * scaleFactor, strip.Font.Style)
                End If
            Next
        End If
    End Sub

    Protected Overridable Sub ControlFontsGet(ByVal parent As Control)
        ' Get's all nested Control's default font size (design-time size)
        ' so that can be properly scaled. All visual elements must be loaded
        ' and exist before this routine is called. The easiest way to do this
        ' is to create any visual elements, especially Chart elements in the
        ' overridden DisplayIntialize() routine, as it is automatically called
        ' when this base class is loaded.
        If parent IsNot Nothing Then
            ' Handle special container types that have internal non-Control sub-elements.
            ' Add more TypeOf cases and initializers.
            If TypeOf parent Is Chart Then
                ChartFontsGet(DirectCast(parent, Chart))
            Else
                ' Standard control or layout container (TableLayoutPanel, Panel, TextBox, Label, etc.).
                ' Verify your TableLayoutPanel rows are using Percentage or AutoSize layout structures,
                ' otherwise TextBoxes may start looking misaligned or deformed.
                If parent.Font IsNot Nothing Then
                    mOriginalFontSizes(parent) = parent.Font.Size
                End If

                ' Drill down through all nested children (supports infinite nesting depths).
                For Each child As Control In parent.Controls
                    ControlFontsGet(child)
                Next
            End If
        End If
    End Sub

    Protected Overridable Sub ControlFontsScale(ByVal parent As Control, ByVal scaleFactor As Single)
        ' Scale all of the parent's nested Controls according to the scaleFactor.
        If parent IsNot Nothing Then
            ' Handle special container types. Add additional TypeOf cases and handlers.
            If TypeOf parent Is Chart Then
                ChartFontsScale(DirectCast(parent, Chart), scaleFactor)
            Else
                If mOriginalFontSizes.ContainsKey(parent) Then
                    Dim originalSize As Single = mOriginalFontSizes(parent)

                    parent.Font = New Font(parent.Font.FontFamily, originalSize * scaleFactor, parent.Font.Style)
                End If
            End If

            ' Drill down through all nested children.
            For Each child As Control In parent.Controls
                ControlFontsScale(child, scaleFactor)
            Next
        End If
    End Sub
#End Region
#End Region
#Region "Event Handlers"

    Protected Overrides Sub OnHandleDestroyed(e As EventArgs)
        ' Clean up the message filter when the control is disposed.
        Application.RemoveMessageFilter(Me)
        MyBase.OnHandleDestroyed(e)
    End Sub

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)
        Me.Padding = New Padding(Me.SelectionBorderSize + 1)
        Me.Cursor = Cursors.Default
        DisplayInitialize()
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        ' Let the base control handle background and standard logic first.
        MyBase.OnPaint(e)
        ' Draw the selection border last so it sits on the very top layer.
        DrawBorder(e.Graphics, Me.ClientRectangle)
    End Sub

    Protected Overrides Sub OnControlAdded(e As ControlEventArgs)
        ' Initialize nested child controls.
        MyBase.OnControlAdded(e)
        ChildControlSet(e.Control)
    End Sub

    Private Sub OnControlMouseDown(ByVal e As MouseEventArgs)
        If e.Button = MouseButtons.Left Then
            mMouseDown = True
            DrawCursor(e.Location)
            mDragOffset = e.Location
        ElseIf e.Button = MouseButtons.Right Then
            Me.ContextMenuStrip.Show(Me, e.Location)
        End If
        MyBase.OnMouseDown(e)
    End Sub

    Private Sub OnControlMouseEnter(ByVal e As EventArgs)
        MyBase.OnMouseEnter(e)
    End Sub

    Private Sub OnControlMouseHover(ByVal e As EventArgs)
        Dim tipText As String = $"{Me.DisplayName} {Me.Bounds}"
        Dim mousePos As Point = Me.PointToClient(System.Windows.Forms.Cursor.Position)
        mToolTip.Show(tipText, Me, mousePos)
        mTipIsVisible = True
    End Sub

    Private Sub OnControlMouseLeave(ByVal e As EventArgs)
        mMouseDown = False
        Me.Cursor = Cursors.Default
        MyBase.OnMouseLeave(e)
    End Sub

    Private Sub OnControlMouseMove(ByVal e As MouseEventArgs)
        ' Don't change the cursor during a drag operation to prevent flickering,
        ' especially if the drag GridSize is large. The cursor will be reset on mouse up.
        DrawCursor(e.Location)
        MyBase.OnMouseMove(e)
    End Sub

    Private Sub OnControlMouseUp(ByVal e As MouseEventArgs)
        If e.Button = MouseButtons.Left Then
            mMouseDown = False
            DrawCursor(e.Location)
        End If
        MyBase.OnMouseUp(e)
    End Sub
    Protected Overrides Sub OnSizeChanged(e As EventArgs)
        ' Compute the visual scaling factor based on the DefaultSize and current Size
        ' properties, and scale all visual elements accordingly.
        '
        ' TODO: This could become computationally expensive, scaling fonts everytime the control's size
        ' changes by even a pixel. May want to consider doing this in DocumentViewer.ResizeEnd() and Zoom.
        MyBase.OnSizeChanged(e)
        If Not Me.DefaultSize.IsEmpty AndAlso Me.Width > 0 AndAlso Me.Height > 0 Then
            ' Area ratio scaling.
            Dim scaleFactor As Single = Math.Sqrt((Me.Width / Me.DefaultSize.Width) * (Me.Height / Me.DefaultSize.Height))
            ' *** Other formulas ***
            ' Arithmetic mean of scale width and height.
            'Dim scaleFactor As Single = ((Me.Width / Me.DefaultSize.Width) + (Me.Height / Me.DefaultSize.Height)) / 2
            ' Smaller of scale width and height.
            'Dim scaleFactor As Single = Math.Min((Me.Width / Me.DefaultSize.Width), (Me.Height / Me.DefaultSize.Height))
            ' ***
            scaleFactor = Math.Clamp(scaleFactor, kScaleFactorMin, kScaleFactorMax)

            Me.SuspendLayout()
            ControlFontsScale(Me, scaleFactor)
            Me.ResumeLayout()
        End If
    End Sub
#End Region
End Class
