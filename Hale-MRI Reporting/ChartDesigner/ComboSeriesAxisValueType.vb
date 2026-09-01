Imports System.ComponentModel
Imports System.Windows.Forms.DataVisualization.Charting

Public Class ComboSeriesAxisValueType
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

        ' Enable custom painting
        Me.DrawMode = DrawMode.OwnerDrawFixed
        Me.DropDownStyle = ComboBoxStyle.DropDownList
        Me.ItemHeight = kItemHeightDefault

        ' CRITICAL: Only populate data when the program is actually running
        If Not Me.DesignMode AndAlso System.ComponentModel.LicenseManager.UsageMode = System.ComponentModel.LicenseUsageMode.Runtime Then
            Me.DataSource = [Enum].GetValues(GetType(ChartValueType))
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
    Public Property ValueType As ChartValueType
        Get
            If Me.SelectedItem IsNot Nothing Then
                Return CType(Me.SelectedItem, ChartValueType)
            End If
            Return ChartValueType.Auto ' Default fallback
        End Get
        Set(value As ChartValueType)
            If Me.DataSource IsNot Nothing Then
                Me.SelectedItem = value
            End If
        End Set
    End Property

    Protected Overrides Sub OnDrawItem(e As DrawItemEventArgs)
        ' Prevent paint crashes during design-time or empty states
        If e.Index < 0 OrElse Me.Items.Count = 0 Then Return

        ' Render standard system backgrounds and focus states
        e.DrawBackground()
        e.DrawFocusRectangle()

        Dim itemValue As ChartValueType = CType(Me.Items(e.Index), ChartValueType)
        Dim bounds As Rectangle = e.Bounds

        ' 1. Calculate geometry for a preview pill tag
        Dim tagHeight As Integer = bounds.Height - 6
        Dim tagWidth As Integer = 32 ' Width to fit short text symbols like "123", "Calendar", "A"
        Dim tagRect As New Rectangle(bounds.X + 4, bounds.Y + 3, tagWidth, tagHeight)

        ' 2. Determine color categories and short indicator text based on the value type
        Dim tagBgColor As Color = Color.LightGray
        Dim indicatorText As String = "???"

        Select Case itemValue
            Case ChartValueType.Auto
                tagBgColor = Color.FromArgb(230, 230, 230)
                indicatorText = "Auto"

            ' Numeric Types
            Case ChartValueType.Double, ChartValueType.Single, ChartValueType.Int32,
                 ChartValueType.Int64, ChartValueType.UInt32, ChartValueType.UInt64
                tagBgColor = Color.PowderBlue
                indicatorText = "123"

            ' Date & Time Types
            Case ChartValueType.Date, ChartValueType.DateTime, ChartValueType.Time,
                 ChartValueType.DateTimeOffset
                tagBgColor = Color.LightGreen
                indicatorText = "📅"

            ' Text / Nominal / Other Types
            Case ChartValueType.String
                tagBgColor = Color.LightPink
                indicatorText = "Abc"
        End Select

        ' 3. Paint the preview indicator badge
        Using g As Graphics = e.Graphics
            Using bgBrush As New SolidBrush(tagBgColor)
                g.FillRectangle(bgBrush, tagRect)
            End Using
            g.DrawRectangle(Pens.Gray, tagRect)

            ' Draw the preview symbol/text inside the badge
            Using tagFont As New Font(e.Font.FontFamily, e.Font.Size - 1.5F, FontStyle.Bold)
                Using sfCenter As New StringFormat() With {
                    .Alignment = StringAlignment.Center,
                    .LineAlignment = StringAlignment.Center
                }
                    g.DrawString(indicatorText, tagFont, Brushes.Black, tagRect, sfCenter)
                End Using
            End Using

            ' 4. Draw the actual ChartValueType enumeration name text
            Dim textX As Integer = tagRect.Right + 6
            Dim textRect As New Rectangle(textX, bounds.Y, bounds.Width - textX, bounds.Height)

            ' Ensure readability against selected highlight vs normal background
            Dim textColor As Color = If((e.State And DrawItemState.Selected) = DrawItemState.Selected,
                                        SystemColors.HighlightText, SystemColors.WindowText)

            Using textBrush As New SolidBrush(textColor)
                Using sfLeft As New StringFormat() With {.LineAlignment = StringAlignment.Center}
                    g.DrawString(itemValue.ToString(), e.Font, textBrush, textRect, sfLeft)
                End Using
            End Using
        End Using
    End Sub
End Class
