Imports System.Windows.Forms.DataVisualization.Charting

Public Module Constants
    Public Const kBladePlotAxesMax As Integer = 100
    Public Const kBladePlotChartType As SeriesChartType = SeriesChartType.Point
    Public Const kBladePlotMarkerSize As Integer = 5
    Public Const kBladePlotMarkerStyle As MarkerStyle = MarkerStyle.Circle
    Public Const kInchToMm As Double = 25.4 ' Multiply inches by this to get millimeters
    Public Const kMmToInch As Double = 0.0393701 ' Multiply millimeters by this to get inches

    Public Class ReportFonts
        Public Shared ReadOnly ChartTitleFont = New Font("Segoe UI", 11.0F, FontStyle.Bold)
        Public Shared ReadOnly ChartAxisFont = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        Public Shared ReadOnly DisplayControlTitleFont = New Font("Segoe UI", 12.0F, FontStyle.Bold)
    End Class
End Module
