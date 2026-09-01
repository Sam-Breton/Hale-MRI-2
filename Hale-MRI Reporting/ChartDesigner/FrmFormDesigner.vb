Imports System.Drawing.Drawing2D

Public Class FrmFormDesigner
    Private mEventsEnabled As Boolean = False
    Private mInitialTheme As Themes = Nothing
    Private mThemeManager As ThemeManager = Nothing
    Private mUserInput As Boolean = False

    Public Property FormThemeManager As ThemeManager
        Get
            Return mThemeManager
        End Get
        Set(value As ThemeManager)
            If value?.Theme IsNot Nothing Then mInitialTheme = value.Theme.Clone()
            mThemeManager = value
        End Set
    End Property

    Protected Overrides Sub OnLoad(e As EventArgs)
        ComboDataEntryFieldBackColor.Colors = ComboColorPicker.ColorList.System
        ComboDataEntryLabelBackColor.Colors = ComboColorPicker.ColorList.System
        ComboFormBackColor.Colors = ComboColorPicker.ColorList.System
        ComboFormForeColor.Colors = ComboColorPicker.ColorList.System
        ComboGroupBordersColor.Colors = ComboColorPicker.ColorList.System
        ComboGroupBordersColor.InsertColors(New List(Of Color) From {Color.Transparent})  ' Border color needs transparent so border can be invisible.
        ComboGroupHeadingsActiveColor.Colors = ComboColorPicker.ColorList.System
        ComboGroupHeadingsActiveColor.InsertColors(New List(Of Color) From {Color.Transparent})   ' Heading active color needs transparent so heading can be invisible.
        ComboGroupHeadingsInactiveColor.Colors = ComboColorPicker.ColorList.System
        ComboGroupHeadingsInactiveColor.InsertColors(New List(Of Color) From {Color.Transparent}) ' Heading inactive color needs transparent so heading can be invisible.
        ShowThemeSettings(Me.FormThemeManager)
        mEventsEnabled = True
    End Sub

    Private Sub ShowThemeSettings(ByVal themeManager As ThemeManager)
        'mEventsEnabled = False
        ChkDataEntryFieldBorder.Checked = themeManager.Theme.DisplayFieldBorder
        ChkDataEntryLabelBorder.Checked = themeManager.Theme.DisplayLabelBorder
        ChkGroupHeadingsVisible.Checked = themeManager.Theme.HeadingVisible
        ComboDataEntryFieldBackColor.SelectColor(themeManager.Theme.DisplayFieldBackColor)
        ComboDataEntryLabelBackColor.SelectColor(themeManager.Theme.DisplayLabelBackColor)
        ComboFormBackColor.SelectColor(themeManager.Theme.FormBackColor)
        ComboFormBorderStyle.BorderStyle = themeManager.Theme.FormBorderStyle
        ComboFormForeColor.SelectColor(themeManager.Theme.FormFontColor)
        ComboGroupBordersColor.SelectColor(themeManager.Theme.GroupingBorderColor)
        ComboGroupBordersDashStylePicker.DashStyle = themeManager.Theme.GroupingBorderDashStyle
        ComboGroupHeadingsActiveColor.SelectColor(themeManager.Theme.HeadingActiveColor)
        ComboGroupHeadingsInactiveColor.SelectColor(themeManager.Theme.HeadingInactiveColor)
        NumericGroupBordersWidth.Value = themeManager.Theme.GroupingBorderWidth
        TxtFormText.Text = themeManager.Theme.FormText
        UCGroupBordersDashPatternPicker.DashPattern = themeManager.Theme.GroupingBorderDashPattern
        'mEventsEnabled = True
    End Sub

    Private Sub CheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles ChkDataEntryFieldBorder.CheckedChanged, ChkDataEntryLabelBorder.CheckedChanged, ChkGroupHeadingsVisible.CheckedChanged
        If Not mEventsEnabled Then Return
        Try
            Dim chk As CheckBox = DirectCast(sender, CheckBox)

            Select Case chk.Name
                Case "ChkDataEntryFieldBorder"
                    If mEventsEnabled Then Me.FormThemeManager.Theme.DisplayFieldBorder = CType(chk.Checked, Boolean)
                Case "ChkDataEntryLabelBorder"
                    If mEventsEnabled Then Me.FormThemeManager.Theme.DisplayLabelBorder = CType(chk.Checked, Boolean)
                Case "ChkGroupHeadingsVisible"
                    CmdGroupHeadingsFont.Enabled = chk.Checked
                    ComboGroupHeadingsActiveColor.Enabled = chk.Checked
                    ComboGroupHeadingsInactiveColor.Enabled = chk.Checked
                    If mEventsEnabled Then Me.FormThemeManager.Theme.HeadingVisible = CType(DirectCast(sender, CheckBox).Checked, Boolean)
                Case Else
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub CmdFont_Click(sender As Object, e As EventArgs) Handles CmdDataEntryFieldFont.Click, CmdDataEntryLabelFont.Click, CmdGroupHeadingsFont.Click, CmdFormFont.Click
        If Not mEventsEnabled Then Return
        Try
            Dim cmd As Button = DirectCast(sender, Button)

            Using dlg As New FontDialog
                dlg.ShowColor = True
                Select Case cmd.Name
                    Case "CmdDataEntryFieldFont"
                        dlg.Font = Me.FormThemeManager.Theme.DisplayFieldFont
                        dlg.Color = Me.FormThemeManager.Theme.DisplayFieldFontColor
                    Case "CmdDataEntryLabelFont"
                        dlg.Font = Me.FormThemeManager.Theme.DisplayLabelFont
                        dlg.Color = Me.FormThemeManager.Theme.DisplayLabelFontColor
                    Case "CmdFormFont"
                        dlg.Font = Me.FormThemeManager.Theme.HeadingFont
                        dlg.Color = Me.FormThemeManager.Theme.HeadingFontColor
                    Case "CmdGroupHeadingsFont"
                        dlg.Font = Me.FormThemeManager.Theme.HeadingFont
                        dlg.Color = Me.FormThemeManager.Theme.HeadingFontColor
                    Case Else
                End Select

                If dlg.ShowDialog() = DialogResult.OK Then
                    Select Case cmd.Name
                        Case "CmdDataEntryFieldFont"
                            Me.FormThemeManager.Theme.DisplayFieldFont = dlg.Font
                            Me.FormThemeManager.Theme.DisplayFieldFontColor = dlg.Color
                        Case "CmdDataEntryLabelFont"
                            Me.FormThemeManager.Theme.DisplayLabelFont = dlg.Font
                            Me.FormThemeManager.Theme.DisplayLabelFontColor = dlg.Color
                        Case "CmdFormFont"
                            Me.FormThemeManager.Theme.FormFont = dlg.Font
                            Me.FormThemeManager.Theme.FormFontColor = dlg.Color
                        Case "CmdGroupHeadingsFont"
                            Me.FormThemeManager.Theme.HeadingFont = dlg.Font
                            Me.FormThemeManager.Theme.HeadingFontColor = dlg.Color
                        Case Else
                    End Select
                End If
            End Using
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ComboColor_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboFormBackColor.SelectionChangeCommitted, ComboFormForeColor.SelectionChangeCommitted, ComboGroupHeadingsActiveColor.SelectionChangeCommitted, ComboGroupHeadingsInactiveColor.SelectionChangeCommitted, ComboDataEntryLabelBackColor.SelectionChangeCommitted, ComboDataEntryFieldBackColor.SelectionChangeCommitted, ComboGroupBordersColor.SelectionChangeCommitted
        If Not mEventsEnabled Then Return
        Try
            Dim combo As ComboColorPicker = DirectCast(sender, ComboColorPicker)

            Select Case combo.Name
                Case "ComboDataEntryLabelBackColor"
                    Me.FormThemeManager.Theme.DisplayLabelBackColor = CType(combo.SelectedItem, Color)
                Case "ComboDataEntryFieldBackColor"
                    Me.FormThemeManager.Theme.DisplayFieldBackColor = CType(combo.SelectedItem, Color)
                Case "ComboFormBackColor"
                    Me.FormThemeManager.Theme.FormBackColor = CType(combo.SelectedItem, Color)
                Case "ComboFormForeColor"

                Case "ComboGroupHeadingsActiveColor"
                    Me.FormThemeManager.Theme.HeadingActiveColor = CType(combo.SelectedItem, Color)
                Case "ComboGroupHeadingsInactiveColor"
                    Me.FormThemeManager.Theme.HeadingInactiveColor = CType(combo.SelectedItem, Color)
                Case "ComboGroupBordersColor"
                    Me.FormThemeManager.Theme.GroupingBorderColor = CType(combo.SelectedItem, Color)
                Case Else
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ComboBorderStyle_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboFormBorderStyle.SelectionChangeCommitted
        If Not mEventsEnabled Then Return
        Try
            Dim combo As ComboFormBorderStylePicker = DirectCast(sender, ComboFormBorderStylePicker)

            Select Case combo.Name
                Case "ComboFormBorderStyle"
                    Me.FormThemeManager.Theme.FormBorderStyle = CType(combo.SelectedItem, FormBorderStyle)
                Case Else
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ComboDashStylePicker_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboGroupBordersDashStylePicker.SelectionChangeCommitted
        If Not mEventsEnabled Then Return
        Try
            Dim combo As ComboDashStylePicker = DirectCast(sender, ComboDashStylePicker)

            Select Case combo.Name
                Case "ComboGroupBordersDashStylePicker"
                    Me.FormThemeManager.Theme.GroupingBorderDashStyle = CType(combo.SelectedItem, DashStyle)
                Case Else
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub NumericUpDown_KeyDown(sender As Object, e As KeyEventArgs) Handles NumericGroupBordersWidth.KeyDown
        If Not mEventsEnabled Then Return
        '
        ' We only want user-initiated numeric value changes to take effect.
        ' Check for {ENTER} and set flag. This routine handles all NumericUpDowns.
        '
        If e.KeyCode = Keys.Enter Then
            ' Prevent the default beep sound.
            e.SuppressKeyPress = True

            mUserInput = True
        End If
    End Sub

    Private Sub NumericUpDown_ValueChanged(sender As Object, e As EventArgs) Handles NumericGroupBordersWidth.ValueChanged
        If Not mEventsEnabled Then Return
        Try
            Dim num As NumericUpDown = DirectCast(sender, NumericUpDown)

            Select Case num.Name
                Case "NumericGroupBordersWidth"
                    Me.FormThemeManager.Theme.GroupingBorderWidth = CType(num.Value, Integer)
                Case Else
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TextBox_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtFormText.KeyDown
        If Not mEventsEnabled Then Return
        Try
            If e.KeyCode = Keys.Enter Then
                Dim txt As TextBox = DirectCast(sender, TextBox)

                Select Case txt.Name
                    Case "TxtFormText"
                        Me.FormThemeManager.Theme.FormText = txt.Text
                    Case Else
                End Select
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub UCDashPatternPicker_KeyDown(sender As Object, e As KeyEventArgs) Handles UCGroupBordersDashPatternPicker.KeyDown
        If Not mEventsEnabled Then Return
        Try
            Dim uc As DashPatternPicker = DirectCast(sender, DashPatternPicker)

            Select Case uc.Name
                Case "UCGroupBordersDashPatternPicker"
                    Me.FormThemeManager.Theme.GroupingBorderDashPattern = CType(uc.DashPattern, Single())
                Case Else
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub CmdCancel_Click(sender As Object, e As EventArgs) Handles CmdCancel.Click
        If mInitialTheme IsNot Nothing Then Me.FormThemeManager.Theme = mInitialTheme
    End Sub
End Class