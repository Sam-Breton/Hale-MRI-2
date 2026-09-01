Imports System.ComponentModel

Namespace Models
    Public Class JobView
        Public Property Id As Integer

        Public Property JobNumber As Integer

        Public Property StartDate As Date?

        Public Property CustomerName As String

        Public Property VesselName As String

        Public Property Description As String

        Public Property InspectedByName As String

        Public Overridable Property Measurements As New BindingList(Of MeasurementsView)()
    End Class
End Namespace
