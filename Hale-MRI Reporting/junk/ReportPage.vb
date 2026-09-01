Imports System.Collections.ObjectModel
Imports System.Collections.Specialized
Imports System.DirectoryServices.ActiveDirectory
Imports System.Reflection

Public Class ReportPage
    Inherits DocumentPage

    Private Const kLetterheadHeaderSpacing As Integer = 10      ' Vertical spacing between the ReportLetterhead, ReportHeader and user content.

    'Private mScale As New SizeF(1.0F, 1.0F)                     ' Current scaling factor.

    ''' <summary>
    ''' ToolStripMenuItem in the given ContextMenuStrip by Name.
    ''' </summary>
    ''' <param name="menu"></param>
    ''' <param name="item"></param>
    ''' <returns>ToolStripMenuItem</returns>
    Public ReadOnly Property ContextMenuItem(menu As ContextMenuStrip, item As String) As ToolStripMenuItem
        Get
            Return DirectCast(menu.Items(item), ToolStripMenuItem)
        End Get
    End Property

    ''' <summary>
    ''' The ReportHeader Control.
    ''' </summary>
    ''' <returns>ReportHeader</returns>
    Public ReadOnly Property Header As ReportHeader
        Get
            Return Me.ReportHeaderControl
        End Get
    End Property

    ''' <summary>
    ''' The ReportLetterhead Control.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Letterhead As ReportLetterhead1
        Get
            Return Me.ReportLetterheadControl
        End Get
    End Property

    Private Sub HeaderBorderStyleSet(style As ToolStripMenuItem)
        ToolStripMenuItemUncheck(style)
        Select Case style.Name
            Case "BorderStyleNoneMenuItem"
                Me.Header.BorderStyle = BorderStyle.None
            Case "BorderStyleFixedSingleMenuItem"
                Me.Header.BorderStyle = BorderStyle.FixedSingle
            Case "BorderStyleFixed3DMenuItem"
                Me.Header.BorderStyle = BorderStyle.Fixed3D
            Case Else
                Return
        End Select
    End Sub

    'Private Sub HeaderVisibleSet()
    'Me.Header.Visible = HeaderVisibleMenuItem.Checked
    'HeaderBorderStyleMenuItem.Enabled = Me.Header.Visible
    'HeaderItemsMenuItem.Enabled = Me.Header.Visible
    'PageLayoutSet(Me.Document)
    'End Sub

    Protected Overrides Sub LayoutSet()
        If Me.Zoom <> SizeF.Empty AndAlso Not Me.Zoom.IsInfinity() Then
            ' Apply zoom to our additional components.
            Me.Letterhead.ApplyZoom(Me.Zoom)
            Me.Header.ApplyZoom(Me.Zoom)
        End If
        MyBase.LayoutSet()  ' The base class handles the DisplayControls.
    End Sub

    Private Sub LetterheadOpenImageFile()
        With OpenFileDialog1
            .Title = "Select Image File"
            .InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
            .Filter = "Image Files (*.jpg;*.jpeg;*.png;*.gif;*.bmp)|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All files (*.*)|*.*"
            .FilterIndex = 1
            .Multiselect = False ' Set to True to allow multiple selections
        End With

        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            ' Get the selected file path
            Dim selectedFile As String = OpenFileDialog1.FileName
            ' Load the image into a PictureBox (e.g., PictureBox1)
            Me.Letterhead.PictureBox.Image = Image.FromFile(selectedFile)
        End If
    End Sub

    Private Sub LetterheadSizeModeSet(mode As ToolStripMenuItem)
        ToolStripMenuItemUncheck(mode)
        Select Case mode.Name
            Case "SizeModeAutoSizeMenuItem"
                Me.Letterhead.PictureBox.SizeMode = PictureBoxSizeMode.AutoSize
            Case "SizeModeCenterMenuItem"
                Me.Letterhead.PictureBox.SizeMode = PictureBoxSizeMode.CenterImage
            Case "SizeModeNormalMenuItem"
                Me.Letterhead.PictureBox.SizeMode = PictureBoxSizeMode.Normal
            Case "SizeModeStretchMenuItem"
                Me.Letterhead.PictureBox.SizeMode = PictureBoxSizeMode.StretchImage
            Case "SizeModeZoomMenuItem"
                Me.Letterhead.PictureBox.SizeMode = PictureBoxSizeMode.Zoom
            Case Else
                Return
        End Select
    End Sub

    Private Sub ToolStripMenuItemUncheck(mode As ToolStripMenuItem)
        'LetterheadSizeModeMenuItem
        For Each item As ToolStripMenuItem In mode.Owner.Items
            If item IsNot mode Then
                item.Checked = False
            End If
        Next
    End Sub

    'Private Sub LetterheadVisibleSet()
    'Letterhead.Visible = LetterheadVisibleMenuItem.Checked
    'LetterheadImageMenuItem.Enabled = Letterhead.Visible
    'LetterheadSizeModeMenuItem.Enabled = Letterhead.Visible
    'PageLayoutSet(Me.Document)
    'End Sub

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)
        'Me.Header.OriginalSize = Me.Header.Size
        'Me.Letterhead.OriginalSize = Me.Letterhead.Size
        For Each ctrl As Control In Me.Controls
            Debug.WriteLine($"{ctrl.Name}")
        Next
        If Not Me.DesignMode Then
            Me.Header.Visible = False
            Me.Letterhead.Visible = False
        End If
    End Sub

    Private Sub PageLayoutSet(ByVal sender As Control, ByVal doc As DocumentSettings)
        If doc IsNot Nothing Then
            ' Initialize the unscaled header location based on the current Letterhead visibility.
            Dim headerTop As Integer = If(Me.Letterhead.Visible,
                Me.Letterhead.BaseLocation.Y + Me.Letterhead.OriginalSize.Height + Me.Letterhead.VerticalSeparation,
                doc.MarginTop
            )
            Dim oldBaseLocation As Point = Me.Header.BaseLocation
            Me.Header.BaseLocation = New Point(
                Me.Letterhead.BaseLocation.X,
                headerTop * doc.Scale.Height
            )
            If Me.Header.Visible Then
                'If Me.Header.BaseLocation <> oldBaseLocation Then Me.Header.ApplyZoom(Me.Zoom)
                Me.VerticalLimit = (Me.Header.BaseLocation.Y + Me.Header.OriginalSize.Height + Me.Header.VerticalSeparation) * doc.Scale.Height
            ElseIf Me.Letterhead.Visible Then
                Me.VerticalLimit = (Me.Letterhead.BaseLocation.Y + Me.Letterhead.OriginalSize.Height + Me.Letterhead.VerticalSeparation) * doc.Scale.Height
            Else
                Me.VerticalLimit = 0
            End If
            'If Me.Letterhead.Visible Then Me.Letterhead.ApplyZoom(Me.Zoom)
            LayoutSet()
            Debug.WriteLine($"{Me.Name} VerticalLimit={Me.VerticalLimit}")
        End If
    End Sub

    Protected Overrides Sub PageSizeSet(ByRef doc As DocumentSettings)
        MyBase.PageSizeSet(doc) ' This will set our OriginalSize.
        If doc IsNot Nothing AndAlso Me.OriginalSize <> Size.Empty Then
            Dim gap As Integer = 20 ' Unscaled gap between components - This should be a property of header and letterhead.
            Dim vl As Integer = 0

            ' Initialize the unscaled Letterhead bounds.
            Me.Letterhead.BaseLocation = New Point(
                doc.MarginLeft * doc.Scale.Width,
                doc.MarginTop * doc.Scale.Height
            )
            Me.Letterhead.OriginalSize = New Size(
                Me.OriginalSize.Width - (doc.MarginLeft + doc.MarginRight) * doc.Scale.Width,
                Me.Letterhead.Height * doc.Scale.Height
            )

            ' Initialize the unscaled Header bounds.
            Me.Header.OriginalSize = New Size(
                Me.Letterhead.OriginalSize.Width,
                Me.Header.Height * doc.Scale.Height
            )
            Dim headerTop As Integer = Me.Letterhead.BaseLocation.Y + Me.Letterhead.OriginalSize.Height + Me.Letterhead.VerticalSeparation
            Me.Header.BaseLocation = New Point(
                Me.Letterhead.BaseLocation.X,
                headerTop * doc.Scale.Height
            )

            'Me.VerticalLimit = (Me.Header.BaseLocation.Y + Me.Header.OriginalSize.Height + Me.Header.VerticalSeparation) * doc.Scale.Height
        End If
    End Sub

    Private Sub HeaderBorderStyleMenuItem_Click(sender As Object, e As EventArgs) Handles BorderStyleNoneMenuItem.Click, BorderStyleFixedSingleMenuItem.Click, BorderStyleFixed3DMenuItem.Click
        'Static insub As Boolean = False
        'If insub Then Return
        'insub = True
        HeaderBorderStyleSet(DirectCast(sender, ToolStripMenuItem))
        'insub = False
    End Sub

    'Private Sub HeaderVisibleMenuItem_CheckedChanged(sender As Object, e As EventArgs)
    '    Static insub = False
    '    If insub Then Return
    '    insub = True
    '    HeaderVisibleSet
    '    insub = False
    'End Sub

    Private Sub LetterheadImageMenuItem_Click(sender As Object, e As EventArgs) Handles LetterheadImageMenuItem.Click
        LetterheadOpenImageFile()
    End Sub

    'Private Sub LetterheadVisibleMenuItem_CheckedChanged(sender As Object, e As EventArgs)
    '    Static insub = False
    '    If insub Then Return
    '    insub = True
    '    LetterheadVisibleSet
    '    insub = False
    'End Sub

    Private Sub LetterSizeModeMenuItem_CheckedChanged(sender As Object, e As EventArgs) Handles SizeModeNormalMenuItem.CheckedChanged, SizeModeStretchMenuItem.CheckedChanged, SizeModeAutoSizeMenuItem.CheckedChanged, SizeModeCenterMenuItem.CheckedChanged, SizeModeZoomMenuItem.CheckedChanged
        'Static insub As Boolean = False
        'If insub Then Return
        'insub = True
        LetterheadSizeModeSet(DirectCast(sender, ToolStripMenuItem))
        'insub = False
    End Sub

    Private Sub ReportHeaderControl_VisibleChanged(sender As Object, e As EventArgs) Handles ReportHeaderControl.VisibleChanged
        PageLayoutSet(DirectCast(sender, Control), Me.Document)
    End Sub

    Private Sub ReportLetterheadControl_VisibleChanged(sender As Object, e As EventArgs) Handles ReportLetterheadControl.VisibleChanged
        PageLayoutSet(DirectCast(sender, Control), Me.Document)
    End Sub
    'Private Sub PageLayoutPanel_MouseDown(sender As Object, e As MouseEventArgs)
    '    OnMouseDown(New MouseEventArgs(e.Button, e.Clicks, PointToClient(Cursor.Position).X, PointToClient(Cursor.Position).Y, e.Delta))
    'End Sub

    'Private Sub PageLayoutPanel_MouseUp(sender As Object, e As MouseEventArgs)
    '    OnMouseUp(New MouseEventArgs(e.Button, e.Clicks, PointToClient(Cursor.Position).X, PointToClient(Cursor.Position).Y, e.Delta))
    'End Sub

    'Private Sub ContentPanel_MouseDown(sender As Object, e As MouseEventArgs)
    '    OnMouseDown(New MouseEventArgs(e.Button, e.Clicks, PointToClient(Cursor.Position).X, PointToClient(Cursor.Position).Y, e.Delta))
    'End Sub

    'Private Sub ContentPanel_MouseUp(sender As Object, e As MouseEventArgs)
    '    OnMouseUp(New MouseEventArgs(e.Button, e.Clicks, PointToClient(Cursor.Position).X, PointToClient(Cursor.Position).Y, e.Delta))
    'End Sub

    'Private Sub ContentPanel_ControlAdded(sender As Object, e As ControlEventArgs)
    '    If TypeOf e.Control Is DisplayControl Then
    '        OnControlAdded(New ControlEventArgs(e.Control))
    '    End If
    'End Sub

    'Private Sub ContentPanel_ControlRemoved(sender As Object, e As ControlEventArgs)
    '    If TypeOf e.Control Is DisplayControl Then
    '        OnControlRemoved(New ControlEventArgs(e.Control))
    '    End If
    'End Sub
End Class
