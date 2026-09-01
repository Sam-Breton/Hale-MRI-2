Imports LibDatabase
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmGraph
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
        Dim DataGridViewCellStyle5 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmGraph))
        tLayoutGraphBack = New TableLayoutPanel()
        RecordNavigationBar1 = New RecordNavigationBar()
        DataGridJobDetails = New DataGridView()
        StartDateDataGridViewTextBoxColumn = New DataGridViewTextBoxColumn()
        MeasurementTypeDataGridViewTextBoxColumn = New DataGridViewComboBoxColumn()
        MeasurementTypesBindingSource = New BindingSource(components)
        ToleranceClassDataGridViewTextBoxColumn = New DataGridViewTextBoxColumn()
        PerformedByDataGridViewTextBoxColumn = New DataGridViewComboBoxColumn()
        EmployeesBindingSource = New BindingSource(components)
        DescriptionDataGridViewTextBoxColumn = New DataGridViewTextBoxColumn()
        JobDetailsBindingSource = New BindingSource(components)
        PictLogo = New PictureBox()
        tLayoutForms = New TableLayoutPanel()
        CmdComparisonForm = New Button()
        CmdInspectForm = New Button()
        CmdGraphForm = New Button()
        CmdLocalPitchForm = New Button()
        CmdMeasureForm = New Button()
        GraphPanel = New Panel()
        GroupChartType = New GroupBox()
        tlayoutChartType = New TableLayoutPanel()
        CmdSectorsbyBladeRadio = New RadioButton()
        CmdExpSectionRadio = New RadioButton()
        CmdSummaryRadio = New RadioButton()
        CmdBladesbySectorRadio = New RadioButton()
        CmdPositionRadio = New RadioButton()
        LabChartType = New Label()
        GroupTolerance = New GroupBox()
        LabTolerance = New Label()
        tlayouttolerance = New TableLayoutPanel()
        ChkAllowProgressivePitch = New CheckBox()
        ComboTolerance = New ComboBox()
        MenuStrip1 = New MenuStrip()
        BladesToolStripMenuItem = New ToolStripMenuItem()
        RadiiToolStripMenuItem = New ToolStripMenuItem()
        tLayoutAllandClear = New TableLayoutPanel()
        CmdClearRadii = New Button()
        CmdClearBlades = New Button()
        CmdAllRadii = New Button()
        CmdAllBlades = New Button()
        GroupBasis = New GroupBox()
        tLayoutBasis = New TableLayoutPanel()
        ComboBasis = New ComboBox()
        tBoxBasis = New TextBox()
        ToleranceBindingSource = New BindingSource(components)
        ClassBindingSource = New BindingSource(components)
        tLayoutGraphBack.SuspendLayout()
        CType(DataGridJobDetails, ComponentModel.ISupportInitialize).BeginInit()
        CType(MeasurementTypesBindingSource, ComponentModel.ISupportInitialize).BeginInit()
        CType(EmployeesBindingSource, ComponentModel.ISupportInitialize).BeginInit()
        CType(JobDetailsBindingSource, ComponentModel.ISupportInitialize).BeginInit()
        CType(PictLogo, ComponentModel.ISupportInitialize).BeginInit()
        tLayoutForms.SuspendLayout()
        GroupChartType.SuspendLayout()
        tlayoutChartType.SuspendLayout()
        GroupTolerance.SuspendLayout()
        tlayouttolerance.SuspendLayout()
        MenuStrip1.SuspendLayout()
        tLayoutAllandClear.SuspendLayout()
        GroupBasis.SuspendLayout()
        tLayoutBasis.SuspendLayout()
        CType(ToleranceBindingSource, ComponentModel.ISupportInitialize).BeginInit()
        CType(ClassBindingSource, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' tLayoutGraphBack
        ' 
        tLayoutGraphBack.ColumnCount = 6
        tLayoutGraphBack.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 216F))
        tLayoutGraphBack.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
        tLayoutGraphBack.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
        tLayoutGraphBack.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
        tLayoutGraphBack.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
        tLayoutGraphBack.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
        tLayoutGraphBack.Controls.Add(RecordNavigationBar1, 2, 0)
        tLayoutGraphBack.Controls.Add(DataGridJobDetails, 3, 1)
        tLayoutGraphBack.Controls.Add(PictLogo, 0, 0)
        tLayoutGraphBack.Controls.Add(tLayoutForms, 1, 1)
        tLayoutGraphBack.Controls.Add(GraphPanel, 1, 2)
        tLayoutGraphBack.Controls.Add(GroupChartType, 0, 2)
        tLayoutGraphBack.Controls.Add(GroupTolerance, 0, 3)
        tLayoutGraphBack.Controls.Add(MenuStrip1, 0, 6)
        tLayoutGraphBack.Controls.Add(tLayoutAllandClear, 0, 5)
        tLayoutGraphBack.Controls.Add(GroupBasis, 0, 4)
        tLayoutGraphBack.Dock = DockStyle.Fill
        tLayoutGraphBack.Location = New Point(0, 0)
        tLayoutGraphBack.Name = "tLayoutGraphBack"
        tLayoutGraphBack.RowCount = 8
        tLayoutGraphBack.RowStyles.Add(New RowStyle(SizeType.Absolute, 36F))
        tLayoutGraphBack.RowStyles.Add(New RowStyle(SizeType.Absolute, 71F))
        tLayoutGraphBack.RowStyles.Add(New RowStyle())
        tLayoutGraphBack.RowStyles.Add(New RowStyle())
        tLayoutGraphBack.RowStyles.Add(New RowStyle())
        tLayoutGraphBack.RowStyles.Add(New RowStyle())
        tLayoutGraphBack.RowStyles.Add(New RowStyle())
        tLayoutGraphBack.RowStyles.Add(New RowStyle(SizeType.Percent, 99.99999F))
        tLayoutGraphBack.RowStyles.Add(New RowStyle(SizeType.Absolute, 20F))
        tLayoutGraphBack.Size = New Size(1184, 641)
        tLayoutGraphBack.TabIndex = 0
        ' 
        ' RecordNavigationBar1
        ' 
        RecordNavigationBar1.AutoSize = True
        RecordNavigationBar1.AutoSizeMode = AutoSizeMode.GrowAndShrink
        RecordNavigationBar1.BoundControls = Nothing
        tLayoutGraphBack.SetColumnSpan(RecordNavigationBar1, 4)
        RecordNavigationBar1.Database = Nothing
        RecordNavigationBar1.Dock = DockStyle.Right
        RecordNavigationBar1.Filter = Nothing
        RecordNavigationBar1.FilterOn = False
        RecordNavigationBar1.Font = New Font("Segoe UI", 10F)
        RecordNavigationBar1.Location = New Point(549, 0)
        RecordNavigationBar1.Margin = New Padding(0)
        RecordNavigationBar1.MasterSource = Nothing
        RecordNavigationBar1.Name = "RecordNavigationBar1"
        RecordNavigationBar1.NoUpdates = False
        RecordNavigationBar1.ServiceProvider = Nothing
        RecordNavigationBar1.Size = New Size(635, 36)
        RecordNavigationBar1.TabIndex = 0
        ' 
        ' DataGridJobDetails
        ' 
        DataGridJobDetails.AllowUserToAddRows = False
        DataGridJobDetails.AllowUserToDeleteRows = False
        DataGridJobDetails.AutoGenerateColumns = False
        DataGridJobDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        DataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = SystemColors.Control
        DataGridViewCellStyle5.Font = New Font("Segoe UI", 13F)
        DataGridViewCellStyle5.ForeColor = SystemColors.WindowText
        DataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = DataGridViewTriState.True
        DataGridJobDetails.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle5
        DataGridJobDetails.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridJobDetails.Columns.AddRange(New DataGridViewColumn() {StartDateDataGridViewTextBoxColumn, MeasurementTypeDataGridViewTextBoxColumn, ToleranceClassDataGridViewTextBoxColumn, PerformedByDataGridViewTextBoxColumn, DescriptionDataGridViewTextBoxColumn})
        tLayoutGraphBack.SetColumnSpan(DataGridJobDetails, 3)
        DataGridJobDetails.DataSource = JobDetailsBindingSource
        DataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = SystemColors.Window
        DataGridViewCellStyle6.Font = New Font("Segoe UI", 13F)
        DataGridViewCellStyle6.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle6.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = DataGridViewTriState.False
        DataGridJobDetails.DefaultCellStyle = DataGridViewCellStyle6
        DataGridJobDetails.Dock = DockStyle.Fill
        DataGridJobDetails.Location = New Point(605, 39)
        DataGridJobDetails.Name = "DataGridJobDetails"
        DataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = SystemColors.Control
        DataGridViewCellStyle7.Font = New Font("Segoe UI", 13F)
        DataGridViewCellStyle7.ForeColor = SystemColors.WindowText
        DataGridViewCellStyle7.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = DataGridViewTriState.True
        DataGridJobDetails.RowHeadersDefaultCellStyle = DataGridViewCellStyle7
        DataGridJobDetails.RowHeadersVisible = False
        DataGridViewCellStyle8.Font = New Font("Segoe UI", 13F)
        DataGridJobDetails.RowsDefaultCellStyle = DataGridViewCellStyle8
        DataGridJobDetails.Size = New Size(576, 65)
        DataGridJobDetails.TabIndex = 1
        ' 
        ' StartDateDataGridViewTextBoxColumn
        ' 
        StartDateDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        StartDateDataGridViewTextBoxColumn.DataPropertyName = "StartDate"
        StartDateDataGridViewTextBoxColumn.HeaderText = "Start Date"
        StartDateDataGridViewTextBoxColumn.Name = "StartDateDataGridViewTextBoxColumn"
        StartDateDataGridViewTextBoxColumn.ReadOnly = True
        StartDateDataGridViewTextBoxColumn.Width = 115
        ' 
        ' MeasurementTypeDataGridViewTextBoxColumn
        ' 
        MeasurementTypeDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        MeasurementTypeDataGridViewTextBoxColumn.DataPropertyName = "MeasurementTypeId"
        MeasurementTypeDataGridViewTextBoxColumn.DataSource = MeasurementTypesBindingSource
        MeasurementTypeDataGridViewTextBoxColumn.DisplayMember = "MeasurementType1"
        MeasurementTypeDataGridViewTextBoxColumn.HeaderText = "Stage"
        MeasurementTypeDataGridViewTextBoxColumn.Name = "MeasurementTypeDataGridViewTextBoxColumn"
        MeasurementTypeDataGridViewTextBoxColumn.Resizable = DataGridViewTriState.True
        MeasurementTypeDataGridViewTextBoxColumn.SortMode = DataGridViewColumnSortMode.Automatic
        MeasurementTypeDataGridViewTextBoxColumn.ValueMember = "Id"
        MeasurementTypeDataGridViewTextBoxColumn.Width = 81
        ' 
        ' MeasurementTypesBindingSource
        ' 
        MeasurementTypesBindingSource.DataSource = GetType(Models.MeasurementType)
        ' 
        ' ToleranceClassDataGridViewTextBoxColumn
        ' 
        ToleranceClassDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        ToleranceClassDataGridViewTextBoxColumn.DataPropertyName = "ToleranceClass"
        ToleranceClassDataGridViewTextBoxColumn.HeaderText = "Class"
        ToleranceClassDataGridViewTextBoxColumn.Name = "ToleranceClassDataGridViewTextBoxColumn"
        ToleranceClassDataGridViewTextBoxColumn.ReadOnly = True
        ToleranceClassDataGridViewTextBoxColumn.Resizable = DataGridViewTriState.True
        ToleranceClassDataGridViewTextBoxColumn.Width = 77
        ' 
        ' PerformedByDataGridViewTextBoxColumn
        ' 
        PerformedByDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        PerformedByDataGridViewTextBoxColumn.DataPropertyName = "PerformedBy"
        PerformedByDataGridViewTextBoxColumn.DataSource = EmployeesBindingSource
        PerformedByDataGridViewTextBoxColumn.DisplayMember = "EmployeeName"
        PerformedByDataGridViewTextBoxColumn.HeaderText = "Employee"
        PerformedByDataGridViewTextBoxColumn.Name = "PerformedByDataGridViewTextBoxColumn"
        PerformedByDataGridViewTextBoxColumn.Resizable = DataGridViewTriState.True
        PerformedByDataGridViewTextBoxColumn.SortMode = DataGridViewColumnSortMode.Automatic
        PerformedByDataGridViewTextBoxColumn.ValueMember = "Id"
        PerformedByDataGridViewTextBoxColumn.Width = 115
        ' 
        ' EmployeesBindingSource
        ' 
        EmployeesBindingSource.DataSource = GetType(Models.Employee)
        ' 
        ' DescriptionDataGridViewTextBoxColumn
        ' 
        DescriptionDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        DescriptionDataGridViewTextBoxColumn.DataPropertyName = "Description"
        DescriptionDataGridViewTextBoxColumn.HeaderText = "Description"
        DescriptionDataGridViewTextBoxColumn.Name = "DescriptionDataGridViewTextBoxColumn"
        DescriptionDataGridViewTextBoxColumn.ReadOnly = True
        ' 
        ' JobDetailsBindingSource
        ' 
        JobDetailsBindingSource.DataSource = GetType(Models.JobDetail)
        ' 
        ' PictLogo
        ' 
        PictLogo.BackgroundImage = CType(resources.GetObject("PictLogo.BackgroundImage"), Image)
        PictLogo.BackgroundImageLayout = ImageLayout.Stretch
        PictLogo.Dock = DockStyle.Fill
        PictLogo.Location = New Point(0, 0)
        PictLogo.Margin = New Padding(0)
        PictLogo.Name = "PictLogo"
        tLayoutGraphBack.SetRowSpan(PictLogo, 2)
        PictLogo.Size = New Size(216, 107)
        PictLogo.TabIndex = 2
        PictLogo.TabStop = False
        ' 
        ' tLayoutForms
        ' 
        tLayoutForms.AutoSize = True
        tLayoutForms.ColumnCount = 5
        tLayoutGraphBack.SetColumnSpan(tLayoutForms, 2)
        tLayoutForms.ColumnStyles.Add(New ColumnStyle())
        tLayoutForms.ColumnStyles.Add(New ColumnStyle())
        tLayoutForms.ColumnStyles.Add(New ColumnStyle())
        tLayoutForms.ColumnStyles.Add(New ColumnStyle())
        tLayoutForms.ColumnStyles.Add(New ColumnStyle())
        tLayoutForms.Controls.Add(CmdComparisonForm, 4, 0)
        tLayoutForms.Controls.Add(CmdInspectForm, 3, 0)
        tLayoutForms.Controls.Add(CmdGraphForm, 2, 0)
        tLayoutForms.Controls.Add(CmdLocalPitchForm, 1, 0)
        tLayoutForms.Controls.Add(CmdMeasureForm, 0, 0)
        tLayoutForms.Dock = DockStyle.Left
        tLayoutForms.Location = New Point(216, 36)
        tLayoutForms.Margin = New Padding(0)
        tLayoutForms.Name = "tLayoutForms"
        tLayoutForms.RowCount = 1
        tLayoutForms.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tLayoutForms.Size = New Size(386, 71)
        tLayoutForms.TabIndex = 3
        ' 
        ' CmdComparisonForm
        ' 
        CmdComparisonForm.AutoSize = True
        CmdComparisonForm.Dock = DockStyle.Fill
        CmdComparisonForm.Location = New Point(343, 3)
        CmdComparisonForm.Name = "CmdComparisonForm"
        CmdComparisonForm.Size = New Size(72, 65)
        CmdComparisonForm.TabIndex = 4
        CmdComparisonForm.Text = "Comp."
        CmdComparisonForm.UseVisualStyleBackColor = True
        ' 
        ' CmdInspectForm
        ' 
        CmdInspectForm.AutoSize = True
        CmdInspectForm.Dock = DockStyle.Fill
        CmdInspectForm.Location = New Point(266, 3)
        CmdInspectForm.Name = "CmdInspectForm"
        CmdInspectForm.Size = New Size(71, 65)
        CmdInspectForm.TabIndex = 3
        CmdInspectForm.Text = "Inspect"
        CmdInspectForm.UseVisualStyleBackColor = True
        ' 
        ' CmdGraphForm
        ' 
        CmdGraphForm.AutoSize = True
        CmdGraphForm.Dock = DockStyle.Fill
        CmdGraphForm.Enabled = False
        CmdGraphForm.Location = New Point(189, 3)
        CmdGraphForm.Name = "CmdGraphForm"
        CmdGraphForm.Size = New Size(71, 65)
        CmdGraphForm.TabIndex = 2
        CmdGraphForm.Text = "Graph"
        CmdGraphForm.UseVisualStyleBackColor = True
        ' 
        ' CmdLocalPitchForm
        ' 
        CmdLocalPitchForm.AutoSize = True
        CmdLocalPitchForm.Dock = DockStyle.Fill
        CmdLocalPitchForm.Location = New Point(89, 3)
        CmdLocalPitchForm.Name = "CmdLocalPitchForm"
        CmdLocalPitchForm.Size = New Size(94, 65)
        CmdLocalPitchForm.TabIndex = 1
        CmdLocalPitchForm.Text = "Local Pitch"
        CmdLocalPitchForm.UseVisualStyleBackColor = True
        ' 
        ' CmdMeasureForm
        ' 
        CmdMeasureForm.AutoSize = True
        CmdMeasureForm.Dock = DockStyle.Fill
        CmdMeasureForm.Location = New Point(3, 3)
        CmdMeasureForm.Name = "CmdMeasureForm"
        CmdMeasureForm.Size = New Size(80, 65)
        CmdMeasureForm.TabIndex = 0
        CmdMeasureForm.Text = "Measure"
        CmdMeasureForm.UseVisualStyleBackColor = True
        ' 
        ' GraphPanel
        ' 
        tLayoutGraphBack.SetColumnSpan(GraphPanel, 5)
        GraphPanel.Dock = DockStyle.Fill
        GraphPanel.Location = New Point(216, 107)
        GraphPanel.Margin = New Padding(0)
        GraphPanel.Name = "GraphPanel"
        tLayoutGraphBack.SetRowSpan(GraphPanel, 6)
        GraphPanel.Size = New Size(968, 534)
        GraphPanel.TabIndex = 4
        ' 
        ' GroupChartType
        ' 
        GroupChartType.AutoSize = True
        GroupChartType.AutoSizeMode = AutoSizeMode.GrowAndShrink
        GroupChartType.Controls.Add(tlayoutChartType)
        GroupChartType.Controls.Add(LabChartType)
        GroupChartType.Dock = DockStyle.Fill
        GroupChartType.FlatStyle = FlatStyle.System
        GroupChartType.ForeColor = SystemColors.ActiveCaptionText
        GroupChartType.Location = New Point(1, 108)
        GroupChartType.Margin = New Padding(1)
        GroupChartType.Name = "GroupChartType"
        GroupChartType.Padding = New Padding(2)
        GroupChartType.Size = New Size(214, 181)
        GroupChartType.TabIndex = 5
        GroupChartType.TabStop = False
        ' 
        ' tlayoutChartType
        ' 
        tlayoutChartType.AutoSize = True
        tlayoutChartType.ColumnCount = 1
        tlayoutChartType.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tlayoutChartType.Controls.Add(CmdSectorsbyBladeRadio, 0, 2)
        tlayoutChartType.Controls.Add(CmdExpSectionRadio, 0, 4)
        tlayoutChartType.Controls.Add(CmdSummaryRadio, 0, 3)
        tlayoutChartType.Controls.Add(CmdBladesbySectorRadio, 0, 1)
        tlayoutChartType.Controls.Add(CmdPositionRadio, 0, 0)
        tlayoutChartType.Dock = DockStyle.Fill
        tlayoutChartType.Location = New Point(2, 24)
        tlayoutChartType.Name = "tlayoutChartType"
        tlayoutChartType.RowCount = 5
        tlayoutChartType.RowStyles.Add(New RowStyle())
        tlayoutChartType.RowStyles.Add(New RowStyle())
        tlayoutChartType.RowStyles.Add(New RowStyle())
        tlayoutChartType.RowStyles.Add(New RowStyle())
        tlayoutChartType.RowStyles.Add(New RowStyle())
        tlayoutChartType.Size = New Size(210, 155)
        tlayoutChartType.TabIndex = 1
        ' 
        ' CmdSectorsbyBladeRadio
        ' 
        CmdSectorsbyBladeRadio.AutoSize = True
        CmdSectorsbyBladeRadio.Dock = DockStyle.Fill
        CmdSectorsbyBladeRadio.Location = New Point(15, 65)
        CmdSectorsbyBladeRadio.Margin = New Padding(15, 3, 3, 3)
        CmdSectorsbyBladeRadio.Name = "CmdSectorsbyBladeRadio"
        CmdSectorsbyBladeRadio.Size = New Size(192, 25)
        CmdSectorsbyBladeRadio.TabIndex = 4
        CmdSectorsbyBladeRadio.TabStop = True
        CmdSectorsbyBladeRadio.Text = "Sectors by Blade"
        CmdSectorsbyBladeRadio.UseVisualStyleBackColor = True
        ' 
        ' CmdExpSectionRadio
        ' 
        CmdExpSectionRadio.AutoSize = True
        CmdExpSectionRadio.Dock = DockStyle.Fill
        CmdExpSectionRadio.Location = New Point(15, 127)
        CmdExpSectionRadio.Margin = New Padding(15, 3, 3, 3)
        CmdExpSectionRadio.Name = "CmdExpSectionRadio"
        CmdExpSectionRadio.Size = New Size(192, 25)
        CmdExpSectionRadio.TabIndex = 3
        CmdExpSectionRadio.TabStop = True
        CmdExpSectionRadio.Text = "Expanded Sections"
        CmdExpSectionRadio.UseVisualStyleBackColor = True
        ' 
        ' CmdSummaryRadio
        ' 
        CmdSummaryRadio.AutoSize = True
        CmdSummaryRadio.Dock = DockStyle.Fill
        CmdSummaryRadio.Location = New Point(15, 96)
        CmdSummaryRadio.Margin = New Padding(15, 3, 3, 3)
        CmdSummaryRadio.Name = "CmdSummaryRadio"
        CmdSummaryRadio.Size = New Size(192, 25)
        CmdSummaryRadio.TabIndex = 2
        CmdSummaryRadio.TabStop = True
        CmdSummaryRadio.Text = "Summary"
        CmdSummaryRadio.UseVisualStyleBackColor = True
        ' 
        ' CmdBladesbySectorRadio
        ' 
        CmdBladesbySectorRadio.AutoSize = True
        CmdBladesbySectorRadio.Dock = DockStyle.Fill
        CmdBladesbySectorRadio.Location = New Point(15, 34)
        CmdBladesbySectorRadio.Margin = New Padding(15, 3, 3, 3)
        CmdBladesbySectorRadio.Name = "CmdBladesbySectorRadio"
        CmdBladesbySectorRadio.Size = New Size(192, 25)
        CmdBladesbySectorRadio.TabIndex = 1
        CmdBladesbySectorRadio.TabStop = True
        CmdBladesbySectorRadio.Text = "Blades by Sector"
        CmdBladesbySectorRadio.UseVisualStyleBackColor = True
        ' 
        ' CmdPositionRadio
        ' 
        CmdPositionRadio.AutoSize = True
        CmdPositionRadio.Dock = DockStyle.Fill
        CmdPositionRadio.Location = New Point(15, 3)
        CmdPositionRadio.Margin = New Padding(15, 3, 3, 3)
        CmdPositionRadio.Name = "CmdPositionRadio"
        CmdPositionRadio.Size = New Size(192, 25)
        CmdPositionRadio.TabIndex = 0
        CmdPositionRadio.TabStop = True
        CmdPositionRadio.Text = "Position"
        CmdPositionRadio.UseVisualStyleBackColor = True
        ' 
        ' LabChartType
        ' 
        LabChartType.AutoSize = True
        LabChartType.Location = New Point(-1, -4)
        LabChartType.Name = "LabChartType"
        LabChartType.Size = New Size(84, 21)
        LabChartType.TabIndex = 0
        LabChartType.Text = "Chart Type"
        ' 
        ' GroupTolerance
        ' 
        GroupTolerance.Controls.Add(LabTolerance)
        GroupTolerance.Controls.Add(tlayouttolerance)
        GroupTolerance.Dock = DockStyle.Fill
        GroupTolerance.Location = New Point(1, 291)
        GroupTolerance.Margin = New Padding(1)
        GroupTolerance.Name = "GroupTolerance"
        GroupTolerance.Padding = New Padding(2)
        GroupTolerance.Size = New Size(214, 85)
        GroupTolerance.TabIndex = 6
        GroupTolerance.TabStop = False
        ' 
        ' LabTolerance
        ' 
        LabTolerance.AutoSize = True
        LabTolerance.Location = New Point(2, -1)
        LabTolerance.Name = "LabTolerance"
        LabTolerance.Size = New Size(75, 21)
        LabTolerance.TabIndex = 1
        LabTolerance.Text = "Tolerance"
        ' 
        ' tlayouttolerance
        ' 
        tlayouttolerance.AutoSize = True
        tlayouttolerance.ColumnCount = 1
        tlayouttolerance.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tlayouttolerance.Controls.Add(ChkAllowProgressivePitch, 0, 1)
        tlayouttolerance.Controls.Add(ComboTolerance, 0, 0)
        tlayouttolerance.Dock = DockStyle.Fill
        tlayouttolerance.Location = New Point(2, 24)
        tlayouttolerance.Name = "tlayouttolerance"
        tlayouttolerance.RowCount = 2
        tlayouttolerance.RowStyles.Add(New RowStyle())
        tlayouttolerance.RowStyles.Add(New RowStyle())
        tlayouttolerance.Size = New Size(210, 59)
        tlayouttolerance.TabIndex = 0
        ' 
        ' ChkAllowProgressivePitch
        ' 
        ChkAllowProgressivePitch.Dock = DockStyle.Fill
        ChkAllowProgressivePitch.Location = New Point(3, 38)
        ChkAllowProgressivePitch.Name = "ChkAllowProgressivePitch"
        ChkAllowProgressivePitch.Size = New Size(204, 25)
        ChkAllowProgressivePitch.TabIndex = 0
        ChkAllowProgressivePitch.Text = "Allow Progressive Pitch"
        ChkAllowProgressivePitch.UseVisualStyleBackColor = True
        ' 
        ' ComboTolerance
        ' 
        ComboTolerance.Dock = DockStyle.Top
        ComboTolerance.FormattingEnabled = True
        ComboTolerance.Location = New Point(3, 3)
        ComboTolerance.Name = "ComboTolerance"
        ComboTolerance.Size = New Size(204, 29)
        ComboTolerance.TabIndex = 1
        ' 
        ' MenuStrip1
        ' 
        MenuStrip1.Font = New Font("Segoe UI", 12F)
        MenuStrip1.Items.AddRange(New ToolStripItem() {BladesToolStripMenuItem, RadiiToolStripMenuItem})
        MenuStrip1.Location = New Point(0, 522)
        MenuStrip1.Name = "MenuStrip1"
        MenuStrip1.Size = New Size(216, 29)
        MenuStrip1.TabIndex = 7
        ' 
        ' BladesToolStripMenuItem
        ' 
        BladesToolStripMenuItem.Name = "BladesToolStripMenuItem"
        BladesToolStripMenuItem.Size = New Size(67, 25)
        BladesToolStripMenuItem.Text = "Blades"
        ' 
        ' RadiiToolStripMenuItem
        ' 
        RadiiToolStripMenuItem.Alignment = ToolStripItemAlignment.Right
        RadiiToolStripMenuItem.Name = "RadiiToolStripMenuItem"
        RadiiToolStripMenuItem.Size = New Size(57, 25)
        RadiiToolStripMenuItem.Text = "Radii"
        ' 
        ' tLayoutAllandClear
        ' 
        tLayoutAllandClear.AutoSize = True
        tLayoutAllandClear.ColumnCount = 2
        tLayoutAllandClear.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        tLayoutAllandClear.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        tLayoutAllandClear.Controls.Add(CmdClearRadii, 1, 1)
        tLayoutAllandClear.Controls.Add(CmdClearBlades, 0, 1)
        tLayoutAllandClear.Controls.Add(CmdAllRadii, 1, 0)
        tLayoutAllandClear.Controls.Add(CmdAllBlades, 0, 0)
        tLayoutAllandClear.Dock = DockStyle.Fill
        tLayoutAllandClear.Location = New Point(0, 440)
        tLayoutAllandClear.Margin = New Padding(0)
        tLayoutAllandClear.Name = "tLayoutAllandClear"
        tLayoutAllandClear.RowCount = 2
        tLayoutAllandClear.RowStyles.Add(New RowStyle(SizeType.Percent, 50F))
        tLayoutAllandClear.RowStyles.Add(New RowStyle(SizeType.Percent, 50F))
        tLayoutAllandClear.Size = New Size(216, 82)
        tLayoutAllandClear.TabIndex = 8
        ' 
        ' CmdClearRadii
        ' 
        CmdClearRadii.Dock = DockStyle.Fill
        CmdClearRadii.Location = New Point(110, 43)
        CmdClearRadii.Margin = New Padding(2)
        CmdClearRadii.Name = "CmdClearRadii"
        CmdClearRadii.Size = New Size(104, 37)
        CmdClearRadii.TabIndex = 3
        CmdClearRadii.Text = "Clear Radii"
        CmdClearRadii.UseVisualStyleBackColor = True
        ' 
        ' CmdClearBlades
        ' 
        CmdClearBlades.Dock = DockStyle.Fill
        CmdClearBlades.Location = New Point(2, 43)
        CmdClearBlades.Margin = New Padding(2)
        CmdClearBlades.Name = "CmdClearBlades"
        CmdClearBlades.Size = New Size(104, 37)
        CmdClearBlades.TabIndex = 2
        CmdClearBlades.Text = "Clear Blades"
        CmdClearBlades.UseVisualStyleBackColor = True
        ' 
        ' CmdAllRadii
        ' 
        CmdAllRadii.Dock = DockStyle.Fill
        CmdAllRadii.Location = New Point(110, 2)
        CmdAllRadii.Margin = New Padding(2)
        CmdAllRadii.Name = "CmdAllRadii"
        CmdAllRadii.Size = New Size(104, 37)
        CmdAllRadii.TabIndex = 1
        CmdAllRadii.Text = "All Radii"
        CmdAllRadii.UseVisualStyleBackColor = True
        ' 
        ' CmdAllBlades
        ' 
        CmdAllBlades.Dock = DockStyle.Fill
        CmdAllBlades.Location = New Point(2, 2)
        CmdAllBlades.Margin = New Padding(2)
        CmdAllBlades.Name = "CmdAllBlades"
        CmdAllBlades.Size = New Size(104, 37)
        CmdAllBlades.TabIndex = 0
        CmdAllBlades.Text = "All Blades"
        CmdAllBlades.UseVisualStyleBackColor = True
        ' 
        ' GroupBasis
        ' 
        GroupBasis.AutoSize = True
        GroupBasis.Controls.Add(tLayoutBasis)
        GroupBasis.Dock = DockStyle.Fill
        GroupBasis.Location = New Point(1, 378)
        GroupBasis.Margin = New Padding(1)
        GroupBasis.Name = "GroupBasis"
        GroupBasis.Padding = New Padding(2)
        GroupBasis.Size = New Size(214, 61)
        GroupBasis.TabIndex = 9
        GroupBasis.TabStop = False
        GroupBasis.Text = "Basis"
        ' 
        ' tLayoutBasis
        ' 
        tLayoutBasis.AutoSize = True
        tLayoutBasis.ColumnCount = 2
        tLayoutBasis.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        tLayoutBasis.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        tLayoutBasis.Controls.Add(ComboBasis, 0, 0)
        tLayoutBasis.Controls.Add(tBoxBasis, 1, 0)
        tLayoutBasis.Dock = DockStyle.Fill
        tLayoutBasis.Location = New Point(2, 24)
        tLayoutBasis.Margin = New Padding(0)
        tLayoutBasis.Name = "tLayoutBasis"
        tLayoutBasis.RowCount = 1
        tLayoutBasis.RowStyles.Add(New RowStyle(SizeType.Percent, 50F))
        tLayoutBasis.RowStyles.Add(New RowStyle(SizeType.Percent, 50F))
        tLayoutBasis.Size = New Size(210, 35)
        tLayoutBasis.TabIndex = 0
        ' 
        ' ComboBasis
        ' 
        ComboBasis.Dock = DockStyle.Top
        ComboBasis.FormattingEnabled = True
        ComboBasis.Location = New Point(1, 3)
        ComboBasis.Margin = New Padding(1, 3, 2, 3)
        ComboBasis.Name = "ComboBasis"
        ComboBasis.Size = New Size(102, 29)
        ComboBasis.TabIndex = 0
        ' 
        ' tBoxBasis
        ' 
        tBoxBasis.Dock = DockStyle.Top
        tBoxBasis.Location = New Point(107, 3)
        tBoxBasis.Margin = New Padding(2, 3, 1, 3)
        tBoxBasis.Name = "tBoxBasis"
        tBoxBasis.Size = New Size(102, 29)
        tBoxBasis.TabIndex = 1
        ' 
        ' ToleranceBindingSource
        ' 
        ToleranceBindingSource.DataSource = GetType(Models.Tolerance)
        ' 
        ' ClassBindingSource
        ' 
        ClassBindingSource.DataSource = GetType(Models.Tolerance)
        ' 
        ' FrmGraph
        ' 
        AutoScaleMode = AutoScaleMode.None
        ClientSize = New Size(1184, 641)
        Controls.Add(tLayoutGraphBack)
        Font = New Font("Segoe UI", 12F)
        MainMenuStrip = MenuStrip1
        Margin = New Padding(4)
        Name = "FrmGraph"
        Text = "FrmGraph"
        tLayoutGraphBack.ResumeLayout(False)
        tLayoutGraphBack.PerformLayout()
        CType(DataGridJobDetails, ComponentModel.ISupportInitialize).EndInit()
        CType(MeasurementTypesBindingSource, ComponentModel.ISupportInitialize).EndInit()
        CType(EmployeesBindingSource, ComponentModel.ISupportInitialize).EndInit()
        CType(JobDetailsBindingSource, ComponentModel.ISupportInitialize).EndInit()
        CType(PictLogo, ComponentModel.ISupportInitialize).EndInit()
        tLayoutForms.ResumeLayout(False)
        tLayoutForms.PerformLayout()
        GroupChartType.ResumeLayout(False)
        GroupChartType.PerformLayout()
        tlayoutChartType.ResumeLayout(False)
        tlayoutChartType.PerformLayout()
        GroupTolerance.ResumeLayout(False)
        GroupTolerance.PerformLayout()
        tlayouttolerance.ResumeLayout(False)
        MenuStrip1.ResumeLayout(False)
        MenuStrip1.PerformLayout()
        tLayoutAllandClear.ResumeLayout(False)
        GroupBasis.ResumeLayout(False)
        GroupBasis.PerformLayout()
        tLayoutBasis.ResumeLayout(False)
        tLayoutBasis.PerformLayout()
        CType(ToleranceBindingSource, ComponentModel.ISupportInitialize).EndInit()
        CType(ClassBindingSource, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents tLayoutGraphBack As TableLayoutPanel
    Friend WithEvents RecordNavigationBar1 As RecordNavigationBar
    Friend WithEvents DataGridJobDetails As DataGridView
    Friend WithEvents PictLogo As PictureBox
    Friend WithEvents tLayoutForms As TableLayoutPanel
    Friend WithEvents GraphPanel As Panel
    Friend WithEvents CmdComparisonForm As Button
    Friend WithEvents CmdInspectForm As Button
    Friend WithEvents CmdGraphForm As Button
    Friend WithEvents CmdLocalPitchForm As Button
    Friend WithEvents CmdMeasureForm As Button
    Friend WithEvents GroupChartType As GroupBox
    Friend WithEvents LabChartType As Label
    Friend WithEvents tlayoutChartType As TableLayoutPanel
    Friend WithEvents CmdExpSectionRadio As RadioButton
    Friend WithEvents CmdSummaryRadio As RadioButton
    Friend WithEvents CmdBladesbySectorRadio As RadioButton
    Friend WithEvents CmdPositionRadio As RadioButton
    Friend WithEvents GroupTolerance As GroupBox
    Friend WithEvents ToleranceBindingSource As BindingSource
    Friend WithEvents LabTolerance As Label
    Friend WithEvents tlayouttolerance As TableLayoutPanel
    Friend WithEvents ChkAllowProgressivePitch As CheckBox
    Friend WithEvents ComboTolerance As ComboBox
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents BladesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RadiiToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents JobDetailsBindingSource As BindingSource
    Friend WithEvents EmployeesBindingSource As BindingSource
    Friend WithEvents MeasurementTypesBindingSource As BindingSource
    Friend WithEvents ClassBindingSource As BindingSource
    Friend WithEvents CmdSectorsbyBladeRadio As RadioButton
    Friend WithEvents tLayoutAllandClear As TableLayoutPanel
    Friend WithEvents CmdClearRadii As Button
    Friend WithEvents CmdClearBlades As Button
    Friend WithEvents CmdAllRadii As Button
    Friend WithEvents CmdAllBlades As Button
    Friend WithEvents StartDateDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents MeasurementTypeDataGridViewTextBoxColumn As DataGridViewComboBoxColumn
    Friend WithEvents ToleranceClassDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents PerformedByDataGridViewTextBoxColumn As DataGridViewComboBoxColumn
    Friend WithEvents DescriptionDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents GroupBasis As GroupBox
    Friend WithEvents tLayoutBasis As TableLayoutPanel
    Friend WithEvents tBoxBasis As TextBox
    Public WithEvents ComboBasis As ComboBox
End Class
