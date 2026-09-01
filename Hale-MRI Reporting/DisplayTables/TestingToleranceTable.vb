Imports LibDatabase.Models

Public Class TestingToleranceTable
    Inherits DisplayControl
#Region "Constructors"
    ''' <summary>
    ''' Creates a new Control instance
    ''' </summary>
    Public Sub New()
        MyBase.New()
        InitializeComponent()
    End Sub
#End Region
#Region "Client Properties"
    ''leave for later
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
End Class
