Imports LibDatabase
Imports LibGlobals

Public Class FrmSettings

#Region "Private Members"
    Private mSettingsControls As New List(Of Tuple(Of String, Control)) ' List of settings and associated controls.
#End Region
#Region "Private Interface"
    Private Sub HandleControl(ByVal aControl As Control)
        ' Assigns "change" event handlers to any settings controls 
        ' to notify us when a setting has been edited.
        If aControl IsNot Nothing Then
            Select Case True
                Case TypeOf aControl Is TextBox
                    AddHandler CType(aControl, TextBox).TextChanged, AddressOf Bound_TextChanged
                Case TypeOf aControl Is ComboBox
                    AddHandler CType(aControl, ComboBox).SelectionChangeCommitted, AddressOf Bound_SelectionChangeCommitted
                Case TypeOf aControl Is CheckBox
                    AddHandler CType(aControl, CheckBox).CheckedChanged, AddressOf Bound_CheckChanged
                Case TypeOf aControl Is DataGridView
                    AddHandler CType(aControl, DataGridView).CellBeginEdit, AddressOf Bound_CellBeginEdit
                Case Else
                    ' Handle other control types if necessary.
            End Select
        End If
    End Sub

    Private Property SaveUndoControlsEnabled As Boolean
        Get
            Return CmdSave.Enabled Or CmdUndo.Enabled
        End Get
        Set(value As Boolean)
            CmdSave.Enabled = value
            CmdUndo.Enabled = value
        End Set
    End Property

    Private Sub GetSettings()
        If My.Settings IsNot Nothing Then
            For Each settingItem As Tuple(Of String, Control) In mSettingsControls
                Dim settingName As String = settingItem.Item1
                Dim control As Control = settingItem.Item2

                ' Get the setting value and update the control
                Dim settingValue As Object = My.Settings(settingName)
                If settingValue IsNot Nothing Then
                    If control IsNot Nothing Then
                        Select Case True
                            Case TypeOf control Is TextBox
                                control.Text = settingValue.ToString()
                            Case TypeOf control Is ComboBox
                                control.Text = settingValue.ToString()
                            Case TypeOf control Is CheckBox
                                Dim resultBoolean As Boolean
                                Dim success As Boolean

                                success = Boolean.TryParse(settingValue.ToString(), resultBoolean)

                                If success Then
                                    DirectCast(control, CheckBox).Checked = resultBoolean
                                End If
                            Case TypeOf control Is DataGridView
                                ' ???
                            Case Else
                                ' Handle other control types if necessary.
                        End Select
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub SaveSettings()
        For Each settingItem As Tuple(Of String, Control) In mSettingsControls
            Dim settingName As String = settingItem.Item1
            Dim control As Control = settingItem.Item2

            ' Update the setting value
            My.Settings(settingName) = control.Text
        Next

        ' Save changes
        My.Settings.Save()
        SaveUndoControlsEnabled = False
    End Sub

    Private Sub UndoSettings()
        GetSettings()
        SaveUndoControlsEnabled = False
    End Sub
#End Region
#Region "Event Handlers"
    Private Sub Bound_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs)
        SaveUndoControlsEnabled = True
    End Sub

    Private Sub Bound_CheckChanged(sender As Object, e As EventArgs)
        SaveUndoControlsEnabled = True
    End Sub

    Private Sub Bound_SelectionChangeCommitted(sender As Object, e As EventArgs)
        Dim cmb As ComboBox = CType(sender, ComboBox)
        If cmb.SelectedIndex <> kNoCurrentSelection Then
            SaveUndoControlsEnabled = True
        End If
    End Sub

    Private Sub Bound_TextChanged(sender As Object, e As EventArgs)
        Dim txtbox As TextBox = CType(sender, TextBox)
        If txtbox.Modified Then
            SaveUndoControlsEnabled = True
            txtbox.Modified = False ' Reset the modified state to prevent repeated triggering.
        End If
    End Sub

    Private Sub CmdDbFilePath_Click(sender As Object, e As EventArgs) Handles CmdDbFilePath.Click
        Dim ofd As New OpenFileDialog With {
            .Title = STR_DIALOG_PROMPT_DB_SELECT,
            .Filter = STR_DIALOG_FILTER_DATABASE
        }
        If ofd.ShowDialog() = DialogResult.OK Then TxtDatabaseConnectionString.Text = ofd.FileName
    End Sub

    Private Sub CmdDefaultFolder_Click(sender As Object, e As EventArgs) Handles CmdDefaultFolder.Click
        Dim ofd As New FolderBrowserDialog With {
            .Description = "Select Default Application Folder",
            .RootFolder = Environment.SpecialFolder.MyComputer
        }
        If ofd.ShowDialog() = DialogResult.OK Then TxtApplicationDefaultFolder.Text = ofd.SelectedPath
    End Sub

    Private Sub CmdSave_Click(sender As Object, e As EventArgs) Handles CmdSave.Click
        SaveSettings()
    End Sub

    Private Sub CmdUndo_Click(sender As Object, e As EventArgs) Handles CmdUndo.Click
        UndoSettings()
    End Sub

    Private Sub FrmSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialize our list of settings and associated controls. For new controls,
        ' add another mSettingsControls.Add(New Tuple(Of String, Control)(Item1, Item2),
        ' where Item1 is the [~Settings].[Setting Name] and Item2 is the new control name.
        '
        ' *** NOTE: for some reason
        '   mSettingsControls = New List(Of Tuple(Of String, Control)) From {
        '       New Tuple(Of String, Control)(STR_SETTING_COMPANY_NAME, TxtCompanyName),
        '       ...
        '   }
        ' doesn't work. The string element is assigned, but the Control is always Nothing
        ' regardless of how or where it's referenced????? So, we have to do this here:
        '''''''
        mSettingsControls.Add(New Tuple(Of String, Control)("CompanyName", TxtCompanyName))
        mSettingsControls.Add(New Tuple(Of String, Control)("CompanyAddress", TxtCompanyAddress))
        mSettingsControls.Add(New Tuple(Of String, Control)("CompanyPhone", TxtCompanyPhone))
        mSettingsControls.Add(New Tuple(Of String, Control)("CompanyContact", TxtCompanyContact))
        mSettingsControls.Add(New Tuple(Of String, Control)("CompanyEmail", TxtCompanyEmail))
        mSettingsControls.Add(New Tuple(Of String, Control)("CompanyWebsite", TxtCompanyWebsite))
        mSettingsControls.Add(New Tuple(Of String, Control)("DbConnectionString", TxtDatabaseConnectionString))
        mSettingsControls.Add(New Tuple(Of String, Control)("ApplicationDefaultFolder", TxtApplicationDefaultFolder))
        mSettingsControls.Add(New Tuple(Of String, Control)("EncoderDataDefaultFolder", TxtEncodersDefaultFolder))
        mSettingsControls.Add(New Tuple(Of String, Control)("EncoderDefaultSampleInterval", TxtEncodersSamplePeriod))
        mSettingsControls.Add(New Tuple(Of String, Control)("EncoderMaxSamplesPerScan", TxtEncodersMaxSamplesPerScan))
        '''''''
        For Each settingControl As Tuple(Of String, Control) In mSettingsControls
            HandleControl(settingControl.Item2)
        Next
        GetSettings()
    End Sub
#End Region
End Class