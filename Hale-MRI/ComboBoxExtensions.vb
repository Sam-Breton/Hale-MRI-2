Imports System.Runtime.CompilerServices

Public Module ComboBoxExtensions
    <Extension()>
    Public Function DoubleClicked(combo As System.Windows.Forms.ComboBox) As Boolean
        ' Returns True if the specified ComboBox was double-clicked, else
        ' returns False. We need this because VB.NET/WinForms doesn't support
        ' the ComboBox double-click event.
        Const kDblClickTime As Integer = 500 ' Maximum time between clicks for a double-click, in milliseconds
        Static lastControl As System.Windows.Forms.ComboBox = Nothing
        Static lastClick As DateTime = DateTime.MinValue
        Dim result As Boolean = False
        If lastControl Is Nothing OrElse lastControl Is combo Then
            If (DateTime.Now - lastClick).TotalMilliseconds <= kDblClickTime Then
                ' If the time since the last click is within the double-click threshold, return true.
                result = True
                lastClick = DateTime.MinValue ' Reset lastClick to prevent further double-clicks
            Else
                lastClick = DateTime.Now
            End If
        End If
        lastControl = combo
        Return result
    End Function

    <Extension()>
    Public Function NotInList(combo As System.Windows.Forms.ComboBox, e As KeyEventArgs) As Boolean
        ' Returns True if the user pressed Enter or Return, the combo text is not empty,
        ' and no existing item is selected in the combo box.
        Return ((e.KeyCode = Keys.Enter OrElse e.KeyCode = Keys.Return) AndAlso Not String.IsNullOrEmpty(combo.Text) AndAlso combo.SelectedIndex = -1)
    End Function
End Module
