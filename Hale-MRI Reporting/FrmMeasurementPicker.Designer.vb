Imports LibDatabase

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmMeasurementPicker
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmMeasurementPicker))
        TableLayoutPanel1 = New TableLayoutPanel()
        PanelButtons = New Panel()
        CmdCancel = New Button()
        CmdOK = New Button()
        DataGridJobs = New DataGridView()
        JobNumberDataGridViewTextBoxColumn = New DataGridViewTextBoxColumn()
        CustomerName = New DataGridViewTextBoxColumn()
        VesselName = New DataGridViewTextBoxColumn()
        StartDateDataGridViewTextBoxColumn = New DataGridViewTextBoxColumn()
        DescriptionDataGridViewTextBoxColumn = New DataGridViewTextBoxColumn()
        InspectedByName = New DataGridViewTextBoxColumn()
        JobsBindingSource = New BindingSource(components)
        DataGridMeasurements = New DataGridView()
        TxtJobs = New TextBox()
        TxtMeasurements = New TextBox()
        MeasurementTypeNameDataGridViewTextBoxColumn = New DataGridViewTextBoxColumn()
        DescriptionDataGridViewTextBoxColumn1 = New DataGridViewTextBoxColumn()
        StartDateDataGridViewTextBoxColumn1 = New DataGridViewTextBoxColumn()
        PerformedByNameDataGridViewTextBoxColumn = New DataGridViewTextBoxColumn()
        TableLayoutPanel1.SuspendLayout()
        PanelButtons.SuspendLayout()
        CType(DataGridJobs, ComponentModel.ISupportInitialize).BeginInit()
        CType(JobsBindingSource, ComponentModel.ISupportInitialize).BeginInit()
        CType(DataGridMeasurements, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' TableLayoutPanel1
        ' 
        TableLayoutPanel1.ColumnCount = 1
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        TableLayoutPanel1.Controls.Add(PanelButtons, 0, 4)
        TableLayoutPanel1.Controls.Add(DataGridJobs, 0, 1)
        TableLayoutPanel1.Controls.Add(DataGridMeasurements, 0, 3)
        TableLayoutPanel1.Controls.Add(TxtJobs, 0, 0)
        TableLayoutPanel1.Controls.Add(TxtMeasurements, 0, 2)
        TableLayoutPanel1.Dock = DockStyle.Fill
        TableLayoutPanel1.Location = New Point(0, 0)
        TableLayoutPanel1.Name = "TableLayoutPanel1"
        TableLayoutPanel1.RowCount = 5
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 66.6666641F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 33.3333359F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Absolute, 46F))
        TableLayoutPanel1.Size = New Size(800, 586)
        TableLayoutPanel1.TabIndex = 0
        ' 
        ' PanelButtons
        ' 
        PanelButtons.Controls.Add(CmdCancel)
        PanelButtons.Controls.Add(CmdOK)
        PanelButtons.Dock = DockStyle.Fill
        PanelButtons.Location = New Point(3, 542)
        PanelButtons.Name = "PanelButtons"
        PanelButtons.Size = New Size(794, 41)
        PanelButtons.TabIndex = 0
        ' 
        ' CmdCancel
        ' 
        CmdCancel.DialogResult = DialogResult.Cancel
        CmdCancel.Image = CType(resources.GetObject("CmdCancel.Image"), Image)
        CmdCancel.Location = New Point(40, 0)
        CmdCancel.Name = "CmdCancel"
        CmdCancel.Size = New Size(40, 40)
        CmdCancel.TabIndex = 6
        CmdCancel.UseVisualStyleBackColor = True
        ' 
        ' CmdOK
        ' 
        CmdOK.DialogResult = DialogResult.OK
        CmdOK.Image = CType(resources.GetObject("CmdOK.Image"), Image)
        CmdOK.Location = New Point(0, 0)
        CmdOK.Name = "CmdOK"
        CmdOK.Size = New Size(40, 40)
        CmdOK.TabIndex = 5
        CmdOK.UseVisualStyleBackColor = True
        ' 
        ' DataGridJobs
        ' 
        DataGridJobs.AutoGenerateColumns = False
        DataGridJobs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridJobs.Columns.AddRange(New DataGridViewColumn() {JobNumberDataGridViewTextBoxColumn, CustomerName, VesselName, StartDateDataGridViewTextBoxColumn, DescriptionDataGridViewTextBoxColumn, InspectedByName})
        DataGridJobs.DataSource = JobsBindingSource
        DataGridJobs.Dock = DockStyle.Fill
        DataGridJobs.Location = New Point(3, 32)
        DataGridJobs.Name = "DataGridJobs"
        DataGridJobs.Size = New Size(794, 315)
        DataGridJobs.TabIndex = 1
        ' 
        ' JobNumberDataGridViewTextBoxColumn
        ' 
        JobNumberDataGridViewTextBoxColumn.DataPropertyName = "JobNumber"
        JobNumberDataGridViewTextBoxColumn.HeaderText = "Job Number"
        JobNumberDataGridViewTextBoxColumn.Name = "JobNumberDataGridViewTextBoxColumn"
        ' 
        ' CustomerName
        ' 
        CustomerName.DataPropertyName = "CustomerName"
        CustomerName.HeaderText = "Customer"
        CustomerName.Name = "CustomerName"
        ' 
        ' VesselName
        ' 
        VesselName.DataPropertyName = "VesselName"
        VesselName.HeaderText = "Vessel"
        VesselName.Name = "VesselName"
        ' 
        ' StartDateDataGridViewTextBoxColumn
        ' 
        StartDateDataGridViewTextBoxColumn.DataPropertyName = "StartDate"
        StartDateDataGridViewTextBoxColumn.HeaderText = "Start Date"
        StartDateDataGridViewTextBoxColumn.Name = "StartDateDataGridViewTextBoxColumn"
        ' 
        ' DescriptionDataGridViewTextBoxColumn
        ' 
        DescriptionDataGridViewTextBoxColumn.DataPropertyName = "Description"
        DescriptionDataGridViewTextBoxColumn.HeaderText = "Description"
        DescriptionDataGridViewTextBoxColumn.Name = "DescriptionDataGridViewTextBoxColumn"
        ' 
        ' InspectedByName
        ' 
        InspectedByName.DataPropertyName = "InspectedByName"
        InspectedByName.HeaderText = "Inspected By"
        InspectedByName.Name = "InspectedByName"
        ' 
        ' JobsBindingSource
        ' 
        JobsBindingSource.DataSource = GetType(LibDatabase.Models.JobView)
        ' 
        ' DataGridMeasurements
        ' 
        DataGridMeasurements.AutoGenerateColumns = False
        DataGridMeasurements.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridMeasurements.Columns.AddRange(New DataGridViewColumn() {MeasurementTypeNameDataGridViewTextBoxColumn, DescriptionDataGridViewTextBoxColumn1, StartDateDataGridViewTextBoxColumn1, PerformedByNameDataGridViewTextBoxColumn})
        DataGridMeasurements.DataMember = "Measurements"
        DataGridMeasurements.DataSource = JobsBindingSource
        DataGridMeasurements.Dock = DockStyle.Fill
        DataGridMeasurements.Location = New Point(3, 382)
        DataGridMeasurements.Name = "DataGridMeasurements"
        DataGridMeasurements.Size = New Size(794, 154)
        DataGridMeasurements.TabIndex = 2
        ' 
        ' TxtJobs
        ' 
        TxtJobs.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        TxtJobs.Location = New Point(3, 3)
        TxtJobs.Name = "TxtJobs"
        TxtJobs.Size = New Size(794, 23)
        TxtJobs.TabIndex = 3
        TxtJobs.Text = "Jobs"
        ' 
        ' TxtMeasurements
        ' 
        TxtMeasurements.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        TxtMeasurements.Location = New Point(3, 353)
        TxtMeasurements.Name = "TxtMeasurements"
        TxtMeasurements.Size = New Size(794, 23)
        TxtMeasurements.TabIndex = 4
        TxtMeasurements.Text = "Measurements"
        ' 
        ' MeasurementTypeNameDataGridViewTextBoxColumn
        ' 
        MeasurementTypeNameDataGridViewTextBoxColumn.DataPropertyName = "MeasurementTypeName"
        MeasurementTypeNameDataGridViewTextBoxColumn.HeaderText = "Measurement Type"
        MeasurementTypeNameDataGridViewTextBoxColumn.MinimumWidth = 140
        MeasurementTypeNameDataGridViewTextBoxColumn.Name = "MeasurementTypeNameDataGridViewTextBoxColumn"
        MeasurementTypeNameDataGridViewTextBoxColumn.Width = 140
        ' 
        ' DescriptionDataGridViewTextBoxColumn1
        ' 
        DescriptionDataGridViewTextBoxColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader
        DescriptionDataGridViewTextBoxColumn1.DataPropertyName = "Description"
        DescriptionDataGridViewTextBoxColumn1.HeaderText = "Description"
        DescriptionDataGridViewTextBoxColumn1.MinimumWidth = 100
        DescriptionDataGridViewTextBoxColumn1.Name = "DescriptionDataGridViewTextBoxColumn1"
        ' 
        ' StartDateDataGridViewTextBoxColumn1
        ' 
        StartDateDataGridViewTextBoxColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader
        StartDateDataGridViewTextBoxColumn1.DataPropertyName = "StartDate"
        StartDateDataGridViewTextBoxColumn1.HeaderText = "Start Date"
        StartDateDataGridViewTextBoxColumn1.MinimumWidth = 140
        StartDateDataGridViewTextBoxColumn1.Name = "StartDateDataGridViewTextBoxColumn1"
        StartDateDataGridViewTextBoxColumn1.Width = 140
        ' 
        ' PerformedByNameDataGridViewTextBoxColumn
        ' 
        PerformedByNameDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader
        PerformedByNameDataGridViewTextBoxColumn.DataPropertyName = "PerformedByName"
        PerformedByNameDataGridViewTextBoxColumn.HeaderText = "Performed By"
        PerformedByNameDataGridViewTextBoxColumn.MinimumWidth = 120
        PerformedByNameDataGridViewTextBoxColumn.Name = "PerformedByNameDataGridViewTextBoxColumn"
        PerformedByNameDataGridViewTextBoxColumn.Width = 120
        ' 
        ' FrmMeasurementPicker
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 586)
        Controls.Add(TableLayoutPanel1)
        Name = "FrmMeasurementPicker"
        Text = "FrmJobPicker"
        TableLayoutPanel1.ResumeLayout(False)
        TableLayoutPanel1.PerformLayout()
        PanelButtons.ResumeLayout(False)
        CType(DataGridJobs, ComponentModel.ISupportInitialize).EndInit()
        CType(JobsBindingSource, ComponentModel.ISupportInitialize).EndInit()
        CType(DataGridMeasurements, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents PanelButtons As Panel
    Friend WithEvents CmdCancel As Button
    Friend WithEvents CmdOK As Button
    Friend WithEvents JobsBindingSource As BindingSource
    Friend WithEvents DataGridJobs As DataGridView
    Friend WithEvents DataGridMeasurements As DataGridView
    Friend WithEvents TxtJobs As TextBox
    Friend WithEvents TxtMeasurements As TextBox
    Friend WithEvents JobNumberDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents CustomerName As DataGridViewTextBoxColumn
    Friend WithEvents VesselName As DataGridViewTextBoxColumn
    Friend WithEvents StartDateDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents DescriptionDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents InspectedByName As DataGridViewTextBoxColumn
    Friend WithEvents CountDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents IsReadOnlyDataGridViewCheckBoxColumn As DataGridViewCheckBoxColumn
    Friend WithEvents MeasurementTypeNameDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents DescriptionDataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents StartDateDataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents PerformedByNameDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
End Class
