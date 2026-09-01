Imports System.Runtime.CompilerServices
Imports System.Drawing
Public Module SizeFExtensions
    ''' <summary>
    ''' Returns TRUE if either Width or Height IsInfinity(), else returns FALSE.
    ''' </summary>
    ''' <param name="size"></param>
    ''' <returns>Boolean</returns>
    <Extension()>
    Public Function IsInfinity(ByVal size As SizeF) As Boolean
        Return Single.IsInfinity(size.Width) OrElse Single.IsInfinity(size.Height)
    End Function
End Module
