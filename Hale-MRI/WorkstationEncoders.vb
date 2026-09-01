Imports LibDatabase.Contexts
Imports LibDatabase.Models
Imports LibEncoder

''' <summary>
''' Encapsulates the encoder hardware and workstation calibration data,
''' and performs routine initialization.
''' </summary>
''' 

Public Class WorkstationEncoders
#Region "Private Members"
    Private ReadOnly mDatabase As HaleMRIContext            ' The current database context.
    Private mEncoders As EncoderHardware                    ' Encoder hardware instance
    Private ReadOnly mServiceProvider As IServiceProvider   ' The current database ServiceProvider reference.
    Private mWorkstation As Workstation                     ' Workstation calibration data from database 
#End Region
#Region "Constructors"
    Public Sub New()
        ' Default constructor initializes the encoder hardware only.
        mEncoders = New EncoderHardware(New USDigital())
    End Sub
    'Public Sub New(context As HaleMRIContext, serviceProvider As IServiceProvider)
    '    mDatabase = context
    '    mServiceProvider = serviceProvider
    'End Sub
#End Region
#Region "Public Interface"
    Public Property PollingInterval As Long = kEncoderPollingIntervalDefault   ' Encoder polling interval in milliseconds

    Public Property Encoders As EncoderHardware     ' Gets or sets the encoder hardware instance.
        Get
            Return mEncoders
        End Get
        Set(value As EncoderHardware)
            InitializeEncoders(mWorkstation, value)
            mEncoders = value
        End Set
    End Property
    Public Property Workstation As Workstation      ' Gets or sets the workstation calibration data.
        Get
            Return mWorkstation
        End Get
        Set(value As Workstation)
            InitializeEncoders(value, mEncoders)
            mWorkstation = value
        End Set
    End Property
#End Region
#Region "Private Interface"
    Private Sub InitializeEncoders(ByVal ws As Workstation, ByVal hw As EncoderHardware)
        ' Copy the workstation calibration data to the encoder calibration properties.
        If ws IsNot Nothing AndAlso hw IsNot Nothing Then
            hw.AngleCalibration = ws.AngleCalibration
            hw.DepthCalibration = ws.DepthCalibration
            hw.RadiusCalibration = ws.RadiusCalibration
            hw.RadiusOffset = ws.RadiusOffset
        End If
    End Sub
#End Region
End Class
