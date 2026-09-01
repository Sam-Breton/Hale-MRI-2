<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RecordNavigationBar
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RecordNavigationBar))
        CmdGotoFirst = New Button()
        CmdGotoPrevious = New Button()
        TxtCurrentPosition = New TextBox()
        CmdGotoNext = New Button()
        CmdGotoLast = New Button()
        CmdAddNew = New Button()
        CmdDelete = New Button()
        TxtFind = New TextBox()
        CmdSave = New Button()
        CmdUndo = New Button()
        TableLayoutPanel1 = New TableLayoutPanel()
        CmdRefresh = New Button()
        ChkToggleFilter = New CheckBox()
        TableLayoutPanel1.SuspendLayout()
        SuspendLayout()
        ' 
        ' CmdGotoFirst
        ' 
        CmdGotoFirst.Dock = DockStyle.Fill
        CmdGotoFirst.Enabled = False
        CmdGotoFirst.Image = CType(resources.GetObject("CmdGotoFirst.Image"), Image)
        CmdGotoFirst.Location = New Point(0, 0)
        CmdGotoFirst.Margin = New Padding(0)
        CmdGotoFirst.Name = "CmdGotoFirst"
        CmdGotoFirst.Size = New Size(38, 26)
        CmdGotoFirst.TabIndex = 0
        CmdGotoFirst.TextImageRelation = TextImageRelation.ImageBeforeText
        CmdGotoFirst.UseVisualStyleBackColor = True
        ' 
        ' CmdGotoPrevious
        ' 
        CmdGotoPrevious.Dock = DockStyle.Fill
        CmdGotoPrevious.Enabled = False
        CmdGotoPrevious.Image = CType(resources.GetObject("CmdGotoPrevious.Image"), Image)
        CmdGotoPrevious.Location = New Point(38, 0)
        CmdGotoPrevious.Margin = New Padding(0)
        CmdGotoPrevious.Name = "CmdGotoPrevious"
        CmdGotoPrevious.Size = New Size(38, 26)
        CmdGotoPrevious.TabIndex = 1
        CmdGotoPrevious.UseVisualStyleBackColor = True
        ' 
        ' TxtCurrentPosition
        ' 
        TxtCurrentPosition.Dock = DockStyle.Top
        TxtCurrentPosition.Enabled = False
        TxtCurrentPosition.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        TxtCurrentPosition.Location = New Point(76, 0)
        TxtCurrentPosition.Margin = New Padding(0)
        TxtCurrentPosition.Name = "TxtCurrentPosition"
        TxtCurrentPosition.Size = New Size(99, 25)
        TxtCurrentPosition.TabIndex = 2
        TxtCurrentPosition.TextAlign = HorizontalAlignment.Center
        ' 
        ' CmdGotoNext
        ' 
        CmdGotoNext.Dock = DockStyle.Fill
        CmdGotoNext.Enabled = False
        CmdGotoNext.Image = CType(resources.GetObject("CmdGotoNext.Image"), Image)
        CmdGotoNext.Location = New Point(175, 0)
        CmdGotoNext.Margin = New Padding(0)
        CmdGotoNext.Name = "CmdGotoNext"
        CmdGotoNext.Size = New Size(38, 26)
        CmdGotoNext.TabIndex = 3
        CmdGotoNext.UseVisualStyleBackColor = True
        ' 
        ' CmdGotoLast
        ' 
        CmdGotoLast.Dock = DockStyle.Fill
        CmdGotoLast.Enabled = False
        CmdGotoLast.Image = CType(resources.GetObject("CmdGotoLast.Image"), Image)
        CmdGotoLast.Location = New Point(213, 0)
        CmdGotoLast.Margin = New Padding(0, 0, 3, 0)
        CmdGotoLast.Name = "CmdGotoLast"
        CmdGotoLast.Size = New Size(38, 26)
        CmdGotoLast.TabIndex = 4
        CmdGotoLast.UseVisualStyleBackColor = True
        ' 
        ' CmdAddNew
        ' 
        CmdAddNew.Dock = DockStyle.Fill
        CmdAddNew.Enabled = False
        CmdAddNew.Image = CType(resources.GetObject("CmdAddNew.Image"), Image)
        CmdAddNew.Location = New Point(256, 0)
        CmdAddNew.Margin = New Padding(2, 0, 0, 0)
        CmdAddNew.Name = "CmdAddNew"
        CmdAddNew.Size = New Size(38, 26)
        CmdAddNew.TabIndex = 5
        CmdAddNew.UseVisualStyleBackColor = True
        ' 
        ' CmdDelete
        ' 
        CmdDelete.Dock = DockStyle.Fill
        CmdDelete.Enabled = False
        CmdDelete.Image = CType(resources.GetObject("CmdDelete.Image"), Image)
        CmdDelete.Location = New Point(294, 0)
        CmdDelete.Margin = New Padding(0, 0, 3, 0)
        CmdDelete.Name = "CmdDelete"
        CmdDelete.Size = New Size(38, 26)
        CmdDelete.TabIndex = 6
        CmdDelete.UseVisualStyleBackColor = True
        ' 
        ' TxtFind
        ' 
        TxtFind.Dock = DockStyle.Top
        TxtFind.Enabled = False
        TxtFind.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        TxtFind.Location = New Point(418, 0)
        TxtFind.Margin = New Padding(2, 0, 2, 0)
        TxtFind.Name = "TxtFind"
        TxtFind.Size = New Size(137, 25)
        TxtFind.TabIndex = 9
        ' 
        ' CmdSave
        ' 
        CmdSave.Dock = DockStyle.Fill
        CmdSave.Enabled = False
        CmdSave.Image = CType(resources.GetObject("CmdSave.Image"), Image)
        CmdSave.Location = New Point(559, 0)
        CmdSave.Margin = New Padding(2, 0, 0, 0)
        CmdSave.Name = "CmdSave"
        CmdSave.Size = New Size(38, 26)
        CmdSave.TabIndex = 10
        CmdSave.UseVisualStyleBackColor = True
        ' 
        ' CmdUndo
        ' 
        CmdUndo.Dock = DockStyle.Fill
        CmdUndo.Enabled = False
        CmdUndo.Image = CType(resources.GetObject("CmdUndo.Image"), Image)
        CmdUndo.Location = New Point(597, 0)
        CmdUndo.Margin = New Padding(0)
        CmdUndo.Name = "CmdUndo"
        CmdUndo.Size = New Size(38, 26)
        CmdUndo.TabIndex = 11
        CmdUndo.UseVisualStyleBackColor = True
        ' 
        ' TableLayoutPanel1
        ' 
        TableLayoutPanel1.AutoSize = True
        TableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink
        TableLayoutPanel1.ColumnCount = 14
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle())
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle())
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle())
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle())
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle())
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle())
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle())
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle())
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle())
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle())
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle())
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle())
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle())
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle())
        TableLayoutPanel1.Controls.Add(CmdGotoFirst, 1, 0)
        TableLayoutPanel1.Controls.Add(CmdUndo, 12, 0)
        TableLayoutPanel1.Controls.Add(CmdGotoPrevious, 2, 0)
        TableLayoutPanel1.Controls.Add(CmdSave, 11, 0)
        TableLayoutPanel1.Controls.Add(TxtCurrentPosition, 3, 0)
        TableLayoutPanel1.Controls.Add(TxtFind, 10, 0)
        TableLayoutPanel1.Controls.Add(CmdGotoNext, 4, 0)
        TableLayoutPanel1.Controls.Add(CmdRefresh, 9, 0)
        TableLayoutPanel1.Controls.Add(CmdGotoLast, 5, 0)
        TableLayoutPanel1.Controls.Add(CmdAddNew, 6, 0)
        TableLayoutPanel1.Controls.Add(CmdDelete, 7, 0)
        TableLayoutPanel1.Controls.Add(ChkToggleFilter, 8, 0)
        TableLayoutPanel1.Dock = DockStyle.Fill
        TableLayoutPanel1.Location = New Point(0, 0)
        TableLayoutPanel1.Margin = New Padding(0)
        TableLayoutPanel1.Name = "TableLayoutPanel1"
        TableLayoutPanel1.RowCount = 1
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.Size = New Size(635, 26)
        TableLayoutPanel1.TabIndex = 12
        ' 
        ' CmdRefresh
        ' 
        CmdRefresh.Dock = DockStyle.Fill
        CmdRefresh.Enabled = False
        CmdRefresh.Image = CType(resources.GetObject("CmdRefresh.Image"), Image)
        CmdRefresh.Location = New Point(376, 0)
        CmdRefresh.Margin = New Padding(0, 0, 2, 0)
        CmdRefresh.Name = "CmdRefresh"
        CmdRefresh.Size = New Size(38, 26)
        CmdRefresh.TabIndex = 8
        CmdRefresh.UseVisualStyleBackColor = True
        ' 
        ' ChkToggleFilter
        ' 
        ChkToggleFilter.Appearance = Appearance.Button
        ChkToggleFilter.Dock = DockStyle.Fill
        ChkToggleFilter.Enabled = False
        ChkToggleFilter.Image = CType(resources.GetObject("ChkToggleFilter.Image"), Image)
        ChkToggleFilter.ImageAlign = ContentAlignment.TopLeft
        ChkToggleFilter.Location = New Point(338, 0)
        ChkToggleFilter.Margin = New Padding(3, 0, 0, 0)
        ChkToggleFilter.Name = "ChkToggleFilter"
        ChkToggleFilter.RightToLeft = RightToLeft.Yes
        ChkToggleFilter.Size = New Size(38, 26)
        ChkToggleFilter.TabIndex = 13
        ChkToggleFilter.Text = " XXX"
        ChkToggleFilter.TextAlign = ContentAlignment.TopLeft
        ChkToggleFilter.TextImageRelation = TextImageRelation.TextBeforeImage
        ChkToggleFilter.UseVisualStyleBackColor = True
        ' 
        ' RecordNavigationBar
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        AutoSize = True
        AutoSizeMode = AutoSizeMode.GrowAndShrink
        Controls.Add(TableLayoutPanel1)
        Margin = New Padding(0)
        Name = "RecordNavigationBar"
        Size = New Size(635, 26)
        TableLayoutPanel1.ResumeLayout(False)
        TableLayoutPanel1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents CmdGotoFirst As Button
    Friend WithEvents CmdGotoPrevious As Button
    Friend WithEvents TxtCurrentPosition As TextBox
    Friend WithEvents CmdGotoNext As Button
    Friend WithEvents CmdGotoLast As Button
    Friend WithEvents CmdAddNew As Button
    Friend WithEvents CmdDelete As Button
    Friend WithEvents TxtFind As TextBox
    Friend WithEvents CmdSave As Button
    Friend WithEvents CmdUndo As Button
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents ChkToggleFilter As CheckBox
    Friend WithEvents CmdRefresh As Button

End Class
