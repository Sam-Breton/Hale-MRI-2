Imports LibDatabase
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmReportPicker
    Inherits FrmDatabaseForm

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
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmReportPicker))
        ReportsBindingSource = New BindingSource(components)
        TableLayoutPanel1 = New TableLayoutPanel()
        DataGridReports = New DataGridView()
        PanelButtons = New Panel()
        CmdCancel = New Button()
        CmdOK = New Button()
        ReportNameDataGridViewTextBoxColumn = New DataGridViewTextBoxColumn()
        DescriptionDataGridViewTextBoxColumn = New DataGridViewTextBoxColumn()
        PageCountDataGridViewTextBoxColumn = New DataGridViewTextBoxColumn()
        LastModified = New DataGridViewTextBoxColumn()
        ModifiedByName = New DataGridViewTextBoxColumn()
        CType(ReportsBindingSource, ComponentModel.ISupportInitialize).BeginInit()
        TableLayoutPanel1.SuspendLayout()
        CType(DataGridReports, ComponentModel.ISupportInitialize).BeginInit()
        PanelButtons.SuspendLayout()
        SuspendLayout()
        ' 
        ' ReportsBindingSource
        ' 
        ReportsBindingSource.DataSource = GetType(LibDatabase.Models.ReportView)
        ReportsBindingSource.Sort = "ReportName"
        ' 
        ' TableLayoutPanel1
        ' 
        TableLayoutPanel1.ColumnCount = 1
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        TableLayoutPanel1.Controls.Add(DataGridReports, 0, 0)
        TableLayoutPanel1.Controls.Add(PanelButtons, 0, 1)
        TableLayoutPanel1.Dock = DockStyle.Fill
        TableLayoutPanel1.Location = New Point(0, 0)
        TableLayoutPanel1.Name = "TableLayoutPanel1"
        TableLayoutPanel1.RowCount = 2
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 90F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 10F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Absolute, 20F))
        TableLayoutPanel1.Size = New Size(800, 450)
        TableLayoutPanel1.TabIndex = 0
        ' 
        ' DataGridReports
        ' 
        DataGridReports.AutoGenerateColumns = False
        DataGridReports.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridReports.Columns.AddRange(New DataGridViewColumn() {ReportNameDataGridViewTextBoxColumn, DescriptionDataGridViewTextBoxColumn, PageCountDataGridViewTextBoxColumn, LastModified, ModifiedByName})
        DataGridReports.DataSource = ReportsBindingSource
        DataGridReports.Dock = DockStyle.Fill
        DataGridReports.Location = New Point(3, 3)
        DataGridReports.Name = "DataGridReports"
        DataGridReports.Size = New Size(794, 399)
        DataGridReports.TabIndex = 1
        ' 
        ' PanelButtons
        ' 
        PanelButtons.Controls.Add(CmdCancel)
        PanelButtons.Controls.Add(CmdOK)
        PanelButtons.Dock = DockStyle.Fill
        PanelButtons.Location = New Point(3, 408)
        PanelButtons.Name = "PanelButtons"
        PanelButtons.Size = New Size(794, 39)
        PanelButtons.TabIndex = 2
        ' 
        ' CmdCancel
        ' 
        CmdCancel.DialogResult = DialogResult.Cancel
        CmdCancel.Image = CType(resources.GetObject("CmdCancel.Image"), Image)
        CmdCancel.Location = New Point(40, 0)
        CmdCancel.Name = "CmdCancel"
        CmdCancel.Size = New Size(40, 39)
        CmdCancel.TabIndex = 4
        CmdCancel.UseVisualStyleBackColor = True
        ' 
        ' CmdOK
        ' 
        CmdOK.DialogResult = DialogResult.OK
        CmdOK.Image = CType(resources.GetObject("CmdOK.Image"), Image)
        CmdOK.Location = New Point(0, 0)
        CmdOK.Name = "CmdOK"
        CmdOK.Size = New Size(40, 39)
        CmdOK.TabIndex = 3
        CmdOK.UseVisualStyleBackColor = True
        ' 
        ' ReportNameDataGridViewTextBoxColumn
        ' 
        ReportNameDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader
        ReportNameDataGridViewTextBoxColumn.DataPropertyName = "ReportName"
        ReportNameDataGridViewTextBoxColumn.HeaderText = "Report Name"
        ReportNameDataGridViewTextBoxColumn.MinimumWidth = 120
        ReportNameDataGridViewTextBoxColumn.Name = "ReportNameDataGridViewTextBoxColumn"
        ReportNameDataGridViewTextBoxColumn.Width = 120
        ' 
        ' DescriptionDataGridViewTextBoxColumn
        ' 
        DescriptionDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader
        DescriptionDataGridViewTextBoxColumn.DataPropertyName = "Description"
        DescriptionDataGridViewTextBoxColumn.HeaderText = "Description"
        DescriptionDataGridViewTextBoxColumn.MinimumWidth = 100
        DescriptionDataGridViewTextBoxColumn.Name = "DescriptionDataGridViewTextBoxColumn"
        ' 
        ' PageCountDataGridViewTextBoxColumn
        ' 
        PageCountDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader
        PageCountDataGridViewTextBoxColumn.DataPropertyName = "PageCount"
        PageCountDataGridViewTextBoxColumn.HeaderText = "Page Count"
        PageCountDataGridViewTextBoxColumn.MinimumWidth = 100
        PageCountDataGridViewTextBoxColumn.Name = "PageCountDataGridViewTextBoxColumn"
        ' 
        ' LastModified
        ' 
        LastModified.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader
        LastModified.DataPropertyName = "LastModified"
        LastModified.HeaderText = "Last Modified"
        LastModified.MinimumWidth = 120
        LastModified.Name = "LastModified"
        LastModified.Width = 120
        ' 
        ' ModifiedByName
        ' 
        ModifiedByName.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader
        ModifiedByName.DataPropertyName = "ModifiedByName"
        ModifiedByName.HeaderText = "Modified By"
        ModifiedByName.MinimumWidth = 100
        ModifiedByName.Name = "ModifiedByName"
        ' 
        ' FrmReportPicker
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(TableLayoutPanel1)
        Name = "FrmReportPicker"
        Text = "FrmReportPicker"
        CType(ReportsBindingSource, ComponentModel.ISupportInitialize).EndInit()
        TableLayoutPanel1.ResumeLayout(False)
        CType(DataGridReports, ComponentModel.ISupportInitialize).EndInit()
        PanelButtons.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents ReportsBindingSource As BindingSource
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents DataGridReports As DataGridView
    Friend WithEvents PanelButtons As Panel
    Friend WithEvents CmdCancel As Button
    Friend WithEvents CmdOK As Button
    Friend WithEvents ReportNameDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents DescriptionDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents PageCountDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents LastModified As DataGridViewTextBoxColumn
    Friend WithEvents ModifiedByName As DataGridViewTextBoxColumn
End Class
