Imports System.ComponentModel
Imports System.Windows.Forms.DataVisualization.Charting

Public Class ComboChartTextStylePicker
    Inherits ComboBox

    Private Const kItemHeightDefault As Integer = 18
    Private Const kPenWidthDefault As Single = 1.0!
    Private Const kPreviewRectOffsetHeight As Integer = -6
    Private Const kPreviewRectOffsetX As Integer = 4
    Private Const kPreviewRectOffsetY As Integer = 3
    Private Const kPreviewRectWidthDefault As Integer = 30
    Private Const kTextBrushOffsetRight As Integer = 8

    Public Sub New()
        MyBase.New()
        InitializeComponent()

        Me.DropDownStyle = ComboBoxStyle.DropDownList
        Me.DrawMode = DrawMode.OwnerDrawFixed
        Me.ItemHeight = kItemHeightDefault

        ' CRITICAL: Only populate data when the program is actually running
        If Not Me.DesignMode AndAlso System.ComponentModel.LicenseManager.UsageMode = System.ComponentModel.LicenseUsageMode.Runtime Then
            Me.DataSource = [Enum].GetValues(GetType(TextStyle))
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
    Public Property TextStyle As TextStyle
        Get
            If Me.SelectedItem IsNot Nothing Then
                Return CType(Me.SelectedItem, TextStyle)
            End If
            Return TextStyle.Default
        End Get
        Set(value As TextStyle)
            If Me.DataSource IsNot Nothing Then
                Me.SelectedItem = value
            End If
        End Set
    End Property

    Protected Overrides Sub OnDrawItem(e As DrawItemEventArgs)
        If e.Index < 0 Then Return

        Dim g As Graphics = e.Graphics
        Dim style As TextStyle = CType(Me.Items(e.Index), TextStyle)

        ' Draws standard Windows highlight or item backgrounds natively
        e.DrawBackground()

        ' Setup bounds for the text effect preview block
        Dim previewRect As New Rectangle(e.Bounds.X + kPreviewRectOffsetX, e.Bounds.Y + kPreviewRectOffsetY, kPreviewRectWidthDefault,
                                         e.Bounds.Height + kPreviewRectOffsetHeight)

        If previewRect.Width > 0 AndAlso previewRect.Height > 0 Then
            ' Draw a standard boundary container outline around the preview block
            Using p As New Pen(e.ForeColor, kPenWidthDefault)
                g.DrawRectangle(p, previewRect)
            End Using

            ' Draw the visual representation of the text style effect ("Abc")
            DrawTextStylePreview(g, previewRect, e.ForeColor, style)
        End If

        ' Render the enum label text cleanly next to the effect box
        Using textBrush As New SolidBrush(e.ForeColor)
            Dim textX As Integer = previewRect.Right + kTextBrushOffsetRight
            Dim textY As Integer = e.Bounds.Y + ((e.Bounds.Height - g.MeasureString(style.ToString(), Me.Font).Height) / 2)
            g.DrawString(style.ToString(), Me.Font, textBrush, textX, textY)
        End Using

        ' Draws standard Windows dotted focus lines if the control has focus
        e.DrawFocusRectangle()
    End Sub

    Private Sub DrawTextStylePreview(g As Graphics, rect As Rectangle, textColor As Color, style As TextStyle)
        Dim sampleText As String = "Aa"

        ' Use a slightly smaller font variant to safely constrain text within the preview boundaries
        Using sampleFont As New Font(Me.Font.FontFamily, Me.Font.SizeInPoints - 1, FontStyle.Bold)
            Dim size As SizeF = g.MeasureString(sampleText, sampleFont)
            Dim textX As Single = rect.X + ((rect.Width - size.Width) / 2)
            Dim textY As Single = rect.Y + ((rect.Height - size.Height) / 2)

            Select Case style
                Case TextStyle.Default
                    Using textBrush As New SolidBrush(textColor)
                        g.DrawString(sampleText, sampleFont, textBrush, textX, textY)
                    End Using

                Case TextStyle.Shadow
                    ' Render a slightly offset, semi-transparent shadow block first
                    Dim shadowColor As Color = Color.FromArgb(100, textColor)
                    Using shadowBrush As New SolidBrush(shadowColor)
                        g.DrawString(sampleText, sampleFont, shadowBrush, textX + 1, textY + 1)
                    End Using
                    Using textBrush As New SolidBrush(textColor)
                        g.DrawString(sampleText, sampleFont, textBrush, textX, textY)
                    End Using

                Case TextStyle.Emboss
                    ' Highlighting effect dropping down and right, foreground pushed up-left
                    Using whiteBrush As New SolidBrush(Color.FromArgb(120, Color.White))
                        g.DrawString(sampleText, sampleFont, whiteBrush, textX + 1, textY + 1)
                    End Using
                    Using textBrush As New SolidBrush(textColor)
                        g.DrawString(sampleText, sampleFont, textBrush, textX, textY)
                    End Using

                Case TextStyle.Embed
                    ' Shadow effect on top-left to make text appear etched/sunken into the canvas
                    Using darkBrush As New SolidBrush(Color.FromArgb(120, Color.Black))
                        g.DrawString(sampleText, sampleFont, darkBrush, textX - 0.5F, textY - 0.5F)
                    End Using
                    Using textBrush As New SolidBrush(textColor)
                        g.DrawString(sampleText, sampleFont, textBrush, textX, textY)
                    End Using

                Case TextStyle.Frame
                    ' Draw structural backing lines around the character frame boundary
                    Using textBrush As New SolidBrush(textColor)
                        ' Simple 4-way path shifting to mimic a standard font outline frame
                        g.DrawString(sampleText, sampleFont, textBrush, textX - 0.5F, textY)
                        g.DrawString(sampleText, sampleFont, textBrush, textX + 0.5F, textY)
                        g.DrawString(sampleText, sampleFont, textBrush, textX, textY - 0.5F)
                        g.DrawString(sampleText, sampleFont, textBrush, textX, textY + 0.5F)
                    End Using
                    ' Overlap with background window context color to reveal clear borders
                    Using bgBrush As New SolidBrush(Me.BackColor)
                        g.DrawString(sampleText, sampleFont, bgBrush, textX, textY)
                    End Using
            End Select
        End Using
    End Sub
End Class
