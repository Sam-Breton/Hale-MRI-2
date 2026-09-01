Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
Imports System.Windows.Forms.DataVisualization.Charting

Public Class ComboChartArea3DStylePicker
    Inherits ComboBox

    Private Class Preset3DItem
        Public Property Name As String
        Public Property Style As ChartArea3DStyle

        Public Sub New(presetName As String, enable As Boolean, inc As Integer, rot As Integer, wall As Integer)
            Me.Name = presetName
            Me.Style = New ChartArea3DStyle() With {
                .Enable3D = enable,
                .Inclination = inc,
                .Rotation = rot,
                .WallWidth = wall
            }
        End Sub

        Public Overrides Function ToString() As String
            Return Me.Name
        End Function
    End Class

    Private mCustomItem As Preset3DItem = Nothing
    Private ReadOnly mPresets As New List(Of Preset3DItem)()

    Public Sub New()
        MyBase.New()
        InitializeComponent()

        Me.DropDownStyle = ComboBoxStyle.DropDownList
        Me.DrawMode = DrawMode.OwnerDrawFixed
        Me.ItemHeight = 18

        ' Populate with standard MS Charting 3D defaults
        mPresets.Add(New Preset3DItem("2D Flat Layout (Disabled)", False, 0, 0, 7))
        mPresets.Add(New Preset3DItem("Standard 3D View", True, 30, 30, 7))
        mPresets.Add(New Preset3DItem("Isometric View", True, 45, 45, 7))
        mPresets.Add(New Preset3DItem("Steep Perspective", True, 60, 20, 5))
        mPresets.Add(New Preset3DItem("Top-Down Tilt", True, 15, 45, 10))
        mPresets.Add(New Preset3DItem("Frontal Depth", True, 10, 0, 7))

        ' CRITICAL: Only populate data when the program is actually running
        If Not Me.DesignMode AndAlso System.ComponentModel.LicenseManager.UsageMode = System.ComponentModel.LicenseUsageMode.Runtime Then
            Me.DataSource = mPresets
        End If
    End Sub

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
    Public Property Style As ChartArea3DStyle
        Get
            If Me.SelectedItem IsNot Nothing Then
                Return mPresets(Me.SelectedIndex).Style
            End If
            Return New ChartArea3DStyle() With {.Enable3D = False}
        End Get
        Set(value As ChartArea3DStyle)
            If value Is Nothing Then
                Me.SelectedIndex = -1
                Return
            End If

            ' 1. Clean up any previous "Custom" entry before evaluating a new match
            If mCustomItem IsNot Nothing AndAlso mPresets.Contains(mCustomItem) Then
                mPresets.Remove(mCustomItem)
                mCustomItem = Nothing
            End If

            ' 2. Look for an exact match among your pre-configured styles
            Dim matchIndex As Integer = -1
            For i As Integer = 0 To mPresets.Count - 1
                Dim p = mPresets(i).Style
                If p.Enable3D = value.Enable3D Then
                    ' If 3D is disabled, the other properties don't matter for a visual match
                    If Not value.Enable3D Then
                        matchIndex = i
                        Exit For
                    End If
                    ' If 3D is enabled, check the structural view properties
                    If p.Inclination = value.Inclination AndAlso
                       p.Rotation = value.Rotation AndAlso
                       p.WallWidth = value.WallWidth Then
                        matchIndex = i
                        Exit For
                    End If
                End If
            Next

            ' 3. If a match is found, select it. Otherwise, construct a custom option.
            If matchIndex >= 0 Then
                ' Rebind the data source to update the control state safely
                Me.DataSource = Nothing
                Me.DataSource = mPresets
                Me.SelectedIndex = matchIndex
            Else
                ' Generate a unique custom option on the fly reflecting the chart's unique layout
                mCustomItem = New Preset3DItem($"Custom ({value.Inclination}°, {value.Rotation}°)", value.Enable3D, value.Inclination, value.Rotation, value.WallWidth)
                mPresets.Add(mCustomItem)

                Me.DataSource = Nothing
                Me.DataSource = mPresets
                Me.SelectedItem = mCustomItem
            End If
        End Set
    End Property

    Private Sub Draw3DCubeSchematic(g As Graphics, rect As Rectangle, foregroundColor As Color, style As ChartArea3DStyle)
        Using p As New Pen(foregroundColor, 1)
            If Not style.Enable3D Then
                g.DrawRectangle(p, rect.X + 6, rect.Y + 4, rect.Width - 12, rect.Height - 8)
                Return
            End If

            Dim offset As Integer = If(style.Inclination > 40, 4, 6)

            Dim fTL As New Point(rect.X + 4, rect.Y + 4 + offset)
            Dim fTR As New Point(rect.Right - 4 - offset, rect.Y + 4 + offset)
            Dim fBL As New Point(rect.X + 4, rect.Bottom - 4)
            Dim fBR As New Point(rect.Right - 4 - offset, rect.Bottom - 4)

            Dim bTL As New Point(rect.X + 4 + offset, rect.Y + 4)
            Dim bTR As New Point(rect.Right - 4, rect.Y + 4)
            Dim bBL As New Point(rect.X + 4 + offset, rect.Bottom - 4 - offset)
            Dim bBR As New Point(rect.Right - 4, rect.Bottom - 4 - offset)

            Using faintPen As New Pen(Color.FromArgb(100, foregroundColor), 1)
                faintPen.DashStyle = DashStyle.Dot
                g.DrawLine(faintPen, bTL, bTR)
                g.DrawLine(faintPen, bBL, bBR)
                g.DrawLine(faintPen, bTL, bBL)
                g.DrawLine(faintPen, bTR, bBR)
            End Using

            g.DrawLine(p, fTL, bTL)
            g.DrawLine(p, fTR, bTR)
            g.DrawLine(p, fBL, bBL)
            g.DrawLine(p, fBR, bBR)

            g.DrawLine(p, fTL, fTR)
            g.DrawLine(p, fBL, fBR)
            g.DrawLine(p, fTL, fBL)
            g.DrawLine(p, fTR, fBR)
        End Using
    End Sub

    Protected Overrides Sub OnDrawItem(e As DrawItemEventArgs)
        If e.Index < 0 Then Return

        Dim g As Graphics = e.Graphics
        Dim currentPreset As Preset3DItem = mPresets(e.Index)

        e.DrawBackground()

        Dim previewRect As New Rectangle(e.Bounds.X + 4, e.Bounds.Y + 3, 30, e.Bounds.Height - 6)

        If previewRect.Width > 0 AndAlso previewRect.Height > 0 Then
            Using p As New Pen(e.ForeColor, 1)
                g.DrawRectangle(p, previewRect)
            End Using
            Draw3DCubeSchematic(g, previewRect, e.ForeColor, currentPreset.Style)
        End If

        Using textBrush As New SolidBrush(e.ForeColor)
            Dim textX As Integer = previewRect.Right + 8
            Dim textY As Integer = e.Bounds.Y + ((e.Bounds.Height - g.MeasureString(currentPreset.Name, Me.Font).Height) / 2)
            g.DrawString(currentPreset.Name, Me.Font, textBrush, textX, textY)
        End Using

        e.DrawFocusRectangle()
    End Sub
End Class
