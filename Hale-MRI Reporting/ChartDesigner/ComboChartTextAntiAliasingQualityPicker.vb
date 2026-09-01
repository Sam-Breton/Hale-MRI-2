Imports System.ComponentModel
Imports System.Windows.Forms.DataVisualization.Charting

Public Class ComboChartTextAntiAliasingQualityPicker
    Inherits ComboBox

    Private Const kItemHeightDefault As Integer = 18

    Public Sub New()
        MyBase.New
        InitializeComponent()

        Me.DropDownStyle = ComboBoxStyle.DropDownList
        Me.ItemHeight = kItemHeightDefault

        Me.DataSource = [Enum].GetValues(GetType(TextAntiAliasingQuality))
    End Sub

    ''' <summary>
    ''' Gets or sets the strongly-typed TextAntiAliasingQuality value selected in the control.
    ''' </summary>
    ''' <returns>TextAntiAliasingQuality</returns>
    Public Property AntiAliasingQuality As TextAntiAliasingQuality
        Get
            If Me.SelectedItem IsNot Nothing AndAlso TypeOf Me.SelectedItem Is TextAntiAliasingQuality Then
                Return CType(Me.SelectedItem, TextAntiAliasingQuality)
            End If
            Return TextAntiAliasingQuality.SystemDefault
        End Get
        Set(value As TextAntiAliasingQuality)
            If Me.DataSource IsNot Nothing Then
                Me.SelectedItem = value
            End If
        End Set
    End Property

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
    ''' Formats the enum quality items into friendly string labels for presentation.
    ''' </summary>
    Protected Overrides Sub OnFormat(e As ListControlConvertEventArgs)
        If TypeOf e.ListItem Is TextAntiAliasingQuality Then
            Dim quality As TextAntiAliasingQuality = CType(e.ListItem, TextAntiAliasingQuality)
            e.Value = GetFriendlyString(quality)
        Else
            MyBase.OnFormat(e)
        End If
    End Sub

    ''' <summary>
    ''' Helper method to map quality modes to explicit performance descriptions.
    ''' </summary>
    Private Function GetFriendlyString(quality As TextAntiAliasingQuality) As String
        Select Case quality
            Case TextAntiAliasingQuality.SystemDefault
                Return "System Default (OS Managed)"
            Case TextAntiAliasingQuality.Normal
                Return "Normal Quality (Balanced Performance)"
            Case TextAntiAliasingQuality.High
                Return "High Quality (Smoothest Rendering)"
            Case Else
                Return quality.ToString()
        End Select
    End Function
End Class
