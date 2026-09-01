''' <summary>
''' Used by employees to check off items during bore inspections.
''' No code needed.
''' </summary>
Public Class BoreInspectTable
    Inherits DisplayControl
#Region "Types and Constants"
    Private Const kTableTitle As String = "Bore Inspect Table"
#End Region
#Region "Constructors"
    Public Sub New()
        MyBase.New()
        InitializeComponent()
    End Sub
#End Region
End Class
