Imports LibDatabase
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmInspection
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
        Dim DataGridViewCellStyle3 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmInspection))
        tLayoutInspection = New TableLayoutPanel()
        DataGridJobDetails = New DataGridView()
        StartDateDataGridViewTextBoxColumn = New DataGridViewTextBoxColumn()
        MeasurementTypeCol = New DataGridViewComboBoxColumn()
        MeasurementTypesBindingSource = New BindingSource(components)
        TolClassCol = New DataGridViewTextBoxColumn()
        EmployeeCol = New DataGridViewComboBoxColumn()
        EmployeesBindingSource = New BindingSource(components)
        DescriptionDataGridViewTextBoxColumn = New DataGridViewTextBoxColumn()
        JobDetailsBindingSource = New BindingSource(components)
        tLayoutLocalPitchDetails = New TableLayoutPanel()
        LabLocalPitchDetails = New Label()
        ChkAxialPosition = New CheckBox()
        ChkAngularDeviation = New CheckBox()
        ChkMeanPitchPropeller = New CheckBox()
        ChkMeanPitchBlade = New CheckBox()
        CmdPrintClassCustom = New Button()
        CmdPrintClassIII = New Button()
        CmdPrintClassII = New Button()
        CmdPrintClassS = New Button()
        CmdPrintClassI = New Button()
        LabPrintPitch = New Label()
        ChkMeanPitchRadius = New CheckBox()
        ChkLocalPitch = New CheckBox()
        tLayoutLPLabels = New TableLayoutPanel()
        LabTolAPC = New Label()
        LabTolAPIII = New Label()
        LabTolAPII = New Label()
        LabTolAPI = New Label()
        LabTolAPS = New Label()
        LabTolADC = New Label()
        LabTolADIII = New Label()
        LabTolADII = New Label()
        LabTolADI = New Label()
        LabTolADS = New Label()
        LabTolMPPC = New Label()
        LabTolMPPIII = New Label()
        LabTolMPPII = New Label()
        LabTolMPPI = New Label()
        LabTolMPPS = New Label()
        LabTolMPBC = New Label()
        LabTolMPBIII = New Label()
        LabTolMPBII = New Label()
        LabTolMPBI = New Label()
        LabTolMPBS = New Label()
        LabTolMPRC = New Label()
        LabTolMPRIII = New Label()
        LabTolMPRII = New Label()
        LabTolMPRI = New Label()
        LabTolMPRS = New Label()
        LabTolLPC = New Label()
        LabTolLPII = New Label()
        LabTolLPI = New Label()
        LabTolLPS = New Label()
        TxtAngularDeviation = New TextBox()
        TxtAxialPosition = New TextBox()
        ChkMinimumsApply = New CheckBox()
        ChkAllowProgressivePitch = New CheckBox()
        ChkDisplayOnly = New CheckBox()
        ChkISO484 = New CheckBox()
        RecordNavigationBar1 = New RecordNavigationBar()
        tLayoutNavigationButtons = New TableLayoutPanel()
        CmdComparisonForm = New Button()
        CmdInspectForm = New Button()
        CmdGraphForm = New Button()
        CmdLocalPitchForm = New Button()
        CmdMeasureForm = New Button()
        PictureBoxLogo = New PictureBox()
        TableLayoutPanel1 = New TableLayoutPanel()
        labChordLengths = New Label()
        dGridChordLengths = New DataGridView()
        ClassBindingSource = New BindingSource(components)
        tLayoutInspection.SuspendLayout()
        CType(DataGridJobDetails, ComponentModel.ISupportInitialize).BeginInit()
        CType(MeasurementTypesBindingSource, ComponentModel.ISupportInitialize).BeginInit()
        CType(EmployeesBindingSource, ComponentModel.ISupportInitialize).BeginInit()
        CType(JobDetailsBindingSource, ComponentModel.ISupportInitialize).BeginInit()
        tLayoutLocalPitchDetails.SuspendLayout()
        tLayoutLPLabels.SuspendLayout()
        tLayoutNavigationButtons.SuspendLayout()
        CType(PictureBoxLogo, ComponentModel.ISupportInitialize).BeginInit()
        TableLayoutPanel1.SuspendLayout()
        CType(dGridChordLengths, ComponentModel.ISupportInitialize).BeginInit()
        CType(ClassBindingSource, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' tLayoutInspection
        ' 
        tLayoutInspection.ColumnCount = 6
        tLayoutInspection.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 215F))
        tLayoutInspection.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
        tLayoutInspection.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
        tLayoutInspection.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
        tLayoutInspection.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
        tLayoutInspection.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
        tLayoutInspection.Controls.Add(DataGridJobDetails, 3, 1)
        tLayoutInspection.Controls.Add(tLayoutLocalPitchDetails, 0, 2)
        tLayoutInspection.Controls.Add(RecordNavigationBar1, 2, 0)
        tLayoutInspection.Controls.Add(tLayoutNavigationButtons, 1, 1)
        tLayoutInspection.Controls.Add(PictureBoxLogo, 0, 0)
        tLayoutInspection.Controls.Add(TableLayoutPanel1, 0, 5)
        tLayoutInspection.Dock = DockStyle.Fill
        tLayoutInspection.Location = New Point(0, 0)
        tLayoutInspection.Name = "tLayoutInspection"
        tLayoutInspection.RowCount = 7
        tLayoutInspection.RowStyles.Add(New RowStyle(SizeType.Absolute, 39F))
        tLayoutInspection.RowStyles.Add(New RowStyle(SizeType.Absolute, 85F))
        tLayoutInspection.RowStyles.Add(New RowStyle(SizeType.Percent, 20F))
        tLayoutInspection.RowStyles.Add(New RowStyle(SizeType.Percent, 20F))
        tLayoutInspection.RowStyles.Add(New RowStyle(SizeType.Percent, 20F))
        tLayoutInspection.RowStyles.Add(New RowStyle(SizeType.Percent, 20F))
        tLayoutInspection.RowStyles.Add(New RowStyle(SizeType.Percent, 20F))
        tLayoutInspection.Size = New Size(1184, 702)
        tLayoutInspection.TabIndex = 0
        ' 
        ' DataGridJobDetails
        ' 
        DataGridJobDetails.AllowUserToAddRows = False
        DataGridJobDetails.AllowUserToDeleteRows = False
        DataGridJobDetails.AutoGenerateColumns = False
        DataGridJobDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader
        DataGridJobDetails.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        DataGridJobDetails.BorderStyle = BorderStyle.Fixed3D
        DataGridJobDetails.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridJobDetails.Columns.AddRange(New DataGridViewColumn() {StartDateDataGridViewTextBoxColumn, MeasurementTypeCol, TolClassCol, EmployeeCol, DescriptionDataGridViewTextBoxColumn})
        tLayoutInspection.SetColumnSpan(DataGridJobDetails, 3)
        DataGridJobDetails.DataSource = JobDetailsBindingSource
        DataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.BackColor = SystemColors.Window
        DataGridViewCellStyle3.Font = New Font("Segoe UI", 13F)
        DataGridViewCellStyle3.ForeColor = SystemColors.ButtonFace
        DataGridViewCellStyle3.NullValue = Nothing
        DataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = SystemColors.ControlText
        DataGridViewCellStyle3.WrapMode = DataGridViewTriState.False
        DataGridJobDetails.DefaultCellStyle = DataGridViewCellStyle3
        DataGridJobDetails.Dock = DockStyle.Fill
        DataGridJobDetails.Location = New Point(605, 43)
        DataGridJobDetails.Margin = New Padding(4, 4, 25, 0)
        DataGridJobDetails.Name = "DataGridJobDetails"
        DataGridJobDetails.RowHeadersVisible = False
        DataGridViewCellStyle4.Font = New Font("Segoe UI", 13F)
        DataGridViewCellStyle4.ForeColor = Color.Black
        DataGridJobDetails.RowsDefaultCellStyle = DataGridViewCellStyle4
        DataGridJobDetails.ScrollBars = ScrollBars.None
        DataGridJobDetails.Size = New Size(554, 81)
        DataGridJobDetails.TabIndex = 33
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
        ' MeasurementTypeCol
        ' 
        MeasurementTypeCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        MeasurementTypeCol.DataPropertyName = "MeasurementTypeId"
        MeasurementTypeCol.DataSource = MeasurementTypesBindingSource
        MeasurementTypeCol.DisplayMember = "MeasurementType1"
        MeasurementTypeCol.HeaderText = "Stage"
        MeasurementTypeCol.Name = "MeasurementTypeCol"
        MeasurementTypeCol.ValueMember = "Id"
        MeasurementTypeCol.Width = 62
        ' 
        ' MeasurementTypesBindingSource
        ' 
        MeasurementTypesBindingSource.DataSource = GetType(Models.MeasurementType)
        ' 
        ' TolClassCol
        ' 
        TolClassCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        TolClassCol.DataPropertyName = "ToleranceClass"
        TolClassCol.HeaderText = "Class"
        TolClassCol.Name = "TolClassCol"
        TolClassCol.ReadOnly = True
        TolClassCol.Resizable = DataGridViewTriState.True
        TolClassCol.SortMode = DataGridViewColumnSortMode.NotSortable
        TolClassCol.Width = 58
        ' 
        ' EmployeeCol
        ' 
        EmployeeCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        EmployeeCol.DataPropertyName = "PerformedBy"
        EmployeeCol.DataSource = EmployeesBindingSource
        EmployeeCol.DisplayMember = "EmployeeName"
        EmployeeCol.HeaderText = "Employee"
        EmployeeCol.Name = "EmployeeCol"
        EmployeeCol.ValueMember = "Id"
        EmployeeCol.Width = 96
        ' 
        ' EmployeesBindingSource
        ' 
        EmployeesBindingSource.DataSource = GetType(Models.Employee)
        EmployeesBindingSource.Sort = ""
        ' 
        ' DescriptionDataGridViewTextBoxColumn
        ' 
        DescriptionDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        DescriptionDataGridViewTextBoxColumn.DataPropertyName = "Description"
        DescriptionDataGridViewTextBoxColumn.HeaderText = "Description"
        DescriptionDataGridViewTextBoxColumn.Name = "DescriptionDataGridViewTextBoxColumn"
        ' 
        ' JobDetailsBindingSource
        ' 
        JobDetailsBindingSource.DataSource = GetType(Models.JobDetail)
        ' 
        ' tLayoutLocalPitchDetails
        ' 
        tLayoutLocalPitchDetails.ColumnCount = 7
        tLayoutInspection.SetColumnSpan(tLayoutLocalPitchDetails, 4)
        tLayoutLocalPitchDetails.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 14.2857113F))
        tLayoutLocalPitchDetails.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 14.2857161F))
        tLayoutLocalPitchDetails.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 14.2857161F))
        tLayoutLocalPitchDetails.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 14.2857161F))
        tLayoutLocalPitchDetails.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 14.2857161F))
        tLayoutLocalPitchDetails.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 14.2857161F))
        tLayoutLocalPitchDetails.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 14.2857161F))
        tLayoutLocalPitchDetails.Controls.Add(LabLocalPitchDetails, 0, 0)
        tLayoutLocalPitchDetails.Controls.Add(ChkAxialPosition, 0, 7)
        tLayoutLocalPitchDetails.Controls.Add(ChkAngularDeviation, 0, 6)
        tLayoutLocalPitchDetails.Controls.Add(ChkMeanPitchPropeller, 0, 5)
        tLayoutLocalPitchDetails.Controls.Add(ChkMeanPitchBlade, 0, 4)
        tLayoutLocalPitchDetails.Controls.Add(CmdPrintClassCustom, 5, 5)
        tLayoutLocalPitchDetails.Controls.Add(CmdPrintClassIII, 6, 4)
        tLayoutLocalPitchDetails.Controls.Add(CmdPrintClassII, 5, 4)
        tLayoutLocalPitchDetails.Controls.Add(CmdPrintClassS, 5, 3)
        tLayoutLocalPitchDetails.Controls.Add(CmdPrintClassI, 6, 3)
        tLayoutLocalPitchDetails.Controls.Add(LabPrintPitch, 5, 2)
        tLayoutLocalPitchDetails.Controls.Add(ChkMeanPitchRadius, 0, 3)
        tLayoutLocalPitchDetails.Controls.Add(ChkLocalPitch, 0, 2)
        tLayoutLocalPitchDetails.Controls.Add(tLayoutLPLabels, 3, 2)
        tLayoutLocalPitchDetails.Controls.Add(TxtAngularDeviation, 5, 6)
        tLayoutLocalPitchDetails.Controls.Add(TxtAxialPosition, 5, 7)
        tLayoutLocalPitchDetails.Controls.Add(ChkMinimumsApply, 3, 1)
        tLayoutLocalPitchDetails.Controls.Add(ChkAllowProgressivePitch, 1, 1)
        tLayoutLocalPitchDetails.Controls.Add(ChkDisplayOnly, 5, 1)
        tLayoutLocalPitchDetails.Controls.Add(ChkISO484, 0, 1)
        tLayoutLocalPitchDetails.Dock = DockStyle.Fill
        tLayoutLocalPitchDetails.Font = New Font("Segoe UI", 14F)
        tLayoutLocalPitchDetails.Location = New Point(0, 124)
        tLayoutLocalPitchDetails.Margin = New Padding(0)
        tLayoutLocalPitchDetails.Name = "tLayoutLocalPitchDetails"
        tLayoutLocalPitchDetails.RowCount = 8
        tLayoutInspection.SetRowSpan(tLayoutLocalPitchDetails, 3)
        tLayoutLocalPitchDetails.RowStyles.Add(New RowStyle(SizeType.Absolute, 20F))
        tLayoutLocalPitchDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 11.1111107F))
        tLayoutLocalPitchDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 11.1111107F))
        tLayoutLocalPitchDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 11.1111107F))
        tLayoutLocalPitchDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 11.1111107F))
        tLayoutLocalPitchDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 11.1111107F))
        tLayoutLocalPitchDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 22.2222214F))
        tLayoutLocalPitchDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 22.2222214F))
        tLayoutLocalPitchDetails.RowStyles.Add(New RowStyle(SizeType.Absolute, 20F))
        tLayoutLocalPitchDetails.Size = New Size(794, 345)
        tLayoutLocalPitchDetails.TabIndex = 30
        ' 
        ' LabLocalPitchDetails
        ' 
        LabLocalPitchDetails.BackColor = SystemColors.ActiveCaption
        tLayoutLocalPitchDetails.SetColumnSpan(LabLocalPitchDetails, 7)
        LabLocalPitchDetails.Dock = DockStyle.Fill
        LabLocalPitchDetails.Font = New Font("Segoe UI", 11F, FontStyle.Bold)
        LabLocalPitchDetails.ForeColor = SystemColors.ActiveCaptionText
        LabLocalPitchDetails.Location = New Point(0, 0)
        LabLocalPitchDetails.Margin = New Padding(0)
        LabLocalPitchDetails.Name = "LabLocalPitchDetails"
        LabLocalPitchDetails.Size = New Size(794, 20)
        LabLocalPitchDetails.TabIndex = 18
        LabLocalPitchDetails.Text = "ISO 484/Custom Tolerances"
        ' 
        ' ChkAxialPosition
        ' 
        ChkAxialPosition.AutoSize = True
        ChkAxialPosition.Checked = True
        ChkAxialPosition.CheckState = CheckState.Checked
        tLayoutLocalPitchDetails.SetColumnSpan(ChkAxialPosition, 3)
        ChkAxialPosition.Dock = DockStyle.Fill
        ChkAxialPosition.Location = New Point(12, 276)
        ChkAxialPosition.Margin = New Padding(12, 4, 4, 4)
        ChkAxialPosition.Name = "ChkAxialPosition"
        ChkAxialPosition.Size = New Size(323, 65)
        ChkAxialPosition.TabIndex = 14
        ChkAxialPosition.Text = "Relative Axial Position of consecutive blades"
        ChkAxialPosition.UseVisualStyleBackColor = True
        ' 
        ' ChkAngularDeviation
        ' 
        ChkAngularDeviation.AutoSize = True
        ChkAngularDeviation.Checked = True
        ChkAngularDeviation.CheckState = CheckState.Checked
        tLayoutLocalPitchDetails.SetColumnSpan(ChkAngularDeviation, 3)
        ChkAngularDeviation.Dock = DockStyle.Fill
        ChkAngularDeviation.Location = New Point(12, 204)
        ChkAngularDeviation.Margin = New Padding(12, 4, 4, 4)
        ChkAngularDeviation.Name = "ChkAngularDeviation"
        ChkAngularDeviation.Size = New Size(323, 64)
        ChkAngularDeviation.TabIndex = 13
        ChkAngularDeviation.Text = "Angular Deviation between consecutive blades"
        ChkAngularDeviation.UseVisualStyleBackColor = True
        ' 
        ' ChkMeanPitchPropeller
        ' 
        ChkMeanPitchPropeller.AutoSize = True
        ChkMeanPitchPropeller.Checked = True
        ChkMeanPitchPropeller.CheckState = CheckState.Checked
        tLayoutLocalPitchDetails.SetColumnSpan(ChkMeanPitchPropeller, 3)
        ChkMeanPitchPropeller.Dock = DockStyle.Fill
        ChkMeanPitchPropeller.Location = New Point(12, 168)
        ChkMeanPitchPropeller.Margin = New Padding(12, 4, 4, 4)
        ChkMeanPitchPropeller.Name = "ChkMeanPitchPropeller"
        ChkMeanPitchPropeller.Size = New Size(323, 28)
        ChkMeanPitchPropeller.TabIndex = 12
        ChkMeanPitchPropeller.Text = "Mean Pitch of Propeller"
        ChkMeanPitchPropeller.UseVisualStyleBackColor = True
        ' 
        ' ChkMeanPitchBlade
        ' 
        ChkMeanPitchBlade.AutoSize = True
        ChkMeanPitchBlade.Checked = True
        ChkMeanPitchBlade.CheckState = CheckState.Checked
        tLayoutLocalPitchDetails.SetColumnSpan(ChkMeanPitchBlade, 3)
        ChkMeanPitchBlade.Dock = DockStyle.Fill
        ChkMeanPitchBlade.Location = New Point(12, 132)
        ChkMeanPitchBlade.Margin = New Padding(12, 4, 4, 4)
        ChkMeanPitchBlade.Name = "ChkMeanPitchBlade"
        ChkMeanPitchBlade.Size = New Size(323, 28)
        ChkMeanPitchBlade.TabIndex = 11
        ChkMeanPitchBlade.Text = "Mean Pitch of Blades"
        ChkMeanPitchBlade.UseVisualStyleBackColor = True
        ' 
        ' CmdPrintClassCustom
        ' 
        CmdPrintClassCustom.Dock = DockStyle.Fill
        CmdPrintClassCustom.ForeColor = SystemColors.ActiveCaptionText
        CmdPrintClassCustom.Location = New Point(590, 165)
        CmdPrintClassCustom.Margin = New Padding(25, 1, 25, 1)
        CmdPrintClassCustom.Name = "CmdPrintClassCustom"
        CmdPrintClassCustom.Size = New Size(63, 34)
        CmdPrintClassCustom.TabIndex = 5
        CmdPrintClassCustom.Text = "Cust"
        CmdPrintClassCustom.UseVisualStyleBackColor = True
        ' 
        ' CmdPrintClassIII
        ' 
        CmdPrintClassIII.Dock = DockStyle.Fill
        CmdPrintClassIII.ForeColor = SystemColors.ActiveCaptionText
        CmdPrintClassIII.Location = New Point(703, 129)
        CmdPrintClassIII.Margin = New Padding(25, 1, 25, 1)
        CmdPrintClassIII.Name = "CmdPrintClassIII"
        CmdPrintClassIII.Size = New Size(66, 34)
        CmdPrintClassIII.TabIndex = 4
        CmdPrintClassIII.Text = "III"
        CmdPrintClassIII.UseVisualStyleBackColor = True
        ' 
        ' CmdPrintClassII
        ' 
        CmdPrintClassII.Dock = DockStyle.Fill
        CmdPrintClassII.ForeColor = SystemColors.ActiveCaptionText
        CmdPrintClassII.Location = New Point(590, 129)
        CmdPrintClassII.Margin = New Padding(25, 1, 25, 1)
        CmdPrintClassII.Name = "CmdPrintClassII"
        CmdPrintClassII.Size = New Size(63, 34)
        CmdPrintClassII.TabIndex = 3
        CmdPrintClassII.Text = "II"
        CmdPrintClassII.UseVisualStyleBackColor = True
        ' 
        ' CmdPrintClassS
        ' 
        CmdPrintClassS.Dock = DockStyle.Fill
        CmdPrintClassS.ForeColor = SystemColors.ActiveCaptionText
        CmdPrintClassS.Location = New Point(590, 93)
        CmdPrintClassS.Margin = New Padding(25, 1, 25, 1)
        CmdPrintClassS.Name = "CmdPrintClassS"
        CmdPrintClassS.Size = New Size(63, 34)
        CmdPrintClassS.TabIndex = 1
        CmdPrintClassS.Text = "S"
        CmdPrintClassS.UseVisualStyleBackColor = True
        ' 
        ' CmdPrintClassI
        ' 
        CmdPrintClassI.Dock = DockStyle.Fill
        CmdPrintClassI.ForeColor = SystemColors.ActiveCaptionText
        CmdPrintClassI.Location = New Point(703, 93)
        CmdPrintClassI.Margin = New Padding(25, 1, 25, 1)
        CmdPrintClassI.Name = "CmdPrintClassI"
        CmdPrintClassI.Size = New Size(66, 34)
        CmdPrintClassI.TabIndex = 2
        CmdPrintClassI.Text = "I"
        CmdPrintClassI.UseVisualStyleBackColor = True
        ' 
        ' LabPrintPitch
        ' 
        LabPrintPitch.AutoSize = True
        tLayoutLocalPitchDetails.SetColumnSpan(LabPrintPitch, 2)
        LabPrintPitch.Dock = DockStyle.Fill
        LabPrintPitch.Location = New Point(569, 56)
        LabPrintPitch.Margin = New Padding(4, 0, 4, 0)
        LabPrintPitch.Name = "LabPrintPitch"
        LabPrintPitch.Size = New Size(221, 36)
        LabPrintPitch.TabIndex = 0
        LabPrintPitch.Text = "Print Local Pitch Details"
        LabPrintPitch.TextAlign = ContentAlignment.BottomCenter
        ' 
        ' ChkMeanPitchRadius
        ' 
        ChkMeanPitchRadius.AutoSize = True
        ChkMeanPitchRadius.Checked = True
        ChkMeanPitchRadius.CheckState = CheckState.Checked
        tLayoutLocalPitchDetails.SetColumnSpan(ChkMeanPitchRadius, 3)
        ChkMeanPitchRadius.Dock = DockStyle.Fill
        ChkMeanPitchRadius.Location = New Point(12, 96)
        ChkMeanPitchRadius.Margin = New Padding(12, 4, 4, 4)
        ChkMeanPitchRadius.Name = "ChkMeanPitchRadius"
        ChkMeanPitchRadius.Size = New Size(323, 28)
        ChkMeanPitchRadius.TabIndex = 10
        ChkMeanPitchRadius.Text = "Mean Pitch of Radius"
        ChkMeanPitchRadius.UseVisualStyleBackColor = True
        ' 
        ' ChkLocalPitch
        ' 
        ChkLocalPitch.AutoSize = True
        ChkLocalPitch.Checked = True
        ChkLocalPitch.CheckState = CheckState.Checked
        tLayoutLocalPitchDetails.SetColumnSpan(ChkLocalPitch, 3)
        ChkLocalPitch.Dock = DockStyle.Fill
        ChkLocalPitch.Location = New Point(12, 60)
        ChkLocalPitch.Margin = New Padding(12, 4, 4, 4)
        ChkLocalPitch.Name = "ChkLocalPitch"
        ChkLocalPitch.Size = New Size(323, 28)
        ChkLocalPitch.TabIndex = 9
        ChkLocalPitch.Text = "Local Pitch"
        ChkLocalPitch.UseVisualStyleBackColor = True
        ' 
        ' tLayoutLPLabels
        ' 
        tLayoutLPLabels.ColumnCount = 5
        tLayoutLocalPitchDetails.SetColumnSpan(tLayoutLPLabels, 2)
        tLayoutLPLabels.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
        tLayoutLPLabels.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
        tLayoutLPLabels.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
        tLayoutLPLabels.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
        tLayoutLPLabels.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
        tLayoutLPLabels.Controls.Add(LabTolAPC, 4, 5)
        tLayoutLPLabels.Controls.Add(LabTolAPIII, 3, 5)
        tLayoutLPLabels.Controls.Add(LabTolAPII, 2, 5)
        tLayoutLPLabels.Controls.Add(LabTolAPI, 1, 5)
        tLayoutLPLabels.Controls.Add(LabTolAPS, 0, 5)
        tLayoutLPLabels.Controls.Add(LabTolADC, 4, 4)
        tLayoutLPLabels.Controls.Add(LabTolADIII, 3, 4)
        tLayoutLPLabels.Controls.Add(LabTolADII, 2, 4)
        tLayoutLPLabels.Controls.Add(LabTolADI, 1, 4)
        tLayoutLPLabels.Controls.Add(LabTolADS, 0, 4)
        tLayoutLPLabels.Controls.Add(LabTolMPPC, 4, 3)
        tLayoutLPLabels.Controls.Add(LabTolMPPIII, 3, 3)
        tLayoutLPLabels.Controls.Add(LabTolMPPII, 2, 3)
        tLayoutLPLabels.Controls.Add(LabTolMPPI, 1, 3)
        tLayoutLPLabels.Controls.Add(LabTolMPPS, 0, 3)
        tLayoutLPLabels.Controls.Add(LabTolMPBC, 4, 2)
        tLayoutLPLabels.Controls.Add(LabTolMPBIII, 3, 2)
        tLayoutLPLabels.Controls.Add(LabTolMPBII, 2, 2)
        tLayoutLPLabels.Controls.Add(LabTolMPBI, 1, 2)
        tLayoutLPLabels.Controls.Add(LabTolMPBS, 0, 2)
        tLayoutLPLabels.Controls.Add(LabTolMPRC, 4, 1)
        tLayoutLPLabels.Controls.Add(LabTolMPRIII, 3, 1)
        tLayoutLPLabels.Controls.Add(LabTolMPRII, 2, 1)
        tLayoutLPLabels.Controls.Add(LabTolMPRI, 1, 1)
        tLayoutLPLabels.Controls.Add(LabTolMPRS, 0, 1)
        tLayoutLPLabels.Controls.Add(LabTolLPC, 4, 0)
        tLayoutLPLabels.Controls.Add(LabTolLPII, 2, 0)
        tLayoutLPLabels.Controls.Add(LabTolLPI, 1, 0)
        tLayoutLPLabels.Controls.Add(LabTolLPS, 0, 0)
        tLayoutLPLabels.Dock = DockStyle.Fill
        tLayoutLPLabels.Font = New Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        tLayoutLPLabels.Location = New Point(339, 56)
        tLayoutLPLabels.Margin = New Padding(0)
        tLayoutLPLabels.Name = "tLayoutLPLabels"
        tLayoutLPLabels.RowCount = 6
        tLayoutLocalPitchDetails.SetRowSpan(tLayoutLPLabels, 6)
        tLayoutLPLabels.RowStyles.Add(New RowStyle(SizeType.Percent, 14.2857141F))
        tLayoutLPLabels.RowStyles.Add(New RowStyle(SizeType.Percent, 14.2857141F))
        tLayoutLPLabels.RowStyles.Add(New RowStyle(SizeType.Percent, 14.2857141F))
        tLayoutLPLabels.RowStyles.Add(New RowStyle(SizeType.Percent, 14.2857141F))
        tLayoutLPLabels.RowStyles.Add(New RowStyle(SizeType.Percent, 21.4285717F))
        tLayoutLPLabels.RowStyles.Add(New RowStyle(SizeType.Percent, 21.4285717F))
        tLayoutLPLabels.Size = New Size(226, 289)
        tLayoutLPLabels.TabIndex = 15
        ' 
        ' LabTolAPC
        ' 
        LabTolAPC.AutoSize = True
        LabTolAPC.Dock = DockStyle.Fill
        LabTolAPC.Location = New Point(184, 225)
        LabTolAPC.Margin = New Padding(4, 0, 4, 0)
        LabTolAPC.Name = "LabTolAPC"
        LabTolAPC.Size = New Size(38, 64)
        LabTolAPC.TabIndex = 29
        LabTolAPC.Text = "C"
        LabTolAPC.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabTolAPIII
        ' 
        LabTolAPIII.AutoSize = True
        LabTolAPIII.Dock = DockStyle.Fill
        LabTolAPIII.Location = New Point(139, 225)
        LabTolAPIII.Margin = New Padding(4, 0, 4, 0)
        LabTolAPIII.Name = "LabTolAPIII"
        LabTolAPIII.Size = New Size(37, 64)
        LabTolAPIII.TabIndex = 28
        LabTolAPIII.Text = "III"
        LabTolAPIII.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabTolAPII
        ' 
        LabTolAPII.AutoSize = True
        LabTolAPII.Dock = DockStyle.Fill
        LabTolAPII.Location = New Point(94, 225)
        LabTolAPII.Margin = New Padding(4, 0, 4, 0)
        LabTolAPII.Name = "LabTolAPII"
        LabTolAPII.Size = New Size(37, 64)
        LabTolAPII.TabIndex = 27
        LabTolAPII.Text = "II"
        LabTolAPII.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabTolAPI
        ' 
        LabTolAPI.AutoSize = True
        LabTolAPI.Dock = DockStyle.Fill
        LabTolAPI.Location = New Point(49, 225)
        LabTolAPI.Margin = New Padding(4, 0, 4, 0)
        LabTolAPI.Name = "LabTolAPI"
        LabTolAPI.Size = New Size(37, 64)
        LabTolAPI.TabIndex = 26
        LabTolAPI.Text = "I"
        LabTolAPI.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabTolAPS
        ' 
        LabTolAPS.AutoSize = True
        LabTolAPS.Dock = DockStyle.Fill
        LabTolAPS.Location = New Point(4, 225)
        LabTolAPS.Margin = New Padding(4, 0, 4, 0)
        LabTolAPS.Name = "LabTolAPS"
        LabTolAPS.Size = New Size(37, 64)
        LabTolAPS.TabIndex = 25
        LabTolAPS.Text = "S"
        LabTolAPS.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabTolADC
        ' 
        LabTolADC.AutoSize = True
        LabTolADC.Dock = DockStyle.Fill
        LabTolADC.Location = New Point(184, 164)
        LabTolADC.Margin = New Padding(4, 0, 4, 0)
        LabTolADC.Name = "LabTolADC"
        LabTolADC.Size = New Size(38, 61)
        LabTolADC.TabIndex = 24
        LabTolADC.Text = "C"
        LabTolADC.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabTolADIII
        ' 
        LabTolADIII.AutoSize = True
        LabTolADIII.Dock = DockStyle.Fill
        LabTolADIII.Location = New Point(139, 164)
        LabTolADIII.Margin = New Padding(4, 0, 4, 0)
        LabTolADIII.Name = "LabTolADIII"
        LabTolADIII.Size = New Size(37, 61)
        LabTolADIII.TabIndex = 23
        LabTolADIII.Text = "III"
        LabTolADIII.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabTolADII
        ' 
        LabTolADII.AutoSize = True
        LabTolADII.Dock = DockStyle.Fill
        LabTolADII.Location = New Point(94, 164)
        LabTolADII.Margin = New Padding(4, 0, 4, 0)
        LabTolADII.Name = "LabTolADII"
        LabTolADII.Size = New Size(37, 61)
        LabTolADII.TabIndex = 22
        LabTolADII.Text = "II"
        LabTolADII.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabTolADI
        ' 
        LabTolADI.AutoSize = True
        LabTolADI.Dock = DockStyle.Fill
        LabTolADI.Location = New Point(49, 164)
        LabTolADI.Margin = New Padding(4, 0, 4, 0)
        LabTolADI.Name = "LabTolADI"
        LabTolADI.Size = New Size(37, 61)
        LabTolADI.TabIndex = 21
        LabTolADI.Text = "I"
        LabTolADI.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabTolADS
        ' 
        LabTolADS.AutoSize = True
        LabTolADS.Dock = DockStyle.Fill
        LabTolADS.Location = New Point(4, 164)
        LabTolADS.Margin = New Padding(4, 0, 4, 0)
        LabTolADS.Name = "LabTolADS"
        LabTolADS.Size = New Size(37, 61)
        LabTolADS.TabIndex = 20
        LabTolADS.Text = "S"
        LabTolADS.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabTolMPPC
        ' 
        LabTolMPPC.AutoSize = True
        LabTolMPPC.Dock = DockStyle.Fill
        LabTolMPPC.Location = New Point(184, 123)
        LabTolMPPC.Margin = New Padding(4, 0, 4, 0)
        LabTolMPPC.Name = "LabTolMPPC"
        LabTolMPPC.Size = New Size(38, 41)
        LabTolMPPC.TabIndex = 19
        LabTolMPPC.Text = "C"
        LabTolMPPC.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabTolMPPIII
        ' 
        LabTolMPPIII.AutoSize = True
        LabTolMPPIII.Dock = DockStyle.Fill
        LabTolMPPIII.Location = New Point(139, 123)
        LabTolMPPIII.Margin = New Padding(4, 0, 4, 0)
        LabTolMPPIII.Name = "LabTolMPPIII"
        LabTolMPPIII.Size = New Size(37, 41)
        LabTolMPPIII.TabIndex = 18
        LabTolMPPIII.Text = "III"
        LabTolMPPIII.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabTolMPPII
        ' 
        LabTolMPPII.AutoSize = True
        LabTolMPPII.Dock = DockStyle.Fill
        LabTolMPPII.Location = New Point(94, 123)
        LabTolMPPII.Margin = New Padding(4, 0, 4, 0)
        LabTolMPPII.Name = "LabTolMPPII"
        LabTolMPPII.Size = New Size(37, 41)
        LabTolMPPII.TabIndex = 17
        LabTolMPPII.Text = "II"
        LabTolMPPII.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabTolMPPI
        ' 
        LabTolMPPI.AutoSize = True
        LabTolMPPI.Dock = DockStyle.Fill
        LabTolMPPI.Location = New Point(49, 123)
        LabTolMPPI.Margin = New Padding(4, 0, 4, 0)
        LabTolMPPI.Name = "LabTolMPPI"
        LabTolMPPI.Size = New Size(37, 41)
        LabTolMPPI.TabIndex = 16
        LabTolMPPI.Text = "I"
        LabTolMPPI.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabTolMPPS
        ' 
        LabTolMPPS.AutoSize = True
        LabTolMPPS.Dock = DockStyle.Fill
        LabTolMPPS.Location = New Point(4, 123)
        LabTolMPPS.Margin = New Padding(4, 0, 4, 0)
        LabTolMPPS.Name = "LabTolMPPS"
        LabTolMPPS.Size = New Size(37, 41)
        LabTolMPPS.TabIndex = 15
        LabTolMPPS.Text = "S"
        LabTolMPPS.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabTolMPBC
        ' 
        LabTolMPBC.AutoSize = True
        LabTolMPBC.Dock = DockStyle.Fill
        LabTolMPBC.Location = New Point(184, 82)
        LabTolMPBC.Margin = New Padding(4, 0, 4, 0)
        LabTolMPBC.Name = "LabTolMPBC"
        LabTolMPBC.Size = New Size(38, 41)
        LabTolMPBC.TabIndex = 14
        LabTolMPBC.Text = "C"
        LabTolMPBC.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabTolMPBIII
        ' 
        LabTolMPBIII.AutoSize = True
        LabTolMPBIII.Dock = DockStyle.Fill
        LabTolMPBIII.Location = New Point(139, 82)
        LabTolMPBIII.Margin = New Padding(4, 0, 4, 0)
        LabTolMPBIII.Name = "LabTolMPBIII"
        LabTolMPBIII.Size = New Size(37, 41)
        LabTolMPBIII.TabIndex = 13
        LabTolMPBIII.Text = "III"
        LabTolMPBIII.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabTolMPBII
        ' 
        LabTolMPBII.AutoSize = True
        LabTolMPBII.Dock = DockStyle.Fill
        LabTolMPBII.Location = New Point(94, 82)
        LabTolMPBII.Margin = New Padding(4, 0, 4, 0)
        LabTolMPBII.Name = "LabTolMPBII"
        LabTolMPBII.Size = New Size(37, 41)
        LabTolMPBII.TabIndex = 12
        LabTolMPBII.Text = "II"
        LabTolMPBII.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabTolMPBI
        ' 
        LabTolMPBI.AutoSize = True
        LabTolMPBI.Dock = DockStyle.Fill
        LabTolMPBI.Location = New Point(49, 82)
        LabTolMPBI.Margin = New Padding(4, 0, 4, 0)
        LabTolMPBI.Name = "LabTolMPBI"
        LabTolMPBI.Size = New Size(37, 41)
        LabTolMPBI.TabIndex = 11
        LabTolMPBI.Text = "I"
        LabTolMPBI.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabTolMPBS
        ' 
        LabTolMPBS.AutoSize = True
        LabTolMPBS.Dock = DockStyle.Fill
        LabTolMPBS.Location = New Point(4, 82)
        LabTolMPBS.Margin = New Padding(4, 0, 4, 0)
        LabTolMPBS.Name = "LabTolMPBS"
        LabTolMPBS.Size = New Size(37, 41)
        LabTolMPBS.TabIndex = 10
        LabTolMPBS.Text = "S"
        LabTolMPBS.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabTolMPRC
        ' 
        LabTolMPRC.AutoSize = True
        LabTolMPRC.Dock = DockStyle.Fill
        LabTolMPRC.Location = New Point(184, 41)
        LabTolMPRC.Margin = New Padding(4, 0, 4, 0)
        LabTolMPRC.Name = "LabTolMPRC"
        LabTolMPRC.Size = New Size(38, 41)
        LabTolMPRC.TabIndex = 9
        LabTolMPRC.Text = "C"
        LabTolMPRC.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabTolMPRIII
        ' 
        LabTolMPRIII.AutoSize = True
        LabTolMPRIII.Dock = DockStyle.Fill
        LabTolMPRIII.Location = New Point(139, 41)
        LabTolMPRIII.Margin = New Padding(4, 0, 4, 0)
        LabTolMPRIII.Name = "LabTolMPRIII"
        LabTolMPRIII.Size = New Size(37, 41)
        LabTolMPRIII.TabIndex = 8
        LabTolMPRIII.Text = "III"
        LabTolMPRIII.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabTolMPRII
        ' 
        LabTolMPRII.AutoSize = True
        LabTolMPRII.Dock = DockStyle.Fill
        LabTolMPRII.Location = New Point(94, 41)
        LabTolMPRII.Margin = New Padding(4, 0, 4, 0)
        LabTolMPRII.Name = "LabTolMPRII"
        LabTolMPRII.Size = New Size(37, 41)
        LabTolMPRII.TabIndex = 7
        LabTolMPRII.Text = "II"
        LabTolMPRII.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabTolMPRI
        ' 
        LabTolMPRI.AutoSize = True
        LabTolMPRI.Dock = DockStyle.Fill
        LabTolMPRI.Location = New Point(49, 41)
        LabTolMPRI.Margin = New Padding(4, 0, 4, 0)
        LabTolMPRI.Name = "LabTolMPRI"
        LabTolMPRI.Size = New Size(37, 41)
        LabTolMPRI.TabIndex = 6
        LabTolMPRI.Text = "I"
        LabTolMPRI.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabTolMPRS
        ' 
        LabTolMPRS.AutoSize = True
        LabTolMPRS.Dock = DockStyle.Fill
        LabTolMPRS.Location = New Point(4, 41)
        LabTolMPRS.Margin = New Padding(4, 0, 4, 0)
        LabTolMPRS.Name = "LabTolMPRS"
        LabTolMPRS.Size = New Size(37, 41)
        LabTolMPRS.TabIndex = 5
        LabTolMPRS.Text = "S"
        LabTolMPRS.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabTolLPC
        ' 
        LabTolLPC.AutoSize = True
        LabTolLPC.Dock = DockStyle.Fill
        LabTolLPC.Location = New Point(184, 0)
        LabTolLPC.Margin = New Padding(4, 0, 4, 0)
        LabTolLPC.Name = "LabTolLPC"
        LabTolLPC.Size = New Size(38, 41)
        LabTolLPC.TabIndex = 4
        LabTolLPC.Text = "C"
        LabTolLPC.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabTolLPII
        ' 
        LabTolLPII.AutoSize = True
        LabTolLPII.Dock = DockStyle.Fill
        LabTolLPII.Location = New Point(94, 0)
        LabTolLPII.Margin = New Padding(4, 0, 4, 0)
        LabTolLPII.Name = "LabTolLPII"
        LabTolLPII.Size = New Size(37, 41)
        LabTolLPII.TabIndex = 2
        LabTolLPII.Text = "II"
        LabTolLPII.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabTolLPI
        ' 
        LabTolLPI.AutoSize = True
        LabTolLPI.Dock = DockStyle.Fill
        LabTolLPI.Location = New Point(49, 0)
        LabTolLPI.Margin = New Padding(4, 0, 4, 0)
        LabTolLPI.Name = "LabTolLPI"
        LabTolLPI.Size = New Size(37, 41)
        LabTolLPI.TabIndex = 1
        LabTolLPI.Text = "I"
        LabTolLPI.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabTolLPS
        ' 
        LabTolLPS.AutoSize = True
        LabTolLPS.Dock = DockStyle.Fill
        LabTolLPS.Location = New Point(4, 0)
        LabTolLPS.Margin = New Padding(4, 0, 4, 0)
        LabTolLPS.Name = "LabTolLPS"
        LabTolLPS.Size = New Size(37, 41)
        LabTolLPS.TabIndex = 0
        LabTolLPS.Text = "S"
        LabTolLPS.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' TxtAngularDeviation
        ' 
        tLayoutLocalPitchDetails.SetColumnSpan(TxtAngularDeviation, 2)
        TxtAngularDeviation.Dock = DockStyle.Top
        TxtAngularDeviation.Location = New Point(569, 204)
        TxtAngularDeviation.Margin = New Padding(4)
        TxtAngularDeviation.Name = "TxtAngularDeviation"
        TxtAngularDeviation.Size = New Size(221, 32)
        TxtAngularDeviation.TabIndex = 16
        ' 
        ' TxtAxialPosition
        ' 
        tLayoutLocalPitchDetails.SetColumnSpan(TxtAxialPosition, 2)
        TxtAxialPosition.Dock = DockStyle.Top
        TxtAxialPosition.Location = New Point(569, 276)
        TxtAxialPosition.Margin = New Padding(4)
        TxtAxialPosition.Name = "TxtAxialPosition"
        TxtAxialPosition.Size = New Size(221, 32)
        TxtAxialPosition.TabIndex = 17
        ' 
        ' ChkMinimumsApply
        ' 
        ChkMinimumsApply.AutoSize = True
        tLayoutLocalPitchDetails.SetColumnSpan(ChkMinimumsApply, 2)
        ChkMinimumsApply.Dock = DockStyle.Fill
        ChkMinimumsApply.Location = New Point(343, 24)
        ChkMinimumsApply.Margin = New Padding(4)
        ChkMinimumsApply.Name = "ChkMinimumsApply"
        ChkMinimumsApply.Size = New Size(218, 28)
        ChkMinimumsApply.TabIndex = 7
        ChkMinimumsApply.Text = "Minimums Apply"
        ChkMinimumsApply.UseVisualStyleBackColor = True
        ' 
        ' ChkAllowProgressivePitch
        ' 
        ChkAllowProgressivePitch.AutoSize = True
        tLayoutLocalPitchDetails.SetColumnSpan(ChkAllowProgressivePitch, 2)
        ChkAllowProgressivePitch.Dock = DockStyle.Fill
        ChkAllowProgressivePitch.Location = New Point(117, 24)
        ChkAllowProgressivePitch.Margin = New Padding(4)
        ChkAllowProgressivePitch.Name = "ChkAllowProgressivePitch"
        ChkAllowProgressivePitch.Size = New Size(218, 28)
        ChkAllowProgressivePitch.TabIndex = 6
        ChkAllowProgressivePitch.Text = "Allow Progressive Pitch"
        ChkAllowProgressivePitch.UseVisualStyleBackColor = True
        ' 
        ' ChkDisplayOnly
        ' 
        ChkDisplayOnly.AutoSize = True
        tLayoutLocalPitchDetails.SetColumnSpan(ChkDisplayOnly, 2)
        ChkDisplayOnly.Dock = DockStyle.Fill
        ChkDisplayOnly.Location = New Point(569, 24)
        ChkDisplayOnly.Margin = New Padding(4)
        ChkDisplayOnly.Name = "ChkDisplayOnly"
        ChkDisplayOnly.Size = New Size(221, 28)
        ChkDisplayOnly.TabIndex = 8
        ChkDisplayOnly.Text = "Display Only"
        ChkDisplayOnly.UseVisualStyleBackColor = True
        ' 
        ' ChkISO484
        ' 
        ChkISO484.AutoSize = True
        ChkISO484.Checked = True
        ChkISO484.CheckState = CheckState.Checked
        ChkISO484.Dock = DockStyle.Fill
        ChkISO484.Location = New Point(4, 24)
        ChkISO484.Margin = New Padding(4)
        ChkISO484.Name = "ChkISO484"
        ChkISO484.Size = New Size(105, 28)
        ChkISO484.TabIndex = 19
        ChkISO484.Text = "ISO/484"
        ChkISO484.UseVisualStyleBackColor = True
        ' 
        ' RecordNavigationBar1
        ' 
        RecordNavigationBar1.AutoSize = True
        RecordNavigationBar1.AutoSizeMode = AutoSizeMode.GrowAndShrink
        RecordNavigationBar1.BoundControls = Nothing
        tLayoutInspection.SetColumnSpan(RecordNavigationBar1, 4)
        RecordNavigationBar1.Database = Nothing
        RecordNavigationBar1.Dock = DockStyle.Right
        RecordNavigationBar1.Filter = Nothing
        RecordNavigationBar1.FilterOn = False
        RecordNavigationBar1.Location = New Point(408, 8)
        RecordNavigationBar1.Margin = New Padding(0, 8, 32, 0)
        RecordNavigationBar1.MasterSource = Nothing
        RecordNavigationBar1.Name = "RecordNavigationBar1"
        RecordNavigationBar1.NoUpdates = False
        RecordNavigationBar1.ServiceProvider = Nothing
        RecordNavigationBar1.Size = New Size(744, 31)
        RecordNavigationBar1.TabIndex = 28
        ' 
        ' tLayoutNavigationButtons
        ' 
        tLayoutNavigationButtons.AutoSize = True
        tLayoutNavigationButtons.ColumnCount = 5
        tLayoutInspection.SetColumnSpan(tLayoutNavigationButtons, 2)
        tLayoutNavigationButtons.ColumnStyles.Add(New ColumnStyle())
        tLayoutNavigationButtons.ColumnStyles.Add(New ColumnStyle())
        tLayoutNavigationButtons.ColumnStyles.Add(New ColumnStyle())
        tLayoutNavigationButtons.ColumnStyles.Add(New ColumnStyle())
        tLayoutNavigationButtons.ColumnStyles.Add(New ColumnStyle())
        tLayoutNavigationButtons.Controls.Add(CmdComparisonForm, 4, 0)
        tLayoutNavigationButtons.Controls.Add(CmdInspectForm, 3, 0)
        tLayoutNavigationButtons.Controls.Add(CmdGraphForm, 2, 0)
        tLayoutNavigationButtons.Controls.Add(CmdLocalPitchForm, 1, 0)
        tLayoutNavigationButtons.Controls.Add(CmdMeasureForm, 0, 0)
        tLayoutNavigationButtons.Dock = DockStyle.Left
        tLayoutNavigationButtons.Location = New Point(215, 39)
        tLayoutNavigationButtons.Margin = New Padding(0)
        tLayoutNavigationButtons.Name = "tLayoutNavigationButtons"
        tLayoutNavigationButtons.RowCount = 1
        tLayoutNavigationButtons.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tLayoutNavigationButtons.Size = New Size(386, 85)
        tLayoutNavigationButtons.TabIndex = 27
        ' 
        ' CmdComparisonForm
        ' 
        CmdComparisonForm.AutoSize = True
        CmdComparisonForm.Dock = DockStyle.Fill
        CmdComparisonForm.ForeColor = SystemColors.ActiveCaptionText
        CmdComparisonForm.Location = New Point(371, 3)
        CmdComparisonForm.Name = "CmdComparisonForm"
        CmdComparisonForm.Size = New Size(75, 79)
        CmdComparisonForm.TabIndex = 4
        CmdComparisonForm.Text = "Comp."
        CmdComparisonForm.UseVisualStyleBackColor = True
        ' 
        ' CmdInspectForm
        ' 
        CmdInspectForm.AutoSize = True
        CmdInspectForm.Dock = DockStyle.Fill
        CmdInspectForm.ForeColor = SystemColors.ButtonFace
        CmdInspectForm.Location = New Point(286, 3)
        CmdInspectForm.Name = "CmdInspectForm"
        CmdInspectForm.Size = New Size(79, 79)
        CmdInspectForm.TabIndex = 3
        CmdInspectForm.Text = "Inspect"
        CmdInspectForm.UseVisualStyleBackColor = True
        ' 
        ' CmdGraphForm
        ' 
        CmdGraphForm.AutoSize = True
        CmdGraphForm.Dock = DockStyle.Fill
        CmdGraphForm.Location = New Point(209, 3)
        CmdGraphForm.Name = "CmdGraphForm"
        CmdGraphForm.Size = New Size(71, 79)
        CmdGraphForm.TabIndex = 2
        CmdGraphForm.Text = "Graph"
        CmdGraphForm.UseVisualStyleBackColor = True
        ' 
        ' CmdLocalPitchForm
        ' 
        CmdLocalPitchForm.AutoSize = True
        CmdLocalPitchForm.Dock = DockStyle.Fill
        CmdLocalPitchForm.ForeColor = SystemColors.ButtonFace
        CmdLocalPitchForm.Location = New Point(98, 3)
        CmdLocalPitchForm.Name = "CmdLocalPitchForm"
        CmdLocalPitchForm.Size = New Size(105, 79)
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
        CmdMeasureForm.Size = New Size(89, 79)
        CmdMeasureForm.TabIndex = 0
        CmdMeasureForm.Text = "Measure"
        CmdMeasureForm.UseVisualStyleBackColor = True
        ' 
        ' PictureBoxLogo
        ' 
        PictureBoxLogo.Dock = DockStyle.Fill
        PictureBoxLogo.Image = CType(resources.GetObject("PictureBoxLogo.Image"), Image)
        PictureBoxLogo.InitialImage = Nothing
        PictureBoxLogo.Location = New Point(0, 0)
        PictureBoxLogo.Margin = New Padding(0)
        PictureBoxLogo.Name = "PictureBoxLogo"
        tLayoutInspection.SetRowSpan(PictureBoxLogo, 2)
        PictureBoxLogo.Size = New Size(215, 124)
        PictureBoxLogo.SizeMode = PictureBoxSizeMode.Zoom
        PictureBoxLogo.TabIndex = 10
        PictureBoxLogo.TabStop = False
        ' 
        ' TableLayoutPanel1
        ' 
        TableLayoutPanel1.ColumnCount = 1
        tLayoutInspection.SetColumnSpan(TableLayoutPanel1, 7)
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        TableLayoutPanel1.Controls.Add(labChordLengths, 0, 0)
        TableLayoutPanel1.Controls.Add(dGridChordLengths, 0, 1)
        TableLayoutPanel1.Dock = DockStyle.Fill
        TableLayoutPanel1.Location = New Point(3, 472)
        TableLayoutPanel1.Name = "TableLayoutPanel1"
        TableLayoutPanel1.RowCount = 2
        tLayoutInspection.SetRowSpan(TableLayoutPanel1, 2)
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Absolute, 26F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        TableLayoutPanel1.Size = New Size(1178, 227)
        TableLayoutPanel1.TabIndex = 32
        ' 
        ' labChordLengths
        ' 
        labChordLengths.AutoSize = True
        labChordLengths.Dock = DockStyle.Fill
        labChordLengths.Location = New Point(3, 0)
        labChordLengths.Name = "labChordLengths"
        labChordLengths.Size = New Size(1172, 26)
        labChordLengths.TabIndex = 0
        labChordLengths.Text = "Measured Chord Lengths"
        ' 
        ' dGridChordLengths
        ' 
        dGridChordLengths.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        dGridChordLengths.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        dGridChordLengths.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dGridChordLengths.Dock = DockStyle.Fill
        dGridChordLengths.Location = New Point(0, 26)
        dGridChordLengths.Margin = New Padding(0)
        dGridChordLengths.Name = "dGridChordLengths"
        dGridChordLengths.Size = New Size(1178, 201)
        dGridChordLengths.TabIndex = 1
        ' 
        ' ClassBindingSource
        ' 
        ClassBindingSource.DataSource = GetType(Models.Tolerance)
        ' 
        ' FrmInspection
        ' 
        AutoScaleMode = AutoScaleMode.None
        ClientSize = New Size(1184, 702)
        Controls.Add(tLayoutInspection)
        Font = New Font("Segoe UI", 13F)
        Margin = New Padding(4)
        Name = "FrmInspection"
        Text = "FrmInspection"
        WindowState = FormWindowState.Maximized
        tLayoutInspection.ResumeLayout(False)
        tLayoutInspection.PerformLayout()
        CType(DataGridJobDetails, ComponentModel.ISupportInitialize).EndInit()
        CType(MeasurementTypesBindingSource, ComponentModel.ISupportInitialize).EndInit()
        CType(EmployeesBindingSource, ComponentModel.ISupportInitialize).EndInit()
        CType(JobDetailsBindingSource, ComponentModel.ISupportInitialize).EndInit()
        tLayoutLocalPitchDetails.ResumeLayout(False)
        tLayoutLocalPitchDetails.PerformLayout()
        tLayoutLPLabels.ResumeLayout(False)
        tLayoutLPLabels.PerformLayout()
        tLayoutNavigationButtons.ResumeLayout(False)
        tLayoutNavigationButtons.PerformLayout()
        CType(PictureBoxLogo, ComponentModel.ISupportInitialize).EndInit()
        TableLayoutPanel1.ResumeLayout(False)
        TableLayoutPanel1.PerformLayout()
        CType(dGridChordLengths, ComponentModel.ISupportInitialize).EndInit()
        CType(ClassBindingSource, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents tLayoutInspection As TableLayoutPanel
    Friend WithEvents PictureBoxLogo As PictureBox
    Friend WithEvents tLayoutNavigationButtons As TableLayoutPanel
    Friend WithEvents CmdComparisonForm As Button
    Friend WithEvents CmdInspectForm As Button
    Friend WithEvents CmdGraphForm As Button
    Friend WithEvents CmdLocalPitchForm As Button
    Friend WithEvents CmdMeasureForm As Button
    Friend WithEvents RecordNavigationBar1 As RecordNavigationBar
    Friend WithEvents JobDetailsBindingSource As BindingSource
    Friend WithEvents MeasurementTypesBindingSource As BindingSource
    Friend WithEvents EmployeesBindingSource As BindingSource
    Friend WithEvents ClassBindingSource As BindingSource
    Friend WithEvents tLayoutLocalPitchDetails As TableLayoutPanel
    Friend WithEvents LabLocalPitchDetails As Label
    Friend WithEvents LabPrintPitch As Label
    Friend WithEvents CmdPrintClassS As Button
    Friend WithEvents CmdPrintClassI As Button
    Friend WithEvents CmdPrintClassII As Button
    Friend WithEvents CmdPrintClassIII As Button
    Friend WithEvents CmdPrintClassCustom As Button
    Friend WithEvents ChkAllowProgressivePitch As CheckBox
    Friend WithEvents ChkMinimumsApply As CheckBox
    Friend WithEvents ChkDisplayOnly As CheckBox
    Friend WithEvents ChkAxialPosition As CheckBox
    Friend WithEvents ChkAngularDeviation As CheckBox
    Friend WithEvents ChkMeanPitchPropeller As CheckBox
    Friend WithEvents ChkMeanPitchBlade As CheckBox
    Friend WithEvents ChkMeanPitchRadius As CheckBox
    Friend WithEvents ChkLocalPitch As CheckBox
    Friend WithEvents tLayoutLPLabels As TableLayoutPanel
    Friend WithEvents LabTolAPC As Label
    Friend WithEvents LabTolAPIII As Label
    Friend WithEvents LabTolAPII As Label
    Friend WithEvents LabTolAPI As Label
    Friend WithEvents LabTolAPS As Label
    Friend WithEvents LabTolADC As Label
    Friend WithEvents LabTolADIII As Label
    Friend WithEvents LabTolADII As Label
    Friend WithEvents LabTolADI As Label
    Friend WithEvents LabTolADS As Label
    Friend WithEvents LabTolMPPC As Label
    Friend WithEvents LabTolMPPIII As Label
    Friend WithEvents LabTolMPPII As Label
    Friend WithEvents LabTolMPPI As Label
    Friend WithEvents LabTolMPPS As Label
    Friend WithEvents LabTolMPBC As Label
    Friend WithEvents LabTolMPBIII As Label
    Friend WithEvents LabTolMPBII As Label
    Friend WithEvents LabTolMPBI As Label
    Friend WithEvents LabTolMPBS As Label
    Friend WithEvents LabTolMPRC As Label
    Friend WithEvents LabTolMPRIII As Label
    Friend WithEvents LabTolMPRII As Label
    Friend WithEvents LabTolMPRI As Label
    Friend WithEvents LabTolMPRS As Label
    Friend WithEvents LabTolLPC As Label
    Friend WithEvents LabTolLPII As Label
    Friend WithEvents LabTolLPI As Label
    Friend WithEvents LabTolLPS As Label
    Friend WithEvents TxtAngularDeviation As TextBox
    Friend WithEvents TxtAxialPosition As TextBox
    Friend WithEvents ChkISO484 As CheckBox
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents labChordLengths As Label
    Friend WithEvents dGridChordLengths As DataGridView
    Friend WithEvents DataGridJobDetails As DataGridView
    Friend WithEvents StartDateDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents MeasurementTypeCol As DataGridViewComboBoxColumn
    Friend WithEvents TolClassCol As DataGridViewTextBoxColumn
    Friend WithEvents EmployeeCol As DataGridViewComboBoxColumn
    Friend WithEvents DescriptionDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
End Class
