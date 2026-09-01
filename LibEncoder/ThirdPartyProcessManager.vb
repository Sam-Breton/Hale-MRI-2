Imports System.IO
Imports LibGlobals

Public Module ThirdPartyProcessManager
    Private helperProcess As Process = Nothing
    Public Sub EnsureHelperStarted()
        Try
            Dim exePath = Path.Combine(AppContext.BaseDirectory, "ThirdPartyHelper.exe")
            If Not File.Exists(exePath) Then
                FileLogger.Log("Helper exe not found: " & exePath)
                Return
            End If

            Dim nameOnly = Path.GetFileNameWithoutExtension(exePath)
            Dim procs = Process.GetProcessesByName(nameOnly)
            If procs IsNot Nothing AndAlso procs.Length > 0 Then
                helperProcess = procs(0)
                FileLogger.Log("Helper already running (pid=" & helperProcess.Id & ")")
                Return
            End If

            Dim startInfo As New ProcessStartInfo(exePath) With {
                .UseShellExecute = False,
                .CreateNoWindow = True
            }
            helperProcess = Process.Start(startInfo)
            FileLogger.Log("Started helper: " & exePath & " (pid=" & helperProcess.Id & ")")
        Catch ex As Exception
            FileLogger.LogException(ex)
        End Try
    End Sub

    Public Sub StopHelper()
        Try
            If helperProcess IsNot Nothing AndAlso Not helperProcess.HasExited Then
                ' Try graceful shutdown by sending "shutdown" command; best-effort.
                Dim resp = ThirdPartyClient.CallHelper("shutdown", 500)
                helperProcess.WaitForExit(1000)
                If Not helperProcess.HasExited Then
                    helperProcess.Kill(True)
                End If
                FileLogger.Log("Stopped helper")
            End If
        Catch ex As Exception
            FileLogger.LogException(ex)
        End Try
    End Sub
End Module
