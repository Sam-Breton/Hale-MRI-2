Imports System.Windows.Forms.DataVisualization.Charting
Imports LibDatabase.Models
Imports Windows.ApplicationModel.Appointments

Public Class ChartSummary
    Inherits DisplayControl


#Region "Constants"
    Private Const kChartTitle As String = "Hale MRI - Summary Chart"
    Private Const kSeriesName As String = "Blade"
    Private Const kYAxisTitle As String = "Pitch"
    Private Const kYAxisMaxFactor As Double = 1.25#
    Private Const kYAxisMinFactor As Double = 0.75#
#End Region
#Region "Constructors"
    ''' <summary>
    ''' Creates a new ReportHeader object.
    ''' </summary>
    Public Sub New()
        MyBase.New()
        InitializeComponent()
    End Sub
#End Region
#Region "Private Members"
    Private mItems As String
    Private mAllowProgressivePitch
    Private mBlades As New List(Of String) ''' list to store integer(string) values of blades to be displayed
    Private mRadii As New List(Of String) ''' list to store rounded integer(string) values of radii to be displayed
#End Region
#Region "Public Interface"
    Public Property Blades As List(Of String)
        Get
            Return mBlades
        End Get
        Set(value As List(Of String))
            mBlades = value
            DataShow()
        End Set
    End Property
    Public Property Radii As List(Of String)
        Get
            Return mRadii
        End Get
        Set(value As List(Of String))
            mRadii = value
            DataShow()
        End Set
    End Property
#Region "Client Properties"
    Public ReadOnly Property Prec As String
        Get
            If Precision Is Nothing Then
                Return "F2"
            ElseIf Precision = 3 Then
                Return "F3"
            ElseIf Precision = 2 Then
                Return "F2"
            Else
                Return "F2"
            End If
        End Get
    End Property
    Public Overrides Property Basis As String
        Get
            Return MyBase.Basis
        End Get
        Set(value As String)
            MyBase.Basis = value
            If mDisplayInitialized = True Then
                BasisSet(Chart1.ChartAreas("Summary"))
                DataShow()
            End If
        End Set
    End Property
    Public Overrides Property Precision As Integer?
        Get
            Return MyBase.Precision
        End Get
        Set(value As Integer?)
            MyBase.Precision = value
            DataShow()
        End Set
    End Property
    ''' <summary>
    ''' Loaded Progression Measurements for making tolerance and reference lines
    ''' </summary>
    ''' <returns>Tolerance</returns>
    Public Overrides Property TolClass As Tolerance
        Get
            Return MyBase.TolClass
        End Get
        Set(value As Tolerance)
            MyBase.TolClass = value
            DataShow()
        End Set
    End Property
    ''' <summary>
    ''' Minimums Apply
    ''' </summary>
    ''' <returns>Boolean</returns>
    Public Property MinimumsApply As Boolean = True
    Public Overrides Property Data As Object
        Get
            Return MyBase.Data
        End Get
        Set(value As Object)
            MyBase.Data = value
            If Blades Is Nothing Then
                Blades = New List(Of String)
            End If
            If Blades.Count = 0 And BladeCount IsNot Nothing Then
                Dim x As Integer
                For x = 1 To BladeCount
                    Blades.Add(x.ToString())
                Next
            End If
            If Radii Is Nothing Then
                Radii = New List(Of String)
            End If
            If Radii.Count = 0 And RadiusMeasurements IsNot Nothing Then
                For Each rm As RadiusMeasurement In RadiusMeasurements
                    Radii.Add(Math.Round(rm.Radius.Value).ToString())
                Next
            End If
            DataShow()
        End Set
    End Property
#End Region
    Public Property AllowProgressivePitch As Boolean = False
#End Region
#Region "Computed Properties"
    ''' <summary>
    ''' Basis pitch value used for tolerance lines based on Basis property and JobDetails data
    ''' </summary>
    ''' <returns>Double</returns>
    Private ReadOnly Property BasisPitch As Double?
        Get
            If JobDetails Is Nothing Then
                Return 0
            End If
            Select Case Basis
                Case "Marked"
                    Return JobDetails.Job.MarkedPitch
                Case "Desired"
                    Return JobDetails.Job.DesiredPitch
                Case "Design"
                    Return 0 ' need to set  up loading designs for comparison
                Case Else ' "Mean"
                    Return JobDetails.WheelPitch
            End Select
        End Get
    End Property

    Public ReadOnly Property BladeCount As Short?
        Get
            Return Me.JobDetails?.Job?.PropellerBlades
        End Get
    End Property

    Private ReadOnly Property JobDetails As JobDetail
        Get
            Return DirectCast(Me.Data, JobDetail)
        End Get
    End Property

    Private ReadOnly Property LEExclusion As Double?
        Get
            Return Me.JobDetails?.Job?.LeExclusion
        End Get
    End Property

    Private ReadOnly Property LocalPitchSectors As Integer?
        Get
            Return Me.TolClass?.LocalPitchSectors
        End Get
    End Property

    Private ReadOnly Property PropellerDiameter As Double?
        Get
            Return Me.JobDetails?.Job?.PropellerDiameter
        End Get
    End Property

    Private ReadOnly Property RadiusMeasurements As List(Of RadiusMeasurement)
        Get
            Return Me.JobDetails?.RadiusMeasurements
        End Get
    End Property

    Private ReadOnly Property TEExclusion As Double?
        Get
            Return Me.JobDetails?.Job?.TeExclusion
        End Get
    End Property

    Private ReadOnly Property ToleranceClass As String
        Get
            Return If(Me.TolClass?.ToleranceClass, String.Empty)
        End Get
    End Property
    Private ReadOnly Property Customer As String
        Get
            Return JobDetails?.Job?.Vessel?.Customer?.CustomerName
        End Get
    End Property
    Private ReadOnly Property Vessel As String
        Get
            Return JobDetails?.Job?.Vessel?.VesselName
        End Get
    End Property
    Private ReadOnly Property Rotation As String
        Get
            Return JobDetails?.Job?.PropellerRotation
        End Get
    End Property
    Private ReadOnly Property StartDate As Date?
        Get
            Return JobDetails?.StartDate
        End Get
    End Property
#End Region
#Region "Private Interface"
    Private Sub BasisSet(cArea As ChartArea)
        Dim bp As Double = BasisPitch.Value
        'cArea.AxisY.Minimum = bp * kYAxisMinFactor
        cArea.AxisY.Maximum = bp * kYAxisMaxFactor
        cArea.AxisY.Interval = bp * 0.1
    End Sub
    Protected Overrides Sub DataShow() ''' finish up the getting multiple radii to work

        If JobDetails Is Nothing OrElse
                TolClass Is Nothing OrElse
            String.IsNullOrEmpty(Basis) Then
            Return
        End If
        Me.SuspendLayout()
        Chart1.Series.Clear()
        Chart1.Titles("Title1").Text = $"{kChartTitle} - {Customer} {Vessel} {Rotation} {StartDate?.ToString()} Class {TolClass.ToleranceClass}"
        Dim cArea As ChartArea = Chart1.ChartAreas("Summary")
        Chart1.Annotations.Clear()
        Chart1.Annotations.Add(New TextAnnotation With {
                              .Text = "Tol Basis - " + Basis + " Pitch = " + BasisPitch.Value.ToString(Prec),
                              .AnchorX = 25,
                              .AnchorY = 25
        })
        If AllowProgressivePitch Then
            Chart1.Annotations.Add(New TextAnnotation With {
                                  .Text = "Allow Progressive Pitch",
                                  .AnchorX = 25,
                                  .AnchorY = 30
            })
        End If
        For Each bladeId As String In Blades
            Dim ser As Series = Chart1.Series.Add("Blade" + bladeId)
            ser.ChartType = SeriesChartType.Column
            ser.ChartArea = cArea.Name
            ser.Color = GraphColorArray(CInt(bladeId))
            ser.XValueType = ChartValueType.String
            Dim BladeData As List(Of RadiusMeasurement) = RadiusMeasurements.Where(Function(r) r.BladeId = CInt(bladeId)).ToList()
            Dim y As Integer = 0
            For Each rm As RadiusMeasurement In BladeData
                If Radii.Contains(Math.Round(rm.Radius.Value).ToString()) Then
                    y += 1
                    Dim pitch As Double = GetRadiusMeasurementPitch(rm.CellMeasurements.ToList(), TEExclusion, LEExclusion)
                    Dim ind As Integer = ser.Points.AddXY(BladeData.IndexOf(rm), Math.Round(pitch, Precision.Value))
                    ser.Points(ind).AxisLabel = Math.Round(rm.Radius.Value).ToString(Prec)
                End If
            Next
            If Blades.IndexOf(bladeId) = 0 Then ' set up strip lines on each column based on tolerance class and Allow Progressive Pitch
                For Each rm As RadiusMeasurement In BladeData
                    If Radii.Contains(Math.Round(rm.Radius.Value).ToString()) Then
                        If AllowProgressivePitch = False Then
                            Dim sline As New StripLine With {
                                .IntervalOffset = BasisPitch.Value - (BasisPitch.Value * (TolClass.MeanPitchPerRadiusPercent / 100)),
                                .BorderWidth = 1,
                                .BorderDashStyle = ChartDashStyle.Solid,
                                .BorderColor = Color.Blue,
                                .StripWidth = Math.Round(rm.Radius.Value).ToString(Prec)
                            }
                            cArea.AxisY.StripLines.Add(sline)
                            sline = New StripLine With {
                                .IntervalOffset = BasisPitch.Value + (BasisPitch.Value * (TolClass.MeanPitchPerRadiusPercent / 100)),
                                .BorderWidth = 1,
                                .BorderDashStyle = ChartDashStyle.Solid,
                                .BorderColor = Color.Red,
                                .StripWidth = Math.Round(rm.Radius.Value).ToString(Prec)
                            }
                            cArea.AxisY.StripLines.Add(sline)
                            sline = New StripLine With {
                                .IntervalOffset = BasisPitch.Value,
                                .BorderWidth = 1,
                                .BorderDashStyle = ChartDashStyle.Solid,
                                .BorderColor = Color.Black,
                                .StripWidth = Math.Round(rm.Radius.Value).ToString(Prec)
                            }
                            cArea.AxisY.StripLines.Add(sline)
                        Else '''If app is false we first find the Average pitch of the segment on all blades and use that for the tolerance
                            Dim appPitch As Double = 0
                            For Each rm2 As RadiusMeasurement In RadiusMeasurements.Where(Function(rad) Math.Round(rad.Radius.Value) = Math.Round(rm.Radius.Value))
                                'appPitch += GetRadiusMeasurementPitch(rm2.CellMeasurements.ToList(), TEExclusion, LEExclusion)
                                appPitch += GetRadiusMeasurementPitch(rm2.CellMeasurements, TEExclusion, LEExclusion)
                            Next
                            appPitch /= JobDetails.Job.PropellerBlades
                            Dim sline As New StripLine With {
                                .IntervalOffset = appPitch - (appPitch * (TolClass.MeanPitchPerRadiusPercent / 100)),
                                .BorderWidth = 1,
                                .BorderDashStyle = ChartDashStyle.Solid,
                                .BorderColor = Color.Blue,
                                .StripWidth = Math.Round(rm.Radius.Value).ToString(Prec)
                            }
                            cArea.AxisY.StripLines.Add(sline)
                            sline = New StripLine With {
                                .IntervalOffset = appPitch + (appPitch * (TolClass.MeanPitchPerRadiusPercent / 100)),
                                .BorderWidth = 1,
                                .BorderDashStyle = ChartDashStyle.Solid,
                                .BorderColor = Color.Red,
                                .StripWidth = Math.Round(rm.Radius.Value).ToString(Prec)
                            }
                            cArea.AxisY.StripLines.Add(sline)
                            sline = New StripLine With {
                                .IntervalOffset = appPitch,
                                .BorderWidth = 1,
                                .BorderDashStyle = ChartDashStyle.Solid,
                                .BorderColor = Color.Black,
                                .StripWidth = Math.Round(rm.Radius.Value).ToString(Prec)
                            }
                            cArea.AxisY.StripLines.Add(sline)
                        End If
                    End If
                Next
            End If
            If Blades.Contains(bladeId) Then
                Dim avgpitch As Double = 0
                Dim pitchcount As Integer = 0
                For Each rm As RadiusMeasurement In BladeData
                    'Dim pitch = GetRadiusMeasurementPitch(rm.CellMeasurements.ToList(), TEExclusion, LEExclusion)
                    Dim pitch = GetRadiusMeasurementPitch(rm.CellMeasurements, TEExclusion, LEExclusion)
                    avgpitch += pitch
                    pitchcount += 1
                Next
                avgpitch /= pitchcount '''calculate and add Blade average to  chart
                Dim ind As Integer = ser.Points.AddXY(BladeData.Count, avgpitch)
                ser.Points(ind).AxisLabel = "Bld Avg"
            End If
        Next

        Dim seri As Series = Chart1.Series.Add("Wheel")
        seri.ChartType = SeriesChartType.Column
        seri.ChartArea = cArea.Name
        seri.Color = GraphColorArray(3)
        Dim int As Integer = seri.Points.AddXY(RadiusMeasurements.Where(Function(r) r.BladeId = 1).ToList().Count + 1, JobDetails.WheelPitch)
        seri.Points(int).AxisLabel = "Wheel"
        TableUpdate()
        Me.ResumeLayout()
    End Sub

    Private Sub TableUpdate()
        If Blades.Count = 0 Or Radii.Count = 0 Then
            Return
        End If
        Dim i As Integer
        Dim x As Integer
        PitchTable.Controls.Clear()
        BladeTable.Controls.Clear()
        PitchTable.RowCount = Blades.Count + 2 ''' +2 is for Radius label row and wheel avg row
        PitchTable.ColumnCount = Radii.Count + 1 ''' + 1 is for Blade Avg column
        BladeTable.RowCount = Blades.Count + 2 ''' +2 is for consistency between tables and Blades Label, and wheel avg row

        PitchTable.ColumnStyles.Clear()
        For i = 0 To PitchTable.ColumnCount - 1
            Dim colsty As New ColumnStyle With {
                .SizeType = SizeType.Percent,
                .Width = 100 / PitchTable.ColumnCount}
            PitchTable.ColumnStyles.Add(colsty)
        Next
        PitchTable.RowStyles.Clear()
        For i = 0 To PitchTable.RowCount - 1
            Dim rowsty As New RowStyle With {
                .SizeType = SizeType.Percent,
                .Height = 100 / PitchTable.RowCount}
            PitchTable.RowStyles.Add(rowsty)
            rowsty = New RowStyle With {
                .SizeType = SizeType.Percent,
                .Height = 100 / BladeTable.RowCount}
            BladeTable.RowStyles.Add(rowsty)
        Next
        For x = 1 To Blades.Count
            Dim lbl As Label
            If x = 1 Then
                lbl = New Label With {
                    .Text = "Blade/Rad",
                    .Dock = DockStyle.Fill,
                    .TextAlign = ContentAlignment.MiddleCenter}
                BladeTable.Controls.Add(lbl, 0, 0) ' place label in first row of blade table
                lbl = New Label With {
                    .Text = "Wheel Pitch",
                    .Dock = DockStyle.Fill,
                    .TextAlign = ContentAlignment.MiddleCenter}
                BladeTable.Controls.Add(lbl, 0, BladeTable.RowCount - 1) ' place label in last row of blade table
            End If
            lbl = New Label With {
                .Text = "Blade " + Blades(x - 1),
                .Dock = DockStyle.Fill,
                .TextAlign = ContentAlignment.MiddleCenter}
            BladeTable.Controls.Add(lbl, 0, x) ' place label in x column of blade table

            Dim BladeData As List(Of RadiusMeasurement) = RadiusMeasurements.Where(Function(r) r.BladeId = Integer.Parse(Blades(x - 1))).ToList()
            Dim y As Integer = 0
            For Each rm As RadiusMeasurement In BladeData
                If Radii.Contains(Math.Round(rm.Radius.Value).ToString()) Then '' only print labels for Radii from the list
                    If x = 1 Then
                        lbl = New Label With {
                            .Text = "Rad " + Math.Round(rm.Radius.Value).ToString(Prec),
                            .Dock = DockStyle.Fill,
                            .TextAlign = ContentAlignment.MiddleCenter}
                        PitchTable.Controls.Add(lbl, y, 0) 'place label in first row of pitch table
                    End If
                    'Dim pitch = GetRadiusMeasurementPitch(rm.CellMeasurements.ToList(), TEExclusion, LEExclusion)
                    Dim pitch = GetRadiusMeasurementPitch(rm.CellMeasurements, TEExclusion, LEExclusion)
                    '' add avg pitch of rad to table 
                    Dim fc As Color = ToColor(CheckBladeRadiusPitch(TolClass, pitch, BasisPitch.Value, False))
                    Dim txt As New Label With {
                        .Text = Math.Round(pitch, 3).ToString(Prec),
                        .ForeColor = fc,
                        .Dock = DockStyle.Fill,
                    .TextAlign = ContentAlignment.MiddleCenter}
                    PitchTable.Controls.Add(txt, y, x)
                    y += 1
                End If
            Next
            If x = 1 Then
                lbl = New Label With {
                    .Text = "Bld Avg",
                    .Dock = DockStyle.Fill,
                    .TextAlign = ContentAlignment.MiddleCenter}
                PitchTable.Controls.Add(lbl, y, 0) 'place label in last column first row of pitch table
            End If
            Dim AvgBladePitch As Double = GetBladeAveragePitch(BladeData)
            Dim ac As Color = ToColor(CheckBladePitch(TolClass, AvgBladePitch, BasisPitch.Value, False))
            Dim avgtext As New Label With {.Text = Math.Round(AvgBladePitch, 3).ToString(Prec),
                    .ForeColor = ac,
                    .Dock = DockStyle.Fill,
                    .TextAlign = ContentAlignment.MiddleCenter}
            PitchTable.Controls.Add(avgtext, y, x)
        Next
        Dim wc As Color = ToColor(CheckWheelPitch(TolClass, JobDetails.WheelPitch, BasisPitch.Value, False))
        Dim wheel As New Label With {.Text = Math.Round(JobDetails.WheelPitch.Value, 3).ToString(Prec),
            .ForeColor = wc,
            .Dock = DockStyle.Fill,
            .TextAlign = ContentAlignment.MiddleCenter}
        PitchTable.Controls.Add(wheel, PitchTable.ColumnCount - 1, PitchTable.RowCount - 1)
        PitchTable.BorderStyle = BorderStyle.Fixed3D
    End Sub

    Protected Overrides Sub DisplayInitialize()
        Chart1.ChartAreas.Clear()
        Chart1.Series.Clear()
        Chart1.Legends.Clear()

        Dim cArea As ChartArea = Chart1.ChartAreas.Add("Summary") '''leaving this instead of Chartarea1
        Dim leg As Legend = Chart1.Legends.Add("Legend1")
        Dim ser As Series = Chart1.Series.Add("Bld Avg")
        leg.Alignment = StringAlignment.Center
        leg.Docking = Docking.Top
        If (Chart1.Titles.Count = 0) Then
            Chart1.Titles.Add("Title1")
        End If
        Chart1.Titles("Title1").Text = kChartTitle '"Hale MRI - Summary Chart1"
        cArea.AxisY.Title = kYAxisTitle
        cArea.AxisX.MajorGrid.Enabled = False
        cArea.AxisX.MinorGrid.Enabled = False
        cArea.AxisX.MinorTickMark.Enabled = False
        cArea.AxisY.MajorGrid.Enabled = False
        cArea.AxisY.MinorGrid.Enabled = False
        cArea.AxisY.MajorTickMark.Enabled = True
        cArea.AxisY.MinorTickMark.Enabled = True
        cArea.AxisY.IsStartedFromZero = True
        Chart1.Annotations.Clear()
        MyBase.DisplayInitialize()
    End Sub
#End Region
End Class