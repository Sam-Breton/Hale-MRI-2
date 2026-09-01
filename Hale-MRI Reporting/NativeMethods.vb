Imports System.Runtime.InteropServices

<System.Security.SuppressUnmanagedCodeSecurity()>
Friend Module NativeMethods
    ' API for direct screen access so we can draw in Controls'
    ' non-client areas.

    Public Const RDW_FRAME As UInteger = &H400
    Public Const RDW_INVALIDATE As UInteger = &H1
    Public Const RDW_UPDATENOW As UInteger = &H100

    Public Const SWP_NOSIZE As UInteger = &H1
    Public Const SWP_NOMOVE As UInteger = &H2
    Public Const SWP_NOZORDER As UInteger = &H4
    Public Const SWP_NOACTIVATE As UInteger = &H10
    Public Const SWP_FRAMECHANGED As UInteger = &H20 ' Forces WM_NCCALCSIZE

    Public Const WM_PAINT As Integer = &HF
    Public Const WM_NCPAINT As Integer = &H85
    Public Const WM_NCCALCSIZE As Integer = &H83
    Public Const WM_WINDOWPOSCHANGED = &H47

    <DllImport("user32.dll", SetLastError:=True)>
    Friend Function SetWindowPos(ByVal hWnd As IntPtr,
                                 ByVal hWndInsertAfter As IntPtr,
                                 ByVal X As Integer,
                                 ByVal Y As Integer,
                                 ByVal cx As Integer,
                                 ByVal cy As Integer,
                                 ByVal uFlags As UInteger) As Boolean
    End Function

    <DllImport("user32.dll")>
    Friend Function GetWindowDC(ByVal hWnd As IntPtr) As IntPtr
    End Function

    <DllImport("user32.dll")>
    Friend Function RedrawWindow(ByVal hWnd As IntPtr,
                                     ByVal lprcUpdate As IntPtr,
                                     ByVal hrgnUpdate As IntPtr,
                                     ByVal flags As UInteger) As Boolean
    End Function

    <DllImport("user32.dll")>
    Friend Function ReleaseDC(ByVal hWnd As IntPtr, ByVal hDC As IntPtr) As Integer
    End Function

    <DllImport("user32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
    Public Function SendMessage(
        ByVal hWnd As IntPtr,
        ByVal Msg As Integer,
        ByVal wParam As IntPtr,
        ByVal lParam As IntPtr
    ) As IntPtr
    End Function
End Module
