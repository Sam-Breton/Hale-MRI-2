Imports LibDatabase
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmComparison
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmComparison))
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        tLayoutComparison = New TableLayoutPanel()
        PictLogo = New PictureBox()
        DataGridJobDetails = New DataGridView()
        StartDateDataGridViewTextBoxColumn = New DataGridViewTextBoxColumn()
        MeasurementTypeIdDataGridViewTextBoxColumn = New DataGridViewComboBoxColumn()
        MeasurementTypesBindingSource = New BindingSource(components)
        ToleranceClassDataGridViewTextBoxColumn = New DataGridViewComboBoxColumn()
        ToleranceBindingSource = New BindingSource(components)
        PerformedByDataGridViewTextBoxColumn = New DataGridViewComboBoxColumn()
        EmployeeBindingSource = New BindingSource(components)
        DescriptionDataGridViewTextBoxColumn = New DataGridViewTextBoxColumn()
        JobDetailsBindingSource = New BindingSource(components)
        tlayoutComparisonControls = New TableLayoutPanel()
        ComboRadiusorBlade = New ComboBox()
        ChkExamineoneBlade = New CheckBox()
        ChkSpline = New CheckBox()
        ChkShowTrack = New CheckBox()
        ChkGraphEntireScan = New CheckBox()
        ChkKeepforComp = New CheckBox()
        LabRefPitch = New Label()
        LabSegments = New Label()
        LabRadiusorBlade = New Label()
        LabTrackRefBlade = New Label()
        TxtRefPitch = New TextBox()
        ChkCenterRef = New CheckBox()
        ComboTrackRefBlade = New ComboBox()
        CmdSelectProgression = New Button()
        CmdPrintAllGraphs = New Button()
        LblAxesScaling = New Label()
        CBoxAxesScaling = New ComboBox()
        LblFont = New Label()
        TrackFont = New TrackBar()
        TrackSegments = New TrackBar()
        RecordNavigationBar1 = New RecordNavigationBar()
        TLayoutNavigation = New TableLayoutPanel()
        CmdComparison = New Button()
        CmdInspect = New Button()
        CmdGraph = New Button()
        CmdLocalPitch = New Button()
        CmdMeasure = New Button()
        PanelCompCharts = New Panel()
        TLayoutCompCharts = New TableLayoutPanel()
        tLayoutComparison.SuspendLayout()
        CType(PictLogo, ComponentModel.ISupportInitialize).BeginInit()
        CType(DataGridJobDetails, ComponentModel.ISupportInitialize).BeginInit()
        CType(MeasurementTypesBindingSource, ComponentModel.ISupportInitialize).BeginInit()
        CType(ToleranceBindingSource, ComponentModel.ISupportInitialize).BeginInit()
        CType(EmployeeBindingSource, ComponentModel.ISupportInitialize).BeginInit()
        CType(JobDetailsBindingSource, ComponentModel.ISupportInitialize).BeginInit()
        tlayoutComparisonControls.SuspendLayout()
        CType(TrackFont, ComponentModel.ISupportInitialize).BeginInit()
        CType(TrackSegments, ComponentModel.ISupportInitialize).BeginInit()
        TLayoutNavigation.SuspendLayout()
        PanelCompCharts.SuspendLayout()
        SuspendLayout()
        ' 
        ' tLayoutComparison
        ' 
        tLayoutComparison.ColumnCount = 6
        tLayoutComparison.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 215F))
        tLayoutComparison.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
        tLayoutComparison.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
        tLayoutComparison.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
        tLayoutComparison.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
        tLayoutComparison.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
        tLayoutComparison.Controls.Add(PictLogo, 0, 0)
        tLayoutComparison.Controls.Add(DataGridJobDetails, 3, 1)
        tLayoutComparison.Controls.Add(tlayoutComparisonControls, 0, 2)
        tLayoutComparison.Controls.Add(RecordNavigationBar1, 2, 0)
        tLayoutComparison.Controls.Add(TLayoutNavigation, 1, 1)
        tLayoutComparison.Controls.Add(PanelCompCharts, 1, 2)
        tLayoutComparison.Dock = DockStyle.Fill
        tLayoutComparison.Location = New Point(0, 0)
        tLayoutComparison.Name = "tLayoutComparison"
        tLayoutComparison.RowCount = 3
        tLayoutComparison.RowStyles.Add(New RowStyle(SizeType.Absolute, 36F))
        tLayoutComparison.RowStyles.Add(New RowStyle(SizeType.Absolute, 75F))
        tLayoutComparison.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tLayoutComparison.Size = New Size(1443, 751)
        tLayoutComparison.TabIndex = 0
        ' 
        ' PictLogo
        ' 
        PictLogo.Dock = DockStyle.Fill
        PictLogo.Image = CType(resources.GetObject("PictLogo.Image"), Image)
        PictLogo.Location = New Point(1, 0)
        PictLogo.Margin = New Padding(1, 0, 0, 0)
        PictLogo.Name = "PictLogo"
        tLayoutComparison.SetRowSpan(PictLogo, 2)
        PictLogo.Size = New Size(214, 111)
        PictLogo.SizeMode = PictureBoxSizeMode.StretchImage
        PictLogo.TabIndex = 0
        PictLogo.TabStop = False
        ' 
        ' DataGridJobDetails
        ' 
        DataGridJobDetails.AllowUserToAddRows = False
        DataGridJobDetails.AllowUserToDeleteRows = False
        DataGridJobDetails.AutoGenerateColumns = False
        DataGridJobDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        DataGridJobDetails.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridJobDetails.Columns.AddRange(New DataGridViewColumn() {StartDateDataGridViewTextBoxColumn, MeasurementTypeIdDataGridViewTextBoxColumn, ToleranceClassDataGridViewTextBoxColumn, PerformedByDataGridViewTextBoxColumn, DescriptionDataGridViewTextBoxColumn})
        tLayoutComparison.SetColumnSpan(DataGridJobDetails, 3)
        DataGridJobDetails.DataSource = JobDetailsBindingSource
        DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = SystemColors.Window
        DataGridViewCellStyle1.Font = New Font("Segoe UI", 13F)
        DataGridViewCellStyle1.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = DataGridViewTriState.False
        DataGridJobDetails.DefaultCellStyle = DataGridViewCellStyle1
        DataGridJobDetails.Dock = DockStyle.Fill
        DataGridJobDetails.Location = New Point(709, 40)
        DataGridJobDetails.Margin = New Padding(4, 4, 15, 0)
        DataGridJobDetails.Name = "DataGridJobDetails"
        DataGridJobDetails.RowHeadersVisible = False
        DataGridViewCellStyle2.BackColor = SystemColors.Control
        DataGridViewCellStyle2.Font = New Font("Segoe UI", 13F)
        DataGridViewCellStyle2.ForeColor = Color.Black
        DataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = Color.Black
        DataGridJobDetails.RowsDefaultCellStyle = DataGridViewCellStyle2
        DataGridJobDetails.ScrollBars = ScrollBars.None
        DataGridJobDetails.Size = New Size(719, 71)
        DataGridJobDetails.TabIndex = 1
        ' 
        ' StartDateDataGridViewTextBoxColumn
        ' 
        StartDateDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        StartDateDataGridViewTextBoxColumn.DataPropertyName = "StartDate"
        StartDateDataGridViewTextBoxColumn.HeaderText = "Start Date"
        StartDateDataGridViewTextBoxColumn.Name = "StartDateDataGridViewTextBoxColumn"
        StartDateDataGridViewTextBoxColumn.Width = 115
        ' 
        ' MeasurementTypeIdDataGridViewTextBoxColumn
        ' 
        MeasurementTypeIdDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        MeasurementTypeIdDataGridViewTextBoxColumn.DataPropertyName = "MeasurementTypeId"
        MeasurementTypeIdDataGridViewTextBoxColumn.DataSource = MeasurementTypesBindingSource
        MeasurementTypeIdDataGridViewTextBoxColumn.DisplayMember = "MeasurementType1"
        MeasurementTypeIdDataGridViewTextBoxColumn.HeaderText = "Stage"
        MeasurementTypeIdDataGridViewTextBoxColumn.Name = "MeasurementTypeIdDataGridViewTextBoxColumn"
        MeasurementTypeIdDataGridViewTextBoxColumn.Resizable = DataGridViewTriState.True
        MeasurementTypeIdDataGridViewTextBoxColumn.SortMode = DataGridViewColumnSortMode.Automatic
        MeasurementTypeIdDataGridViewTextBoxColumn.ValueMember = "Id"
        MeasurementTypeIdDataGridViewTextBoxColumn.Width = 81
        ' 
        ' MeasurementTypesBindingSource
        ' 
        MeasurementTypesBindingSource.DataSource = GetType(Models.MeasurementType)
        ' 
        ' ToleranceClassDataGridViewTextBoxColumn
        ' 
        ToleranceClassDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        ToleranceClassDataGridViewTextBoxColumn.DataPropertyName = "ToleranceClass"
        ToleranceClassDataGridViewTextBoxColumn.DataSource = ToleranceBindingSource
        ToleranceClassDataGridViewTextBoxColumn.DisplayMember = "ToleranceClass"
        ToleranceClassDataGridViewTextBoxColumn.HeaderText = "Class"
        ToleranceClassDataGridViewTextBoxColumn.Name = "ToleranceClassDataGridViewTextBoxColumn"
        ToleranceClassDataGridViewTextBoxColumn.Resizable = DataGridViewTriState.True
        ToleranceClassDataGridViewTextBoxColumn.SortMode = DataGridViewColumnSortMode.Automatic
        ToleranceClassDataGridViewTextBoxColumn.ValueMember = "ToleranceClass"
        ToleranceClassDataGridViewTextBoxColumn.Width = 77
        ' 
        ' ToleranceBindingSource
        ' 
        ToleranceBindingSource.DataSource = GetType(Models.Tolerance)
        ' 
        ' PerformedByDataGridViewTextBoxColumn
        ' 
        PerformedByDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        PerformedByDataGridViewTextBoxColumn.DataPropertyName = "PerformedBy"
        PerformedByDataGridViewTextBoxColumn.DataSource = EmployeeBindingSource
        PerformedByDataGridViewTextBoxColumn.DisplayMember = "EmployeeName"
        PerformedByDataGridViewTextBoxColumn.HeaderText = "Employee"
        PerformedByDataGridViewTextBoxColumn.Name = "PerformedByDataGridViewTextBoxColumn"
        PerformedByDataGridViewTextBoxColumn.Resizable = DataGridViewTriState.True
        PerformedByDataGridViewTextBoxColumn.SortMode = DataGridViewColumnSortMode.Automatic
        PerformedByDataGridViewTextBoxColumn.ValueMember = "Id"
        PerformedByDataGridViewTextBoxColumn.Width = 115
        ' 
        ' EmployeeBindingSource
        ' 
        EmployeeBindingSource.DataSource = GetType(Models.Employee)
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
        ' tlayoutComparisonControls
        ' 
        tlayoutComparisonControls.ColumnCount = 1
        tlayoutComparisonControls.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tlayoutComparisonControls.Controls.Add(ComboRadiusorBlade, 0, 10)
        tlayoutComparisonControls.Controls.Add(ChkExamineoneBlade, 0, 6)
        tlayoutComparisonControls.Controls.Add(ChkSpline, 0, 15)
        tlayoutComparisonControls.Controls.Add(ChkShowTrack, 0, 5)
        tlayoutComparisonControls.Controls.Add(ChkGraphEntireScan, 0, 4)
        tlayoutComparisonControls.Controls.Add(ChkKeepforComp, 0, 3)
        tlayoutComparisonControls.Controls.Add(LabRefPitch, 0, 0)
        tlayoutComparisonControls.Controls.Add(LabSegments, 0, 11)
        tlayoutComparisonControls.Controls.Add(LabRadiusorBlade, 0, 9)
        tlayoutComparisonControls.Controls.Add(LabTrackRefBlade, 0, 7)
        tlayoutComparisonControls.Controls.Add(TxtRefPitch, 0, 1)
        tlayoutComparisonControls.Controls.Add(ChkCenterRef, 0, 2)
        tlayoutComparisonControls.Controls.Add(ComboTrackRefBlade, 0, 8)
        tlayoutComparisonControls.Controls.Add(CmdSelectProgression, 0, 13)
        tlayoutComparisonControls.Controls.Add(CmdPrintAllGraphs, 0, 14)
        tlayoutComparisonControls.Controls.Add(LblAxesScaling, 0, 16)
        tlayoutComparisonControls.Controls.Add(CBoxAxesScaling, 0, 17)
        tlayoutComparisonControls.Controls.Add(LblFont, 0, 18)
        tlayoutComparisonControls.Controls.Add(TrackFont, 0, 19)
        tlayoutComparisonControls.Controls.Add(TrackSegments, 0, 12)
        tlayoutComparisonControls.Dock = DockStyle.Fill
        tlayoutComparisonControls.Location = New Point(5, 111)
        tlayoutComparisonControls.Margin = New Padding(5, 0, 0, 0)
        tlayoutComparisonControls.Name = "tlayoutComparisonControls"
        tlayoutComparisonControls.RowCount = 21
        tlayoutComparisonControls.RowStyles.Add(New RowStyle())
        tlayoutComparisonControls.RowStyles.Add(New RowStyle())
        tlayoutComparisonControls.RowStyles.Add(New RowStyle())
        tlayoutComparisonControls.RowStyles.Add(New RowStyle())
        tlayoutComparisonControls.RowStyles.Add(New RowStyle())
        tlayoutComparisonControls.RowStyles.Add(New RowStyle())
        tlayoutComparisonControls.RowStyles.Add(New RowStyle())
        tlayoutComparisonControls.RowStyles.Add(New RowStyle())
        tlayoutComparisonControls.RowStyles.Add(New RowStyle())
        tlayoutComparisonControls.RowStyles.Add(New RowStyle())
        tlayoutComparisonControls.RowStyles.Add(New RowStyle())
        tlayoutComparisonControls.RowStyles.Add(New RowStyle())
        tlayoutComparisonControls.RowStyles.Add(New RowStyle())
        tlayoutComparisonControls.RowStyles.Add(New RowStyle())
        tlayoutComparisonControls.RowStyles.Add(New RowStyle())
        tlayoutComparisonControls.RowStyles.Add(New RowStyle())
        tlayoutComparisonControls.RowStyles.Add(New RowStyle())
        tlayoutComparisonControls.RowStyles.Add(New RowStyle())
        tlayoutComparisonControls.RowStyles.Add(New RowStyle())
        tlayoutComparisonControls.RowStyles.Add(New RowStyle())
        tlayoutComparisonControls.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tlayoutComparisonControls.Size = New Size(210, 640)
        tlayoutComparisonControls.TabIndex = 3
        ' 
        ' ComboRadiusorBlade
        ' 
        ComboRadiusorBlade.Dock = DockStyle.Top
        ComboRadiusorBlade.FormattingEnabled = True
        ComboRadiusorBlade.Location = New Point(3, 318)
        ComboRadiusorBlade.Margin = New Padding(3, 0, 3, 0)
        ComboRadiusorBlade.Name = "ComboRadiusorBlade"
        ComboRadiusorBlade.Size = New Size(204, 31)
        ComboRadiusorBlade.TabIndex = 18
        ' 
        ' ChkExamineoneBlade
        ' 
        ChkExamineoneBlade.AutoSize = True
        ChkExamineoneBlade.Dock = DockStyle.Left
        ChkExamineoneBlade.Location = New Point(10, 205)
        ChkExamineoneBlade.Margin = New Padding(10, 3, 3, 3)
        ChkExamineoneBlade.Name = "ChkExamineoneBlade"
        ChkExamineoneBlade.Size = New Size(179, 29)
        ChkExamineoneBlade.TabIndex = 16
        ChkExamineoneBlade.Text = "Examine one Blade"
        ChkExamineoneBlade.UseVisualStyleBackColor = True
        ' 
        ' ChkSpline
        ' 
        ChkSpline.AutoSize = True
        ChkSpline.Dock = DockStyle.Left
        ChkSpline.Location = New Point(10, 498)
        ChkSpline.Margin = New Padding(10, 3, 3, 3)
        ChkSpline.Name = "ChkSpline"
        ChkSpline.Size = New Size(79, 29)
        ChkSpline.TabIndex = 15
        ChkSpline.Text = "Spline"
        ChkSpline.UseVisualStyleBackColor = True
        ' 
        ' ChkShowTrack
        ' 
        ChkShowTrack.AutoSize = True
        ChkShowTrack.Dock = DockStyle.Left
        ChkShowTrack.Location = New Point(10, 170)
        ChkShowTrack.Margin = New Padding(10, 3, 3, 3)
        ChkShowTrack.Name = "ChkShowTrack"
        ChkShowTrack.Size = New Size(119, 29)
        ChkShowTrack.TabIndex = 8
        ChkShowTrack.Text = "Show Track"
        ChkShowTrack.UseVisualStyleBackColor = True
        ' 
        ' ChkGraphEntireScan
        ' 
        ChkGraphEntireScan.AutoSize = True
        ChkGraphEntireScan.Dock = DockStyle.Left
        ChkGraphEntireScan.Location = New Point(10, 135)
        ChkGraphEntireScan.Margin = New Padding(10, 3, 3, 3)
        ChkGraphEntireScan.Name = "ChkGraphEntireScan"
        ChkGraphEntireScan.Size = New Size(170, 29)
        ChkGraphEntireScan.TabIndex = 7
        ChkGraphEntireScan.Text = "Graph Entire Scan"
        ChkGraphEntireScan.UseVisualStyleBackColor = True
        ' 
        ' ChkKeepforComp
        ' 
        ChkKeepforComp.AutoSize = True
        ChkKeepforComp.Dock = DockStyle.Left
        ChkKeepforComp.Location = New Point(10, 100)
        ChkKeepforComp.Margin = New Padding(10, 3, 3, 3)
        ChkKeepforComp.Name = "ChkKeepforComp"
        ChkKeepforComp.Size = New Size(197, 29)
        ChkKeepforComp.TabIndex = 6
        ChkKeepforComp.Text = "Keep for Comparison"
        ChkKeepforComp.UseVisualStyleBackColor = True
        ' 
        ' LabRefPitch
        ' 
        LabRefPitch.AutoSize = True
        LabRefPitch.Dock = DockStyle.Bottom
        LabRefPitch.Location = New Point(3, 0)
        LabRefPitch.Name = "LabRefPitch"
        LabRefPitch.Size = New Size(204, 25)
        LabRefPitch.TabIndex = 0
        LabRefPitch.Text = "Ref Pitch"
        ' 
        ' LabSegments
        ' 
        LabSegments.AutoSize = True
        LabSegments.Dock = DockStyle.Bottom
        LabSegments.Location = New Point(3, 349)
        LabSegments.Name = "LabSegments"
        LabSegments.Size = New Size(204, 25)
        LabSegments.TabIndex = 3
        LabSegments.Text = "Segments"
        ' 
        ' LabRadiusorBlade
        ' 
        LabRadiusorBlade.AutoSize = True
        LabRadiusorBlade.Dock = DockStyle.Bottom
        LabRadiusorBlade.Location = New Point(3, 293)
        LabRadiusorBlade.Name = "LabRadiusorBlade"
        LabRadiusorBlade.Size = New Size(204, 25)
        LabRadiusorBlade.TabIndex = 2
        LabRadiusorBlade.Text = "Radius"
        ' 
        ' LabTrackRefBlade
        ' 
        LabTrackRefBlade.AutoSize = True
        LabTrackRefBlade.Dock = DockStyle.Bottom
        LabTrackRefBlade.Location = New Point(3, 237)
        LabTrackRefBlade.Name = "LabTrackRefBlade"
        LabTrackRefBlade.Size = New Size(204, 25)
        LabTrackRefBlade.TabIndex = 1
        LabTrackRefBlade.Text = "Track Ref Blade"
        ' 
        ' TxtRefPitch
        ' 
        TxtRefPitch.Dock = DockStyle.Top
        TxtRefPitch.Location = New Point(3, 28)
        TxtRefPitch.Name = "TxtRefPitch"
        TxtRefPitch.Size = New Size(204, 31)
        TxtRefPitch.TabIndex = 4
        ' 
        ' ChkCenterRef
        ' 
        ChkCenterRef.AutoSize = True
        ChkCenterRef.Dock = DockStyle.Left
        ChkCenterRef.Location = New Point(10, 65)
        ChkCenterRef.Margin = New Padding(10, 3, 3, 3)
        ChkCenterRef.Name = "ChkCenterRef"
        ChkCenterRef.Size = New Size(112, 29)
        ChkCenterRef.TabIndex = 5
        ChkCenterRef.Text = "Center Ref"
        ChkCenterRef.UseVisualStyleBackColor = True
        ' 
        ' ComboTrackRefBlade
        ' 
        ComboTrackRefBlade.Dock = DockStyle.Top
        ComboTrackRefBlade.FormattingEnabled = True
        ComboTrackRefBlade.Location = New Point(3, 262)
        ComboTrackRefBlade.Margin = New Padding(3, 0, 3, 0)
        ComboTrackRefBlade.Name = "ComboTrackRefBlade"
        ComboTrackRefBlade.Size = New Size(204, 31)
        ComboTrackRefBlade.TabIndex = 17
        ' 
        ' CmdSelectProgression
        ' 
        CmdSelectProgression.Dock = DockStyle.Fill
        CmdSelectProgression.Location = New Point(1, 426)
        CmdSelectProgression.Margin = New Padding(1)
        CmdSelectProgression.Name = "CmdSelectProgression"
        CmdSelectProgression.Size = New Size(208, 33)
        CmdSelectProgression.TabIndex = 20
        CmdSelectProgression.Text = "Select Progression"
        CmdSelectProgression.UseVisualStyleBackColor = True
        ' 
        ' CmdPrintAllGraphs
        ' 
        CmdPrintAllGraphs.Dock = DockStyle.Fill
        CmdPrintAllGraphs.Location = New Point(1, 461)
        CmdPrintAllGraphs.Margin = New Padding(1)
        CmdPrintAllGraphs.Name = "CmdPrintAllGraphs"
        CmdPrintAllGraphs.Size = New Size(208, 33)
        CmdPrintAllGraphs.TabIndex = 21
        CmdPrintAllGraphs.Text = "Print All Graphs"
        CmdPrintAllGraphs.UseVisualStyleBackColor = True
        ' 
        ' LblAxesScaling
        ' 
        LblAxesScaling.AutoSize = True
        LblAxesScaling.Dock = DockStyle.Fill
        LblAxesScaling.Location = New Point(3, 530)
        LblAxesScaling.Name = "LblAxesScaling"
        LblAxesScaling.Size = New Size(204, 25)
        LblAxesScaling.TabIndex = 22
        LblAxesScaling.Text = "Axes Scaling"
        ' 
        ' CBoxAxesScaling
        ' 
        CBoxAxesScaling.Dock = DockStyle.Fill
        CBoxAxesScaling.FormattingEnabled = True
        CBoxAxesScaling.Location = New Point(3, 555)
        CBoxAxesScaling.Margin = New Padding(3, 0, 3, 0)
        CBoxAxesScaling.Name = "CBoxAxesScaling"
        CBoxAxesScaling.Size = New Size(204, 31)
        CBoxAxesScaling.TabIndex = 23
        ' 
        ' LblFont
        ' 
        LblFont.AutoSize = True
        LblFont.Dock = DockStyle.Bottom
        LblFont.Location = New Point(3, 586)
        LblFont.Name = "LblFont"
        LblFont.Size = New Size(204, 25)
        LblFont.TabIndex = 24
        LblFont.Text = "Font"
        ' 
        ' TrackFont
        ' 
        TrackFont.Dock = DockStyle.Top
        TrackFont.LargeChange = 1
        TrackFont.Location = New Point(0, 611)
        TrackFont.Margin = New Padding(0)
        TrackFont.Maximum = 20
        TrackFont.Minimum = 11
        TrackFont.Name = "TrackFont"
        TrackFont.Size = New Size(210, 45)
        TrackFont.TabIndex = 25
        TrackFont.Value = 11
        ' 
        ' TrackSegments
        ' 
        TrackSegments.Dock = DockStyle.Fill
        TrackSegments.Location = New Point(3, 377)
        TrackSegments.Minimum = 1
        TrackSegments.Name = "TrackSegments"
        TrackSegments.Size = New Size(204, 45)
        TrackSegments.TabIndex = 26
        TrackSegments.Value = 1
        ' 
        ' RecordNavigationBar1
        ' 
        RecordNavigationBar1.AutoSize = True
        RecordNavigationBar1.AutoSizeMode = AutoSizeMode.GrowAndShrink
        RecordNavigationBar1.BoundControls = Nothing
        tLayoutComparison.SetColumnSpan(RecordNavigationBar1, 4)
        RecordNavigationBar1.Database = Nothing
        RecordNavigationBar1.Dock = DockStyle.Right
        RecordNavigationBar1.Filter = Nothing
        RecordNavigationBar1.FilterOn = False
        RecordNavigationBar1.Location = New Point(608, 0)
        RecordNavigationBar1.Margin = New Padding(0, 0, 17, 0)
        RecordNavigationBar1.MasterSource = Nothing
        RecordNavigationBar1.Name = "RecordNavigationBar1"
        RecordNavigationBar1.NoUpdates = False
        RecordNavigationBar1.ServiceProvider = Nothing
        RecordNavigationBar1.Size = New Size(818, 36)
        RecordNavigationBar1.TabIndex = 5
        ' 
        ' TLayoutNavigation
        ' 
        TLayoutNavigation.AutoSize = True
        TLayoutNavigation.ColumnCount = 5
        tLayoutComparison.SetColumnSpan(TLayoutNavigation, 2)
        TLayoutNavigation.ColumnStyles.Add(New ColumnStyle())
        TLayoutNavigation.ColumnStyles.Add(New ColumnStyle())
        TLayoutNavigation.ColumnStyles.Add(New ColumnStyle())
        TLayoutNavigation.ColumnStyles.Add(New ColumnStyle())
        TLayoutNavigation.ColumnStyles.Add(New ColumnStyle())
        TLayoutNavigation.Controls.Add(CmdComparison, 4, 0)
        TLayoutNavigation.Controls.Add(CmdInspect, 3, 0)
        TLayoutNavigation.Controls.Add(CmdGraph, 2, 0)
        TLayoutNavigation.Controls.Add(CmdLocalPitch, 1, 0)
        TLayoutNavigation.Controls.Add(CmdMeasure, 0, 0)
        TLayoutNavigation.Dock = DockStyle.Left
        TLayoutNavigation.Location = New Point(215, 36)
        TLayoutNavigation.Margin = New Padding(0)
        TLayoutNavigation.Name = "TLayoutNavigation"
        TLayoutNavigation.RowCount = 1
        TLayoutNavigation.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        TLayoutNavigation.Size = New Size(490, 75)
        TLayoutNavigation.TabIndex = 6
        ' 
        ' CmdComparison
        ' 
        CmdComparison.AutoSize = True
        CmdComparison.Dock = DockStyle.Fill
        CmdComparison.Location = New Point(408, 3)
        CmdComparison.Name = "CmdComparison"
        CmdComparison.Size = New Size(92, 69)
        CmdComparison.TabIndex = 4
        CmdComparison.Text = "Comp."
        CmdComparison.UseVisualStyleBackColor = True
        ' 
        ' CmdInspect
        ' 
        CmdInspect.AutoSize = True
        CmdInspect.Dock = DockStyle.Fill
        CmdInspect.Location = New Point(310, 3)
        CmdInspect.Name = "CmdInspect"
        CmdInspect.Size = New Size(92, 69)
        CmdInspect.TabIndex = 3
        CmdInspect.Text = "Inspect"
        CmdInspect.UseVisualStyleBackColor = True
        ' 
        ' CmdGraph
        ' 
        CmdGraph.AutoSize = True
        CmdGraph.Dock = DockStyle.Fill
        CmdGraph.Location = New Point(212, 3)
        CmdGraph.Name = "CmdGraph"
        CmdGraph.Size = New Size(92, 69)
        CmdGraph.TabIndex = 2
        CmdGraph.Text = "Graph"
        CmdGraph.UseVisualStyleBackColor = True
        ' 
        ' CmdLocalPitch
        ' 
        CmdLocalPitch.AutoSize = True
        CmdLocalPitch.Dock = DockStyle.Fill
        CmdLocalPitch.Location = New Point(101, 3)
        CmdLocalPitch.Name = "CmdLocalPitch"
        CmdLocalPitch.Size = New Size(105, 69)
        CmdLocalPitch.TabIndex = 1
        CmdLocalPitch.Text = "Local Pitch"
        CmdLocalPitch.UseVisualStyleBackColor = True
        ' 
        ' CmdMeasure
        ' 
        CmdMeasure.AutoSize = True
        CmdMeasure.Dock = DockStyle.Fill
        CmdMeasure.Location = New Point(3, 3)
        CmdMeasure.Name = "CmdMeasure"
        CmdMeasure.Size = New Size(92, 69)
        CmdMeasure.TabIndex = 0
        CmdMeasure.Text = "Measure"
        CmdMeasure.UseVisualStyleBackColor = True
        ' 
        ' PanelCompCharts
        ' 
        PanelCompCharts.AutoScroll = True
        PanelCompCharts.AutoScrollMargin = New Size(5, 0)
        PanelCompCharts.BorderStyle = BorderStyle.FixedSingle
        tLayoutComparison.SetColumnSpan(PanelCompCharts, 5)
        PanelCompCharts.Controls.Add(TLayoutCompCharts)
        PanelCompCharts.Dock = DockStyle.Fill
        PanelCompCharts.Location = New Point(215, 111)
        PanelCompCharts.Margin = New Padding(0)
        PanelCompCharts.Name = "PanelCompCharts"
        PanelCompCharts.Size = New Size(1228, 640)
        PanelCompCharts.TabIndex = 8
        ' 
        ' TLayoutCompCharts
        ' 
        TLayoutCompCharts.BackColor = SystemColors.Control
        TLayoutCompCharts.CellBorderStyle = TableLayoutPanelCellBorderStyle.OutsetDouble
        TLayoutCompCharts.ColumnCount = 1
        TLayoutCompCharts.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        TLayoutCompCharts.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        TLayoutCompCharts.Dock = DockStyle.Top
        TLayoutCompCharts.ForeColor = SystemColors.ControlText
        TLayoutCompCharts.Location = New Point(0, 0)
        TLayoutCompCharts.Margin = New Padding(0)
        TLayoutCompCharts.Name = "TLayoutCompCharts"
        TLayoutCompCharts.RowCount = 1
        TLayoutCompCharts.RowStyles.Add(New RowStyle(SizeType.Percent, 50F))
        TLayoutCompCharts.RowStyles.Add(New RowStyle(SizeType.Percent, 50F))
        TLayoutCompCharts.Size = New Size(1226, 202)
        TLayoutCompCharts.TabIndex = 7
        ' 
        ' FrmComparison
        ' 
        AutoScaleMode = AutoScaleMode.None
        BackColor = SystemColors.Control
        ClientSize = New Size(1443, 751)
        Controls.Add(tLayoutComparison)
        Font = New Font("Segoe UI", 13F)
        Margin = New Padding(3, 4, 3, 4)
        Name = "FrmComparison"
        Text = "FrmComparison"
        WindowState = FormWindowState.Maximized
        tLayoutComparison.ResumeLayout(False)
        tLayoutComparison.PerformLayout()
        CType(PictLogo, ComponentModel.ISupportInitialize).EndInit()
        CType(DataGridJobDetails, ComponentModel.ISupportInitialize).EndInit()
        CType(MeasurementTypesBindingSource, ComponentModel.ISupportInitialize).EndInit()
        CType(ToleranceBindingSource, ComponentModel.ISupportInitialize).EndInit()
        CType(EmployeeBindingSource, ComponentModel.ISupportInitialize).EndInit()
        CType(JobDetailsBindingSource, ComponentModel.ISupportInitialize).EndInit()
        tlayoutComparisonControls.ResumeLayout(False)
        tlayoutComparisonControls.PerformLayout()
        CType(TrackFont, ComponentModel.ISupportInitialize).EndInit()
        CType(TrackSegments, ComponentModel.ISupportInitialize).EndInit()
        TLayoutNavigation.ResumeLayout(False)
        TLayoutNavigation.PerformLayout()
        PanelCompCharts.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents tLayoutComparison As TableLayoutPanel
    Friend WithEvents PictLogo As PictureBox
    Friend WithEvents DataGridJobDetails As DataGridView
    Friend WithEvents tlayoutComparisonControls As TableLayoutPanel
    Friend WithEvents ChkExamineoneBlade As CheckBox
    Friend WithEvents ChkSpline As CheckBox
    Friend WithEvents ChkShowTrack As CheckBox
    Friend WithEvents ChkGraphEntireScan As CheckBox
    Friend WithEvents ChkKeepforComp As CheckBox
    Friend WithEvents LabRefPitch As Label
    Friend WithEvents LabSegments As Label
    Friend WithEvents LabRadiusorBlade As Label
    Friend WithEvents LabTrackRefBlade As Label
    Friend WithEvents TxtRefPitch As TextBox
    Friend WithEvents ChkCenterRef As CheckBox
    Friend WithEvents ComboRadiusorBlade As ComboBox
    Friend WithEvents ComboTrackRefBlade As ComboBox
    Friend WithEvents CmdSelectProgression As Button
    Friend WithEvents CmdPrintAllGraphs As Button
    Friend WithEvents RecordNavigationBar1 As RecordNavigationBar
    Friend WithEvents JobDetailsBindingSource As BindingSource
    Friend WithEvents MeasurementTypesBindingSource As BindingSource
    Friend WithEvents LblAxesScaling As Label
    Friend WithEvents CBoxAxesScaling As ComboBox
    Friend WithEvents LblFont As Label
    Friend WithEvents TrackFont As TrackBar
    Friend WithEvents TrackSegments As TrackBar
    Friend WithEvents ToleranceBindingSource As BindingSource
    Friend WithEvents EmployeeBindingSource As BindingSource
    Friend WithEvents TLayoutNavigation As TableLayoutPanel
    Friend WithEvents CmdComparison As Button
    Friend WithEvents CmdInspect As Button
    Friend WithEvents CmdGraph As Button
    Friend WithEvents CmdLocalPitch As Button
    Friend WithEvents CmdMeasure As Button
    Friend WithEvents TLayoutCompCharts As TableLayoutPanel
    Friend WithEvents PanelCompCharts As Panel
    Friend WithEvents StartDateDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents MeasurementTypeIdDataGridViewTextBoxColumn As DataGridViewComboBoxColumn
    Friend WithEvents ToleranceClassDataGridViewTextBoxColumn As DataGridViewComboBoxColumn
    Friend WithEvents PerformedByDataGridViewTextBoxColumn As DataGridViewComboBoxColumn
    Friend WithEvents DescriptionDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
End Class
