Imports System
Imports System.Collections.Generic
Imports Microsoft.VisualBasic

Namespace Models
    Partial Public Class Employee
        Public Property Id As Integer?

        Public Property EmployeeName As String

        Public Property Password As String

        Public Property Permissions As Integer

        Public Overridable Property JobDetails As ICollection(Of JobDetail) = New List(Of JobDetail)()

        Public Overridable Property Jobs As ICollection(Of Job) = New List(Of Job)()

        Public Overridable Property Reports As ICollection(Of Report) = New List(Of Report)()
    End Class
End Namespace
