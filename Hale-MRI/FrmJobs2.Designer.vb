Imports LibDatabase
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmJobs2
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmJobs2))
        Dim DataGridViewCellStyle3 As DataGridViewCellStyle = New DataGridViewCellStyle()
        MeasurementTypesBindingSource = New BindingSource(components)
        EmployeesBindingSource = New BindingSource(components)
        JobDetailsBindingSource = New BindingSource(components)
        JobsBindingSource = New BindingSource(components)
        PictureBoxLogo = New PictureBox()
        TableLayoutImport = New TableLayoutPanel()
        CmdScanDataPick = New Button()
        CmdScanDataExport = New Button()
        CmdScanDataImport = New Button()
        CustomLabelImport = New CustomLabel()
        LabScanData = New Label()
        TxtScanData = New TextBox()
        TableLayoutSearch = New TableLayoutPanel()
        CustomLabelSearch = New CustomLabel()
        LabJob = New Label()
        LabVessel = New Label()
        ComboVessels = New ComboBox()
        ComboJobs = New ComboBox()
        ComboCustomers = New ComboBox()
        LabCustomer = New Label()
        TableLayoutPropeller = New TableLayoutPanel()
        CustomLabelPropeller = New CustomLabel()
        TxtDAR = New TextBox()
        TxtStampNumber = New TextBox()
        LabManufacturer = New Label()
        LabPartNumber = New Label()
        LabStyle = New Label()
        LabMaterial = New Label()
        LabRotation = New Label()
        LabBlades = New Label()
        LabDiameter = New Label()
        LabBore = New Label()
        LabSerialNumber = New Label()
        LabStampNumber = New Label()
        TxtBore = New TextBox()
        TxtDiameter = New TextBox()
        ComboRotation = New ComboBox()
        ComboBlades = New ComboBox()
        ComboMaterial = New ComboBox()
        ComboStyle = New ComboBox()
        ComboManufacturer = New ComboBox()
        TxtPartNumber = New TextBox()
        TxtSerialNumber = New TextBox()
        ComboTeExclusion = New ComboBox()
        ComboLEExclusion = New ComboBox()
        ComboInspectedBy = New ComboBox()
        ComboCup = New ComboBox()
        LabMarkedPitch = New Label()
        LabDesiredPitch = New Label()
        TxtMarkedPitch = New TextBox()
        TxtDesiredPitch = New TextBox()
        LabLEExclusion = New Label()
        LabTEExclusion = New Label()
        LabCup = New Label()
        LabDAR = New Label()
        LabelInspectedBy = New Label()
        TableLayoutMeasurements = New TableLayoutPanel()
        CustomLabelMeasurements = New CustomLabel()
        DataGridMeasurements = New DataGridView()
        DateStarted = New DataGridViewTextBoxColumn()
        MeasurementType = New DataGridViewComboBoxColumn()
        PerformedBy = New DataGridViewComboBoxColumn()
        Description = New DataGridViewTextBoxColumn()
        PanelSearch = New CustomPanel()
        PanelImport = New CustomPanel()
        PanelPropeller = New CustomPanel()
        PanelMeasurements = New CustomPanel()
        FontDialog1 = New FontDialog()
        RecordNavigationBar2 = New RecordNavigationBar()
        TableLayoutNavigation = New TableLayoutPanel()
        CustomLabelNavigation = New CustomLabel()
        PanelNavigation = New CustomPanel()
        Button1 = New Button()
        CType(MeasurementTypesBindingSource, ComponentModel.ISupportInitialize).BeginInit()
        CType(EmployeesBindingSource, ComponentModel.ISupportInitialize).BeginInit()
        CType(JobDetailsBindingSource, ComponentModel.ISupportInitialize).BeginInit()
        CType(JobsBindingSource, ComponentModel.ISupportInitialize).BeginInit()
        CType(PictureBoxLogo, ComponentModel.ISupportInitialize).BeginInit()
        TableLayoutImport.SuspendLayout()
        TableLayoutSearch.SuspendLayout()
        TableLayoutPropeller.SuspendLayout()
        TableLayoutMeasurements.SuspendLayout()
        CType(DataGridMeasurements, ComponentModel.ISupportInitialize).BeginInit()
        TableLayoutNavigation.SuspendLayout()
        SuspendLayout()
        ' 
        ' MeasurementTypesBindingSource
        ' 
        MeasurementTypesBindingSource.DataSource = GetType(Models.MeasurementType)
        ' 
        ' EmployeesBindingSource
        ' 
        EmployeesBindingSource.DataSource = GetType(Models.Employee)
        EmployeesBindingSource.Sort = ""
        ' 
        ' JobDetailsBindingSource
        ' 
        JobDetailsBindingSource.DataSource = GetType(Models.JobDetail)
        ' 
        ' JobsBindingSource
        ' 
        JobsBindingSource.DataSource = GetType(Models.Job)
        JobsBindingSource.Sort = ""
        ' 
        ' PictureBoxLogo
        ' 
        PictureBoxLogo.Image = CType(resources.GetObject("PictureBoxLogo.Image"), Image)
        PictureBoxLogo.InitialImage = CType(resources.GetObject("PictureBoxLogo.InitialImage"), Image)
        PictureBoxLogo.Location = New Point(12, 13)
        PictureBoxLogo.Name = "PictureBoxLogo"
        PictureBoxLogo.Size = New Size(643, 86)
        PictureBoxLogo.SizeMode = PictureBoxSizeMode.CenterImage
        PictureBoxLogo.TabIndex = 269
        PictureBoxLogo.TabStop = False
        ' 
        ' TableLayoutImport
        ' 
        TableLayoutImport.ColumnCount = 4
        TableLayoutImport.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 98F))
        TableLayoutImport.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 76F))
        TableLayoutImport.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        TableLayoutImport.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 38F))
        TableLayoutImport.Controls.Add(CmdScanDataPick, 3, 1)
        TableLayoutImport.Controls.Add(CmdScanDataExport, 2, 2)
        TableLayoutImport.Controls.Add(CmdScanDataImport, 1, 2)
        TableLayoutImport.Controls.Add(CustomLabelImport, 0, 0)
        TableLayoutImport.Controls.Add(LabScanData, 0, 1)
        TableLayoutImport.Controls.Add(TxtScanData, 1, 1)
        TableLayoutImport.Location = New Point(362, 188)
        TableLayoutImport.Name = "TableLayoutImport"
        TableLayoutImport.RowCount = 3
        TableLayoutImport.RowStyles.Add(New RowStyle(SizeType.Absolute, 21F))
        TableLayoutImport.RowStyles.Add(New RowStyle(SizeType.Absolute, 29F))
        TableLayoutImport.RowStyles.Add(New RowStyle(SizeType.Absolute, 31F))
        TableLayoutImport.Size = New Size(293, 75)
        TableLayoutImport.TabIndex = 272
        ' 
        ' CmdScanDataPick
        ' 
        CmdScanDataPick.Image = CType(resources.GetObject("CmdScanDataPick.Image"), Image)
        CmdScanDataPick.Location = New Point(257, 23)
        CmdScanDataPick.Margin = New Padding(2, 2, 2, 1)
        CmdScanDataPick.Name = "CmdScanDataPick"
        CmdScanDataPick.Size = New Size(34, 25)
        CmdScanDataPick.TabIndex = 276
        CmdScanDataPick.UseVisualStyleBackColor = True
        ' 
        ' CmdScanDataExport
        ' 
        CmdScanDataExport.Enabled = False
        CmdScanDataExport.Image = CType(resources.GetObject("CmdScanDataExport.Image"), Image)
        CmdScanDataExport.Location = New Point(176, 51)
        CmdScanDataExport.Margin = New Padding(2, 1, 2, 1)
        CmdScanDataExport.Name = "CmdScanDataExport"
        CmdScanDataExport.Size = New Size(72, 22)
        CmdScanDataExport.TabIndex = 277
        CmdScanDataExport.UseVisualStyleBackColor = True
        ' 
        ' CmdScanDataImport
        ' 
        CmdScanDataImport.Enabled = False
        CmdScanDataImport.Image = CType(resources.GetObject("CmdScanDataImport.Image"), Image)
        CmdScanDataImport.Location = New Point(100, 51)
        CmdScanDataImport.Margin = New Padding(2, 1, 2, 1)
        CmdScanDataImport.Name = "CmdScanDataImport"
        CmdScanDataImport.Size = New Size(72, 22)
        CmdScanDataImport.TabIndex = 278
        CmdScanDataImport.UseVisualStyleBackColor = True
        ' 
        ' CustomLabelImport
        ' 
        CustomLabelImport.AutoSize = True
        CustomLabelImport.AutoSizeMode = AutoSizeMode.GrowAndShrink
        CustomLabelImport.BackColor = SystemColors.GradientInactiveCaption
        TableLayoutImport.SetColumnSpan(CustomLabelImport, 4)
        CustomLabelImport.Dock = DockStyle.Fill
        CustomLabelImport.Location = New Point(3, 3)
        CustomLabelImport.Name = "CustomLabelImport"
        CustomLabelImport.Size = New Size(287, 15)
        CustomLabelImport.TabIndex = 289
        CustomLabelImport.Text = "Import"
        ' 
        ' LabScanData
        ' 
        LabScanData.Anchor = AnchorStyles.Left
        LabScanData.AutoSize = True
        LabScanData.Location = New Point(3, 28)
        LabScanData.Name = "LabScanData"
        LabScanData.Size = New Size(59, 15)
        LabScanData.TabIndex = 238
        LabScanData.Text = "Scan Data"
        ' 
        ' TxtScanData
        ' 
        TableLayoutImport.SetColumnSpan(TxtScanData, 2)
        TxtScanData.Dock = DockStyle.Fill
        TxtScanData.Location = New Point(101, 24)
        TxtScanData.Name = "TxtScanData"
        TxtScanData.Size = New Size(151, 23)
        TxtScanData.TabIndex = 239
        TxtScanData.Tag = "LabScanData"
        ' 
        ' TableLayoutSearch
        ' 
        TableLayoutSearch.BackColor = SystemColors.Control
        TableLayoutSearch.ColumnCount = 2
        TableLayoutSearch.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 66F))
        TableLayoutSearch.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        TableLayoutSearch.Controls.Add(CustomLabelSearch, 0, 0)
        TableLayoutSearch.Controls.Add(LabJob, 0, 3)
        TableLayoutSearch.Controls.Add(LabVessel, 0, 2)
        TableLayoutSearch.Controls.Add(ComboVessels, 1, 2)
        TableLayoutSearch.Controls.Add(ComboJobs, 1, 3)
        TableLayoutSearch.Controls.Add(ComboCustomers, 1, 1)
        TableLayoutSearch.Controls.Add(LabCustomer, 0, 1)
        TableLayoutSearch.Location = New Point(12, 188)
        TableLayoutSearch.Name = "TableLayoutSearch"
        TableLayoutSearch.RowCount = 4
        TableLayoutSearch.RowStyles.Add(New RowStyle(SizeType.Percent, 19.56787F))
        TableLayoutSearch.RowStyles.Add(New RowStyle(SizeType.Percent, 26.8102169F))
        TableLayoutSearch.RowStyles.Add(New RowStyle(SizeType.Percent, 26.8102169F))
        TableLayoutSearch.RowStyles.Add(New RowStyle(SizeType.Percent, 26.8116951F))
        TableLayoutSearch.Size = New Size(285, 110)
        TableLayoutSearch.TabIndex = 3
        ' 
        ' CustomLabelSearch
        ' 
        CustomLabelSearch.AutoSize = True
        CustomLabelSearch.AutoSizeMode = AutoSizeMode.GrowAndShrink
        CustomLabelSearch.BackColor = SystemColors.GradientInactiveCaption
        TableLayoutSearch.SetColumnSpan(CustomLabelSearch, 2)
        CustomLabelSearch.Dock = DockStyle.Fill
        CustomLabelSearch.Location = New Point(3, 3)
        CustomLabelSearch.Name = "CustomLabelSearch"
        CustomLabelSearch.Size = New Size(279, 15)
        CustomLabelSearch.TabIndex = 289
        CustomLabelSearch.Text = "Search"
        ' 
        ' LabJob
        ' 
        LabJob.Anchor = AnchorStyles.Left
        LabJob.AutoSize = True
        LabJob.Location = New Point(3, 87)
        LabJob.Name = "LabJob"
        LabJob.Size = New Size(25, 15)
        LabJob.TabIndex = 199
        LabJob.Text = "Job"
        ' 
        ' LabVessel
        ' 
        LabVessel.Anchor = AnchorStyles.Left
        LabVessel.AutoSize = True
        LabVessel.Location = New Point(3, 57)
        LabVessel.Name = "LabVessel"
        LabVessel.Size = New Size(38, 15)
        LabVessel.TabIndex = 198
        LabVessel.Text = "Vessel"
        ' 
        ' ComboVessels
        ' 
        ComboVessels.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        ComboVessels.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        ComboVessels.AutoCompleteSource = AutoCompleteSource.ListItems
        ComboVessels.DisplayMember = "VesselName"
        ComboVessels.FormattingEnabled = True
        ComboVessels.Location = New Point(92, 53)
        ComboVessels.Name = "ComboVessels"
        ComboVessels.Size = New Size(190, 23)
        ComboVessels.TabIndex = 194
        ComboVessels.Tag = "LabVessel"
        ComboVessels.ValueMember = "Id"
        ' 
        ' ComboJobs
        ' 
        ComboJobs.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        ComboJobs.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        ComboJobs.AutoCompleteSource = AutoCompleteSource.ListItems
        ComboJobs.DataSource = JobsBindingSource
        ComboJobs.DisplayMember = "JobNumber"
        ComboJobs.FormattingEnabled = True
        ComboJobs.Location = New Point(92, 82)
        ComboJobs.Name = "ComboJobs"
        ComboJobs.Size = New Size(190, 23)
        ComboJobs.TabIndex = 193
        ComboJobs.Tag = "LabJob"
        ComboJobs.ValueMember = "Id"
        ' 
        ' ComboCustomers
        ' 
        ComboCustomers.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        ComboCustomers.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        ComboCustomers.AutoCompleteSource = AutoCompleteSource.ListItems
        ComboCustomers.BackColor = SystemColors.Window
        ComboCustomers.DisplayMember = "CustomerName"
        ComboCustomers.Font = New Font("Segoe UI", 9F)
        ComboCustomers.ForeColor = SystemColors.ControlText
        ComboCustomers.FormattingEnabled = True
        ComboCustomers.Location = New Point(92, 24)
        ComboCustomers.Name = "ComboCustomers"
        ComboCustomers.Size = New Size(190, 23)
        ComboCustomers.TabIndex = 195
        ComboCustomers.Tag = "LabCustomer"
        ComboCustomers.ValueMember = "Id"
        ' 
        ' LabCustomer
        ' 
        LabCustomer.Anchor = AnchorStyles.Left
        LabCustomer.AutoSize = True
        LabCustomer.Location = New Point(3, 28)
        LabCustomer.Name = "LabCustomer"
        LabCustomer.Size = New Size(59, 15)
        LabCustomer.TabIndex = 197
        LabCustomer.Text = "Customer"
        ' 
        ' TableLayoutPropeller
        ' 
        TableLayoutPropeller.ColumnCount = 4
        TableLayoutPropeller.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 89F))
        TableLayoutPropeller.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 260F))
        TableLayoutPropeller.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 99F))
        TableLayoutPropeller.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 177F))
        TableLayoutPropeller.Controls.Add(CustomLabelPropeller, 0, 0)
        TableLayoutPropeller.Controls.Add(TxtDAR, 3, 7)
        TableLayoutPropeller.Controls.Add(TxtStampNumber, 3, 2)
        TableLayoutPropeller.Controls.Add(LabManufacturer, 0, 1)
        TableLayoutPropeller.Controls.Add(LabPartNumber, 0, 2)
        TableLayoutPropeller.Controls.Add(LabStyle, 0, 3)
        TableLayoutPropeller.Controls.Add(LabMaterial, 0, 4)
        TableLayoutPropeller.Controls.Add(LabRotation, 0, 5)
        TableLayoutPropeller.Controls.Add(LabBlades, 0, 6)
        TableLayoutPropeller.Controls.Add(LabDiameter, 0, 7)
        TableLayoutPropeller.Controls.Add(LabBore, 2, 3)
        TableLayoutPropeller.Controls.Add(LabSerialNumber, 2, 1)
        TableLayoutPropeller.Controls.Add(LabStampNumber, 2, 2)
        TableLayoutPropeller.Controls.Add(TxtBore, 3, 3)
        TableLayoutPropeller.Controls.Add(TxtDiameter, 1, 7)
        TableLayoutPropeller.Controls.Add(ComboRotation, 1, 5)
        TableLayoutPropeller.Controls.Add(ComboBlades, 1, 6)
        TableLayoutPropeller.Controls.Add(ComboMaterial, 1, 4)
        TableLayoutPropeller.Controls.Add(ComboStyle, 1, 3)
        TableLayoutPropeller.Controls.Add(ComboManufacturer, 1, 1)
        TableLayoutPropeller.Controls.Add(TxtPartNumber, 1, 2)
        TableLayoutPropeller.Controls.Add(TxtSerialNumber, 3, 1)
        TableLayoutPropeller.Controls.Add(ComboTeExclusion, 3, 5)
        TableLayoutPropeller.Controls.Add(ComboLEExclusion, 3, 4)
        TableLayoutPropeller.Controls.Add(ComboInspectedBy, 3, 8)
        TableLayoutPropeller.Controls.Add(ComboCup, 3, 6)
        TableLayoutPropeller.Controls.Add(LabMarkedPitch, 0, 8)
        TableLayoutPropeller.Controls.Add(LabDesiredPitch, 0, 9)
        TableLayoutPropeller.Controls.Add(TxtMarkedPitch, 1, 8)
        TableLayoutPropeller.Controls.Add(TxtDesiredPitch, 1, 9)
        TableLayoutPropeller.Controls.Add(LabLEExclusion, 2, 4)
        TableLayoutPropeller.Controls.Add(LabTEExclusion, 2, 5)
        TableLayoutPropeller.Controls.Add(LabCup, 2, 6)
        TableLayoutPropeller.Controls.Add(LabDAR, 2, 7)
        TableLayoutPropeller.Controls.Add(LabelInspectedBy, 2, 8)
        TableLayoutPropeller.Location = New Point(12, 318)
        TableLayoutPropeller.Name = "TableLayoutPropeller"
        TableLayoutPropeller.RowCount = 10
        TableLayoutPropeller.RowStyles.Add(New RowStyle(SizeType.Absolute, 21F))
        TableLayoutPropeller.RowStyles.Add(New RowStyle(SizeType.Absolute, 29.33F))
        TableLayoutPropeller.RowStyles.Add(New RowStyle(SizeType.Absolute, 29.33F))
        TableLayoutPropeller.RowStyles.Add(New RowStyle(SizeType.Absolute, 29.33F))
        TableLayoutPropeller.RowStyles.Add(New RowStyle(SizeType.Absolute, 29.33F))
        TableLayoutPropeller.RowStyles.Add(New RowStyle(SizeType.Absolute, 29.33F))
        TableLayoutPropeller.RowStyles.Add(New RowStyle(SizeType.Absolute, 29.33F))
        TableLayoutPropeller.RowStyles.Add(New RowStyle(SizeType.Absolute, 29.33F))
        TableLayoutPropeller.RowStyles.Add(New RowStyle(SizeType.Absolute, 29.33F))
        TableLayoutPropeller.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        TableLayoutPropeller.Size = New Size(644, 283)
        TableLayoutPropeller.TabIndex = 2
        ' 
        ' CustomLabelPropeller
        ' 
        CustomLabelPropeller.AutoSize = True
        CustomLabelPropeller.AutoSizeMode = AutoSizeMode.GrowAndShrink
        CustomLabelPropeller.BackColor = SystemColors.GradientInactiveCaption
        TableLayoutPropeller.SetColumnSpan(CustomLabelPropeller, 4)
        CustomLabelPropeller.Dock = DockStyle.Fill
        CustomLabelPropeller.Location = New Point(3, 3)
        CustomLabelPropeller.Name = "CustomLabelPropeller"
        CustomLabelPropeller.Size = New Size(638, 15)
        CustomLabelPropeller.TabIndex = 283
        CustomLabelPropeller.Text = "Propeller"
        ' 
        ' TxtDAR
        ' 
        TxtDAR.DataBindings.Add(New Binding("Text", JobsBindingSource, "Dar", True))
        TxtDAR.Dock = DockStyle.Fill
        TxtDAR.Location = New Point(451, 198)
        TxtDAR.Name = "TxtDAR"
        TxtDAR.Size = New Size(190, 23)
        TxtDAR.TabIndex = 278
        TxtDAR.Tag = "LabDAR"
        ' 
        ' TxtStampNumber
        ' 
        TxtStampNumber.DataBindings.Add(New Binding("Text", JobsBindingSource, "StampNumber", True))
        TxtStampNumber.Dock = DockStyle.Fill
        TxtStampNumber.Location = New Point(451, 53)
        TxtStampNumber.Name = "TxtStampNumber"
        TxtStampNumber.Size = New Size(190, 23)
        TxtStampNumber.TabIndex = 272
        TxtStampNumber.Tag = "LabStampNumber"
        ' 
        ' LabManufacturer
        ' 
        LabManufacturer.Anchor = AnchorStyles.Left
        LabManufacturer.AutoSize = True
        LabManufacturer.Location = New Point(3, 28)
        LabManufacturer.Name = "LabManufacturer"
        LabManufacturer.Size = New Size(79, 15)
        LabManufacturer.TabIndex = 237
        LabManufacturer.Text = "Manufacturer"
        ' 
        ' LabPartNumber
        ' 
        LabPartNumber.Anchor = AnchorStyles.Left
        LabPartNumber.AutoSize = True
        LabPartNumber.Location = New Point(3, 57)
        LabPartNumber.Name = "LabPartNumber"
        LabPartNumber.Size = New Size(75, 15)
        LabPartNumber.TabIndex = 238
        LabPartNumber.Text = "Part Number"
        ' 
        ' LabStyle
        ' 
        LabStyle.Anchor = AnchorStyles.Left
        LabStyle.AutoSize = True
        LabStyle.Location = New Point(3, 86)
        LabStyle.Name = "LabStyle"
        LabStyle.Size = New Size(32, 15)
        LabStyle.TabIndex = 239
        LabStyle.Text = "Style"
        ' 
        ' LabMaterial
        ' 
        LabMaterial.Anchor = AnchorStyles.Left
        LabMaterial.AutoSize = True
        LabMaterial.Location = New Point(3, 115)
        LabMaterial.Name = "LabMaterial"
        LabMaterial.Size = New Size(50, 15)
        LabMaterial.TabIndex = 240
        LabMaterial.Text = "Material"
        ' 
        ' LabRotation
        ' 
        LabRotation.Anchor = AnchorStyles.Left
        LabRotation.AutoSize = True
        LabRotation.ForeColor = Color.Red
        LabRotation.Location = New Point(3, 144)
        LabRotation.Name = "LabRotation"
        LabRotation.Size = New Size(52, 15)
        LabRotation.TabIndex = 241
        LabRotation.Text = "Rotation"
        ' 
        ' LabBlades
        ' 
        LabBlades.Anchor = AnchorStyles.Left
        LabBlades.AutoSize = True
        LabBlades.ForeColor = Color.Red
        LabBlades.Location = New Point(3, 173)
        LabBlades.Name = "LabBlades"
        LabBlades.Size = New Size(41, 15)
        LabBlades.TabIndex = 242
        LabBlades.Text = "Blades"
        ' 
        ' LabDiameter
        ' 
        LabDiameter.Anchor = AnchorStyles.Left
        LabDiameter.AutoSize = True
        LabDiameter.ForeColor = Color.Red
        LabDiameter.Location = New Point(3, 202)
        LabDiameter.Name = "LabDiameter"
        LabDiameter.Size = New Size(55, 15)
        LabDiameter.TabIndex = 243
        LabDiameter.Tag = "LabDiameter"
        LabDiameter.Text = "Diameter"
        ' 
        ' LabBore
        ' 
        LabBore.Anchor = AnchorStyles.Left
        LabBore.AutoSize = True
        LabBore.Location = New Point(352, 86)
        LabBore.Name = "LabBore"
        LabBore.Size = New Size(31, 15)
        LabBore.TabIndex = 248
        LabBore.Text = "Bore"
        ' 
        ' LabSerialNumber
        ' 
        LabSerialNumber.Anchor = AnchorStyles.Left
        LabSerialNumber.AutoSize = True
        LabSerialNumber.Location = New Point(352, 28)
        LabSerialNumber.Name = "LabSerialNumber"
        LabSerialNumber.Size = New Size(82, 15)
        LabSerialNumber.TabIndex = 249
        LabSerialNumber.Text = "Serial Number"
        ' 
        ' LabStampNumber
        ' 
        LabStampNumber.Anchor = AnchorStyles.Left
        LabStampNumber.AutoSize = True
        LabStampNumber.Location = New Point(352, 57)
        LabStampNumber.Name = "LabStampNumber"
        LabStampNumber.Size = New Size(88, 15)
        LabStampNumber.TabIndex = 250
        LabStampNumber.Text = "Stamp Number"
        ' 
        ' TxtBore
        ' 
        TxtBore.DataBindings.Add(New Binding("Text", JobsBindingSource, "PropellerBore", True))
        TxtBore.Location = New Point(451, 82)
        TxtBore.Name = "TxtBore"
        TxtBore.Size = New Size(190, 23)
        TxtBore.TabIndex = 270
        TxtBore.Tag = "LabBore"
        ' 
        ' TxtDiameter
        ' 
        TxtDiameter.DataBindings.Add(New Binding("Text", JobsBindingSource, "PropellerDiameter", True))
        TxtDiameter.Location = New Point(92, 198)
        TxtDiameter.Name = "TxtDiameter"
        TxtDiameter.Size = New Size(190, 23)
        TxtDiameter.TabIndex = 269
        TxtDiameter.Tag = "LabDiameter"
        ' 
        ' ComboRotation
        ' 
        ComboRotation.DataBindings.Add(New Binding("SelectedValue", JobsBindingSource, "PropellerRotation", True))
        ComboRotation.DisplayMember = "Rotation1"
        ComboRotation.FormattingEnabled = True
        ComboRotation.Location = New Point(92, 140)
        ComboRotation.Name = "ComboRotation"
        ComboRotation.Size = New Size(190, 23)
        ComboRotation.TabIndex = 267
        ComboRotation.Tag = "LabRotation"
        ComboRotation.ValueMember = "Rotation1"
        ' 
        ' ComboBlades
        ' 
        ComboBlades.DataBindings.Add(New Binding("SelectedValue", JobsBindingSource, "PropellerBlades", True))
        ComboBlades.DataBindings.Add(New Binding("SelectedItem", JobsBindingSource, "PropellerBladesNavigation", True))
        ComboBlades.DisplayMember = "BladeCount"
        ComboBlades.FormattingEnabled = True
        ComboBlades.Location = New Point(92, 169)
        ComboBlades.Name = "ComboBlades"
        ComboBlades.Size = New Size(190, 23)
        ComboBlades.TabIndex = 268
        ComboBlades.Tag = "LabBlades"
        ComboBlades.ValueMember = "BladeCount"
        ' 
        ' ComboMaterial
        ' 
        ComboMaterial.DataBindings.Add(New Binding("SelectedValue", JobsBindingSource, "PropellerMaterial", True))
        ComboMaterial.DisplayMember = "Material1"
        ComboMaterial.FormattingEnabled = True
        ComboMaterial.Location = New Point(92, 111)
        ComboMaterial.Name = "ComboMaterial"
        ComboMaterial.Size = New Size(190, 23)
        ComboMaterial.TabIndex = 266
        ComboMaterial.Tag = "LabMaterial"
        ComboMaterial.ValueMember = "Material1"
        ' 
        ' ComboStyle
        ' 
        ComboStyle.DataBindings.Add(New Binding("SelectedValue", JobsBindingSource, "PropellerStyle", True))
        ComboStyle.DisplayMember = "Style1"
        ComboStyle.FormattingEnabled = True
        ComboStyle.Location = New Point(92, 82)
        ComboStyle.Name = "ComboStyle"
        ComboStyle.Size = New Size(190, 23)
        ComboStyle.TabIndex = 265
        ComboStyle.Tag = "LabStyle"
        ComboStyle.ValueMember = "Style1"
        ' 
        ' ComboManufacturer
        ' 
        ComboManufacturer.DataBindings.Add(New Binding("SelectedValue", JobsBindingSource, "PropellerManufacturerId", True))
        ComboManufacturer.DisplayMember = "ManufacturerName"
        ComboManufacturer.FormattingEnabled = True
        ComboManufacturer.Location = New Point(92, 24)
        ComboManufacturer.Name = "ComboManufacturer"
        ComboManufacturer.Size = New Size(190, 23)
        ComboManufacturer.TabIndex = 263
        ComboManufacturer.Tag = "LabManufacturer"
        ComboManufacturer.ValueMember = "Id"
        ' 
        ' TxtPartNumber
        ' 
        TxtPartNumber.DataBindings.Add(New Binding("Text", JobsBindingSource, "PropellerPartNumber", True))
        TxtPartNumber.Location = New Point(92, 53)
        TxtPartNumber.Name = "TxtPartNumber"
        TxtPartNumber.Size = New Size(190, 23)
        TxtPartNumber.TabIndex = 264
        TxtPartNumber.Tag = "LabPartNumber"
        ' 
        ' TxtSerialNumber
        ' 
        TxtSerialNumber.DataBindings.Add(New Binding("Text", JobsBindingSource, "SerialNumber", True))
        TxtSerialNumber.Dock = DockStyle.Fill
        TxtSerialNumber.Location = New Point(451, 24)
        TxtSerialNumber.Name = "TxtSerialNumber"
        TxtSerialNumber.Size = New Size(190, 23)
        TxtSerialNumber.TabIndex = 271
        TxtSerialNumber.Tag = "LabSerialNumber"
        ' 
        ' ComboTeExclusion
        ' 
        ComboTeExclusion.DataBindings.Add(New Binding("SelectedValue", JobsBindingSource, "TeExclusion", True))
        ComboTeExclusion.DisplayMember = "Exclusion1"
        ComboTeExclusion.Dock = DockStyle.Fill
        ComboTeExclusion.FormattingEnabled = True
        ComboTeExclusion.Location = New Point(451, 140)
        ComboTeExclusion.Name = "ComboTeExclusion"
        ComboTeExclusion.Size = New Size(190, 23)
        ComboTeExclusion.TabIndex = 275
        ComboTeExclusion.Tag = "LabTEExclusion"
        ComboTeExclusion.ValueMember = "Exclusion1"
        ' 
        ' ComboLEExclusion
        ' 
        ComboLEExclusion.DataBindings.Add(New Binding("SelectedValue", JobsBindingSource, "LeExclusion", True))
        ComboLEExclusion.DisplayMember = "Exclusion1"
        ComboLEExclusion.Dock = DockStyle.Fill
        ComboLEExclusion.FormattingEnabled = True
        ComboLEExclusion.Location = New Point(451, 111)
        ComboLEExclusion.Name = "ComboLEExclusion"
        ComboLEExclusion.Size = New Size(190, 23)
        ComboLEExclusion.TabIndex = 274
        ComboLEExclusion.Tag = "LabLEExclusion"
        ComboLEExclusion.ValueMember = "Exclusion1"
        ' 
        ' ComboInspectedBy
        ' 
        ComboInspectedBy.DataBindings.Add(New Binding("SelectedValue", JobsBindingSource, "InspectedBy", True))
        ComboInspectedBy.DataSource = EmployeesBindingSource
        ComboInspectedBy.DisplayMember = "EmployeeName"
        ComboInspectedBy.Dock = DockStyle.Fill
        ComboInspectedBy.Font = New Font("Segoe UI", 9F)
        ComboInspectedBy.FormattingEnabled = True
        ComboInspectedBy.Location = New Point(451, 227)
        ComboInspectedBy.Name = "ComboInspectedBy"
        ComboInspectedBy.Size = New Size(190, 23)
        ComboInspectedBy.TabIndex = 279
        ComboInspectedBy.Tag = "LabelInspectedBy"
        ComboInspectedBy.ValueMember = "Id"
        ' 
        ' ComboCup
        ' 
        ComboCup.DataBindings.Add(New Binding("SelectedValue", JobsBindingSource, "Cup", True))
        ComboCup.DisplayMember = "Cup1"
        ComboCup.Dock = DockStyle.Fill
        ComboCup.FormattingEnabled = True
        ComboCup.Location = New Point(451, 169)
        ComboCup.Name = "ComboCup"
        ComboCup.Size = New Size(190, 23)
        ComboCup.TabIndex = 277
        ComboCup.Tag = "LabCup"
        ComboCup.ValueMember = "Cup1"
        ' 
        ' LabMarkedPitch
        ' 
        LabMarkedPitch.Anchor = AnchorStyles.Left
        LabMarkedPitch.AutoSize = True
        LabMarkedPitch.ForeColor = Color.Red
        LabMarkedPitch.Location = New Point(3, 231)
        LabMarkedPitch.Name = "LabMarkedPitch"
        LabMarkedPitch.Size = New Size(77, 15)
        LabMarkedPitch.TabIndex = 256
        LabMarkedPitch.Text = "Marked Pitch"
        ' 
        ' LabDesiredPitch
        ' 
        LabDesiredPitch.Anchor = AnchorStyles.Left
        LabDesiredPitch.AutoSize = True
        LabDesiredPitch.ForeColor = Color.Red
        LabDesiredPitch.Location = New Point(3, 260)
        LabDesiredPitch.Name = "LabDesiredPitch"
        LabDesiredPitch.Size = New Size(76, 15)
        LabDesiredPitch.TabIndex = 257
        LabDesiredPitch.Text = "Desired Pitch"
        ' 
        ' TxtMarkedPitch
        ' 
        TxtMarkedPitch.DataBindings.Add(New Binding("Text", JobsBindingSource, "MarkedPitch", True))
        TxtMarkedPitch.Location = New Point(92, 227)
        TxtMarkedPitch.Name = "TxtMarkedPitch"
        TxtMarkedPitch.Size = New Size(190, 23)
        TxtMarkedPitch.TabIndex = 273
        TxtMarkedPitch.Tag = "LabMarkedPitch"
        ' 
        ' TxtDesiredPitch
        ' 
        TxtDesiredPitch.DataBindings.Add(New Binding("Text", JobsBindingSource, "DesiredPitch", True))
        TxtDesiredPitch.Location = New Point(92, 256)
        TxtDesiredPitch.Name = "TxtDesiredPitch"
        TxtDesiredPitch.Size = New Size(190, 23)
        TxtDesiredPitch.TabIndex = 276
        TxtDesiredPitch.Tag = "LabDesiredPitch"
        ' 
        ' LabLEExclusion
        ' 
        LabLEExclusion.Anchor = AnchorStyles.Left
        LabLEExclusion.AutoSize = True
        LabLEExclusion.Location = New Point(352, 115)
        LabLEExclusion.Name = "LabLEExclusion"
        LabLEExclusion.Size = New Size(71, 15)
        LabLEExclusion.TabIndex = 258
        LabLEExclusion.Text = "LE Exclusion"
        ' 
        ' LabTEExclusion
        ' 
        LabTEExclusion.Anchor = AnchorStyles.Left
        LabTEExclusion.AutoSize = True
        LabTEExclusion.Location = New Point(352, 144)
        LabTEExclusion.Name = "LabTEExclusion"
        LabTEExclusion.Size = New Size(72, 15)
        LabTEExclusion.TabIndex = 259
        LabTEExclusion.Text = "TE Exclusion"
        ' 
        ' LabCup
        ' 
        LabCup.Anchor = AnchorStyles.Left
        LabCup.AutoSize = True
        LabCup.Location = New Point(352, 173)
        LabCup.Name = "LabCup"
        LabCup.Size = New Size(29, 15)
        LabCup.TabIndex = 260
        LabCup.Text = "Cup"
        ' 
        ' LabDAR
        ' 
        LabDAR.Anchor = AnchorStyles.Left
        LabDAR.AutoSize = True
        LabDAR.Location = New Point(352, 202)
        LabDAR.Name = "LabDAR"
        LabDAR.Size = New Size(39, 15)
        LabDAR.TabIndex = 261
        LabDAR.Text = "D.A.R."
        ' 
        ' LabelInspectedBy
        ' 
        LabelInspectedBy.Anchor = AnchorStyles.Left
        LabelInspectedBy.AutoSize = True
        LabelInspectedBy.Font = New Font("Segoe UI", 9F)
        LabelInspectedBy.Location = New Point(352, 231)
        LabelInspectedBy.Name = "LabelInspectedBy"
        LabelInspectedBy.Size = New Size(74, 15)
        LabelInspectedBy.TabIndex = 262
        LabelInspectedBy.Text = "Inspected By"
        ' 
        ' TableLayoutMeasurements
        ' 
        TableLayoutMeasurements.ColumnCount = 1
        TableLayoutMeasurements.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        TableLayoutMeasurements.Controls.Add(CustomLabelMeasurements, 0, 0)
        TableLayoutMeasurements.Controls.Add(DataGridMeasurements, 0, 1)
        TableLayoutMeasurements.Location = New Point(12, 623)
        TableLayoutMeasurements.Name = "TableLayoutMeasurements"
        TableLayoutMeasurements.RowCount = 2
        TableLayoutMeasurements.RowStyles.Add(New RowStyle(SizeType.Percent, 7F))
        TableLayoutMeasurements.RowStyles.Add(New RowStyle(SizeType.Percent, 93F))
        TableLayoutMeasurements.Size = New Size(644, 299)
        TableLayoutMeasurements.TabIndex = 1
        ' 
        ' CustomLabelMeasurements
        ' 
        CustomLabelMeasurements.AutoSize = True
        CustomLabelMeasurements.AutoSizeMode = AutoSizeMode.GrowAndShrink
        CustomLabelMeasurements.BackColor = SystemColors.GradientInactiveCaption
        CustomLabelMeasurements.Dock = DockStyle.Fill
        CustomLabelMeasurements.Location = New Point(3, 3)
        CustomLabelMeasurements.Name = "CustomLabelMeasurements"
        CustomLabelMeasurements.Size = New Size(638, 14)
        CustomLabelMeasurements.TabIndex = 283
        CustomLabelMeasurements.Text = "Measurements"
        ' 
        ' DataGridMeasurements
        ' 
        DataGridMeasurements.AllowUserToAddRows = False
        DataGridMeasurements.AllowUserToDeleteRows = False
        DataGridMeasurements.BackgroundColor = SystemColors.Control
        DataGridMeasurements.BorderStyle = BorderStyle.None
        DataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = SystemColors.Control
        DataGridViewCellStyle3.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle3.ForeColor = SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = DataGridViewTriState.True
        DataGridMeasurements.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        DataGridMeasurements.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridMeasurements.Columns.AddRange(New DataGridViewColumn() {DateStarted, MeasurementType, PerformedBy, Description})
        DataGridMeasurements.Dock = DockStyle.Fill
        DataGridMeasurements.Location = New Point(3, 23)
        DataGridMeasurements.MultiSelect = False
        DataGridMeasurements.Name = "DataGridMeasurements"
        DataGridMeasurements.ReadOnly = True
        DataGridMeasurements.Size = New Size(638, 273)
        DataGridMeasurements.TabIndex = 1
        ' 
        ' DateStarted
        ' 
        DateStarted.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        DateStarted.DataPropertyName = "StartDate"
        DateStarted.HeaderText = "Data Started"
        DateStarted.MinimumWidth = 100
        DateStarted.Name = "DateStarted"
        DateStarted.ReadOnly = True
        DateStarted.Resizable = DataGridViewTriState.True
        ' 
        ' MeasurementType
        ' 
        MeasurementType.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        MeasurementType.DataPropertyName = "MeasurementTypeId"
        MeasurementType.DataSource = MeasurementTypesBindingSource
        MeasurementType.DisplayMember = "MeasurementType1"
        MeasurementType.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing
        MeasurementType.HeaderText = "Measurement Type"
        MeasurementType.MinimumWidth = 139
        MeasurementType.Name = "MeasurementType"
        MeasurementType.ReadOnly = True
        MeasurementType.Resizable = DataGridViewTriState.True
        MeasurementType.SortMode = DataGridViewColumnSortMode.Automatic
        MeasurementType.ValueMember = "Id"
        MeasurementType.Width = 139
        ' 
        ' PerformedBy
        ' 
        PerformedBy.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        PerformedBy.DataPropertyName = "PerformedBy"
        PerformedBy.DataSource = EmployeesBindingSource
        PerformedBy.DisplayMember = "EmployeeName"
        PerformedBy.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing
        PerformedBy.HeaderText = "Performed By"
        PerformedBy.MinimumWidth = 119
        PerformedBy.Name = "PerformedBy"
        PerformedBy.ReadOnly = True
        PerformedBy.Resizable = DataGridViewTriState.True
        PerformedBy.SortMode = DataGridViewColumnSortMode.Automatic
        PerformedBy.ValueMember = "Id"
        PerformedBy.Width = 119
        ' 
        ' Description
        ' 
        Description.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        Description.DataPropertyName = "Description"
        Description.HeaderText = "Description"
        Description.MinimumWidth = 226
        Description.Name = "Description"
        Description.ReadOnly = True
        Description.Width = 226
        ' 
        ' PanelSearch
        ' 
        PanelSearch.BorderColor = Color.Gray
        PanelSearch.BorderWidth = 0
        PanelSearch.DashPattern = New Single() {0F, 10F}
        PanelSearch.DashStyle = Drawing2D.DashStyle.Solid
        PanelSearch.Location = New Point(9, 185)
        PanelSearch.Name = "PanelSearch"
        PanelSearch.Size = New Size(291, 116)
        PanelSearch.TabIndex = 283
        ' 
        ' PanelImport
        ' 
        PanelImport.BorderColor = Color.Gray
        PanelImport.BorderWidth = 0
        PanelImport.DashPattern = New Single() {0F, 10F}
        PanelImport.DashStyle = Drawing2D.DashStyle.Solid
        PanelImport.Location = New Point(359, 185)
        PanelImport.Name = "PanelImport"
        PanelImport.Size = New Size(299, 81)
        PanelImport.TabIndex = 284
        ' 
        ' PanelPropeller
        ' 
        PanelPropeller.BorderColor = Color.Gray
        PanelPropeller.BorderWidth = 0
        PanelPropeller.DashPattern = New Single() {0F, 10F}
        PanelPropeller.DashStyle = Drawing2D.DashStyle.Solid
        PanelPropeller.Location = New Point(9, 315)
        PanelPropeller.Name = "PanelPropeller"
        PanelPropeller.Size = New Size(650, 289)
        PanelPropeller.TabIndex = 286
        ' 
        ' PanelMeasurements
        ' 
        PanelMeasurements.BorderColor = Color.Gray
        PanelMeasurements.BorderWidth = 0
        PanelMeasurements.DashPattern = New Single() {0F, 10F}
        PanelMeasurements.DashStyle = Drawing2D.DashStyle.Solid
        PanelMeasurements.Location = New Point(9, 619)
        PanelMeasurements.Name = "PanelMeasurements"
        PanelMeasurements.Size = New Size(650, 306)
        PanelMeasurements.TabIndex = 287
        ' 
        ' RecordNavigationBar2
        ' 
        RecordNavigationBar2.AutoSize = True
        RecordNavigationBar2.AutoSizeMode = AutoSizeMode.GrowAndShrink
        RecordNavigationBar2.BoundControls = Nothing
        RecordNavigationBar2.Database = Nothing
        RecordNavigationBar2.Filter = Nothing
        RecordNavigationBar2.FilterOn = False
        RecordNavigationBar2.Location = New Point(3, 21)
        RecordNavigationBar2.Margin = New Padding(3, 0, 0, 0)
        RecordNavigationBar2.MasterSource = Nothing
        RecordNavigationBar2.Name = "RecordNavigationBar2"
        RecordNavigationBar2.NoUpdates = False
        RecordNavigationBar2.ServiceProvider = Nothing
        RecordNavigationBar2.Size = New Size(635, 26)
        RecordNavigationBar2.TabIndex = 281
        ' 
        ' TableLayoutNavigation
        ' 
        TableLayoutNavigation.ColumnCount = 1
        TableLayoutNavigation.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        TableLayoutNavigation.Controls.Add(RecordNavigationBar2, 0, 1)
        TableLayoutNavigation.Controls.Add(CustomLabelNavigation, 0, 0)
        TableLayoutNavigation.Location = New Point(12, 115)
        TableLayoutNavigation.Name = "TableLayoutNavigation"
        TableLayoutNavigation.RowCount = 2
        TableLayoutNavigation.RowStyles.Add(New RowStyle(SizeType.Absolute, 21F))
        TableLayoutNavigation.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        TableLayoutNavigation.Size = New Size(643, 53)
        TableLayoutNavigation.TabIndex = 282
        ' 
        ' CustomLabelNavigation
        ' 
        CustomLabelNavigation.AutoSize = True
        CustomLabelNavigation.AutoSizeMode = AutoSizeMode.GrowAndShrink
        CustomLabelNavigation.BackColor = SystemColors.GradientInactiveCaption
        CustomLabelNavigation.Dock = DockStyle.Fill
        CustomLabelNavigation.Location = New Point(3, 3)
        CustomLabelNavigation.Name = "CustomLabelNavigation"
        CustomLabelNavigation.Size = New Size(637, 15)
        CustomLabelNavigation.TabIndex = 282
        CustomLabelNavigation.Text = "Navigation"
        ' 
        ' PanelNavigation
        ' 
        PanelNavigation.BorderColor = Color.Gray
        PanelNavigation.BorderWidth = 0
        PanelNavigation.DashPattern = New Single() {0F, 10F}
        PanelNavigation.DashStyle = Drawing2D.DashStyle.Solid
        PanelNavigation.Location = New Point(9, 112)
        PanelNavigation.Name = "PanelNavigation"
        PanelNavigation.Size = New Size(649, 59)
        PanelNavigation.TabIndex = 285
        ' 
        ' Button1
        ' 
        Button1.Image = CType(resources.GetObject("Button1.Image"), Image)
        Button1.Location = New Point(15, 76)
        Button1.Name = "Button1"
        Button1.Size = New Size(34, 23)
        Button1.TabIndex = 288
        Button1.UseVisualStyleBackColor = True
        ' 
        ' FrmJobs2
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = SystemColors.Control
        ClientSize = New Size(1042, 870)
        Controls.Add(Button1)
        Controls.Add(TableLayoutMeasurements)
        Controls.Add(TableLayoutPropeller)
        Controls.Add(TableLayoutNavigation)
        Controls.Add(TableLayoutImport)
        Controls.Add(TableLayoutSearch)
        Controls.Add(PictureBoxLogo)
        Controls.Add(PanelSearch)
        Controls.Add(PanelImport)
        Controls.Add(PanelNavigation)
        Controls.Add(PanelPropeller)
        Controls.Add(PanelMeasurements)
        Name = "FrmJobs2"
        Text = "Jobs"
        CType(MeasurementTypesBindingSource, ComponentModel.ISupportInitialize).EndInit()
        CType(EmployeesBindingSource, ComponentModel.ISupportInitialize).EndInit()
        CType(JobDetailsBindingSource, ComponentModel.ISupportInitialize).EndInit()
        CType(JobsBindingSource, ComponentModel.ISupportInitialize).EndInit()
        CType(PictureBoxLogo, ComponentModel.ISupportInitialize).EndInit()
        TableLayoutImport.ResumeLayout(False)
        TableLayoutImport.PerformLayout()
        TableLayoutSearch.ResumeLayout(False)
        TableLayoutSearch.PerformLayout()
        TableLayoutPropeller.ResumeLayout(False)
        TableLayoutPropeller.PerformLayout()
        TableLayoutMeasurements.ResumeLayout(False)
        TableLayoutMeasurements.PerformLayout()
        CType(DataGridMeasurements, ComponentModel.ISupportInitialize).EndInit()
        TableLayoutNavigation.ResumeLayout(False)
        TableLayoutNavigation.PerformLayout()
        ResumeLayout(False)
    End Sub
    Friend WithEvents JobsBindingSource As BindingSource
    Friend WithEvents JobDetailsBindingSource As BindingSource
    Friend WithEvents EmployeesBindingSource As BindingSource
    Friend WithEvents PictureBoxLogo As PictureBox
    Friend WithEvents RecordNavigationBar1 As RecordNavigationBar
    Friend WithEvents MeasurementTypesBindingSource As BindingSource
    Friend WithEvents TableLayoutImport As TableLayoutPanel
    Friend WithEvents TableLayoutSearch As TableLayoutPanel
    Friend WithEvents ComboVessels As ComboBox
    Friend WithEvents ComboJobs As ComboBox
    Friend WithEvents ComboCustomers As ComboBox
    Friend WithEvents TableLayoutPropeller As TableLayoutPanel
    Friend WithEvents TxtDAR As TextBox
    Friend WithEvents TxtStampNumber As TextBox
    Friend WithEvents LabManufacturer As Label
    Friend WithEvents LabPartNumber As Label
    Friend WithEvents LabStyle As Label
    Friend WithEvents LabMaterial As Label
    Friend WithEvents LabRotation As Label
    Friend WithEvents LabBlades As Label
    Friend WithEvents LabDiameter As Label
    Friend WithEvents LabBore As Label
    Friend WithEvents LabSerialNumber As Label
    Friend WithEvents LabStampNumber As Label
    Friend WithEvents LabMarkedPitch As Label
    Friend WithEvents LabDesiredPitch As Label
    Friend WithEvents LabLEExclusion As Label
    Friend WithEvents LabTEExclusion As Label
    Friend WithEvents LabCup As Label
    Friend WithEvents LabDAR As Label
    Friend WithEvents LabelInspectedBy As Label
    Friend WithEvents TxtDiameter As TextBox
    Friend WithEvents TxtBore As TextBox
    Friend WithEvents ComboRotation As ComboBox
    Friend WithEvents ComboBlades As ComboBox
    Friend WithEvents ComboMaterial As ComboBox
    Friend WithEvents ComboStyle As ComboBox
    Friend WithEvents ComboManufacturer As ComboBox
    Friend WithEvents TxtPartNumber As TextBox
    Friend WithEvents TxtSerialNumber As TextBox
    Friend WithEvents TxtMarkedPitch As TextBox
    Friend WithEvents ComboTeExclusion As ComboBox
    Friend WithEvents TxtDesiredPitch As TextBox
    Friend WithEvents ComboLEExclusion As ComboBox
    Friend WithEvents ComboInspectedBy As ComboBox
    Friend WithEvents ComboCup As ComboBox
    Friend WithEvents TableLayoutMeasurements As TableLayoutPanel
    Friend WithEvents DataGridMeasurements As DataGridView
    Friend WithEvents CmdScanDataPick As Button
    Friend WithEvents CmdScanDataImport As Button
    Friend WithEvents CmdScanDataExport As Button
    Friend WithEvents PanelSearch As CustomPanel
    Friend WithEvents PanelImport As CustomPanel
    Friend WithEvents PanelPropeller As CustomPanel
    Friend WithEvents PanelMeasurements As CustomPanel
    Friend WithEvents FontDialog1 As FontDialog
    Friend WithEvents RecordNavigationBar2 As RecordNavigationBar
    Friend WithEvents TableLayoutNavigation As TableLayoutPanel
    Friend WithEvents PanelNavigation As CustomPanel
    Friend WithEvents LabScanData As Label
    Friend WithEvents LabJob As Label
    Friend WithEvents LabVessel As Label
    Friend WithEvents LabCustomer As Label
    Friend WithEvents TxtScanData As TextBox
    Friend WithEvents CustomLabelImport As CustomLabel
    Friend WithEvents CustomLabelSearch As CustomLabel
    Friend WithEvents CustomLabelPropeller As CustomLabel
    Friend WithEvents CustomLabelMeasurements As CustomLabel
    Friend WithEvents CustomLabelNavigation As CustomLabel
    Friend WithEvents Button1 As Button
    Friend WithEvents DateStarted As DataGridViewTextBoxColumn
    Friend WithEvents MeasurementType As DataGridViewComboBoxColumn
    Friend WithEvents PerformedBy As DataGridViewComboBoxColumn
    Friend WithEvents Description As DataGridViewTextBoxColumn
End Class
