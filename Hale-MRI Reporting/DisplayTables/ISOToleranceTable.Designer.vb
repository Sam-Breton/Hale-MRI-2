<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ISOToleranceTable
    Inherits DisplayControl

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
        components = New ComponentModel.Container()
        TableLayoutPanel1 = New TableLayoutPanel()
        LabTitle = New Label()
        TLayoutISOTol = New TableLayoutPanel()
        ContextMenuStrip1 = New ContextMenuStrip(components)
        MinimumsApplyToolStripMenuItem = New ToolStripMenuItem()
        TableLayoutPanel1.SuspendLayout()
        ContextMenuStrip1.SuspendLayout()
        SuspendLayout()
        ' 
        ' TableLayoutPanel1
        ' 
        TableLayoutPanel1.ColumnCount = 1
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        TableLayoutPanel1.Controls.Add(LabTitle, 0, 0)
        TableLayoutPanel1.Controls.Add(TLayoutISOTol, 0, 1)
        TableLayoutPanel1.Dock = DockStyle.Fill
        TableLayoutPanel1.Location = New Point(2, 2)
        TableLayoutPanel1.Name = "TableLayoutPanel1"
        TableLayoutPanel1.RowCount = 2
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 10F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 90F))
        TableLayoutPanel1.Size = New Size(640, 169)
        TableLayoutPanel1.TabIndex = 0
        ' 
        ' LabTitle
        ' 
        LabTitle.AutoSize = True
        LabTitle.Dock = DockStyle.Fill
        LabTitle.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        LabTitle.Location = New Point(3, 0)
        LabTitle.Name = "LabTitle"
        LabTitle.Size = New Size(634, 16)
        LabTitle.TabIndex = 0
        LabTitle.Text = "ISO Tolerance Table"
        ' 
        ' TLayoutISOTol
        ' 
        TLayoutISOTol.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
        TLayoutISOTol.ColumnCount = 2
        TLayoutISOTol.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        TLayoutISOTol.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        TLayoutISOTol.Dock = DockStyle.Fill
        TLayoutISOTol.Location = New Point(3, 19)
        TLayoutISOTol.Name = "TLayoutISOTol"
        TLayoutISOTol.RowCount = 2
        TLayoutISOTol.RowStyles.Add(New RowStyle(SizeType.Percent, 50F))
        TLayoutISOTol.RowStyles.Add(New RowStyle(SizeType.Percent, 50F))
        TLayoutISOTol.Size = New Size(634, 147)
        TLayoutISOTol.TabIndex = 1
        ' 
        ' ContextMenuStrip1
        ' 
        ContextMenuStrip1.Items.AddRange(New ToolStripItem() {MinimumsApplyToolStripMenuItem})
        ContextMenuStrip1.Name = "ContextMenuStrip1"
        ContextMenuStrip1.Size = New Size(167, 26)
        ' 
        ' MinimumsApplyToolStripMenuItem
        ' 
        MinimumsApplyToolStripMenuItem.Checked = True
        MinimumsApplyToolStripMenuItem.CheckOnClick = True
        MinimumsApplyToolStripMenuItem.CheckState = CheckState.Checked
        MinimumsApplyToolStripMenuItem.Name = "MinimumsApplyToolStripMenuItem"
        MinimumsApplyToolStripMenuItem.Size = New Size(166, 22)
        MinimumsApplyToolStripMenuItem.Text = "Minimums Apply"
        ' 
        ' ISOToleranceTable
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(TableLayoutPanel1)
        IsMovable = True
        IsSelectable = True
        IsSizeable = True
        Name = "ISOToleranceTable"
        Padding = New Padding(2)
        Size = New Size(644, 173)
        TableLayoutPanel1.ResumeLayout(False)
        TableLayoutPanel1.PerformLayout()
        ContextMenuStrip1.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents LabTitle As Label
    Friend WithEvents TLayoutISOTol As TableLayoutPanel
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents MinimumsApplyToolStripMenuItem As ToolStripMenuItem

End Class
