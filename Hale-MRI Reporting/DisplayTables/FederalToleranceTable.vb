Imports System.Windows.Forms
Imports LibDatabase.Models

Public Class FederalToleranceTable
    Inherits DisplayControl
#Region "Types and Constants"
    Private Const kTableTitle = "Federal Tolerance Table "
    Private Const kPlusMinus = "+/-"
    Private Const kLabelMinName = "LabMin"
    Private Const kLabelDiffName = "LabDiff"
    Private Const kLabelHighLowName = "LabHighLow"
    Private Const kLabelSlash = " / "
#End Region
#Region "Constructors"
    ''' <summary>
    ''' Default constructor.
    ''' </summary>
    Public Sub New()
        MyBase.New()
        InitializeComponent()
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
            BasisSet(value)
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
    Private ReadOnly Property Diameter As Double
        Get
            Return JobDetails?.Job?.PropellerDiameter
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
    Private Sub BasisSet(value As String)
        If Basis Is Nothing Then
            Basis = "Marked"
        End If
    End Sub
    Protected Overrides Sub DisplayInitialize()
        MyBase.DisplayInitialize() '' no further code needed here
    End Sub
    Protected Overrides Sub DataShow()
        If JobDetails Is Nothing Or Basis Is Nothing Then Return
        LabTitle.Text = kTableTitle + $" Basis = {BasisPitch.Value.ToString(Prec)}   Diameter = {Diameter}"
        Dim pit As Double
        Dim rad As Double = Diameter / 2
        pit = rad * 0.003
        LabRadDiff.Text = $"{kPlusMinus} {pit.ToString(Prec)}"
        LabRadHighLow.Text = $"{(rad + pit).ToString(Prec)}{kLabelSlash}{(rad - pit).ToString(Prec)}"
        pit = BasisPitch.Value * 0.02
        LabLPDiff.Text = $"{kPlusMinus} {pit.ToString(Prec)}"
        LabLPHighLow.Text = $"{(BasisPitch.Value + pit).ToString(Prec)}{kLabelSlash}{(BasisPitch.Value - pit).ToString(Prec)}"
        pit = BasisPitch.Value * 0.015
        LabSPDiff.Text = $"{kPlusMinus} {pit.ToString(Prec)}"
        LabSPHighLow.Text = $"{(BasisPitch.Value + pit).ToString(Prec)}{kLabelSlash}{(BasisPitch.Value - pit).ToString(Prec)}"
        pit = BasisPitch.Value * 0.01
        LabBAPDiff.Text = $"{kPlusMinus} {pit.ToString(Prec)}"
        LabBAPHighLow.Text = $"{(BasisPitch.Value + pit).ToString(Prec)}{kLabelSlash}{(BasisPitch.Value - pit).ToString(Prec)}"
        pit = BasisPitch.Value * 0.0075
        LabPAPDiff.Text = $"{kPlusMinus} {pit.ToString(Prec)}"
        LabPAPHighLow.Text = $"{(BasisPitch.Value + pit).ToString(Prec)}{kLabelSlash}{(BasisPitch.Value - pit).ToString(Prec)}"
        LabTrackDiff.Text = rad * 0.02
        MyBase.DataShow()
    End Sub
#End Region
End Class
