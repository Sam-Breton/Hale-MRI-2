<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmFormDesigner
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        GroupDataEntry = New GroupBox()
        GroupDataEntryFields = New GroupBox()
        ComboDataEntryFieldBackColor = New ComboColorPicker()
        LabDataEntryFieldFont = New Label()
        ChkDataEntryFieldBorder = New CheckBox()
        LabDataEntryFieldBackColor = New Label()
        CmdDataEntryFieldFont = New Button()
        GroupDataEntryLabels = New GroupBox()
        ComboDataEntryLabelBackColor = New ComboColorPicker()
        LabDataEntryLabelFont = New Label()
        ChkDataEntryLabelBorder = New CheckBox()
        LabDataEntryLabelBackColor = New Label()
        CmdDataEntryLabelFont = New Button()
        GroupForm = New GroupBox()
        GroupLayout = New GroupBox()
        RadioLayoutVertical = New RadioButton()
        RadioLayoutRectangle = New RadioButton()
        LabFormFont = New Label()
        LabFormBorderStyle = New Label()
        ComboFormBorderStyle = New ComboFormBorderStylePicker()
        LabFormText = New Label()
        TxtFormText = New TextBox()
        LabFormForeColor = New Label()
        ComboFormForeColor = New ComboColorPicker()
        ComboFormBackColor = New ComboColorPicker()
        CmdFormFont = New Button()
        LabFormBackColor = New Label()
        GroupGrouping = New GroupBox()
        LabGroupBordersDashPattern = New Label()
        UCGroupBordersDashPatternPicker = New DashPatternPicker()
        LabGroupBordersDashStyle = New Label()
        ComboGroupBordersDashStylePicker = New ComboDashStylePicker()
        LabGroupBordersWidth = New Label()
        NumericGroupBordersWidth = New NumericUpDown()
        ComboGroupBordersColor = New ComboColorPicker()
        LabGroupBordersColor = New Label()
        GroupTheme = New GroupBox()
        LabGroupHeadingsFont = New Label()
        ChkGroupHeadingsVisible = New CheckBox()
        ComboGroupHeadingsInactiveColor = New ComboColorPicker()
        ComboGroupHeadingsActiveColor = New ComboColorPicker()
        LabGroupHeadingsInactiveColor = New Label()
        CmdGroupHeadingsFont = New Button()
        LabGroupHeadingsActiveColor = New Label()
        CmdOK = New Button()
        CmdCancel = New Button()
        GroupDataEntry.SuspendLayout()
        GroupDataEntryFields.SuspendLayout()
        GroupDataEntryLabels.SuspendLayout()
        GroupForm.SuspendLayout()
        GroupLayout.SuspendLayout()
        GroupGrouping.SuspendLayout()
        CType(NumericGroupBordersWidth, ComponentModel.ISupportInitialize).BeginInit()
        GroupTheme.SuspendLayout()
        SuspendLayout()
        ' 
        ' GroupDataEntry
        ' 
        GroupDataEntry.Controls.Add(GroupDataEntryFields)
        GroupDataEntry.Controls.Add(GroupDataEntryLabels)
        GroupDataEntry.Location = New Point(11, 360)
        GroupDataEntry.Name = "GroupDataEntry"
        GroupDataEntry.Size = New Size(641, 115)
        GroupDataEntry.TabIndex = 13
        GroupDataEntry.TabStop = False
        GroupDataEntry.Text = "Data Entry"
        ' 
        ' GroupDataEntryFields
        ' 
        GroupDataEntryFields.Controls.Add(ComboDataEntryFieldBackColor)
        GroupDataEntryFields.Controls.Add(LabDataEntryFieldFont)
        GroupDataEntryFields.Controls.Add(ChkDataEntryFieldBorder)
        GroupDataEntryFields.Controls.Add(LabDataEntryFieldBackColor)
        GroupDataEntryFields.Controls.Add(CmdDataEntryFieldFont)
        GroupDataEntryFields.Location = New Point(333, 22)
        GroupDataEntryFields.Name = "GroupDataEntryFields"
        GroupDataEntryFields.Size = New Size(302, 87)
        GroupDataEntryFields.TabIndex = 1
        GroupDataEntryFields.TabStop = False
        GroupDataEntryFields.Text = "Fields"
        ' 
        ' ComboDataEntryFieldBackColor
        ' 
        ComboDataEntryFieldBackColor.Colors = ComboColorPicker.ColorList.None
        ComboDataEntryFieldBackColor.DrawMode = DrawMode.OwnerDrawFixed
        ComboDataEntryFieldBackColor.DropDownStyle = ComboBoxStyle.DropDownList
        ComboDataEntryFieldBackColor.FormattingEnabled = True
        ComboDataEntryFieldBackColor.Location = New Point(93, 48)
        ComboDataEntryFieldBackColor.Name = "ComboDataEntryFieldBackColor"
        ComboDataEntryFieldBackColor.Size = New Size(180, 24)
        ComboDataEntryFieldBackColor.TabIndex = 7
        ' 
        ' LabDataEntryFieldFont
        ' 
        LabDataEntryFieldFont.AutoSize = True
        LabDataEntryFieldFont.Location = New Point(16, 23)
        LabDataEntryFieldFont.Name = "LabDataEntryFieldFont"
        LabDataEntryFieldFont.Size = New Size(31, 15)
        LabDataEntryFieldFont.TabIndex = 6
        LabDataEntryFieldFont.Text = "Font"
        ' 
        ' ChkDataEntryFieldBorder
        ' 
        ChkDataEntryFieldBorder.AutoSize = True
        ChkDataEntryFieldBorder.Location = New Point(168, 21)
        ChkDataEntryFieldBorder.Name = "ChkDataEntryFieldBorder"
        ChkDataEntryFieldBorder.Size = New Size(61, 19)
        ChkDataEntryFieldBorder.TabIndex = 5
        ChkDataEntryFieldBorder.Text = "Border"
        ChkDataEntryFieldBorder.UseVisualStyleBackColor = True
        ' 
        ' LabDataEntryFieldBackColor
        ' 
        LabDataEntryFieldBackColor.AutoSize = True
        LabDataEntryFieldBackColor.Location = New Point(16, 54)
        LabDataEntryFieldBackColor.Name = "LabDataEntryFieldBackColor"
        LabDataEntryFieldBackColor.Size = New Size(61, 15)
        LabDataEntryFieldBackColor.TabIndex = 3
        LabDataEntryFieldBackColor.Text = "BackColor"
        ' 
        ' CmdDataEntryFieldFont
        ' 
        CmdDataEntryFieldFont.Location = New Point(93, 19)
        CmdDataEntryFieldFont.Name = "CmdDataEntryFieldFont"
        CmdDataEntryFieldFont.Size = New Size(46, 23)
        CmdDataEntryFieldFont.TabIndex = 1
        CmdDataEntryFieldFont.Text = "Select"
        CmdDataEntryFieldFont.UseVisualStyleBackColor = True
        ' 
        ' GroupDataEntryLabels
        ' 
        GroupDataEntryLabels.Controls.Add(ComboDataEntryLabelBackColor)
        GroupDataEntryLabels.Controls.Add(LabDataEntryLabelFont)
        GroupDataEntryLabels.Controls.Add(ChkDataEntryLabelBorder)
        GroupDataEntryLabels.Controls.Add(LabDataEntryLabelBackColor)
        GroupDataEntryLabels.Controls.Add(CmdDataEntryLabelFont)
        GroupDataEntryLabels.Location = New Point(6, 22)
        GroupDataEntryLabels.Name = "GroupDataEntryLabels"
        GroupDataEntryLabels.Size = New Size(309, 88)
        GroupDataEntryLabels.TabIndex = 0
        GroupDataEntryLabels.TabStop = False
        GroupDataEntryLabels.Text = "Labels"
        ' 
        ' ComboDataEntryLabelBackColor
        ' 
        ComboDataEntryLabelBackColor.Colors = ComboColorPicker.ColorList.None
        ComboDataEntryLabelBackColor.DrawMode = DrawMode.OwnerDrawFixed
        ComboDataEntryLabelBackColor.DropDownStyle = ComboBoxStyle.DropDownList
        ComboDataEntryLabelBackColor.FormattingEnabled = True
        ComboDataEntryLabelBackColor.Location = New Point(94, 50)
        ComboDataEntryLabelBackColor.Name = "ComboDataEntryLabelBackColor"
        ComboDataEntryLabelBackColor.Size = New Size(180, 24)
        ComboDataEntryLabelBackColor.TabIndex = 8
        ' 
        ' LabDataEntryLabelFont
        ' 
        LabDataEntryLabelFont.AutoSize = True
        LabDataEntryLabelFont.Location = New Point(12, 24)
        LabDataEntryLabelFont.Name = "LabDataEntryLabelFont"
        LabDataEntryLabelFont.Size = New Size(31, 15)
        LabDataEntryLabelFont.TabIndex = 7
        LabDataEntryLabelFont.Text = "Font"
        ' 
        ' ChkDataEntryLabelBorder
        ' 
        ChkDataEntryLabelBorder.AutoSize = True
        ChkDataEntryLabelBorder.Location = New Point(172, 23)
        ChkDataEntryLabelBorder.Name = "ChkDataEntryLabelBorder"
        ChkDataEntryLabelBorder.Size = New Size(61, 19)
        ChkDataEntryLabelBorder.TabIndex = 4
        ChkDataEntryLabelBorder.Text = "Border"
        ChkDataEntryLabelBorder.UseVisualStyleBackColor = True
        ' 
        ' LabDataEntryLabelBackColor
        ' 
        LabDataEntryLabelBackColor.AutoSize = True
        LabDataEntryLabelBackColor.Location = New Point(12, 56)
        LabDataEntryLabelBackColor.Name = "LabDataEntryLabelBackColor"
        LabDataEntryLabelBackColor.Size = New Size(61, 15)
        LabDataEntryLabelBackColor.TabIndex = 2
        LabDataEntryLabelBackColor.Text = "BackColor"
        ' 
        ' CmdDataEntryLabelFont
        ' 
        CmdDataEntryLabelFont.Location = New Point(95, 20)
        CmdDataEntryLabelFont.Name = "CmdDataEntryLabelFont"
        CmdDataEntryLabelFont.Size = New Size(46, 23)
        CmdDataEntryLabelFont.TabIndex = 0
        CmdDataEntryLabelFont.Text = "Font"
        CmdDataEntryLabelFont.UseVisualStyleBackColor = True
        ' 
        ' GroupForm
        ' 
        GroupForm.Controls.Add(GroupLayout)
        GroupForm.Controls.Add(LabFormFont)
        GroupForm.Controls.Add(LabFormBorderStyle)
        GroupForm.Controls.Add(ComboFormBorderStyle)
        GroupForm.Controls.Add(LabFormText)
        GroupForm.Controls.Add(TxtFormText)
        GroupForm.Controls.Add(LabFormForeColor)
        GroupForm.Controls.Add(ComboFormForeColor)
        GroupForm.Controls.Add(ComboFormBackColor)
        GroupForm.Controls.Add(CmdFormFont)
        GroupForm.Controls.Add(LabFormBackColor)
        GroupForm.Font = New Font("Segoe UI", 9F)
        GroupForm.Location = New Point(11, 11)
        GroupForm.Name = "GroupForm"
        GroupForm.Size = New Size(641, 179)
        GroupForm.TabIndex = 12
        GroupForm.TabStop = False
        GroupForm.Text = "Form"
        ' 
        ' GroupLayout
        ' 
        GroupLayout.Controls.Add(RadioLayoutVertical)
        GroupLayout.Controls.Add(RadioLayoutRectangle)
        GroupLayout.Location = New Point(426, 16)
        GroupLayout.Name = "GroupLayout"
        GroupLayout.Size = New Size(91, 85)
        GroupLayout.TabIndex = 14
        GroupLayout.TabStop = False
        GroupLayout.Text = "Layout"
        GroupLayout.Visible = False
        ' 
        ' RadioLayoutVertical
        ' 
        RadioLayoutVertical.AutoSize = True
        RadioLayoutVertical.Checked = True
        RadioLayoutVertical.Location = New Point(8, 48)
        RadioLayoutVertical.Name = "RadioLayoutVertical"
        RadioLayoutVertical.Size = New Size(63, 19)
        RadioLayoutVertical.TabIndex = 1
        RadioLayoutVertical.TabStop = True
        RadioLayoutVertical.Text = "Vertical"
        RadioLayoutVertical.UseVisualStyleBackColor = True
        ' 
        ' RadioLayoutRectangle
        ' 
        RadioLayoutRectangle.AutoSize = True
        RadioLayoutRectangle.Location = New Point(8, 23)
        RadioLayoutRectangle.Name = "RadioLayoutRectangle"
        RadioLayoutRectangle.Size = New Size(77, 19)
        RadioLayoutRectangle.TabIndex = 0
        RadioLayoutRectangle.Text = "Rectangle"
        RadioLayoutRectangle.UseVisualStyleBackColor = True
        ' 
        ' LabFormFont
        ' 
        LabFormFont.AutoSize = True
        LabFormFont.Location = New Point(14, 27)
        LabFormFont.Name = "LabFormFont"
        LabFormFont.Size = New Size(31, 15)
        LabFormFont.TabIndex = 13
        LabFormFont.Text = "Font"
        ' 
        ' LabFormBorderStyle
        ' 
        LabFormBorderStyle.AutoSize = True
        LabFormBorderStyle.Location = New Point(14, 86)
        LabFormBorderStyle.Name = "LabFormBorderStyle"
        LabFormBorderStyle.Size = New Size(67, 15)
        LabFormBorderStyle.TabIndex = 12
        LabFormBorderStyle.Text = "BorderStyle"
        ' 
        ' ComboFormBorderStyle
        ' 
        ComboFormBorderStyle.DrawMode = DrawMode.OwnerDrawFixed
        ComboFormBorderStyle.DropDownStyle = ComboBoxStyle.DropDownList
        ComboFormBorderStyle.FormattingEnabled = True
        ComboFormBorderStyle.ItemHeight = 18
        ComboFormBorderStyle.Location = New Point(101, 83)
        ComboFormBorderStyle.Name = "ComboFormBorderStyle"
        ComboFormBorderStyle.Size = New Size(180, 24)
        ComboFormBorderStyle.TabIndex = 11
        ' 
        ' LabFormText
        ' 
        LabFormText.AutoSize = True
        LabFormText.Location = New Point(14, 146)
        LabFormText.Name = "LabFormText"
        LabFormText.Size = New Size(28, 15)
        LabFormText.TabIndex = 10
        LabFormText.Text = "Text"
        ' 
        ' TxtFormText
        ' 
        TxtFormText.Location = New Point(101, 143)
        TxtFormText.Name = "TxtFormText"
        TxtFormText.Size = New Size(180, 23)
        TxtFormText.TabIndex = 9
        ' 
        ' LabFormForeColor
        ' 
        LabFormForeColor.AutoSize = True
        LabFormForeColor.Location = New Point(14, 116)
        LabFormForeColor.Name = "LabFormForeColor"
        LabFormForeColor.Size = New Size(59, 15)
        LabFormForeColor.TabIndex = 8
        LabFormForeColor.Text = "ForeColor"
        ' 
        ' ComboFormForeColor
        ' 
        ComboFormForeColor.Colors = ComboColorPicker.ColorList.None
        ComboFormForeColor.DrawMode = DrawMode.OwnerDrawFixed
        ComboFormForeColor.DropDownStyle = ComboBoxStyle.DropDownList
        ComboFormForeColor.FormattingEnabled = True
        ComboFormForeColor.Location = New Point(101, 113)
        ComboFormForeColor.Name = "ComboFormForeColor"
        ComboFormForeColor.Size = New Size(180, 24)
        ComboFormForeColor.TabIndex = 7
        ' 
        ' ComboFormBackColor
        ' 
        ComboFormBackColor.Colors = ComboColorPicker.ColorList.None
        ComboFormBackColor.DrawMode = DrawMode.OwnerDrawFixed
        ComboFormBackColor.DropDownStyle = ComboBoxStyle.DropDownList
        ComboFormBackColor.FormattingEnabled = True
        ComboFormBackColor.Location = New Point(101, 53)
        ComboFormBackColor.Name = "ComboFormBackColor"
        ComboFormBackColor.Size = New Size(180, 24)
        ComboFormBackColor.TabIndex = 6
        ' 
        ' CmdFormFont
        ' 
        CmdFormFont.Location = New Point(101, 22)
        CmdFormFont.Name = "CmdFormFont"
        CmdFormFont.Size = New Size(46, 25)
        CmdFormFont.TabIndex = 5
        CmdFormFont.Text = "Select"
        CmdFormFont.UseVisualStyleBackColor = True
        ' 
        ' LabFormBackColor
        ' 
        LabFormBackColor.AutoSize = True
        LabFormBackColor.Location = New Point(14, 56)
        LabFormBackColor.Name = "LabFormBackColor"
        LabFormBackColor.Size = New Size(61, 15)
        LabFormBackColor.TabIndex = 4
        LabFormBackColor.Text = "BackColor"
        ' 
        ' GroupGrouping
        ' 
        GroupGrouping.Controls.Add(LabGroupBordersDashPattern)
        GroupGrouping.Controls.Add(UCGroupBordersDashPatternPicker)
        GroupGrouping.Controls.Add(LabGroupBordersDashStyle)
        GroupGrouping.Controls.Add(ComboGroupBordersDashStylePicker)
        GroupGrouping.Controls.Add(LabGroupBordersWidth)
        GroupGrouping.Controls.Add(NumericGroupBordersWidth)
        GroupGrouping.Controls.Add(ComboGroupBordersColor)
        GroupGrouping.Controls.Add(LabGroupBordersColor)
        GroupGrouping.Location = New Point(344, 196)
        GroupGrouping.Name = "GroupGrouping"
        GroupGrouping.Size = New Size(308, 146)
        GroupGrouping.TabIndex = 11
        GroupGrouping.TabStop = False
        GroupGrouping.Text = "Group Borders"
        ' 
        ' LabGroupBordersDashPattern
        ' 
        LabGroupBordersDashPattern.AutoSize = True
        LabGroupBordersDashPattern.Location = New Point(16, 59)
        LabGroupBordersDashPattern.Name = "LabGroupBordersDashPattern"
        LabGroupBordersDashPattern.Size = New Size(71, 15)
        LabGroupBordersDashPattern.TabIndex = 13
        LabGroupBordersDashPattern.Text = "DashPattern"
        ' 
        ' UCGroupBordersDashPatternPicker
        ' 
        UCGroupBordersDashPatternPicker.BackColor = SystemColors.Window
        UCGroupBordersDashPatternPicker.DashPattern = New Single() {4F, 2F}
        UCGroupBordersDashPatternPicker.Location = New Point(93, 53)
        UCGroupBordersDashPatternPicker.Name = "UCGroupBordersDashPatternPicker"
        UCGroupBordersDashPatternPicker.Padding = New Padding(1)
        UCGroupBordersDashPatternPicker.Size = New Size(180, 22)
        UCGroupBordersDashPatternPicker.TabIndex = 12
        ' 
        ' LabGroupBordersDashStyle
        ' 
        LabGroupBordersDashStyle.AutoSize = True
        LabGroupBordersDashStyle.Location = New Point(15, 88)
        LabGroupBordersDashStyle.Name = "LabGroupBordersDashStyle"
        LabGroupBordersDashStyle.Size = New Size(58, 15)
        LabGroupBordersDashStyle.TabIndex = 11
        LabGroupBordersDashStyle.Text = "DashStyle"
        ' 
        ' ComboGroupBordersDashStylePicker
        ' 
        ComboGroupBordersDashStylePicker.DrawMode = DrawMode.OwnerDrawFixed
        ComboGroupBordersDashStylePicker.DropDownStyle = ComboBoxStyle.DropDownList
        ComboGroupBordersDashStylePicker.FormattingEnabled = True
        ComboGroupBordersDashStylePicker.ItemHeight = 22
        ComboGroupBordersDashStylePicker.Location = New Point(93, 81)
        ComboGroupBordersDashStylePicker.Name = "ComboGroupBordersDashStylePicker"
        ComboGroupBordersDashStylePicker.Size = New Size(180, 28)
        ComboGroupBordersDashStylePicker.TabIndex = 10
        ' 
        ' LabGroupBordersWidth
        ' 
        LabGroupBordersWidth.AutoSize = True
        LabGroupBordersWidth.Location = New Point(15, 115)
        LabGroupBordersWidth.Name = "LabGroupBordersWidth"
        LabGroupBordersWidth.Size = New Size(39, 15)
        LabGroupBordersWidth.TabIndex = 9
        LabGroupBordersWidth.Text = "Width"
        ' 
        ' NumericGroupBordersWidth
        ' 
        NumericGroupBordersWidth.Location = New Point(93, 112)
        NumericGroupBordersWidth.Maximum = New Decimal(New Integer() {5, 0, 0, 0})
        NumericGroupBordersWidth.Name = "NumericGroupBordersWidth"
        NumericGroupBordersWidth.Size = New Size(55, 23)
        NumericGroupBordersWidth.TabIndex = 8
        ' 
        ' ComboGroupBordersColor
        ' 
        ComboGroupBordersColor.Colors = ComboColorPicker.ColorList.None
        ComboGroupBordersColor.DrawMode = DrawMode.OwnerDrawFixed
        ComboGroupBordersColor.DropDownStyle = ComboBoxStyle.DropDownList
        ComboGroupBordersColor.FormattingEnabled = True
        ComboGroupBordersColor.Location = New Point(93, 22)
        ComboGroupBordersColor.Name = "ComboGroupBordersColor"
        ComboGroupBordersColor.Size = New Size(180, 24)
        ComboGroupBordersColor.TabIndex = 7
        ' 
        ' LabGroupBordersColor
        ' 
        LabGroupBordersColor.AutoSize = True
        LabGroupBordersColor.Location = New Point(16, 27)
        LabGroupBordersColor.Name = "LabGroupBordersColor"
        LabGroupBordersColor.Size = New Size(36, 15)
        LabGroupBordersColor.TabIndex = 6
        LabGroupBordersColor.Text = "Color"
        ' 
        ' GroupTheme
        ' 
        GroupTheme.Controls.Add(LabGroupHeadingsFont)
        GroupTheme.Controls.Add(ChkGroupHeadingsVisible)
        GroupTheme.Controls.Add(ComboGroupHeadingsInactiveColor)
        GroupTheme.Controls.Add(ComboGroupHeadingsActiveColor)
        GroupTheme.Controls.Add(LabGroupHeadingsInactiveColor)
        GroupTheme.Controls.Add(CmdGroupHeadingsFont)
        GroupTheme.Controls.Add(LabGroupHeadingsActiveColor)
        GroupTheme.Location = New Point(11, 196)
        GroupTheme.Name = "GroupTheme"
        GroupTheme.Size = New Size(315, 146)
        GroupTheme.TabIndex = 10
        GroupTheme.TabStop = False
        GroupTheme.Text = "Group Headings"
        ' 
        ' LabGroupHeadingsFont
        ' 
        LabGroupHeadingsFont.AutoSize = True
        LabGroupHeadingsFont.Location = New Point(14, 56)
        LabGroupHeadingsFont.Name = "LabGroupHeadingsFont"
        LabGroupHeadingsFont.Size = New Size(31, 15)
        LabGroupHeadingsFont.TabIndex = 13
        LabGroupHeadingsFont.Text = "Font"
        ' 
        ' ChkGroupHeadingsVisible
        ' 
        ChkGroupHeadingsVisible.AutoSize = True
        ChkGroupHeadingsVisible.CheckAlign = ContentAlignment.MiddleRight
        ChkGroupHeadingsVisible.Location = New Point(13, 27)
        ChkGroupHeadingsVisible.Name = "ChkGroupHeadingsVisible"
        ChkGroupHeadingsVisible.Size = New Size(102, 19)
        ChkGroupHeadingsVisible.TabIndex = 13
        ChkGroupHeadingsVisible.Text = "Visible              "
        ChkGroupHeadingsVisible.UseVisualStyleBackColor = True
        ' 
        ' ComboGroupHeadingsInactiveColor
        ' 
        ComboGroupHeadingsInactiveColor.Colors = ComboColorPicker.ColorList.None
        ComboGroupHeadingsInactiveColor.DrawMode = DrawMode.OwnerDrawFixed
        ComboGroupHeadingsInactiveColor.DropDownStyle = ComboBoxStyle.DropDownList
        ComboGroupHeadingsInactiveColor.Enabled = False
        ComboGroupHeadingsInactiveColor.FormattingEnabled = True
        ComboGroupHeadingsInactiveColor.Location = New Point(101, 112)
        ComboGroupHeadingsInactiveColor.Name = "ComboGroupHeadingsInactiveColor"
        ComboGroupHeadingsInactiveColor.Size = New Size(180, 24)
        ComboGroupHeadingsInactiveColor.TabIndex = 12
        ' 
        ' ComboGroupHeadingsActiveColor
        ' 
        ComboGroupHeadingsActiveColor.Colors = ComboColorPicker.ColorList.None
        ComboGroupHeadingsActiveColor.DrawMode = DrawMode.OwnerDrawFixed
        ComboGroupHeadingsActiveColor.DropDownStyle = ComboBoxStyle.DropDownList
        ComboGroupHeadingsActiveColor.Enabled = False
        ComboGroupHeadingsActiveColor.FormattingEnabled = True
        ComboGroupHeadingsActiveColor.Location = New Point(101, 82)
        ComboGroupHeadingsActiveColor.Name = "ComboGroupHeadingsActiveColor"
        ComboGroupHeadingsActiveColor.Size = New Size(180, 24)
        ComboGroupHeadingsActiveColor.TabIndex = 11
        ' 
        ' LabGroupHeadingsInactiveColor
        ' 
        LabGroupHeadingsInactiveColor.AutoSize = True
        LabGroupHeadingsInactiveColor.Location = New Point(14, 111)
        LabGroupHeadingsInactiveColor.Name = "LabGroupHeadingsInactiveColor"
        LabGroupHeadingsInactiveColor.Size = New Size(77, 15)
        LabGroupHeadingsInactiveColor.TabIndex = 10
        LabGroupHeadingsInactiveColor.Text = "InactiveColor"
        ' 
        ' CmdGroupHeadingsFont
        ' 
        CmdGroupHeadingsFont.Enabled = False
        CmdGroupHeadingsFont.Location = New Point(100, 51)
        CmdGroupHeadingsFont.Name = "CmdGroupHeadingsFont"
        CmdGroupHeadingsFont.Size = New Size(46, 25)
        CmdGroupHeadingsFont.TabIndex = 8
        CmdGroupHeadingsFont.Text = "Select"
        CmdGroupHeadingsFont.UseVisualStyleBackColor = True
        ' 
        ' LabGroupHeadingsActiveColor
        ' 
        LabGroupHeadingsActiveColor.AutoSize = True
        LabGroupHeadingsActiveColor.Location = New Point(14, 82)
        LabGroupHeadingsActiveColor.Name = "LabGroupHeadingsActiveColor"
        LabGroupHeadingsActiveColor.Size = New Size(69, 15)
        LabGroupHeadingsActiveColor.TabIndex = 7
        LabGroupHeadingsActiveColor.Text = "ActiveColor"
        ' 
        ' CmdOK
        ' 
        CmdOK.DialogResult = DialogResult.OK
        CmdOK.Image = My.Resources.Resources.StatusOK_18_18
        CmdOK.Location = New Point(11, 481)
        CmdOK.Name = "CmdOK"
        CmdOK.Size = New Size(36, 24)
        CmdOK.TabIndex = 14
        CmdOK.UseVisualStyleBackColor = True
        ' 
        ' CmdCancel
        ' 
        CmdCancel.DialogResult = DialogResult.Cancel
        CmdCancel.Image = My.Resources.Resources.Cancel
        CmdCancel.Location = New Point(51, 482)
        CmdCancel.Name = "CmdCancel"
        CmdCancel.Size = New Size(36, 24)
        CmdCancel.TabIndex = 15
        CmdCancel.UseVisualStyleBackColor = True
        ' 
        ' FrmFormDesigner
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(663, 512)
        Controls.Add(CmdCancel)
        Controls.Add(CmdOK)
        Controls.Add(GroupDataEntry)
        Controls.Add(GroupForm)
        Controls.Add(GroupGrouping)
        Controls.Add(GroupTheme)
        Name = "FrmFormDesigner"
        Text = "Form Designer"
        GroupDataEntry.ResumeLayout(False)
        GroupDataEntryFields.ResumeLayout(False)
        GroupDataEntryFields.PerformLayout()
        GroupDataEntryLabels.ResumeLayout(False)
        GroupDataEntryLabels.PerformLayout()
        GroupForm.ResumeLayout(False)
        GroupForm.PerformLayout()
        GroupLayout.ResumeLayout(False)
        GroupLayout.PerformLayout()
        GroupGrouping.ResumeLayout(False)
        GroupGrouping.PerformLayout()
        CType(NumericGroupBordersWidth, ComponentModel.ISupportInitialize).EndInit()
        GroupTheme.ResumeLayout(False)
        GroupTheme.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents GroupDataEntry As GroupBox
    Friend WithEvents GroupDataEntryFields As GroupBox
    Friend WithEvents ChkDataEntryFieldBorder As CheckBox
    Friend WithEvents LabDataEntryFieldBackColor As Label
    Friend WithEvents CmdDataEntryFieldFont As Button
    Friend WithEvents GroupDataEntryLabels As GroupBox
    Friend WithEvents ChkDataEntryLabelBorder As CheckBox
    Friend WithEvents LabDataEntryLabelBackColor As Label
    Friend WithEvents CmdDataEntryLabelFont As Button
    Friend WithEvents GroupForm As GroupBox
    Friend WithEvents CmdFormFont As Button
    Friend WithEvents LabFormBackColor As Label
    Friend WithEvents GroupGrouping As GroupBox
    Friend WithEvents ComboGroupBordersColor As ComboColorPicker
    Friend WithEvents LabGroupBordersColor As Label
    Friend WithEvents GroupTheme As GroupBox
    Friend WithEvents ChkGroupHeadingsVisible As CheckBox
    Friend WithEvents ComboGroupHeadingsInactiveColor As ComboColorPicker
    Friend WithEvents ComboGroupHeadingsActiveColor As ComboColorPicker
    Friend WithEvents LabGroupHeadingsInactiveColor As Label
    Friend WithEvents CmdGroupHeadingsFont As Button
    Friend WithEvents LabGroupHeadingsActiveColor As Label
    Friend WithEvents ComboFormBackColor As ComboColorPicker
    Friend WithEvents LabGroupBordersWidth As Label
    Friend WithEvents NumericGroupBordersWidth As NumericUpDown
    Friend WithEvents LabFormText As Label
    Friend WithEvents TxtFormText As TextBox
    Friend WithEvents LabFormForeColor As Label
    Friend WithEvents ComboFormForeColor As ComboColorPicker
    Friend WithEvents LabFormBorderStyle As Label
    Friend WithEvents ComboFormBorderStyle As ComboFormBorderStylePicker
    Friend WithEvents LabFormFont As Label
    Friend WithEvents LabGroupHeadingsFont As Label
    Friend WithEvents LabGroupBordersDashStyle As Label
    Friend WithEvents ComboGroupBordersDashStylePicker As ComboDashStylePicker
    Friend WithEvents LabGroupBordersDashPattern As Label
    Friend WithEvents UCGroupBordersDashPatternPicker As DashPatternPicker
    Friend WithEvents LabDataEntryFieldFont As Label
    Friend WithEvents ComboDataEntryFieldBackColor As ComboColorPicker
    Friend WithEvents ComboDataEntryLabelBackColor As ComboColorPicker
    Friend WithEvents LabDataEntryLabelFont As Label
    Friend WithEvents GroupLayout As GroupBox
    Friend WithEvents RadioLayoutVertical As RadioButton
    Friend WithEvents RadioLayoutRectangle As RadioButton
    Friend WithEvents CmdOK As Button
    Friend WithEvents CmdCancel As Button
End Class
