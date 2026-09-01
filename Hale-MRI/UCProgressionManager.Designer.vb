<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCProgressionManager
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
        TLayoutProgManager = New TableLayoutPanel()
        CmdFillManual = New Button()
        CmdLoadCurrent = New Button()
        CmdLoadFile = New Button()
        CmdSavetoFile = New Button()
        CmdClearDesc = New Button()
        LabPitchProg = New Label()
        TxtDesc = New RichTextBox()
        LabPropDesc = New Label()
        LabProgTable = New Label()
        CmdClearTable = New Button()
        CmdScalePitch = New Button()
        CmdCalcAvg = New Button()
        DGridProgTable = New DataGridView()
        TLayoutProgManager.SuspendLayout()
        CType(DGridProgTable, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' TLayoutProgManager
        ' 
        TLayoutProgManager.ColumnCount = 2
        TLayoutProgManager.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 27.77778F))
        TLayoutProgManager.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 72.22222F))
        TLayoutProgManager.Controls.Add(CmdFillManual, 0, 1)
        TLayoutProgManager.Controls.Add(CmdLoadCurrent, 0, 2)
        TLayoutProgManager.Controls.Add(CmdLoadFile, 0, 3)
        TLayoutProgManager.Controls.Add(CmdSavetoFile, 0, 4)
        TLayoutProgManager.Controls.Add(CmdClearDesc, 0, 6)
        TLayoutProgManager.Controls.Add(LabPitchProg, 0, 0)
        TLayoutProgManager.Controls.Add(TxtDesc, 1, 1)
        TLayoutProgManager.Controls.Add(LabPropDesc, 1, 0)
        TLayoutProgManager.Controls.Add(LabProgTable, 0, 8)
        TLayoutProgManager.Controls.Add(CmdClearTable, 0, 7)
        TLayoutProgManager.Controls.Add(CmdScalePitch, 1, 6)
        TLayoutProgManager.Controls.Add(CmdCalcAvg, 1, 7)
        TLayoutProgManager.Controls.Add(DGridProgTable, 0, 9)
        TLayoutProgManager.Dock = DockStyle.Fill
        TLayoutProgManager.Location = New Point(0, 0)
        TLayoutProgManager.Margin = New Padding(4, 5, 4, 5)
        TLayoutProgManager.Name = "TLayoutProgManager"
        TLayoutProgManager.RowCount = 10
        TLayoutProgManager.RowStyles.Add(New RowStyle(SizeType.Absolute, 30F))
        TLayoutProgManager.RowStyles.Add(New RowStyle(SizeType.Absolute, 40F))
        TLayoutProgManager.RowStyles.Add(New RowStyle(SizeType.Absolute, 40F))
        TLayoutProgManager.RowStyles.Add(New RowStyle(SizeType.Absolute, 40F))
        TLayoutProgManager.RowStyles.Add(New RowStyle(SizeType.Absolute, 40F))
        TLayoutProgManager.RowStyles.Add(New RowStyle(SizeType.Absolute, 20F))
        TLayoutProgManager.RowStyles.Add(New RowStyle(SizeType.Absolute, 40F))
        TLayoutProgManager.RowStyles.Add(New RowStyle(SizeType.Absolute, 40F))
        TLayoutProgManager.RowStyles.Add(New RowStyle(SizeType.Absolute, 30F))
        TLayoutProgManager.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        TLayoutProgManager.Size = New Size(1155, 661)
        TLayoutProgManager.TabIndex = 0
        ' 
        ' CmdFillManual
        ' 
        CmdFillManual.AutoSize = True
        CmdFillManual.Dock = DockStyle.Fill
        CmdFillManual.Location = New Point(3, 33)
        CmdFillManual.Name = "CmdFillManual"
        CmdFillManual.Size = New Size(314, 34)
        CmdFillManual.TabIndex = 0
        CmdFillManual.Text = "Fill Table Manually"
        CmdFillManual.UseVisualStyleBackColor = True
        ' 
        ' CmdLoadCurrent
        ' 
        CmdLoadCurrent.Dock = DockStyle.Fill
        CmdLoadCurrent.Location = New Point(3, 73)
        CmdLoadCurrent.Name = "CmdLoadCurrent"
        CmdLoadCurrent.Size = New Size(314, 34)
        CmdLoadCurrent.TabIndex = 1
        CmdLoadCurrent.Text = "Load Table from Scan"
        CmdLoadCurrent.UseVisualStyleBackColor = True
        ' 
        ' CmdLoadFile
        ' 
        CmdLoadFile.Dock = DockStyle.Fill
        CmdLoadFile.Location = New Point(3, 113)
        CmdLoadFile.Name = "CmdLoadFile"
        CmdLoadFile.Size = New Size(314, 34)
        CmdLoadFile.TabIndex = 2
        CmdLoadFile.Text = "Load Table from File"
        CmdLoadFile.UseVisualStyleBackColor = True
        ' 
        ' CmdSavetoFile
        ' 
        CmdSavetoFile.Dock = DockStyle.Fill
        CmdSavetoFile.Location = New Point(3, 153)
        CmdSavetoFile.Name = "CmdSavetoFile"
        CmdSavetoFile.Size = New Size(314, 34)
        CmdSavetoFile.TabIndex = 3
        CmdSavetoFile.Text = "Save Table to File"
        CmdSavetoFile.UseVisualStyleBackColor = True
        ' 
        ' CmdClearDesc
        ' 
        CmdClearDesc.Dock = DockStyle.Fill
        CmdClearDesc.Location = New Point(3, 213)
        CmdClearDesc.Name = "CmdClearDesc"
        CmdClearDesc.Size = New Size(314, 34)
        CmdClearDesc.TabIndex = 4
        CmdClearDesc.Text = "Clear Description"
        CmdClearDesc.UseVisualStyleBackColor = True
        ' 
        ' LabPitchProg
        ' 
        LabPitchProg.AutoSize = True
        LabPitchProg.Dock = DockStyle.Left
        LabPitchProg.Location = New Point(4, 0)
        LabPitchProg.Margin = New Padding(4, 0, 4, 0)
        LabPitchProg.Name = "LabPitchProg"
        LabPitchProg.Size = New Size(149, 30)
        LabPitchProg.TabIndex = 9
        LabPitchProg.Text = "Pitch Progression"
        ' 
        ' TxtDesc
        ' 
        TxtDesc.Dock = DockStyle.Fill
        TxtDesc.Location = New Point(327, 35)
        TxtDesc.Margin = New Padding(7, 5, 7, 5)
        TxtDesc.Name = "TxtDesc"
        TLayoutProgManager.SetRowSpan(TxtDesc, 4)
        TxtDesc.Size = New Size(821, 150)
        TxtDesc.TabIndex = 10
        TxtDesc.Text = ""
        ' 
        ' LabPropDesc
        ' 
        LabPropDesc.AutoSize = True
        LabPropDesc.Dock = DockStyle.Bottom
        LabPropDesc.Location = New Point(323, 5)
        LabPropDesc.Name = "LabPropDesc"
        LabPropDesc.Size = New Size(829, 25)
        LabPropDesc.TabIndex = 11
        LabPropDesc.Text = "Description"
        ' 
        ' LabProgTable
        ' 
        LabProgTable.AutoSize = True
        LabProgTable.Dock = DockStyle.Bottom
        LabProgTable.Location = New Point(4, 295)
        LabProgTable.Margin = New Padding(4, 0, 4, 0)
        LabProgTable.Name = "LabProgTable"
        LabProgTable.Size = New Size(312, 25)
        LabProgTable.TabIndex = 6
        LabProgTable.Text = "Progression Table"
        ' 
        ' CmdClearTable
        ' 
        CmdClearTable.Dock = DockStyle.Fill
        CmdClearTable.Location = New Point(3, 253)
        CmdClearTable.Name = "CmdClearTable"
        CmdClearTable.Size = New Size(314, 34)
        CmdClearTable.TabIndex = 5
        CmdClearTable.Text = "Clear Table"
        CmdClearTable.UseVisualStyleBackColor = True
        ' 
        ' CmdScalePitch
        ' 
        CmdScalePitch.AutoSize = True
        CmdScalePitch.Dock = DockStyle.Left
        CmdScalePitch.Location = New Point(323, 213)
        CmdScalePitch.Name = "CmdScalePitch"
        CmdScalePitch.Size = New Size(159, 34)
        CmdScalePitch.TabIndex = 7
        CmdScalePitch.Text = "Scale Wheel Pitch"
        CmdScalePitch.UseVisualStyleBackColor = True
        ' 
        ' CmdCalcAvg
        ' 
        CmdCalcAvg.AutoSize = True
        CmdCalcAvg.Dock = DockStyle.Left
        CmdCalcAvg.Location = New Point(323, 253)
        CmdCalcAvg.Name = "CmdCalcAvg"
        CmdCalcAvg.Size = New Size(162, 34)
        CmdCalcAvg.TabIndex = 8
        CmdCalcAvg.Text = "Calculate Average"
        CmdCalcAvg.UseVisualStyleBackColor = True
        ' 
        ' DGridProgTable
        ' 
        DGridProgTable.AllowUserToAddRows = False
        DGridProgTable.AllowUserToDeleteRows = False
        DGridProgTable.AllowUserToResizeColumns = False
        DGridProgTable.AllowUserToResizeRows = False
        DGridProgTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells
        DGridProgTable.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
        DGridProgTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DGridProgTable.ColumnHeadersVisible = False
        TLayoutProgManager.SetColumnSpan(DGridProgTable, 2)
        DGridProgTable.Dock = DockStyle.Fill
        DGridProgTable.Location = New Point(15, 320)
        DGridProgTable.Margin = New Padding(15, 0, 15, 15)
        DGridProgTable.Name = "DGridProgTable"
        DGridProgTable.RowHeadersVisible = False
        DGridProgTable.Size = New Size(1125, 326)
        DGridProgTable.TabIndex = 12
        ' 
        ' UCProgressionManager
        ' 
        AutoScaleDimensions = New SizeF(9F, 23F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(TLayoutProgManager)
        Font = New Font("Segoe UI", 13F)
        Margin = New Padding(4, 5, 4, 5)
        Name = "UCProgressionManager"
        Size = New Size(1155, 661)
        TLayoutProgManager.ResumeLayout(False)
        TLayoutProgManager.PerformLayout()
        CType(DGridProgTable, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents TLayoutProgManager As TableLayoutPanel
    Friend WithEvents CmdFillManual As Button
    Friend WithEvents CmdLoadCurrent As Button
    Friend WithEvents CmdLoadFile As Button
    Friend WithEvents CmdSavetoFile As Button
    Friend WithEvents CmdClearDesc As Button
    Friend WithEvents CmdClearTable As Button
    Friend WithEvents LabProgTable As Label
    Friend WithEvents CmdScalePitch As Button
    Friend WithEvents CmdCalcAvg As Button
    Friend WithEvents LabPitchProg As Label
    Friend WithEvents TxtDesc As RichTextBox
    Friend WithEvents LabPropDesc As Label
    Friend WithEvents DGridProgTable As DataGridView

End Class
