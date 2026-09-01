Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports System.Globalization

''' <summary>
''' A Toolbox-ready UserControl combining a borderless TextBox and an autonomous GDI+ 
''' line preview panel inside a single, unified text box layout wrapper container.
''' </summary>
<ToolboxItem(True)>
<Description("A single-line input control that resembles a standard TextBox with an integrated live line preview section.")>
Public Class DashPatternPicker
    Inherits UserControl

    ' UI Elements built programmatically to maintain single-file asset portability
    Private WithEvents mTxtPatternInput As TextBox
    Private mDashPattern As Single() = New Single() {4.0F, 2.0F} ' Default pattern configuration (Dash 4, Space 2)

    Public Sub New()
        MyBase.New()
        InitializeControlLayout()
    End Sub

    ''' <summary>
    ''' Instantiates internal child layout elements programmatically.
    ''' </summary>
    Private Sub InitializeControlLayout()
        ' Set default background color to white to look identical to a native standard TextBox field
        Me.BackColor = Color.FromKnownColor(KnownColor.Window)
        Me.Padding = New Padding(1)

        ' Initialize default width and structural parameters cleanly
        Me.Width = 200

        ' Instantiate and configure input text fields cleanly without an internal border
        mTxtPatternInput = New TextBox()
        mTxtPatternInput.BorderStyle = BorderStyle.None
        mTxtPatternInput.Text = "4, 2"

        ' Embed controls inside the parent layout base container hierarchy
        Me.Controls.Add(mTxtPatternInput)
    End Sub

    ''' <summary>
    ''' Automatically adjusts layout dimensions at design time and runtime to ensure 
    ''' the control height matches standard TextBox sizing profiles exactly.
    ''' </summary>
    Protected Overrides Sub OnLayout(ByVal layoutEvent As LayoutEventArgs)
        MyBase.OnLayout(layoutEvent)

        If mTxtPatternInput IsNot Nothing Then
            ' Dynamically anchor the overall control height to mirror the text box's text font height
            Me.Height = mTxtPatternInput.Height + 6

            ' Align the borderless entry box cleanly to the left edge region
            mTxtPatternInput.Location = New Point(5, 3)
            mTxtPatternInput.Width = (Me.Width \ 2) - 10
        End If
    End Sub

    ''' <summary>
    ''' Gets or sets the compiled array of Singles representing custom lengths for sequential dashes and spaces.
    ''' </summary>
    <Category("Behavior")>
    <Description("The validated floating-point array configuration mapping dash/space pattern sequences.")>
    Public Property DashPattern() As Single()
        Get
            Return mDashPattern
        End Get
        Set(ByVal value As Single())
            If value IsNot Nothing AndAlso value.Length > 0 Then
                mDashPattern = value

                ' Block event processing temporarily to safely synchronize UI text without circular loops
                RemoveHandler mTxtPatternInput.TextChanged, AddressOf txtPatternInput_TextChanged
                mTxtPatternInput.Text = String.Join(", ", mDashPattern)
                AddHandler mTxtPatternInput.TextChanged, AddressOf txtPatternInput_TextChanged

                Me.Invalidate() ' Force redraw of the local vector illustration area
            End If
        End Set
    End Property

    ''' <summary>
    ''' Internal change parsing engine triggered on text manipulation sequence changes.
    ''' </summary>
    Private Sub txtPatternInput_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles mTxtPatternInput.TextChanged
        Dim rawText As String = mTxtPatternInput.Text.Trim()

        ' If string is empty, fallback silently without breaking runtime execution loops
        If String.IsNullOrEmpty(rawText) Then Return

        ' Split entries flexibly using either comma dividers or simple empty spacing sequences
        Dim valueTokens As String() = rawText.Split(New Char() {","c, " "c}, StringSplitOptions.RemoveEmptyEntries)
        Dim uniqueCollection As New List(Of Single)()

        For Each token In valueTokens
            Dim parsedValue As Single
            ' Force invariant culture conversions to ensure periods function uniformly across worldwide systems
            If Single.TryParse(token, NumberStyles.Any, CultureInfo.InvariantCulture, parsedValue) Then
                ' Win32 GDI+ cores strictly demand dash parameters greater than zero 
                If parsedValue > 0.0F Then
                    uniqueCollection.Add(parsedValue)
                End If
            End If
        Next

        ' Only commit updates if parsing returns a functional array length greater than zero elements
        If uniqueCollection.Count > 0 Then
            mDashPattern = uniqueCollection.ToArray()
            Me.Invalidate() ' Repaint the preview block container instantly
        End If
    End Sub

    ''' <summary>
    ''' Visual custom rendering thread overlay to wrap borders and paint live patterns.
    ''' </summary>
    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        MyBase.OnPaint(e)
        Dim g As Graphics = e.Graphics

        ' 1. Draw a unified window frame border surrounding the entire control boundary
        Using borderPen As New Pen(Color.FromKnownColor(KnownColor.WindowFrame))
            Dim frameRect As New Rectangle(0, 0, Me.Width - 1, Me.Height - 1)
            g.DrawRectangle(borderPen, frameRect)

            ' Draw a clean internal vertical split divider line right after the input text box field ends
            Dim dividerX As Integer = mTxtPatternInput.Right + 4
            g.DrawLine(borderPen, dividerX, 0, dividerX, Me.Height)
        End Using

        ' 2. Calculate the precise canvas footprint for the preview panel space on the right side
        Dim previewLeft As Integer = mTxtPatternInput.Right + 5
        Dim previewWidth As Integer = Me.Width - previewLeft - 1
        Dim previewArea As New Rectangle(previewLeft, 1, previewWidth, Me.Height - 2)

        ' Render a subtle gray background plate to call out the preview section clearly
        Using previewBg As New SolidBrush(Color.FromArgb(245, 245, 245))
            g.FillRectangle(previewBg, previewArea)
        End Using

        ' 3. Render the active vector dash segment pattern cleanly down the middle center y-axis path
        If mDashPattern IsNot Nothing AndAlso mDashPattern.Length > 0 Then
            Dim priorSmoothing As SmoothingMode = g.SmoothingMode
            g.SmoothingMode = SmoothingMode.AntiAlias

            Using customLinePen As New Pen(Me.ForeColor, 2)
                Try
                    customLinePen.DashStyle = DashStyle.Custom
                    customLinePen.DashPattern = mDashPattern

                    Dim verticalCenter As Integer = Me.Height \ 2
                    g.DrawLine(customLinePen, previewArea.Left + 6, verticalCenter, previewArea.Right - 6, verticalCenter)
                Catch
                    ' Draw a dashed red indicator line to notify users of broken pattern inputs mid-keystroke
                    Dim verticalCenter As Integer = Me.Height \ 2
                    Using invalidInputPen As New Pen(Color.Red, 1) With {.DashStyle = DashStyle.Dot}
                        g.DrawLine(invalidInputPen, previewArea.Left + 6, verticalCenter, previewArea.Right - 6, verticalCenter)
                    End Using
                End Try
            End Using

            g.SmoothingMode = priorSmoothing
        End If
    End Sub
End Class
