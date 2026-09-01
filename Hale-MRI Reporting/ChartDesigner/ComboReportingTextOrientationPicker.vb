Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D

Public Class ComboReportingTextOrientationPicker
    Inherits ComboBox

    Private Const kItemHeightDefault As Integer = 18
    Private Const kPenWidthDefault As Single = 1.0!
    Private Const kPenDrawOffsetBottom As Integer = -3
    Private Const kPenDrawOffsetBottom2 As Integer = -6
    Private Const kPenDrawOffsetMid As Integer = -3
    Private Const kPenDrawOffsetMid2 As Integer = 3
    Private Const kPenDrawOffsetTop As Integer = 3
    Private Const kPenDrawOffsetTop2 As Integer = 6
    Private Const kPreviewRectOffsetHeight As Integer = -6
    Private Const kPreviewRectOffsetX As Integer = 4
    Private Const kPreviewRectOffsetY As Integer = 3
    Private Const kPreviewRectWidthDefault As Integer = 30
    Private Const kTextBrushOffsetRight As Integer = 8

    Private ReadOnly mOrientationNames As String() = {"Auto", "Horizontal", "Rotated90", "Rotated270", "Stacked"}

    Public Sub New()
        MyBase.New()
        InitializeComponent()

        Me.DropDownStyle = ComboBoxStyle.DropDownList
        Me.DrawMode = DrawMode.OwnerDrawFixed
        Me.ItemHeight = kItemHeightDefault

        ' CRITICAL: Only populate data when the program is actually running
        If Not Me.DesignMode AndAlso System.ComponentModel.LicenseManager.UsageMode = System.ComponentModel.LicenseUsageMode.Runtime Then
            Me.DataSource = mOrientationNames
        End If
    End Sub

    ''' <summary>
    ''' Prevents Designer from adding code that throws run-time exception.
    ''' </summary>
    ''' <returns>Object</returns>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    <Browsable(False)>
    <Bindable(False)>
    Public Shadows Property DataSource As Object
        Get
            Return MyBase.DataSource
        End Get
        Set(value As Object)
            MyBase.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Prevents Designer from adding code that throws run-time exception.
    ''' </summary>
    ''' <returns>ObjectCollection</returns>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    <Browsable(False)>
    <Bindable(False)>
    Public Shadows ReadOnly Property Items As ObjectCollection
        Get
            Return MyBase.Items
        End Get
    End Property

    <Browsable(False)>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property OrientationName As String
        Get
            If Me.SelectedIndex >= 0 Then
                Return mOrientationNames(Me.SelectedIndex)
            End If
            Return "Auto"
        End Get
        Set(value As String)
            Dim index As Integer = Array.IndexOf(mOrientationNames, value)
            If index >= 0 Then
                Me.SelectedIndex = index
            End If
        End Set
    End Property

    Protected Overrides Sub OnDrawItem(e As DrawItemEventArgs)
        If e.Index < 0 Then Return

        Dim g As Graphics = e.Graphics
        Dim orientationName As String = mOrientationNames(e.Index)

        ' Draws standard native Windows selection background or fallback container background
        e.DrawBackground()

        ' Setup boundaries for the physical transformation canvas boundary box
        Dim previewRect As New Rectangle(e.Bounds.X + kPreviewRectOffsetX, e.Bounds.Y + kPreviewRectOffsetY, kPreviewRectWidthDefault,
                                         e.Bounds.Height + kPreviewRectOffsetHeight)

        If previewRect.Width > 0 AndAlso previewRect.Height > 0 Then
            Using p As New Pen(e.ForeColor, kPenWidthDefault)
                g.DrawRectangle(p, previewRect)
            End Using

            ' Draw the transformation preview inside the boundary box
            DrawOrientationPreview(g, previewRect, e.ForeColor, orientationName)
        End If

        ' Render standard name string label text next to the thumbnail
        Using textBrush As New SolidBrush(e.ForeColor)
            Dim textX As Integer = previewRect.Right + kTextBrushOffsetRight
            Dim textY As Integer = e.Bounds.Y + ((e.Bounds.Height - g.MeasureString(orientationName, Me.Font).Height) / 2)
            g.DrawString(orientationName, Me.Font, textBrush, textX, textY)
        End Using

        ' Draws native Windows dotted focus indicator box if active
        e.DrawFocusRectangle()
    End Sub

    Private Sub DrawOrientationPreview(g As Graphics, rect As Rectangle, textColor As Color, orientationName As String)
        Dim sampleText As String = "Abc"

        Using sampleFont As New Font(Me.Font.FontFamily, Me.Font.SizeInPoints - 2, FontStyle.Regular)
            Dim size As SizeF = g.MeasureString(sampleText, sampleFont)
            Dim state As GraphicsState = g.Save()

            ' Reposition coordinate matrix system to point to the exact midpoint of our preview container
            Dim cx As Single = rect.X + (rect.Width / 2)
            Dim cy As Single = rect.Y + (rect.Height / 2)
            g.TranslateTransform(cx, cy)

            Using textBrush As New SolidBrush(textColor)
                Select Case orientationName
                    Case "Auto", "Horizontal"
                        g.DrawString(sampleText, sampleFont, textBrush, -size.Width / 2, -size.Height / 2)

                    Case "Rotated90"
                        g.RotateTransform(90)
                        g.DrawString(sampleText, sampleFont, textBrush, -size.Width / 2, -size.Height / 2)

                    Case "Rotated270"
                        g.RotateTransform(270)
                        g.DrawString(sampleText, sampleFont, textBrush, -size.Width / 2, -size.Height / 2)

                    Case "Stacked"
                        g.Restore(state) ' Nullify ongoing coordinate modifications
                        Using p As New Pen(textColor, kPenWidthDefault)
                            Dim midX As Integer = rect.X + (rect.Width \ 2)
                            g.DrawLine(p, midX, rect.Top + kPenDrawOffsetTop, midX, rect.Bottom + kPenDrawOffsetBottom)
                            g.DrawLine(p, midX + kPenDrawOffsetMid, rect.Top + kPenDrawOffsetTop2, midX + kPenDrawOffsetMid2, rect.Top + kPenDrawOffsetTop2)
                            g.DrawLine(p, midX + kPenDrawOffsetMid, rect.Bottom + kPenDrawOffsetBottom2, midX + kPenDrawOffsetMid2, rect.Bottom + kPenDrawOffsetBottom2)
                        End Using
                        Return
                End Select
            End Using

            g.Restore(state)
        End Using
    End Sub
End Class
