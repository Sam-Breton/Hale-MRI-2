Imports System.ComponentModel
Imports System.Drawing.Drawing2D

Public Class CustomPanel
    Inherits UserControl

    Private kBorderColorDefault As Color = Color.Gray
    Private kBorderPatternDefault() As Single = {0.0F, 10.0F}
    Private kBorderStyleDefault As DashStyle = DashStyle.Dash
    Private Const kBorderWidthDefault As Integer = 0
    Private mBorderColor As Color = kBorderColorDefault
    Private mBorderPattern() As Single = kBorderPatternDefault
    Private mBorderStyle As DashStyle = kBorderStyleDefault
    Private mBorderWidth As Single = kBorderWidthDefault

    <Browsable(True)>
    <Category("Appearance")>
    Public Property BorderColor As Color
        Get
            Return mBorderColor
        End Get
        Set(value As Color)
            mBorderColor = value
            Me.Invalidate()
        End Set
    End Property

    <Browsable(True)>
    <Category("Appearance")>
    Public Property DashPattern As Single()
        Get
            Return mBorderPattern
        End Get
        Set(value As Single())
            mBorderPattern = value
            Me.Invalidate()
        End Set
    End Property

    <Browsable(True)>
    <Category("Appearance")>
    Public Property DashStyle As DashStyle
        Get
            Return mBorderStyle
        End Get
        Set(value As DashStyle)
            mBorderStyle = value
            Me.Invalidate()
        End Set
    End Property

    <Browsable(True)>
    <Category("Appearance")>
    Public Property BorderWidth As Integer
        Get
            Return mBorderWidth
        End Get
        Set(value As Integer)
            mBorderWidth = value
            Me.Invalidate()
        End Set
    End Property

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        ' DO NOT call MyBase.OnPaint()!!!
        If Me.BorderWidth = 0 Then Return
        ' Use the "Using" statement to ensure the Pen object is disposed of correctly.
        Using p As New Pen(Me.BorderColor, Me.BorderWidth)
            ' Set the DashStyle property.
            p.DashStyle = Me.DashStyle
            If Me.DashStyle = DashStyle.Custom Then p.DashPattern = Me.DashPattern

            ' Calculate the rectangle area to draw the border.
            ' Adjust the rectangle size and location to ensure the full border is visible.
            Dim rect As New Rectangle(0, 0, Me.Width - 1, Me.Height - 1)

            ' Draw the rectangle.
            e.Graphics.DrawRectangle(p, rect)
        End Using
    End Sub
End Class
