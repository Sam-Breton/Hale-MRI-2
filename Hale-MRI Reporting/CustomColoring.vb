Public Module CustomColoring
    Public Enum CustomColors
        CharcoalBlack
        Gray100
        Gray300
        Gray500
        ModernCoralPink
        NeutralGray
        SaaSLightBackground
        Slate50
        Slate200
        SoftViolet
        SubtleCoolGray
        TechBlue
        VibrantTeal
        WarmAmberOrange
        WarmOffWhite
    End Enum

    Public Class CustomColor
        Public Property DisplayName As String
        Public Property ColorValue As Color

        Public Sub New(name As String, value As Color)
            Me.DisplayName = name
            Me.ColorValue = value
        End Sub

        Public Shared Function FromName(ByVal name As String) As CustomColor
            Return mCustomColors.FirstOrDefault(Function(clr) clr.DisplayName = name)
        End Function
    End Class

    Private mCustomColors As New List(Of CustomColor) From {
        New CustomColor("CharcoalBlack", Color.FromArgb(31, 41, 55)),
        New CustomColor("Gray100", Color.FromArgb(242, 242, 242)),
        New CustomColor("Gray300", Color.FromArgb(209, 213, 219)),
        New CustomColor("Gray500", Color.FromArgb(107, 114, 128)),
        New CustomColor("NeutralGray", Color.FromArgb(249, 250, 251)),
        New CustomColor("SaaSLightBackground", Color.FromArgb(248, 249, 250)),
        New CustomColor("Slate50", Color.FromArgb(248, 250, 252)),
        New CustomColor("Slate200", Color.FromArgb(226, 232, 240)),
        New CustomColor("SubtleCoolGray", Color.FromArgb(220, 224, 230)),
        New CustomColor("WarmOffWhite", Color.FromArgb(252, 252, 250)),
        New CustomColor("TechBlue", Color.FromArgb(59, 130, 246)),          ' Series-Blue
        New CustomColor("VibrantTeal", Color.FromArgb(20, 184, 166)),       ' Series-Teal
        New CustomColor("SoftViolet", Color.FromArgb(139, 92, 246)),        ' Series-Purple
        New CustomColor("ModernCoralPink", Color.FromArgb(244, 63, 94)),    ' Series-Coral
        New CustomColor("WarmAmberOrange", Color.FromArgb(245, 158, 11)),   ' Series-Amber
        New CustomColor("CleanGreen", Color.FromArgb(16, 185, 129))         ' Series-Emerald
    }
End Module
