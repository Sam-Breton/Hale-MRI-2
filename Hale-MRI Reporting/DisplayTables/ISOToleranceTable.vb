Imports System.Globalization
Imports System.Threading.Tasks.Dataflow
Imports LibDatabase.Models

Public Class ISOToleranceTable
    Inherits DisplayControl
#Region "Constants"
    Private Const kTableTitle = "ISO Tolerance Class "
    Private Const kPlusMinus = "+/-"
    Private Const kLabelMinName = "LabMin"
    Private Const kLabelDiffName = "LabDiff"
    Private Const kLabelHighLowName = "LabHighLow"
#End Region
#Region "Constructors"
    ''' <summary>
    ''' Default constructor.
    ''' </summary>
    Public Sub New()
        MyBase.New()
        InitializeComponent()
        Me.ContextMenuStrip = ContextMenuStrip1
    End Sub
#End Region
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
            DisplayInitialize()
            DataShow()
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
            DisplayInitialize()
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
            DataShow()
        End Set
    End Property
#End Region
#Region "Computed Properties"
    Private ReadOnly Property JobDetails As JobDetail
        Get
            Return CType(Data, JobDetail)
        End Get
    End Property
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
#End Region
#Region "Private Interface"
    Protected Overrides Sub ContextMenuStripSet()
        If Me.ContextMenuStrip?.Enabled = False Or Me.ContextMenuStrip Is Nothing Then
            Me.ContextMenuStrip = Nothing
            Return
        End If
        ''' this control has one Item in the Context menu which controls MinimumsApply as a simple boolean value. Therefore it doesn't need more setup
        MyBase.ContextMenuStripSet()
    End Sub
    Protected Overrides Sub DataShow()
        If TolClass Is Nothing Or Basis Is Nothing Or JobDetails Is Nothing Then Return
        With TLayoutISOTol
            ' revisit when implementing system so as to handle In and mm unit tags at the end of strings
            LabTitle.Text = $"{kTableTitle}{TolClass.ToleranceClass},  Basis = {BasisPitch} In.  " + If(MinimumsApply, "Mins Apply", "Mins do not apply.")
            ''' this sub is in essence a written out for loop through each member of a tolerance class,
            ''' too much changes between each member to realistically make it a real for loop
            If TolClass.ToleranceClass = "III" Then
                Dim Lab As Label '''create label reference
                ''' Class III doesn't have a Local Pitch Tolerance so it is skipped

                ''' Mean Pitch per Radius 
                Dim pit As Double = BasisPitch.Value * TolClass.MeanPitchPerRadiusPercent / 100 ''' calculate difference in pitch based on Tolerance Class
                Dim min As Boolean = False
                If MinimumsApply Then ''' If minimums apply check if tolerance is within or over the minimum threshold and edit the value accordingly
                    If pit * Constants.kInchToMm < TolClass.MeanPitchPerRadiusMinimum Then
                        pit = TolClass.MeanPitchPerRadiusMinimum * Constants.kMmToInch
                        min = True
                    End If
                End If
                Lab = New Label With {''' if within minimum threshold print a label with Min if not input the tolerance percentage, This is in the second column
                    .Text = If(min, "Min", TolClass.MeanPitchPerRadiusPercent.ToString() + "%"),
                    .Dock = DockStyle.Fill,
                    .TextAlign = ContentAlignment.MiddleCenter,
                    .Name = kLabelMinName + "1"}
                If Not .Controls.ContainsKey(kLabelMinName + "1") Then
                    .Controls.Add(Lab, 1, 0)
                End If
                Lab = New Label With {''' using calculated pitch print a label with +/- in front to show the allowed tolerance, column 3
                    .Text = kPlusMinus + pit.ToString("0.00"),
                    .Dock = DockStyle.Fill,
                    .TextAlign = ContentAlignment.MiddleCenter,
                    .Name = kLabelDiffName + "1"}
                If Not .Controls.ContainsKey(kLabelDiffName + "1") Then
                    .Controls.Add(Lab, 2, 0)
                End If
                '''using the calculated pitch create a string denoting the highest allowed pitch and the lowest allowed pitch
                Dim highlow As String = (BasisPitch.Value + pit).ToString("0.00") + " / " + (BasisPitch.Value - pit).ToString("0.00")
                Lab = New Label With {''' create a label with the new string, column 4
                    .Text = highlow,
                    .Dock = DockStyle.Fill,
                    .TextAlign = ContentAlignment.MiddleCenter,
                    .Name = kLabelHighLowName + "1"}
                If Not .Controls.ContainsKey(kLabelHighLowName + "1") Then
                    .Controls.Add(Lab, 3, 0)
                End If

                ''' Mean Pitch per Blade
                pit = BasisPitch.Value * TolClass.MeanPitchPerBladePercent / 100 ''' calculate difference in pitch based on Tolerance Class
                min = False
                    If MinimumsApply Then ''' If minimums apply check if tolerance is within or over the minimum threshold and edit the value accordingly
                        If pit * Constants.kInchToMm < TolClass.MeanPitchPerBladeMinimum Then
                            pit = TolClass.MeanPitchPerBladeMinimum * Constants.kMmToInch
                            min = True
                        End If
                    End If
                Lab = New Label With {''' if within minimum threshold print a label with Min if not input the tolerance percentage, This is in the second column
                    .Text = If(min, "Min", TolClass.MeanPitchPerBladePercent.ToString() + "%"),
                    .Dock = DockStyle.Fill,
                    .TextAlign = ContentAlignment.MiddleCenter,
                    .Name = kLabelMinName + "2"}
                If Not .Controls.ContainsKey(kLabelMinName + "2") Then
                    .Controls.Add(Lab, 1, 1)
                End If
                Lab = New Label With {''' using calculated pitch print a label with +/- in front to show the allowed tolerance, column 3
                    .Text = kPlusMinus + pit.ToString("0.00"),
                    .Dock = DockStyle.Fill,
                    .TextAlign = ContentAlignment.MiddleCenter,
                    .Name = kLabelDiffName + "2"}
                If Not .Controls.ContainsKey(kLabelDiffName + "2") Then
                    .Controls.Add(Lab, 2, 1)
                End If
                '''using the calculated pitch create a string denoting the highest allowed pitch and the lowest allowed pitch
                highlow = (BasisPitch.Value + pit).ToString("0.00") + " / " + (BasisPitch.Value - pit).ToString("0.00")
                Lab = New Label With {
                    .Text = highlow,
                    .Dock = DockStyle.Fill,
                    .TextAlign = ContentAlignment.MiddleCenter,
                    .Name = kLabelHighLowName + "2"}
                If Not .Controls.ContainsKey(kLabelHighLowName + "2") Then
                    .Controls.Add(Lab, 3, 1)
                End If

                '''Mean Pitch of Propeller/Wheel Pitch
                pit = BasisPitch.Value * TolClass.MeanPitchForPropellerPercent / 100 ''' calculate difference in pitch based on Tolerance Class
                min = False
                    If MinimumsApply Then ''' If minimums apply check if tolerance is within or over the minimum threshold and edit the value accordingly
                        If pit * Constants.kInchToMm < TolClass.MeanPitchForPropellerMinimum Then
                            pit = TolClass.MeanPitchForPropellerMinimum * Constants.kMmToInch
                            min = True
                        End If
                    End If
                Lab = New Label With {''' if within minimum threshold print a label with Min if not input the tolerance percentage, This is in the second column
                    .Text = If(min, "Min", TolClass.MeanPitchForPropellerPercent.ToString() + "%"),
                    .Dock = DockStyle.Fill,
                    .TextAlign = ContentAlignment.MiddleCenter,
                    .Name = kLabelMinName + "3"}
                If Not .Controls.ContainsKey(kLabelMinName + "3") Then
                    .Controls.Add(Lab, 1, 2)
                End If
                Lab = New Label With {''' using calculated pitch print a label with +/- in front to show the allowed tolerance, column 3
                    .Text = kPlusMinus + pit.ToString("0.00"),
                    .Dock = DockStyle.Fill,
                    .TextAlign = ContentAlignment.MiddleCenter,
                    .Name = kLabelDiffName + "3"}
                If Not .Controls.ContainsKey(kLabelDiffName + "3") Then
                    .Controls.Add(Lab, 2, 2)
                End If
                '''using the calculated pitch create a string denoting the highest allowed pitch and the lowest allowed pitch
                highlow = (BasisPitch.Value + pit).ToString("0.00") + " / " + (BasisPitch.Value - pit).ToString("0.00")
                Lab = New Label With {
                    .Text = highlow,
                    .Dock = DockStyle.Fill,
                    .TextAlign = ContentAlignment.MiddleCenter,
                    .Name = kLabelHighLowName + "3"}
                If Not .Controls.ContainsKey(kLabelHighLowName + "3") Then
                    .Controls.Add(Lab, 3, 2)
                End If
            Else
                    Dim Lab As Label
                ''' Local Pitch
                Dim pit As Double = BasisPitch.Value * TolClass.LocalPitchPercent / 100 ''' calculate difference in pitch based on Tolerance Class
                Dim min As Boolean = False
                If MinimumsApply Then ''' If minimums apply check if tolerance is within or over the minimum threshold and edit the value accordingly
                    If pit * Constants.kInchToMm < TolClass.LocalPitchMinimum Then
                        pit = TolClass.LocalPitchMinimum * Constants.kMmToInch
                        min = True
                    End If
                End If
                Lab = New Label With {''' if within minimum threshold print a label with Min if not input the tolerance percentage, This is in the second column
                    .Text = If(min, "Min", TolClass.LocalPitchPercent.ToString() + "%"),
                    .Dock = DockStyle.Fill,
                    .TextAlign = ContentAlignment.MiddleCenter,
                    .Name = kLabelMinName + "1"}
                If Not .Controls.ContainsKey(kLabelMinName + "1") Then
                    .Controls.Add(Lab, 1, 0)
                End If
                Lab = New Label With {''' using calculated pitch print a label with +/- in front to show the allowed tolerance, column 3
                    .Text = kPlusMinus + pit.ToString("0.00"),
                    .Dock = DockStyle.Fill,
                    .TextAlign = ContentAlignment.MiddleCenter,
                    .Name = kLabelDiffName + "1"}
                If Not .Controls.ContainsKey(kLabelDiffName + "1") Then
                    .Controls.Add(Lab, 2, 0)
                End If
                '''using the calculated pitch create a string denoting the highest allowed pitch and the lowest allowed pitch
                Dim highlow As String = (BasisPitch.Value + pit).ToString("F2") + " / " + (BasisPitch.Value - pit).ToString("F2")
                Lab = New Label With {
                    .Text = highlow,
                    .Dock = DockStyle.Fill,
                    .TextAlign = ContentAlignment.MiddleCenter,
                    .Name = kLabelHighLowName + "1"}
                If Not .Controls.ContainsKey(kLabelHighLowName + "1") Then
                    .Controls.Add(Lab, 3, 0)
                End If

                '''Mean Pitch for Radius Segment
                pit = BasisPitch.Value * TolClass.MeanPitchPerRadiusPercent / 100 ''' calculate difference in pitch based on Tolerance Class
                min = False
                If MinimumsApply Then ''' If minimums apply check if tolerance is within or over the minimum threshold and edit the value accordingly
                    If pit * Constants.kInchToMm < TolClass.MeanPitchPerRadiusMinimum Then
                        pit = TolClass.MeanPitchPerRadiusMinimum * Constants.kMmToInch
                        min = True
                    End If
                End If
                Lab = New Label With {''' if within minimum threshold print a label with Min if not input the tolerance percentage, This is in the second column
                    .Text = If(min, "Min", TolClass.MeanPitchPerRadiusPercent.ToString() + "%"),
                    .Dock = DockStyle.Fill,
                    .TextAlign = ContentAlignment.MiddleCenter,
                    .Name = kLabelMinName + "2"}
                If Not .Controls.ContainsKey(kLabelMinName + "2") Then
                    .Controls.Add(Lab, 1, 1)
                End If
                Lab = New Label With {''' using calculated pitch print a label with +/- in front to show the allowed tolerance, column 3
                    .Text = kPlusMinus + pit.ToString("0.00"),
                    .Dock = DockStyle.Fill,
                    .TextAlign = ContentAlignment.MiddleCenter,
                    .Name = kLabelDiffName + "2"}
                If Not .Controls.ContainsKey(kLabelDiffName + "2") Then
                    .Controls.Add(Lab, 2, 1)
                End If
                '''using the calculated pitch create a string denoting the highest allowed pitch and the lowest allowed pitch
                highlow = (BasisPitch.Value + pit).ToString("0.00") + " / " + (BasisPitch.Value - pit).ToString("0.00")
                Lab = New Label With {
                    .Text = highlow,
                    .Dock = DockStyle.Fill,
                    .TextAlign = ContentAlignment.MiddleCenter,
                    .Name = kLabelHighLowName + "2"}
                If Not .Controls.ContainsKey(kLabelHighLowName + "2") Then
                    .Controls.Add(Lab, 3, 1)
                End If
                min = False

                ''' Mean Pitch per blade
                pit = BasisPitch.Value * TolClass.MeanPitchPerBladePercent / 100 ''' calculate difference in pitch based on Tolerance Class
                If MinimumsApply Then ''' If minimums apply check if tolerance is within or over the minimum threshold and edit the value accordingly
                    If pit * Constants.kInchToMm < TolClass.MeanPitchPerBladeMinimum Then
                        pit = TolClass.MeanPitchPerBladeMinimum * Constants.kMmToInch
                        min = True
                    End If
                End If
                Lab = New Label With {''' if within minimum threshold print a label with Min if not input the tolerance percentage, This is in the second column
                    .Text = If(min, "Min", TolClass.MeanPitchPerBladePercent.ToString() + "%"),
                    .Dock = DockStyle.Fill,
                    .TextAlign = ContentAlignment.MiddleCenter,
                    .Name = kLabelMinName + "3"}
                If Not .Controls.ContainsKey(kLabelMinName + "3") Then
                    .Controls.Add(Lab, 1, 2)
                End If
                Lab = New Label With {''' using calculated pitch print a label with +/- in front to show the allowed tolerance, column 3
                    .Text = kPlusMinus + pit.ToString("0.00"),
                    .Dock = DockStyle.Fill,
                    .TextAlign = ContentAlignment.MiddleCenter,
                    .Name = kLabelDiffName + "3"}
                If Not .Controls.ContainsKey(kLabelDiffName + "3") Then
                    .Controls.Add(Lab, 2, 2)
                End If
                '''using the calculated pitch create a string denoting the highest allowed pitch and the lowest allowed pitch
                highlow = (BasisPitch.Value + pit).ToString("0.00") + " / " + (BasisPitch.Value - pit).ToString("0.00")
                Lab = New Label With {
                    .Text = highlow,
                    .Dock = DockStyle.Fill,
                    .TextAlign = ContentAlignment.MiddleCenter,
                    .Name = kLabelHighLowName + "3"}
                If Not .Controls.ContainsKey(kLabelHighLowName + "3") Then
                    .Controls.Add(Lab, 3, 2)
                End If
                min = False

                '''Mean Pitch for Propeller/Wheel Pitch
                pit = BasisPitch.Value * TolClass.MeanPitchForPropellerPercent / 100 ''' calculate difference in pitch based on Tolerance Class
                If MinimumsApply Then ''' If minimums apply check if tolerance is within or over the minimum threshold and edit the value accordingly
                    If pit * Constants.kInchToMm < TolClass.MeanPitchForPropellerMinimum Then
                        pit = TolClass.MeanPitchForPropellerMinimum * Constants.kMmToInch
                        min = True
                    End If
                End If
                Lab = New Label With {''' if within minimum threshold print a label with Min if not input the tolerance percentage, This is in the second column
                    .Text = If(min, "Min", TolClass.MeanPitchForPropellerPercent.ToString() + "%"),
                    .Dock = DockStyle.Fill,
                    .TextAlign = ContentAlignment.MiddleCenter,
                    .Name = kLabelMinName + "4"}
                If Not .Controls.ContainsKey(kLabelMinName + "4") Then
                    .Controls.Add(Lab, 1, 3)
                End If
                Lab = New Label With {''' using calculated pitch print a label with +/- in front to show the allowed tolerance, column 3
                    .Text = kPlusMinus + pit.ToString("0.00"),
                    .Dock = DockStyle.Fill,
                    .TextAlign = ContentAlignment.MiddleCenter,
                    .Name = kLabelDiffName + "4"}
                If Not .Controls.ContainsKey(kLabelDiffName + "4") Then
                    .Controls.Add(Lab, 2, 3)
                End If
                '''using the calculated pitch create a string denoting the highest allowed pitch and the lowest allowed pitch
                highlow = (BasisPitch.Value + pit).ToString("0.00") + " / " + (BasisPitch.Value - pit).ToString("0.00")
                Lab = New Label With {
                    .Text = highlow,
                    .Dock = DockStyle.Fill,
                    .TextAlign = ContentAlignment.MiddleCenter,
                    .Name = kLabelHighLowName + "4"}
                If Not .Controls.ContainsKey(kLabelHighLowName + "4") Then
                    .Controls.Add(Lab, 3, 3)
                End If
            End If
        End With
    End Sub
    Protected Overrides Sub DisplayInitialize()
        ' Instantiate and format all visual elements once, in DisplayIntialize()
        ' Remove magic numbers and strings, and define as constants.
        ' Add comments explaining whatg the code does and why.
        If TolClass Is Nothing Then Return
        With TLayoutISOTol
            .Controls.Clear()
            .ColumnCount = 4
            .ColumnStyles.Clear()
            .ColumnStyles.Add(New ColumnStyle(SizeType.AutoSize))
            .ColumnStyles.Add(New ColumnStyle(SizeType.AutoSize))
            .ColumnStyles.Add(New ColumnStyle(SizeType.AutoSize))
            .ColumnStyles.Add(New ColumnStyle(SizeType.AutoSize))
            If TolClass.ToleranceClass = "III" Then
                .RowCount = 3
                .RowStyles.Clear()
                .RowStyles.Add(New RowStyle(SizeType.AutoSize))
                .RowStyles.Add(New RowStyle(SizeType.AutoSize))
                .RowStyles.Add(New RowStyle(SizeType.AutoSize))

                Dim Lab As New Label With {
                    .Text = "Radius Average",
                    .Dock = DockStyle.Fill,
                    .TextAlign = ContentAlignment.MiddleCenter,
                    .Name = "LabRA"}
                .Controls.Add(Lab, 0, 0)
                Lab = New Label With {
                    .Text = "Blade Average",
                    .Dock = DockStyle.Fill,
                    .TextAlign = ContentAlignment.MiddleCenter,
                    .Name = "LabBA"}
                .Controls.Add(Lab, 0, 1)
                Lab = New Label With {
                    .Text = "Propeller Average",
                    .Dock = DockStyle.Fill,
                    .TextAlign = ContentAlignment.MiddleCenter,
                    .Name = "LabPA"}
                .Controls.Add(Lab, 0, 2)
            Else
                .RowCount = 4
                .RowStyles.Clear()
                .RowStyles.Add(New RowStyle(SizeType.AutoSize))
                .RowStyles.Add(New RowStyle(SizeType.AutoSize))
                .RowStyles.Add(New RowStyle(SizeType.AutoSize))
                .RowStyles.Add(New RowStyle(SizeType.AutoSize))
                Dim Lab As New Label With {
                    .Text = "Local Pitch",
                    .Dock = DockStyle.Fill,
                    .TextAlign = ContentAlignment.MiddleCenter,
                    .Name = "LabLP"}
                .Controls.Add(Lab, 0, 0)
                Lab = New Label With {
                    .Text = "Radius Average",
                    .Dock = DockStyle.Fill,
                    .TextAlign = ContentAlignment.MiddleCenter,
                    .Name = "LabRA"}
                .Controls.Add(Lab, 0, 1)
                Lab = New Label With {
                    .Text = "Blade Average",
                    .Dock = DockStyle.Fill,
                    .TextAlign = ContentAlignment.MiddleCenter,
                    .Name = "LabBA"}
                .Controls.Add(Lab, 0, 2)
                Lab = New Label With {
                    .Text = "Propeller Average",
                    .Dock = DockStyle.Fill,
                    .TextAlign = ContentAlignment.MiddleCenter,
                    .Name = "LabPA"}
                .Controls.Add(Lab, 0, 3)
            End If
        End With
        MyBase.DisplayInitialize()
    End Sub
    Private Sub MinimumsApplyChanged(tsm As ToolStripMenuItem)
        MinimumsApply = tsm.Checked
        DataShow()
    End Sub
#End Region
#Region "Event Handlers"
    Private Sub MinimumsApplyChecked_Changed(sender As Object, e As EventArgs) Handles MinimumsApplyToolStripMenuItem.CheckedChanged
        MinimumsApplyChanged(DirectCast(sender, ToolStripMenuItem))
    End Sub
#End Region
End Class
