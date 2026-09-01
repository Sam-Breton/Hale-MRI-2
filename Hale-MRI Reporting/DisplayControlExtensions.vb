Imports System.Reflection
Imports System.Runtime.CompilerServices

Module DisplayControlExtensions
    '''' <summary>
    '''' Creates a "deep-copy" of a DisplayControl using Reflection.
    '''' </summary>
    '''' <param name="srcControl"></param>
    '''' <returns>Control</returns>
    '<Extension()>
    'Public Function Clone(ByVal srcControl As Control) As Control
    '    Dim newControl As Control = DirectCast(Activator.CreateInstance(srcControl.GetType()), Control)
    '    Dim properties = srcControl.GetType().GetProperties(BindingFlags.Public Or BindingFlags.Instance)

    '    srcControl.SuspendLayout()

    '    ' 1. Copy standard properties via Reflection.
    '    For Each prop As PropertyInfo In properties
    '        If prop.CanWrite AndAlso prop.CanRead AndAlso
    '            prop.Name <> "Parent" AndAlso prop.Name <> "WindowTarget" AndAlso
    '            prop.Name <> "Capture" AndAlso prop.Name <> "Controls" AndAlso
    '            prop.Name <> "LayoutSettings" Then
    '            Try
    '                prop.SetValue(newControl, prop.GetValue(srcControl, Nothing), Nothing)
    '            Catch ex As Exception

    '            End Try
    '        End If
    '    Next

    '    ' 2. ONE-TIME SETUP for Container-specific structures.
    '    If TypeOf srcControl Is TableLayoutPanel Then
    '        Dim tlpSrc = DirectCast(srcControl, TableLayoutPanel)
    '        Dim tlpDest = DirectCast(newControl, TableLayoutPanel)

    '        tlpDest.ColumnCount = tlpSrc.ColumnCount
    '        tlpDest.RowCount = tlpSrc.RowCount

    '        tlpDest.ColumnStyles.Clear()
    '        For Each style As ColumnStyle In tlpSrc.ColumnStyles
    '            tlpDest.ColumnStyles.Add(New ColumnStyle(style.SizeType, style.Width))
    '        Next

    '        tlpDest.RowStyles.Clear()
    '        For Each style As RowStyle In tlpSrc.RowStyles
    '            tlpDest.RowStyles.Add(New RowStyle(style.SizeType, style.Height))
    '        Next
    '    End If

    '    ' 3. RECURSION: Clone and Add children.
    '    For Each child As Control In srcControl.Controls
    '        Dim clonedChild As Control = Clone(child)

    '        If TypeOf srcControl Is TableLayoutPanel Then
    '            Dim tlpSrc = DirectCast(srcControl, TableLayoutPanel)
    '            Dim tlpDest = DirectCast(newControl, TableLayoutPanel)

    '            Dim col = tlpSrc.GetColumn(child)
    '            Dim row = tlpSrc.GetRow(child)
    '            tlpDest.Controls.Add(clonedChild, col, row)

    '            tlpDest.SetColumnSpan(clonedChild, tlpSrc.GetColumnSpan(child))
    '            tlpDest.SetRowSpan(clonedChild, tlpSrc.GetRowSpan(child))
    '        ElseIf TypeOf srcControl Is FlowLayoutPanel Then
    '            Dim flpSrc = DirectCast(srcControl, FlowLayoutPanel)
    '            Dim flpDest = DirectCast(newControl, FlowLayoutPanel)
    '            flpDest.Controls.Add(clonedChild)
    '            flpDest.SetFlowBreak(clonedChild, flpSrc.GetFlowBreak(child))
    '        Else
    '            newControl.Controls.Add(clonedChild)
    '        End If
    '    Next

    '    srcControl.ResumeLayout(True)
    '    If TypeOf newControl Is TableLayoutPanel Then DirectCast(newControl, TableLayoutPanel).PerformLayout()

    '    Return newControl
    'End Function

    '''' <summary>
    '''' Creates a "deep-copy" of a DisplayControl using Reflection.
    '''' </summary>
    '''' <param name="srcControl"></param>
    '''' <returns>Control</returns>
    '<Extension()>
    'Public Function Clone(ByVal srcMenu As ContextMenuStrip) As ContextMenuStrip
    '    Return CloneContextMenu(srcMenu)
    'End Function
    'Private Function CloneContextMenu(ByVal srcMenu As ContextMenuStrip) As ContextMenuStrip
    '    If srcMenu Is Nothing Then Return Nothing

    '    Dim newMenu As New ContextMenuStrip()
    '    ' Copy basic properties
    '    newMenu.Name = srcMenu.Name

    '    ' Copy Items
    '    For Each item As ToolStripItem In srcMenu.Items
    '        newMenu.Items.Add(CloneToolStripItem(item))
    '    Next

    '    Return newMenu
    'End Function
    'Private Function CloneToolStripItem(ByVal srcItem As ToolStripItem) As ToolStripItem
    '    ' This handles separators
    '    If TypeOf srcItem Is ToolStripSeparator Then
    '        Return New ToolStripSeparator() With {.Name = srcItem.Name}
    '    End If

    '    ' Create instance of the same type (ToolStripMenuItem, etc.)
    '    Dim newItem As ToolStripItem = DirectCast(Activator.CreateInstance(srcItem.GetType()), ToolStripItem)

    '    ' Copy basic visual properties
    '    newItem.Text = srcItem.Text
    '    newItem.Image = srcItem.Image
    '    newItem.Enabled = srcItem.Enabled
    '    newItem.Tag = srcItem.Tag

    '    ' If it has sub-items (recursion for menus)
    '    If TypeOf srcItem Is ToolStripMenuItem Then
    '        Dim srcMenuItem = DirectCast(srcItem, ToolStripMenuItem)
    '        Dim newMenuItem = DirectCast(newItem, ToolStripMenuItem)
    '        For Each subItem As ToolStripItem In srcMenuItem.DropDownItems
    '            newMenuItem.DropDownItems.Add(CloneToolStripItem(subItem))
    '        Next
    '    End If

    '    ' NOTE: Event handlers (Click events) are NOT copied automatically here.
    '    ' You would need to manually re-attach them if needed.

    '    Return newItem
    'End Function

    ''' <summary>
    ''' Checks whether a ControlCollection contains the given targetControl's type.
    ''' </summary>
    ''' <param name="controls"></param>
    ''' <param name="targetControl"></param>
    ''' <returns>Boolean</returns>
    '<Extension()>
    'Public Function HasControlType(ByVal controls As Control.ControlCollection, ByVal targetControl As Control) As Boolean
    '    For Each ctrl As Control In controls
    '        If ctrl.GetType() Is targetControl.GetType() Then
    '            Return True
    '        End If
    '    Next
    '    Return False
    'End Function
End Module
