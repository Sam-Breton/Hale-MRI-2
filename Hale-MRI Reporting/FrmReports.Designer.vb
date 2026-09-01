Imports LibDatabase

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmReports
    Inherits FrmDatabaseForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmReports))
        ToolStripContainer1 = New ToolStripContainer()
        ReportViewer1 = New ReportViewer()
        FormMenuStrip = New MenuStrip()
        FileToolStripMenuItem = New ToolStripMenuItem()
        FileNewToolStripMenuItem = New ToolStripMenuItem()
        ToolStripSeparator11 = New ToolStripSeparator()
        FileOpenToolStripMenuItem = New ToolStripMenuItem()
        FileRecentToolStripMenuItem = New ToolStripMenuItem()
        ToolStripFileRecentSeparator = New ToolStripSeparator()
        RecentReportsClearListToolStripMenuItem = New ToolStripMenuItem()
        ToolStripSeparator1 = New ToolStripSeparator()
        FileCloseToolStripMenuItem = New ToolStripMenuItem()
        ToolStripSeparator3 = New ToolStripSeparator()
        FileSaveToolStripMenuItem = New ToolStripMenuItem()
        FileSaveAsToolStripMenuItem = New ToolStripMenuItem()
        ToolStripSeparator4 = New ToolStripSeparator()
        FilePrintToolStripMenuItem = New ToolStripMenuItem()
        PrintToolStripMenuItem = New ToolStripMenuItem()
        PrintPreviewToolStripMenuItem = New ToolStripMenuItem()
        PageSetupToolStripMenuItem = New ToolStripMenuItem()
        ToolStripSeparator2 = New ToolStripSeparator()
        FileExitToolStripMenuItem = New ToolStripMenuItem()
        EditToolStripMenuItem = New ToolStripMenuItem()
        EditUndoToolStripMenuItem = New ToolStripMenuItem()
        ToolStripSeparator7 = New ToolStripSeparator()
        EditCutToolStripMenuItem = New ToolStripMenuItem()
        EditCopyToolStripMenuItem = New ToolStripMenuItem()
        EditPasteToolStripMenuItem = New ToolStripMenuItem()
        EditDeleteToolStripMenuItem = New ToolStripMenuItem()
        ToolStripSeparator9 = New ToolStripSeparator()
        EditSelectAllToolStripMenuItem = New ToolStripMenuItem()
        JobsToolStripMenuItem = New ToolStripMenuItem()
        JobsOpenToolStripMenuItem = New ToolStripMenuItem()
        JobsRecentToolStripMenuItem = New ToolStripMenuItem()
        ToolStripSeparator15 = New ToolStripSeparator()
        JobsRecentClearListToolStripMenuItem = New ToolStripMenuItem()
        ToolStripSeparator13 = New ToolStripSeparator()
        JobsCloseToolStripMenuItem = New ToolStripMenuItem()
        ReportsToolStripMenuItem = New ToolStripMenuItem()
        ReportsToolStripRecentSeparator = New ToolStripSeparator()
        ReportsEditToolStripMenuItem = New ToolStripMenuItem()
        ReportsImportToolStripMenuItem = New ToolStripMenuItem()
        ReportsExportToolStripMenuItem = New ToolStripMenuItem()
        ToolStripSeparator12 = New ToolStripSeparator()
        ElementsToolStripMenuItem = New ToolStripMenuItem()
        ElementsLetterheadToolStripMenuItem = New ToolStripMenuItem()
        ToolStripSeparator5 = New ToolStripSeparator()
        LetterheadShowAllToolStripMenuItem = New ToolStripMenuItem()
        ElementsHeaderToolStripMenuItem = New ToolStripMenuItem()
        HeaderShowAllToolStripMenuItem = New ToolStripMenuItem()
        ToolStripSeparator6 = New ToolStripSeparator()
        HeaderItemsToolStripMenuItem = New ToolStripMenuItem()
        ToolStripSeparator16 = New ToolStripSeparator()
        SettingsToolStripMenuItem = New ToolStripMenuItem()
        ClassToolStripMenuItem = New ToolStripMenuItem()
        ClassSpecialToolStripMenuItem = New ToolStripMenuItem()
        ClassIToolStripMenuItem = New ToolStripMenuItem()
        ClasasIIToolStripMenuItem = New ToolStripMenuItem()
        ClassIIIToolStripMenuItem = New ToolStripMenuItem()
        BasisToolStripMenuItem = New ToolStripMenuItem()
        MeanToolStripMenuItem = New ToolStripMenuItem()
        MarkedToolStripMenuItem = New ToolStripMenuItem()
        DesiredToolStripMenuItem = New ToolStripMenuItem()
        PrecisionToolStripMenuItem = New ToolStripMenuItem()
        Precision00ToolStripMenuItem = New ToolStripMenuItem()
        Precision000ToolStripMenuItem = New ToolStripMenuItem()
        ViewToolStripMenuItem = New ToolStripMenuItem()
        ViewPagesToolStripMenuItem = New ToolStripMenuItem()
        ToolStripSeparator14 = New ToolStripSeparator()
        GridSizeToolStripMenuItem = New ToolStripMenuItem()
        GridSizeToolStripTextBox = New ToolStripTextBox()
        ToolStripSeparator8 = New ToolStripSeparator()
        ZoomInToolStripMenuItem = New ToolStripMenuItem()
        ZoomOutToolStripMenuItem = New ToolStripMenuItem()
        ActualSizeToolStripMenuItem = New ToolStripMenuItem()
        DocumentPageContextMenuStrip = New ContextMenuStrip(components)
        AddNewPageToolStripMenuItem = New ToolStripMenuItem()
        DeletePageToolStripMenuItem = New ToolStripMenuItem()
        ToolStripSeparator17 = New ToolStripSeparator()
        ScrollFirstToolStripMenuItem = New ToolStripMenuItem()
        ScrollLastToolStripMenuItem = New ToolStripMenuItem()
        ScrollNextToolStripMenuItem = New ToolStripMenuItem()
        ScrollPreviousToolStripMenuItem = New ToolStripMenuItem()
        ScrollToToolStripMenuItem = New ToolStripMenuItem()
        ToolStripSeparator18 = New ToolStripSeparator()
        MoveFirstToolStripMenuItem = New ToolStripMenuItem()
        MoveLastToolStripMenuItem = New ToolStripMenuItem()
        MoveDownToolStripMenuItem = New ToolStripMenuItem()
        MoveUpToolStripMenuItem = New ToolStripMenuItem()
        ToolStripSeparator22 = New ToolStripSeparator()
        PageEditToolStripMenuItem = New ToolStripMenuItem()
        PageCutToolStripMenuItem = New ToolStripMenuItem()
        PagePasteToolStripMenuItem = New ToolStripMenuItem()
        PageDeleteToolStripMenuItem = New ToolStripMenuItem()
        ToolStripSeparator23 = New ToolStripSeparator()
        PageSelectAllToolStripMenuItem = New ToolStripMenuItem()
        PrintPreviewDialog = New PrintPreviewDialog()
        PageSetupDialog = New PageSetupDialog()
        ReportsBindingSource = New BindingSource(components)
        ReportDataBindingSource = New BindingSource(components)
        DisplayControlContextMenuStrip = New ContextMenuStrip(components)
        BringToFrontToolStripMenuItem = New ToolStripMenuItem()
        SendToBackToolStripMenuItem = New ToolStripMenuItem()
        ToolStripSeparator19 = New ToolStripSeparator()
        UndoToolStripMenuItem = New ToolStripMenuItem()
        ToolStripSeparator20 = New ToolStripSeparator()
        CutToolStripMenuItem = New ToolStripMenuItem()
        PasteToolStripMenuItem = New ToolStripMenuItem()
        DeleteToolStripMenuItem = New ToolStripMenuItem()
        ToolStripSeparator21 = New ToolStripSeparator()
        SelectAllToolStripMenuItem = New ToolStripMenuItem()
        ToolStripSeparator25 = New ToolStripSeparator()
        ThemeEditorToolStripMenuItem = New ToolStripMenuItem()
        OpenFileDialog1 = New OpenFileDialog()
        HeaderMenuStrip = New ContextMenuStrip(components)
        HeaderBorderStyleMenuItem = New ToolStripMenuItem()
        BorderStyleNoneMenuItem = New ToolStripMenuItem()
        BorderStyleFixedSingleMenuItem = New ToolStripMenuItem()
        BorderStyleFixed3DMenuItem = New ToolStripMenuItem()
        LetterheadMenuStrip = New ContextMenuStrip(components)
        LetterheadImageMenuItem = New ToolStripMenuItem()
        ToolStripSeparator24 = New ToolStripSeparator()
        LetterheadSizeModeMenuItem = New ToolStripMenuItem()
        SizeModeNormalMenuItem = New ToolStripMenuItem()
        SizeModeStretchMenuItem = New ToolStripMenuItem()
        SizeModeAutoSizeMenuItem = New ToolStripMenuItem()
        SizeModeCenterMenuItem = New ToolStripMenuItem()
        SizeModeZoomMenuItem = New ToolStripMenuItem()
        LetterheadBorderStyleMenuItem = New ToolStripMenuItem()
        LetterheadBorderStyleNoneMenuItem = New ToolStripMenuItem()
        LetterheadBorderStyleFixedSingleMenuItem = New ToolStripMenuItem()
        LetterheadBorderStyleFixed3DMenuItem = New ToolStripMenuItem()
        PrintDocument = New Drawing.Printing.PrintDocument()
        ToolStripContainer1.ContentPanel.SuspendLayout()
        ToolStripContainer1.TopToolStripPanel.SuspendLayout()
        ToolStripContainer1.SuspendLayout()
        FormMenuStrip.SuspendLayout()
        DocumentPageContextMenuStrip.SuspendLayout()
        CType(ReportsBindingSource, ComponentModel.ISupportInitialize).BeginInit()
        CType(ReportDataBindingSource, ComponentModel.ISupportInitialize).BeginInit()
        DisplayControlContextMenuStrip.SuspendLayout()
        HeaderMenuStrip.SuspendLayout()
        LetterheadMenuStrip.SuspendLayout()
        SuspendLayout()
        ' 
        ' ToolStripContainer1
        ' 
        ' 
        ' ToolStripContainer1.ContentPanel
        ' 
        ToolStripContainer1.ContentPanel.Controls.Add(ReportViewer1)
        ToolStripContainer1.ContentPanel.Size = New Size(1050, 820)
        ToolStripContainer1.Dock = DockStyle.Fill
        ToolStripContainer1.Location = New Point(0, 0)
        ToolStripContainer1.Name = "ToolStripContainer1"
        ToolStripContainer1.Size = New Size(1050, 844)
        ToolStripContainer1.TabIndex = 0
        ToolStripContainer1.Text = "ToolStripContainer1"
        ' 
        ' ToolStripContainer1.TopToolStripPanel
        ' 
        ToolStripContainer1.TopToolStripPanel.Controls.Add(FormMenuStrip)
        ' 
        ' ReportViewer1
        ' 
        ReportViewer1.AutoScroll = True
        ReportViewer1.CurrentPageIndex = 0
        ReportViewer1.DataSource = Nothing
        ReportViewer1.DisplayControlContextMenu = Nothing
        ReportViewer1.Dock = DockStyle.Fill
        ReportViewer1.Document = Nothing
        ReportViewer1.FlowDirection = FlowDirection.TopDown
        ReportViewer1.GridSize = 0
        ReportViewer1.HeaderContextMenuStrip = Nothing
        ReportViewer1.HeaderShowOnAllPages = False
        ReportViewer1.LetterheadContextMenuStrip = Nothing
        ReportViewer1.LetterheadShowOnAllPages = False
        ReportViewer1.Location = New Point(0, 0)
        ReportViewer1.MultiSelect = False
        ReportViewer1.Name = "ReportViewer1"
        ReportViewer1.Size = New Size(1050, 820)
        ReportViewer1.TabIndex = 0
        ReportViewer1.VerticalLimit = 0
        ReportViewer1.WrapContents = False
        ReportViewer1.Zoom = 1F
        ' 
        ' FormMenuStrip
        ' 
        FormMenuStrip.Dock = DockStyle.None
        FormMenuStrip.Items.AddRange(New ToolStripItem() {FileToolStripMenuItem, EditToolStripMenuItem, JobsToolStripMenuItem, ReportsToolStripMenuItem, ElementsToolStripMenuItem, SettingsToolStripMenuItem, ViewToolStripMenuItem})
        FormMenuStrip.Location = New Point(0, 0)
        FormMenuStrip.Name = "FormMenuStrip"
        FormMenuStrip.Size = New Size(1050, 24)
        FormMenuStrip.TabIndex = 5
        FormMenuStrip.Text = "MenuStrip1"
        ' 
        ' FileToolStripMenuItem
        ' 
        FileToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {FileNewToolStripMenuItem, ToolStripSeparator11, FileOpenToolStripMenuItem, FileRecentToolStripMenuItem, ToolStripSeparator1, FileCloseToolStripMenuItem, ToolStripSeparator3, FileSaveToolStripMenuItem, FileSaveAsToolStripMenuItem, ToolStripSeparator4, FilePrintToolStripMenuItem, ToolStripSeparator2, FileExitToolStripMenuItem})
        FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        FileToolStripMenuItem.Size = New Size(37, 20)
        FileToolStripMenuItem.Text = "File"
        ' 
        ' FileNewToolStripMenuItem
        ' 
        FileNewToolStripMenuItem.Name = "FileNewToolStripMenuItem"
        FileNewToolStripMenuItem.Size = New Size(186, 22)
        FileNewToolStripMenuItem.Text = "New"
        ' 
        ' ToolStripSeparator11
        ' 
        ToolStripSeparator11.Name = "ToolStripSeparator11"
        ToolStripSeparator11.Size = New Size(183, 6)
        ' 
        ' FileOpenToolStripMenuItem
        ' 
        FileOpenToolStripMenuItem.Name = "FileOpenToolStripMenuItem"
        FileOpenToolStripMenuItem.Size = New Size(186, 22)
        FileOpenToolStripMenuItem.Text = "Open"
        ' 
        ' FileRecentToolStripMenuItem
        ' 
        FileRecentToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {ToolStripFileRecentSeparator, RecentReportsClearListToolStripMenuItem})
        FileRecentToolStripMenuItem.Name = "FileRecentToolStripMenuItem"
        FileRecentToolStripMenuItem.Size = New Size(186, 22)
        FileRecentToolStripMenuItem.Text = "Recent"
        ' 
        ' ToolStripFileRecentSeparator
        ' 
        ToolStripFileRecentSeparator.Name = "ToolStripFileRecentSeparator"
        ToolStripFileRecentSeparator.Size = New Size(119, 6)
        ' 
        ' RecentReportsClearListToolStripMenuItem
        ' 
        RecentReportsClearListToolStripMenuItem.Name = "RecentReportsClearListToolStripMenuItem"
        RecentReportsClearListToolStripMenuItem.Size = New Size(122, 22)
        RecentReportsClearListToolStripMenuItem.Text = "Clear List"
        ' 
        ' ToolStripSeparator1
        ' 
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New Size(183, 6)
        ' 
        ' FileCloseToolStripMenuItem
        ' 
        FileCloseToolStripMenuItem.Enabled = False
        FileCloseToolStripMenuItem.Name = "FileCloseToolStripMenuItem"
        FileCloseToolStripMenuItem.Size = New Size(186, 22)
        FileCloseToolStripMenuItem.Text = "Close"
        ' 
        ' ToolStripSeparator3
        ' 
        ToolStripSeparator3.Name = "ToolStripSeparator3"
        ToolStripSeparator3.Size = New Size(183, 6)
        ' 
        ' FileSaveToolStripMenuItem
        ' 
        FileSaveToolStripMenuItem.Enabled = False
        FileSaveToolStripMenuItem.Name = "FileSaveToolStripMenuItem"
        FileSaveToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+S"
        FileSaveToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.S
        FileSaveToolStripMenuItem.Size = New Size(186, 22)
        FileSaveToolStripMenuItem.Text = "Save"
        ' 
        ' FileSaveAsToolStripMenuItem
        ' 
        FileSaveAsToolStripMenuItem.Enabled = False
        FileSaveAsToolStripMenuItem.Name = "FileSaveAsToolStripMenuItem"
        FileSaveAsToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Shift+S"
        FileSaveAsToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.Shift Or Keys.S
        FileSaveAsToolStripMenuItem.Size = New Size(186, 22)
        FileSaveAsToolStripMenuItem.Text = "Save As"
        ' 
        ' ToolStripSeparator4
        ' 
        ToolStripSeparator4.Name = "ToolStripSeparator4"
        ToolStripSeparator4.Size = New Size(183, 6)
        ' 
        ' FilePrintToolStripMenuItem
        ' 
        FilePrintToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {PrintToolStripMenuItem, PrintPreviewToolStripMenuItem, PageSetupToolStripMenuItem})
        FilePrintToolStripMenuItem.Name = "FilePrintToolStripMenuItem"
        FilePrintToolStripMenuItem.Size = New Size(186, 22)
        FilePrintToolStripMenuItem.Text = "Print"
        ' 
        ' PrintToolStripMenuItem
        ' 
        PrintToolStripMenuItem.Enabled = False
        PrintToolStripMenuItem.Name = "PrintToolStripMenuItem"
        PrintToolStripMenuItem.Size = New Size(143, 22)
        PrintToolStripMenuItem.Text = "Print"
        ' 
        ' PrintPreviewToolStripMenuItem
        ' 
        PrintPreviewToolStripMenuItem.Enabled = False
        PrintPreviewToolStripMenuItem.Name = "PrintPreviewToolStripMenuItem"
        PrintPreviewToolStripMenuItem.Size = New Size(143, 22)
        PrintPreviewToolStripMenuItem.Text = "Print Preview"
        ' 
        ' PageSetupToolStripMenuItem
        ' 
        PageSetupToolStripMenuItem.Name = "PageSetupToolStripMenuItem"
        PageSetupToolStripMenuItem.Size = New Size(143, 22)
        PageSetupToolStripMenuItem.Text = "Page Setup"
        ' 
        ' ToolStripSeparator2
        ' 
        ToolStripSeparator2.Name = "ToolStripSeparator2"
        ToolStripSeparator2.Size = New Size(183, 6)
        ' 
        ' FileExitToolStripMenuItem
        ' 
        FileExitToolStripMenuItem.Name = "FileExitToolStripMenuItem"
        FileExitToolStripMenuItem.Size = New Size(186, 22)
        FileExitToolStripMenuItem.Text = "Exit"
        ' 
        ' EditToolStripMenuItem
        ' 
        EditToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {EditUndoToolStripMenuItem, ToolStripSeparator7, EditCutToolStripMenuItem, EditCopyToolStripMenuItem, EditPasteToolStripMenuItem, EditDeleteToolStripMenuItem, ToolStripSeparator9, EditSelectAllToolStripMenuItem})
        EditToolStripMenuItem.Enabled = False
        EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        EditToolStripMenuItem.Size = New Size(39, 20)
        EditToolStripMenuItem.Text = "Edit"
        ' 
        ' EditUndoToolStripMenuItem
        ' 
        EditUndoToolStripMenuItem.Enabled = False
        EditUndoToolStripMenuItem.Name = "EditUndoToolStripMenuItem"
        EditUndoToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.Z
        EditUndoToolStripMenuItem.Size = New Size(164, 22)
        EditUndoToolStripMenuItem.Text = "Undo"
        ' 
        ' ToolStripSeparator7
        ' 
        ToolStripSeparator7.Name = "ToolStripSeparator7"
        ToolStripSeparator7.Size = New Size(161, 6)
        ' 
        ' EditCutToolStripMenuItem
        ' 
        EditCutToolStripMenuItem.Enabled = False
        EditCutToolStripMenuItem.Name = "EditCutToolStripMenuItem"
        EditCutToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+X"
        EditCutToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.X
        EditCutToolStripMenuItem.Size = New Size(164, 22)
        EditCutToolStripMenuItem.Text = "Cut"
        ' 
        ' EditCopyToolStripMenuItem
        ' 
        EditCopyToolStripMenuItem.Enabled = False
        EditCopyToolStripMenuItem.Name = "EditCopyToolStripMenuItem"
        EditCopyToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+C"
        EditCopyToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.C
        EditCopyToolStripMenuItem.Size = New Size(164, 22)
        EditCopyToolStripMenuItem.Text = "Copy"
        ' 
        ' EditPasteToolStripMenuItem
        ' 
        EditPasteToolStripMenuItem.Enabled = False
        EditPasteToolStripMenuItem.Name = "EditPasteToolStripMenuItem"
        EditPasteToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+V"
        EditPasteToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.V
        EditPasteToolStripMenuItem.Size = New Size(164, 22)
        EditPasteToolStripMenuItem.Text = "Paste"
        ' 
        ' EditDeleteToolStripMenuItem
        ' 
        EditDeleteToolStripMenuItem.Enabled = False
        EditDeleteToolStripMenuItem.Name = "EditDeleteToolStripMenuItem"
        EditDeleteToolStripMenuItem.ShortcutKeyDisplayString = "Del"
        EditDeleteToolStripMenuItem.ShortcutKeys = Keys.Delete
        EditDeleteToolStripMenuItem.Size = New Size(164, 22)
        EditDeleteToolStripMenuItem.Text = "Delete"
        ' 
        ' ToolStripSeparator9
        ' 
        ToolStripSeparator9.Name = "ToolStripSeparator9"
        ToolStripSeparator9.Size = New Size(161, 6)
        ' 
        ' EditSelectAllToolStripMenuItem
        ' 
        EditSelectAllToolStripMenuItem.Enabled = False
        EditSelectAllToolStripMenuItem.Name = "EditSelectAllToolStripMenuItem"
        EditSelectAllToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+A"
        EditSelectAllToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.A
        EditSelectAllToolStripMenuItem.Size = New Size(164, 22)
        EditSelectAllToolStripMenuItem.Text = "Select All"
        ' 
        ' JobsToolStripMenuItem
        ' 
        JobsToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {JobsOpenToolStripMenuItem, JobsRecentToolStripMenuItem, ToolStripSeparator13, JobsCloseToolStripMenuItem})
        JobsToolStripMenuItem.Name = "JobsToolStripMenuItem"
        JobsToolStripMenuItem.Size = New Size(42, 20)
        JobsToolStripMenuItem.Text = "Jobs"
        ' 
        ' JobsOpenToolStripMenuItem
        ' 
        JobsOpenToolStripMenuItem.Name = "JobsOpenToolStripMenuItem"
        JobsOpenToolStripMenuItem.Size = New Size(110, 22)
        JobsOpenToolStripMenuItem.Text = "Open"
        ' 
        ' JobsRecentToolStripMenuItem
        ' 
        JobsRecentToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {ToolStripSeparator15, JobsRecentClearListToolStripMenuItem})
        JobsRecentToolStripMenuItem.Name = "JobsRecentToolStripMenuItem"
        JobsRecentToolStripMenuItem.Size = New Size(110, 22)
        JobsRecentToolStripMenuItem.Text = "Recent"
        ' 
        ' ToolStripSeparator15
        ' 
        ToolStripSeparator15.Name = "ToolStripSeparator15"
        ToolStripSeparator15.Size = New Size(119, 6)
        ' 
        ' JobsRecentClearListToolStripMenuItem
        ' 
        JobsRecentClearListToolStripMenuItem.Enabled = False
        JobsRecentClearListToolStripMenuItem.Name = "JobsRecentClearListToolStripMenuItem"
        JobsRecentClearListToolStripMenuItem.Size = New Size(122, 22)
        JobsRecentClearListToolStripMenuItem.Text = "Clear List"
        ' 
        ' ToolStripSeparator13
        ' 
        ToolStripSeparator13.Name = "ToolStripSeparator13"
        ToolStripSeparator13.Size = New Size(107, 6)
        ' 
        ' JobsCloseToolStripMenuItem
        ' 
        JobsCloseToolStripMenuItem.Enabled = False
        JobsCloseToolStripMenuItem.Name = "JobsCloseToolStripMenuItem"
        JobsCloseToolStripMenuItem.Size = New Size(110, 22)
        JobsCloseToolStripMenuItem.Text = "Close"
        ' 
        ' ReportsToolStripMenuItem
        ' 
        ReportsToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {ReportsToolStripRecentSeparator, ReportsEditToolStripMenuItem, ReportsImportToolStripMenuItem, ReportsExportToolStripMenuItem, ToolStripSeparator12})
        ReportsToolStripMenuItem.Name = "ReportsToolStripMenuItem"
        ReportsToolStripMenuItem.Size = New Size(59, 20)
        ReportsToolStripMenuItem.Text = "Reports"
        ' 
        ' ReportsToolStripRecentSeparator
        ' 
        ReportsToolStripRecentSeparator.Name = "ReportsToolStripRecentSeparator"
        ReportsToolStripRecentSeparator.Size = New Size(107, 6)
        ' 
        ' ReportsEditToolStripMenuItem
        ' 
        ReportsEditToolStripMenuItem.Name = "ReportsEditToolStripMenuItem"
        ReportsEditToolStripMenuItem.Size = New Size(110, 22)
        ReportsEditToolStripMenuItem.Text = "Edit"
        ' 
        ' ReportsImportToolStripMenuItem
        ' 
        ReportsImportToolStripMenuItem.Name = "ReportsImportToolStripMenuItem"
        ReportsImportToolStripMenuItem.Size = New Size(110, 22)
        ReportsImportToolStripMenuItem.Text = "Import"
        ' 
        ' ReportsExportToolStripMenuItem
        ' 
        ReportsExportToolStripMenuItem.Enabled = False
        ReportsExportToolStripMenuItem.Name = "ReportsExportToolStripMenuItem"
        ReportsExportToolStripMenuItem.Size = New Size(110, 22)
        ReportsExportToolStripMenuItem.Text = "Export"
        ' 
        ' ToolStripSeparator12
        ' 
        ToolStripSeparator12.Name = "ToolStripSeparator12"
        ToolStripSeparator12.Size = New Size(107, 6)
        ' 
        ' ElementsToolStripMenuItem
        ' 
        ElementsToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {ElementsLetterheadToolStripMenuItem, ElementsHeaderToolStripMenuItem, ToolStripSeparator16})
        ElementsToolStripMenuItem.Enabled = False
        ElementsToolStripMenuItem.Name = "ElementsToolStripMenuItem"
        ElementsToolStripMenuItem.Size = New Size(67, 20)
        ElementsToolStripMenuItem.Text = "Elements"
        ' 
        ' ElementsLetterheadToolStripMenuItem
        ' 
        ElementsLetterheadToolStripMenuItem.CheckOnClick = True
        ElementsLetterheadToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {ToolStripSeparator5, LetterheadShowAllToolStripMenuItem})
        ElementsLetterheadToolStripMenuItem.Name = "ElementsLetterheadToolStripMenuItem"
        ElementsLetterheadToolStripMenuItem.Size = New Size(130, 22)
        ElementsLetterheadToolStripMenuItem.Text = "Letterhead"
        ' 
        ' ToolStripSeparator5
        ' 
        ToolStripSeparator5.Name = "ToolStripSeparator5"
        ToolStripSeparator5.Size = New Size(170, 6)
        ' 
        ' LetterheadShowAllToolStripMenuItem
        ' 
        LetterheadShowAllToolStripMenuItem.CheckOnClick = True
        LetterheadShowAllToolStripMenuItem.Name = "LetterheadShowAllToolStripMenuItem"
        LetterheadShowAllToolStripMenuItem.Size = New Size(173, 22)
        LetterheadShowAllToolStripMenuItem.Text = "Show On All Pages"
        ' 
        ' ElementsHeaderToolStripMenuItem
        ' 
        ElementsHeaderToolStripMenuItem.CheckOnClick = True
        ElementsHeaderToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {HeaderShowAllToolStripMenuItem, ToolStripSeparator6, HeaderItemsToolStripMenuItem})
        ElementsHeaderToolStripMenuItem.Name = "ElementsHeaderToolStripMenuItem"
        ElementsHeaderToolStripMenuItem.Size = New Size(130, 22)
        ElementsHeaderToolStripMenuItem.Text = "Header"
        ' 
        ' HeaderShowAllToolStripMenuItem
        ' 
        HeaderShowAllToolStripMenuItem.CheckOnClick = True
        HeaderShowAllToolStripMenuItem.Name = "HeaderShowAllToolStripMenuItem"
        HeaderShowAllToolStripMenuItem.Size = New Size(173, 22)
        HeaderShowAllToolStripMenuItem.Text = "Show On All Pages"
        ' 
        ' ToolStripSeparator6
        ' 
        ToolStripSeparator6.Name = "ToolStripSeparator6"
        ToolStripSeparator6.Size = New Size(170, 6)
        ' 
        ' HeaderItemsToolStripMenuItem
        ' 
        HeaderItemsToolStripMenuItem.Name = "HeaderItemsToolStripMenuItem"
        HeaderItemsToolStripMenuItem.Size = New Size(173, 22)
        HeaderItemsToolStripMenuItem.Text = "Items"
        ' 
        ' ToolStripSeparator16
        ' 
        ToolStripSeparator16.Name = "ToolStripSeparator16"
        ToolStripSeparator16.Size = New Size(127, 6)
        ' 
        ' SettingsToolStripMenuItem
        ' 
        SettingsToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {ClassToolStripMenuItem, BasisToolStripMenuItem, PrecisionToolStripMenuItem})
        SettingsToolStripMenuItem.Enabled = False
        SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem"
        SettingsToolStripMenuItem.Size = New Size(61, 20)
        SettingsToolStripMenuItem.Text = "Settings"
        ' 
        ' ClassToolStripMenuItem
        ' 
        ClassToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {ClassSpecialToolStripMenuItem, ClassIToolStripMenuItem, ClasasIIToolStripMenuItem, ClassIIIToolStripMenuItem})
        ClassToolStripMenuItem.Name = "ClassToolStripMenuItem"
        ClassToolStripMenuItem.Size = New Size(122, 22)
        ClassToolStripMenuItem.Text = "Class"
        ' 
        ' ClassSpecialToolStripMenuItem
        ' 
        ClassSpecialToolStripMenuItem.CheckOnClick = True
        ClassSpecialToolStripMenuItem.Name = "ClassSpecialToolStripMenuItem"
        ClassSpecialToolStripMenuItem.Size = New Size(83, 22)
        ClassSpecialToolStripMenuItem.Text = "S"
        ' 
        ' ClassIToolStripMenuItem
        ' 
        ClassIToolStripMenuItem.Checked = True
        ClassIToolStripMenuItem.CheckOnClick = True
        ClassIToolStripMenuItem.CheckState = CheckState.Checked
        ClassIToolStripMenuItem.Name = "ClassIToolStripMenuItem"
        ClassIToolStripMenuItem.Size = New Size(83, 22)
        ClassIToolStripMenuItem.Text = "I"
        ' 
        ' ClasasIIToolStripMenuItem
        ' 
        ClasasIIToolStripMenuItem.CheckOnClick = True
        ClasasIIToolStripMenuItem.Name = "ClasasIIToolStripMenuItem"
        ClasasIIToolStripMenuItem.Size = New Size(83, 22)
        ClasasIIToolStripMenuItem.Text = "II"
        ' 
        ' ClassIIIToolStripMenuItem
        ' 
        ClassIIIToolStripMenuItem.CheckOnClick = True
        ClassIIIToolStripMenuItem.Name = "ClassIIIToolStripMenuItem"
        ClassIIIToolStripMenuItem.Size = New Size(83, 22)
        ClassIIIToolStripMenuItem.Text = "III"
        ' 
        ' BasisToolStripMenuItem
        ' 
        BasisToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {MeanToolStripMenuItem, MarkedToolStripMenuItem, DesiredToolStripMenuItem})
        BasisToolStripMenuItem.Name = "BasisToolStripMenuItem"
        BasisToolStripMenuItem.Size = New Size(122, 22)
        BasisToolStripMenuItem.Text = "Basis"
        ' 
        ' MeanToolStripMenuItem
        ' 
        MeanToolStripMenuItem.Checked = True
        MeanToolStripMenuItem.CheckOnClick = True
        MeanToolStripMenuItem.CheckState = CheckState.Checked
        MeanToolStripMenuItem.Name = "MeanToolStripMenuItem"
        MeanToolStripMenuItem.Size = New Size(114, 22)
        MeanToolStripMenuItem.Text = "Mean"
        ' 
        ' MarkedToolStripMenuItem
        ' 
        MarkedToolStripMenuItem.CheckOnClick = True
        MarkedToolStripMenuItem.Name = "MarkedToolStripMenuItem"
        MarkedToolStripMenuItem.Size = New Size(114, 22)
        MarkedToolStripMenuItem.Text = "Marked"
        ' 
        ' DesiredToolStripMenuItem
        ' 
        DesiredToolStripMenuItem.CheckOnClick = True
        DesiredToolStripMenuItem.Name = "DesiredToolStripMenuItem"
        DesiredToolStripMenuItem.Size = New Size(114, 22)
        DesiredToolStripMenuItem.Text = "Desired"
        ' 
        ' PrecisionToolStripMenuItem
        ' 
        PrecisionToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {Precision00ToolStripMenuItem, Precision000ToolStripMenuItem})
        PrecisionToolStripMenuItem.Name = "PrecisionToolStripMenuItem"
        PrecisionToolStripMenuItem.Size = New Size(122, 22)
        PrecisionToolStripMenuItem.Text = "Precision"
        ' 
        ' Precision00ToolStripMenuItem
        ' 
        Precision00ToolStripMenuItem.Checked = True
        Precision00ToolStripMenuItem.CheckOnClick = True
        Precision00ToolStripMenuItem.CheckState = CheckState.Checked
        Precision00ToolStripMenuItem.Name = "Precision00ToolStripMenuItem"
        Precision00ToolStripMenuItem.Size = New Size(136, 22)
        Precision00ToolStripMenuItem.Tag = "2"
        Precision00ToolStripMenuItem.Text = ".0 and .00"
        ' 
        ' Precision000ToolStripMenuItem
        ' 
        Precision000ToolStripMenuItem.CheckOnClick = True
        Precision000ToolStripMenuItem.Name = "Precision000ToolStripMenuItem"
        Precision000ToolStripMenuItem.Size = New Size(136, 22)
        Precision000ToolStripMenuItem.Tag = "3"
        Precision000ToolStripMenuItem.Text = ".00 and .000"
        ' 
        ' ViewToolStripMenuItem
        ' 
        ViewToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {ViewPagesToolStripMenuItem, ToolStripSeparator14, GridSizeToolStripMenuItem, ToolStripSeparator8, ZoomInToolStripMenuItem, ZoomOutToolStripMenuItem, ActualSizeToolStripMenuItem})
        ViewToolStripMenuItem.Enabled = False
        ViewToolStripMenuItem.Name = "ViewToolStripMenuItem"
        ViewToolStripMenuItem.Size = New Size(44, 20)
        ViewToolStripMenuItem.Text = "View"
        ' 
        ' ViewPagesToolStripMenuItem
        ' 
        ViewPagesToolStripMenuItem.Name = "ViewPagesToolStripMenuItem"
        ViewPagesToolStripMenuItem.Size = New Size(171, 22)
        ViewPagesToolStripMenuItem.Text = "Pages"
        ' 
        ' ToolStripSeparator14
        ' 
        ToolStripSeparator14.Name = "ToolStripSeparator14"
        ToolStripSeparator14.Size = New Size(168, 6)
        ' 
        ' GridSizeToolStripMenuItem
        ' 
        GridSizeToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {GridSizeToolStripTextBox})
        GridSizeToolStripMenuItem.Name = "GridSizeToolStripMenuItem"
        GridSizeToolStripMenuItem.Size = New Size(171, 22)
        GridSizeToolStripMenuItem.Text = "Grid Size"
        ' 
        ' GridSizeToolStripTextBox
        ' 
        GridSizeToolStripTextBox.Name = "GridSizeToolStripTextBox"
        GridSizeToolStripTextBox.Size = New Size(100, 23)
        ' 
        ' ToolStripSeparator8
        ' 
        ToolStripSeparator8.Name = "ToolStripSeparator8"
        ToolStripSeparator8.Size = New Size(168, 6)
        ' 
        ' ZoomInToolStripMenuItem
        ' 
        ZoomInToolStripMenuItem.Name = "ZoomInToolStripMenuItem"
        ZoomInToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl++"
        ZoomInToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.Oemplus
        ZoomInToolStripMenuItem.Size = New Size(171, 22)
        ZoomInToolStripMenuItem.Text = "Zoom In"
        ' 
        ' ZoomOutToolStripMenuItem
        ' 
        ZoomOutToolStripMenuItem.Name = "ZoomOutToolStripMenuItem"
        ZoomOutToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+-"
        ZoomOutToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.OemMinus
        ZoomOutToolStripMenuItem.Size = New Size(171, 22)
        ZoomOutToolStripMenuItem.Text = "Zoom Out"
        ' 
        ' ActualSizeToolStripMenuItem
        ' 
        ActualSizeToolStripMenuItem.Name = "ActualSizeToolStripMenuItem"
        ActualSizeToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.D0
        ActualSizeToolStripMenuItem.Size = New Size(171, 22)
        ActualSizeToolStripMenuItem.Text = "Actual Size"
        ' 
        ' DocumentPageContextMenuStrip
        ' 
        DocumentPageContextMenuStrip.ImageScalingSize = New Size(32, 32)
        DocumentPageContextMenuStrip.Items.AddRange(New ToolStripItem() {AddNewPageToolStripMenuItem, DeletePageToolStripMenuItem, ToolStripSeparator17, ScrollFirstToolStripMenuItem, ScrollLastToolStripMenuItem, ScrollNextToolStripMenuItem, ScrollPreviousToolStripMenuItem, ScrollToToolStripMenuItem, ToolStripSeparator18, MoveFirstToolStripMenuItem, MoveLastToolStripMenuItem, MoveDownToolStripMenuItem, MoveUpToolStripMenuItem, ToolStripSeparator22, PageEditToolStripMenuItem})
        DocumentPageContextMenuStrip.Name = "PageContextMenuStrip"
        DocumentPageContextMenuStrip.Size = New Size(153, 286)
        ' 
        ' AddNewPageToolStripMenuItem
        ' 
        AddNewPageToolStripMenuItem.Name = "AddNewPageToolStripMenuItem"
        AddNewPageToolStripMenuItem.Size = New Size(152, 22)
        AddNewPageToolStripMenuItem.Text = "Add New Page"
        ' 
        ' DeletePageToolStripMenuItem
        ' 
        DeletePageToolStripMenuItem.Name = "DeletePageToolStripMenuItem"
        DeletePageToolStripMenuItem.Size = New Size(152, 22)
        DeletePageToolStripMenuItem.Text = "Delete Page"
        ' 
        ' ToolStripSeparator17
        ' 
        ToolStripSeparator17.Name = "ToolStripSeparator17"
        ToolStripSeparator17.Size = New Size(149, 6)
        ' 
        ' ScrollFirstToolStripMenuItem
        ' 
        ScrollFirstToolStripMenuItem.Name = "ScrollFirstToolStripMenuItem"
        ScrollFirstToolStripMenuItem.Size = New Size(152, 22)
        ScrollFirstToolStripMenuItem.Text = "First Page"
        ' 
        ' ScrollLastToolStripMenuItem
        ' 
        ScrollLastToolStripMenuItem.Name = "ScrollLastToolStripMenuItem"
        ScrollLastToolStripMenuItem.Size = New Size(152, 22)
        ScrollLastToolStripMenuItem.Text = "Last Page"
        ' 
        ' ScrollNextToolStripMenuItem
        ' 
        ScrollNextToolStripMenuItem.Name = "ScrollNextToolStripMenuItem"
        ScrollNextToolStripMenuItem.Size = New Size(152, 22)
        ScrollNextToolStripMenuItem.Text = "Next Page"
        ' 
        ' ScrollPreviousToolStripMenuItem
        ' 
        ScrollPreviousToolStripMenuItem.Name = "ScrollPreviousToolStripMenuItem"
        ScrollPreviousToolStripMenuItem.Size = New Size(152, 22)
        ScrollPreviousToolStripMenuItem.Text = "Previous Page"
        ' 
        ' ScrollToToolStripMenuItem
        ' 
        ScrollToToolStripMenuItem.Name = "ScrollToToolStripMenuItem"
        ScrollToToolStripMenuItem.Size = New Size(152, 22)
        ScrollToToolStripMenuItem.Text = "Scroll To Page"
        ' 
        ' ToolStripSeparator18
        ' 
        ToolStripSeparator18.Name = "ToolStripSeparator18"
        ToolStripSeparator18.Size = New Size(149, 6)
        ' 
        ' MoveFirstToolStripMenuItem
        ' 
        MoveFirstToolStripMenuItem.Name = "MoveFirstToolStripMenuItem"
        MoveFirstToolStripMenuItem.Size = New Size(152, 22)
        MoveFirstToolStripMenuItem.Text = "Move To First"
        ' 
        ' MoveLastToolStripMenuItem
        ' 
        MoveLastToolStripMenuItem.Name = "MoveLastToolStripMenuItem"
        MoveLastToolStripMenuItem.Size = New Size(152, 22)
        MoveLastToolStripMenuItem.Text = "Move To Last"
        ' 
        ' MoveDownToolStripMenuItem
        ' 
        MoveDownToolStripMenuItem.Name = "MoveDownToolStripMenuItem"
        MoveDownToolStripMenuItem.Size = New Size(152, 22)
        MoveDownToolStripMenuItem.Text = "Move Down"
        ' 
        ' MoveUpToolStripMenuItem
        ' 
        MoveUpToolStripMenuItem.Name = "MoveUpToolStripMenuItem"
        MoveUpToolStripMenuItem.Size = New Size(152, 22)
        MoveUpToolStripMenuItem.Text = "Move Up"
        ' 
        ' ToolStripSeparator22
        ' 
        ToolStripSeparator22.Name = "ToolStripSeparator22"
        ToolStripSeparator22.Size = New Size(149, 6)
        ' 
        ' PageEditToolStripMenuItem
        ' 
        PageEditToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {PageCutToolStripMenuItem, PagePasteToolStripMenuItem, PageDeleteToolStripMenuItem, ToolStripSeparator23, PageSelectAllToolStripMenuItem})
        PageEditToolStripMenuItem.Name = "PageEditToolStripMenuItem"
        PageEditToolStripMenuItem.Size = New Size(152, 22)
        PageEditToolStripMenuItem.Text = "Edit"
        ' 
        ' PageCutToolStripMenuItem
        ' 
        PageCutToolStripMenuItem.Enabled = False
        PageCutToolStripMenuItem.Name = "PageCutToolStripMenuItem"
        PageCutToolStripMenuItem.Size = New Size(122, 22)
        PageCutToolStripMenuItem.Text = "Cut"
        ' 
        ' PagePasteToolStripMenuItem
        ' 
        PagePasteToolStripMenuItem.Enabled = False
        PagePasteToolStripMenuItem.Name = "PagePasteToolStripMenuItem"
        PagePasteToolStripMenuItem.Size = New Size(122, 22)
        PagePasteToolStripMenuItem.Text = "Paste"
        ' 
        ' PageDeleteToolStripMenuItem
        ' 
        PageDeleteToolStripMenuItem.Enabled = False
        PageDeleteToolStripMenuItem.Name = "PageDeleteToolStripMenuItem"
        PageDeleteToolStripMenuItem.Size = New Size(122, 22)
        PageDeleteToolStripMenuItem.Text = "Delete"
        ' 
        ' ToolStripSeparator23
        ' 
        ToolStripSeparator23.Name = "ToolStripSeparator23"
        ToolStripSeparator23.Size = New Size(119, 6)
        ' 
        ' PageSelectAllToolStripMenuItem
        ' 
        PageSelectAllToolStripMenuItem.Enabled = False
        PageSelectAllToolStripMenuItem.Name = "PageSelectAllToolStripMenuItem"
        PageSelectAllToolStripMenuItem.Size = New Size(122, 22)
        PageSelectAllToolStripMenuItem.Text = "Select All"
        ' 
        ' PrintPreviewDialog
        ' 
        PrintPreviewDialog.AutoScrollMargin = New Size(0, 0)
        PrintPreviewDialog.AutoScrollMinSize = New Size(0, 0)
        PrintPreviewDialog.ClientSize = New Size(400, 300)
        PrintPreviewDialog.Enabled = True
        PrintPreviewDialog.Icon = CType(resources.GetObject("PrintPreviewDialog.Icon"), Icon)
        PrintPreviewDialog.Name = "PrintPreviewDialog1"
        PrintPreviewDialog.Visible = False
        ' 
        ' ReportsBindingSource
        ' 
        ReportsBindingSource.DataSource = GetType(LibDatabase.Models.Report)
        ReportsBindingSource.Sort = "ReportName"
        ' 
        ' ReportDataBindingSource
        ' 
        ReportDataBindingSource.DataSource = GetType(LibDatabase.Models.JobDetail)
        ' 
        ' DisplayControlContextMenuStrip
        ' 
        DisplayControlContextMenuStrip.Items.AddRange(New ToolStripItem() {BringToFrontToolStripMenuItem, SendToBackToolStripMenuItem, ToolStripSeparator19, UndoToolStripMenuItem, ToolStripSeparator20, CutToolStripMenuItem, PasteToolStripMenuItem, DeleteToolStripMenuItem, ToolStripSeparator21, SelectAllToolStripMenuItem, ToolStripSeparator25, ThemeEditorToolStripMenuItem})
        DisplayControlContextMenuStrip.Name = "DisplayControlContextMenuStrip"
        DisplayControlContextMenuStrip.Size = New Size(181, 226)
        ' 
        ' BringToFrontToolStripMenuItem
        ' 
        BringToFrontToolStripMenuItem.Enabled = False
        BringToFrontToolStripMenuItem.Name = "BringToFrontToolStripMenuItem"
        BringToFrontToolStripMenuItem.Size = New Size(180, 22)
        BringToFrontToolStripMenuItem.Text = "Bring To Front"
        ' 
        ' SendToBackToolStripMenuItem
        ' 
        SendToBackToolStripMenuItem.Enabled = False
        SendToBackToolStripMenuItem.Name = "SendToBackToolStripMenuItem"
        SendToBackToolStripMenuItem.Size = New Size(180, 22)
        SendToBackToolStripMenuItem.Text = "Send To Back"
        ' 
        ' ToolStripSeparator19
        ' 
        ToolStripSeparator19.Name = "ToolStripSeparator19"
        ToolStripSeparator19.Size = New Size(177, 6)
        ' 
        ' UndoToolStripMenuItem
        ' 
        UndoToolStripMenuItem.Enabled = False
        UndoToolStripMenuItem.Name = "UndoToolStripMenuItem"
        UndoToolStripMenuItem.Size = New Size(180, 22)
        UndoToolStripMenuItem.Text = "Undo"
        ' 
        ' ToolStripSeparator20
        ' 
        ToolStripSeparator20.Name = "ToolStripSeparator20"
        ToolStripSeparator20.Size = New Size(177, 6)
        ' 
        ' CutToolStripMenuItem
        ' 
        CutToolStripMenuItem.Enabled = False
        CutToolStripMenuItem.Name = "CutToolStripMenuItem"
        CutToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.X
        CutToolStripMenuItem.Size = New Size(180, 22)
        CutToolStripMenuItem.Text = "Cut"
        ' 
        ' PasteToolStripMenuItem
        ' 
        PasteToolStripMenuItem.Enabled = False
        PasteToolStripMenuItem.Name = "PasteToolStripMenuItem"
        PasteToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.V
        PasteToolStripMenuItem.Size = New Size(180, 22)
        PasteToolStripMenuItem.Text = "Paste"
        ' 
        ' DeleteToolStripMenuItem
        ' 
        DeleteToolStripMenuItem.Enabled = False
        DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem"
        DeleteToolStripMenuItem.ShortcutKeys = Keys.Delete
        DeleteToolStripMenuItem.Size = New Size(180, 22)
        DeleteToolStripMenuItem.Text = "Delete"
        ' 
        ' ToolStripSeparator21
        ' 
        ToolStripSeparator21.Name = "ToolStripSeparator21"
        ToolStripSeparator21.Size = New Size(177, 6)
        ' 
        ' SelectAllToolStripMenuItem
        ' 
        SelectAllToolStripMenuItem.Enabled = False
        SelectAllToolStripMenuItem.Name = "SelectAllToolStripMenuItem"
        SelectAllToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.A
        SelectAllToolStripMenuItem.Size = New Size(180, 22)
        SelectAllToolStripMenuItem.Text = "Select All"
        ' 
        ' ToolStripSeparator25
        ' 
        ToolStripSeparator25.Name = "ToolStripSeparator25"
        ToolStripSeparator25.Size = New Size(177, 6)
        ' 
        ' ThemeEditorToolStripMenuItem
        ' 
        ThemeEditorToolStripMenuItem.Name = "ThemeEditorToolStripMenuItem"
        ThemeEditorToolStripMenuItem.Size = New Size(180, 22)
        ThemeEditorToolStripMenuItem.Text = "Theme Editor"
        ' 
        ' OpenFileDialog1
        ' 
        OpenFileDialog1.FileName = "OpenFileDialog1"
        ' 
        ' HeaderMenuStrip
        ' 
        HeaderMenuStrip.Items.AddRange(New ToolStripItem() {HeaderBorderStyleMenuItem})
        HeaderMenuStrip.Name = "HeaderMenuStrip"
        HeaderMenuStrip.Size = New Size(138, 26)
        ' 
        ' HeaderBorderStyleMenuItem
        ' 
        HeaderBorderStyleMenuItem.DropDownItems.AddRange(New ToolStripItem() {BorderStyleNoneMenuItem, BorderStyleFixedSingleMenuItem, BorderStyleFixed3DMenuItem})
        HeaderBorderStyleMenuItem.Name = "HeaderBorderStyleMenuItem"
        HeaderBorderStyleMenuItem.Size = New Size(137, 22)
        HeaderBorderStyleMenuItem.Text = "Border Style"
        ' 
        ' BorderStyleNoneMenuItem
        ' 
        BorderStyleNoneMenuItem.Checked = True
        BorderStyleNoneMenuItem.CheckOnClick = True
        BorderStyleNoneMenuItem.CheckState = CheckState.Checked
        BorderStyleNoneMenuItem.Name = "BorderStyleNoneMenuItem"
        BorderStyleNoneMenuItem.Size = New Size(136, 22)
        BorderStyleNoneMenuItem.Text = "None"
        ' 
        ' BorderStyleFixedSingleMenuItem
        ' 
        BorderStyleFixedSingleMenuItem.CheckOnClick = True
        BorderStyleFixedSingleMenuItem.Name = "BorderStyleFixedSingleMenuItem"
        BorderStyleFixedSingleMenuItem.Size = New Size(136, 22)
        BorderStyleFixedSingleMenuItem.Text = "Fixed Single"
        ' 
        ' BorderStyleFixed3DMenuItem
        ' 
        BorderStyleFixed3DMenuItem.CheckOnClick = True
        BorderStyleFixed3DMenuItem.Name = "BorderStyleFixed3DMenuItem"
        BorderStyleFixed3DMenuItem.Size = New Size(136, 22)
        BorderStyleFixed3DMenuItem.Text = "Fixed 3D"
        ' 
        ' LetterheadMenuStrip
        ' 
        LetterheadMenuStrip.Items.AddRange(New ToolStripItem() {LetterheadImageMenuItem, ToolStripSeparator24, LetterheadSizeModeMenuItem, LetterheadBorderStyleMenuItem})
        LetterheadMenuStrip.Name = "LetterheadMenuStrip"
        LetterheadMenuStrip.Size = New Size(138, 76)
        ' 
        ' LetterheadImageMenuItem
        ' 
        LetterheadImageMenuItem.Name = "LetterheadImageMenuItem"
        LetterheadImageMenuItem.Size = New Size(137, 22)
        LetterheadImageMenuItem.Text = "Image"
        ' 
        ' ToolStripSeparator24
        ' 
        ToolStripSeparator24.Name = "ToolStripSeparator24"
        ToolStripSeparator24.Size = New Size(134, 6)
        ' 
        ' LetterheadSizeModeMenuItem
        ' 
        LetterheadSizeModeMenuItem.DropDownItems.AddRange(New ToolStripItem() {SizeModeNormalMenuItem, SizeModeStretchMenuItem, SizeModeAutoSizeMenuItem, SizeModeCenterMenuItem, SizeModeZoomMenuItem})
        LetterheadSizeModeMenuItem.Name = "LetterheadSizeModeMenuItem"
        LetterheadSizeModeMenuItem.Size = New Size(137, 22)
        LetterheadSizeModeMenuItem.Text = "Size Mode"
        ' 
        ' SizeModeNormalMenuItem
        ' 
        SizeModeNormalMenuItem.CheckOnClick = True
        SizeModeNormalMenuItem.Name = "SizeModeNormalMenuItem"
        SizeModeNormalMenuItem.Size = New Size(120, 22)
        SizeModeNormalMenuItem.Text = "Normal"
        ' 
        ' SizeModeStretchMenuItem
        ' 
        SizeModeStretchMenuItem.CheckOnClick = True
        SizeModeStretchMenuItem.Name = "SizeModeStretchMenuItem"
        SizeModeStretchMenuItem.Size = New Size(120, 22)
        SizeModeStretchMenuItem.Text = "Stretch"
        ' 
        ' SizeModeAutoSizeMenuItem
        ' 
        SizeModeAutoSizeMenuItem.CheckOnClick = True
        SizeModeAutoSizeMenuItem.Name = "SizeModeAutoSizeMenuItem"
        SizeModeAutoSizeMenuItem.Size = New Size(120, 22)
        SizeModeAutoSizeMenuItem.Text = "AutoSize"
        ' 
        ' SizeModeCenterMenuItem
        ' 
        SizeModeCenterMenuItem.CheckOnClick = True
        SizeModeCenterMenuItem.Name = "SizeModeCenterMenuItem"
        SizeModeCenterMenuItem.Size = New Size(120, 22)
        SizeModeCenterMenuItem.Text = "Center"
        ' 
        ' SizeModeZoomMenuItem
        ' 
        SizeModeZoomMenuItem.CheckOnClick = True
        SizeModeZoomMenuItem.Name = "SizeModeZoomMenuItem"
        SizeModeZoomMenuItem.Size = New Size(120, 22)
        SizeModeZoomMenuItem.Text = "Zoom"
        ' 
        ' LetterheadBorderStyleMenuItem
        ' 
        LetterheadBorderStyleMenuItem.DropDownItems.AddRange(New ToolStripItem() {LetterheadBorderStyleNoneMenuItem, LetterheadBorderStyleFixedSingleMenuItem, LetterheadBorderStyleFixed3DMenuItem})
        LetterheadBorderStyleMenuItem.Name = "LetterheadBorderStyleMenuItem"
        LetterheadBorderStyleMenuItem.Size = New Size(137, 22)
        LetterheadBorderStyleMenuItem.Text = "Border Style"
        ' 
        ' LetterheadBorderStyleNoneMenuItem
        ' 
        LetterheadBorderStyleNoneMenuItem.Name = "LetterheadBorderStyleNoneMenuItem"
        LetterheadBorderStyleNoneMenuItem.Size = New Size(136, 22)
        LetterheadBorderStyleNoneMenuItem.Text = "None"
        ' 
        ' LetterheadBorderStyleFixedSingleMenuItem
        ' 
        LetterheadBorderStyleFixedSingleMenuItem.Name = "LetterheadBorderStyleFixedSingleMenuItem"
        LetterheadBorderStyleFixedSingleMenuItem.Size = New Size(136, 22)
        LetterheadBorderStyleFixedSingleMenuItem.Text = "Fixed Single"
        ' 
        ' LetterheadBorderStyleFixed3DMenuItem
        ' 
        LetterheadBorderStyleFixed3DMenuItem.Name = "LetterheadBorderStyleFixed3DMenuItem"
        LetterheadBorderStyleFixed3DMenuItem.Size = New Size(136, 22)
        LetterheadBorderStyleFixed3DMenuItem.Text = "Fixed 3D"
        ' 
        ' PrintDocument
        ' 
        ' 
        ' FrmReports
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1050, 844)
        Controls.Add(ToolStripContainer1)
        Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        KeyPreview = True
        Name = "FrmReports"
        Text = "FrmReports"
        ToolStripContainer1.ContentPanel.ResumeLayout(False)
        ToolStripContainer1.TopToolStripPanel.ResumeLayout(False)
        ToolStripContainer1.TopToolStripPanel.PerformLayout()
        ToolStripContainer1.ResumeLayout(False)
        ToolStripContainer1.PerformLayout()
        FormMenuStrip.ResumeLayout(False)
        FormMenuStrip.PerformLayout()
        DocumentPageContextMenuStrip.ResumeLayout(False)
        CType(ReportsBindingSource, ComponentModel.ISupportInitialize).EndInit()
        CType(ReportDataBindingSource, ComponentModel.ISupportInitialize).EndInit()
        DisplayControlContextMenuStrip.ResumeLayout(False)
        HeaderMenuStrip.ResumeLayout(False)
        LetterheadMenuStrip.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents ToolStripContainer1 As ToolStripContainer
    Friend WithEvents FormMenuStrip As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FileNewToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator11 As ToolStripSeparator
    Friend WithEvents FileOpenToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FileRecentToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents FileCloseToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents FileSaveToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FileSaveAsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents FilePrintToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PrintToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PrintPreviewToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PageSetupToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents FileExitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents EditToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents EditCutToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents EditCopyToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents EditPasteToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents EditDeleteToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator9 As ToolStripSeparator
    Friend WithEvents EditSelectAllToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents JobsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents JobsOpenToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents JobsRecentToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator13 As ToolStripSeparator
    Friend WithEvents JobsCloseToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ReportsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ReportsEditToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ReportsImportToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ReportsExportToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator12 As ToolStripSeparator
    Friend WithEvents ElementsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ElementsLetterheadToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ElementsHeaderToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator16 As ToolStripSeparator
    Friend WithEvents SettingsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ClassToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ClassSpecialToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ClassIToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ClasasIIToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ClassIIIToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BasisToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents MeanToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents MarkedToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DesiredToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PrecisionToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Precision00ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Precision000ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ViewToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ViewPagesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator14 As ToolStripSeparator
    Friend WithEvents GridSizeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents GridSizeToolStripTextBox As ToolStripTextBox
    Friend WithEvents ToolStripSeparator8 As ToolStripSeparator
    Friend WithEvents ZoomInToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ZoomOutToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ActualSizeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DocumentPageContextMenuStrip As ContextMenuStrip
    Friend WithEvents AddNewPageToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DeletePageToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator17 As ToolStripSeparator
    Friend WithEvents ScrollFirstToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ScrollLastToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ScrollNextToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ScrollPreviousToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ScrollToToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator18 As ToolStripSeparator
    Friend WithEvents MoveFirstToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents MoveLastToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents MoveDownToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents MoveUpToolStripMenuItem As ToolStripMenuItem
    'Friend WithEvents PrintDocument As Printing.PrintDocument
    Friend WithEvents PrintPreviewDialog As PrintPreviewDialog
    Friend WithEvents PageSetupDialog As PageSetupDialog
    Friend WithEvents ToolStripSeparator5 As ToolStripSeparator
    Friend WithEvents LetterheadShowAllToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HeaderShowAllToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator6 As ToolStripSeparator
    Friend WithEvents EditUndoToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator7 As ToolStripSeparator
    Friend WithEvents ReportsBindingSource As BindingSource
    Friend WithEvents ReportDataBindingSource As BindingSource
    Friend WithEvents HeaderItemsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripFileRecentSeparator As ToolStripSeparator
    Friend WithEvents RecentReportsClearListToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator15 As ToolStripSeparator
    Friend WithEvents JobsRecentClearListToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DisplayControlContextMenuStrip As ContextMenuStrip
    Friend WithEvents BringToFrontToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SendToBackToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator19 As ToolStripSeparator
    Friend WithEvents UndoToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator20 As ToolStripSeparator
    Friend WithEvents CutToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PasteToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DeleteToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator21 As ToolStripSeparator
    Friend WithEvents SelectAllToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ReportViewer1 As ReportViewer
    Friend WithEvents ToolStripSeparator22 As ToolStripSeparator
    Friend WithEvents PageEditToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PageCutToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PagePasteToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PageDeleteToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator23 As ToolStripSeparator
    Friend WithEvents PageSelectAllToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents HeaderMenuStrip As ContextMenuStrip
    Friend WithEvents HeaderBorderStyleMenuItem As ToolStripMenuItem
    Friend WithEvents BorderStyleNoneMenuItem As ToolStripMenuItem
    Friend WithEvents BorderStyleFixedSingleMenuItem As ToolStripMenuItem
    Friend WithEvents BorderStyleFixed3DMenuItem As ToolStripMenuItem
    Friend WithEvents LetterheadMenuStrip As ContextMenuStrip
    Friend WithEvents LetterheadImageMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator24 As ToolStripSeparator
    Friend WithEvents LetterheadSizeModeMenuItem As ToolStripMenuItem
    Friend WithEvents SizeModeNormalMenuItem As ToolStripMenuItem
    Friend WithEvents SizeModeStretchMenuItem As ToolStripMenuItem
    Friend WithEvents SizeModeAutoSizeMenuItem As ToolStripMenuItem
    Friend WithEvents SizeModeCenterMenuItem As ToolStripMenuItem
    Friend WithEvents SizeModeZoomMenuItem As ToolStripMenuItem
    Friend WithEvents LetterheadBorderStyleMenuItem As ToolStripMenuItem
    Friend WithEvents LetterheadBorderStyleNoneMenuItem As ToolStripMenuItem
    Friend WithEvents LetterheadBorderStyleFixedSingleMenuItem As ToolStripMenuItem
    Friend WithEvents LetterheadBorderStyleFixed3DMenuItem As ToolStripMenuItem
    Friend WithEvents PrintDocument As Drawing.Printing.PrintDocument
    Friend WithEvents ToolStripSeparator25 As ToolStripSeparator
    Friend WithEvents ThemeEditorToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ReportsToolStripRecentSeparator As ToolStripSeparator

End Class
