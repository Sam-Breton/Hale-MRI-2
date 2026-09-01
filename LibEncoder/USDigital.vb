Imports LibGlobals

Public Class USDigital
    ' Concrete implementation of the IEncoderData interface for the USDigital hardware.
    Implements IEncoderHardware

    ' Private members
    Private Shared mUSB4 As USB4 = Nothing
    Private mDeviceCount As Integer = 0         ' Device count from USB4_Initialize(), non-zero if board initialized successfully
    Private mAngleCalibration As Double = CALIBRATION_DEFAULT   ' Current angle calibration value, defaults to 1.0
    Private mDepthCalibration As Double = CALIBRATION_DEFAULT   ' Current depth calibration value, defaults to 1.0
    Private mRadiusCalibration As Double = CALIBRATION_DEFAULT  ' Current radius calibration value, defaults to 1.0
    Private mRadiusOffset As Integer = 0        ' Current radius offset value, defaults to 0
    Private mLeftProbeOffset As Integer = 0 ' Current negative Offset from hub, Defaults to 0. Set from the Measurement Form
    Private mHubOffset As Integer = 0 ' Current additional offset from Radial Arm to probe, Defaults to 0. set From the Measurement Form
    Private mFixedOffset As Integer = 0 ' Current fixed offset from Radial Arm to Probe, Defaults to 0.
    Public Sub New()
        ' Create a new default-initialized instance of the USDigital encoder hardware class
        mUSB4 = New USB4
        Init()
    End Sub
    Public Sub New(ByVal angleCal As Double, ByVal depthCal As Double, radiusCal As Double, radiusOff As Integer)
        ' Create and initialize a new instance of the USDigital encoder hardware class
        ' with specified calibration values
        mUSB4 = New USB4
        AngleCalibration = angleCal
        DepthCalibration = depthCal
        RadiusCalibration = radiusCal
        RadiusOffset = radiusOff
        Init()
    End Sub
    Public Property AngleCalibration() As Double Implements IEncoderHardware.AngleCalibration
        Get
            ' Return the angle calibration value
            Return mAngleCalibration
        End Get
        Set(ByVal value As Double)
            ' Set the angle calibration value
            ArgumentOutOfRangeException.ThrowIfZero(value, NameOf(value))
            mAngleCalibration = value
        End Set
    End Property
    Public Property DepthCalibration() As Double Implements IEncoderHardware.DepthCalibration
        Get
            ' Return the depth calibration value
            Return mDepthCalibration
        End Get
        Set(ByVal value As Double)
            ' Set the depth calibration value
            ArgumentOutOfRangeException.ThrowIfZero(value, NameOf(value))
            mDepthCalibration = value
        End Set
    End Property
    Public ReadOnly Property Initialized As Boolean Implements IEncoderHardware.Initialized
        Get
            ' Return true if the hardware was initialized successfully
            Return mDeviceCount > 0
        End Get
    End Property
    Public Property RadiusCalibration() As Double Implements IEncoderHardware.RadiusCalibration
        Get
            ' Return the radius calibration value
            Return mRadiusCalibration
        End Get
        Set(ByVal value As Double)
            ' Set the radius calibration value
            ArgumentOutOfRangeException.ThrowIfZero(value, NameOf(value))
            mRadiusCalibration = value
        End Set
    End Property
    Public Property RadiusOffset() As Integer Implements IEncoderHardware.RadiusOffset
        Get
            ' Return the radius offset value
            Return mRadiusOffset
        End Get
        Set(ByVal value As Integer)
            ' Set the radius offset value
            mRadiusOffset = value
        End Set
    End Property
    Public Property FixedOffset() As Integer Implements IEncoderHardware.FixedOffset
        Get ' fixed offset is the fixed distance from the radial arm to the tip of the probe
            Return mFixedOffset
        End Get
        Set(value As Integer)
            mFixedOffset = value
        End Set
    End Property
    Public Property LeftProbeOffset() As Integer Implements IEncoderHardware.LeftProbeOffset
        Get ' This offset is along the Radial Arm
            Return mLeftProbeOffset
        End Get
        Set(value As Integer)
            mLeftProbeOffset = value
        End Set
    End Property
    Public Property HubOffset() As Integer Implements IEncoderHardware.HubOffset
        Get ' This Offset is Perpendicular to the Radial Arm
            Return mHubOffset
        End Get
        Set(value As Integer)
            mHubOffset = value
        End Set
    End Property
    Public Function Angle() As Double Implements IEncoderHardware.Angle
        ' Return the angle value
        Return IPropellerData_Angle()
    End Function
    Public Function Calibrate(ByVal encoderNo As Integer) As Double Implements IEncoderHardware.Calibrate
        ' Calibrate the specified encoder and return the calibration value
        ResetCountSafely(0, encoderNo)
        Select Case encoderNo
            Case ANGLE_ENCODER
                ' Save and return the angle calibration value
                AngleCalibration = 1.0#
                Return AngleCalibration
            Case RADIUS_ENCODER
                ' Save and return the radius calibration value
                RadiusCalibration = AdjustedCount(RADIUS_ENCODER) / 4.0#
                Return RadiusCalibration
            Case DEPTH_ENCODER
                ' Save and return the depth calibration value
                DepthCalibration = AdjustedCount(DEPTH_ENCODER) / 4.0#
                Return DepthCalibration
            Case Else
                ' Unexpected encoder number
                Throw New NotImplementedException(STR_ERR_ENCODER_INVALID)
        End Select
    End Function
    Public Function Depth() As Double Implements IEncoderHardware.Depth
        ' Return the depth value
        Return IPropellerData_Depth()
    End Function
    Public Function Radius(ByVal diameter As Double) As IEncoderHardware.RadiusMeasurement Implements IEncoderHardware.Radius
        ' Return the radius and percent values
        Return IPropellerData_Radius(diameter)
    End Function
    Private Shared Function AdjustedCount(ByVal encoderNo As Integer) As Long
        ' Get the adjusted count for the specified encoder
        Dim count As Long

        count = GetCountSafely(0, encoderNo)
        If count >= ENCODER_UNREACHABLE_COUNT Then count -= ENCODER_MAX_COUNT
        Return count
    End Function
    Public Function SetEncoderCount(encoderNo As Integer, count As Integer) As Boolean Implements IEncoderHardware.SetEncoderCount
        'Sets the encoder count to the specified value
        Dim worked As Boolean = SetCount(encoderNo, count)
        Return worked
    End Function
    Public Function SetForward(encoderNo As Integer, forward As Boolean) As Boolean Implements IEncoderHardware.SetForward
        ' sets the count direction for the specified encoder
        Dim worked As Boolean = SetForward(0, encoderNo, forward) ' false for Right hand true for left hand propellers
        Return worked
    End Function
    Public Sub Init() Implements IEncoderHardware.Initialize
        ' Hardware specific initialization code
        mDeviceCount = Initialize()
        SetPresetValue(0, ANGLE_ENCODER, ANGLE_PRESET_VALUE)
        SetMultiplier(0, ANGLE_ENCODER, QCM_MODE_X4_QUADRATURE)
        SetCounterMode(0, ANGLE_ENCODER, COUNTER_MODE_MODULO_N)
        SetForward(0, ANGLE_ENCODER, USB4_FALSE)
        SetCounterEnabled(0, ANGLE_ENCODER, USB4_TRUE)

        SetPresetValue(0, RADIUS_ENCODER, RADIUS_PRESET_VALUE)
        SetMultiplier(0, RADIUS_ENCODER, QCM_MODE_X4_QUADRATURE)
        SetCounterMode(0, RADIUS_ENCODER, COUNTER_MODE_24BIT)
        SetForward(0, RADIUS_ENCODER, USB4_FALSE)
        SetCounterEnabled(0, RADIUS_ENCODER, USB4_TRUE)

        SetPresetValue(0, DEPTH_ENCODER, DEPTH_PRESET_VALUE)
        SetMultiplier(0, DEPTH_ENCODER, QCM_MODE_X4_QUADRATURE)
        SetCounterMode(0, DEPTH_ENCODER, COUNTER_MODE_24BIT)
        SetForward(0, DEPTH_ENCODER, USB4_TRUE)
        SetCounterEnabled(0, RADIUS_ENCODER, USB4_TRUE)
    End Sub
    Private Function IPropellerData_Angle() As Double
        ' Returns the angle measurement in degrees
        Dim angle As Double
        Dim TotalOffset As Single = FixedOffset + LeftProbeOffset
        Dim temprad As Double = (GetCountSafely(0, RADIUS_ENCODER) + RadiusOffset) / RadiusCalibration
        angle = GetCountSafely(0, ANGLE_ENCODER) / AngleCalibration
        Dim adjRad As Double = Math.Sqrt((temprad * temprad) + (TotalOffset * TotalOffset))
        Dim angleOffset As Double = Math.Atan2(TotalOffset, adjRad) * (180.0# / Math.PI)
        Return angle + angleOffset
    End Function
    Private Function IPropellerData_Depth() As Double
        ' Hardware specific code to get the depth value
        Return AdjustedCount(DEPTH_ENCODER) / DepthCalibration
    End Function
    Private Function IPropellerData_Radius(ByVal diameter As Double) As IEncoderHardware.RadiusMeasurement
        ' Hardware specific code to get the radius measurement and calculate the percent.
        ' Currently throws an exception for invalid diameter values, but can be modified
        ' to skip the Percent calculation if desired.
        Dim measurement As IEncoderHardware.RadiusMeasurement
        Dim TotalOffset As Single = FixedOffset + LeftProbeOffset
        Dim rad As Double = (AdjustedCount(RADIUS_ENCODER) + RadiusOffset) / RadiusCalibration
        Dim temp As Double = Math.Sqrt((rad * rad) + (TotalOffset * TotalOffset))
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(diameter, NameOf(diameter))
        measurement.Value = temp - LeftProbeOffset
        measurement.Percent = measurement.Value / (diameter / 2.0#)
        Return measurement
    End Function
    Public Sub ResetCount(encoderNo As Integer) Implements IEncoderHardware.ResetCount
        ResetCountSafely(0, encoderNo)
    End Sub
    '
    ' Wrapper functions that call and handle results from USDigital hardware functions
    '
    Private Shared Function GetCountSafely(ByVal deviceNo As Integer, ByVal encoderNo As Integer) As Long
        ' Safely get the encoder count(Needs mutex implementation for safety
        Dim count As Long, result As Boolean
        mUSB4.DeviceNo = deviceNo
        result = mUSB4.GetCount(ValidEncoder(encoderNo), count)
        If result <> USB4_SUCCESS Then Throw New Exception(STR_ERR_COUNT)
        Return count
    End Function
    Private Shared Function Initialize() As Long
        ' Initialize the hardware board
        Dim result As Boolean

        result = mUSB4.Initialize()
        If result <> True Then Throw New Exception(STR_ERR_HARDWARE_INIT)
        Return mUSB4.DeviceCount
    End Function
    Private Shared Sub ResetCountSafely(ByVal deviceNo As Integer, ByVal encoderNo As Integer)
        ' Safely reset the encoder count
        Dim result As Long
        mUSB4.DeviceNo = deviceNo
        result = mUSB4.ResetCount(ValidEncoder(encoderNo))
        If result <> USB4_SUCCESS Then Throw New Exception(STR_ERR_COUNT)
    End Sub
    Private Shared Sub SetCounterMode(ByVal iDeviceNo As Integer, ByVal iEncoder As Integer, ByVal iVal As Integer)
        ' Set the counter mode for the specified encoder
        Dim result As Boolean
        mUSB4.DeviceNo = iDeviceNo
        result = mUSB4.SetCounterMode(ValidEncoder(iEncoder), iVal)
        If result <> USB4_SUCCESS Then Throw New Exception(STR_ERR_HARDWARE_INIT)
    End Sub
    Private Shared Sub SetCounterEnabled(ByVal iDeviceNo As Integer, ByVal iEncoder As Integer, ByVal bVal As Long)
        ' Sets the counter enabled state for the specified encoder
        Dim result As Long
        mUSB4.DeviceNo = iDeviceNo
        result = mUSB4.SetCounterEnabled(ValidEncoder(iEncoder), bVal)
        If result <> USB4_SUCCESS Then Throw New Exception(STR_ERR_HARDWARE_INIT)
    End Sub
    Private Shared Function SetForward(ByVal iDeviceNo As Integer, ByVal iEncoder As Integer, ByVal bVal As Long) As Boolean
        ' Sets the count direction for the specified encoder
        Dim result As Boolean
        mUSB4.DeviceNo = iDeviceNo
        result = mUSB4.SetForward(ValidEncoder(iEncoder), bVal)
        If result <> USB4_SUCCESS Then Throw New Exception(STR_ERR_HARDWARE_INIT)
        Return result
    End Function
    Private Shared Sub SetMultiplier(ByVal iDeviceNo As Integer, ByVal iEncoder As Integer, ByVal iVal As Integer)
        ' Sets the quadrature counter multiplier mode for the specified encoder
        Dim result As Boolean
        mUSB4.DeviceNo = iDeviceNo
        result = mUSB4.SetMultiplier(ValidEncoder(iEncoder), iVal)
        If result <> USB4_SUCCESS Then Throw New Exception(STR_ERR_HARDWARE_INIT)
    End Sub
    Private Shared Sub SetPresetValue(ByVal iDeviceNo As Integer, ByVal iEncoder As Integer, ByVal ulVal As Long)
        ' Set the preset register value for the specified encoder
        Dim result As Boolean
        mUSB4.DeviceNo = iDeviceNo
        result = mUSB4.SetPresetValue(ValidEncoder(iEncoder), ulVal)
        If result <> USB4_SUCCESS Then Throw New Exception(STR_ERR_HARDWARE_INIT)
    End Sub
    Private Shared Function SetCount(encoderNo As Integer, count As Integer) As Boolean
        Dim result As Boolean
        result = mUSB4.SetCount(ValidEncoder(encoderNo), count)
        If result <> USB4_SUCCESS Then Throw New Exception(STR_ERR_COUNT)
        Return result
    End Function
    Private Shared Function ValidEncoder(ByVal encoderNo As Integer) As Long
        ' Check if the specified encoder number is valid
        ArgumentOutOfRangeException.ThrowIfLessThan(Of Integer)(encoderNo, ANGLE_ENCODER, NameOf(encoderNo))
        ArgumentOutOfRangeException.ThrowIfGreaterThan(Of Integer)(encoderNo, DEPTH_ENCODER, NameOf(encoderNo))
        Return encoderNo
    End Function
End Class
