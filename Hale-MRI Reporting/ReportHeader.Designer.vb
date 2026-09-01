<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ReportHeader
    Inherits UserControl

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
        PanelHeaderLayout = New TableLayoutPanel()
        TxtWheelPitch = New TextBox()
        JobDetailsBindingSource = New BindingSource(components)
        TxtMarkedPitch = New TextBox()
        TxtMeasuredDiameter = New TextBox()
        TxtMarkedDiameter = New TextBox()
        TxtRotation = New TextBox()
        TxtPerformedBy = New TextBox()
        TxtScanDate = New TextBox()
        TxtFileName = New TextBox()
        LabFilename = New Label()
        LabJobId = New Label()
        LabJobNumber = New Label()
        LabCustomer = New Label()
        LabVessel = New Label()
        LabManufacturer = New Label()
        LabPartNumber = New Label()
        LabSerialNumber = New Label()
        LabStampNumber = New Label()
        LabInspectedBy = New Label()
        TxtCustomer = New TextBox()
        TxtVessel = New TextBox()
        TxtManufacturer = New TextBox()
        TxtPartNumber = New TextBox()
        TxtSerialNumber = New TextBox()
        TxtStampNumber = New TextBox()
        TxtInspectedBy = New TextBox()
        LabClass = New Label()
        LabRepairStatus = New Label()
        LabStyle = New Label()
        LabMaterial = New Label()
        LabBore = New Label()
        LabDAR = New Label()
        LabCup = New Label()
        TxtClass = New TextBox()
        TxtRepairStatus = New TextBox()
        TxtStyle = New TextBox()
        TxtMaterial = New TextBox()
        TxtBore = New TextBox()
        TxtDAR = New TextBox()
        TxtCup = New TextBox()
        LabScanDate = New Label()
        LabPerformedBy = New Label()
        LabMarkedPitch = New Label()
        LabWheelPitch = New Label()
        LabMeasuredDiameter = New Label()
        LabMarkedDiameter = New Label()
        LabRotation = New Label()
        TxtJobId = New TextBox()
        TxtJobNumber = New TextBox()
        PanelHeaderLayout.SuspendLayout()
        CType(JobDetailsBindingSource, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' PanelHeaderLayout
        ' 
        PanelHeaderLayout.ColumnCount = 6
        PanelHeaderLayout.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 15.0014F))
        PanelHeaderLayout.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 18.3319321F))
        PanelHeaderLayout.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 15.0014009F))
        PanelHeaderLayout.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 18.3319321F))
        PanelHeaderLayout.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 15.0014009F))
        PanelHeaderLayout.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 18.3319321F))
        PanelHeaderLayout.Controls.Add(TxtWheelPitch, 5, 7)
        PanelHeaderLayout.Controls.Add(TxtMarkedPitch, 5, 6)
        PanelHeaderLayout.Controls.Add(TxtMeasuredDiameter, 5, 5)
        PanelHeaderLayout.Controls.Add(TxtMarkedDiameter, 5, 4)
        PanelHeaderLayout.Controls.Add(TxtRotation, 5, 3)
        PanelHeaderLayout.Controls.Add(TxtPerformedBy, 5, 2)
        PanelHeaderLayout.Controls.Add(TxtScanDate, 5, 1)
        PanelHeaderLayout.Controls.Add(TxtFileName, 5, 0)
        PanelHeaderLayout.Controls.Add(LabFilename, 4, 0)
        PanelHeaderLayout.Controls.Add(LabJobId, 2, 0)
        PanelHeaderLayout.Controls.Add(LabJobNumber, 0, 0)
        PanelHeaderLayout.Controls.Add(LabCustomer, 0, 1)
        PanelHeaderLayout.Controls.Add(LabVessel, 0, 2)
        PanelHeaderLayout.Controls.Add(LabManufacturer, 0, 3)
        PanelHeaderLayout.Controls.Add(LabPartNumber, 0, 4)
        PanelHeaderLayout.Controls.Add(LabSerialNumber, 0, 5)
        PanelHeaderLayout.Controls.Add(LabStampNumber, 0, 6)
        PanelHeaderLayout.Controls.Add(LabInspectedBy, 0, 7)
        PanelHeaderLayout.Controls.Add(TxtCustomer, 1, 1)
        PanelHeaderLayout.Controls.Add(TxtVessel, 1, 2)
        PanelHeaderLayout.Controls.Add(TxtManufacturer, 1, 3)
        PanelHeaderLayout.Controls.Add(TxtPartNumber, 1, 4)
        PanelHeaderLayout.Controls.Add(TxtSerialNumber, 1, 5)
        PanelHeaderLayout.Controls.Add(TxtStampNumber, 1, 6)
        PanelHeaderLayout.Controls.Add(TxtInspectedBy, 1, 7)
        PanelHeaderLayout.Controls.Add(LabClass, 2, 1)
        PanelHeaderLayout.Controls.Add(LabRepairStatus, 2, 2)
        PanelHeaderLayout.Controls.Add(LabStyle, 2, 3)
        PanelHeaderLayout.Controls.Add(LabMaterial, 2, 4)
        PanelHeaderLayout.Controls.Add(LabBore, 2, 5)
        PanelHeaderLayout.Controls.Add(LabDAR, 2, 6)
        PanelHeaderLayout.Controls.Add(LabCup, 2, 7)
        PanelHeaderLayout.Controls.Add(TxtClass, 3, 1)
        PanelHeaderLayout.Controls.Add(TxtRepairStatus, 3, 2)
        PanelHeaderLayout.Controls.Add(TxtStyle, 3, 3)
        PanelHeaderLayout.Controls.Add(TxtMaterial, 3, 4)
        PanelHeaderLayout.Controls.Add(TxtBore, 3, 5)
        PanelHeaderLayout.Controls.Add(TxtDAR, 3, 6)
        PanelHeaderLayout.Controls.Add(TxtCup, 3, 7)
        PanelHeaderLayout.Controls.Add(LabScanDate, 4, 1)
        PanelHeaderLayout.Controls.Add(LabPerformedBy, 4, 2)
        PanelHeaderLayout.Controls.Add(LabMarkedPitch, 4, 6)
        PanelHeaderLayout.Controls.Add(LabWheelPitch, 4, 7)
        PanelHeaderLayout.Controls.Add(LabMeasuredDiameter, 4, 5)
        PanelHeaderLayout.Controls.Add(LabMarkedDiameter, 4, 4)
        PanelHeaderLayout.Controls.Add(LabRotation, 4, 3)
        PanelHeaderLayout.Controls.Add(TxtJobId, 3, 0)
        PanelHeaderLayout.Controls.Add(TxtJobNumber, 1, 0)
        PanelHeaderLayout.Dock = DockStyle.Fill
        PanelHeaderLayout.ForeColor = SystemColors.InactiveCaption
        PanelHeaderLayout.Location = New Point(0, 0)
        PanelHeaderLayout.Margin = New Padding(0)
        PanelHeaderLayout.Name = "PanelHeaderLayout"
        PanelHeaderLayout.RowCount = 8
        PanelHeaderLayout.RowStyles.Add(New RowStyle(SizeType.Percent, 12.5F))
        PanelHeaderLayout.RowStyles.Add(New RowStyle(SizeType.Percent, 12.5F))
        PanelHeaderLayout.RowStyles.Add(New RowStyle(SizeType.Percent, 12.5F))
        PanelHeaderLayout.RowStyles.Add(New RowStyle(SizeType.Percent, 12.5F))
        PanelHeaderLayout.RowStyles.Add(New RowStyle(SizeType.Percent, 12.5F))
        PanelHeaderLayout.RowStyles.Add(New RowStyle(SizeType.Percent, 12.5F))
        PanelHeaderLayout.RowStyles.Add(New RowStyle(SizeType.Percent, 12.5F))
        PanelHeaderLayout.RowStyles.Add(New RowStyle(SizeType.Percent, 12.5F))
        PanelHeaderLayout.Size = New Size(817, 177)
        PanelHeaderLayout.TabIndex = 29
        ' 
        ' TxtWheelPitch
        ' 
        TxtWheelPitch.BorderStyle = BorderStyle.None
        TxtWheelPitch.DataBindings.Add(New Binding("Text", JobDetailsBindingSource, "WheelPitch", True))
        TxtWheelPitch.Dock = DockStyle.Fill
        TxtWheelPitch.Location = New Point(667, 157)
        TxtWheelPitch.Name = "TxtWheelPitch"
        TxtWheelPitch.ReadOnly = True
        TxtWheelPitch.Size = New Size(147, 16)
        TxtWheelPitch.TabIndex = 49
        TxtWheelPitch.TabStop = False
        TxtWheelPitch.Tag = "WhlPit"
        ' 
        ' JobDetailsBindingSource
        ' 
        JobDetailsBindingSource.DataSource = GetType(LibDatabase.Models.HeaderView)
        ' 
        ' TxtMarkedPitch
        ' 
        TxtMarkedPitch.BorderStyle = BorderStyle.None
        TxtMarkedPitch.DataBindings.Add(New Binding("Text", JobDetailsBindingSource, "MarkedPitch", True))
        TxtMarkedPitch.Dock = DockStyle.Fill
        TxtMarkedPitch.Location = New Point(667, 135)
        TxtMarkedPitch.Name = "TxtMarkedPitch"
        TxtMarkedPitch.ReadOnly = True
        TxtMarkedPitch.Size = New Size(147, 16)
        TxtMarkedPitch.TabIndex = 48
        TxtMarkedPitch.TabStop = False
        TxtMarkedPitch.Tag = "MrkPit"
        TxtMarkedPitch.Visible = False
        ' 
        ' TxtMeasuredDiameter
        ' 
        TxtMeasuredDiameter.BorderStyle = BorderStyle.None
        TxtMeasuredDiameter.DataBindings.Add(New Binding("Text", JobDetailsBindingSource, "MeasuredDiameter", True))
        TxtMeasuredDiameter.Dock = DockStyle.Fill
        TxtMeasuredDiameter.Location = New Point(667, 113)
        TxtMeasuredDiameter.Name = "TxtMeasuredDiameter"
        TxtMeasuredDiameter.ReadOnly = True
        TxtMeasuredDiameter.Size = New Size(147, 16)
        TxtMeasuredDiameter.TabIndex = 47
        TxtMeasuredDiameter.TabStop = False
        TxtMeasuredDiameter.Tag = "MeasDia"
        TxtMeasuredDiameter.Visible = False
        ' 
        ' TxtMarkedDiameter
        ' 
        TxtMarkedDiameter.BorderStyle = BorderStyle.None
        TxtMarkedDiameter.DataBindings.Add(New Binding("Text", JobDetailsBindingSource, "MarkedDiameter", True))
        TxtMarkedDiameter.Dock = DockStyle.Fill
        TxtMarkedDiameter.Location = New Point(667, 91)
        TxtMarkedDiameter.Name = "TxtMarkedDiameter"
        TxtMarkedDiameter.ReadOnly = True
        TxtMarkedDiameter.Size = New Size(147, 16)
        TxtMarkedDiameter.TabIndex = 46
        TxtMarkedDiameter.TabStop = False
        TxtMarkedDiameter.Tag = "MrkDia"
        TxtMarkedDiameter.Visible = False
        ' 
        ' TxtRotation
        ' 
        TxtRotation.BorderStyle = BorderStyle.None
        TxtRotation.DataBindings.Add(New Binding("Text", JobDetailsBindingSource, "Rotation", True))
        TxtRotation.Dock = DockStyle.Fill
        TxtRotation.Location = New Point(667, 69)
        TxtRotation.Name = "TxtRotation"
        TxtRotation.ReadOnly = True
        TxtRotation.Size = New Size(147, 16)
        TxtRotation.TabIndex = 45
        TxtRotation.TabStop = False
        TxtRotation.Tag = "Rotn"
        TxtRotation.Visible = False
        ' 
        ' TxtPerformedBy
        ' 
        TxtPerformedBy.BorderStyle = BorderStyle.None
        TxtPerformedBy.DataBindings.Add(New Binding("Text", JobDetailsBindingSource, "PerformedByName", True))
        TxtPerformedBy.Dock = DockStyle.Fill
        TxtPerformedBy.Location = New Point(667, 47)
        TxtPerformedBy.Name = "TxtPerformedBy"
        TxtPerformedBy.ReadOnly = True
        TxtPerformedBy.Size = New Size(147, 16)
        TxtPerformedBy.TabIndex = 44
        TxtPerformedBy.TabStop = False
        TxtPerformedBy.Tag = "PerfBy"
        TxtPerformedBy.Visible = False
        ' 
        ' TxtScanDate
        ' 
        TxtScanDate.BorderStyle = BorderStyle.None
        TxtScanDate.DataBindings.Add(New Binding("Text", JobDetailsBindingSource, "ScanDate", True))
        TxtScanDate.Dock = DockStyle.Fill
        TxtScanDate.Location = New Point(667, 25)
        TxtScanDate.Name = "TxtScanDate"
        TxtScanDate.ReadOnly = True
        TxtScanDate.Size = New Size(147, 16)
        TxtScanDate.TabIndex = 43
        TxtScanDate.TabStop = False
        TxtScanDate.Tag = "Scan"
        TxtScanDate.Visible = False
        ' 
        ' TxtFileName
        ' 
        TxtFileName.BorderStyle = BorderStyle.None
        TxtFileName.DataBindings.Add(New Binding("Text", JobDetailsBindingSource, "FileName", True))
        TxtFileName.Dock = DockStyle.Fill
        TxtFileName.Location = New Point(667, 3)
        TxtFileName.Name = "TxtFileName"
        TxtFileName.ReadOnly = True
        TxtFileName.Size = New Size(147, 16)
        TxtFileName.TabIndex = 42
        TxtFileName.TabStop = False
        TxtFileName.Tag = "File"
        ' 
        ' LabFilename
        ' 
        LabFilename.Dock = DockStyle.Fill
        LabFilename.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        LabFilename.ForeColor = SystemColors.ControlText
        LabFilename.Location = New Point(545, 3)
        LabFilename.Margin = New Padding(3, 3, 3, 0)
        LabFilename.Name = "LabFilename"
        LabFilename.Size = New Size(116, 19)
        LabFilename.TabIndex = 34
        LabFilename.Tag = "TxtFileName"
        LabFilename.Text = "File Name"
        ' 
        ' LabJobId
        ' 
        LabJobId.Dock = DockStyle.Fill
        LabJobId.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        LabJobId.ForeColor = SystemColors.ControlText
        LabJobId.Location = New Point(274, 3)
        LabJobId.Margin = New Padding(3, 3, 3, 0)
        LabJobId.Name = "LabJobId"
        LabJobId.Size = New Size(116, 19)
        LabJobId.TabIndex = 18
        LabJobId.Tag = "TxtJobId"
        LabJobId.Text = "Job Id"
        ' 
        ' LabJobNumber
        ' 
        LabJobNumber.Dock = DockStyle.Fill
        LabJobNumber.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        LabJobNumber.ForeColor = SystemColors.ControlText
        LabJobNumber.Location = New Point(3, 3)
        LabJobNumber.Margin = New Padding(3, 3, 3, 0)
        LabJobNumber.Name = "LabJobNumber"
        LabJobNumber.Size = New Size(116, 19)
        LabJobNumber.TabIndex = 0
        LabJobNumber.Tag = "TxtJobNumber"
        LabJobNumber.Text = "Job No."
        ' 
        ' LabCustomer
        ' 
        LabCustomer.Dock = DockStyle.Fill
        LabCustomer.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        LabCustomer.ForeColor = SystemColors.ControlText
        LabCustomer.Location = New Point(3, 25)
        LabCustomer.Margin = New Padding(3, 3, 3, 0)
        LabCustomer.Name = "LabCustomer"
        LabCustomer.Size = New Size(116, 19)
        LabCustomer.TabIndex = 3
        LabCustomer.Tag = "TxtCustomer"
        LabCustomer.Text = "Customer"
        LabCustomer.Visible = False
        ' 
        ' LabVessel
        ' 
        LabVessel.Dock = DockStyle.Fill
        LabVessel.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        LabVessel.ForeColor = SystemColors.ControlText
        LabVessel.Location = New Point(3, 47)
        LabVessel.Margin = New Padding(3, 3, 3, 0)
        LabVessel.Name = "LabVessel"
        LabVessel.Size = New Size(116, 19)
        LabVessel.TabIndex = 4
        LabVessel.Tag = "TxtVessel"
        LabVessel.Text = "Vessel"
        LabVessel.Visible = False
        ' 
        ' LabManufacturer
        ' 
        LabManufacturer.Dock = DockStyle.Fill
        LabManufacturer.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        LabManufacturer.ForeColor = SystemColors.ControlText
        LabManufacturer.Location = New Point(3, 69)
        LabManufacturer.Margin = New Padding(3, 3, 3, 0)
        LabManufacturer.Name = "LabManufacturer"
        LabManufacturer.Size = New Size(116, 19)
        LabManufacturer.TabIndex = 5
        LabManufacturer.Tag = "TxtManufacturer"
        LabManufacturer.Text = "Manufacturer"
        LabManufacturer.Visible = False
        ' 
        ' LabPartNumber
        ' 
        LabPartNumber.Dock = DockStyle.Fill
        LabPartNumber.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        LabPartNumber.ForeColor = SystemColors.ControlText
        LabPartNumber.Location = New Point(3, 91)
        LabPartNumber.Margin = New Padding(3, 3, 3, 0)
        LabPartNumber.Name = "LabPartNumber"
        LabPartNumber.Size = New Size(116, 19)
        LabPartNumber.TabIndex = 6
        LabPartNumber.Tag = "TxtPartNumber"
        LabPartNumber.Text = "Part No."
        LabPartNumber.Visible = False
        ' 
        ' LabSerialNumber
        ' 
        LabSerialNumber.Dock = DockStyle.Fill
        LabSerialNumber.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        LabSerialNumber.ForeColor = SystemColors.ControlText
        LabSerialNumber.Location = New Point(3, 113)
        LabSerialNumber.Margin = New Padding(3, 3, 3, 0)
        LabSerialNumber.Name = "LabSerialNumber"
        LabSerialNumber.Size = New Size(116, 19)
        LabSerialNumber.TabIndex = 7
        LabSerialNumber.Tag = "TxtSerialNumber"
        LabSerialNumber.Text = "S/N"
        LabSerialNumber.Visible = False
        ' 
        ' LabStampNumber
        ' 
        LabStampNumber.Dock = DockStyle.Fill
        LabStampNumber.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        LabStampNumber.ForeColor = SystemColors.ControlText
        LabStampNumber.Location = New Point(3, 135)
        LabStampNumber.Margin = New Padding(3, 3, 3, 0)
        LabStampNumber.Name = "LabStampNumber"
        LabStampNumber.Size = New Size(116, 19)
        LabStampNumber.TabIndex = 8
        LabStampNumber.Tag = "TxtStampNumber"
        LabStampNumber.Text = "Stamp No."
        LabStampNumber.Visible = False
        ' 
        ' LabInspectedBy
        ' 
        LabInspectedBy.Dock = DockStyle.Fill
        LabInspectedBy.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        LabInspectedBy.ForeColor = SystemColors.ControlText
        LabInspectedBy.Location = New Point(3, 157)
        LabInspectedBy.Margin = New Padding(3, 3, 3, 0)
        LabInspectedBy.Name = "LabInspectedBy"
        LabInspectedBy.Size = New Size(116, 20)
        LabInspectedBy.TabIndex = 9
        LabInspectedBy.Tag = "TxtInspectedBy"
        LabInspectedBy.Text = "Inspected By"
        ' 
        ' TxtCustomer
        ' 
        TxtCustomer.BorderStyle = BorderStyle.None
        TxtCustomer.DataBindings.Add(New Binding("Text", JobDetailsBindingSource, "CustomerName", True))
        TxtCustomer.Dock = DockStyle.Fill
        TxtCustomer.Location = New Point(125, 25)
        TxtCustomer.Margin = New Padding(3, 3, 3, 0)
        TxtCustomer.Name = "TxtCustomer"
        TxtCustomer.ReadOnly = True
        TxtCustomer.Size = New Size(143, 16)
        TxtCustomer.TabIndex = 11
        TxtCustomer.TabStop = False
        TxtCustomer.Tag = "Cust"
        TxtCustomer.Visible = False
        ' 
        ' TxtVessel
        ' 
        TxtVessel.BorderStyle = BorderStyle.None
        TxtVessel.DataBindings.Add(New Binding("Text", JobDetailsBindingSource, "VesselName", True))
        TxtVessel.Dock = DockStyle.Fill
        TxtVessel.Location = New Point(125, 47)
        TxtVessel.Margin = New Padding(3, 3, 3, 0)
        TxtVessel.Name = "TxtVessel"
        TxtVessel.ReadOnly = True
        TxtVessel.Size = New Size(143, 16)
        TxtVessel.TabIndex = 12
        TxtVessel.TabStop = False
        TxtVessel.Tag = "Vess"
        TxtVessel.Visible = False
        ' 
        ' TxtManufacturer
        ' 
        TxtManufacturer.BorderStyle = BorderStyle.None
        TxtManufacturer.DataBindings.Add(New Binding("Text", JobDetailsBindingSource, "ManufacturerName", True))
        TxtManufacturer.Dock = DockStyle.Fill
        TxtManufacturer.Location = New Point(125, 69)
        TxtManufacturer.Margin = New Padding(3, 3, 3, 0)
        TxtManufacturer.Name = "TxtManufacturer"
        TxtManufacturer.ReadOnly = True
        TxtManufacturer.Size = New Size(143, 16)
        TxtManufacturer.TabIndex = 13
        TxtManufacturer.TabStop = False
        TxtManufacturer.Tag = "Mfg"
        TxtManufacturer.Visible = False
        ' 
        ' TxtPartNumber
        ' 
        TxtPartNumber.BorderStyle = BorderStyle.None
        TxtPartNumber.DataBindings.Add(New Binding("Text", JobDetailsBindingSource, "PartNumber", True))
        TxtPartNumber.Dock = DockStyle.Fill
        TxtPartNumber.Location = New Point(125, 91)
        TxtPartNumber.Margin = New Padding(3, 3, 3, 0)
        TxtPartNumber.Name = "TxtPartNumber"
        TxtPartNumber.ReadOnly = True
        TxtPartNumber.Size = New Size(143, 16)
        TxtPartNumber.TabIndex = 14
        TxtPartNumber.TabStop = False
        TxtPartNumber.Tag = "P/N"
        TxtPartNumber.Visible = False
        ' 
        ' TxtSerialNumber
        ' 
        TxtSerialNumber.BorderStyle = BorderStyle.None
        TxtSerialNumber.DataBindings.Add(New Binding("Text", JobDetailsBindingSource, "SerialNumber", True))
        TxtSerialNumber.Dock = DockStyle.Fill
        TxtSerialNumber.Location = New Point(125, 113)
        TxtSerialNumber.Margin = New Padding(3, 3, 3, 0)
        TxtSerialNumber.Name = "TxtSerialNumber"
        TxtSerialNumber.ReadOnly = True
        TxtSerialNumber.Size = New Size(143, 16)
        TxtSerialNumber.TabIndex = 15
        TxtSerialNumber.TabStop = False
        TxtSerialNumber.Tag = "S/N"
        TxtSerialNumber.Visible = False
        ' 
        ' TxtStampNumber
        ' 
        TxtStampNumber.BorderStyle = BorderStyle.None
        TxtStampNumber.DataBindings.Add(New Binding("Text", JobDetailsBindingSource, "StampNumber", True))
        TxtStampNumber.Dock = DockStyle.Fill
        TxtStampNumber.Location = New Point(125, 135)
        TxtStampNumber.Margin = New Padding(3, 3, 3, 0)
        TxtStampNumber.Name = "TxtStampNumber"
        TxtStampNumber.ReadOnly = True
        TxtStampNumber.Size = New Size(143, 16)
        TxtStampNumber.TabIndex = 16
        TxtStampNumber.TabStop = False
        TxtStampNumber.Tag = "Stamp"
        TxtStampNumber.Visible = False
        ' 
        ' TxtInspectedBy
        ' 
        TxtInspectedBy.BorderStyle = BorderStyle.None
        TxtInspectedBy.DataBindings.Add(New Binding("Text", JobDetailsBindingSource, "InspectedByName", True))
        TxtInspectedBy.Dock = DockStyle.Fill
        TxtInspectedBy.Location = New Point(125, 157)
        TxtInspectedBy.Margin = New Padding(3, 3, 3, 0)
        TxtInspectedBy.Name = "TxtInspectedBy"
        TxtInspectedBy.ReadOnly = True
        TxtInspectedBy.Size = New Size(143, 16)
        TxtInspectedBy.TabIndex = 17
        TxtInspectedBy.TabStop = False
        TxtInspectedBy.Tag = "InspBy"
        ' 
        ' LabClass
        ' 
        LabClass.Dock = DockStyle.Fill
        LabClass.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        LabClass.ForeColor = SystemColors.ControlText
        LabClass.Location = New Point(274, 25)
        LabClass.Margin = New Padding(3, 3, 3, 0)
        LabClass.Name = "LabClass"
        LabClass.Size = New Size(116, 19)
        LabClass.TabIndex = 19
        LabClass.Tag = "TxtClass"
        LabClass.Text = "Class"
        LabClass.Visible = False
        ' 
        ' LabRepairStatus
        ' 
        LabRepairStatus.Dock = DockStyle.Fill
        LabRepairStatus.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        LabRepairStatus.ForeColor = SystemColors.ControlText
        LabRepairStatus.Location = New Point(274, 47)
        LabRepairStatus.Margin = New Padding(3, 3, 3, 0)
        LabRepairStatus.Name = "LabRepairStatus"
        LabRepairStatus.Size = New Size(116, 19)
        LabRepairStatus.TabIndex = 20
        LabRepairStatus.Tag = "TxtRepairStatus"
        LabRepairStatus.Text = "Repair Status"
        LabRepairStatus.Visible = False
        ' 
        ' LabStyle
        ' 
        LabStyle.Dock = DockStyle.Fill
        LabStyle.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        LabStyle.ForeColor = SystemColors.ControlText
        LabStyle.Location = New Point(274, 69)
        LabStyle.Margin = New Padding(3, 3, 3, 0)
        LabStyle.Name = "LabStyle"
        LabStyle.Size = New Size(116, 19)
        LabStyle.TabIndex = 21
        LabStyle.Tag = "TxtStyle"
        LabStyle.Text = "Style"
        LabStyle.Visible = False
        ' 
        ' LabMaterial
        ' 
        LabMaterial.Dock = DockStyle.Fill
        LabMaterial.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        LabMaterial.ForeColor = SystemColors.ControlText
        LabMaterial.Location = New Point(274, 91)
        LabMaterial.Margin = New Padding(3, 3, 3, 0)
        LabMaterial.Name = "LabMaterial"
        LabMaterial.Size = New Size(116, 19)
        LabMaterial.TabIndex = 22
        LabMaterial.Tag = "TxtMaterial"
        LabMaterial.Text = "Material"
        LabMaterial.Visible = False
        ' 
        ' LabBore
        ' 
        LabBore.Dock = DockStyle.Fill
        LabBore.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        LabBore.ForeColor = SystemColors.ControlText
        LabBore.Location = New Point(274, 113)
        LabBore.Margin = New Padding(3, 3, 3, 0)
        LabBore.Name = "LabBore"
        LabBore.Size = New Size(116, 19)
        LabBore.TabIndex = 23
        LabBore.Tag = "TxtBore"
        LabBore.Text = "Bore"
        LabBore.Visible = False
        ' 
        ' LabDAR
        ' 
        LabDAR.Dock = DockStyle.Fill
        LabDAR.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        LabDAR.ForeColor = SystemColors.ControlText
        LabDAR.Location = New Point(274, 135)
        LabDAR.Margin = New Padding(3, 3, 3, 0)
        LabDAR.Name = "LabDAR"
        LabDAR.Size = New Size(116, 19)
        LabDAR.TabIndex = 24
        LabDAR.Tag = "TxtDAR"
        LabDAR.Text = "DAR"
        LabDAR.Visible = False
        ' 
        ' LabCup
        ' 
        LabCup.Dock = DockStyle.Fill
        LabCup.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        LabCup.ForeColor = SystemColors.ControlText
        LabCup.Location = New Point(274, 157)
        LabCup.Margin = New Padding(3, 3, 3, 0)
        LabCup.Name = "LabCup"
        LabCup.Size = New Size(116, 20)
        LabCup.TabIndex = 25
        LabCup.Tag = "TxtCup"
        LabCup.Text = "Cup"
        ' 
        ' TxtClass
        ' 
        TxtClass.BorderStyle = BorderStyle.None
        TxtClass.DataBindings.Add(New Binding("Text", JobDetailsBindingSource, "ToleranceClass", True))
        TxtClass.Dock = DockStyle.Fill
        TxtClass.Location = New Point(396, 25)
        TxtClass.Name = "TxtClass"
        TxtClass.ReadOnly = True
        TxtClass.Size = New Size(143, 16)
        TxtClass.TabIndex = 27
        TxtClass.TabStop = False
        TxtClass.Tag = "Cls"
        TxtClass.Visible = False
        ' 
        ' TxtRepairStatus
        ' 
        TxtRepairStatus.BorderStyle = BorderStyle.None
        TxtRepairStatus.DataBindings.Add(New Binding("Text", JobDetailsBindingSource, "RepairStatus", True))
        TxtRepairStatus.Dock = DockStyle.Fill
        TxtRepairStatus.Location = New Point(396, 47)
        TxtRepairStatus.Name = "TxtRepairStatus"
        TxtRepairStatus.ReadOnly = True
        TxtRepairStatus.Size = New Size(143, 16)
        TxtRepairStatus.TabIndex = 28
        TxtRepairStatus.TabStop = False
        TxtRepairStatus.Tag = "RStat"
        TxtRepairStatus.Visible = False
        ' 
        ' TxtStyle
        ' 
        TxtStyle.BorderStyle = BorderStyle.None
        TxtStyle.DataBindings.Add(New Binding("Text", JobDetailsBindingSource, "Style", True))
        TxtStyle.Dock = DockStyle.Fill
        TxtStyle.Location = New Point(396, 69)
        TxtStyle.Name = "TxtStyle"
        TxtStyle.ReadOnly = True
        TxtStyle.Size = New Size(143, 16)
        TxtStyle.TabIndex = 29
        TxtStyle.TabStop = False
        TxtStyle.Tag = "Style"
        TxtStyle.Visible = False
        ' 
        ' TxtMaterial
        ' 
        TxtMaterial.BorderStyle = BorderStyle.None
        TxtMaterial.DataBindings.Add(New Binding("Text", JobDetailsBindingSource, "Material", True))
        TxtMaterial.Dock = DockStyle.Fill
        TxtMaterial.Location = New Point(396, 91)
        TxtMaterial.Name = "TxtMaterial"
        TxtMaterial.ReadOnly = True
        TxtMaterial.Size = New Size(143, 16)
        TxtMaterial.TabIndex = 30
        TxtMaterial.TabStop = False
        TxtMaterial.Tag = "Matl"
        TxtMaterial.Visible = False
        ' 
        ' TxtBore
        ' 
        TxtBore.BorderStyle = BorderStyle.None
        TxtBore.DataBindings.Add(New Binding("Text", JobDetailsBindingSource, "Bore", True))
        TxtBore.Dock = DockStyle.Fill
        TxtBore.Location = New Point(396, 113)
        TxtBore.Name = "TxtBore"
        TxtBore.ReadOnly = True
        TxtBore.Size = New Size(143, 16)
        TxtBore.TabIndex = 31
        TxtBore.TabStop = False
        TxtBore.Tag = "Bore"
        TxtBore.Visible = False
        ' 
        ' TxtDAR
        ' 
        TxtDAR.BorderStyle = BorderStyle.None
        TxtDAR.DataBindings.Add(New Binding("Text", JobDetailsBindingSource, "Dar", True))
        TxtDAR.Dock = DockStyle.Fill
        TxtDAR.Location = New Point(396, 135)
        TxtDAR.Name = "TxtDAR"
        TxtDAR.ReadOnly = True
        TxtDAR.Size = New Size(143, 16)
        TxtDAR.TabIndex = 32
        TxtDAR.TabStop = False
        TxtDAR.Tag = "DAR"
        TxtDAR.Visible = False
        ' 
        ' TxtCup
        ' 
        TxtCup.BorderStyle = BorderStyle.None
        TxtCup.DataBindings.Add(New Binding("Text", JobDetailsBindingSource, "Cup", True))
        TxtCup.Dock = DockStyle.Fill
        TxtCup.Location = New Point(396, 157)
        TxtCup.Name = "TxtCup"
        TxtCup.ReadOnly = True
        TxtCup.Size = New Size(143, 16)
        TxtCup.TabIndex = 33
        TxtCup.TabStop = False
        TxtCup.Tag = "Cup"
        ' 
        ' LabScanDate
        ' 
        LabScanDate.Dock = DockStyle.Fill
        LabScanDate.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        LabScanDate.ForeColor = SystemColors.ControlText
        LabScanDate.Location = New Point(545, 25)
        LabScanDate.Margin = New Padding(3, 3, 3, 0)
        LabScanDate.Name = "LabScanDate"
        LabScanDate.Size = New Size(116, 19)
        LabScanDate.TabIndex = 35
        LabScanDate.Tag = "TxtScanDate"
        LabScanDate.Text = "Scan Date"
        LabScanDate.Visible = False
        ' 
        ' LabPerformedBy
        ' 
        LabPerformedBy.Dock = DockStyle.Fill
        LabPerformedBy.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        LabPerformedBy.ForeColor = SystemColors.ControlText
        LabPerformedBy.Location = New Point(545, 47)
        LabPerformedBy.Margin = New Padding(3, 3, 3, 0)
        LabPerformedBy.Name = "LabPerformedBy"
        LabPerformedBy.Size = New Size(116, 19)
        LabPerformedBy.TabIndex = 36
        LabPerformedBy.Tag = "TxtPerformedBy"
        LabPerformedBy.Text = "Performed By"
        LabPerformedBy.Visible = False
        ' 
        ' LabMarkedPitch
        ' 
        LabMarkedPitch.Dock = DockStyle.Fill
        LabMarkedPitch.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        LabMarkedPitch.ForeColor = SystemColors.ControlText
        LabMarkedPitch.Location = New Point(545, 135)
        LabMarkedPitch.Margin = New Padding(3, 3, 3, 0)
        LabMarkedPitch.Name = "LabMarkedPitch"
        LabMarkedPitch.Size = New Size(116, 19)
        LabMarkedPitch.TabIndex = 40
        LabMarkedPitch.Tag = "TxtMarkedPitch"
        LabMarkedPitch.Text = "Marked Pitch"
        LabMarkedPitch.Visible = False
        ' 
        ' LabWheelPitch
        ' 
        LabWheelPitch.Dock = DockStyle.Fill
        LabWheelPitch.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        LabWheelPitch.ForeColor = SystemColors.ControlText
        LabWheelPitch.Location = New Point(545, 157)
        LabWheelPitch.Margin = New Padding(3, 3, 3, 0)
        LabWheelPitch.Name = "LabWheelPitch"
        LabWheelPitch.Size = New Size(116, 20)
        LabWheelPitch.TabIndex = 41
        LabWheelPitch.Tag = "TxtWheelPitch"
        LabWheelPitch.Text = "Wheel Pitch"
        ' 
        ' LabMeasuredDiameter
        ' 
        LabMeasuredDiameter.Dock = DockStyle.Fill
        LabMeasuredDiameter.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        LabMeasuredDiameter.ForeColor = SystemColors.ControlText
        LabMeasuredDiameter.Location = New Point(545, 113)
        LabMeasuredDiameter.Margin = New Padding(3, 3, 3, 0)
        LabMeasuredDiameter.Name = "LabMeasuredDiameter"
        LabMeasuredDiameter.Size = New Size(116, 19)
        LabMeasuredDiameter.TabIndex = 38
        LabMeasuredDiameter.Tag = "TxtMeasuredDiameter"
        LabMeasuredDiameter.Text = "Measured Dia"
        LabMeasuredDiameter.Visible = False
        ' 
        ' LabMarkedDiameter
        ' 
        LabMarkedDiameter.Dock = DockStyle.Fill
        LabMarkedDiameter.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        LabMarkedDiameter.ForeColor = SystemColors.ControlText
        LabMarkedDiameter.Location = New Point(545, 91)
        LabMarkedDiameter.Margin = New Padding(3, 3, 3, 0)
        LabMarkedDiameter.Name = "LabMarkedDiameter"
        LabMarkedDiameter.Size = New Size(116, 19)
        LabMarkedDiameter.TabIndex = 37
        LabMarkedDiameter.Tag = "TxtMarkedDiameter"
        LabMarkedDiameter.Text = "Marked Dia"
        LabMarkedDiameter.Visible = False
        ' 
        ' LabRotation
        ' 
        LabRotation.Dock = DockStyle.Fill
        LabRotation.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        LabRotation.ForeColor = SystemColors.ControlText
        LabRotation.Location = New Point(545, 69)
        LabRotation.Margin = New Padding(3, 3, 3, 0)
        LabRotation.Name = "LabRotation"
        LabRotation.Size = New Size(116, 19)
        LabRotation.TabIndex = 39
        LabRotation.Tag = "TxtRotation"
        LabRotation.Text = "Rotation"
        LabRotation.Visible = False
        ' 
        ' TxtJobId
        ' 
        TxtJobId.BorderStyle = BorderStyle.None
        TxtJobId.DataBindings.Add(New Binding("Text", JobDetailsBindingSource, "Id", True))
        TxtJobId.Dock = DockStyle.Fill
        TxtJobId.Location = New Point(396, 3)
        TxtJobId.Name = "TxtJobId"
        TxtJobId.ReadOnly = True
        TxtJobId.Size = New Size(143, 16)
        TxtJobId.TabIndex = 26
        TxtJobId.TabStop = False
        TxtJobId.Tag = "JobId"
        ' 
        ' TxtJobNumber
        ' 
        TxtJobNumber.BorderStyle = BorderStyle.None
        TxtJobNumber.DataBindings.Add(New Binding("Text", JobDetailsBindingSource, "JobNumber", True))
        TxtJobNumber.Dock = DockStyle.Fill
        TxtJobNumber.Location = New Point(125, 3)
        TxtJobNumber.Margin = New Padding(3, 3, 3, 0)
        TxtJobNumber.Name = "TxtJobNumber"
        TxtJobNumber.ReadOnly = True
        TxtJobNumber.Size = New Size(143, 16)
        TxtJobNumber.TabIndex = 10
        TxtJobNumber.TabStop = False
        TxtJobNumber.Tag = "JobNo"
        ' 
        ' ReportHeader
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(PanelHeaderLayout)
        Margin = New Padding(0)
        Name = "ReportHeader"
        Size = New Size(817, 177)
        PanelHeaderLayout.ResumeLayout(False)
        PanelHeaderLayout.PerformLayout()
        CType(JobDetailsBindingSource, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents PanelHeaderLayout As TableLayoutPanel
    Friend WithEvents TxtWheelPitch As TextBox
    Friend WithEvents TxtMarkedPitch As TextBox
    Friend WithEvents TxtMeasuredDiameter As TextBox
    Friend WithEvents TxtMarkedDiameter As TextBox
    Friend WithEvents TxtRotation As TextBox
    Friend WithEvents TxtPerformedBy As TextBox
    Friend WithEvents TxtScanDate As TextBox
    Friend WithEvents TxtFileName As TextBox
    Friend WithEvents LabFilename As Label
    Friend WithEvents LabJobId As Label
    Friend WithEvents LabJobNumber As Label
    Friend WithEvents LabCustomer As Label
    Friend WithEvents LabVessel As Label
    Friend WithEvents LabManufacturer As Label
    Friend WithEvents LabPartNumber As Label
    Friend WithEvents LabSerialNumber As Label
    Friend WithEvents LabStampNumber As Label
    Friend WithEvents LabInspectedBy As Label
    Friend WithEvents TxtCustomer As TextBox
    Friend WithEvents TxtVessel As TextBox
    Friend WithEvents TxtManufacturer As TextBox
    Friend WithEvents TxtPartNumber As TextBox
    Friend WithEvents TxtSerialNumber As TextBox
    Friend WithEvents TxtStampNumber As TextBox
    Friend WithEvents TxtInspectedBy As TextBox
    Friend WithEvents LabClass As Label
    Friend WithEvents LabRepairStatus As Label
    Friend WithEvents LabStyle As Label
    Friend WithEvents LabMaterial As Label
    Friend WithEvents LabBore As Label
    Friend WithEvents LabDAR As Label
    Friend WithEvents LabCup As Label
    Friend WithEvents TxtClass As TextBox
    Friend WithEvents TxtRepairStatus As TextBox
    Friend WithEvents TxtStyle As TextBox
    Friend WithEvents TxtMaterial As TextBox
    Friend WithEvents TxtBore As TextBox
    Friend WithEvents TxtDAR As TextBox
    Friend WithEvents TxtCup As TextBox
    Friend WithEvents LabScanDate As Label
    Friend WithEvents LabPerformedBy As Label
    Friend WithEvents LabMarkedPitch As Label
    Friend WithEvents LabWheelPitch As Label
    Friend WithEvents LabMeasuredDiameter As Label
    Friend WithEvents LabMarkedDiameter As Label
    Friend WithEvents LabRotation As Label
    Friend WithEvents TxtJobId As TextBox
    Friend WithEvents TxtJobNumber As TextBox
    Friend WithEvents JobDetailsBindingSource As BindingSource

End Class
