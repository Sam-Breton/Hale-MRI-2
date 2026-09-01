Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports System.Reflection
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class Themes
    Implements ICloneable
#Region "Types and Constants"
    ''' <summary>
    ''' Event raised whenever a property value changes.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs)
#End Region
#Region "Private Members"
    Private mDisplayFieldBackColor As Color = Nothing
    Private mDisplayFieldBorder As Boolean = False
    Private mDisplayFieldFont As Font = Nothing
    Private mDisplayFieldFontColor As Color = Nothing
    Private mDisplayLabelBackColor As Color = Nothing
    Private mDisplayLabelBorder As Boolean = False
    Private mDisplayLabelFont As Font = Nothing
    Private mDisplayLabelFontColor As Color = Nothing
    Private mFormBackColor As Color = Nothing
    Private mFormBorderStyle As FormBorderStyle = Nothing
    Private mFormFont As Font = Nothing
    Private mFormFontColor As Color = Nothing
    Private mFormText As String = Nothing
    Private mGroupingBorderColor As Color = Nothing
    Private mGroupingBorderWidth As Integer = 0
    Private mGroupingBorderDashPattern() As Single = Nothing
    Private mGroupingBorderDashStyle As DashStyle = Nothing
    Private mHeadingActiveColor As Color = Nothing
    Private mHeadingFont As Font = Nothing
    Private mHeadingFontColor As Color = Nothing
    Private mHeadingInactiveColor As Color = Nothing
    Private mHeadingVisible As Boolean = False
#End Region
#Region "Constructors"
    ''' <summary>
    ''' Default constructor.
    ''' </summary>
    Public Sub New()

    End Sub

    ''' <summary>
    ''' Initializing constructor.
    ''' </summary>
    ''' <param name="displayFieldFont"></param>
    ''' <param name="displayLabelFont"></param>
    ''' <param name="formBackColor"></param>
    ''' <param name="formFont"></param>
    ''' <param name="formFontColor"></param>
    ''' <param name="groupingBorderColor"></param>
    ''' <param name="groupingBorderWidth"></param>
    ''' <param name="headingActiveColor"></param>
    ''' <param name="headingFont"></param>
    ''' <param name="headingFontColor"></param>
    ''' <param name="headingInactiveColor"></param>
    Public Sub New(displayFieldBackColor As Color, displayFieldBorder As Boolean, displayFieldFont As Font, displayFieldFontColor As Color,
                   displayLabelBackColor As Color, displayLabelBorder As Boolean, displayLabelFont As Font, displayLabelFontColor As Color,
                   formBackColor As Color, formFont As Font, formFontColor As Color, formText As String, groupingBorderColor As Color,
                   groupingBorderWidth As Integer, headingActiveColor As Color, headingFont As Font, headingFontColor As Color,
                   headingInactiveColor As Color, headingVisible As Boolean)
        Me.DisplayFieldBackColor = displayFieldBackColor
        Me.DisplayFieldBorder = displayFieldBorder
        Me.DisplayFieldFont = displayFieldFont
        Me.DisplayFieldFontColor = displayFieldFontColor
        Me.DisplayLabelBackColor = displayLabelBackColor
        Me.DisplayLabelBorder = displayLabelBorder
        Me.DisplayLabelFont = displayLabelFont
        Me.DisplayLabelFontColor = displayLabelFontColor
        Me.FormBackColor = formBackColor
        Me.FormFont = formFont
        Me.FormFontColor = formFontColor
        Me.FormText = formText
        Me.GroupingBorderColor = groupingBorderColor
        Me.GroupingBorderWidth = groupingBorderWidth
        Me.HeadingActiveColor = headingActiveColor
        Me.HeadingFont = headingFont
        Me.HeadingFontColor = headingFontColor
        Me.HeadingInactiveColor = headingInactiveColor
        Me.HeadingVisible = headingVisible
    End Sub

    Public Function Clone() As Object Implements ICloneable.Clone
        ' Create a new instance of the current class type
        Dim newInstance As Object = Activator.CreateInstance(Me.GetType())

        ' Get all public instance properties
        Dim properties As PropertyInfo() = Me.GetType().GetProperties(BindingFlags.Public Or BindingFlags.Instance)

        For Each prop As PropertyInfo In properties
            ' Ensure the property can be read from the source and written to the destination
            If prop.CanRead AndAlso prop.CanWrite Then
                Dim value As Object = prop.GetValue(Me, Nothing)
                prop.SetValue(newInstance, value, Nothing)
            End If
        Next

        Return newInstance
    End Function

    Public Sub New(other As Themes)
        Me.DisplayFieldBackColor = other.DisplayFieldBackColor
        Me.DisplayFieldBorder = other.DisplayFieldBorder
        Me.DisplayFieldFont = other.DisplayFieldFont
        Me.DisplayFieldFontColor = other.DisplayFieldFontColor
        Me.DisplayLabelBackColor = other.DisplayLabelBackColor
        Me.DisplayLabelBorder = other.DisplayLabelBorder
        Me.DisplayLabelFont = other.DisplayLabelFont
        Me.DisplayLabelFontColor = other.DisplayLabelFontColor
        Me.FormBackColor = other.FormBackColor
        Me.FormFont = other.FormFont
        Me.FormFontColor = other.FormFontColor
        Me.FormText = other.FormText
        Me.GroupingBorderColor = other.GroupingBorderColor
        Me.GroupingBorderWidth = other.GroupingBorderWidth
        Me.HeadingActiveColor = other.HeadingActiveColor
        Me.HeadingFont = other.HeadingFont
        Me.HeadingFontColor = other.HeadingFontColor
        Me.HeadingInactiveColor = other.HeadingInactiveColor
        Me.HeadingVisible = other.HeadingVisible
    End Sub
#End Region
#Region "Public Interface"
    ''' <summary>
    ''' BackColor used in data bound Controls.
    ''' </summary>
    ''' <returns>Color</returns>
    Public Property DisplayFieldBackColor As Color
        Get
            Return mDisplayFieldBackColor
        End Get
        Set(value As Color)
            If mDisplayFieldBackColor <> value Then
                mDisplayFieldBackColor = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("DisplayFieldBackColor"))
            End If
        End Set
    End Property

    ''' <summary>
    ''' Flag indicating whether a border is used in data bound Controls.
    ''' </summary>
    ''' <returns>Boolean</returns>
    Public Property DisplayFieldBorder As Boolean
        Get
            Return mDisplayFieldBorder
        End Get
        Set(value As Boolean)
            If mDisplayFieldBorder <> value Then
                mDisplayFieldBorder = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("DisplayFieldBorder"))
            End If
        End Set
    End Property

    ''' <summary>
    ''' Font used in data bound Controls.
    ''' </summary>
    ''' <returns>Font</returns>
    Public Property DisplayFieldFont As Font
        Get
            Return mDisplayFieldFont
        End Get
        Set(value As Font)
            If mDisplayFieldFont IsNot value Then
                mDisplayFieldFont = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("DisplayFieldFont"))
            End If
        End Set
    End Property

    ''' <summary>
    ''' Font used in data bound TextBoxes.
    ''' </summary>
    ''' <returns>Color</returns>
    Public Property DisplayFieldFontColor As Color
        Get
            Return mDisplayFieldFontColor
        End Get
        Set(value As Color)
            If mDisplayFieldFontColor <> value Then
                mDisplayFieldFontColor = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("DisplayFieldFontColor"))
            End If
        End Set
    End Property

    ''' <summary>
    ''' BackColor used in data bound Controls.
    ''' </summary>
    ''' <returns>Color</returns>
    Public Property DisplayLabelBackColor As Color
        Get
            Return mDisplayLabelBackColor
        End Get
        Set(value As Color)
            If mDisplayLabelBackColor <> value Then
                mDisplayLabelBackColor = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("DisplayLabelBackColor"))
            End If
        End Set
    End Property

    ''' <summary>
    ''' Flag indicating whether a border is used in data bound Controls.
    ''' </summary>
    ''' <returns>Boolean</returns>
    Public Property DisplayLabelBorder As Boolean
        Get
            Return mDisplayLabelBorder
        End Get
        Set(value As Boolean)
            If mDisplayLabelBorder <> value Then
                mDisplayLabelBorder = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("DisplayLabelBorder"))
            End If
        End Set
    End Property

    ''' <summary>
    ''' Font used in Labels of data bound TextBoxes.
    ''' </summary>
    ''' <returns></returns>
    Public Property DisplayLabelFont As Font
        Get
            Return mDisplayLabelFont
        End Get
        Set(value As Font)
            If mDisplayLabelFont IsNot value Then
                mDisplayLabelFont = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("DisplayLabelFont"))
            End If
        End Set
    End Property

    ''' <summary>
    ''' Font used in Labels of data bound TextBoxes.
    ''' </summary>
    ''' <returns>Color</returns>
    Public Property DisplayLabelFontColor As Color
        Get
            Return mDisplayLabelFontColor
        End Get
        Set(value As Color)
            If mDisplayLabelFontColor <> value Then
                mDisplayLabelFontColor = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("DisplayLabelFontColor"))
            End If
        End Set
    End Property

    ''' <summary>
    ''' Background color of the client Form.
    ''' </summary>
    ''' <returns>Color</returns>
    Public Property FormBackColor As Color
        Get
            Return mFormBackColor
        End Get
        Set(value As Color)
            If mFormBackColor <> value Then
                mFormBackColor = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("FormBackColor"))
            End If
        End Set
    End Property

    ''' <summary>
    ''' Client Form's FormBorderStyle
    ''' </summary>
    ''' <returns></returns>
    Public Property FormBorderStyle As FormBorderStyle
        Get
            Return mFormBorderStyle
        End Get
        Set(value As FormBorderStyle)
            mFormBorderStyle = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("FormBorderStyle"))
        End Set
    End Property

    ''' <summary>
    ''' Font used in the client Form.
    ''' </summary>
    ''' <returns>Font</returns>
    Public Property FormFont As Font
        Get
            Return mFormFont
        End Get
        Set(value As Font)
            If mFormFont IsNot value Then
                mFormFont = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("FormFont"))
            End If
        End Set
    End Property

    ''' <summary>
    ''' Font color used in the client Form.
    ''' </summary>
    ''' <returns>Color</returns>
    Public Property FormFontColor As Color
        Get
            Return mFormFontColor
        End Get
        Set(value As Color)
            If mFormFontColor <> value Then
                mFormFontColor = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("FormFontColor"))
            End If
        End Set
    End Property

    Public Property FormText As String
        Get
            Return mFormText
        End Get
        Set(value As String)
            mFormText = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("FormText"))
        End Set
    End Property

    ''' <summary>
    ''' Border color drawn around a ControlGroup.
    ''' </summary>
    ''' <returns>Color</returns>
    Public Property GroupingBorderColor As Color
        Get
            Return mGroupingBorderColor
        End Get
        Set(value As Color)
            If mGroupingBorderColor <> value Then
                mGroupingBorderColor = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("GroupingBorderColor"))
            End If
        End Set
    End Property

    ''' <summary>
    ''' Border DashPattern color drawn around a ControlGroup.
    ''' </summary>
    ''' <returns></returns>
    Public Property GroupingBorderDashPattern As Single()
        Get
            Return mGroupingBorderDashPattern
        End Get
        Set(value As Single())
            mGroupingBorderDashPattern = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("GroupingBorderDashPattern"))
        End Set
    End Property

    ''' <summary>
    ''' Border DashPattern color drawn around a ControlGroup.
    ''' </summary>
    ''' <returns></returns>
    Public Property GroupingBorderDashStyle As DashStyle
        Get
            Return mGroupingBorderDashStyle
        End Get
        Set(value As DashStyle)
            mGroupingBorderDashStyle = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("GroupingBorderDashStyle"))
        End Set
    End Property

    ''' <summary>
    ''' Border width drawn around a ControlGroup.
    ''' </summary>
    ''' <returns>Integer</returns>
    Public Property GroupingBorderWidth As Integer
        Get
            Return mGroupingBorderWidth
        End Get
        Set(value As Integer)
            If value < 0 Then value = 0
            If mGroupingBorderWidth <> value Then
                mGroupingBorderWidth = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("GroupingBorderWidth"))
            End If
        End Set
    End Property

    ''' <summary>
    ''' Background color of a ControlGroup heading when the group has focus.
    ''' </summary>
    ''' <returns>Color</returns>
    Public Property HeadingActiveColor As Color
        Get
            Return mHeadingActiveColor
        End Get
        Set(value As Color)
            If mHeadingActiveColor <> value Then
                mHeadingActiveColor = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("HeadingActiveColor"))
            End If
        End Set
    End Property

    ''' <summary>
    ''' Font used in a ControlGroup heading.
    ''' </summary>
    ''' <returns>Font</returns>
    Public Property HeadingFont As Font
        Get
            Return mHeadingFont
        End Get
        Set(value As Font)
            If mHeadingFont IsNot value Then
                mHeadingFont = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("HeadingFont"))
            End If
        End Set
    End Property

    ''' <summary>
    ''' Font color used in a ControlGroup heading.
    ''' </summary>
    ''' <returns>Color</returns>
    Public Property HeadingFontColor As Color
        Get
            Return mHeadingFontColor
        End Get
        Set(value As Color)
            If mHeadingFontColor <> value Then
                mHeadingFontColor = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("HeadingFontColor"))
            End If
        End Set
    End Property

    ''' <summary>
    ''' Background color of a ControlGroup heading when the group does not have focus.
    ''' </summary>
    ''' <returns>Color</returns>
    Public Property HeadingInactiveColor As Color
        Get
            Return mHeadingInactiveColor
        End Get
        Set(value As Color)
            If mHeadingInactiveColor <> value Then
                mHeadingInactiveColor = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("HeadingInactiveColor"))
            End If
        End Set
    End Property

    Public Property HeadingVisible As Boolean
        Get
            Return mHeadingVisible
        End Get
        Set(value As Boolean)
            If mHeadingVisible <> value Then
                mHeadingVisible = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("HeadingVisible"))
            End If
        End Set
    End Property
#End Region
End Class

Public Class ThemeManager
#Region "Types and Constants"
    Public Class GroupControl   ' Type that holds information about a Control that is part of a ControlGroup.
        Public Control As Control       ' A data bound Control.
        Public HasFocus As Boolean      ' Flag indicating whether the Control handles its ControlGroup's focus events.

        Public Sub New(ctrl As Control, focus As Boolean)
            Me.Control = ctrl
            Me.HasFocus = focus
        End Sub
    End Class

    Public Class ControlGroup               ' Type that holds information about a logical group of GroupControls.
        Public Heading As CustomLabel               ' The CustomLabel that displays the group heading.
        Public Container As CustomPanel             ' The CustomPanel that contains the GroupControls and renders the group borders.
        Public Fields As List(Of GroupControl)      ' The list of logically grouped, data bound GroupControls.
        Public Labels As List(Of Label)             ' The list of Labels attached the group's Fields.

        Public Sub New(heading As CustomLabel, container As CustomPanel, fields As List(Of GroupControl), labels As List(Of Label))
            Me.Heading = heading
            Me.Container = container
            Me.Fields = fields
            Me.Labels = labels
        End Sub
    End Class
#End Region
#Region "Private Members"
    Private mGroups As List(Of ControlGroup) = Nothing  ' The current list of ControlGroups.
    Private WithEvents mTheme As Themes = Nothing       ' The current theme.
#End Region
#Region "Constructors"
    ''' <summary>
    ''' Default constructor.
    ''' </summary>
    Public Sub New()

    End Sub

    ''' <summary>
    ''' Initializing constructor.
    ''' </summary>
    ''' <param name="form"></param>
    ''' <param name="groups"></param>
    ''' <param name="fields"></param>
    ''' <param name="labels"></param>
    ''' <param name="theme"></param>
    Public Sub New(form As Form, groups As List(Of ControlGroup), Optional fields As List(Of Control) = Nothing, Optional labels As List(Of Label) = Nothing, Optional theme As Themes = Nothing)
        Me.Form = form
        Me.Fields = fields
        Me.Groups = groups
        Me.Labels = labels
        Me.Theme = theme
    End Sub
#End Region
#Region "Public Interface"
    Public Property Fields As List(Of Control)      ' The current list of ungrouped Controls.

    Public Property Form As Form

    Public Property Groups As List(Of ControlGroup) ' The current list of ControlGroups.
        Get
            Return mGroups
        End Get
        Set(value As List(Of ControlGroup))
            For Each grp As ControlGroup In value
                For Each ctrl As GroupControl In grp.Fields
                    If ctrl.HasFocus AndAlso ctrl.Control IsNot Nothing Then
                        AddHandler ctrl.Control.Enter, AddressOf Me.Group_Enter
                        AddHandler ctrl.Control.Leave, AddressOf Me.Group_Leave
                    End If
                Next
            Next
            mGroups = value
        End Set
    End Property

    Public Property Labels As List(Of Label)        ' The list of ungrouped Labels.

    Public Property Theme As Themes
        Get
            Return mTheme
        End Get
        Set(value As Themes)
            mTheme = value
            If value IsNot Nothing Then ApplyTheme(value)
        End Set
    End Property
#End Region
#Region "Private Interface"
    Private Property ActiveGroup As ControlGroup = Nothing

    Private Function FindControlGroup(ctrl As Control) As ControlGroup
        If ctrl Is Nothing Then Return Nothing

        Return Me.Groups.FirstOrDefault(Function(cg)
                                            Return cg.Fields.Any(Function(gc) gc.Control Is ctrl)
                                        End Function)
    End Function

    Private Sub ApplyDisplayFieldBackColor()
        For Each grp As ControlGroup In Me.Groups
            If grp?.Fields IsNot Nothing Then
                For Each fld In grp.Fields
                    fld.Control.BackColor = Me.Theme.DisplayFieldBackColor
                Next
            End If
        Next
    End Sub

    Private Sub ApplyDisplayFieldBorder()
        For Each grp As ControlGroup In Me.Groups
            If grp?.Fields IsNot Nothing Then
                For Each fld In grp.Fields
                    GroupControlBorder(fld, If(Me.Theme.DisplayFieldBorder, BorderStyle.FixedSingle, BorderStyle.None))
                Next
            End If
        Next
    End Sub

    Private Sub ApplyDisplayFieldFont()
        For Each grp As ControlGroup In Me.Groups
            If grp?.Fields IsNot Nothing Then
                For Each fld In grp.Fields
                    fld.Control.Font = Me.Theme.DisplayFieldFont
                Next
            End If
        Next
    End Sub

    Private Sub ApplyDisplayFieldFontColor()
        For Each grp As ControlGroup In Me.Groups
            If grp?.Fields IsNot Nothing Then
                For Each fld In grp.Fields
                    fld.Control.ForeColor = Me.Theme.DisplayFieldFontColor
                Next
            End If
        Next
    End Sub

    Private Sub ApplyDisplayLabelBackColor()
        For Each grp As ControlGroup In Me.Groups
            If grp?.Labels IsNot Nothing Then
                For Each lab In grp.Labels
                    lab.BackColor = Me.Theme.DisplayLabelBackColor
                Next
            End If
        Next
    End Sub

    Private Sub ApplyDisplayLabelBorder()
        For Each grp As ControlGroup In Me.Groups
            If grp?.Labels IsNot Nothing Then
                For Each lab In grp.Labels
                    lab.BorderStyle = If(Me.Theme.DisplayLabelBorder, BorderStyle.FixedSingle, BorderStyle.None)
                Next
            End If
        Next
    End Sub

    Private Sub ApplyDisplayLabelFont()
        For Each grp As ControlGroup In Me.Groups
            If grp.Labels IsNot Nothing Then
                For Each lab In grp.Labels
                    lab.Font = Me.Theme.DisplayLabelFont
                Next
            End If
        Next
    End Sub

    Private Sub ApplyDisplayLabelFontColor()
        For Each grp As ControlGroup In Me.Groups
            If grp.Labels IsNot Nothing Then
                For Each lab In grp.Labels
                    lab.ForeColor = Me.Theme.DisplayLabelFontColor
                Next
            End If
        Next
    End Sub

    Private Sub ApplyFormBackColor()
        If Me.Form IsNot Nothing Then Me.Form.BackColor = Me.Theme.FormBackColor
    End Sub

    Private Sub ApplyFormBorderStyle()
        If Me.Form IsNot Nothing Then Me.Form.FormBorderStyle = Me.Theme.FormBorderStyle
    End Sub

    Private Sub ApplyFormFont()
        If Me.Form IsNot Nothing Then Me.Form.Font = Me.Theme.FormFont
    End Sub

    Private Sub ApplyFormFontColor()
        If Me.Form IsNot Nothing Then Me.Form.ForeColor = Me.Theme.FormFontColor
    End Sub

    Private Sub ApplyFormForeColor()
        If Me.Form IsNot Nothing Then Me.Form.ForeColor = Me.Theme.FormFontColor
    End Sub

    Private Sub ApplyFormText()
        If Me.Form IsNot Nothing Then Me.Form.Text = Me.Theme.FormText
    End Sub

    Private Sub ApplyGroupBorderColor()
        For Each grp As ControlGroup In Me.Groups
            If grp?.Container IsNot Nothing Then grp.Container.BorderColor = Me.Theme.GroupingBorderColor
        Next
    End Sub

    Private Sub ApplyGroupingBorderDashPattern()
        For Each grp As ControlGroup In Me.Groups
            If grp?.Container IsNot Nothing Then grp.Container.DashPattern = Me.Theme.GroupingBorderDashPattern
        Next
    End Sub

    Private Sub ApplyGroupingBorderDashStyle()
        For Each grp As ControlGroup In Me.Groups
            If grp?.Container IsNot Nothing Then grp.Container.DashStyle = Me.Theme.GroupingBorderDashStyle
        Next
    End Sub

    Private Sub ApplyGroupBorderWidth()
        For Each grp As ControlGroup In Me.Groups
            If grp?.Container IsNot Nothing Then grp.Container.BorderWidth = Me.Theme.GroupingBorderWidth
        Next
    End Sub

    Private Sub ApplyHeadingColor()
        For Each grp As ControlGroup In Me.Groups
            If grp?.Heading IsNot Nothing Then
                If grp Is ActiveGroup Then
                    grp.Heading.BackColor = Me.Theme.HeadingActiveColor
                Else
                    grp.Heading.BackColor = Me.Theme.HeadingInactiveColor
                End If
            End If
        Next
    End Sub

    Private Sub ApplyHeadingFont()
        For Each grp As ControlGroup In Me.Groups
            If grp?.Heading IsNot Nothing Then grp.Heading.Font = Me.Theme.HeadingFont
        Next
    End Sub

    Private Sub ApplyHeadingFontColor()
        For Each grp As ControlGroup In Me.Groups
            If grp?.Heading IsNot Nothing Then grp.Heading.ForeColor = Me.Theme.HeadingFontColor
        Next
    End Sub

    Private Sub ApplyHeadingVisible()
        For Each grp As ControlGroup In Me.Groups
            If grp?.Heading IsNot Nothing Then grp.Heading.Visible = Me.Theme.HeadingVisible
        Next
    End Sub

    Private Sub ApplyTheme(ByVal aTheme As Themes)
        ApplyDisplayFieldBackColor()
        ApplyDisplayFieldBorder()
        ApplyDisplayFieldFont()
        ApplyDisplayFieldFontColor()
        ApplyDisplayLabelBackColor()
        ApplyDisplayLabelBorder()
        ApplyDisplayLabelFont()
        ApplyDisplayLabelFontColor()
        ApplyFormBackColor()
        ApplyFormBorderStyle()
        ApplyFormFont()
        ApplyFormFontColor()
        ApplyFormText()
        ApplyGroupBorderColor()
        ApplyGroupingBorderDashPattern()
        ApplyGroupingBorderDashStyle()
        ApplyGroupBorderWidth()
        ApplyHeadingColor()
        ApplyHeadingFont()
        ApplyHeadingFontColor()
        ApplyHeadingVisible()
    End Sub

    Public Sub GroupControlBorder(groupCtrl As GroupControl, style As BorderStyle)
        If groupCtrl?.Control Is Nothing Then Return

        ' Matches the control against specific types that support BorderStyle
        Select Case True
        ' This allows a clean, comma-separated list of types
            Case TypeOf groupCtrl.Control Is Panel,
             TypeOf groupCtrl.Control Is PictureBox,
             TypeOf groupCtrl.Control Is System.Windows.Forms.TextBox,
             TypeOf groupCtrl.Control Is ListBox,
             TypeOf groupCtrl.Control Is UserControl

                ' Cast to Object/Object-reference to set the property via late-binding
                ' Note: This requires Option Strict Off for this specific file, OR using CallByName
                CallByName(groupCtrl.Control, "BorderStyle", CallType.Set, style)

            Case TypeOf groupCtrl.Control Is System.Windows.Forms.ComboBox
                ' ComboBox does not have a BorderStyle property, so we can handle it differently if needed
                ' For example, we could change the FlatStyle or other properties to simulate a border change
                Dim combo As System.Windows.Forms.ComboBox = DirectCast(groupCtrl.Control, System.Windows.Forms.ComboBox)
                If style = BorderStyle.FixedSingle Then
                    combo.FlatStyle = FlatStyle.Standard

                    ' Restores the default box region so system borders can draw normally again
                    combo.Region = Nothing
                Else
                    combo.FlatStyle = FlatStyle.Flat

                    ' 2. Clip the outer 2 pixels off all sides to completely throw away the white border line
                    ' This forces Windows to ignore the border painting sector entirely.
                    combo.Region = New Region(New Rectangle(2, 2, combo.Width - 4, combo.Height - 4))
                End If
            Case Else
                ' Do nothing
        End Select
    End Sub

    Private Function GroupHeadingColor(sender As Object, newColor As Color) As ControlGroup
        ' Applies the specified color to the heading of the ControlGroup that contains the Control that raised the event.
        Dim activeControl As Control = DirectCast(sender, Control)
        Dim parentGroup As ControlGroup = FindControlGroup(activeControl)

        If parentGroup?.Heading IsNot Nothing Then
            parentGroup.Heading.BackColor = newColor
        End If

        Return parentGroup
    End Function
#End Region
#Region "Event Handlers"
    Private Sub Group_Enter(sender As Object, e As EventArgs)
        ActiveGroup = GroupHeadingColor(sender, Me.Theme.HeadingActiveColor)
    End Sub

    Private Sub Group_Leave(sender As Object, e As EventArgs)
        GroupHeadingColor(sender, Me.Theme.HeadingInactiveColor)
        ActiveGroup = Nothing
    End Sub

    Private Sub Theme_PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Handles mTheme.PropertyChanged
        Select Case e.PropertyName
            Case "DisplayFieldBackColor"
                ApplyDisplayFieldBackColor()
            Case "DisplayFieldBorder"
                ApplyDisplayFieldBorder()
            Case "DisplayFieldFont"
                ApplyDisplayFieldFont()
            Case "DisplayFieldFontColor"
                ApplyDisplayFieldFontColor()
            Case "DisplayLabelBackColor"
                ApplyDisplayLabelBackColor()
            Case "DisplayLabelBorder"
                ApplyDisplayLabelBorder()
            Case "DisplayLabelFont"
                ApplyDisplayLabelFont()
            Case "DisplayLabelFontColor"
                ApplyDisplayLabelFontColor()
            Case "FormBackColor"
                ApplyFormBackColor()
            Case "FormBorderStyle"
                ApplyFormBorderStyle()
            Case "FormFont"
                ApplyFormFont()
            Case "FormFontColor"
                ApplyFormFontColor()
            Case "FormForeColor"
                ApplyFormForeColor()
            Case "FormText"
                ApplyFormText()
            Case "GroupingBorderColor"
                ApplyGroupBorderColor()
            Case "GroupingBorderDashPattern"
                ApplyGroupingBorderDashPattern()
            Case "GroupingBorderDashStyle"
                ApplyGroupingBorderDashStyle()
            Case "GroupingBorderWidth"
                ApplyGroupBorderWidth()
            Case "HeadingActiveColor", "HeadingInactiveColor"
                ApplyHeadingColor()
            Case "HeadingFont"
                ApplyHeadingFont()
            Case "HeadingFontColor"
                ApplyHeadingFontColor()
            Case "HeadingVisible"
                ApplyHeadingVisible()
            Case Else
        End Select
    End Sub
#End Region
End Class
