Imports System.ComponentModel
Imports System.Windows.Forms.DataVisualization.Charting

Public Class ComboChartTickMarkStylePicker
    Inherits ComboBox

    Private Const kItemHeightDefault As Integer = 18

    ''' <summary>
    ''' Initializes a new instance of the ChartTickMarkStyleComboBox control.
    ''' </summary>
    Public Sub New()
        MyBase.New()
        InitializeComponent()

        Me.DropDownStyle = ComboBoxStyle.DropDownList
        Me.ItemHeight = kItemHeightDefault

        If Not Me.DesignMode AndAlso System.ComponentModel.LicenseManager.UsageMode = System.ComponentModel.LicenseUsageMode.Runtime Then
            Me.DataSource = [Enum].GetValues(GetType(TickMarkStyle))
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

    ''' <summary>
    ''' Gets or sets the strongly-typed ChartTickMarkStyle value selected in the control.
    ''' </summary>
    <Browsable(True)>
    <Category("Appearance")>
    <Description("Gets or sets the selected chart axis tick mark display style.")>
    Public Property TickMarkStyle As TickMarkStyle
        Get
            If Me.SelectedItem IsNot Nothing AndAlso TypeOf Me.SelectedItem Is TickMarkStyle Then
                Return CType(Me.SelectedItem, TickMarkStyle)
            End If
            Return TickMarkStyle.OutsideArea
        End Get
        Set(value As TickMarkStyle)
            Me.SelectedItem = value
        End Set
    End Property

    ''' <summary>
    ''' Formats the enum style items into clean human-readable option labels.
    ''' </summary>
    Protected Overrides Sub OnFormat(e As ListControlConvertEventArgs)
        If TypeOf e.ListItem Is TickMarkStyle Then
            Dim style As TickMarkStyle = CType(e.ListItem, TickMarkStyle)
            e.Value = GetFriendlyString(style)
        Else
            MyBase.OnFormat(e)
        End If
    End Sub

    ''' <summary>
    ''' Helper method to map tick mark styles to explicit descriptive text strings.
    ''' </summary>
    Private Function GetFriendlyString(style As TickMarkStyle) As String
        Select Case style
            Case TickMarkStyle.None
                Return "None (Hidden)"
            Case TickMarkStyle.OutsideArea
                Return "Outside Chart Area"
            Case TickMarkStyle.InsideArea
                Return "Inside Chart Area"
            Case TickMarkStyle.AcrossAxis
                Return "Across Axis Line"
            Case Else
                Return style.ToString()
        End Select
    End Function
End Class
