Imports System.IO
Imports System.IO.Pipes
Imports System.Text

Public Module ThirdPartyClient
    Private Const PipeName As String = "USDigitalPipe"

    ' Synchronous request with connect timeout (ms). Call from Task.Run to avoid UI blocking.
    Public Function CallHelper(request As String, Optional connectTimeoutMs As Integer = 1000) As String
        Try
            Using client As New NamedPipeClientStream(".", PipeName, PipeDirection.InOut, PipeOptions.None)
                client.Connect(connectTimeoutMs)
                Using sw As New StreamWriter(client, Encoding.UTF8) With {.AutoFlush = True}
                    Using sr As New StreamReader(client, Encoding.UTF8)
                        sw.WriteLine(request)
                        Dim response As String = sr.ReadLine()
                        Return If(response, String.Empty)
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Return "ERROR: " & ex.Message
        End Try
    End Function
End Module
