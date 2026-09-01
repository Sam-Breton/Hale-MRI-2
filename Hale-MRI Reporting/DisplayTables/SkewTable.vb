Imports LibDatabase.Models

Public Class SkewTable
    Inherits DisplayControl
#Region "Constructors"
    Public Sub New()
        MyBase.New()
        InitializeComponent()
    End Sub
#End Region
#Region "Client Properties"
    ''' <summary>
    ''' JobDetail containing data used for table
    ''' </summary>
    ''' <returns></returns>
    ''' USE STANDARD NAMING CONVENTIONS USED THROUGHOUT THE APP, this property is always declared as JobDetails.
    'Public Property MJobDetails As JobDetail
    ''' THE BASE CLASS 'DATA' ALREADY PROPERTY HOLDS THESE RECORDS.
    'Public Property JobDetails As JobDetail
    ''' IF YOU NEED TO EXPLICITLY ACCESS THIS, THEN DEFINE AS READONLY PROPERTY AND CAST ACCORDINGLY.
    Public ReadOnly Property JobDetails As JobDetail
        Get
            Return DirectCast(Me.Data, JobDetail)
        End Get
    End Property
    ''' <summary>
    ''' Reference Radius used for calculating skew angle.
    ''' </summary>
    ''' <returns></returns>
    Public Property RefRadius As Double
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
            'BasisSet(value, Me.BasisPitch, Me.Radius)
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
            'TolClassSet(value)
            DataShow()
        End Set
    End Property
#End Region
#Region "Computed Properties"
    Public ReadOnly Property Blades As Integer
        Get
            Return JobDetails.Job.PropellerBlades
        End Get
    End Property
    ' USE CAMELCASE AND DESCRIPTIVE PROPERTY NAMES.
    Public ReadOnly Property TEE As Double
        Get
            Return JobDetails.Job.TeExclusion
        End Get
    End Property
    ' USE CAMELCASE AND DESCRIPTIVE PROPERTY NAMES.
    Public ReadOnly Property LEE As Double
        Get
            Return JobDetails.Job.LeExclusion
        End Get
    End Property
    Public ReadOnly Property Diameter As Double
        Get
            Return JobDetails.Job.PropellerDiameter
        End Get
    End Property
    Public ReadOnly Property Rads As List(Of RadiusMeasurement)
        Get
            Return JobDetails.RadiusMeasurements
        End Get
    End Property
    Public ReadOnly Property RefSkews As List(Of Double)
        Get
            Dim skew As New List(Of Double)
            For Each rm As RadiusMeasurement In Rads.Where(Function(r) Math.Round(r.Radius.Value) = RefRadius).ToList()
                skew.Add(GetChordMidAngle(rm.CellMeasurements))
            Next
            Return skew
        End Get
    End Property
#End Region
#Region "Private Interface"
    Protected Overrides Sub DataShow()
        ' Remove magic numbers and strings, and define as constants.
        ' Add comments explaining what the code does and why.
        If JobDetails Is Nothing Then Exit Sub
        FormatTables()
        With tlayoutSkewReal
            Dim I As Integer
            Dim lab As Label
            For I = 1 To Blades
                Dim meas As List(Of RadiusMeasurement) = Rads.Where(Function(r) r.BladeId = I).ToList()
                For Each rm As RadiusMeasurement In meas
                    If Math.Round(rm.Radius.Value) = RefRadius Then
                        lab = New Label With {
                            .Text = "Ref.",
                            .Dock = DockStyle.Fill,
                            .TextAlign = ContentAlignment.MiddleCenter,
                            .Name = "Lab" + Math.Round(rm.Radius.Value).ToString() + I.ToString()}
                        .Controls.Add(lab, I, meas.IndexOf(rm) + 1)
                    Else
                        '''get rm mid angle compare to ref find percentage of blade get chordlength return percentage of chordlength
                        Dim midangle As Double = GetChordMidAngle(rm.CellMeasurements)
                        Dim skewangle As Double = midangle - RefSkews(I - 1)
                        Dim totangle = If(rm.CellMeasurements.LastOrDefault().Angle < 0, (rm.CellMeasurements.FirstOrDefault().Angle + 360) - (rm.CellMeasurements.LastOrDefault().Angle + 360), rm.CellMeasurements.FirstOrDefault().Angle - rm.CellMeasurements.LastOrDefault().Angle)
                        Dim percentskew As Double = skewangle / totangle
                        Dim cl As Double = GetChordLength(rm.CellMeasurements, Diameter, Math.Round(rm.Radius.Value))
                        Dim perccl As Double = cl * percentskew
                        Dim str As String = $"{Math.Round(skewangle, 2)}° / {Math.Round(perccl, 2)} In" '''replace with system string when added
                        lab = New Label With {
                            .Text = str,
                            .Dock = DockStyle.Fill,
                            .TextAlign = ContentAlignment.MiddleCenter,
                            .Name = $"Lab{Math.Round(rm.Radius.Value)}{I}"}
                        .Controls.Add(lab, I, meas.IndexOf(rm) + 1)
                    End If
                Next
            Next
        End With
        MyBase.DataShow()
    End Sub
    Public Sub FormatTables()
        With tlayoutSkewReal
            .ColumnCount = Blades + 1
            .ColumnStyles.Clear()
            Dim lab As Label
            ' Please use lowercase letters for loop vars.
            For I As Integer = 0 To .ColumnCount - 1
                ' This all needs to be done in DisplayInitialize() not here.
                ' No Clear() or Add() commands, with the exception of Series
                ' can appear here because font scaling will not work. Only
                ' update the data, don't recreate Charts, Label, TextBoxes
                ' or anything here.
                .ColumnStyles.Add(New ColumnStyle(SizeType.AutoSize))
                If I = 0 Then
                    lab = New Label With {
                        .Text = "r/R",
                        .Dock = DockStyle.Fill,
                        .TextAlign = ContentAlignment.MiddleCenter,
                        .Name = "r/R"}
                    .Controls.Add(lab, 0, 0)
                Else
                    lab = New Label With {
                        .Text = "Blade " + I.ToString(),
                        .Dock = DockStyle.Fill,
                        .TextAlign = ContentAlignment.MiddleCenter,
                        .Name = "LabBlade" + I.ToString()}
                    .Controls.Add(lab, I, 0)
                End If
            Next
            Dim meas As List(Of RadiusMeasurement) = Rads.Where(Function(r) r.BladeId = 1).ToList()
            For Each rm As RadiusMeasurement In meas
                lab = New Label With {
                    .Text = Math.Round(rm.Radius.Value).ToString() + "%",
                    .Dock = DockStyle.Fill,
                    .TextAlign = ContentAlignment.MiddleCenter,
                    .Name = "LabRadius" + Math.Round(rm.Radius.Value).ToString()}
                .Controls.Add(lab, 0, meas.IndexOf(rm) + 1)
            Next
        End With
    End Sub
#End Region
End Class
