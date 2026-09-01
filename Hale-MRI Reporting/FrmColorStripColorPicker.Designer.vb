<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmColorStripColorPicker
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
        TableLayoutColorPickers = New TableLayoutPanel()
        ComboColor = New ComboColorPicker()
        CmdColorTrash = New Button()
        CmdCancel = New Button()
        CmdOK = New Button()
        ColorPanel1 = New ColorPanel()
        ColorPanel2 = New ColorPanel()
        ColorPanel3 = New ColorPanel()
        ColorPanel4 = New ColorPanel()
        ColorPanel5 = New ColorPanel()
        ColorPanel6 = New ColorPanel()
        ColorPanel7 = New ColorPanel()
        ColorPanel8 = New ColorPanel()
        ColorPanel9 = New ColorPanel()
        ColorPanel10 = New ColorPanel()
        TableLayoutColorPickers.SuspendLayout()
        SuspendLayout()
        ' 
        ' TableLayoutColorPickers
        ' 
        TableLayoutColorPickers.AutoSize = True
        TableLayoutColorPickers.ColumnCount = 6
        TableLayoutColorPickers.ColumnStyles.Add(New ColumnStyle())
        TableLayoutColorPickers.ColumnStyles.Add(New ColumnStyle())
        TableLayoutColorPickers.ColumnStyles.Add(New ColumnStyle())
        TableLayoutColorPickers.ColumnStyles.Add(New ColumnStyle())
        TableLayoutColorPickers.ColumnStyles.Add(New ColumnStyle())
        TableLayoutColorPickers.ColumnStyles.Add(New ColumnStyle())
        TableLayoutColorPickers.Controls.Add(ComboColor, 1, 2)
        TableLayoutColorPickers.Controls.Add(CmdColorTrash, 5, 2)
        TableLayoutColorPickers.Controls.Add(CmdCancel, 1, 3)
        TableLayoutColorPickers.Controls.Add(CmdOK, 0, 3)
        TableLayoutColorPickers.Controls.Add(ColorPanel1, 0, 0)
        TableLayoutColorPickers.Controls.Add(ColorPanel2, 1, 0)
        TableLayoutColorPickers.Controls.Add(ColorPanel3, 2, 0)
        TableLayoutColorPickers.Controls.Add(ColorPanel4, 3, 0)
        TableLayoutColorPickers.Controls.Add(ColorPanel5, 4, 0)
        TableLayoutColorPickers.Controls.Add(ColorPanel6, 0, 1)
        TableLayoutColorPickers.Controls.Add(ColorPanel7, 1, 1)
        TableLayoutColorPickers.Controls.Add(ColorPanel8, 2, 1)
        TableLayoutColorPickers.Controls.Add(ColorPanel9, 3, 1)
        TableLayoutColorPickers.Controls.Add(ColorPanel10, 4, 1)
        TableLayoutColorPickers.Location = New Point(12, 12)
        TableLayoutColorPickers.Name = "TableLayoutColorPickers"
        TableLayoutColorPickers.RowCount = 4
        TableLayoutColorPickers.RowStyles.Add(New RowStyle())
        TableLayoutColorPickers.RowStyles.Add(New RowStyle())
        TableLayoutColorPickers.RowStyles.Add(New RowStyle())
        TableLayoutColorPickers.RowStyles.Add(New RowStyle())
        TableLayoutColorPickers.Size = New Size(274, 157)
        TableLayoutColorPickers.TabIndex = 0
        ' 
        ' ComboColor
        ' 
        ComboColor.Colors = ComboColorPicker.ColorList.None
        TableLayoutColorPickers.SetColumnSpan(ComboColor, 4)
        ComboColor.DisplayMember = "DisplayName"
        ComboColor.DrawMode = DrawMode.OwnerDrawFixed
        ComboColor.DropDownStyle = ComboBoxStyle.DropDownList
        ComboColor.FormattingEnabled = True
        ComboColor.Location = New Point(52, 97)
        ComboColor.Name = "ComboColor"
        ComboColor.Size = New Size(190, 24)
        ComboColor.TabIndex = 11
        ComboColor.ValueMember = "ColorValue"
        ' 
        ' CmdColorTrash
        ' 
        CmdColorTrash.Enabled = False
        CmdColorTrash.Image = My.Resources.Resources.Trash
        CmdColorTrash.Location = New Point(248, 97)
        CmdColorTrash.Name = "CmdColorTrash"
        CmdColorTrash.Size = New Size(23, 23)
        CmdColorTrash.TabIndex = 12
        CmdColorTrash.UseVisualStyleBackColor = True
        ' 
        ' CmdCancel
        ' 
        CmdCancel.DialogResult = DialogResult.Cancel
        CmdCancel.Image = My.Resources.Resources.Cancel
        CmdCancel.Location = New Point(52, 127)
        CmdCancel.Name = "CmdCancel"
        CmdCancel.Size = New Size(42, 27)
        CmdCancel.TabIndex = 14
        CmdCancel.UseVisualStyleBackColor = True
        ' 
        ' CmdOK
        ' 
        CmdOK.DialogResult = DialogResult.OK
        CmdOK.Enabled = False
        CmdOK.Image = My.Resources.Resources.StatusOK_18_18
        CmdOK.Location = New Point(3, 127)
        CmdOK.Name = "CmdOK"
        CmdOK.Size = New Size(40, 27)
        CmdOK.TabIndex = 13
        CmdOK.UseVisualStyleBackColor = True
        ' 
        ' ColorPanel1
        ' 
        ColorPanel1.BackColor = Color.Transparent
        ColorPanel1.BorderColor = Color.Gray
        ColorPanel1.BorderWidth = 1
        ColorPanel1.Color = Color.Empty
        ColorPanel1.DashPattern = New Single() {0F, 10F}
        ColorPanel1.DashStyle = Drawing2D.DashStyle.Dash
        ColorPanel1.Location = New Point(4, 4)
        ColorPanel1.Margin = New Padding(4, 4, 3, 3)
        ColorPanel1.Name = "ColorPanel1"
        ColorPanel1.Selected = False
        ColorPanel1.Size = New Size(42, 40)
        ColorPanel1.TabIndex = 1
        ' 
        ' ColorPanel2
        ' 
        ColorPanel2.BackColor = Color.Transparent
        ColorPanel2.BorderColor = Color.Gray
        ColorPanel2.BorderWidth = 1
        ColorPanel2.Color = Color.Empty
        ColorPanel2.DashPattern = New Single() {0F, 10F}
        ColorPanel2.DashStyle = Drawing2D.DashStyle.Dash
        ColorPanel2.Location = New Point(53, 4)
        ColorPanel2.Margin = New Padding(4, 4, 3, 3)
        ColorPanel2.Name = "ColorPanel2"
        ColorPanel2.Selected = False
        ColorPanel2.Size = New Size(42, 40)
        ColorPanel2.TabIndex = 2
        ' 
        ' ColorPanel3
        ' 
        ColorPanel3.BackColor = Color.Transparent
        ColorPanel3.BorderColor = Color.Gray
        ColorPanel3.BorderWidth = 1
        ColorPanel3.Color = Color.Empty
        ColorPanel3.DashPattern = New Single() {0F, 10F}
        ColorPanel3.DashStyle = Drawing2D.DashStyle.Dash
        ColorPanel3.Location = New Point(102, 4)
        ColorPanel3.Margin = New Padding(4, 4, 3, 3)
        ColorPanel3.Name = "ColorPanel3"
        ColorPanel3.Selected = False
        ColorPanel3.Size = New Size(42, 40)
        ColorPanel3.TabIndex = 3
        ' 
        ' ColorPanel4
        ' 
        ColorPanel4.BackColor = Color.Transparent
        ColorPanel4.BorderColor = Color.Gray
        ColorPanel4.BorderWidth = 1
        ColorPanel4.Color = Color.Empty
        ColorPanel4.DashPattern = New Single() {0F, 10F}
        ColorPanel4.DashStyle = Drawing2D.DashStyle.Dash
        ColorPanel4.Location = New Point(151, 4)
        ColorPanel4.Margin = New Padding(4, 4, 3, 3)
        ColorPanel4.Name = "ColorPanel4"
        ColorPanel4.Selected = False
        ColorPanel4.Size = New Size(42, 40)
        ColorPanel4.TabIndex = 4
        ' 
        ' ColorPanel5
        ' 
        ColorPanel5.BackColor = Color.Transparent
        ColorPanel5.BorderColor = Color.Gray
        ColorPanel5.BorderWidth = 1
        ColorPanel5.Color = Color.Empty
        ColorPanel5.DashPattern = New Single() {0F, 10F}
        ColorPanel5.DashStyle = Drawing2D.DashStyle.Dash
        ColorPanel5.Location = New Point(200, 4)
        ColorPanel5.Margin = New Padding(4, 4, 3, 3)
        ColorPanel5.Name = "ColorPanel5"
        ColorPanel5.Selected = False
        ColorPanel5.Size = New Size(42, 40)
        ColorPanel5.TabIndex = 5
        ' 
        ' ColorPanel6
        ' 
        ColorPanel6.BackColor = Color.Transparent
        ColorPanel6.BorderColor = Color.Gray
        ColorPanel6.BorderWidth = 1
        ColorPanel6.Color = Color.Empty
        ColorPanel6.DashPattern = New Single() {0F, 10F}
        ColorPanel6.DashStyle = Drawing2D.DashStyle.Dash
        ColorPanel6.Location = New Point(4, 51)
        ColorPanel6.Margin = New Padding(4, 4, 3, 3)
        ColorPanel6.Name = "ColorPanel6"
        ColorPanel6.Selected = False
        ColorPanel6.Size = New Size(42, 40)
        ColorPanel6.TabIndex = 6
        ' 
        ' ColorPanel7
        ' 
        ColorPanel7.BackColor = Color.Transparent
        ColorPanel7.BorderColor = Color.Gray
        ColorPanel7.BorderWidth = 1
        ColorPanel7.Color = Color.Empty
        ColorPanel7.DashPattern = New Single() {0F, 10F}
        ColorPanel7.DashStyle = Drawing2D.DashStyle.Dash
        ColorPanel7.Location = New Point(53, 51)
        ColorPanel7.Margin = New Padding(4, 4, 3, 3)
        ColorPanel7.Name = "ColorPanel7"
        ColorPanel7.Selected = False
        ColorPanel7.Size = New Size(42, 40)
        ColorPanel7.TabIndex = 7
        ' 
        ' ColorPanel8
        ' 
        ColorPanel8.BackColor = Color.Transparent
        ColorPanel8.BorderColor = Color.Gray
        ColorPanel8.BorderWidth = 1
        ColorPanel8.Color = Color.Empty
        ColorPanel8.DashPattern = New Single() {0F, 10F}
        ColorPanel8.DashStyle = Drawing2D.DashStyle.Dash
        ColorPanel8.Location = New Point(102, 51)
        ColorPanel8.Margin = New Padding(4, 4, 3, 3)
        ColorPanel8.Name = "ColorPanel8"
        ColorPanel8.Selected = False
        ColorPanel8.Size = New Size(42, 40)
        ColorPanel8.TabIndex = 8
        ' 
        ' ColorPanel9
        ' 
        ColorPanel9.BackColor = Color.Transparent
        ColorPanel9.BorderColor = Color.Gray
        ColorPanel9.BorderWidth = 1
        ColorPanel9.Color = Color.Empty
        ColorPanel9.DashPattern = New Single() {0F, 10F}
        ColorPanel9.DashStyle = Drawing2D.DashStyle.Dash
        ColorPanel9.Location = New Point(151, 51)
        ColorPanel9.Margin = New Padding(4, 4, 3, 3)
        ColorPanel9.Name = "ColorPanel9"
        ColorPanel9.Selected = False
        ColorPanel9.Size = New Size(42, 40)
        ColorPanel9.TabIndex = 9
        ' 
        ' ColorPanel10
        ' 
        ColorPanel10.BackColor = Color.Transparent
        ColorPanel10.BorderColor = Color.Gray
        ColorPanel10.BorderWidth = 1
        ColorPanel10.Color = Color.Empty
        ColorPanel10.DashPattern = New Single() {0F, 10F}
        ColorPanel10.DashStyle = Drawing2D.DashStyle.Dash
        ColorPanel10.Location = New Point(200, 51)
        ColorPanel10.Margin = New Padding(4, 4, 3, 3)
        ColorPanel10.Name = "ColorPanel10"
        ColorPanel10.Selected = False
        ColorPanel10.Size = New Size(42, 40)
        ColorPanel10.TabIndex = 10
        ' 
        ' FrmColorStripColorPicker
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(298, 181)
        Controls.Add(TableLayoutColorPickers)
        Name = "FrmColorStripColorPicker"
        Text = "ColorStrip Color Picker"
        TableLayoutColorPickers.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents TableLayoutColorPickers As TableLayoutPanel
    Friend WithEvents ComboColor As ComboColorPicker
    Friend WithEvents CmdColorTrash As Button
    Friend WithEvents CmdCancel As Button
    Friend WithEvents CmdOK As Button
    Friend WithEvents ColorPanel1 As ColorPanel
    Friend WithEvents ColorPanel2 As ColorPanel
    Friend WithEvents ColorPanel3 As ColorPanel
    Friend WithEvents ColorPanel4 As ColorPanel
    Friend WithEvents ColorPanel5 As ColorPanel
    Friend WithEvents ColorPanel6 As ColorPanel
    Friend WithEvents ColorPanel7 As ColorPanel
    Friend WithEvents ColorPanel8 As ColorPanel
    Friend WithEvents ColorPanel9 As ColorPanel
    Friend WithEvents ColorPanel10 As ColorPanel
End Class
