Imports System.IO
Imports System.Reflection
Imports LibDatabase.Contexts
Imports LibGlobals
Imports Microsoft.Extensions.Configuration
Imports Microsoft.Extensions.DependencyInjection
Imports Microsoft.EntityFrameworkCore

' TODO: [Q3 2026] Check Focusrite & HP Drivers

Friend Module Program
    Public ServiceProvider As IServiceProvider

    <STAThread>
    Sub Main()
        ' Standard WinForms startup
        Application.SetHighDpiMode(HighDpiMode.SystemAware)
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)

        Dim logPath As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), STR_TITLE_DEFAULT)

        ' If the ApplicationDefaultFolder setting is empty, fallback to the User's Documents folder.
        If String.IsNullOrWhiteSpace(logPath) Then
            ' Combine Documents with a subfolder for the app.
            logPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), STR_TITLE_DEFAULT)
        End If

        ' Push the final path to the LibGlobals module.
        FileLogger.Initialize(logPath)
        FileLogger.Log("Sub Main(): Application starting")

        ' Global exception handlers to capture unhandled exceptions and log them.
        AddHandler Application.ThreadException, Sub(sender, e)
                                                    Try
                                                        FileLogger.LogException(e.Exception)
                                                    Catch
                                                        ' Swallow to avoid secondary exceptions.
                                                    End Try
                                                End Sub
        AddHandler AppDomain.CurrentDomain.UnhandledException, Sub(sender, e)
                                                                   Try
                                                                       Dim ex = TryCast(e.ExceptionObject, Exception)
                                                                       If ex IsNot Nothing Then
                                                                           FileLogger.LogException(ex)
                                                                       Else
                                                                           FileLogger.Log("UnhandledException: non-Exception object: " & e.ExceptionObject?.ToString())
                                                                       End If
                                                                   Catch
                                                                       ' Swallow
                                                                   End Try
                                                               End Sub
        AddHandler AppDomain.CurrentDomain.AssemblyResolve, Function(sender As Object, args As ResolveEventArgs) As Assembly
                                                                Dim result As Assembly = Nothing
                                                                FileLogger.Log($"Attempting to resolve assembly: {args.Name}")
                                                                Dim assemblyName As String = New AssemblyName(args.Name).Name
                                                                Dim assemblyPath As String = Path.Combine(AppContext.BaseDirectory, assemblyName + ".dll")
                                                                If (File.Exists(assemblyPath)) Then
                                                                    result = Assembly.LoadFrom(assemblyPath)
                                                                    FileLogger.Log($"{assemblyName} loaded from {assemblyPath}")
                                                                Else
                                                                    FileLogger.Log(String.Format(STR_ERR_FILE_NOT_FOUND, assemblyPath))
                                                                End If
                                                                Return result
                                                            End Function
        ' 1. Build configuration safely.
        Dim configuration As IConfiguration = LoadConfiguration()

        ' 2. Prepare service collection and register configuration
        Dim services = New ServiceCollection()
        services.AddSingleton(Of IConfiguration)(configuration)

        ' 3. Conditionally register DbContext if connection string is present
        Dim conn As String = Nothing

        Try
            ' Ensure the Master Source is populated (User prompt happens here)
            If EnsureDatabaseIsReady() Then
                ' Build the Jet string using the path from My.Settings
                conn = String.Format(STR_DATABASE_CONNECTION_PARAMS, My.Settings.DbConnectionString)
            End If

            ' Optional: Fallback to appsettings
            If String.IsNullOrWhiteSpace(conn) Then
                conn = configuration.GetConnectionString("HaleMRI")
            End If

        Catch ex As Exception
            conn = Nothing
            FileLogger.LogException(ex)
        End Try

        If Not String.IsNullOrWhiteSpace(conn) Then
            ' 1. Create a local variable that is NOT modified anywhere else.
            ' This prevents the "Set Once" error by locking the value for the DI container.
            Dim finalConnString As String = conn

            services.AddDbContext(Of HaleMRIContext)(Sub(options)
                                                         ' 2. Use the local variable here
                                                         options.UseJet(finalConnString)
                                                     End Sub)
            FileLogger.Log("Registering with: " & finalConnString)
        Else
            FileLogger.Log("DbContext NOT registered - connection string missing.")
        End If

        ' 4. Register forms and other services
        ' Add other forms/services as needed:
        ' *************************************************************
        ' *** Any new forms must be added here using AddTransient() ***
        ' *************************************************************
        services.AddScoped(Of FrmHaleMRI)()
        services.AddTransient(Of FrmCalibration)()
        services.AddTransient(Of FrmComparison)()
        services.AddTransient(Of FrmCustomers)()
        services.AddTransient(Of FrmGraph)()
        services.AddTransient(Of FrmInspection)()
        services.AddTransient(Of FrmJobs)()
        services.AddTransient(Of FrmManufacturers)()
        services.AddTransient(Of FrmPropellers)()
        services.AddTransient(Of FrmReports)()
        services.AddTransient(Of FrmVessels)()
        services.AddTransient(Of FrmMeasurements)()
        services.AddTransient(Of FrmMeasurementPicker)()
        services.AddTransient(Of FrmReportPicker)()
        ' For visual style comparison to FrmJobs.
        services.AddTransient(Of FrmJobs2)()
        ' For testing FormManagement.
        services.AddTransient(Of FrmDatabaseClient)()


        ' 5. Build provider and run main form
        ServiceProvider = services.BuildServiceProvider()

        Try
            Dim mainForm = ServiceProvider.GetRequiredService(Of FrmHaleMRI)()
            FileLogger.Log("Running main form.")
            Application.EnableVisualStyles()
            Application.Run(mainForm)
            FileLogger.Log("Application exited cleanly.")
        Catch ex As Exception
            FileLogger.LogException(ex)
            Throw
        End Try
    End Sub

    Private Function EnsureDatabaseIsReady() As Boolean
        ' 1. If we already have a valid path in our Master Source, we're done.
        If Not String.IsNullOrWhiteSpace(My.Settings.DbConnectionString) AndAlso File.Exists(My.Settings.DbConnectionString) Then
            Return True
        End If

        ' --- DEVELOPMENT AUTOMATION START ---
#If DEBUG Then
        ' This only runs while you are developing in Visual Studio.
        ' It looks for the LibDatabase folder relative to your EXE's debug folder.
        ' Adjust the number of "..\" to reach your solution root.
        Dim gitPath As String = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\..\..\..\LibDatabase\", STR_SETTING_NAME_DBFILE))

        If File.Exists(gitPath) Then
        My.Settings.DbConnectionString = gitPath
        My.Settings.Save()
        Return True
    End If
#End If
        ' --- DEVELOPMENT AUTOMATION END ---

        ' 2. Define the Installer's default location (Common App Data)
        Dim dbFileName As String = STR_SETTING_NAME_DBFILE
        'Dim defaultFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Hale-MRI")
        'Dim defaultPath = Path.Combine(defaultFolder, dbFileName)

        ' This message was used when application installation was done using the setup project. It is now commented out because the application is now installed manually.
        'Dim msg = "Database connection not found." & vbCrLf & vbCrLf &
        '  "Would you like to use the default installed database?" & vbCrLf & defaultPath

        ' 3. Ask the user (This will now only happen on the Alpha Tester's machine)
        Dim msg = STR_PROMPT_DATABASE_CONNECTION

        'Dim result = MessageBox.Show(msg, "Database Setup", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
        Dim result = MessageBox.Show(msg, STR_TITLE_DATABASE_SETUP, MessageBoxButtons.OKCancel, MessageBoxIcon.Question)

        'If result = DialogResult.OK Then
        '    If File.Exists(defaultPath) Then
        '        My.Settings.DbConnectionString = defaultPath
        '    Else
        '        MessageBox.Show("Default database file not found. Please browse for it manually.", "File Missing")
        '        Return PromptForManualPath(dbFileName)
        '    End If
        'ElseIf result = DialogResult.No Then
        '    Return PromptForManualPath(dbFileName)
        'Else
        '    Return False ' User clicked Cancel
        'End If
        If result = DialogResult.OK Then
            Return PromptForManualPath(dbFileName)
        Else
            Return False ' User clicked Cancel
        End If

        ' 4. Persist the choice to the Master Source
        My.Settings.Save()
        Return True
    End Function

    Private Function LoadConfiguration() As IConfiguration
        Try
            ' Directly access the setting you created in Project Properties
            Dim connString As String = My.Settings.DbConnectionString

            ' Build a simple in-memory configuration object 
            ' This keeps your existing code compatible with IConfiguration
            Dim memoryConfig = New Dictionary(Of String, String) From {
                {"ConnectionStrings:HaleMRI", connString}
            }

            Dim configuration = New ConfigurationBuilder() _
                .AddInMemoryCollection(memoryConfig) _
                .Build()

            FileLogger.Log("Configuration loaded successfully from Application Settings.")
            Return configuration
        Catch ex As Exception
            FileLogger.LogException(ex)
            Return New ConfigurationBuilder().Build()
        End Try
    End Function

    Private Function PromptForManualPath(fileName As String) As Boolean
        Using fbd As New FolderBrowserDialog()
            fbd.Description = String.Format(STR_PROMPT_PICK_FOLDER, fileName)
            If fbd.ShowDialog() = DialogResult.OK Then
                Dim fullPath = Path.Combine(fbd.SelectedPath, fileName)
                If File.Exists(fullPath) Then
                    My.Settings.DbConnectionString = fullPath
                    My.Settings.Save()
                    Return True
                End If
                MessageBox.Show(String.Format(STR_ERR_DATABASE_NOT_FOUND, fileName), STR_TITLE_DATABASE_ERROR)
            End If
        End Using
        Return False
    End Function
End Module
