Imports System.ComponentModel

Public Class ComboChartAntiAliasingPicker
    Inherits ComboBox

    <Flags>
    Public Enum ChartAntiAliasingStyles
        ''' <summary>
        ''' No anti-aliasing is applied to any chart elements.
        ''' </summary>
        None = 0

        ''' <summary>
        ''' Anti-aliasing is applied explicitly when rendering text and labels.
        ''' </summary>
        Text = 1

        ''' <summary>
        ''' Anti-aliasing is applied explicitly when rendering graphics primitives (lines, shapes).
        ''' </summary>
        Graphics = 2

        ''' <summary>
        ''' Anti-aliasing is enabled for all chart components.
        ''' </summary>
        All = Text Or Graphics
    End Enum

    Private Const kItemHeightDefault As Integer = 18

    Public Sub New()
        MyBase.New
        InitializeComponent()

        Me.DropDownStyle = ComboBoxStyle.DropDownList
        Me.ItemHeight = kItemHeightDefault

        Me.DataSource = [Enum].GetValues(GetType(ChartAntiAliasingStyles))
    End Sub

    ''' <summary>
    ''' Gets or sets the strongly-typed ChartAntiAliasingStyles value selected in the control.
    ''' </summary>
    ''' <returns>ChartAntiAliasingStyles</returns>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    <Browsable(False)>
    Public Property AntiAliasingStyle As ChartAntiAliasingStyles
        Get
            If Me.SelectedItem IsNot Nothing AndAlso TypeOf Me.SelectedItem Is ChartAntiAliasingStyles Then
                Return CType(Me.SelectedItem, ChartAntiAliasingStyles)
            End If
            Return ChartAntiAliasingStyles.None
        End Get
        Set(value As ChartAntiAliasingStyles)
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
    ''' Formats the enum flag items into human-readable text expressions for the UI layout.
    ''' </summary>
    ''' <param name="e"></param>
    Protected Overrides Sub OnFormat(e As ListControlConvertEventArgs)
        If TypeOf e.ListItem Is ChartAntiAliasingStyles Then
            Dim style As ChartAntiAliasingStyles = CType(e.ListItem, ChartAntiAliasingStyles)
            e.Value = GetFriendlyString(style)
        Else
            MyBase.OnFormat(e)
        End If
    End Sub

    Private Function GetFriendlyString(style As ChartAntiAliasingStyles) As String
        If style = ChartAntiAliasingStyles.None Then
            Return "Disabled (Fastest Performance)"
        End If
        If style = ChartAntiAliasingStyles.All Then
            Return "Full Quality (Text & Graphics Enabled)"
        End If

        Dim components As New List(Of String)()

        ' Evaluate specific bitwise combinations safely
        If (style And ChartAntiAliasingStyles.Text) = ChartAntiAliasingStyles.Text Then
            components.Add("Text Smoothing")
        End If
        If (style And ChartAntiAliasingStyles.Graphics) = ChartAntiAliasingStyles.Graphics Then
            components.Add("Graphics Vector Smoothing")
        End If

        Return String.Join(" + ", components)
    End Function
End Class
