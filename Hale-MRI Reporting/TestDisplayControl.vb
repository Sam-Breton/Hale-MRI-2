Imports System.Windows.Forms.DataVisualization.Charting
Imports LibDatabase.Models

Public Class TestDisplayControl
    Inherits DisplayControl

    ' ********************************************************************************
    ' * Remove all magic numbers and strings from code and define either as module-  *
    ' * level constants here or global constants if used elsewhere.                  *
    ' ********************************************************************************
    Private Const kChartTitle As String = "Graph ABC"
    Private Const kChartYAxisTitle As String = "Y-Axis"
    Private Const kChartYAxisMax As Single = 10.0!
    Private Const kChartYAxisMin As Single = 1.0!
    Private Const kControlTitle As String = "Test Display Control"

    Public Sub New()
        MyBase.New()
        InitializeComponent()
    End Sub

    ' ********************************************************************************
    ' * This is how any properties required by this control should be defined.       *
    ' * All should be declared ReadOnly and be nullable. These are just for example. *
    ' * Keep in mind that essentially all data used to display propeller 
    ' ********************************************************************************
    Public ReadOnly Property JobDetails As JobDetail
        Get
            Return DirectCast(Me.Data, JobDetail)
        End Get
    End Property

    Public ReadOnly Property RadiusMeasurements As List(Of RadiusMeasurement)
        Get
            Return Me.JobDetails?.RadiusMeasurements
        End Get
    End Property

    Private ReadOnly Property BladeCount As Short?
        Get
            Return Me.JobDetails?.Job?.PropellerBlades
        End Get
    End Property

    Protected Overrides Sub DisplayInitialize()
        ' Any and all visual elements must be instantiated here.
        ' They can be referenced anywhere in the class by name,
        ' for instance to change their text or values.
        With Chart1
            .Annotations.Clear()
            .ChartAreas.Clear()
            .Legends.Clear()
            .Series.Clear()
            .Titles.Clear()
            .ChartAreas.Add("Test")
            .Titles.Add(New Title With {
                .Name = "ChartTitle",
                .Text = kChartTitle,
                .Font = New Font("Segoe UI", 11.0F, FontStyle.Bold),
                .Alignment = ContentAlignment.TopCenter
             })
        End With

        LabTitle.Text = kControlTitle

        With Chart1.ChartAreas("Test")
            .AxisY.Maximum = kChartYAxisMax
            .AxisY.Minimum = kChartYAxisMin
            .AxisY.Title = kChartYAxisTitle
            .AxisY.TitleFont = New Font("Segoe UI", 10.0F, FontStyle.Regular)
        End With

        MyBase.DisplayInitialize()  ' Always call the base method last.
    End Sub

    Protected Overrides Sub DataGet()
        ' Use this routine to get any data not provided by the base class Data property
        ' (which is always JobDetail). If no such data exists, this Sub can be removed.
        MyBase.DataGet()
    End Sub

    Protected Overrides Sub DataShow()
        ' Clear any controls first, since the user can close things like Jobs in FrmReports
        ' and we don't want any ghost data showing.
        TxtA.Clear()
        TxtB.Clear()
        TxtC.Clear()
        Chart1.Series.Clear()
        If Me.JobDetails IsNot Nothing Then
            ' Only display data if all required properties are set.
            TxtA.Text = If(Me.JobDetails?.Job?.PropellerDiameter, "")
            TxtB.Text = Date.Now.ToString()
            TxtC.Text = If(Me.JobDetails?.WheelPitch, "")

            Dim seriesTest As New Series() With {
                    .Name = $"TestSeries",
                    .ChartType = SeriesChartType.Column,
                    .ChartArea = "Test"
                }
            Dim rand As New Random()

            ' Use lowercase letters for loop variables.
            For i As Integer = 1 To 4
                ' Use camelcase words for local vars unless they're just letters or mono-syllables.
                Dim randomX As Single = CSng((rand.NextDouble() * (kChartYAxisMax - 1.0)) + 1.0)
                Dim randomY As Single = CSng((rand.NextDouble() * (kChartYAxisMax - 1.0)) + 1.0)
                Dim p As Integer = seriesTest.Points.AddXY($"B{i}=({randomX})", randomY)
                seriesTest.Points(p).Color = GraphColorArray(i - 1)
            Next

            ' Create all points then add entire Series at once.
            Chart1.Series.Add(seriesTest)
        End If
        MyBase.DataShow()   ' Always call the base method last.
    End Sub
End Class
