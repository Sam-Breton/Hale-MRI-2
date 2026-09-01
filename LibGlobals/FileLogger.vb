Imports System.IO

''' <summary>
''' Thread-safe minimal file logger for simple value logging.
''' </summary>
Public Module FileLogger
    Private mDirectoryName As String
    Private mFileName As String
    Private mInitialized As Boolean = False
    Private ReadOnly mLockObj As New Object()

    Private Sub EnsureLogDirectory()
        If Not mInitialized Then Return
        Try
            If Not Directory.Exists(mDirectoryName) Then
                Directory.CreateDirectory(mDirectoryName)
            End If
        Catch
            ' Ignore directory creation failures.
        End Try
    End Sub

    Public Sub Initialize(logPath As String)
        mDirectoryName = Path.Combine(logPath, STR_SETTING_NAME_LOGDIR)
        mFileName = Path.Combine(mDirectoryName, STR_SETTING_NAME_LOGFILE)
        mInitialized = True
    End Sub

    Public Sub Log(message As String)
        Try
            EnsureLogDirectory()
            Dim entry As String = String.Format(STR_SETTING_FORMAT_LOGINFO, DateTime.Now, message, Environment.NewLine)
            SyncLock mLockObj
                ' Append text safely; use File.AppendAllText which opens/closes stream for each write.
                File.AppendAllText(mFileName, entry)
            End SyncLock
        Catch
            ' Ensure logging never throws to caller.
        End Try
    End Sub

    Public Sub LogException(ex As Exception)
        Try
            EnsureLogDirectory()
            Dim entry As String = String.Format(STR_SETTING_FORMAT_LOGERROR, DateTime.Now, ex.Message, Environment.NewLine, Environment.NewLine, ex.StackTrace, Environment.NewLine)
            SyncLock mLockObj
                File.AppendAllText(mFileName, entry)
            End SyncLock
        Catch
            ' Swallow exceptions from logging.
        End Try
    End Sub
End Module
