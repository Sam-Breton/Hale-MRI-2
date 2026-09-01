Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms.DataVisualization.Charting

Public Class ComboChartBorderSkinPicker
    Inherits ComboBox

    Private Const kItemHeightDefault As Integer = 21 ' Height to allow border preview lines to stand out cleanly

    Public Sub New()
        MyBase.New()
        InitializeComponent()

        ' Configure required custom drawing settings.
        Me.DrawMode = DrawMode.OwnerDrawFixed
        Me.DropDownStyle = ComboBoxStyle.DropDownList
        Me.ItemHeight = kItemHeightDefault

        ' Populate data exclusively at runtime to protect the designer canvas.
        If Not Me.DesignMode AndAlso System.ComponentModel.LicenseManager.UsageMode = System.ComponentModel.LicenseUsageMode.Runtime Then
            Me.DataSource = [Enum].GetValues(GetType(BorderSkinStyle))
        End If
    End Sub

    ''' <summary>
    ''' Exposes the strongly-typed BorderSkinStyle property. Hidden from the property grid to prevent design-time conflicts.
    ''' </summary>
    <Browsable(False)>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property SkinStyle As BorderSkinStyle
        Get
            If Me.SelectedItem IsNot Nothing Then
                Return CType(Me.SelectedItem, BorderSkinStyle)
            End If
            Return BorderSkinStyle.None
        End Get
        Set(value As BorderSkinStyle)
            ' Guard block to ensure we don't apply values before DataSource binds at runtime.
            If Me.DataSource IsNot Nothing Then
                If value = Nothing Then
                    Me.SelectedItem = BorderSkinStyle.None
                Else
                    Me.SelectedItem = value
                End If
            End If
        End Set
    End Property

    ''' <summary>
    ''' Prevents Designer from adding code that throws run-time exception.
    ''' </summary>
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
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    <Browsable(False)>
    <Bindable(False)>
    Public Shadows ReadOnly Property Items As ObjectCollection
        Get
            Return MyBase.Items
        End Get
    End Property

    ''' <summary>
    ''' Graphically draws each enum option accompanied by a miniature stylized frame border box.
    ''' </summary>
    Protected Overrides Sub OnDrawItem(e As DrawItemEventArgs)
        If e.Index < 0 OrElse Me.Items.Count = 0 OrElse e.Index >= Me.Items.Count Then Return

        e.DrawBackground()

        Dim currentStyle As BorderSkinStyle = CType(Me.Items(e.Index), BorderSkinStyle)
        Dim elementColor As Color = If((e.State And DrawItemState.Selected) = DrawItemState.Selected,
                                        SystemColors.HighlightText,
                                        SystemColors.WindowText)

        Dim textWidth As Integer = CInt(e.Bounds.Width * 0.5)
        Dim textBounds As New Rectangle(e.Bounds.Left + 4, e.Bounds.Top, textWidth - 4, e.Bounds.Height)

        Dim iconWidth As Integer = 24
        Dim iconHeight As Integer = 16
        Dim iconX As Integer = e.Bounds.Left + textWidth + 10
        Dim iconY As Integer = e.Bounds.Top + ((e.Bounds.Height - iconHeight) \ 2)
        Dim iconBounds As New Rectangle(iconX, iconY, iconWidth, iconHeight)

        ' 1. Draw the name text
        Using textBrush As New SolidBrush(elementColor),
              sf As New StringFormat()
            sf.LineAlignment = StringAlignment.Center
            e.Graphics.DrawString(currentStyle.ToString(), e.Font, textBrush, textBounds, sf)
        End Using

        ' 2. Draw the layout preview
        If currentStyle <> BorderSkinStyle.None Then
            Using pen As New Pen(elementColor, 1),
                  fillBrush As New SolidBrush(Color.FromArgb(40, elementColor))

                e.Graphics.FillRectangle(fillBrush, iconBounds)

                ' Match based on actual framework enumeration strings
                Dim styleName As String = currentStyle.ToString()

                If styleName.StartsWith("FrameTitle") Then
                    ' FrameTitle1 to FrameTitle8: Draw an outer border, plus a top title block segment
                    e.Graphics.DrawRectangle(pen, iconBounds)

                    Dim headerHeight As Integer = 4
                    Dim headerBounds As New Rectangle(iconBounds.Left, iconBounds.Top, iconBounds.Width, headerHeight)

                    Using headerBrush As New SolidBrush(Color.FromArgb(140, elementColor))
                        e.Graphics.FillRectangle(headerBrush, headerBounds)
                    End Using
                    e.Graphics.DrawLine(pen, iconBounds.Left, iconBounds.Top + headerHeight, iconBounds.Right, iconBounds.Top + headerHeight)

                ElseIf styleName.StartsWith("FrameThin") Then
                    ' FrameThin1 to FrameThin6: Standard elegant perimeter outline
                    e.Graphics.DrawRectangle(pen, iconBounds)

                Else
                    ' Handle 3D Bevel Behaviors
                    Select Case currentStyle
                        Case BorderSkinStyle.Emboss
                            ControlPaint.DrawBorder3D(e.Graphics, iconBounds, Border3DStyle.SunkenOuter)
                        Case BorderSkinStyle.Raised
                            ControlPaint.DrawBorder3D(e.Graphics, iconBounds, Border3DStyle.Raised)
                        Case BorderSkinStyle.Sunken
                            ControlPaint.DrawBorder3D(e.Graphics, iconBounds, Border3DStyle.Sunken)
                    End Select
                End If
            End Using
        Else
            ' None: Draw a soft dotted perimeter line
            Using faintPen As New Pen(Color.FromArgb(80, elementColor), 1)
                faintPen.DashStyle = DashStyle.Dot
                e.Graphics.DrawRectangle(faintPen, iconBounds)
            End Using
        End If

        e.DrawFocusRectangle()
    End Sub
End Class
