Public Module Constants
    ' Constants for USDigital encoder hardware
    Public Const ANGLE_ENCODER As Integer = 0
    Public Const RADIUS_ENCODER As Integer = 1
    Public Const DEPTH_ENCODER As Integer = 2
    Public Const CALIBRATION_DEFAULT As Double = 1.0
    Public Const ENCODER_MAX_COUNT As Long = 16777215
    Public Const ENCODER_UNREACHABLE_COUNT As Long = 14777215
    Public Const QCM_MODE_CLOCK_DIRECTION As Integer = 0
    Public Const QCM_MODE_X1_QUADRATURE As Integer = 1
    Public Const QCM_MODE_X2_QUADRATURE As Integer = 2
    Public Const QCM_MODE_X4_QUADRATURE As Integer = 3
    Public Const COUNTER_MODE_24BIT As Integer = 0
    Public Const COUNTER_MODE_RANGE_LIMIT As Integer = 1
    Public Const COUNTER_MODE_NON_RECYCLE As Integer = 2
    Public Const COUNTER_MODE_MODULO_N As Integer = 3
    Public Const ANGLE_PRESET_VALUE As Long = 7999
    Public Const DEPTH_PRESET_VALUE As Long = 8000
    Public Const RADIUS_PRESET_VALUE As Long = 8000
    Public Const USB4_FALSE As Long = 0
    Public Const USB4_TRUE As Long = 1
    Public Const USB4_SUCCESS As Long = -1

    ' Application encoder constants.
    Public Const kEncoderPollingIntervalDefault As Integer = 10
End Module
