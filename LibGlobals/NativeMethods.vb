Imports System.Runtime.InteropServices

<System.Security.SuppressUnmanagedCodeSecurity()>
Public Module NativeMethods
    ' API for direct screen access so we can draw in Controls'
    ' non-client areas.
    Public Const GWL_EXSTYLE As Integer = -20

    Public Const RDW_FRAME As UInteger = &H400
    Public Const RDW_INVALIDATE As UInteger = &H1
    Public Const RDW_UPDATENOW As UInteger = &H100

    Public Const SWP_NOSIZE As UInteger = &H1
    Public Const SWP_NOMOVE As UInteger = &H2
    Public Const SWP_NOZORDER As UInteger = &H4
    Public Const SWP_NOACTIVATE As UInteger = &H10
    Public Const SWP_FRAMECHANGED As UInteger = &H20 ' Forces WM_NCCALCSIZE

    Public Const TME_HOVER As Integer = &H1
    Public Const TME_LEAVE As Integer = &H2

    Public Const WM_PAINT As Integer = &HF
    Public Const WM_NCPAINT As Integer = &H85
    Public Const WM_NCCALCSIZE As Integer = &H83
    Public Const WM_WINDOWPOSCHANGED = &H47

    Public Const WM_MOUSEMOVE As Integer = &H200
    Public Const WM_LBUTTONDOWN As Integer = &H201
    Public Const WM_LBUTTONUP As Integer = &H202
    Public Const WM_RBUTTONDOWN As Integer = &H204
    Public Const WM_RBUTTONUP As Integer = &H205
    Public Const WM_MOUSEHOVER As Integer = &H2A1
    Public Const WM_MOUSELEAVE As Integer = &H2A3

    Public Const WS_EX_TRANSPARENT As Integer = &H20
    Public Const WS_EX_LAYERED As Integer = &H80000

    <StructLayout(LayoutKind.Sequential)>
    Public Structure TRACK_MOUSE_EVENT
        Public cbSize As Integer, dwFlags As Integer, hwndTrack As IntPtr, dwHoverTime As Integer
    End Structure

    Public Declare Function FindWindowEx Lib "user32" Alias "FindWindowExA" (ByVal hWndParent As IntPtr, ByVal hWndChildAfter As IntPtr, ByVal lpszClass As String, ByVal lpszWindow As String) As IntPtr
    Public Declare Function GetWindowLong Lib "user32" Alias "GetWindowLongA" (ByVal hWnd As IntPtr, ByVal nIndex As Integer) As Integer
    Public Declare Function SetWindowLong Lib "user32" Alias "SetWindowLongA" (ByVal hWnd As IntPtr, ByVal nIndex As Integer, ByVal dwNewLong As Integer) As Integer

    <DllImport("user32.dll", SetLastError:=True)>
    Public Function SetWindowPos(ByVal hWnd As IntPtr,
                                 ByVal hWndInsertAfter As IntPtr,
                                 ByVal X As Integer,
                                 ByVal Y As Integer,
                                 ByVal cx As Integer,
                                 ByVal cy As Integer,
                                 ByVal uFlags As UInteger) As Boolean
    End Function

    <DllImport("user32.dll")>
    Public Function GetWindowDC(ByVal hWnd As IntPtr) As IntPtr
    End Function

    <DllImport("user32.dll")>
    Public Function RedrawWindow(ByVal hWnd As IntPtr,
                                 ByVal lprcUpdate As IntPtr,
                                 ByVal hrgnUpdate As IntPtr,
                                 ByVal flags As UInteger) As Boolean
    End Function

    <DllImport("user32.dll")>
    Public Function ReleaseDC(ByVal hWnd As IntPtr, ByVal hDC As IntPtr) As Integer
    End Function

    <DllImport("user32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
    Public Function SendMessage(
        ByVal hWnd As IntPtr,
        ByVal Msg As Integer,
        ByVal wParam As IntPtr,
        ByVal lParam As IntPtr
    ) As IntPtr
    End Function

    <DllImport("user32.dll")>
    Public Function TrackMouseEvent(ByRef lpEventTrack As TRACK_MOUSE_EVENT) As Boolean
    End Function

    <DllImport("user32.dll")>
    Public Function GetParent(hWnd As IntPtr) As IntPtr
    End Function
End Module
