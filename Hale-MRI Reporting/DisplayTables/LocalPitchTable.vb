Imports System.ComponentModel
Imports LibDatabase.Models

Public Class LocalPitchTable
    Inherits DisplayControl
    ''' this is a display table not used in reports that gives a by sector readout of the local Pitch sectors of each radius Measurement based on the selected tolerance class and basis
#Region "Constructors"
    ''' <summary>
    ''' Creates a new ReportHeader object.
    ''' </summary>
    Public Sub New()
        MyBase.New()
        InitializeComponent()
    End Sub
#End Region
#Region "Constants"
    Private Const kTitle1 = "Local Pitch details for Class "
    Private Const kTitle2NoProgressivePitch = " Inspection"
    Private Const kTitle2AllowProgressivePitch = " Inspection, allowing Progressive Pitch"
    Private Const kBladeLabelSize = 65
    Private Const kTELELabelSize = 35
    Private Const kStandardColumnSize = 100
#End Region
#Region "Public Interface"
#Region "Client Properties"
    ''' <summary>
    ''' Allow progressive pitch for tolerance lines
    ''' </summary>
    ''' <returns>Boolean</returns>
    Public Property AllowProgressivePitch As Boolean = False
    ''' <summary>
    ''' Minimums Apply
    ''' </summary>
    ''' <returns>Boolean</returns>
    Public Property MinimumsApply As Boolean = False
    Public Overrides Property Basis As String
        Get
            Return MyBase.Basis
        End Get
        Set(value As String)
            MyBase.Basis = value
        End Set
    End Property
    Public Overrides Property Precision As Integer?
        Get
            Return MyBase.Precision
        End Get
        Set(value As Integer?)
            MyBase.Precision = value
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
        End Set
    End Property
    Public Overrides Property Data As Object
        Get
            Return CType(MyBase.Data, JobDetail)
        End Get
        Set(value As Object)
            MyBase.Data = value
            DataShow()
        End Set
    End Property
#End Region
#Region "Computed Properties"
    <Browsable(False)>
    Private ReadOnly Property BasisPitch As Double?
        Get
            If JobDetails Is Nothing Then
                Return 0
            End If
            Select Case Basis
                Case "Marked"
                    Return JobDetails?.Job?.MarkedPitch
                Case "Desired"
                    Return JobDetails?.Job?.DesiredPitch
                Case "Design"
                    Return 0 ' need to set up loading designs for comparison
                Case Else ' "Mean"
                    Return JobDetails?.WheelPitch
            End Select
        End Get
    End Property

    ''' <summary>
    ''' Propeller blade count.
    ''' </summary>
    ''' <returns>Integer?</returns>
    <Browsable(False)>
    Public ReadOnly Property BladeCount As Short?
        Get
            Return Me.JobDetails?.Job?.PropellerBlades
        End Get
    End Property

    ''' <summary>
    ''' Provides chart metadata.
    ''' </summary>
    ''' <returns>JobDetail</returns>
    <Browsable(False)>
    Private ReadOnly Property JobDetails As JobDetail
        Get
            Return DirectCast(Me.Data, JobDetail)
        End Get
    End Property
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
    Private ReadOnly Property PropellerDiameter As Double?
        Get
            Return JobDetails?.Job.PropellerDiameter
        End Get
    End Property
    Private ReadOnly Property LocalPitchSectors As Integer?
        Get
            Return TolClass?.LocalPitchSectors
        End Get
    End Property

    <Browsable(False)>
    Private ReadOnly Property LEExclusion As Double?
        Get
            Return Me.JobDetails?.Job?.LeExclusion
        End Get
    End Property

    ''' <summary>
    ''' Blade radius measurements.
    ''' </summary>
    ''' <returns></returns>
    <Browsable(False)>
    Private ReadOnly Property RadiusMeasurements As List(Of RadiusMeasurement)
        Get
            Return JobDetails?.RadiusMeasurements
        End Get
    End Property

    Private ReadOnly Property TEExclusion As Double?
        Get
            Return JobDetails?.Job?.TeExclusion
        End Get
    End Property
    Public ReadOnly Property NeededSize As Size
        Get
            If JobDetails IsNot Nothing And TolClass IsNot Nothing Then
                Dim contsize As New Size
                Dim height As Integer = 225 + (25 * (BladeCount * (LocalPitchSectors + 1)))
                contsize.Height = height
                Dim Width As Integer = 100 + (85 * (RadiusMeasurements.Where(Function(r) r.BladeId = 1).ToList().Count + 1))
                If Width < 750 Then
                    Width = 750
                End If
                contsize.Width = Width
                Return contsize
            Else
                Dim contsize As New Size With {
                    .Height = 350,
                    .Width = 750}
            End If
        End Get
    End Property
#End Region
#End Region
#Region "Private Interface"'need to add tolerance list and modify for AllowProgressivePitch
    'Protected Overrides Sub DisplayInitialize()
    '    MyBase.DisplayInitialize()
    'End Sub
    Protected Overrides Sub DataShow()
        If JobDetails Is Nothing OrElse
            TolClass Is Nothing OrElse
        String.IsNullOrEmpty(Basis) Then
            Return
        End If
        If AllowProgressivePitch = True Then
            LabTolClass.Text = kTitle1 + TolClass.ToleranceClass + kTitle2AllowProgressivePitch
        Else
            LabTolClass.Text = kTitle1 + TolClass.ToleranceClass + kTitle2NoProgressivePitch
        End If
        Dim fontfam As New FontFamily("Arial")
        Dim tonf As New Font(fontfam, 12)
        LabTolClass.Font = New Font(fontfam, 18)
        TLayoutBackground.RowCount = BladeCount
        TLayoutBackground.RowStyles.Clear()

        Dim x As Integer
        For x = 1 To BladeCount
            Dim tlayout As TableLayoutPanel '' this tlayout will hold all labels and information about blade x
            If x = 1 Then '' when x is one the table layout panel is created with an additional row to hold Radius Labels
                tlayout = New TableLayoutPanel With {
            .Name = "Blade" + x.ToString(),
            .Dock = DockStyle.Fill,
            .RowCount = LocalPitchSectors + 2,
            .ColumnCount = RadiusMeasurements.Where(Function(r) r.BladeId = 1).ToList().Count + 3,
            .Margin = New Padding(0, 0, 0, 0)}
            Else
                tlayout = New TableLayoutPanel With {
            .Name = "Blade" + x.ToString(),
            .Dock = DockStyle.Fill,
            .RowCount = LocalPitchSectors + 1,
            .ColumnCount = RadiusMeasurements.Where(Function(r) r.BladeId = 1).ToList().Count + 3,
            .Margin = New Padding(0, 0, 0, 0)}
            End If

            'Manage ColumnStyles first two are absolute
            TLayoutBackground.Controls.Add(tlayout, 0, x - 1)
            Dim colsty As New ColumnStyle With {
            .SizeType = SizeType.Absolute,
            .Width = kBladeLabelSize}
            tlayout.ColumnStyles.Add(colsty)
            colsty = New ColumnStyle With {
            .SizeType = SizeType.Absolute,
            .Width = kTELELabelSize}
            tlayout.ColumnStyles.Add(colsty)
            Dim y As Integer
            For y = 2 To tlayout.ColumnCount
                colsty = New ColumnStyle With {
                .Width = kStandardColumnSize,
                .SizeType = SizeType.Percent}
                tlayout.ColumnStyles.Add(colsty)
            Next
            ''manage RowStyles all should be percentage
            If x = 1 Then
                For q = 0 To LocalPitchSectors + 1 'start from 0 to include the avg row
                    Dim rowsty As New RowStyle With {
                    .SizeType = SizeType.Percent,
                    .Height = 100}
                    tlayout.RowStyles.Add(rowsty)
                Next
            Else
                For q = 0 To LocalPitchSectors 'start from 0 to include the avg row
                    Dim rowsty As New RowStyle With {
                    .SizeType = SizeType.Percent,
                    .Height = 100}
                    tlayout.RowStyles.Add(rowsty)
                Next
            End If
            ''' create label denoting blade number and add to table layout panel
            Dim bldlab As New Label With {
            .Name = "LabBlade" + x.ToString(),
            .Dock = DockStyle.Fill,
            .TextAlign = ContentAlignment.MiddleCenter,
            .Text = "Bld " + x.ToString()}
            If x = 1 Then
                tlayout.Controls.Add(bldlab, 0, 1)
            Else
                tlayout.Controls.Add(bldlab, 0, 0)
            End If
            ''' create labels denoting the Leading and Trailing edges and add them to the table layout panel
            Dim telab As New Label With {
            .Name = "LabTE" + x.ToString(),
            .Dock = DockStyle.Fill,
            .TextAlign = ContentAlignment.MiddleCenter,
            .Text = "TE"}
            If x = 1 Then
                tlayout.Controls.Add(telab, 1, 1)
            Else
                tlayout.Controls.Add(telab, 1, 0)
            End If
            If LocalPitchSectors <> 1 Then
                Dim lelab As New Label With {
            .Name = "LabLE" + x.ToString(),
            .Dock = DockStyle.Fill,
            .TextAlign = ContentAlignment.MiddleCenter,
            .Text = "LE"}
                If x = 1 Then
                    tlayout.Controls.Add(lelab, 1, LocalPitchSectors)
                Else
                    tlayout.Controls.Add(lelab, 1, LocalPitchSectors - 1)
                End If
            End If
            ''' add a label for the average row as well
            Dim avglab As New Label With {
            .Name = "avglab" + x.ToString(),
            .Dock = DockStyle.Fill,
            .TextAlign = ContentAlignment.MiddleCenter,
            .Text = "Avg"}
            If x = 1 Then
                tlayout.Controls.Add(avglab, 0, LocalPitchSectors + 1)
            Else
                tlayout.Controls.Add(avglab, 0, LocalPitchSectors)
            End If
            Dim avgbladepitch As Double = 0.0
            y = 2 ''' y is an integer used to place labels in the correct columns in tlayout, it begins at 2 because the first columns are for blade number and  TE/LE
            If AllowProgressivePitch Then
                For Each rm As RadiusMeasurement In RadiusMeasurements.Where(Function(r) r.BladeId = x).ToList()
                    '''loop through radius measurements of the given blade
                    Dim avgpitch As Double = 0
                    If x = 1 Then
                        Dim radlab As New Label With {'''for blade 1(when x = 1) create a label displaying the radius percent of the the Radius Measurement and add it to the table layout panel
                    .Name = "BladeRad" + Math.Round(rm.Radius.Value).ToString(),
                    .TextAlign = ContentAlignment.MiddleLeft,
                    .Dock = DockStyle.Left,
                    .Text = Math.Round(rm.Radius.Value).ToString() + "%"}
                        tlayout.Controls.Add(radlab, y, 0)
                    End If
                    For q = 1 To LocalPitchSectors ''' for each tolerance class sector find the avg pitch and use it for the tolerance check
                        Dim m As Integer = q - 1 '''used to place in correct row functionally equal to q unless on blade 1
                        If x = 1 Then m = q
                        Dim tolpitch As Double = 0.0
                        For Each rad As RadiusMeasurement In RadiusMeasurements.Where(Function(r) Math.Round(r.Radius.Value) = Math.Round(rm.Radius.Value)).ToList()
                            tolpitch += GetLocalPitch(rad.CellMeasurements, LocalPitchSectors, q, PropellerDiameter, rad.Radius, TEExclusion, LEExclusion)
                        Next
                        tolpitch /= BladeCount ''' Get the local pitch of the sector and use the found Progressive pitch to check the tolerance
                        Dim pitch = GetLocalPitch(rm.CellMeasurements, LocalPitchSectors, q, PropellerDiameter, rm.Radius, TEExclusion, LEExclusion)
                        Dim PitchCol = CheckLocalPitchToleranceNoPlot(TolClass, pitch, tolpitch, MinimumsApply)
                        Dim Pitchlab As New Label With {'' create a label with the pitch and color of the tolerance check
                            .Name = "Rad" + Math.Round(rm.Radius.Value).ToString() + (q).ToString(),
                            .Dock = DockStyle.Left,
                            .Text = Math.Round(pitch, 2).ToString("F2"),
                            .TextAlign = ContentAlignment.MiddleLeft,
                            .ForeColor = ToColor(PitchCol)}
                        tlayout.Controls.Add(Pitchlab, y, m)
                    Next
                    Dim tolavgpitch As Double = 0.0 '''for this radius measurement find the average pitch of all similar Radii to use for Tolerance
                    For Each rad As RadiusMeasurement In RadiusMeasurements.Where(Function(r) Math.Round(r.Radius.Value) = Math.Round(rm.Radius.Value)).ToList()
                        tolavgpitch += GetRadiusMeasurementPitch(rm.CellMeasurements, TEExclusion, LEExclusion)
                    Next
                    tolavgpitch /= BladeCount ''' get pitch of Radius measurement, add it to the blade total pitch and check it against the calculated tolerance
                    avgpitch = GetRadiusMeasurementPitch(rm.CellMeasurements, TEExclusion, LEExclusion)
                    avgbladepitch += avgpitch
                    Dim avgPitchCol = CheckBladeRadiusPitch(TolClass, avgpitch, tolavgpitch, MinimumsApply)
                    Dim avgpitchlab As New Label With {' add new label with Radii Pitch with color
                        .Name = "avgpitch" + x.ToString() + Math.Round(rm.Radius.Value).ToString(),
                        .Dock = DockStyle.Fill,
                        .Text = Math.Round(avgpitch, 3).ToString("F3"),
                        .TextAlign = ContentAlignment.MiddleLeft,
                        .ForeColor = ToColor(avgPitchCol),
                        .Margin = New Padding(20, 0, 0, 0)}
                    tlayout.Controls.Add(avgpitchlab, y, tlayout.RowCount - 1)
                    y += 1 ''' increase y to move to next column for the next Radii measurment in the loop
                Next
                Dim avgbladepitchtol As Double = 0.0
                Dim f As Integer
                For f = 1 To BladeCount ''' Find the Progressive pitch of all blade pitches
                    Dim avgradpitch As Double = 0.0
                    For Each rad As RadiusMeasurement In RadiusMeasurements.Where(Function(r) r.BladeId = f).ToList()
                        avgradpitch += GetRadiusMeasurementPitch(rad.CellMeasurements, TEExclusion, LEExclusion)
                    Next
                    avgbladepitchtol += (avgradpitch / RadiusMeasurements.Where(Function(r) r.BladeId = f).ToList().Count)
                Next ''' get blade average pitch and check it's tolerance
                avgbladepitch /= RadiusMeasurements.Where(Function(r) r.BladeId = x).ToList().Count
                Dim avgbladecol = CheckBladePitch(TolClass, avgbladepitch, avgbladepitchtol, MinimumsApply)
                Dim avgbladelab As New Label With { '' add blade pitch label with color
                .Name = "AvgBlade" + x.ToString(),
                .Dock = DockStyle.Fill,
                .TextAlign = ContentAlignment.MiddleLeft,
                .Text = Math.Round(avgbladepitch, 3).ToString("F3"),
                .ForeColor = ToColor(avgbladecol)}
                tlayout.Controls.Add(avgbladelab, tlayout.ColumnCount - 1, tlayout.RowCount - 1)
                tlayout.Height = tlayout.RowCount * 25
            Else
                For Each rm As RadiusMeasurement In RadiusMeasurements.Where(Function(r) r.BladeId = x).ToList()
                    ''' loop through the radius measurements of the given blade
                    Dim avgpitch As Double = 0
                    If x = 1 Then '' for blade 1(x = 1) add a label in the first row stating the Radius percent of that measurement
                        Dim radlab As New Label With {
                        .Name = "BladeRad" + Math.Round(rm.Radius.Value).ToString(),
                        .TextAlign = ContentAlignment.MiddleLeft,
                        .Dock = DockStyle.Left,
                        .Text = Math.Round(rm.Radius.Value).ToString("F2") + "%"}
                        tlayout.Controls.Add(radlab, y, 0)
                    End If
                    For q = 1 To LocalPitchSectors
                        Dim m As Integer = q - 1 '''used to place in correct row functionally equal to q unless on blade 1
                        If x = 1 Then m = q
                        ''' loop through each local pitch sector
                        ''' get local pitch of the sector and check it against the basis pitch
                        Dim pitch = GetLocalPitch(rm.CellMeasurements, TolClass.LocalPitchSectors, q, PropellerDiameter, rm.Radius, TEExclusion, LEExclusion)
                        Dim PitchCol = CheckLocalPitchToleranceNoPlot(TolClass, pitch, BasisPitch, MinimumsApply)
                        Dim Pitchlab As New Label With {''' create a label with the pitch and tolerance check color
                            .Name = "Rad" + Math.Round(rm.Radius.Value).ToString() + (q).ToString(),
                            .Dock = DockStyle.Fill,
                            .Text = Math.Round(pitch, 2).ToString("F2"),
                            .TextAlign = ContentAlignment.MiddleLeft,
                            .ForeColor = ToColor(PitchCol)}
                        tlayout.Controls.Add(Pitchlab, y, m)
                    Next
                    '''get the Radius Measurements average pitch and check it against the basispitch
                    avgpitch = GetRadiusMeasurementPitch(rm.CellMeasurements, JobDetails.Job.TeExclusion, JobDetails.Job.LeExclusion)
                    avgbladepitch += avgpitch
                    Dim avgPitchCol = CheckBladeRadiusPitch(TolClass, avgpitch, BasisPitch, MinimumsApply)
                    Dim avgpitchlab As New Label With {''' create a label with the pitch and tolerance check color
                        .Name = "avgpitch" + x.ToString() + Math.Round(rm.Radius.Value).ToString(),
                        .Dock = DockStyle.Fill,
                        .Text = Math.Round(avgpitch, 3).ToString("F3"),
                        .TextAlign = ContentAlignment.MiddleLeft,
                        .ForeColor = ToColor(avgPitchCol),
                        .Margin = New Padding(20, 0, 0, 0)}
                    tlayout.Controls.Add(avgpitchlab, y, tlayout.RowCount - 1)
                    y += 1
                Next
                ''' calculate the average blade pitch and check it against the basis pitch
                avgbladepitch /= RadiusMeasurements.Where(Function(r) r.BladeId = x).ToList().Count
                Dim avgbladecol = CheckBladePitch(TolClass, avgbladepitch, BasisPitch, MinimumsApply)
                Dim avgbladelab As New Label With {''' create a label with the blade pitch and tolerance check color
                .Name = "AvgBlade" + x.ToString(),
                .Dock = DockStyle.Fill,
                .TextAlign = ContentAlignment.MiddleLeft,
                .Text = Math.Round(avgbladepitch, 3).ToString("F3"),
                .ForeColor = ToColor(avgbladecol)}
                tlayout.Controls.Add(avgbladelab, tlayout.ColumnCount - 1, tlayout.RowCount - 1)
                tlayout.Height = tlayout.RowCount * 25
            End If
            Dim rowstyl As New RowStyle With {''' adjust the latest row style to the needed height
                .SizeType = SizeType.Absolute,
                .Height = tlayout.Height}
            TLayoutBackground.RowStyles.Add(rowstyl)
        Next
        LabWheelPitch.Text = JobDetails.WheelPitch.Value.ToString("F3")
        If AllowProgressivePitch = True Then
            TLayoutTolerances.Visible = False ''' If Progressive pitch is allowed the Tolerance labels can't be displayed with accurate information so they are disabled
        Else
            ''' using the given tolerance class and the Basis Pitch adjust the Tolerance labels to display the correct values
            Dim pitTol As Double = BasisPitch.Value * (TolClass.LocalPitchPercent / 100)
            If (pitTol * Constants.kInchToMm) < TolClass.LocalPitchMinimum And MinimumsApply = True Then '' check for minimums Apply
                pitTol = TolClass.LocalPitchMinimum * Constants.kMmToInch
            End If
            LabLPHiLimit.Text = (BasisPitch.Value + pitTol).ToString("F3")
            LabLPLoLimit.Text = (BasisPitch.Value - pitTol).ToString("F3")
            pitTol = BasisPitch.Value * (TolClass.MeanPitchPerRadiusPercent / 100)
            If (pitTol * Constants.kInchToMm) < TolClass.MeanPitchPerRadiusMinimum And MinimumsApply = True Then '' check for minimums Apply
                pitTol = TolClass.MeanPitchPerRadiusMinimum * Constants.kMmToInch
            End If
            LabRadiusHiLimit.Text = (BasisPitch.Value + pitTol).ToString("F3")
            LabRadiusLoLimit.Text = (BasisPitch.Value - pitTol).ToString("F3")
            pitTol = BasisPitch.Value * (TolClass.MeanPitchPerBladePercent / 100)
            If (pitTol * Constants.kInchToMm) < TolClass.MeanPitchPerBladeMinimum And MinimumsApply = True Then '' check for minimums Apply
                pitTol = TolClass.MeanPitchPerBladeMinimum * Constants.kMmToInch
            End If
            LabBladeHiLimit.Text = (BasisPitch.Value + pitTol).ToString("F3")
            LabBladeLoLimit.Text = (BasisPitch.Value - pitTol).ToString("F3")
            pitTol = BasisPitch.Value * (TolClass.MeanPitchForPropellerPercent / 100)
            If (pitTol * Constants.kInchToMm) < TolClass.MeanPitchForPropellerMinimum And MinimumsApply = True Then '' check for minimums Apply
                pitTol = TolClass.MeanPitchForPropellerMinimum * Constants.kMmToInch
            End If
            LabWheelHiLimit.Text = (BasisPitch.Value + pitTol).ToString("F3")
            LabWheelLoLimit.Text = (BasisPitch.Value - pitTol).ToString("F3")
        End If
    End Sub
#End Region
End Class
