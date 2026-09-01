Imports System.Drawing.Drawing2D

Public Class ColorPanel
    Inherits CustomPanel

    Private ReadOnly kBorderColorSelected As Color = Color.Black
    Private ReadOnly kBorderColorUnselected As Color = Color.Gray
    Private Const kBorderWidthSelected As Integer = 3
    Private Const kBorderWidthUnselected As Integer = 1
    Private ReadOnly kDashStyleSelected As DashStyle = DashStyle.Solid
    Private ReadOnly kDashStyleUnselected As DashStyle = DashStyle.Dash

    Private mColor As Color = Color.Empty
    Private mSelected As Boolean = False

    Public Property Color As Color
        Get
            Return mColor
        End Get
        Set(value As Color)
            ColorSet(value)
            mColor = value
        End Set
    End Property

    Public Property Selected As Boolean
        Get
            Return mSelected
        End Get
        Set(value As Boolean)
            ColorSelect(value)
            mSelected = value
        End Set
    End Property

    Private Sub ColorSelect(ByVal selected As Boolean)
        Me.BorderColor = If(selected, kBorderColorSelected, kBorderColorUnselected)
        Me.BorderWidth = If(selected, kBorderWidthSelected, kBorderWidthUnselected)
        Me.DashStyle = If(selected, kDashStyleSelected, kDashStyleUnselected)
    End Sub

    Private Sub ColorSet(ByVal color As Color)
        If color = Color.Empty Then
            Me.BackColor = Color.Transparent
        Else
            Me.BackColor = color
        End If
    End Sub
End Class
