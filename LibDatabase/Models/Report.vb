Imports System
Imports System.Collections.Generic
Imports Microsoft.VisualBasic

Namespace Models
    Partial Public Class Report
        Public Property Id As Integer?

        Public Property ReportName As String

        Public Property Description As String

        Public Property LastModifed As Date

        Public Property ModifiedBy As Integer

        Public Property PageCount As Short

        Public Property LetterheadAllPages As Boolean?

        Public Property LetterheadBorderStyle As Short?

        Public Property LetterheadImage As String

        Public Property LetterheadSizeMode As Short?

        Public Property LetterheadVisible As Boolean?

        Public Property HeaderAllPages As Boolean?

        Public Property HeaderBorderStyle As Short?

        Public Property HeaderItems As String

        Public Property HeaderVisible As Boolean?

        Public Overridable Property ModifiedByNavigation As Employee

        Public Overridable Property ReportElements As ICollection(Of ReportElement) = New List(Of ReportElement)()
    End Class
End Namespace
