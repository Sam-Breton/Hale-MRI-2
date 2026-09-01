Imports System.Collections.ObjectModel
Imports System.Collections.Specialized
Imports System.Windows.Forms.DataVisualization.Charting

Public Class DocumentViewer
    Inherits FlowLayoutPanel
#Region "Types and Constants"
    Public Class DocumentControl
        Public Control As DisplayControl
        Public Index As Integer
    End Class

    Public Class DocumentState
        Public Page As DocumentPage
        Public Controls As IList(Of DocumentControl)
    End Class

    Public Class PasteLocation
        Public Page As DocumentPage
        Public Location As Point
    End Class

    Public Class SelectedControl
        Public Page As DocumentPage
        Public Control As DisplayControl
    End Class

    Private Const kGridSizeMax As Integer = 20              ' Maximum drag/resize grid size in pixels.
    Private Const kGridSizeMin As Integer = 0               ' Minimum drag/resize grid size in pixels.
    Private Const kPageCountMax As Integer = 32             ' Maximum number of DocumentPages we can hold.
    Private Const kPageVerticalSeparation As Integer = 20   ' Vertical separation between DocumentPages in pixels.
    Private Const kPageLeftEdgeMin As Integer = 20          ' Minimum spacing between DocumentPages left edge and the parent form.
    Private Const kScrollHeight As Integer = 30
    Private Const kScrollSpeed As Integer = 3
    Private Const kUndoStackCountMax As Integer = 64        ' Maximum number of undo operations.
    Private Const kZoomDefault As Single = 1.0!             ' Default zoom factor.
    Private Const kZoomMax As Single = 2.0!                 ' Maximum zoom factor
    Private Const kZoomMin As Single = 0.5!                 ' Minimum zoom factor.
#End Region
#Region "Private Members"
    Private mAutoScrollPos As Point = Point.Empty                                                   ' Used to work around AutoScroll snapping during drags.
    Private WithEvents mClipboard As New ObservableCollection(Of ControlData)                       ' List of most recently cut DisplayControls.
    Private mControlsContextMenu As ContextMenuStrip = Nothing                                      ' Generic ContextMenuStrip assigned to DisplayControls.
    Private mCurrentPageIndex As Integer = 0                                                        ' Currently most-visible DocumentPage.
    Private WithEvents mDisplayControls As New ValidatingObservableCollection(Of DisplayControl)    ' Current collection of all DisplayControls.
    Private mDocumentSettings As DocumentSettings = Nothing                                         ' Printer paper settings. 
    Private mDragStartPos As Point = Point.Empty                                                    ' Mouse location at start of drag/resize operation.
    Private mGridSize As Integer = 0                                                                ' Drag/resize operation grid size in pixels.
    Private mIsCentering As Boolean = False                                                         ' Flag to prevent reentrant layout loops
    Private mIsDragging As Boolean = False                                                          ' Indicates a drag operation in progress.
    Private mIsResizing As Boolean = False                                                          ' Indicates a resize operation in progress.
    Private mLayoutChanged As Boolean = False                                                       ' Indicates whether a layout change occurred.
    Private mNoEnter As Boolean = False                                                             ' Indicates the DisplayControl.Enter event should not select the control.
    Private mNoUndo As Boolean = False                                                              ' Indicates whether an event will cause the current state to be pushed onto the UndoStack.
    Protected WithEvents mPages As New ValidatingObservableCollection(Of DocumentPage)               ' Current collection of DocumentPages.
    Private mPasteLocation As PasteLocation = New PasteLocation()                                   ' Base page and location for pasting DisplayControls.
    Private mReset As Boolean = False                                                               ' Indicates whether we're in reset mode.
    Private WithEvents mSelectedControls As New ValidatingObservableCollection(Of DisplayControl)   ' Currently selected DisplayControls.
    Private WithEvents mUndoStack As New ObservableStack(Of List(Of PageData))                      ' LIFO stack of saved DocumentPages.
    Private mVerticalLimit As Integer = 0
    Private mZoom As Single = kZoomDefault
#End Region
#Region "Public Interface"
    Public ReadOnly Property ClipBoard As IList(Of ControlData)
        Get
            Return mClipboard
        End Get
    End Property

    Public Overrides Property ContextMenuStrip As ContextMenuStrip
        Get
            Return MyBase.ContextMenuStrip
        End Get
        Set(value As ContextMenuStrip)
            DocumentPageMenuBindHandlers(value)
            MyBase.ContextMenuStrip = value
        End Set
    End Property

    Public Sub ControlsBringToFront(ByVal controls As IList(Of DisplayControl))
        UndoSave(Me.Pages)
        For Each dc As DisplayControl In controls
            dc.BringToFront()
        Next
    End Sub

    Public Sub ControlsCut(ByVal controls As IList(Of DisplayControl))
        Debug.Print("**** Cut ****")
        mNoEnter = True         ' Disable Windows from automatically selecting any DisplayControls.
        UndoSave(Me.Pages)      ' Save the current DocumentViewer state.
        Me.ClipBoard.Clear()    ' Each cut clears the current contents of the Clipboard.
        For Each dc As DisplayControl In controls
            Dim cData As ControlData = CaptureControl(dc)   ' Capture the DisplayControl's restore data.

            cData.PageIndex = Me.Pages.IndexOf(dc.Parent)   ' This is so we can sort the ClipBoard by each DisplayControl's parent DisplayPage and location.
            Me.ClipBoard.Add(cData)                         ' Add the ControlData to the Clipboard.
            DirectCast(dc.Parent, DocumentPage).DisplayControls.Remove(dc)  ' Remove DisplayControl from its parent DocumentPage.
        Next
        mNoEnter = False        ' Enable normal selecting of DisplayControls.
    End Sub

    Public Sub ControlsDelete(ByVal controls As IList(Of DisplayControl))
        UndoSave(Me.Pages)
        For Each dc As DisplayControl In controls
            DirectCast(dc.Parent, DocumentPage).DisplayControls.Remove(dc)  ' Remove the DisplayControl from its parent DocumentPage.
            'Me.DisplayControls.Remove(dc)
        Next
    End Sub
    Public Sub ControlsPaste(ByVal cData As IList(Of ControlData))
        ' 1. Use your established Transaction method for the Viewer
        Me.TransactionBegin()

        Try
            mNoUndo = True

            Dim sortedData = cData.OrderBy(Function(cd) cd.PageIndex).ThenBy(Function(cd) cd.Bounds.Y).ThenBy(Function(cd) cd.Bounds.X)
            Dim defaultTargetPage As DocumentPage = If(mPasteLocation.Page, Me.Pages(Me.CurrentPageIndex))
            Dim pasteOffset As New Point(mPasteLocation.Location.X - sortedData(0).Bounds.X, mPasteLocation.Location.Y - sortedData(0).Bounds.Y)

            ' Use a HashSet to track which pages received controls 
            ' so we only call TransactionEnd on them once.
            Dim affectedPages As New HashSet(Of DocumentPage)

            For Each cd As ControlData In sortedData
                Dim dc As DisplayControl = DisplayControlCreateFromData(cd)
                Dim pasteLoc As PasteLocation = ComputePasteLocation(cd, pasteOffset)

                Dim page = pasteLoc.Page

                ' 2. Start a transaction on the page if we haven't already
                If Not affectedPages.Contains(page) Then
                    page.TransactionBegin()
                    affectedPages.Add(page)
                End If

                dc.Location = pasteLoc.Location

                ' This triggers the page's internal DisplayControlAdded logic
                page.DisplayControls.Add(dc)

                If dc.Selected Then Me.SelectedControls.Add(dc)
            Next

            ' 3. Close the transactions for all affected pages
            For Each pg In affectedPages
                pg.TransactionEnd()
            Next

            Me.ClipBoard.Clear()

        Finally
            mNoUndo = False
            ' 4. Finalize the Viewer: Centering and PerformLayout happen here
            Me.TransactionEnd()
        End Try
    End Sub

    Private Function ComputePasteLocation(ByVal cd As ControlData, ByVal diff As Point) As PasteLocation
        ' Compute the paste location based on the offset (diff).
        Dim p As Point = cd.Bounds.Location
        p.Offset(diff)

        ' If the new location is out of bounds for the target page, try to find a nearby page in the direction of the offset.
        ' If there are no nearby pages, create a new page and move the control there.
        Dim pg As DocumentPage = Me.Pages(cd.PageIndex)
        If p.Y < pg.VerticalLimit Then
            Dim offsetY As Integer = p.Y - cd.Bounds.Height - pg.ClientRectangle.Top
            If cd.PageIndex > 0 Then
                pg = Me.Pages(cd.PageIndex - 1)
            Else
                pg = DocumentPageAddNew()
                DocumentPageMoveFirst(pg)
            End If
            p = New Point(p.X, pg.ClientRectangle.Bottom + offsetY)
        ElseIf p.Y + cd.Bounds.Height > pg.ClientRectangle.Bottom Then
            Dim offsetY As Integer = p.Y + cd.Bounds.Height - pg.ClientRectangle.Bottom
            If cd.PageIndex < Me.Pages.Count - 1 Then
                pg = Me.Pages(cd.PageIndex + 1)
            Else
                pg = DocumentPageAddNew()
                DocumentPageMoveLast(pg)
            End If
            p = New Point(p.X, pg.ClientRectangle.Top + offsetY)
        End If

        Return New PasteLocation With {.Page = pg, .Location = p}
    End Function

    Public Sub ControlsSelect(ByVal controls As IList(Of DisplayControl))
        Debug.Print("**** Select ****")
        For Each dc As DisplayControl In controls
            If Not dc.Selected Then Me.SelectedControls.Add(dc)
        Next
    End Sub

    Public Sub ControlsSendToBack(ByVal controls As IList(Of DisplayControl))
        UndoSave(Me.Pages)
        For Each dc As DisplayControl In controls
            dc.SendToBack()
        Next
    End Sub

    Public Sub ControlsUndo(ByVal items As IList(Of PageData))
        Debug.Print("**** Undo ****")
        UndoRestore(Me.Pages, items)
    End Sub

    Public Property CurrentPageIndex As Integer
        Get
            Return mCurrentPageIndex
        End Get
        Set(value As Integer)
            ' 1. Validate the range.
            If value >= 0 AndAlso value < Me.Pages.Count Then
                mNoEnter = True     ' Disable Windows from automatically selecting any DisplayControls.
                mCurrentPageIndex = value

                ' 2. Scroll the specific PrintablePage into view.
                Dim targetPage = Me.Pages(mCurrentPageIndex)
                Me.ScrollControlIntoView(targetPage)

                ' 3. Optional: Give it focus so keys like Arrow Down work immediately.
                targetPage.Focus()
                mNoEnter = False    ' Enable normal selecting of DisplayControls.
            End If
        End Set
    End Property

    Public Property DisplayControlContextMenu As ContextMenuStrip
        Get
            Return mControlsContextMenu
        End Get
        Set(value As ContextMenuStrip)
            DisplayControlMenuBindHandlers(value)
            mControlsContextMenu = value
        End Set
    End Property

    Public ReadOnly Property DisplayControls As IList(Of DisplayControl)
        Get
            Return mDisplayControls
        End Get
    End Property

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
    ''' Returns the current collection of DocumentPages as an ObservableCollection.
    ''' </summary>
    ''' <returns>ObservableCollection(Of DocumentPage)</returns>
    Public ReadOnly Property DocumentPages As ObservableCollection(Of DocumentPage)
        Get
            Return mPages
        End Get
    End Property

    Public Property GridSize As Integer
        Get
            Return mGridSize
        End Get
        Set(value As Integer)
            mGridSize = GridSizeSet(value)
        End Set
    End Property

    ''' <summary>
    ''' Returns the current collection of DocumentPages as an IList(Of DocumentPage).
    ''' </summary>
    ''' <returns>IList(Of DocumentPage)</returns>
    Public Overridable ReadOnly Property Pages As IList(Of DocumentPage)
        Get
            Return mPages
        End Get
    End Property

    Public Property MultiSelect As Boolean = False

    Public Sub PageNext()
        If CurrentPageIndex < Me.Controls.Count - 1 Then
            CurrentPageIndex += 1
            ScrollToPage(CurrentPageIndex)
        End If
    End Sub

    Public Sub PagePrevious()
        If CurrentPageIndex > 0 Then
            CurrentPageIndex -= 1
            ScrollToPage(CurrentPageIndex)
        End If
    End Sub

    ''' <summary>
    ''' Removes all DocumentViewer content and clears the ClipBoard and UndoStack.
    ''' </summary>
    Public Sub Reset()
        mPages.BeginTransaction()
        Me.UndoStack.Clear()
        Me.ClipBoard.Clear()
        Me.Pages.Clear()
        mPages.EndTransaction()
        mReset = True
    End Sub

    Public Sub TransactionBegin()
        mPages.BeginTransaction()
    End Sub

    Public Sub TransactionEnd()
        mPages.EndTransaction()
    End Sub

    Public ReadOnly Property SelectedControls As IList(Of DisplayControl)
        Get
            Return mSelectedControls
        End Get
    End Property

    Public Sub Start()
        Me.Select()
        mReset = False
    End Sub

    Public ReadOnly Property UndoStack As ObservableStack(Of List(Of PageData))
        Get
            Return mUndoStack
        End Get
    End Property

    Public Property VerticalLimit As Integer
        Get
            Return mVerticalLimit
        End Get
        Set(value As Integer)
            VerticalLimitSet(value)
            mVerticalLimit = value
        End Set
    End Property

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
    Private Sub CenterContent()
        If Me.Pages.Count > 0 Then
            If Not mIsCentering Then
                mIsCentering = True

                Try
                    Dim pageWidth As Integer = Me.Pages(0).Width
                    Dim availableSpace As Integer = Me.ClientSize.Width ' Use DisplayRectangle.Width if centering is off by scrollbar width ~17 pixels.
                    Dim hPad As Integer = Math.Max(0, (availableSpace - pageWidth) \ 2)
                    Dim newPadding As New Padding(hPad, Me.Padding.Top, hPad, Me.Padding.Bottom)

                    If Not Me.Padding.Equals(newPadding) Then
                        Me.Padding = newPadding
                    End If
                Finally
                    mIsCentering = False
                End Try
            End If
        End If
    End Sub


    Private Sub ChartDesignerIterateCharts(ByVal controls As Control.ControlCollection, ByRef chartList As List(Of Chart))
        For Each ctrl As Control In controls
            If TypeOf ctrl Is Chart Then
                chartList.Add(CType(ctrl, Chart))
            End If

            If ctrl.HasChildren Then
                ChartDesignerIterateCharts(ctrl.Controls, chartList)
            End If
        Next
    End Sub

    Private Sub ChartDesignerOpen()
        Dim chartList As New List(Of Chart)
        Dim frm As New FrmChartDesigner()

        For Each dc As DisplayControl In Me.SelectedControls
            ChartDesignerIterateCharts(dc.Controls, chartList)
        Next

        frm.Charts = chartList
        frm.Show()
    End Sub

    Private Function ClickedPage(sender As Object) As DocumentPage
        ' Returns sender's owning DocumentPage.
        Dim cms As ContextMenuStrip = TryCast(DirectCast(sender, ToolStripMenuItem).Owner, ContextMenuStrip)
        Return cms?.SourceControl
    End Function

    Private Sub ClipboardItemAdded(ByVal item As ControlData)

    End Sub

    Private Sub ClipboardItemRemoved(ByVal item As ControlData)

    End Sub

    Private Function DisplayControlCreateFromData(ByVal cData As ControlData) As DisplayControl
        Dim dc As DisplayControl = DisplayControl.CreateInstance($"{Me.GetType().Namespace}.{cData.Name}")

        dc.Id = cData.ID
        dc.BaseLocation = cData.BaseLocation
        dc.Basis = cData.Basis
        dc.BaseSize = cData.BaseSize
        dc.Bounds = cData.Bounds
        dc.DisplayName = cData.DisplayName
        dc.DragEdgeSize = cData.DragEdgeSize
        dc.IsMovable = cData.IsMovable
        dc.IsSelectable = cData.IsSelectable
        dc.IsSizeable = cData.IsSizeable
        dc.LastPosition = cData.LastPosition
        dc.LastSize = cData.LastSize
        dc.MaxSize = cData.MaxSize
        dc.MinSize = cData.MinSize
        dc.Name = cData.Name
        dc.Precision = cData.Precision
        dc.Selected = cData.Selected
        dc.SelectionBorderColor = cData.SelectionBorderColor
        dc.SelectionBorderSize = cData.SelectionBorderSize
        dc.TolClass = cData.TolClass

        Return dc
    End Function

    Private Sub DisplayControlMenuApplyHandlers(ByVal dc As DisplayControl, ByVal menu As ContextMenuStrip)
        ' Append to or create the DisplayControl's ContextMenuStrip.
        DisplayControlMenuInitialize(dc)

        ' Add each generic menu item to the DisplayControl's specific ContextMenuStrip.
        For Each item As ToolStripItem In menu.Items
            If TypeOf item Is ToolStripMenuItem Then
                Dim templateItem = DirectCast(item, ToolStripMenuItem)

                ' Create a clone
                Dim clone As New ToolStripMenuItem(templateItem.Text, templateItem.Image)
                clone.Name = templateItem.Name ' Use this to identify which handler to attach.

                ' Attach handlers to the clone.
                Select Case clone.Name
                    Case "BringToFrontToolStripMenuItem" : AddHandler clone.Click, AddressOf Me.DisplayControlBringToFront_Click
                    Case "CutToolStripMenuItem" : AddHandler clone.Click, AddressOf Me.DisplayControlCut_Click
                    Case "DeleteToolStripMenuItem" : AddHandler clone.Click, AddressOf Me.DisplayControlDelete_Click
                    Case "PasteToolStripMenuItem" : AddHandler clone.Click, AddressOf Me.DisplayControlPaste_Click
                    Case "SelectAllToolStripMenuItem" : AddHandler clone.Click, AddressOf Me.DisplayControlSelectAll_Click
                    Case "SendToBackToolStripMenuItem" : AddHandler clone.Click, AddressOf Me.DisplayControlSendToBack_Click
                    Case "ThemeEditorToolStripMenuItem" : AddHandler clone.Click, AddressOf Me.DisplayControlThemeEditor_Click
                    Case "UndoToolStripMenuItem" : AddHandler clone.Click, AddressOf Me.DisplayControlUndo_Click
                End Select
                dc.ContextMenuStrip.Items.Add(clone)
            ElseIf TypeOf item Is ToolStripSeparator Then
                dc.ContextMenuStrip.Items.Add(New ToolStripSeparator())
            End If
        Next

        RemoveHandler dc.ContextMenuStrip.Opening, AddressOf Me.DisplayControlMenu_Opening
        AddHandler dc.ContextMenuStrip.Opening, AddressOf Me.DisplayControlMenu_Opening
    End Sub

    Private Sub DisplayControlMenuBindHandlers(ByVal menu As ContextMenuStrip)
        ' Apply the generic menu to each existing DisplayControl.
        If menu IsNot Nothing Then
            For Each dc As DisplayControl In Me.DisplayControls
                DisplayControlMenuApplyHandlers(dc, menu)
            Next
        End If
    End Sub

    Private Sub DisplayControlMenuInitialize(ByVal dc As DisplayControl)
        'Create a new menu if the DisplayControl doesn't already have one.
        If dc.ContextMenuStrip Is Nothing Then
            dc.ContextMenuStrip = New ContextMenuStrip()
            Return ' Nothing to clean up.
        End If

        'Locate our "Anchor" separator
        Dim sep = dc.ContextMenuStrip.Items("GenericToolStripSeparator")

        If sep IsNot Nothing Then
            ' Remove everything from that separator onwards.
            Dim startIndex = dc.ContextMenuStrip.Items.IndexOf(sep)
            For i As Integer = dc.ContextMenuStrip.Items.Count - 1 To startIndex Step -1
                ' Dispose items to free up resources (images, etc.)
                Dim item = dc.ContextMenuStrip.Items(i)
                dc.ContextMenuStrip.Items.RemoveAt(i)
                item.Dispose()
            Next
        End If

        'Add the fresh anchor if there are any items in the menu.
        If dc.ContextMenuStrip.Items.Count > 0 Then
            dc.ContextMenuStrip.Items.Add(New ToolStripSeparator() With {.Name = "GenericToolStripSeparator"})
        End If
    End Sub

    Private Sub DisplayControlAdded(ByVal dc As DisplayControl)
        AddHandler dc.Enter, AddressOf Me.DisplayControl_Enter
        AddHandler dc.MouseDown, AddressOf Me.DisplayControl_MouseDown
        AddHandler dc.MouseMove, AddressOf Me.DisplayControl_MouseMove
        AddHandler dc.MouseUp, AddressOf Me.DisplayControl_MouseUp
        If mControlsContextMenu IsNot Nothing Then
            DisplayControlMenuApplyHandlers(dc, mControlsContextMenu)
        End If
        If dc.BaseLocation = Point.Empty Then dc.BaseLocation = New Point(dc.Location.X / Me.Zoom, dc.Location.Y / Me.Zoom)
        If dc.BaseSize = Size.Empty Then dc.BaseSize = New Size(dc.Size.Width / Me.Zoom, dc.Size.Height / Me.Zoom)
        dc.LastPosition = dc.Location
        dc.LastSize = dc.Size
        dc.DragEdgeSize = Me.GridSize
        If dc.Selected Then Me.SelectedControls.Add(dc)
        dc.Visible = True
        ' Register this control to see every message the app receives so we can intercept mouse events.
        Application.AddMessageFilter(dc)
    End Sub

    Private Sub DisplayControlDrag(ByRef dc As DisplayControl, ByVal sender As DisplayControl, ByVal location As Point, ByVal pg As DocumentPage, ByVal e As MouseEventArgs, ByVal delta As Point)
        ' Move the control to location and, if set, to the given page.
        If pg Is Nothing Then
            dc.Location = location
        Else
            dc.Parent = pg
            dc.Location = location
            If dc Is sender Then
                System.Windows.Forms.Cursor.Position = dc.PointToScreen(dc.DragOffset)
                mDragStartPos = dc.PointToClient(System.Windows.Forms.Cursor.Position)
            End If
        End If
    End Sub

    Private Sub DisplayControlEnter(dc As DisplayControl, e As EventArgs)
        If Not (mNoEnter OrElse dc.Selected) Then DisplayControlSelect(dc)
    End Sub

    Private Sub DisplayControlMouseDown(dc As DisplayControl, e As MouseEventArgs)
        If Not dc.Selected Then DisplayControlSelect(dc)
        If dc.Selected Then DisplayControlsDragStart(dc, e)
    End Sub

    Private Sub DisplayControlMouseMove(dc As DisplayControl, e As MouseEventArgs)
        If mIsDragging Then
            DisplayControlsDragMove(dc, e)
        ElseIf mIsResizing Then
            DisplayControlsResizeMove(dc, e)
        End If
    End Sub

    Private Sub DisplayControlRemoved(dc As DisplayControl)
        If Not dc.IsDisposed Then
            Me.SelectedControls.Remove(dc)
            RemoveHandler dc.Enter, AddressOf Me.DisplayControl_Enter
            RemoveHandler dc.MouseDown, AddressOf Me.DisplayControl_MouseDown
            RemoveHandler dc.MouseMove, AddressOf Me.DisplayControl_MouseMove
            RemoveHandler dc.MouseUp, AddressOf Me.DisplayControl_MouseUp
            dc.Dispose()
        End If
    End Sub

    Private Sub DisplayControlSelect(dc As DisplayControl)
        If Not Me.MultiSelect Then Me.SelectedControls.Clear()  ' If MultiSelect isn't enabled then unselect all DisplayControls.
        Me.SelectedControls.Add(dc)                               ' Select the given DisplayControl. 
    End Sub

    Private Sub DisplayControlsDragEnd(dc As DisplayControl)
        LayoutSave(Me.SelectedControls)
        mIsDragging = False
        mIsResizing = False
        dc.Capture = False
    End Sub

    Private Sub DisplayControlsDragMove(sender As DisplayControl, e As MouseEventArgs)
        ' Drag selected controls to a new location. Sender is the grabbed control.
        '
        ' Get the mouse position offset from the drag start location.
        Dim deltaX As Integer = e.Location.X - mDragStartPos.X
        Dim deltaY As Integer = e.Location.Y - mDragStartPos.Y

        ' Apply grid snapping if GridSize is set
        If Me.GridSize > 0 Then
            deltaX = Math.Round(deltaX / Me.GridSize) * Me.GridSize
            deltaY = Math.Round(deltaY / Me.GridSize) * Me.GridSize
        End If
        If deltaX = 0 AndAlso deltaY = 0 Then Return

        ' Check all moveable controls. If any control can't
        ' be moved, then none will be moved (once any control
        ' gets "stuck" dragging stops for all controls).
        Dim movements As New List(Of ValueTuple(Of DisplayControl, Point, DocumentPage))
        For Each dc As DisplayControl In Me.SelectedControls
            If dc.IsMovable Then
                Dim newBounds As New Rectangle(New Point(dc.Left + deltaX, dc.Top + deltaY), dc.Size)
                Dim boundsCheck = dc.BoundsCheck(newBounds)

                If boundsCheck = DisplayControl.BoundsChecks.None Then
                    movements.Add((dc, New Point(dc.Left + deltaX, dc.Top + deltaY), Nothing))
                ElseIf (boundsCheck And (boundsCheck - 1)) <> 0 Then
                    Return
                ElseIf boundsCheck.HasFlag(DisplayControl.BoundsChecks.Left) OrElse boundsCheck.HasFlag(DisplayControl.BoundsChecks.Right) Then
                    'movements.Add((dc, New Point(dc.Left, dc.Top + deltaY), Nothing))
                    Return
                ElseIf boundsCheck.HasFlag(DisplayControl.BoundsChecks.Top) Then
                    Dim parentPage As Integer = Me.Pages.IndexOf(DirectCast(dc.Parent, DocumentPage))
                    If parentPage > 0 Then
                        Dim previousPage As DocumentPage = Me.Pages(parentPage - 1)
                        movements.Add((dc, New Point(dc.Left, previousPage.ClientRectangle.Bottom - dc.Height), previousPage))
                    Else
                        Return
                        'movements.Add((dc, New Point(dc.Left + deltaX, dc.Top), Nothing))
                    End If
                ElseIf boundsCheck.HasFlag(DisplayControl.BoundsChecks.Bottom) Then
                    Dim parentPage As Integer = Me.Pages.IndexOf(DirectCast(dc.Parent, DocumentPage))
                    If parentPage < Me.Pages.Count - 1 Then
                        Dim nextPage As DocumentPage = Me.Pages(parentPage + 1)
                        movements.Add((dc, New Point(dc.Left, nextPage.ClientRectangle.Top), nextPage))
                    Else
                        Return
                        'movements.Add((dc, New Point(dc.Left + deltaX, dc.Top), Nothing))
                    End If
                End If
            End If
        Next

        ' Now move the controls all at once.
        RemoveHandler sender.MouseMove, AddressOf Me.DisplayControl_MouseMove
        For Each movement As ValueTuple(Of DisplayControl, Point, DocumentPage) In movements
            Try
                DisplayControlDrag(movement.Item1, sender, movement.Item2, movement.Item3, e, New Point(deltaX, deltaY))
            Catch ex As Exception
                ' Swallow any errors and keep going.
            End Try
        Next

        ' If the mouse is near the edge of the viewer, scroll in that direction.
        Dim mouseInViewer As Point = Me.PointToClient(sender.PointToScreen(e.Location))
        DisplayControlsHandleScrolling(mouseInViewer)

        AddHandler sender.MouseMove, AddressOf Me.DisplayControl_MouseMove
        mLayoutChanged = True
    End Sub

    Private Sub DisplayControlsHandleScrolling(mousePos As Point)
        ' mousePos should be relative to the DocumentViewer (PointToClient)

        Dim currentScroll = Me.AutoScrollPosition
        Dim newX As Integer = Math.Abs(currentScroll.X)
        Dim newY As Integer = Math.Abs(currentScroll.Y)

        ' Check Bottom Edge
        If mousePos.Y > (Me.Height - kScrollHeight) Then
            newY += 3
            ' Check Top Edge
        ElseIf mousePos.Y < kScrollHeight Then
            newY -= kScrollSpeed
        End If

        ' Apply the new scroll position
        ' Note: AutoScrollPosition must be set with positive values for X/Y
        Me.AutoScrollPosition = New Point(newX, newY)
    End Sub

    Private Sub DisplayControlsDragStart(ByVal dc As DisplayControl, e As MouseEventArgs)
        Select Case dc.DragType
            Case DisplayControl.DragTypes.Move
                mIsDragging = True
                mDragStartPos = e.Location
            Case DisplayControl.DragTypes.Resize
                mIsResizing = True
                mDragStartPos = System.Windows.Forms.Cursor.Position
            Case Else
                mIsDragging = False
                mIsResizing = False
                dc.Capture = False
                Return
        End Select
        dc.Capture = True
        UndoSave(Me.Pages)
    End Sub

    Private Sub DisplayControlsResizeMove(sender As DisplayControl, e As MouseEventArgs)
        ' Resize the selected controls. Sender is the grabbed control.

        ' Get the mouse position offset from the drag start location.
        Dim deltaX As Integer = System.Windows.Forms.Cursor.Position.X - mDragStartPos.X
        Dim deltaY As Integer = System.Windows.Forms.Cursor.Position.Y - mDragStartPos.Y
        ' Apply grid snapping if GridSize is set
        If GridSize > 0 Then
            deltaX = Math.Round(deltaX / GridSize) * GridSize
            deltaY = Math.Round(deltaY / GridSize) * GridSize
        End If
        If deltaX = 0 And deltaY = 0 Then Return
        ' Check all sizeable controls.
        Dim resizes As New List(Of ValueTuple(Of Rectangle, DisplayControl))
        Dim newBounds As Rectangle
        For Each dc In Me.SelectedControls
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
                Dim dcBoundsCheck = dc.BoundsCheck(newBounds)
                Select Case sender.ResizePoint
                    Case DisplayControl.ResizePoints.RightEdge, DisplayControl.ResizePoints.LeftEdge, DisplayControl.ResizePoints.TopEdge, DisplayControl.ResizePoints.BottomEdge
                        If dcBoundsCheck <> DisplayControl.BoundsChecks.None Then Return
                    Case DisplayControl.ResizePoints.TopRightCorner
                        If dcBoundsCheck.HasFlag(DisplayControl.BoundsChecks.Right) AndAlso dcBoundsCheck.HasFlag(DisplayControl.BoundsChecks.Top) Then
                            Return
                        ElseIf dcBoundsCheck.HasFlag(DisplayControl.BoundsChecks.Right) Then
                            newBounds.Width = dc.Width
                        ElseIf dcBoundsCheck.HasFlag(DisplayControl.BoundsChecks.Top) Then
                            newBounds.Y = dc.Top
                            newBounds.Height = dc.Height
                        End If
                    Case DisplayControl.ResizePoints.BottomRightCorner
                        If dcBoundsCheck.HasFlag(DisplayControl.BoundsChecks.Right) AndAlso dcBoundsCheck.HasFlag(DisplayControl.BoundsChecks.Bottom) Then
                            Return
                        ElseIf dcBoundsCheck.HasFlag(DisplayControl.BoundsChecks.Right) Then
                            newBounds.Width = dc.Width
                        ElseIf dcBoundsCheck.HasFlag(DisplayControl.BoundsChecks.Bottom) Then
                            newBounds.Height = dc.Height
                        End If
                    Case DisplayControl.ResizePoints.TopLeftCorner
                        If dcBoundsCheck.HasFlag(DisplayControl.BoundsChecks.Left) AndAlso dcBoundsCheck.HasFlag(DisplayControl.BoundsChecks.Top) Then
                            Return
                        ElseIf dcBoundsCheck.HasFlag(DisplayControl.BoundsChecks.Left) Then
                            newBounds.X = dc.Left
                            newBounds.Width = dc.Width
                        ElseIf dcBoundsCheck.HasFlag(DisplayControl.BoundsChecks.Top) Then
                            newBounds.Y = dc.Top
                            newBounds.Height = dc.Height
                        End If
                    Case DisplayControl.ResizePoints.BottomLeftCorner
                        If dcBoundsCheck.HasFlag(DisplayControl.BoundsChecks.Left) AndAlso dcBoundsCheck.HasFlag(DisplayControl.BoundsChecks.Bottom) Then
                            Return
                        ElseIf dcBoundsCheck.HasFlag(DisplayControl.BoundsChecks.Left) Then
                            newBounds.X = dc.Left
                            newBounds.Width = dc.Width
                        ElseIf dcBoundsCheck.HasFlag(DisplayControl.BoundsChecks.Bottom) Then
                            newBounds.Height = dc.Height
                        End If
                End Select
                resizes.Add((newBounds, dc))
            End If
        Next

        ' Now resize the controls all at once.
        For Each resize As ValueTuple(Of Rectangle, DisplayControl) In resizes
            Try
                resize.Item2.Bounds = resize.Item1
            Catch ex As Exception
                ' Swallow the error and keep going.
            End Try
        Next
        mLayoutChanged = True
    End Sub

    Protected Overridable Sub DocumentPageAdded(pg As DocumentPage)
        'pg.SuspendLayout()
        pg.TransactionBegin()   ' This, rather than SuspendLayout() prevents the repaint glitvhing when changing the document size.
        pg.Margin = New Padding(0, 0, 0, kPageVerticalSeparation)
        pg.ContextMenuStrip = Me.ContextMenuStrip
        pg.RightToLeft = RightToLeft.No
        AddHandler pg.ControlAdded, AddressOf Me.DocumentPage_ControlAdded
        AddHandler pg.ControlRemoved, AddressOf Me.DocumentPage_ControlRemoved
        AddHandler pg.KeyDown, AddressOf Me.DocumentPage_KeyDown
        AddHandler pg.KeyUp, AddressOf Me.DocumentPage_KeyUp
        AddHandler pg.MouseDown, AddressOf Me.DocumentPage_MouseDown
        AddHandler pg.MouseUp, AddressOf Me.DocumentPage_MouseUp
        For Each dc As DisplayControl In pg.DisplayControls
            Me.DisplayControls.Add(dc)
        Next
        pg.Document = Me.Document
        pg.Zoom = Me.Zoom
        Me.Controls.Add(pg)
        PageAddToMenuStrip(pg)
        pg.TransactionEnd() ' See pg.TransactionBegin(), above.
    End Sub

    Protected Overridable Function DocumentPageAddNew(Optional ByVal pg As DocumentPage = Nothing) As DocumentPage
        UndoSave(Me.Pages)
        If pg Is Nothing Then
            pg = New DocumentPage()
        End If
        Me.Pages.Add(pg)
        Return pg
    End Function

    Private Sub DocumentPageDelete(pg As DocumentPage)
        If pg IsNot Nothing Then
            UndoSave(Me.Pages)
            Me.Pages.Remove(pg)
        End If
    End Sub

    Private Sub DocumentPageEditMenuBindHandlers(menu As ToolStripDropDown)
        If menu IsNot Nothing Then
            Dim pageCutItem = menu.Items("PageCutToolStripMenuItem")
            Dim pageDeleteItem = menu.Items("PageDeleteToolStripMenuItem")
            Dim pagePasteItem = menu.Items("PagePasteToolStripMenuItem")
            Dim pageSelectAllItem = menu.Items("PageSelectAllToolStripMenuItem")
            Dim pageUndoItem = menu.Items("PageUndoToolStripMenuItem")

            AddHandler menu.Opening, AddressOf Me.DocumentPageControlsMenu_Opening

            If pageCutItem IsNot Nothing Then
                AddHandler pageCutItem.Click, AddressOf DocumentPageMenuPageCut_Click
            End If
            If pageDeleteItem IsNot Nothing Then
                AddHandler pageDeleteItem.Click, AddressOf DocumentPageMenuPageDelete_Click
            End If
            If pagePasteItem IsNot Nothing Then
                AddHandler pagePasteItem.Click, AddressOf DocumentPageMenuPagePaste_Click
            End If
            If pageSelectAllItem IsNot Nothing Then
                AddHandler pageSelectAllItem.Click, AddressOf DocumentPageMenuPageSelectAll_Click
            End If
            If pageUndoItem IsNot Nothing Then
                AddHandler pageUndoItem.Click, AddressOf DocumentPageMenuPageUndo_Click
            End If
        End If
    End Sub

    Private Sub DocumentPageMenuBindHandlers(menu As ContextMenuStrip)
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
            Dim pageEditItem = menu.Items("PageEditToolStripMenuItem")

            ' Assign event handlers if they exists.
            AddHandler menu.Opening, AddressOf Me.DocumentPageMenu_Opening

            If addNewItem IsNot Nothing Then
                AddHandler addNewItem.Click, AddressOf DocumentPageMenuAddNew_Click
            End If

            If deletePageItem IsNot Nothing Then
                AddHandler deletePageItem.Click, AddressOf DocumentPageMenuDelete_Click
            End If

            If moveFirstItem IsNot Nothing Then
                AddHandler moveFirstItem.Click, AddressOf DocumentPageMenuMoveFirst_Click
            End If
            If moveLastItem IsNot Nothing Then
                AddHandler moveLastItem.Click, AddressOf DocumentPageMenuMoveLast_Click
            End If
            If moveUpItem IsNot Nothing Then
                AddHandler moveUpItem.Click, AddressOf DocumentPageMenuMoveUp_Click
            End If
            If moveDownItem IsNot Nothing Then
                AddHandler moveDownItem.Click, AddressOf DocumentPageMenuMoveDown_Click
            End If
            If pageEditItem IsNot Nothing Then
                DocumentPageEditMenuBindHandlers(DirectCast(pageEditItem, ToolStripMenuItem).DropDown)
            End If
            If scrollFirstItem IsNot Nothing Then
                AddHandler scrollFirstItem.Click, AddressOf DocumentPageMenuScrollFirst_Click
            End If
            If scrollLastItem IsNot Nothing Then
                AddHandler scrollLastItem.Click, AddressOf DocumentPageMenuScrollLast_Click
            End If
            If scrollNextItem IsNot Nothing Then
                AddHandler scrollNextItem.Click, AddressOf DocumentPageMenuScrollNext_Click
            End If
            If scrollPreviousItem IsNot Nothing Then
                AddHandler scrollPreviousItem.Click, AddressOf DocumentPageMenuScrollPrevious_Click
            End If
            If scrollToItem IsNot Nothing Then
                scrollToItem.Enabled = Me.Controls.Count > 1
            End If
        End If
    End Sub

    Private Sub DocumentPageMouseDown(pg As DocumentPage)
        If Not Me.MultiSelect Then Me.SelectedControls.Clear()
    End Sub

    Private Sub DocumentPageMoveDown(pg As DocumentPage)
        If pg IsNot Nothing Then
            UndoSave(Me.Pages)
            Me.Controls.SetChildIndex(pg, Me.Controls.GetChildIndex(pg) + 1)
        End If
    End Sub

    Private Sub DocumentPageMoveFirst(pg As DocumentPage)
        If pg IsNot Nothing Then
            UndoSave(Me.Pages)
            Me.Controls.SetChildIndex(pg, 0)
        End If
    End Sub

    Private Sub DocumentPageMoveLast(pg As DocumentPage)
        If pg IsNot Nothing Then
            UndoSave(Me.Pages)
            Me.Controls.SetChildIndex(pg, Me.Pages.Count - 1)
        End If
    End Sub

    Private Sub DocumentPageMoveUp(pg As DocumentPage)
        If pg IsNot Nothing Then
            UndoSave(Me.Pages.ToList())
            Me.Controls.SetChildIndex(pg, Me.Controls.GetChildIndex(pg) - 1)
        End If
    End Sub

    Protected Overridable Sub DocumentPageRemoved(pg As DocumentPage)
        If Not pg.IsDisposed Then
            While pg.DisplayControls.Count > 0
                pg.DisplayControls.Remove(pg.DisplayControls(0))
            End While
            RemoveHandler pg.ControlAdded, AddressOf Me.DocumentPage_ControlAdded
            RemoveHandler pg.ControlRemoved, AddressOf Me.DocumentPage_ControlRemoved
            RemoveHandler pg.KeyDown, AddressOf Me.DocumentPage_KeyDown
            RemoveHandler pg.KeyUp, AddressOf Me.DocumentPage_KeyUp
            RemoveHandler pg.MouseDown, AddressOf Me.DocumentPage_MouseDown
            RemoveHandler pg.MouseUp, AddressOf Me.DocumentPage_MouseUp
            PageRemoveFromMenuStrip(pg)
            Me.Controls.Remove(pg)
            pg.Dispose()
        Else
        End If
    End Sub

    Private Sub DocumentPageScrollFirst()
        Me.CurrentPageIndex = 0
    End Sub

    Private Sub DocumentPageScrollLast()
        Me.CurrentPageIndex = Me.Pages.Count - 1
    End Sub

    Private Sub DocumentPageScrollNext()
        Me.CurrentPageIndex += 1
    End Sub

    Private Sub DocumentPageScrollPrevious()
        Me.CurrentPageIndex -= 1
    End Sub

    Private Sub DocumentPageScrollTo(index As Integer)
        Me.CurrentPageIndex = index
    End Sub

    Private Sub DocumentSet(ByVal doc As DocumentSettings)
        Me.TransactionBegin()
        Try
            For Each pg In mPages
                pg.Document = doc
                pg.Zoom = Me.Zoom
            Next
        Finally
            Me.TransactionEnd()
        End Try
    End Sub

    Private Function GridSizeSet(ByVal size As Integer) As Integer
        ' Set the grid size for all DisplayControls.
        size = Math.Clamp(size, kGridSizeMin, kGridSizeMax)
        For Each dc As DisplayControl In Me.DisplayControls
            dc.DragEdgeSize = size
        Next
        Return size
    End Function

    Private Sub LayoutSave(controls As ICollection(Of DisplayControl))
        If mLayoutChanged Then
            For Each dc As DisplayControl In controls
                dc.BaseLocation = New Point(dc.Location.X / Me.Zoom, dc.Location.Y / Me.Zoom)
                dc.BaseSize = New Size(dc.Size.Width / Me.Zoom, dc.Size.Height / Me.Zoom)
                dc.LastPosition = dc.Location
                dc.LastSize = dc.Size
            Next
            mLayoutChanged = False
        ElseIf Me.UndoStack.Count > 0 Then
            ' If the layout hasn't changed since the last save, then pop the undo stack to prevent an invalid redo.
            Dim unused = Me.UndoStack.Pop()
        End If
    End Sub

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        CenterContent()
    End Sub

    Protected Overrides Sub OnScroll(e As ScrollEventArgs)
        MyBase.OnScroll(e)
        ' Define the center of the visible viewport
        ' ClientSize.Height gives the visible area, and we find its midpoint.
        Dim viewportCenter As Integer = Me.ClientSize.Height / 2

        Dim closestIndex As Integer = -1
        Dim minDistance As Integer = Integer.MaxValue

        ' Iterate through pages to find the one closest to the viewport center.
        For i As Integer = 0 To Me.Pages.Count - 1
            Dim pg As DocumentPage = Me.Pages(i)

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

        ' Update the property if the prominent page has changed.
        If closestIndex <> -1 AndAlso closestIndex <> mCurrentPageIndex Then
            mCurrentPageIndex = closestIndex
            ' Raise an event here if the ParentForm needs to update its Menu Dropdown.
        End If
        If Me.AutoScrollPosition <> mAutoScrollPos Then
            mAutoScrollPos = Me.AutoScrollPosition
        End If
    End Sub

    Private Sub PageAddToMenuStrip(ByVal pg As DocumentPage)
        Dim menuItem As ToolStripMenuItem = Me.ContextMenuStrip?.Items("ScrollToToolStripMenuItem")
        If menuItem IsNot Nothing Then
            Dim item As New ToolStripMenuItem($"{Me.Pages.Count}")
            AddHandler item.Click, AddressOf Me.DocumentPageMenuScrollTo_Click
            menuItem.DropDownItems.Add(item)
        End If
    End Sub

    Private Sub PageRemoveFromMenuStrip(ByVal pg As DocumentPage)
        Dim menuItem As ToolStripMenuItem = Me.ContextMenuStrip?.Items("ScrollToToolStripMenuItem")
        If menuItem IsNot Nothing Then
            Dim i As Integer = Me.Controls.GetChildIndex(pg)
            Dim item As ToolStripMenuItem = menuItem.DropDownItems(i)
            RemoveHandler item.Click, AddressOf Me.DocumentPageMenuScrollTo_Click
            menuItem.DropDownItems.Remove(item)
            For j As Integer = i To menuItem.DropDownItems.Count - 1
                menuItem.DropDownItems(j).Text = $"{j + 1}"
            Next
        End If
    End Sub

    Private Sub PageZoom(pg As DocumentPage, ByVal zoomFactor As Single)
        ' Calculate absolute size from the original 100% dimensions
        Dim newW As Integer = CInt(Math.Round(pg.OriginalSize.Width * zoomFactor, 0))
        Dim newH As Integer = CInt(Math.Round(pg.OriginalSize.Height * zoomFactor, 0))

        ' Set the size directly (Zero drift)
        pg.Size = New Size(newW, newH)

        ' Update margin
        Dim absoluteBottomMargin As Integer = CInt(Math.Round(kPageVerticalSeparation * zoomFactor, 0))
        pg.Margin = New Padding(0, 0, 0, absoluteBottomMargin)
    End Sub

    Private Sub Repaginate(ByVal pgs As IEnumerable(Of DocumentPage))
        ' Rename each DocumentPage according to its index in the collection.
        For i As Integer = 0 To pgs.Count - 1
            pgs(i).Name = $"Page {i + 1}"
        Next
    End Sub

    Protected Overrides Function ScrollToControl(activeControl As Control) As Point
        ' Don't AutoScroll to the active control if we're in the middle of a drag or resize operation since
        ' it interferes with it and we handle that elsewhere.
        If mIsDragging OrElse mIsResizing Then
            Return Me.DisplayRectangle.Location
        Else
            Return MyBase.ScrollToControl(activeControl)
        End If
    End Function

    Private Sub ScrollToPage(ByVal index As Integer)
        If index >= 0 AndAlso index < Me.Controls.Count Then
            Dim targetPage = Me.Controls(index)
            Me.ScrollControlIntoView(targetPage)
        End If
    End Sub

    Private Sub SelectedControlAdded(ByVal dc As DisplayControl)
        dc.Selected = True
    End Sub

    Private Sub SelectedControlRemoved(ByVal dc As DisplayControl)
        dc.Selected = False
    End Sub

    Protected Overridable Sub UndoRestore(ByVal restoreTo As IList(Of DocumentPage), ByVal restoreFrom As IList(Of PageData))
        ' Restore the previous DocumentViewer state from the PageData.
        ' 1. Silence the entire Viewer
        Me.TransactionBegin()
        Try
            restoreTo.Clear()
            For Each pData As PageData In restoreFrom
                Dim pg As New DocumentPage() With {
                    .Name = pData.Name,
                    .OriginalSize = pData.OriginalSize,
                    .VerticalLimit = pData.VerticalLimit
                }
                Me.Pages.Add(pg)

                For Each cData As ControlData In pData.Controls
                    Dim dc As DisplayControl = DisplayControlCreateFromData(cData)

                    pg.DisplayControls.Add(dc)
                Next
            Next
        Finally
            Me.TransactionEnd()
        End Try
    End Sub

    Protected Overridable Sub UndoSave(ByVal pgs As IList(Of DocumentPage))
        ' Save the current DocumentViewer state on the stack.
        If Not mReset AndAlso Me.UndoStack.Count < kUndoStackCountMax Then
            Me.UndoStack.Push(CapturePages(pgs))
        End If
    End Sub

    Private Sub UndoStackItemsAdded(ByVal items As IList(Of PageData))

    End Sub

    Private Sub UndoStackItemsRemoved(ByVal items As IList(Of PageData))

    End Sub

    Private Sub VerticalLimitSet(ByVal limit As Integer)
        ' 1. Suspend the entire Viewer
        Me.TransactionBegin()
        Try
            For Each pg In mPages
                ' 2. Delegate the move to the Page
                ' (This property setter in DocumentPage will handle the loop)
                pg.VerticalLimit = limit
            Next
        Finally
            ' 3. Resume, Re-center, and PerformLayout ONCE
            Me.TransactionEnd()
        End Try
    End Sub

    Private Sub ZoomSet(ByRef factor As Single)
        factor = Math.Clamp(factor, kZoomMin, kZoomMax)
        Me.TransactionBegin()
        Try
            For Each pg In mPages
                pg.Zoom = factor
            Next
        Finally
            Me.TransactionEnd()
        End Try
    End Sub
#End Region
#Region "Event Handlers"
#Region "DocumentViewer Events"
    Private Sub Clipboard_CollectionChanged(sender As Object, e As NotifyCollectionChangedEventArgs) Handles mClipboard.CollectionChanged
        Select Case e.Action
            Case NotifyCollectionChangedAction.Reset

            Case Else
                If e.OldItems IsNot Nothing Then
                    For Each cd As ControlData In e.NewItems
                        ClipboardItemRemoved(cd)
                    Next
                End If
                If e.NewItems IsNot Nothing Then
                    For Each cd As ControlData In e.NewItems
                        ClipboardItemAdded(cd)
                    Next
                End If
        End Select
    End Sub

    Private Sub DisplayControl_Enter(sender As Object, e As EventArgs)
        If Control.MouseButtons = MouseButtons.Left OrElse Control.MouseButtons = MouseButtons.None Then
            DisplayControlEnter(DirectCast(sender, DisplayControl), e)
        End If
    End Sub

    Private Sub DisplayControl_MouseDown(sender As Object, e As MouseEventArgs)
        If e.Button = MouseButtons.Left Then
            DisplayControlMouseDown(DirectCast(sender, DisplayControl), e)
        End If
    End Sub

    Private Sub DisplayControl_MouseMove(sender As Object, e As MouseEventArgs)
        DisplayControlMouseMove(DirectCast(sender, DisplayControl), e)
    End Sub
    Private Sub DisplayControl_MouseUp(sender As Object, e As MouseEventArgs)
        If e.Button = MouseButtons.Left Then
            DisplayControlsDragEnd(DirectCast(sender, DisplayControl))
        End If
    End Sub

    Private Sub DisplayControls_BeforeCollectionChanged(sender As Object, e As CancelEventArgs(Of DisplayControl)) Handles mDisplayControls.AddingItem, mDisplayControls.RemovingItem, mDisplayControls.ClearingItems
        If mIsDragging OrElse mIsResizing Then
            e.Cancel = True
        End If
    End Sub

    Private Sub DisplayControls_BeforeItemAdded(sender As Object, e As CancelEventArgs(Of DisplayControl)) Handles mDisplayControls.AddingItem
        ' Don't allow new controls to be added while dragging or resizing to prevent issues with event handlers and collection changes.
        If mDisplayControls.Contains(DirectCast(e.Item, DisplayControl)) Then
            e.Cancel = True
        End If
    End Sub
    Private Sub DisplayControls_CollectionChanged(sender As Object, e As NotifyCollectionChangedEventArgs) Handles mDisplayControls.CollectionChanged
        Select Case e.Action
            Case NotifyCollectionChangedAction.Reset
                Throw New NotImplementedException("Reset action not implemented for DisplayControls collection.")
            Case Else
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
        End Select
    End Sub

    Private Sub DocumentPage_ControlAdded(sender As Object, e As ControlEventArgs)
        Dim dc As DisplayControl = TryCast(e.Control, DisplayControl)
        If dc IsNot Nothing Then
            Me.DisplayControls.Add(dc)
        End If
    End Sub

    Private Sub DocumentPage_ControlRemoved(sender As Object, e As ControlEventArgs)
        Dim dc As DisplayControl = TryCast(e.Control, DisplayControl)
        If dc IsNot Nothing Then
            Me.DisplayControls.Remove(dc)
        End If
    End Sub

    Private Sub DocumentPage_KeyDown(sender As Object, e As KeyEventArgs)

    End Sub

    Private Sub DocumentPage_KeyUp(sender As Object, e As KeyEventArgs)

    End Sub

    Private Sub DocumentPage_MouseDown(sender As Object, e As MouseEventArgs)
        mPasteLocation.Page = DirectCast(sender, DocumentPage)
        mPasteLocation.Location = e.Location
        If e.Button = MouseButtons.Left Then
            DocumentPageMouseDown(DirectCast(sender, DocumentPage))
        ElseIf e.Button = MouseButtons.Right Then
            Me.ContextMenuStrip.Show(DirectCast(sender, DocumentPage), e.Location)
        End If
    End Sub

    Private Sub DocumentPage_MouseUp(sender As Object, e As MouseEventArgs)

    End Sub

    Private Sub DocumentPages_BeginTrans(sender As Object, e As EventArgs) Handles mPages.BeginTrans
        Me.SuspendLayout()
    End Sub

    Private Sub DocumentPages_EndTrans(sender As Object, e As EventArgs) Handles mPages.EndTrans
        Try
            Me.ResumeLayout(False)
            For Each pg As DocumentPage In Me.Pages
                ' This forces the page to finish its internal Zoom logic 
                ' and report its TRUE final width to the parent.
                pg.Update()
            Next
            CenterContent()
            Me.PerformLayout()
            ' Me.Invalidate()   ' Uncomment if weird scrolling or resizing occur.
        Catch ex As Exception
            Me.ResumeLayout(True)
        End Try
    End Sub

    Private Sub DocumentPages_CollectionChanged(sender As Object, e As NotifyCollectionChangedEventArgs) Handles mPages.CollectionChanged
        Repaginate(Me.Pages)
        Select Case e.Action
            Case NotifyCollectionChangedAction.Reset
                Try
                    For i As Integer = Me.Controls.Count - 1 To 0 Step -1
                        If TypeOf Me.Controls(i) Is DocumentPage Then
                            DocumentPageRemoved(DirectCast(Me.Controls(i), DocumentPage))
                        End If
                    Next
                Finally
                End Try
            Case Else
                If e.OldItems IsNot Nothing Then
                    For Each pg As DocumentPage In e.OldItems
                        DocumentPageRemoved(pg)
                    Next
                End If
                If e.NewItems IsNot Nothing Then
                    For Each pg As DocumentPage In e.NewItems
                        DocumentPageAdded(pg)
                    Next
                End If
        End Select
    End Sub

    Private Sub SelectedControls_AddingItem(sender As Object, e As CancelEventArgs(Of DisplayControl)) Handles mSelectedControls.AddingItem
        If mSelectedControls.Contains(e.Item) Then e.Cancel = True
    End Sub

    Private Sub SelectedControls_BeforeChange(sender As Object, e As CancelEventArgs(Of DisplayControl)) Handles mSelectedControls.AddingItem, mSelectedControls.RemovingItem, mSelectedControls.ClearingItems
        ' Don't allow selection changes while dragging or resizing to prevent issues with event handlers and collection changes.
        If mIsDragging OrElse mIsResizing Then
            e.Cancel = True
        End If
    End Sub

    Private Sub SelectedControls_CollectionChanged(sender As Object, e As NotifyCollectionChangedEventArgs) Handles mSelectedControls.CollectionChanged
        Select Case e.Action
            Case NotifyCollectionChangedAction.Reset
                Try
                    ' Unselect all DisplayControls, reselect all SelectedControls and set focus to Me.
                    For Each dc As DisplayControl In Me.DisplayControls
                        SelectedControlRemoved(dc)
                    Next
                    Me.Select()
                Finally
                End Try
            Case Else
                If e.OldItems IsNot Nothing Then
                    For Each dc As DisplayControl In e.OldItems
                        SelectedControlRemoved(dc)
                    Next
                End If
                If e.NewItems IsNot Nothing Then
                    For Each dc As DisplayControl In e.NewItems
                        SelectedControlAdded(dc)
                    Next
                End If
        End Select
    End Sub

    Private Sub UndoStack_BeforeCollectionChange(sender As Object, e As CancelEventArgs(Of List(Of PageData))) Handles mUndoStack.CollectionBeforeChange
        If mNoUndo Then e.Cancel = True
    End Sub

    Private Sub UndoStack_CollectionChanged(sender As Object, e As StackChangedEventArgs(Of List(Of PageData))) Handles mUndoStack.CollectionChanged
        Select Case e.Action
            Case StackAction.Push
                UndoStackItemsAdded(DirectCast(e.Item, List(Of PageData)))
            Case StackAction.Pop
                UndoStackItemsRemoved(DirectCast(e.Item, List(Of PageData)))
            Case StackAction.Clear
        End Select
    End Sub
#End Region
#Region "DisplayControl Context Menus"
    Private Sub DisplayControlMenu_Opening(sender As Object, e As EventArgs)
        Dim menu = DirectCast(sender, ContextMenuStrip)
        Dim bringToFrontItem = menu.Items("BringToFrontToolStripMenuItem")
        Dim sendToBackItem = menu.Items("SendToBackToolStripMenuItem")
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

    Private Sub DisplayControlBringToFront_Click(sender As Object, e As EventArgs)
        ControlsBringToFront(Me.SelectedControls.ToList())
    End Sub

    Private Sub DisplayControlCut_Click(sender As Object, e As EventArgs)
        ControlsCut(Me.SelectedControls.ToList())
    End Sub

    Private Sub DisplayControlDelete_Click(sender As Object, e As EventArgs)
        ControlsDelete(Me.SelectedControls.ToList())
    End Sub

    Private Sub DisplayControlPaste_Click(sender As Object, e As EventArgs)
        ControlsPaste(Me.ClipBoard)
    End Sub

    Private Sub DisplayControlSendToBack_Click(sender As Object, e As EventArgs)
        ControlsSendToBack(Me.SelectedControls.ToList())
    End Sub

    Private Sub DisplayControlSelectAll_Click(sender As Object, e As EventArgs)
        ControlsSelect(Me.DisplayControls.ToList())
    End Sub

    Private Sub DisplayControlThemeEditor_Click(sender As Object, e As EventArgs)
        ChartDesignerOpen()
    End Sub

    Private Sub DisplayControlUndo_Click(sender As Object, e As EventArgs)
        ControlsUndo(Me.UndoStack.Pop())
    End Sub
#End Region
#Region "DocumentPage Context Menus"
    Private Sub DocumentPageControlsMenu_Opening(sender As Object, e As EventArgs)
        Dim menu = DirectCast(sender, ToolStripDropDown)
        Dim pageCutItem = menu.Items("PageCutToolStripMenuItem")
        Dim pageDeleteItem = menu.Items("PageDeleteToolStripMenuItem")
        Dim pagePasteItem = menu.Items("PagePasteToolStripMenuItem")
        Dim pageSelectAllItem = menu.Items("PageSelectAllToolStripMenuItem")
        Dim pageUndoItem = menu.Items("PageUndoToolStripMenuItem")

        If pageCutItem IsNot Nothing Then
            pageCutItem.Enabled = Me.SelectedControls.Count > 0
        End If
        If pageDeleteItem IsNot Nothing Then
            pageDeleteItem.Enabled = Me.SelectedControls.Count > 0
        End If
        If pagePasteItem IsNot Nothing Then
            pagePasteItem.Enabled = Me.ClipBoard.Count > 0
        End If
        If pageSelectAllItem IsNot Nothing Then
            pageSelectAllItem.Enabled = Me.DisplayControls.Count > 0
        End If
        If pageUndoItem IsNot Nothing Then
            pageUndoItem.Enabled = Me.UndoStack.Count > 0
        End If

    End Sub

    Private Sub DocumentPageMenu_Opening(sender As Object, e As EventArgs)
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
        Dim pageControlItem = menu.Items("PageControlToolStripMenuItem")

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

    Protected Overridable Sub DocumentPageMenuAddNew_Click(sender As Object, e As EventArgs)
        DocumentPageAddNew()
    End Sub

    Private Sub DocumentPageMenuDelete_Click(sender As Object, e As EventArgs)
        DocumentPageDelete(ClickedPage(sender))
    End Sub

    Private Sub DocumentPageMenuEdit_DropDownOpening(sender As Object, e As EventArgs)

    End Sub

    Private Sub DocumentPageMenuMoveDown_Click(sender As Object, e As EventArgs)
        DocumentPageMoveDown(ClickedPage(sender))
    End Sub

    Private Sub DocumentPageMenuMoveFirst_Click(sender As Object, e As EventArgs)
        DocumentPageMoveFirst(ClickedPage(sender))
    End Sub

    Private Sub DocumentPageMenuMoveLast_Click(sender As Object, e As EventArgs)
        DocumentPageMoveLast(ClickedPage(sender))
    End Sub

    Private Sub DocumentPageMenuMoveUp_Click(sender As Object, e As EventArgs)
        DocumentPageMoveUp(ClickedPage(sender))
    End Sub

    Private Sub DocumentPageMenuPageCut_Click(sender As Object, e As EventArgs)
        ControlsCut(Me.SelectedControls.ToList())
    End Sub

    Private Sub DocumentPageMenuPageDelete_Click(sender As Object, e As EventArgs)
        ControlsDelete(Me.SelectedControls.ToList())
    End Sub

    Private Sub DocumentPageMenuPageSelectAll_Click(sender As Object, e As EventArgs)
        ControlsSelect(Me.DisplayControls.ToList())
    End Sub

    Private Sub DocumentPageMenuPageUndo_Click(sender As Object, e As EventArgs)
        ControlsUndo(Me.UndoStack.Pop())
    End Sub

    Private Sub DocumentPageMenuPagePaste_Click(sender As Object, e As EventArgs)
        ControlsPaste(Me.ClipBoard)
    End Sub

    Private Sub DocumentPageMenuScrollFirst_Click(sender As Object, e As EventArgs)
        DocumentPageScrollFirst()
    End Sub

    Private Sub DocumentPageMenuScrollLast_Click(sender As Object, e As EventArgs)
        DocumentPageScrollLast()
    End Sub

    Private Sub DocumentPageMenuScrollNext_Click(sender As Object, e As EventArgs)
        DocumentPageScrollNext()
    End Sub

    Private Sub DocumentPageMenuScrollPrevious_Click(sender As Object, e As EventArgs)
        DocumentPageScrollPrevious()
    End Sub

    Private Sub DocumentPageMenuScrollTo_Click(sender As Object, e As EventArgs)
        DocumentPageScrollTo(Integer.Parse(DirectCast(sender, ToolStripMenuItem).Text) - 1)
    End Sub
#End Region
#End Region
End Class
