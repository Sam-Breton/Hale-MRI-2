Imports LibDatabase
Imports LibDatabase.Contexts
Imports LibDatabase.Models
Imports Microsoft.Extensions.DependencyInjection
Imports Microsoft.EntityFrameworkCore
Imports LibEncoder
Imports LibGlobals

Public Class FrmHaleMRI
    Inherits FrmDatabaseForm
#Region "Private Members"
    Private mWorkstationEncoders As WorkstationEncoders
#End Region
#Region "Constructors"
    ' DESIGNER CONSTRUCTOR: Must exist for Visual Studio Designer.
    Public Sub New()
        MyBase.New()
        InitializeComponent()
    End Sub

    ' RUNTIME DI CONSTRUCTOR: Match the Sub Main DI container.
    Public Sub New(context As HaleMRIContext, serviceProvider As IServiceProvider, scopeFactory As IServiceScopeFactory)
        ' Passes the context and serviceProvider straight up to FrmDatabaseForm.
        MyBase.New(context, serviceProvider, scopeFactory)
        InitializeComponent()
    End Sub
#End Region
#Region "Private Interface"
    Private Sub Login(ByVal userName As String, ByVal password As String)
        ' This method should handle user login logic.
        ' For now, it just clears the text boxes.
        If String.IsNullOrWhiteSpace(userName) OrElse String.IsNullOrWhiteSpace(password) Then
            MessageBox.Show("Please enter both username and password.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Me.User = ApplicationLogin(Me.Database, userName, password)
        If Me.User IsNot Nothing Then
            ' If login is successful, proceed to the main application.
            ' Here you can initialize the main form or load the necessary data.
            PanelLogin.Hide() ' Hide the login form if needed.
            PanelMenuButtons.Show() ' Show the main menu buttons.
        Else
            ' If login fails, show an error message.
            MessageBox.Show("Invalid username or password.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
        TxtUser.Text = String.Empty
        TxtPassword.Text = String.Empty
    End Sub

    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        CloseAllForms()
    End Sub

    Protected Overrides Sub OnLoad(e As EventArgs)
        Try
            MyBase.OnLoad(e)
            FileLogger.Log("Application started.")
            Me.Database.Database.EnsureCreated()
            FileLogger.Log("Database connection established.")
            Me.Database.Employees.Load()
            Me.Database.Workstations.Load()
            FileLogger.Log("Database context initialized.")
            mWorkstationEncoders = New WorkstationEncoders() With {
                .Encoders = New EncoderHardware(New USDigital()),
                .Workstation = If(
                    Me.Database.Workstations.FirstOrDefault(Function(w) w.Hostname = My.Computer.Name),
                    Me.Database.Workstations.FirstOrDefault(Function(w) w.Hostname = STR_CALIBRATION_DEFAULT)
                )
            }
        Catch ex As Exception
            FileLogger.LogException(ex)
            MessageBox.Show(String.Format(STR_ERR_ENCODERS_DETAILS, ex.Message), STR_TITLE_ENCODER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region
#Region "Event Handlers"
    Private Sub CmdCustomers_Click(sender As Object, e As EventArgs) Handles CmdCustomers.Click
        Try
            ShowForm(Of FrmCustomers)(Me.ScopeFactory, Me.User)
        Catch ex As Exception
            MessageBox.Show(String.Format(STR_ERR_FORM_OPEN, STR_OBJECT_CUSTOMER & "s", ex.Message), STR_TITLE_APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CmdJobs_Click(sender As Object, e As EventArgs) Handles CmdJobs.Click
        Try
            Dim frm As FrmJobs2 = DirectCast(ShowForm(Of FrmJobs2)(Me.ScopeFactory, Me.User), FrmJobs2)

            frm.Hardware = mWorkstationEncoders
        Catch ex As Exception
            MessageBox.Show(String.Format(STR_ERR_FORM_OPEN, STR_OBJECT_JOB & "s", ex.Message), STR_TITLE_APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CmdLoginCancel_Click(sender As Object, e As EventArgs) Handles CmdLoginCancel.Click
        Try
            TxtPassword.Text = String.Empty
            TxtUser.Text = String.Empty
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub CmdLoginOK_Click(sender As Object, e As EventArgs) Handles CmdLoginOK.Click
        Try
            Login(TxtUser.Text, TxtPassword.Text)
        Catch ex As Exception
            MessageBox.Show(String.Format(STR_ERR_LOGIN, $"{ex.Message}"), STR_TITLE_APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CmdManufacturers_Click(sender As Object, e As EventArgs) Handles CmdManufacturers.Click
        Try
            ShowForm(Of FrmManufacturers)(Me.ScopeFactory, Me.User)
        Catch ex As Exception
            MessageBox.Show(String.Format(STR_ERR_FORM_OPEN, STR_OBJECT_MANUFACTURER & "s", ex.Message), STR_TITLE_APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CmdPropellers_Click(sender As Object, e As EventArgs) Handles CmdPropellers.Click
        Try
            ShowForm(Of FrmPropellers)(Me.ScopeFactory, Me.User)
        Catch ex As Exception
            MessageBox.Show(String.Format(STR_ERR_FORM_OPEN, STR_OBJECT_PROPELLER & "s", ex.Message), STR_TITLE_APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CmdReports_Click(sender As Object, e As EventArgs) Handles CmdReports.Click
        Try
            ShowForm(Of FrmReports)(Me.ScopeFactory, Me.User)
        Catch ex As Exception
            MessageBox.Show(String.Format(STR_ERR_FORM_OPEN, STR_OBJECT_REPORT & "s", ex.Message), STR_TITLE_APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CmdSettings_Click(sender As Object, e As EventArgs) Handles CmdSettings.Click
        Try
            ShowForm(Of FrmSettings)()
        Catch ex As Exception
            MessageBox.Show(String.Format(STR_ERR_FORM_OPEN, STR_OBJECT_SETTING & "s", ex.Message), STR_TITLE_APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub CmdVessels_Click(sender As Object, e As EventArgs) Handles CmdVessels.Click
        Try
            ShowForm(Of FrmVessels)(Me.ScopeFactory, Me.User)
        Catch ex As Exception
            MessageBox.Show(String.Format(STR_ERR_FORM_OPEN, STR_OBJECT_VESSEL & "s", ex.Message), STR_TITLE_APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CmdWorkstation_Click(sender As Object, e As EventArgs) Handles CmdWorkstation.Click
        Try

            Dim frm As FrmCalibration = DirectCast(ShowForm(Of FrmCalibration)(Me.ScopeFactory, Me.User), FrmCalibration)

            frm.Hardware = mWorkstationEncoders
        Catch ex As Exception
            MessageBox.Show(String.Format(STR_ERR_FORM_OPEN, STR_OBJECT_CALIBRATION, ex.Message), STR_TITLE_APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub TxtUser_TextChanged(sender As Object, e As EventArgs) Handles TxtUser.TextChanged
        Try
            CmdLoginOK.Enabled = Not String.IsNullOrWhiteSpace(TxtUser.Text) AndAlso Not String.IsNullOrWhiteSpace(TxtPassword.Text)
            CmdLoginCancel.Enabled = CmdLoginOK.Enabled
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TxtPassword_TextChanged(sender As Object, e As EventArgs) Handles TxtPassword.TextChanged
        Try
            CmdLoginOK.Enabled = Not String.IsNullOrWhiteSpace(TxtUser.Text) AndAlso Not String.IsNullOrWhiteSpace(TxtPassword.Text)
            CmdLoginCancel.Enabled = CmdLoginOK.Enabled
        Catch ex As Exception

        End Try
    End Sub
#End Region
End Class
