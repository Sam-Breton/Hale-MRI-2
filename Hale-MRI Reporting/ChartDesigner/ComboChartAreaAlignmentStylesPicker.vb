Imports System.ComponentModel
Imports System.Text
Imports System.Windows.Forms.DataVisualization.Charting

Public Class ComboChartAreaAlignmentStylesPicker
    Inherits ComboBox

    Private Class StyleItem
        Public Property StyleValue As AreaAlignmentStyles
        Public Property IsChecked As Boolean

        Public Sub New(value As AreaAlignmentStyles)
            Me.StyleValue = value
            Me.IsChecked = False
        End Sub

        Public Overrides Function ToString() As String
            Return Me.StyleValue.ToString()
        End Function
    End Class

    Private Const kItemHeightDefault As Integer = 18
    Private Const kPenWidthDefault As Single = 1.0!
    Private Const kCheckRectOffsetBottom As Integer = 3
    Private Const kCheckRectOffsetHeight As Integer = -8
    Private Const kCheckRectOffsetTop As Integer = 3
    Private Const kCheckRectOffsetX As Integer = 4
    Private Const kCheckRectOffsetY As Integer = 4
    Private Const kCheckRectWidthDefault As Integer = 14
    Private Const kTextBrushOffsetRight As Integer = 8
    Private Const kTextRectOffsetHeight As Integer = -5
    Private Const kTextRectOffsetWidth As Integer = -5
    Private Const kTextRectOffsetX As Integer = 3
    Private Const kTextRectOffsetY As Integer = 3

    Private mItemsList As New List(Of StyleItem)()
    Private mUpdatingText As Boolean = False

    Public Sub New()
        MyBase.New()
        InitializeComponent()

        ' Set required rendering modes for custom owner-drawn styling
        Me.DrawMode = DrawMode.OwnerDrawFixed
        Me.ItemHeight = kItemHeightDefault

        ' CRITICAL: Only populate data when the program is actually running
        If Not Me.DesignMode AndAlso System.ComponentModel.LicenseManager.UsageMode = System.ComponentModel.LicenseUsageMode.Runtime Then
            ' Populate inner structure with standard individual alignment flag states
            ' Skips duplicating complex automated composite variants for cleaner rendering
            mItemsList.Add(New StyleItem(AreaAlignmentStyles.None))
            mItemsList.Add(New StyleItem(AreaAlignmentStyles.Position))
            mItemsList.Add(New StyleItem(AreaAlignmentStyles.PlotPosition))
            mItemsList.Add(New StyleItem(AreaAlignmentStyles.AxesView))
            mItemsList.Add(New StyleItem(AreaAlignmentStyles.Cursor))
            mItemsList.Add(New StyleItem(AreaAlignmentStyles.All))

            ' Bind internal data lists
            Me.Items.AddRange(mItemsList.ToArray())
            UpdateTextSummary()
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

    <Category("Behavior")>
    Public Property AlignmentStyle As AreaAlignmentStyles
        Get
            Dim combinedFlags As AreaAlignmentStyles = AreaAlignmentStyles.None
            For Each item In mItemsList
                If item.IsChecked Then
                    combinedFlags = combinedFlags Or item.StyleValue
                End If
            Next
            Return combinedFlags
        End Get
        Set(value As AreaAlignmentStyles)
            ' Special case handling for 'All' flag assignment
            If value = AreaAlignmentStyles.All Then
                For Each item In mItemsList
                    item.IsChecked = True
                Next
            Else
                For Each item In mItemsList
                    If item.StyleValue = AreaAlignmentStyles.All Then
                        item.IsChecked = (value = AreaAlignmentStyles.All)
                    ElseIf item.StyleValue = AreaAlignmentStyles.None Then
                        item.IsChecked = (value = AreaAlignmentStyles.None)
                    Else
                        item.IsChecked = (value And item.StyleValue) = item.StyleValue
                    End If
                Next
            End If
            UpdateTextSummary()
            Me.Refresh()
        End Set
    End Property

    Private Sub UpdateTextSummary()
        mUpdatingText = True
        Dim sb As New StringBuilder()

        For Each item In mItemsList
            If item.IsChecked AndAlso item.StyleValue <> AreaAlignmentStyles.All AndAlso item.StyleValue <> AreaAlignmentStyles.None Then
                If sb.Length > 0 Then sb.Append(", ")
                sb.Append(item.StyleValue.ToString())
            End If
        Next

        If sb.Length = 0 Then
            ' Check fallback assignments
            Dim allItem = mItemsList.Find(Function(i) i.StyleValue = AreaAlignmentStyles.All)
            If allItem IsNot Nothing AndAlso allItem.IsChecked Then
                Me.Text = "All"
            Else
                Me.Text = "None"
            End If
        Else
            Me.Text = sb.ToString()
        End If
        mUpdatingText = False
    End Sub

    Protected Overrides Sub OnDrawItem(e As DrawItemEventArgs)
        If e.Index < 0 Then Return

        Dim g As Graphics = e.Graphics
        Dim currentItem As StyleItem = CType(Me.Items(e.Index), StyleItem)

        ' Render standard background color transitions native to Windows selection highlight themes
        e.DrawBackground()

        ' Draw a custom check-box toggle diagram container block
        Dim chkRect As New Rectangle(e.Bounds.X + kCheckRectOffsetX, e.Bounds.Y + kCheckRectOffsetY, kCheckRectWidthDefault,
                                     e.Bounds.Height + kCheckRectOffsetHeight)
        Using p As New Pen(e.ForeColor, kPenWidthDefault)
            g.DrawRectangle(p, chkRect)
        End Using

        ' Draw the internal indicator anchor point if the specific list flag is true
        If currentItem.IsChecked Then
            Using b As New SolidBrush(e.ForeColor)
                g.FillRectangle(b, chkRect.X + kTextRectOffsetX, chkRect.Y + kTextRectOffsetY, chkRect.Width + kTextRectOffsetHeight,
                                chkRect.Height + kTextRectOffsetWidth)
            End Using
        End If

        ' Render the string text label matching the target flag index identifier block
        Using textBrush As New SolidBrush(e.ForeColor)
            Dim textX As Integer = chkRect.Right + kTextBrushOffsetRight
            Dim textY As Integer = e.Bounds.Y + ((e.Bounds.Height - g.MeasureString(currentItem.ToString(), Me.Font).Height) / 2)
            g.DrawString(currentItem.ToString(), Me.Font, textBrush, textX, textY)
        End Using

        ' Render basic native dotted focus boundaries
        e.DrawFocusRectangle()
    End Sub

    Protected Overrides Sub OnSelectedIndexChanged(e As EventArgs)
        If Me.SelectedIndex >= 0 AndAlso Not mUpdatingText Then
            Dim clickedItem As StyleItem = CType(Me.Items(Me.SelectedIndex), StyleItem)

            ' Define unique cascading behavior checks for 'None' and 'All'
            If clickedItem.StyleValue = AreaAlignmentStyles.None Then
                For Each item In mItemsList
                    item.IsChecked = (item.StyleValue = AreaAlignmentStyles.None)
                Next
            ElseIf clickedItem.StyleValue = AreaAlignmentStyles.All Then
                For Each item In mItemsList
                    item.IsChecked = (item.StyleValue <> AreaAlignmentStyles.None)
                Next
            Else
                clickedItem.IsChecked = Not clickedItem.IsChecked
                ' Automatically clean up opposite state tags
                Dim noneItem = mItemsList.Find(Function(i) i.StyleValue = AreaAlignmentStyles.None)
                If noneItem IsNot Nothing Then noneItem.IsChecked = False
            End If

            UpdateTextSummary()
            Me.Refresh()
        End If

        MyBase.OnSelectedIndexChanged(e)
    End Sub
End Class
