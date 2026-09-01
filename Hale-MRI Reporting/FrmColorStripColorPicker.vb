
Imports LibGlobals

''' <summary>
''' Dialog for picking Chart.CustomColorPalettes.
''' </summary>
Public Class FrmColorStripColorPicker
    Private mColorPanels As List(Of ColorPanel) = Nothing       ' List of custom color panels for displaying and selecting the current color palette.
    Private mInitialColors As Color() = Array.Empty(Of Color)   ' Variable to prevent race conditions between public Colors property and OnLoad() event.
    Private mSelectedPanel As ColorPanel = Nothing              ' The currently selected custom color panel, if any.

    ''' <summary>
    ''' Gets/sets the current color palette.
    ''' </summary>
    ''' <returns>Array(Of Color)</returns>
    Public Property Colors As Color()
        Get
            Return ColorsGet()
        End Get
        Set(value As Color())
            ColorsSet(value)
        End Set
    End Property

    ''' <summary>
    ''' Gets/sets the currently selected ColorPanel.
    ''' </summary>
    ''' <returns>ColorPanel</returns>
    Private Property SelectedPanel As ColorPanel
        Get
            Return mSelectedPanel
        End Get
        Set(value As ColorPanel)
            ColorSelect(value)
            mSelectedPanel = value
        End Set
    End Property

    Private Sub ColorAdd(ByVal panel As ColorPanel)
        ' Adds the given ColorPanel's Color to the current color palette.
        ' The "current" color palette is the collection of ColorPanels
        ' who's current Color is not Color.Empty.
        panel.Color = ComboColor.Color
        CmdColorTrash.Enabled = SelectedPanel.Color <> Color.Empty
        CmdOK.Enabled = True
    End Sub

    Private Sub ColorRemove(ByVal panel As ColorPanel)
        ' Removes the given color from the current color palette.
        If panel IsNot Nothing Then
            Dim index As Integer = mColorPanels.IndexOf(panel)

            For i As Integer = index To mColorPanels.Count - 2
                mColorPanels(i).BackColor = mColorPanels(i + 1).BackColor
            Next

            mColorPanels(mColorPanels.Count - 1).Color = Color.Empty
            SelectedPanel = mColorPanels(index)
        End If
    End Sub

    Private Sub ColorSelect(ByVal panel As ColorPanel)
        ' Unselects the current SelectedPanel, if any, and selects the given ColorPanel.
        If mSelectedPanel IsNot Nothing Then Me.mSelectedPanel.Selected = False
        panel.Selected = True
        CmdColorTrash.Enabled = panel.Color <> Color.Empty  ' Trash button enabled if SelectedPanel's Color is not Color.Empty.
    End Sub

    Private Function ColorsGet() As Color()
        ' Returns an array of Colors sequentially holding each ColorPanel's Color that is not Color.Empty.
        Dim myColors As New List(Of Color)

        For Each colorPanel As ColorPanel In mColorPanels
            If colorPanel.Color <> Color.Empty Then myColors.Add(colorPanel.BackColor)
        Next

        Return myColors.ToArray()
    End Function

    Private Sub ColorsSet(ByVal myColors As Color())
        ' Sequentially sets the Color property of each ColorPanel from the given array.
        Dim i As Integer = 0

        If mColorPanels IsNot Nothing Then

            ' Set each ColorPanel's Color from the given array sequentially, starting
            ' with the top-left and going to the bottom-right.
            For Each color As Color In myColors
                If i < mColorPanels.Count Then
                    mColorPanels(i).Color = color
                    i += 1
                Else
                    Exit For ' Stop after the last panel in the ColorPanel list.
                End If
            Next

            ' Clear any existing Colors from the remainder of the ColorPanel list.
            While i < mColorPanels.Count
                mColorPanels(i).Color = Color.Empty
                i += 1
            End While
        End If

        ' We need to save the initial Colors property so we can add them to the current color palette.
        ' in the OnLoad() event handler.
        If mInitialColors.Length = 0 Then mInitialColors = myColors
    End Sub

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)
        mColorPanels = New List(Of ColorPanel) From {ColorPanel1, ColorPanel2, ColorPanel3, ColorPanel4, ColorPanel5, ColorPanel6, ColorPanel7, ColorPanel8, ColorPanel9, ColorPanel10}
        ComboColor.Colors = ComboColorPicker.ColorList.All
        ComboColor.InsertColor(Color.Transparent)
        For Each colorPanel As ColorPanel In mColorPanels
            colorPanel.Color = Color.Empty
        Next
        ColorsSet(mInitialColors)   ' Loads the colors given to the Color property before the form was initialized.
    End Sub

    Private Sub CmdColorTrash_Click(sender As Object, e As EventArgs) Handles CmdColorTrash.Click
        Try
            ColorRemove(Me.SelectedPanel)
        Catch ex As Exception
#If DEBUG Then
            Debug.WriteLine($"{sender}: {ex.Message}")
#Else
            FileLogger.LogException(ex)
#End If
        End Try
    End Sub

    Private Sub ColorPanel_Enter(sender As Object, e As EventArgs) Handles ColorPanel1.Enter, ColorPanel2.Enter, ColorPanel3.Enter, ColorPanel4.Enter, ColorPanel5.Enter, ColorPanel6.Enter, ColorPanel7.Enter, ColorPanel8.Enter, ColorPanel9.Enter, ColorPanel10.Enter
        Try
            SelectedPanel = DirectCast(sender, ColorPanel)
        Catch ex As Exception
#If DEBUG Then
            Debug.WriteLine($"{sender}: {ex.Message}")
#Else
            FileLogger.LogException(ex)
#End If
        End Try
    End Sub

    Private Sub ComboColor_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboColor.SelectionChangeCommitted
        Try
            If Me.SelectedPanel IsNot Nothing Then
                ColorAdd(SelectedPanel)
            End If
        Catch ex As Exception
#If DEBUG Then
            Debug.WriteLine($"{sender}: {ex.Message}")
#Else
            FileLogger.LogException(ex)
#End If
        End Try
    End Sub
End Class