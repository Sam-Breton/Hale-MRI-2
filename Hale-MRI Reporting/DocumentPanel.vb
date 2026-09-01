Imports System.Collections.ObjectModel
Imports System.Collections.Specialized

Public Class DocumentPanel
    Inherits FlowLayoutPanel
    ' TODO: The UndoStack only tracks DisplayControls. Need to incorporate
    ' Letterhead and Header changes as well.
#Region "Types and Constants"
    Private Const kGridSizeMax As Integer = 20              ' Maximum drag/resize gride size in pixels.
    Private Const kGridSizeMin As Integer = 0               ' Minimum drag/resize gride size in pixels.
    Private Const kPageCountMax As Integer = 32             ' Maximum number of DocumentPages we can hold.
    Private Const kPageVerticalSeparation As Integer = 20   ' Vertical separation between DocumentPages in pixels.
    Private Const kPageLeftEdgeMin As Integer = 20          ' Minimum spacing between DocumentPages left edge and the parent form.
    Private Const kUndoStackCountMax As Integer = 32        ' Maximum number of undo operations.
    Private Const kZoomDefault As Single = 1.0F             ' Default zoom factor.
    Private Const kZoomMax As Single = 2.0F                 ' Maximum zoom factor
    Private Const kZoomMin As Single = 0.5F                 ' Minimum zoom factor.
#End Region
#Region "Private Members"
    Private mClipboard As New List(Of DisplayControl)       ' Holds a list of most recently cut DisplayControls.
    Private mCurrentPageIndex As Integer = 0                ' Currently most-visible DocumentPage.
    Private mDCContextMenuStrip As ContextMenuStrip = Nothing ' ContextMenuStrip assigned to DisplayControls.
    Private mDocumentSettings As DocumentSettings = Nothing ' DocumentPage printer bounds. 
    Private mDragStartPos As Point = Point.Empty            ' Mouse location at start of drag/resize operation.
    Private mGridSize As Integer = 0                        ' Drag/resize operation grid size in pixels.
    Private mIsDragging As Boolean = False                  ' Indicates a drag operation in progress.
    Private mIsResizing As Boolean = False                  ' Indicates a resize operation in progress.
    Private mLayoutChanged As Boolean = False               ' Indicates the layout changed during a drag/resize operation.
    Private mNoEnter As Boolean = False                     ' Indicates the DisplayControl.Enter event should not select the control.
    Private mUndoStack As New Stack(Of List(Of UndoState))  ' Stack of snapshots of previous DocumentPanel states.
    Private mVerticalLimit As Integer = 0                   ' The top-most location of any DocumentPage.
    Private mZoom As Single = kZoomDefault                  ' Current zoom factor.
    Private WithEvents mPages As New ValidatingObservableCollection(Of DocumentPage)                ' Current collection of DocumentPages.
    Private WithEvents mSelectedControls As New ObservableCollection(Of DisplayControl)             ' Currently selected DisplayControls.
    Private WithEvents mDisplayControls As New ValidatingObservableCollection(Of DisplayControl)    ' Current collection of all DisplayControls.
#End Region
#Region "Constructors"
    ''' <summary>
    ''' Default constructor.
    ''' </summary>
    Public Sub New()
        MyBase.New()
        InitializeComponent()
    End Sub
#End Region
#Region "Public Interface"
    ''' <summary>
    ''' List of most recently cut DisplayControls.
    ''' </summary>
    ''' <returns>List(Of DisplayControl)</returns>
    Public ReadOnly Property ClipBoard As List(Of DisplayControl)
        Get
            Return mClipboard
        End Get
    End Property

    ''' <summary>
    '''  The DocumentPanel's ContextMenuStrip, if any.
    ''' </summary>
    ''' <returns>ContextMenuStrip</returns>
    Public Overrides Property ContextMenuStrip As ContextMenuStrip
        Get
            Return MyBase.ContextMenuStrip
        End Get
        Set(value As ContextMenuStrip)
            MyBase.ContextMenuStrip = value
            If value IsNot Nothing Then
                BindPageMenuHandlers(value)
            End If
        End Set
    End Property

    ''' <summary>
    ''' Brings all currently selected DisplayControls to the front of the z-order.
    ''' </summary>
    ''' <param name="controls"></param>
    Public Sub ControlsBringToFront(ByVal controls As List(Of DisplayControl))
        UndoSave(Me.Pages.ToList())
        For Each dc As DisplayControl In controls
            dc.BringToFront()
        Next
    End Sub

    ''' <summary>
    ''' Copies all currently selected DisplayControls to the Clipboard and removes them from the DocumentPanel.
    ''' </summary>
    ''' <param name="controls"></param>
    Public Sub ControlsCut(ByVal controls As List(Of DisplayControl))
        UndoSave(Me.Pages.ToList())
        Me.ClipBoard.AddRange(controls) ' Save the cut DisplayControls to the Clipboard.
        Debug.WriteLine($"Clipboard={String.Join("'", Me.ClipBoard.Select(Function(ctrl) ctrl.Name).ToList())}")
        For Each dc As DisplayControl In controls
            Me.DisplayControls.Remove(dc)
        Next ' Remove them from the collection.
    End Sub

    ''' <summary>
    ''' Removes all currently selected DisplayControls from the DocumentPanel.
    ''' </summary>
    ''' <param name="controls"></param>
    Public Sub ControlsDelete(ByVal controls As List(Of DisplayControl))
        UndoSave(Me.Pages.ToList())
        For Each dc As DisplayControl In controls
            Me.DisplayControls.Remove(dc)
        Next
    End Sub


    ''' <summary>
    ''' Adds all DisplayControls on the Clipboard to currently most-visible DocumentPage..
    ''' </summary>
    ''' <param name="controls"></param>
    Public Sub ControlsPaste(ByVal controls As List(Of DisplayControl))
        UndoSave(Me.Pages.ToList())
        For Each dc As DisplayControl In controls
            Dim pg As DocumentPage = Me.Pages(Me.CurrentPageIndex)
            pg.DisplayControls.Add(dc)
        Next
        Me.ClipBoard.Clear()    ' DisplayControls can only be pasted once as duplicates aren't allowed.
        Me.PerformLayout()      ' Refresh our viewport so the pasted pages are redrawn.
    End Sub

    ''' <summary>
    ''' Selects all DisplayControls on the DocumentPanel.
    ''' </summary>
    ''' <param name="controls"></param>
    Public Sub ControlsSelectAll(ByVal controls As List(Of DisplayControl))
        For Each dc As DisplayControl In controls
            If Not dc.Selected Then
                Me.SelectedControls.Add(dc)
            End If
        Next
    End Sub

    ''' <summary>
    ''' Sends all currently selected DisplayControls to the back of the z-order.
    ''' </summary>
    ''' <param name="controls"></param>
    Public Sub ControlsSendToBack(ByVal controls As List(Of DisplayControl))
        UndoSave(Me.Pages.ToList())
        For Each dc As DisplayControl In controls
            dc.SendToBack()
        Next
    End Sub

    ''' <summary>
    ''' Restores the DocumentPanel to its previous state.
    ''' </summary>
    ''' <param name="controls"></param>
    Public Sub ControlsUndo(ByVal items As List(Of UndoState))
        UndoRestore(Me.Pages, items)
    End Sub

    ''' <summary>
    ''' The currently most visible DocumentPage in the viewport.
    ''' </summary>
    ''' <returns>Integer</returns>
    Public Property CurrentPageIndex As Integer
        Get
            Return mCurrentPageIndex
        End Get
        Set(value As Integer)
            ' 1. Validate the range
            If value >= 0 AndAlso value < Me.Controls.Count Then
                mCurrentPageIndex = value

                ' 2. Scroll the specific PrintablePage into view
                Dim targetPage = Me.Controls(mCurrentPageIndex)
                Me.ScrollControlIntoView(targetPage)

                ' 3. Optional: Give it focus so keys like Arrow Down work immediately
                targetPage.Focus()
            End If
        End Set
    End Property

    Public Property DisplayControlContextMenuStrip As ContextMenuStrip
        Get
            Return mDCContextMenuStrip
        End Get
        Set(value As ContextMenuStrip)
            DCContextMenuStripSet(value)
            mDCContextMenuStrip = value
        End Set
    End Property
    ''' <summary>
    ''' The current collection of DisplayControls on the DocumentPanel.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property DisplayControls As ICollection(Of DisplayControl)
        Get
            Return mDisplayControls
        End Get
    End Property

    ''' <summary>
    ''' DocumentPage bounds as set by the current printer settings.
    ''' </summary>
    ''' <returns>DocumentSettings</returns>
    Public Property Document As DocumentSettings
        Get
            Return mDocumentSettings
        End Get
        Set(value As DocumentSettings)
            DocumentSet(value)
            mDocumentSettings = value
        End Set
    End Property

    ''' <summary>
    ''' Drag grid size (resolution) in pixels.
    ''' </summary>
    ''' <returns>Integer</returns>
    Public Property GridSize As Integer
        Get
            Return mGridSize
        End Get
        Set(value As Integer)
            mGridSize = Math.Clamp(value, kGridSizeMin, kGridSizeMax)
        End Set
    End Property

    ''' <summary>
    ''' The current collection of DocumentPages on the DocumentPanel.
    ''' </summary>
    ''' <returns>ObservableCollection(Of DocumentPage)</returns>
    Public ReadOnly Property Pages As ValidatingObservableCollection(Of DocumentPage)
        Get
            Return mPages
        End Get
    End Property

    ''' <summary>
    ''' Indicates whether multiple DisplayControls can be selected.
    ''' </summary>
    ''' <returns>Boolean</returns>
    Public Property MultiSelect As Boolean = False

    ''' <summary>
    ''' Bump the CurrentPageIndex up 1.
    ''' </summary>
    Public Sub PageNext()
        If CurrentPageIndex < Me.Controls.Count - 1 Then
            CurrentPageIndex += 1
            ScrollToPage(CurrentPageIndex)
        End If
    End Sub

    ''' <summary>
    ''' Bump the CurrentPageIndex down 1.
    ''' </summary>
    Public Sub PagePrevious()
        If CurrentPageIndex > 0 Then
            CurrentPageIndex -= 1
            ScrollToPage(CurrentPageIndex)
        End If
    End Sub

    ''' <summary>
    ''' Removes all DocumentPanel content and clears the UndoStack.
    ''' </summary>
    Public Sub Reset()
        ' Clear the UndoStack and Pages collection, 
        ' disposing of each DocumentPage and its 
        ' collection of DisplayControls to prevent
        ' memory leaks.
        Me.UndoStack.Clear()
        While Me.Pages.Count > 0
            While Me.Pages(0).DisplayControls.Count > 0
                Dim dc As DisplayControl = Me.Pages(0).DisplayControls(0)
                Me.Pages(0).DisplayControls.Remove(dc)
                dc.Dispose()
            End While
            Dim pg As DocumentPage = Me.Pages(0)
            Me.Pages.Remove(pg)
            pg.Dispose()
        End While
    End Sub

    ''' <summary>
    ''' Collection of currently selected DisplayControls.
    ''' </summary>
    ''' <returns>ObservableCollection(Of DisplayControl)</returns>
    Public ReadOnly Property SelectedControls As ICollection(Of DisplayControl)
        Get
            Return mSelectedControls
        End Get
    End Property

    ''' <summary>
    ''' Holds LIFO snapshots of previous DocumentPanel states.
    ''' </summary>
    ''' <returns>Stack(Of List(Of UndoState))</returns>
    Public ReadOnly Property UndoStack As Stack(Of List(Of UndoState))
        Get
            Return mUndoStack
        End Get
    End Property

    ''' <summary>
    ''' The top-most location of any DocumentPage.
    ''' </summary>
    ''' <returns>Integer</returns>
    Public Property VerticalLimit As Integer
        Get
            Return mVerticalLimit
        End Get
        Set(value As Integer)
            VerticalLimitSet(value)
            mVerticalLimit = value
        End Set
    End Property

    ''' <summary>
    ''' The current zoom factor.
    ''' </summary>
    ''' <returns>Single</returns>
    Public Property Zoom As Single
        Get
            Return mZoom
        End Get
        Set(value As Single)
            ZoomSet(value)
            mZoom = value
        End Set
    End Property
#End Region
#Region "Private Interface"
    Protected Overridable Sub BindControlMenuHandlers(menu As ContextMenuStrip)
        If menu IsNot Nothing Then
            ' Look for menu items by name.
            Dim bringToFrontItem = menu.Items("BringToFrontToolStripMenuItem")
            Dim sendToBackItem = menu.Items("SendToToolStripMenuItem")
            Dim undoItem = menu.Items("UndoToolStripMenuItem")
            Dim cutItem = menu.Items("CutToolStripMenuItem")
            Dim pasteItem = menu.Items("PasteToolStripMenuItem")
            Dim deleteControlItem = menu.Items("DeleteToolStripMenuItem")
            Dim selectAllItem = menu.Items("SelectAllToolStripMenuItem")
            AddHandler menu.Opening, AddressOf Me.ControlMenu_Opening

            ' Assign event handlers if they exists.
            If bringToFrontItem IsNot Nothing Then
                AddHandler bringToFrontItem.Click, AddressOf Me.MenuControlBringToFront_Click
            End If
            If sendToBackItem IsNot Nothing Then
                AddHandler sendToBackItem.Click, AddressOf Me.MenuControlSendToBack_Click
            End If
            If undoItem IsNot Nothing Then
                AddHandler undoItem.Click, AddressOf Me.MenuControlUndo_Click
            End If
            If cutItem IsNot Nothing Then
                AddHandler cutItem.Click, AddressOf Me.MenuControlCut_Click
            End If
            If pasteItem IsNot Nothing Then
                AddHandler pasteItem.Click, AddressOf Me.MenuControlPaste_Click
            End If
            If deleteControlItem IsNot Nothing Then
                AddHandler deleteControlItem.Click, AddressOf Me.MenuControlDelete_Click
            End If
            If selectAllItem IsNot Nothing Then
                AddHandler selectAllItem.Click, AddressOf Me.MenuControlSelectAll_Click
            End If
        End If
    End Sub

    Protected Overridable Sub BindPageMenuHandlers(menu As ContextMenuStrip)
        If menu IsNot Nothing Then
            ' Look for menu items by name.
            Dim addNewItem = menu.Items("AddNewPageToolStripMenuItem")
            Dim deletePageItem = menu.Items("DeletePageToolStripMenuItem")
            Dim scrollFirstItem = menu.Items("ScrollFirstToolStripMenuItem")
            Dim scrollLastItem = menu.Items("ScrollLastToolStripMenuItem")
            Dim scrollNextItem = menu.Items("ScrollNextToolStripMenuItem")
            Dim scrollPreviousItem = menu.Items("ScrollPreviousToolStripMenuItem")
            Dim scrollToItem = menu.Items("ScrollToToolStripMenuItem")
            Dim moveFirstItem = menu.Items("MoveFirstToolStripMenuItem")
            Dim moveLastItem = menu.Items("MoveLastToolStripMenuItem")
            Dim moveUpItem = menu.Items("MoveUpToolStripMenuItem")
            Dim moveDownItem = menu.Items("MoveDownToolStripMenuItem")

            ' Assign event handlers if they exists.
            AddHandler menu.Opening, AddressOf Me.PageMenu_Opening

            If addNewItem IsNot Nothing Then
                AddHandler addNewItem.Click, AddressOf MenuAddNew_Click
            End If

            If deletePageItem IsNot Nothing Then
                AddHandler deletePageItem.Click, AddressOf MenuDelete_Click
            End If

            If moveFirstItem IsNot Nothing Then
                AddHandler moveFirstItem.Click, AddressOf MenuMoveFirst_Click
            End If
            If moveLastItem IsNot Nothing Then
                AddHandler moveLastItem.Click, AddressOf MenuMoveLast_Click
            End If
            If moveUpItem IsNot Nothing Then
                AddHandler moveUpItem.Click, AddressOf MenuMoveUp_Click
            End If
            If moveDownItem IsNot Nothing Then
                AddHandler moveDownItem.Click, AddressOf MenuMoveDown_Click
            End If
            If scrollFirstItem IsNot Nothing Then
                AddHandler scrollFirstItem.Click, AddressOf MenuScrollFirst_Click
            End If
            If scrollLastItem IsNot Nothing Then
                AddHandler scrollLastItem.Click, AddressOf MenuScrollLast_Click
            End If
            If scrollNextItem IsNot Nothing Then
                AddHandler scrollNextItem.Click, AddressOf MenuScrollNext_Click
            End If
            If scrollPreviousItem IsNot Nothing Then
                AddHandler scrollPreviousItem.Click, AddressOf MenuScrollPrevious_Click
            End If
            If scrollToItem IsNot Nothing Then
                scrollToItem.Enabled = Me.Controls.Count > 1
            End If
        End If
    End Sub

    Private Sub CenterContent()
        ' Center DocumentPages in the DocumentPanel.
        If Me.Pages.Count > 0 Then
            Me.SuspendLayout()

            Dim pageWidth As Integer = Me.Pages(0).Width
            Dim availableSpace As Integer = Me.ClientSize.Width
            Dim hPad As Integer = Math.Max(0, (availableSpace - pageWidth) \ 2)

            Dim newPadding As New Padding(hPad, Me.Padding.Top, hPad, Me.Padding.Bottom)
            If Not Me.Padding.Equals(newPadding) Then
                Me.Padding = newPadding
            End If

            Me.ResumeLayout()
        End If
    End Sub

    Private Function ClickedPage(sender As Object) As DocumentPage
        ' Returns sender's owning DocumentPage.
        Dim cms As ContextMenuStrip = TryCast(DirectCast(sender, ToolStripMenuItem).Owner, ContextMenuStrip)
        Return cms?.SourceControl
    End Function

    Private Sub DCContextMenuStripSet(cms As ContextMenuStrip)
        ' Assigns a ContextMenuStrip to each DisplayControl.
        BindControlMenuHandlers(cms)
        For Each dc As DisplayControl In Me.DisplayControls
            dc.ContextMenuStrip = cms
        Next
    End Sub

    Private Sub DisplayControlAdded(dc As DisplayControl)
        ' Add handlers for drag/resize functionality.
        RemoveHandler dc.Enter, AddressOf Me.DisplayControl_Enter
        RemoveHandler dc.MouseDown, AddressOf Me.DisplayControl_MouseDown
        RemoveHandler dc.MouseMove, AddressOf Me.DisplayControl_MouseMove
        RemoveHandler dc.MouseUp, AddressOf Me.DisplayControl_MouseUp
        AddHandler dc.Enter, AddressOf Me.DisplayControl_Enter
        AddHandler dc.MouseDown, AddressOf Me.DisplayControl_MouseDown
        AddHandler dc.MouseMove, AddressOf Me.DisplayControl_MouseMove
        AddHandler dc.MouseUp, AddressOf Me.DisplayControl_MouseUp
        dc.ContextMenuStrip = Me.DisplayControlContextMenuStrip
    End Sub

    Private Sub DisplayControlDrag(ByRef dc As DisplayControl, ByVal sender As DisplayControl, ByVal location As Point, ByVal pg As ReportPage, ByVal e As MouseEventArgs, ByVal delta As Point)
        ' Move the control to location and, if set, to the given page.
        dc.Location = location
        If pg IsNot Nothing Then
            dc.Parent = pg
            If dc Is sender Then
                Me.CurrentPageIndex = Me.Pages.IndexOf(pg) + 1
                Cursor.Position = dc.PointToScreen(dc.DragOffset)
                dc.BaseLocation = dc.Location
                Debug.WriteLine($"Page Hop---> {sender.Parent.Name} {sender.Location} {sender.DragOffset} {e.Location} {mDragStartPos} {delta})")
            End If
            'Debug.WriteLine($"")
            'dc.PageIndex = mPages.IndexOf(pg)
            '' If the control changed pages and is the one being dragged, notify the parent form.
            'If dc Is sender Then
            '    RaiseEvent DCPageEvent(sender, e)
            'End If
        End If
    End Sub

    Private Sub DisplayControlEnter(dc As DisplayControl, e As EventArgs)
        If Not (mNoEnter OrElse dc.Selected) Then DisplayControlSelect(dc)
    End Sub

    Private Sub DisplayControlMouseDown(dc As DisplayControl, e As MouseEventArgs)
        If Not dc.Selected Then DisplayControlSelect(dc)
        If dc.Selected Then DragStart(dc, e)
    End Sub

    Private Sub DisplayControlRemoved(dc As DisplayControl)
        Dim pg As DocumentPage = dc?.Parent
        If pg IsNot Nothing Then
            mNoEnter = True
            pg.DisplayControls.Remove(dc)
            If Not dc.IsDisposed Then Me.SelectedControls.Remove(dc)
            mNoEnter = False
        End If
    End Sub

    Private Sub DisplayControlSelect(dc As DisplayControl)
        If Not Me.MultiSelect Then Me.SelectedControls.Clear()  ' If MultiSelect isn't enabled then unselect all DisplayControls.
        mSelectedControls.Add(dc)                               ' Select the given DisplayControl. 
        Debug.WriteLine($"Selected={String.Join("'", Me.SelectedControls.Select(Function(ctrl) ctrl.Name).ToList())}")
    End Sub

    Private Sub DocumentPageMouseDown(pg As DocumentPage)
        If Not Me.MultiSelect Then Me.SelectedControls.Clear()
    End Sub

    Private Sub DocumentPanelKeyDown(sender As Object, e As KeyEventArgs)
        Debug.WriteLine($"DocumentPanelKeyDown={e.KeyCode}")
    End Sub

    Protected Overridable Sub DocumentPageAdded(pg As DocumentPage)
        pg.SuspendLayout()
        pg.Margin = New Padding(0, 0, 0, kPageVerticalSeparation)
        pg.ContextMenuStrip = Me.ContextMenuStrip
        pg.Document = Me.Document
        pg.RightToLeft = RightToLeft.No
        PageSizeSet(pg, mZoom)
        PageAddToMenuStrip(pg)
        RemoveHandler pg.ControlAdded, AddressOf Me.DisplayControl_ControlAdded
        RemoveHandler pg.ControlRemoved, AddressOf Me.DisplayControl_ControlRemoved
        RemoveHandler pg.KeyDown, AddressOf Me.DocumentPanel_DocumentPageKeyDown
        RemoveHandler pg.MouseDown, AddressOf Me.DocumentPanel_DocumentPageMouseDown
        RemoveHandler pg.MouseUp, AddressOf Me.DocumentPanel_DocumentPageMouseUp
        RemoveHandler pg.SizeChanged, AddressOf Me.DocumentPanel_DocumentPageSizeChanged
        AddHandler pg.ControlAdded, AddressOf Me.DisplayControl_ControlAdded
        AddHandler pg.ControlRemoved, AddressOf Me.DisplayControl_ControlRemoved
        AddHandler pg.KeyDown, AddressOf Me.DocumentPanel_DocumentPageKeyDown
        AddHandler pg.MouseDown, AddressOf Me.DocumentPanel_DocumentPageMouseDown
        AddHandler pg.MouseUp, AddressOf Me.DocumentPanel_DocumentPageMouseUp
        AddHandler pg.SizeChanged, AddressOf Me.DocumentPanel_DocumentPageSizeChanged
        pg.Visible = True
        pg.ResumeLayout(True)
        Me.Controls.Add(pg)
        CenterContent()
    End Sub

    Protected Overridable Sub DocumentPageRemoved(pg As DocumentPage)
        PageRemoveFromMenuStrip(pg)
        Me.Controls.Remove(pg)
    End Sub

    Private Sub DocumentSet(ByVal ds As DocumentSettings)
        Me.SuspendLayout()
        Try
            For Each pg As DocumentPage In mPages
                pg.Document = ds
                PageSizeSet(pg, mZoom)
            Next
            CenterContent()
        Finally
            Me.ResumeLayout(True)
        End Try
    End Sub

    Private Sub DragEnd()
        mIsDragging = False
        mIsResizing = False
        LayoutCheck(Me.DisplayControls)
        LayoutSet(Me.SelectedControls)
        Debug.WriteLine("DragEnd")
    End Sub

    Private Sub DragMove(sender As DisplayControl, e As MouseEventArgs)
        ' Drag selected controls to a new location. Sender is the grabbed control.
        '
        ' Get the mouse position offset from the drag start location.
        Dim deltaX As Integer = e.Location.X - mDragStartPos.X
        Dim deltaY As Integer = e.Location.Y - mDragStartPos.Y
        Debug.WriteLine($"{sender.Parent.Name} {sender.Location} {sender.DragOffset} {e.Location} {mDragStartPos} {New Point(deltaX, deltaY)}")
        ' Apply grid snapping if GridSize is set
        If Me.GridSize > 0 Then
            Dim szGrid As Integer = Math.Max(Me.GridSize * Me.Zoom, 1)
            deltaX = Math.Round(deltaX / szGrid) * szGrid
            deltaY = Math.Round(deltaY / szGrid) * szGrid
        End If
        If deltaX = 0 AndAlso deltaY = 0 Then Return

        ' Check all moveable controls. If any control can't
        ' be moved, then none will be moved (once any control
        ' gets "stuck" dragging stops for all controls).
        Dim movements As New List(Of ValueTuple(Of DisplayControl, Point, oldReportPage))
        For Each dc As DisplayControl In Me.SelectedControls
            If dc.IsMovable Then
                Dim newBounds As New Rectangle(New Point(dc.Left + deltaX, dc.Top + deltaY), dc.Size)

                Select Case dc.BoundsCheck(newBounds)
                    Case DisplayControl.BoundsChecks.None          ' Relocate control to new position according to the mouse offset.
                        movements.Add((dc, New Point(dc.Left + deltaX, dc.Top + deltaY), Nothing))
                    Case DisplayControl.BoundsChecks.Left    ' Controls cannot be dragged off page horizontally, so just return.
                        If dc.Left > dc.Parent.ClientRectangle.Left Then
                            movements.Add((dc, New Point(dc.Parent.ClientRectangle.Left, dc.Top + deltaY), Nothing)) ' Allow drag to parent left limit if GridSize would prevent it.
                        Else
                            Return
                        End If
                    Case DisplayControl.BoundsChecks.Right
                        If dc.Right < dc.Parent.ClientRectangle.Right Then
                            movements.Add((dc, New Point(dc.Parent.ClientRectangle.Right - dc.Width, dc.Top + deltaY), Nothing)) ' Allow drag to parent right limit if GridSize would prevent it.
                        Else
                            Return
                        End If
                    Case DisplayControl.BoundsChecks.Top      ' If there's a page above, move the control there.
                        Dim parentPage As Integer = Me.Pages.IndexOf(sender.Parent)
                        If parentPage > 0 Then
                            Dim previousPage As DocumentPage = Me.Pages(parentPage - 1)
                            movements.Add((dc, New Point(dc.Left, previousPage.ClientRectangle.Bottom - dc.Height), previousPage))
                        ElseIf dc.Top > Me.Pages(parentPage).VerticalLimit * Me.Pages(parentPage).Zoom.Height Then
                            movements.Add((dc, New Point(dc.Left + deltaX, Me.Pages(parentPage).ClientRectangle.Bottom - dc.Height), Nothing)) ' Allow drag to parent right limit if GridSize would prevent it.
                        Else
                            Return
                        End If
                        Return ' TODO: not implemented
                    Case DisplayControl.BoundsChecks.Bottom   ' If there's a page below, move the control there.
                        Dim parentPage As Integer = Me.Pages.IndexOf(sender.Parent)
                        If parentPage < Me.Pages.Count - 1 Then
                            Dim nextPage As DocumentPage = Me.Pages(parentPage + 1)
                            movements.Add((dc, New Point(dc.Left, nextPage.ClientRectangle.Top), nextPage))
                        ElseIf dc.Bottom < Me.Pages(parentPage).ClientRectangle.Bottom Then
                            movements.Add((dc, New Point(dc.Left + deltaX, Me.Pages(parentPage).ClientRectangle.Bottom - dc.Height), Nothing)) ' Allow drag to parent right limit if GridSize would prevent it.
                        Else
                            Return
                        End If
                        'Return ' TODO: not implemented
                End Select
            End If
        Next

        ' Now move the controls all at once.
        RemoveHandler sender.MouseMove, AddressOf Me.DisplayControl_MouseMove
        For Each movement As ValueTuple(Of DisplayControl, Point, oldReportPage) In movements
            Try
                DisplayControlDrag(movement.Item1, sender, movement.Item2, movement.Item3, e, New Point(deltaX, deltaY))
            Catch ex As Exception
                ' Swallow any errors and keep going.
            End Try
        Next
        AddHandler sender.MouseMove, AddressOf Me.DisplayControl_MouseMove
        mLayoutChanged = True
    End Sub

    Private Sub DragStart(ByVal dc As DisplayControl, e As MouseEventArgs)
        Select Case dc.DragType
            Case DisplayControl.DragTypes.Move
                mIsDragging = True
                mDragStartPos = e.Location
                Debug.WriteLine($"DragStart: {dc.Name} mIsDragging")
            Case DisplayControl.DragTypes.Resize
                mIsResizing = True
                mDragStartPos = Cursor.Position
                Debug.WriteLine($"DragStart: {dc.Name} mIsResizing")
            Case Else
                mIsDragging = False
                mIsResizing = False
                Debug.WriteLine($"DragStart: {dc.Name} False")
                Return
        End Select
        UndoSave(Me.Pages.ToList())
    End Sub

    Protected Overridable Sub LayoutCheck(controls As ICollection(Of DisplayControl))
        If Not mLayoutChanged AndAlso Me.UndoStack.Count > 0 Then
            Dim unused = Me.UndoStack.Pop()
        End If
        mLayoutChanged = False
    End Sub

    Protected Overridable Sub LayoutSet(controls As ICollection(Of DisplayControl))
        For Each dc As DisplayControl In controls
            'dc.ApplyResizeMove()
            Debug.WriteLine($"LayoutSet: {dc.Parent?.Name} {dc.Name} {dc.Bounds} {New Rectangle(dc.BaseLocation, dc.BaseSize)}")
        Next
    End Sub

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        CenterContent()
    End Sub

    Protected Overrides Sub OnScroll(e As ScrollEventArgs)
        MyBase.OnScroll(e)
        ' 1. Define the center of the visible viewport
        ' ClientSize.Height gives the visible area, and we find its midpoint.
        Dim viewportCenter As Integer = Me.ClientSize.Height / 2

        Dim closestIndex As Integer = -1
        Dim minDistance As Integer = Integer.MaxValue

        ' 2. Iterate through pages to find the one closest to the viewport center.
        For i As Integer = 0 To Me.Controls.Count - 1
            Dim pg = Me.Controls(i)

            ' The control's Top/Bottom are relative to the panel's total scrollable area.
            ' We find the control's center point relative to the current visible top.
            Dim controlCenter As Integer = pg.Top + (pg.Height / 2)

            ' Calculate how far this page's center is from the viewport center.
            Dim distance As Integer = Math.Abs(viewportCenter - controlCenter)

            If distance < minDistance Then
                minDistance = distance
                closestIndex = i
            End If
        Next

        ' 3. Update the property if the prominent page has changed.
        If closestIndex <> -1 AndAlso closestIndex <> mCurrentPageIndex Then
            mCurrentPageIndex = closestIndex
            Debug.WriteLine($"mCurrentPageIndex={mCurrentPageIndex}")
            ' Raise an event here if the ParentForm needs to update its Menu Dropdown.
        End If
    End Sub

    Private Sub PageAddToMenuStrip(ByVal pg As DocumentPage)
        Dim menu As ContextMenuStrip = Me.ContextMenuStrip
        Dim menuItem As ToolStripMenuItem = menu.Items("ScrollToToolStripMenuItem")
        If menuItem IsNot Nothing Then
            Dim item As New ToolStripMenuItem($"{Me.Pages.Count}")
            AddHandler item.Click, AddressOf Me.MenuScrollTo_Click
            menuItem.DropDownItems.Add(item)
        End If
    End Sub

    Private Sub PageRemoveFromMenuStrip(ByVal pg As DocumentPage)
        Dim menu As ContextMenuStrip = Me.ContextMenuStrip
        Dim menuItem As ToolStripMenuItem = menu.Items("ScrollToToolStripMenuItem")
        If menuItem IsNot Nothing Then
            Dim i As Integer = Me.Controls.GetChildIndex(pg)
            Dim item As ToolStripMenuItem = menuItem.DropDownItems(i)
            RemoveHandler item.Click, AddressOf Me.MenuScrollTo_Click
            menuItem.DropDownItems.Remove(item)
            For j As Integer = i To menuItem.DropDownItems.Count - 1
                menuItem.DropDownItems(j).Text = $"{j + 1}"
            Next
        End If
    End Sub

    Private Sub PageSizeSet(pg As DocumentPage, ByVal zoomFactor As Single)
        ' Calculate absolute size from the original 100% dimensions
        Dim newW As Integer = CInt(Math.Round(pg.OriginalSize.Width * zoomFactor, 0))
        Dim newH As Integer = CInt(Math.Round(pg.OriginalSize.Height * zoomFactor, 0))

        ' Set the size directly (Zero drift)
        pg.Size = New Size(newW, newH)

        ' Update margin
        Dim absoluteBottomMargin As Integer = CInt(Math.Round(kPageVerticalSeparation * zoomFactor, 0))
        pg.Margin = New Padding(0, 0, 0, absoluteBottomMargin)
    End Sub

    Private Sub Repaginate(pgs As ObservableCollection(Of DocumentPage))
        ' Rename each DocumentPage according to its index in the collection.
        For i As Integer = 0 To pgs.Count - 1
            pgs(i).Name = $"Page {i + 1}"
        Next
    End Sub

    Private Sub ResizeMove(sender As DisplayControl, e As MouseEventArgs)
        ' Resize the selected controls. Sender is the grabbed control.

        ' Get the mouse position offset from the drag start location.
        Dim deltaX As Integer = Cursor.Position.X - mDragStartPos.X
        Dim deltaY As Integer = Cursor.Position.Y - mDragStartPos.Y
        ' Apply grid snapping if GridSize is set
        If GridSize > 0 Then
            deltaX = Math.Round(deltaX / GridSize) * GridSize
            deltaY = Math.Round(deltaY / GridSize) * GridSize
        End If
        If deltaX = 0 And deltaY = 0 Then Return
        'Debug.WriteLine($"{sender.Name} {sender.Bounds} {New Point(deltaX, deltaY)}")
        'Return
        ' Check all sizeable controls.
        Dim resizes As New List(Of ValueTuple(Of Rectangle, DisplayControl))
        Dim newBounds As Rectangle
        For Each dc In mSelectedControls
            If dc.IsSizeable Then
                ' Stretch the control according to the edge grabbed and the mouse move direction.
                Select Case sender.ResizePoint
                    Case DisplayControl.ResizePoints.RightEdge
                        newBounds = New Rectangle(dc.Location, New Size(dc.LastSize.Width + deltaX, dc.Height))
                    Case DisplayControl.ResizePoints.LeftEdge
                        newBounds = New Rectangle(New Point(dc.LastPosition.X + deltaX, dc.Top), New Size(dc.LastSize.Width - deltaX, dc.Height))
                    Case DisplayControl.ResizePoints.TopEdge
                        newBounds = New Rectangle(New Point(dc.Left, dc.LastPosition.Y + deltaY), New Size(dc.Width, dc.LastSize.Height - deltaY))
                    Case DisplayControl.ResizePoints.BottomEdge
                        newBounds = New Rectangle(dc.Location, New Size(dc.Width, dc.LastSize.Height + deltaY))
                    Case DisplayControl.ResizePoints.TopRightCorner
                        newBounds = New Rectangle(New Point(dc.Left, dc.LastPosition.Y + deltaY), New Size(dc.LastSize.Width + deltaX, dc.LastSize.Height - deltaY))
                    Case DisplayControl.ResizePoints.BottomRightCorner
                        newBounds = New Rectangle(dc.Location, New Size(dc.LastSize.Width + deltaX, dc.LastSize.Height + deltaY))
                    Case DisplayControl.ResizePoints.TopLeftCorner
                        newBounds = New Rectangle(New Point(dc.LastPosition.X + deltaX, dc.LastPosition.Y + deltaY), New Size(dc.LastSize.Width - deltaX, dc.LastSize.Height - deltaY))
                    Case DisplayControl.ResizePoints.BottomLeftCorner
                        newBounds = New Rectangle(New Point(dc.LastPosition.X + deltaX, dc.Top), New Size(dc.LastSize.Width - deltaX, dc.LastSize.Height + deltaY))
                    Case Else
                        Return
                End Select
                ' Enforce page bounds. Once any control can't be resized, just return. 
                If dc.BoundsCheck(newBounds) <> DisplayControl.BoundsChecks.None Then Return
                resizes.Add((newBounds, dc))
            End If
        Next

        ' Now resize the controls all at once.
        For Each resize As ValueTuple(Of Rectangle, DisplayControl) In resizes
            Try
                resize.Item2.Bounds = resize.Item1
                Debug.WriteLine($"ResizeMove: {resize.Item2.Name} {resize.Item2.Bounds}")
            Catch ex As Exception
            End Try
        Next
        mLayoutChanged = True
    End Sub

    Private Sub ScrollToPage(index As Integer)
        If index >= 0 AndAlso index < Me.Controls.Count Then
            Dim targetPage = Me.Controls(index)
            Me.ScrollControlIntoView(targetPage)
        End If
    End Sub
    Protected Overridable Sub UndoRestore(ByRef dest As ObservableCollection(Of DocumentPage), ByVal src As List(Of UndoState))
        Me.SuspendLayout()
        mPages.Clear()
        For Each undo As UndoState In src
            Dim pg As DocumentPage = undo.Control
            pg.DisplayControls.Clear()
            For Each tc As UndoControl In undo.Children
                Dim dc As DisplayControl = DirectCast(tc.Control, DisplayControl)
                dc.Location = dc.LastPosition   ' Restore the last location and size.
                dc.Size = dc.LastSize
                dc.Selected = Me.SelectedControls.Any(Function(ctrl) ctrl.Name = dc.Name)
                Debug.WriteLine($"{vbTab}UndoRestore: {dc.Name} lastPos={dc.LastPosition} loc={dc.Location} selected={dc.Selected}")
                pg.DisplayControls.Add(dc)
                pg.Controls.SetChildIndex(dc, tc.Index)
            Next
            mPages.Add(undo.Control)
        Next
        Me.ResumeLayout()
        Me.Select()
    End Sub

    Protected Overridable Sub UndoSave(pgs As List(Of DocumentPage))
        If Me.UndoStack.Count < kUndoStackCountMax Then
            Dim tsList As New List(Of UndoState)

            For Each pg As DocumentPage In pgs
                Dim ts As New UndoState() With {.Control = pg, .Children = New List(Of UndoControl)()}
                'Debug.WriteLine($"{ts.Control.Name} --------")
                For Each dc As DisplayControl In pg.DisplayControls
                    dc.LastPosition = dc.Location   ' Save the last location and size.
                    dc.LastSize = dc.Size
                    ts.Children.Add(New UndoControl() With {
                        .Control = dc.Clone(),
                        .Index = dc.Parent.Controls.GetChildIndex(dc)
                    })
                    'Debug.WriteLine($"{vbTab}UndoSave: {ts.Children(ts.Children.Count - 1).Control.Name} lastPos={DirectCast(ts.Children(ts.Children.Count - 1).Control, DisplayControl).LastPosition} loc={ts.Children(ts.Children.Count - 1).Control.Location}")
                Next
                tsList.Add(ts)
            Next
            Me.UndoStack.Push(tsList)
        End If
    End Sub

    Private Sub VerticalLimitSet(ByVal vLimit As Integer)
        ' Probably unnecessary
    End Sub

    Private Sub ZoomSet(ByVal zoomFactor As Single)
        zoomFactor = Math.Min(kZoomMax, Math.Max(kZoomMin, zoomFactor))

        Me.SuspendLayout()
        Try
            For Each pg As DocumentPage In mPages
                PageSizeSet(pg, zoomFactor)
            Next
            CenterContent()
        Finally
            Me.ResumeLayout(True)
        End Try
    End Sub
#End Region
#Region "Event Handlers"
#Region "DocumentPanel Events"
    Private Sub Collections_BeginTransaction(sender As Object, e As EventArgs) Handles mPages.BeginTrans, mDisplayControls.BeginTrans
        ' Save a snapshot of the DocumentPanel to the UndoStack.
        Debug.WriteLine($"Collections_BeginTransaction: page count={Me.Pages.Count}")
        For i As Integer = 0 To Me.Pages.Count - 1
            Debug.WriteLine($"{vbTab}{Me.Pages(i).Name} dc count={Me.Pages(i).DisplayControls.Count}")
        Next
        UndoSave(Me.Pages.ToList())
    End Sub

    Private Sub Collections_EndTransaction(sender As Object, e As EventArgs) Handles mPages.EndTrans, mDisplayControls.EndTrans
        Debug.WriteLine($"Pages_EndTransaction: count={Me.Pages.Count}")
        For i As Integer = 0 To Me.Pages.Count - 1
            Debug.WriteLine($"{vbTab}{Me.Pages(i).Name} dc count={Me.Pages(i).DisplayControls.Count}")
        Next
    End Sub

    Private Sub DisplayControls_BeforeItemAdded(sender As Object, e As CancelEventArgs(Of DisplayControl)) Handles mDisplayControls.AddingItem
        ' Duplicate DisplayControls aren't allowed.
        If mDisplayControls.Contains(e.Item) Then
            e.Cancel = True
        End If
    End Sub

    Private Sub DisplayControlsHandleCollectionChanged(e As NotifyCollectionChangedEventArgs)
        ' Adds/removes DisplayControls according to the given NotifyCollectionChangedEventArgs.
        If e.OldItems IsNot Nothing Then
            For Each dc As DisplayControl In e.OldItems
                DisplayControlRemoved(dc)
            Next
        End If
        If e.NewItems IsNot Nothing Then
            For Each dc As DisplayControl In e.NewItems
                DisplayControlAdded(dc)
            Next
        End If
    End Sub

    Private Sub DisplayControls_CollectionChanged(sender As Object, e As NotifyCollectionChangedEventArgs) Handles mDisplayControls.CollectionChanged
        Select Case e.Action
            Case NotifyCollectionChangedAction.Reset
                Me.SuspendLayout()
                Try
                    While Me.DisplayControls.Count > 0
                        DisplayControlRemoved(Me.DisplayControls(0))
                    End While
                    For i As Integer = Me.DisplayControls.Count - 1 To 0 Step -1
                        DisplayControlRemoved(Me.DisplayControls(i))
                    Next
                Finally
                    Me.ResumeLayout(True)
                End Try
            Case Else
                DisplayControlsHandleCollectionChanged(e)
        End Select
    End Sub

    Private Sub DisplayControl_ControlAdded(sender As Object, e As ControlEventArgs)
        Me.DisplayControls.Add(e.Control)
        Debug.WriteLine($"DocumentPanel_ControlAdded: {DirectCast(sender, DocumentPage).Name} {e.Control.Name} {e.Control.Bounds} {New Rectangle(DirectCast(e.Control, DisplayControl).BaseLocation, DirectCast(e.Control, DisplayControl).BaseSize)}")
    End Sub

    Private Sub DisplayControl_ControlRemoved(sender As Object, e As ControlEventArgs)
        mDisplayControls.Remove(e.Control)
        Debug.WriteLine($"DocumentPanel_ControlRemoved: {DirectCast(sender, DocumentPage).Name} {e.Control.Name}")
    End Sub

    Private Sub DisplayControl_CursorChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub DisplayControl_Enter(sender As Object, e As EventArgs)
        DisplayControlEnter(DirectCast(sender, DisplayControl), e)
        'Debug.WriteLine($"DisplayControl_Enter: {DirectCast(sender, DisplayControl).Name}")
    End Sub

    Private Sub DisplayControl_MouseDown(sender As Object, e As MouseEventArgs)
        DisplayControlMouseDown(DirectCast(sender, DisplayControl), e)

        'Debug.WriteLine($"DisplayControl_MouseDown: {DirectCast(sender, DisplayControl).Name}")
    End Sub

    Private Sub DisplayControl_MouseMove(sender As Object, e As MouseEventArgs)
        If mIsDragging Then
            DragMove(DirectCast(sender, DisplayControl), e)
        ElseIf mIsResizing Then
            ResizeMove(DirectCast(sender, DisplayControl), e)
        End If
        'Debug.WriteLine($"DisplayControl_MouseMove: {DirectCast(sender, DisplayControl).Name}")
    End Sub

    Private Sub DisplayControl_MouseUp(sender As Object, e As MouseEventArgs)
        DragEnd()
        'Debug.WriteLine($"DisplayControl_MouseUp: {DirectCast(sender, DisplayControl).Name}")
    End Sub

    Private Sub DocumentPanel_DocumentPageAdded(sender As Object, e As ControlEventArgs) Handles MyBase.ControlAdded
        If TypeOf e.Control Is DocumentPage Then
            Debug.WriteLine($"DocumentPanel_DocumentPageAdded: {e.Control.Name} {e.Control.Bounds}")
        End If
    End Sub

    Private Sub DocumentPanel_DocumentPageRemoved(sender As Object, e As ControlEventArgs) Handles MyBase.ControlRemoved
        If TypeOf e.Control Is DocumentPage Then
            Debug.WriteLine($"DocumentPanel_DocumentPageRemoved: {e.Control.Name}")
        End If
    End Sub

    Private Sub DocumentPanel_DocumentPageKeyDown(sender As Object, e As KeyEventArgs)
        'Debug.WriteLine($"DocumentPanel_DocumentPageKeyDown: {DirectCast(sender, DocumentPage).Name} code={e.KeyCode} data={e.KeyData} value={e.KeyValue}")
    End Sub

    Private Sub DocumentPanel_DocumentPageLocationChanged(sender As Object, e As EventArgs)
        Debug.WriteLine($"DocumentPanel_DocumentPageLocationChanged: {DirectCast(sender, DocumentPage).Name} location={DirectCast(sender, DocumentPage).Location}")
    End Sub

    Private Sub DocumentPanel_DocumentPageMouseDown(sender As Object, e As MouseEventArgs)
        DocumentPageMouseDown(DirectCast(sender, DocumentPage))
        'Debug.WriteLine($"DocumentPanel_DocumentPageMouseDown: {DirectCast(sender, DocumentPage).Name} @ {e.Location}")
    End Sub

    Private Sub DocumentPanel_DocumentPageMouseUp(sender As Object, e As MouseEventArgs)
        'Debug.WriteLine($"DocumentPanel_DocumentPageMouseUp: {DirectCast(sender, DocumentPage).Name} @ {e.Location}")
    End Sub

    Private Sub DocumentPanel_DocumentPageSizeChanged(sender As Object, e As EventArgs)
        'Debug.WriteLine($"DocumentPanel_DocumentPageSizeChanged: {DirectCast(sender, DocumentPage).Name} size={DirectCast(sender, DocumentPage).Size}")
    End Sub

    Private Sub DocumentPanel_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        DocumentPanelKeyDown(sender, e)
    End Sub

    Private Sub DocumentPanel_KeyUp(sender As Object, e As KeyEventArgs) Handles MyBase.KeyUp
        'DocumentPanelKeyUp(sender, e)
    End Sub

    Private Sub PagesHandleCollectionChanged(e As NotifyCollectionChangedEventArgs)
        If e.NewItems IsNot Nothing Then
            For Each pg As DocumentPage In e.NewItems
                DocumentPageAdded(pg)
            Next
        End If
        If e.OldItems IsNot Nothing Then
            For Each pg As DocumentPage In e.OldItems
                DocumentPageRemoved(pg)
            Next
        End If
    End Sub

    Private Sub Pages_CollectionChanged(sender As Object, e As NotifyCollectionChangedEventArgs) Handles mPages.CollectionChanged
        ' Adds/removes pages to/from the DocumentPanel.
        Repaginate(Me.Pages)
        Select Case e.Action
            Case NotifyCollectionChangedAction.Reset
                Me.SuspendLayout()
                Try
                    For i As Integer = Me.Controls.Count - 1 To 0 Step -1
                        If TypeOf Me.Controls(i) Is DocumentPage Then
                            DocumentPageRemoved(DirectCast(Me.Controls(0), DocumentPage))
                        End If
                    Next
                Finally
                    Me.ResumeLayout(True)
                    CenterContent()
                End Try
            Case Else
                PagesHandleCollectionChanged(e)
        End Select
    End Sub
#End Region
#Region "ContextMenuStrip Events"
    Private Sub ControlMenu_Opening(sender As Object, e As EventArgs)
        Dim menu = DirectCast(sender, ContextMenuStrip)
        Dim source = menu.SourceControl
        Dim bringToFrontItem = menu.Items("BringToFrontToolStripMenuItem")
        Dim sendToBackItem = menu.Items("SendToToolStripMenuItem")
        Dim undoItem = menu.Items("UndoToolStripMenuItem")
        Dim cutItem = menu.Items("CutToolStripMenuItem")
        Dim pasteItem = menu.Items("PasteToolStripMenuItem")
        Dim deleteItem = menu.Items("DeleteToolStripMenuItem")
        Dim selectAllItem = menu.Items("SelectAllToolStripMenuItem")


        If deleteItem IsNot Nothing Then
            deleteItem.Enabled = Me.SelectedControls.Count > 0
        End If

        If bringToFrontItem IsNot Nothing Then
            bringToFrontItem.Enabled = deleteItem.Enabled
        End If

        If sendToBackItem IsNot Nothing Then
            sendToBackItem.Enabled = deleteItem.Enabled
        End If

        If undoItem IsNot Nothing Then
            undoItem.Enabled = Me.UndoStack.Count > 0
        End If

        If cutItem IsNot Nothing Then
            cutItem.Enabled = deleteItem.Enabled
        End If

        If pasteItem IsNot Nothing Then
            pasteItem.Enabled = Me.ClipBoard.Count > 0
        End If

        If selectAllItem IsNot Nothing Then
            selectAllItem.Enabled = Me.DisplayControls.Count > 0
        End If
    End Sub

    Private Sub PageMenu_Opening(sender As Object, e As EventArgs)
        Dim menu = DirectCast(sender, ContextMenuStrip)
        Dim source = menu.SourceControl
        If TypeOf source Is DocumentPage Then
            mCurrentPageIndex = Me.Pages.IndexOf(DirectCast(source, DocumentPage))
        End If

        Dim addNewItem = menu.Items("AddNewPageToolStripMenuItem")
        Dim deleteItem = menu.Items("DeletePageToolStripMenuItem")
        Dim scrollFirstItem = menu.Items("ScrollFirstToolStripMenuItem")
        Dim scrollLastItem = menu.Items("ScrollLastToolStripMenuItem")
        Dim scrollNextItem = menu.Items("ScrollNextToolStripMenuItem")
        Dim scrollPreviousItem = menu.Items("ScrollPreviousToolStripMenuItem")
        Dim scrollToItem = menu.Items("ScrollToToolStripMenuItem")
        Dim moveFirstItem = menu.Items("MoveFirstToolStripMenuItem")
        Dim moveLastItem = menu.Items("MoveLastToolStripMenuItem")
        Dim moveUpItem = menu.Items("MoveUpToolStripMenuItem")
        Dim moveDownItem = menu.Items("MoveDownToolStripMenuItem")


        If addNewItem IsNot Nothing Then
            addNewItem.Enabled = Me.Controls.Count < kPageCountMax
        End If

        If deleteItem IsNot Nothing Then
            deleteItem.Enabled = Me.Controls.Count > 0
        End If

        If moveFirstItem IsNot Nothing Then
            moveFirstItem.Enabled = mCurrentPageIndex > 0
        End If
        If moveLastItem IsNot Nothing Then
            moveLastItem.Enabled = mCurrentPageIndex < Me.Controls.Count - 1
        End If
        If moveUpItem IsNot Nothing Then
            moveUpItem.Enabled = scrollPreviousItem.Enabled = mCurrentPageIndex > 0
        End If
        If moveDownItem IsNot Nothing Then
            moveDownItem.Enabled = mCurrentPageIndex < Me.Controls.Count - 1
        End If

        If scrollFirstItem IsNot Nothing Then
            scrollFirstItem.Enabled = mCurrentPageIndex > 0
        End If
        If scrollLastItem IsNot Nothing Then
            scrollLastItem.Enabled = mCurrentPageIndex < Me.Controls.Count - 1
        End If
        If scrollNextItem IsNot Nothing Then
            scrollNextItem.Enabled = mCurrentPageIndex < Me.Controls.Count - 1
        End If
        If scrollPreviousItem IsNot Nothing Then
            scrollPreviousItem.Enabled = mCurrentPageIndex > 0
        End If
        If scrollToItem IsNot Nothing Then
            scrollToItem.Enabled = Me.Controls.Count > 1
        End If
    End Sub

    Private Sub MenuControlBringToFront_Click(sender As Object, e As EventArgs)
        ControlsBringToFront(Me.SelectedControls.ToList())
    End Sub

    Private Sub MenuControlCut_Click(sender As Object, e As EventArgs)
        ControlsCut(Me.SelectedControls.ToList())
    End Sub

    Private Sub MenuControlDelete_Click(sender As Object, e As EventArgs)
        ControlsDelete(Me.SelectedControls.ToList())
    End Sub

    Private Sub MenuControlPaste_Click(sender As Object, e As EventArgs)
        ControlsPaste(Me.ClipBoard)
    End Sub

    Private Sub MenuControlSendToBack_Click(sender As Object, e As EventArgs)
        ControlsSendToBack(Me.SelectedControls.ToList())
    End Sub

    Private Sub MenuControlSelectAll_Click(sender As Object, e As EventArgs)
        ControlsSelectAll(Me.DisplayControls.ToList())
    End Sub

    Private Sub MenuControlUndo_Click(sender As Object, e As EventArgs)
        ControlsUndo(Me.UndoStack.Pop())
    End Sub

    Protected Overridable Sub MenuAddNew_Click(sender As Object, e As EventArgs)
        UndoSave(Me.Pages.ToList())
        Me.Pages.Add(New DocumentPage())
    End Sub

    Private Sub MenuDelete_Click(sender As Object, e As EventArgs)
        Dim pg As DocumentPage = ClickedPage(sender)
        If pg IsNot Nothing Then
            UndoSave(Me.Pages.ToList())
            Me.Pages.Remove(pg)
        End If
    End Sub

    Private Sub MenuMoveFirst_Click(sender As Object, e As EventArgs)
        Dim pg As DocumentPage = ClickedPage(sender)
        If pg IsNot Nothing Then
            UndoSave(Me.Pages.ToList())
            Me.Controls.SetChildIndex(pg, 0)
        End If
    End Sub

    Private Sub MenuMoveLast_Click(sender As Object, e As EventArgs)
        Dim pg As DocumentPage = ClickedPage(sender)
        If pg IsNot Nothing Then
            UndoSave(Me.Pages.ToList())
            Me.Controls.SetChildIndex(pg, Me.Pages.Count - 1)
        End If
    End Sub

    Private Sub MenuMoveUp_Click(sender As Object, e As EventArgs)
        Dim pg As DocumentPage = ClickedPage(sender)
        If pg IsNot Nothing Then
            UndoSave(Me.Pages.ToList())
            Me.Controls.SetChildIndex(pg, Me.Controls.GetChildIndex(pg) - 1)
        End If
    End Sub

    Private Sub MenuMoveDown_Click(sender As Object, e As EventArgs)
        Dim pg As DocumentPage = ClickedPage(sender)
        If pg IsNot Nothing Then
            UndoSave(Me.Pages.ToList())
            Me.Controls.SetChildIndex(pg, Me.Controls.GetChildIndex(pg) + 1)
        End If
    End Sub

    Private Sub MenuScrollFirst_Click(sender As Object, e As EventArgs)
        Me.CurrentPageIndex = 0
    End Sub

    Private Sub MenuScrollLast_Click(sender As Object, e As EventArgs)
        Me.CurrentPageIndex = Me.Pages.Count - 1
    End Sub

    Private Sub MenuScrollNext_Click(sender As Object, e As EventArgs)
        Me.CurrentPageIndex += 1
    End Sub

    Private Sub MenuScrollPrevious_Click(sender As Object, e As EventArgs)
        Me.CurrentPageIndex -= 1
    End Sub

    Private Sub MenuScrollTo_Click(sender As Object, e As EventArgs)
        Me.CurrentPageIndex = Integer.Parse(DirectCast(sender, ToolStripMenuItem).Text) - 1
    End Sub

    Private Sub SelectedControls_CollectionChanged(sender As Object, e As NotifyCollectionChangedEventArgs) Handles mSelectedControls.CollectionChanged
        Select Case e.Action
            Case NotifyCollectionChangedAction.Add
                If e.NewItems IsNot Nothing Then
                    For Each dc As DisplayControl In e.NewItems
                        dc.Selected = True
                    Next
                End If
            Case NotifyCollectionChangedAction.Remove
                If e.OldItems IsNot Nothing Then
                    For Each dc As DisplayControl In e.OldItems
                        dc.Selected = False
                    Next
                End If
            Case NotifyCollectionChangedAction.Replace
                If e.OldItems IsNot Nothing Then
                    For Each dc As DisplayControl In e.OldItems
                        dc.Selected = False
                    Next
                End If
                If e.NewItems IsNot Nothing Then
                    For Each dc As DisplayControl In e.NewItems
                        dc.Selected = True
                    Next
                End If
            Case NotifyCollectionChangedAction.Reset
                Me.SuspendLayout()
                Try
                    ' Unselect all DisplayControls and set focus to Me.
                    For i As Integer = Me.DisplayControls.Count - 1 To 0 Step -1
                        Me.DisplayControls(i).Selected = False
                    Next
                    Me.Select()
                Finally
                    Me.ResumeLayout()
                End Try
        End Select
    End Sub
#End Region
#End Region
End Class
