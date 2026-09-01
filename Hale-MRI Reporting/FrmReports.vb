Imports System.Collections.ObjectModel
Imports System.Collections.Specialized
Imports System.ComponentModel
Imports System.Drawing.Printing
Imports LibDatabase
Imports LibDatabase.BindingSourceExtensions
Imports LibDatabase.Contexts
Imports LibDatabase.Models
Imports LibGlobals
Imports Microsoft.Extensions.DependencyInjection

Public Class FrmReports
    Inherits FrmDatabaseForm

#Region "Types and Constants"
    Private Const kRecentMenusCountMax As Integer = 10
#End Region
#Region "Private Members"
    Private mBasis As String = Nothing                                  ' The current basis for tolerance calculations, e.g. "Federal", "ISO", "Local", etc. This is used to set the DisplayControls' Basis property.  
    'Private ReadOnly mDatabase As HaleMRIContext                        ' The current database context.
    Private mDocumentSettings As DocumentSettings = Nothing             ' The current printer settings used to render our DocumentPages.
    Private mJobDetails As JobDetail = Nothing                          ' The current JobDetail record from which report data is retrieved.
    Private mPageSettings As PageSettings = Nothing                     ' The current page setup PageSettings, if any.
    Private mPrintPageIndex As Integer = 0                              ' Index of the DocumentPage currently being printed.
    Private mPrinterSettings As PrinterSettings = Nothing               ' The current PrinterSettings, if any.
    Private mPrecision As Integer? = Nothing                            ' The current precision for displaying measurement values, if any. This is used to set the DisplayControls' Precision property.
    Private mReport As Report = Nothing                                 ' The currently open Report, if any.
    'Private ReadOnly mServiceProvider As IServiceProvider               ' The current database ServiceProvider reference.
    Private mTolClass As Tolerance = Nothing                            ' The current Tolerance class, if any. This is used to set the DisplayControls' TolClass property.
#End Region
#Region "Constructors"
    ' Visual Studio Designer uses this.
    Public Sub New()
        MyBase.New()
        InitializeComponent()
    End Sub

    ' DI Container uses this at runtime.
    Public Sub New(context As HaleMRIContext, serviceProvider As IServiceProvider, scopeFactory As IServiceScopeFactory)
        MyBase.New(context, serviceProvider, scopeFactory)
        InitializeComponent()
    End Sub

#End Region
#Region "Public Interface"
    Public Property Document As DocumentSettings
        Get
            Return mDocumentSettings
        End Get
        Set(value As DocumentSettings)
            DocumentSet(value)
            mDocumentSettings = value
        End Set
    End Property

    Public Property JobDetails As JobDetail
        ' TODO: Lose the BindingSources since we're not binding any controls
        ' to the data, and instead find the record in mJobDetails,
        ' which is the LocalView.
        Get
            Return mJobDetails
        End Get
        Set(value As JobDetail)
            If value IsNot mJobDetails Then
                JobClose(mJobDetails)
                If value Is Nothing OrElse ReportDataBindingSource.Find(Of JobDetail)("Id", value.Id) IsNot Nothing Then
                    JobLoad(value)
                End If
                mJobDetails = value
            End If
        End Set
    End Property

    Public Property Report As Report
        ' TODO: See JobDetails.
        Get
            Return mReport
        End Get
        Set(value As Report)
            If ReportClose(mReport) Then
                ReportLoad(value)
                mReport = value
            End If
        End Set
    End Property
#End Region
#Region "Private Interface"
    Private Property Basis As String
        Get
            Return mBasis
        End Get
        Set(value As String)
            For Each dc As DisplayControl In ReportViewer1.DisplayControls
                dc.Basis = value
            Next
            mBasis = value
        End Set
    End Property

    Private Property Precision As Integer?
        Get
            Return mPrecision
        End Get
        Set(value As Integer?)
            For Each dc As DisplayControl In ReportViewer1.DisplayControls
                dc.Precision = value
            Next
            mPrecision = value
        End Set
    End Property

    Private Property TolClass As Tolerance
        Get
            Return mTolClass
        End Get
        Set(value As Tolerance)
            For Each dc As DisplayControl In ReportViewer1.DisplayControls
                dc.TolClass = value
            Next
            mTolClass = value
        End Set
    End Property

    Private Sub BasisChanged(ByVal basisItem As ToolStripMenuItem)
        Me.Basis = basisItem.Text
        For Each item As ToolStripMenuItem In BasisToolStripMenuItem.DropDownItems
            If item IsNot basisItem Then item.Checked = False
        Next
    End Sub

    Private Sub BindDataSources()
        ' Check any required local tables and load them if not already loaded.
        If Not Me.Database.JobDetails.Local.Any() Then
            LoadJobDetails(Me.Database)
        End If
        If Not Me.Database.Reports.Local.Any() Then
            LoadReports(Me.Database)
        End If

        ' Load BindingSources from the LocalView.
        ReportsBindingSource.DataSource = Me.Database.Reports.Local.ToBindingList()
        ReportDataBindingSource.DataSource = Me.Database.JobDetails.Local.ToBindingList()
    End Sub

    Private Sub ClassChanged(ByVal classItem As ToolStripMenuItem)
        Me.TolClass = GetToleranceTable(Me.Database, classItem.Text)
        For Each item As ToolStripMenuItem In ClassToolStripMenuItem.DropDownItems
            If item IsNot classItem Then item.Checked = False
        Next
    End Sub

    Private Sub DisplayControlSizeInitialize(ByVal dc As DisplayControl, ByVal sz As Size)
        ' Band-aid fix for Windoze layout bug. We have to change the desired size (sz)
        ' to something else before assigning it to the DisplayControl. This forces a
        ' correct layout. If we don't do this, its children (charts, panels, etc.) will 
        ' not respect the control's padding and, for certain sizes, render "offset", and
        ' often obscure part of the selection border (which uses the DisplayControl's 
        ' SelectionBorderSize property to set its Padding so the border is fully visible).
        ' According to Windoze docs, setting the DisplayControl's Padding and the child's
        ' Dock = Fill should solve this, but it doesn't and the layout problem only occurs
        ' for certain sizes???
        dc.Size = New Size(sz.Width + 1, sz.Height + 1)
        dc.Size = sz
    End Sub

    Private Sub DisplayControlToggle(displayControlItem As ToolStripMenuItem)
        If displayControlItem.Checked Then
            ' DisplayControls are instantiated by the CreateInstance() factory function
            ' according to their type name. Add the instance to the ReportViewer's current ReportPage.
            Try
                Dim dc As DisplayControl = DisplayControl.CreateInstance($"{Me.GetType().Namespace}.{displayControlItem.Name}")

                DisplayControlSizeInitialize(dc, dc.Size)                                       ' Set the size so the selection border and dragging work.
                If dc.ContextMenuStrip IsNot Nothing Then dc.ContextMenuStrip.Enabled = True    ' Enable the ContextMenuStrip.
                ReportViewer1.Pages(ReportViewer1.CurrentPageIndex).DisplayControls.Add(dc)     ' The DocumentPage will position the control automatically.
                dc.Basis = Me.Basis                                                             ' Set the current basis for tolerance calculations.
                dc.TolClass = Me.TolClass                                                       ' Set the current tolerance class.
                dc.Precision = Me.Precision                                                     ' Set the current precision.
                dc.Data = Me.JobDetails                                                         ' Set the current JobDetails data.
                dc.BringToFront()
            Catch ex As Exception
                MessageBox.Show($"Error creating DisplayControl '{displayControlItem.Name}': {ex.Message}", "DisplayControl Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            ' Remove the first matching DisplayControl from the ReportViewer.
            Dim dc As DisplayControl = ReportViewer1.DisplayControls.FirstOrDefault(Function(ctrl) ctrl.Name = displayControlItem.Name)
            If dc IsNot Nothing Then ReportViewer1.DisplayControls.Remove(dc)
        End If
    End Sub

    Private Sub DocumentSet(ByVal doc As DocumentSettings)
        ReportViewer1.Document = doc
    End Sub

    Private Sub EditMenuDropDownOpening(ByVal editMenu As ToolStripMenuItem)
        EditCopyToolStripMenuItem.Enabled = False
        EditCutToolStripMenuItem.Enabled = ReportViewer1.SelectedControls.Count > 0
        EditDeleteToolStripMenuItem.Enabled = ReportViewer1.SelectedControls.Count > 0
        EditPasteToolStripMenuItem.Enabled = ReportViewer1.ClipBoard.Count > 0
        EditSelectAllToolStripMenuItem.Enabled = ReportViewer1.DisplayControls.Count > 0
        EditUndoToolStripMenuItem.Enabled = ReportViewer1.UndoStack.Count > 0
    End Sub

    Private Sub ElementsMenuInitialize()
        ' Populate the Elements menu dropdown list with all available DisplayControls:
        ' New Tuple(Of String, String)("DisplayName", "TypeName").
        ' DisplayName is the menu item text and TypeName is the DisplayControl's type
        ' name that is used to instantiate it.
        ' ***********************************************************************
        ' *** This is the Master List of all available Report DisplayControls ***
        ' *** Newly designed controls need to be added to this list so they   ***
        ' *** appear in the form's dropdown lists and can be selected by the  ***
        ' *** user at runtime.                                                ***
        ' ***********************************************************************
        Dim elements() As Tuple(Of String, String) = {
            New Tuple(Of String, String)("Angular Position", "ChartAngularPosition"),
            New Tuple(Of String, String)("Bore Inspect", "BoreInspectTable"),
            New Tuple(Of String, String)("Blade Height", "ChartBladeHeight"),
            New Tuple(Of String, String)("Blade Averages", "ChartBladeAverage"),
            New Tuple(Of String, String)("Blades By Sector", "ChartBladesBySector"),
            New Tuple(Of String, String)("Chord Length", "ChordLengthTable"),
            New Tuple(Of String, String)("Comp Line", "ChartCompLine"),
            New Tuple(Of String, String)("Federal Tolerance", "FederalToleranceTable"),
            New Tuple(Of String, String)("ISO Tolerance", "ISOToleranceTable"),
            New Tuple(Of String, String)("Local Pitch", "LocalPitchTableReport"),
            New Tuple(Of String, String)("Manual Inspection", "ManualInspectionTable"),
            New Tuple(Of String, String)("Michigan Tolerance", "MichiganToleranceTable"),
            New Tuple(Of String, String)("Radii Averages", "RadiiAveragesTable"),
            New Tuple(Of String, String)("Sectors By Blade", "ChartSectorsByBlade"),
            New Tuple(Of String, String)("Skew Table", "SkewTable"),
            New Tuple(Of String, String)("Standard Tolerance", "StandardToleranceTable"),
            New Tuple(Of String, String)("Summary", "ChartSummary"),
            New Tuple(Of String, String)("Testing Tolerance", "TestingToleranceTable"),
            New Tuple(Of String, String)("Chart Designer Canvas", "ChartDesignerCanvas")
        }
        For Each item As Tuple(Of String, String) In elements
            Dim menuItem As New ToolStripMenuItem(item.Item1) With {.Name = item.Item2}
            menuItem.CheckOnClick = True
            AddHandler menuItem.Click, AddressOf Me.ElementsDisplayControlToolStripMenuItem_Click
            Me.ElementsToolStripMenuItem.DropDownItems.Add(menuItem)
        Next
    End Sub

    Private Sub ElementsMenuItemsClear()
        ' Uncheck all Elements menu items.
        For Each menuItem As ToolStripMenuItem In ElementsToolStripMenuItem.DropDownItems.OfType(Of ToolStripMenuItem)()
            menuItem.Checked = False
        Next
        For Each menuItem As ToolStripMenuItem In HeaderItemsToolStripMenuItem.DropDownItems
            menuItem.Checked = False
        Next
    End Sub

    Private Sub FileRecentDropDownOpening(menuItem As ToolStripMenuItem)
        ' Enable the FileRecentClearList menu item if there are any items in the list.
        Dim item As ToolStripItem = menuItem.DropDownItems.Find("RecentReportsClearListToolStripMenuItem", False).FirstOrDefault()
        If item IsNot Nothing Then
            item.Enabled = menuItem.DropDownItems.Count > 2 ' The separator and Clear List, the last two list items, are always present.
        End If
    End Sub

    Private Sub FormTextUpdate(ByVal rpt As Report, ByVal jd As JobDetail)
        Dim reportName As String = rpt?.ReportName
        Dim jobNumber As String = jd?.Job?.JobNumber.ToString()
        Dim measurementType As String = jd?.MeasurementType?.MeasurementType1
        Dim sepReport As String = If(Not (String.IsNullOrEmpty(reportName) OrElse String.IsNullOrEmpty(jobNumber)), ", ", "")
        Dim sepJob As String = If(Not (String.IsNullOrEmpty(jobNumber) OrElse String.IsNullOrEmpty(measurementType)), " - ", "")

        Me.Text = $"{reportName}{sepReport}{jobNumber}{sepJob}{measurementType}"
    End Sub

    Private Sub GridSizeSet(ByRef gridSizeItem As ToolStripMenuItem, e As EventArgs)
        Dim txtGridSize = gridSizeItem.DropDownItems.Item("GridSizeToolStripTextBox")
        Dim value As Integer = Integer.Parse(txtGridSize.Text)
        If ReportViewer1.GridSize <> value Then
            ReportViewer1.GridSize = value
            txtGridSize.Text = ReportViewer1.GridSize.ToString()
        End If
    End Sub

    Private Sub HeaderVisibleChanged(ByVal headerItem As ToolStripMenuItem)
        ' Header visibility is handled according to headerItem.Checked state.
        ReportViewer1.Header.Visible = headerItem.Checked
        ElementsHeaderToolStripMenuItem.DropDown.Enabled = headerItem.Checked
    End Sub

    Private Sub HeaderBorderStyleChanged(ByVal borderStyle As ToolStripMenuItem)
        For Each item As ToolStripItem In HeaderBorderStyleMenuItem.DropDownItems
            If TypeOf item Is ToolStripMenuItem AndAlso item.Name <> borderStyle.Name Then
                DirectCast(item, ToolStripMenuItem).Checked = False
            End If
        Next
        If ReportViewer1.Letterhead IsNot Nothing Then
            Select Case borderStyle.Name
                Case "BorderStyleNoneMenuItem"
                    ReportViewer1.Header.BorderStyle = System.Windows.Forms.BorderStyle.None
                Case "BorderStyleFixedSingleMenuItem"
                    ReportViewer1.Header.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
                Case "BorderStyleFixed3DMenuItem"
                    ReportViewer1.Header.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
            End Select
        End If
    End Sub

    Private Function HeaderDataSourceSet() As BindingList(Of HeaderView)
        Dim headerList = Me.Database.JobDetails.Local.
            Select(Function(jd) New HeaderView() With {
                .Id = jd.Id,
                .JobNumber = jd.Job.JobNumber,
                .Description = jd.Job.Description,
                .ScanDate = jd.StartDate,
                .PerformedByName = If(jd.PerformedByNavigation IsNot Nothing, jd.PerformedByNavigation.EmployeeName, ""),
                .InspectedByName = If(jd.Job.InspectedByNavigation IsNot Nothing, jd.Job.InspectedByNavigation.EmployeeName, ""),
                .FileName = If(jd.FileName, ""),
                .ManufacturerName = If(jd.Job.PropellerManufacturer IsNot Nothing, jd.Job.PropellerManufacturer.ManufacturerName, ""),
                .PartNumber = If(jd.Job.PropellerPartNumber, ""),
                .SerialNumber = If(jd.Job.PropellerPartNumber, ""),
                .StampNumber = If(jd.Job.StampNumber, ""),
                .Blades = jd.Job.PropellerBlades,
                .Bore = If(jd.Job.PropellerBore, ""),
                .Cup = jd.Job.Cup,
                .CustomerName = If(jd.Job.Vessel.Customer.CustomerName, ""),
                .Dar = jd.Job.Dar,
                .DesiredPitch = jd.Job.DesiredPitch,
                .MarkedDiameter = jd.Job.PropellerDiameter,
                .MarkedPitch = 0.0F,
                .Material = If(jd.Job.PropellerMaterial, ""),
                .MeasuredDiameter = 0.0F,
                .RepairStatus = If(jd.ToleranceClass, ""),
                .Rotation = If(jd.Job.PropellerRotation, ""),
                .Style = If(jd.Job.PropellerStyle, ""),
                .ToleranceClass = If(jd.ToleranceClass IsNot Nothing, jd.ToleranceClass, ""),
                .VesselName = If(jd.Job.Vessel.VesselName, ""),
                .WheelPitch = jd.WheelPitch
            })
        Return New BindingList(Of HeaderView)(headerList.ToList())
    End Function

    Private Sub HeaderItemsChanged(ByVal headerItem As ToolStripMenuItem)
        ' Add/remove header items according to the checked state of the headerItem.
        If headerItem.Checked Then
            ReportViewer1.Header.VisibleControls.Add(ReportViewer1.Header.Item(headerItem.Text))
        Else
            ReportViewer1.Header.VisibleControls.Remove(ReportViewer1.Header.Item(headerItem.Text))
        End If
    End Sub

    Private Sub HeaderMenuInitialize()
        ' Populate the ElementsHeaderItems dropdown list with available header items.
        With ReportViewer1.Header
            Dim unused = .Handle    ' Force the ReportHeader to create a handle so its initialization code runs.
            For Each item As ReportHeader.HeaderControl In .HeaderControls
                Dim headerItem As New ToolStripMenuItem() With {.Name = item.Name, .Text = item.Name}
                headerItem.CheckOnClick = True
                AddHandler headerItem.Click, AddressOf Me.HeaderItem_Click
                HeaderItemsToolStripMenuItem.DropDownItems.Add(headerItem)
            Next
        End With
    End Sub

    Private Sub HeaderShowAllChanged(ByVal headerItem As ToolStripMenuItem)
        ReportViewer1.HeaderShowOnAllPages = headerItem.Checked
    End Sub

    Private Sub JobClose(ByRef jd As JobDetail)
        jd = Nothing
        ReportDataSet(jd)
        JobMenuItemsSet(jd)
        FormTextUpdate(Me.Report, jd)
    End Sub

    Private Sub JobClose(jobItem As ToolStripMenuItem)
        JobClose(mJobDetails)
    End Sub

    Private Sub JobLoad(jd As JobDetail)
        If jd IsNot Nothing Then
            ' Load the Radius, Cell and ExtremeMmeasurements records related to the JobDetail if they aren't already loaded.
            ' We have to do this manually since these records aren't automatically included when we load the JobDetails table,
            ' due to the large amount of data in these tables. LoadMeasurements() loads the records AsNoTracking() into the 
            ' LocalView so we don't have to manually dispose of them each time a new JobDetail record is selected.
            If jd.RadiusMeasurements Is Nothing OrElse jd.RadiusMeasurements.Count = 0 Then
                ' Manually link the results to the JobDetail object.
                jd.RadiusMeasurements = LoadMeasurements(Me.Database, jd)
                RecentJobsAdd(jd.Id.ToString())
                ' Notify the UI that the measurements are now available.
                ReportDataBindingSource.ResetCurrentItem()
            End If
        End If
        ReportDataSet(jd)
        JobMenuItemsSet(jd)
        FormTextUpdate(Me.Report, jd)
    End Sub

    Private Sub JobMenuItemsSet(ByVal jd As JobDetail)
        JobsCloseToolStripMenuItem.Enabled = jd IsNot Nothing
    End Sub

    Private Sub JobOpen(ByVal menuItem As ToolStripMenuItem, ByVal pick As Boolean)
        Dim jd As JobDetail = Nothing
        If pick Then
            ' TODO:
            Dim frm As FrmMeasurementPicker = DirectCast(ShowFormModal(Of FrmMeasurementPicker)(Me.ScopeFactory, Me.User), FrmMeasurementPicker)
            'Dim frm As New FrmMeasurementPicker(Me.Database, mServiceProvider)
            If frm.ShowDialog() = DialogResult.OK Then
                jd = Me.Database.JobDetails.Local.FirstOrDefault(Function(j) j.Id = frm.Current?.Id)
            End If
        Else
            jd = Me.Database.JobDetails.Local.FirstOrDefault(Function(j) j.Id = menuItem.Tag)
        End If
        If jd IsNot Nothing Then Me.JobDetails = jd
    End Sub

    Private Sub LetterheadBorderStyleChanged(ByVal borderStyle As ToolStripMenuItem)
        ' Uncheck all dropdown items except the one clicked.
        For Each item As ToolStripItem In LetterheadBorderStyleMenuItem.DropDownItems
            If TypeOf item Is ToolStripMenuItem AndAlso item.Name <> borderStyle.Name Then
                DirectCast(item, ToolStripMenuItem).Checked = False
            End If
        Next

        ' Set the Letterhead.BorderStyle according the clicked item's name.
        If ReportViewer1.Letterhead IsNot Nothing Then
            Select Case borderStyle.Name
                Case "LetterheadBorderStyleNoneMenuItem"
                    ReportViewer1.Letterhead.BorderStyle = System.Windows.Forms.BorderStyle.None
                Case "LetterheadBorderStyleFixedSingleMenuItem"
                    ReportViewer1.Letterhead.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
                Case "LetterheadBorderStyleFixed3DMenuItem"
                    ReportViewer1.Letterhead.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
            End Select
        End If
    End Sub

    Private Sub LetterheadImageSelect()
        With OpenFileDialog1
            .Title = STR_DIALOG_PROMPT_IMAGE_SELECT
            .InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
            .Filter = STR_DIALOG_FILTER_IMAGE
            .FilterIndex = 1
            .Multiselect = False
        End With

        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            ' Get the selected file path.
            Dim selectedFile As String = OpenFileDialog1.FileName
            ' Load the image into the ReportViewer.Letterhead
            ReportViewer1.Letterhead.ImageLocation = selectedFile
        End If
    End Sub

    Private Sub LetterheadMenuItemsSet()

    End Sub

    Private Sub LetterheadSizeModeChange(ByVal sizeMode As ToolStripMenuItem)
        For Each item As ToolStripItem In LetterheadImageMenuItem.DropDownItems
            If TypeOf item Is ToolStripMenuItem AndAlso item.Name <> sizeMode.Name Then
                DirectCast(item, ToolStripMenuItem).Checked = False
            End If
        Next
        If ReportViewer1.Letterhead IsNot Nothing Then
            Select Case sizeMode.Name
                Case "SizeModeNormalMenuItem"
                    ReportViewer1.Letterhead.SizeMode = PictureBoxSizeMode.Normal
                Case "SizeModeCenterMenuItem"
                    ReportViewer1.Letterhead.SizeMode = PictureBoxSizeMode.CenterImage
                Case "SizeModeStretchMenuItem"
                    ReportViewer1.Letterhead.SizeMode = PictureBoxSizeMode.StretchImage
                Case "SizeModeAutoSizeMenuItem"
                    ReportViewer1.Letterhead.SizeMode = PictureBoxSizeMode.AutoSize
                Case "SizeModeZoomMenuItem"
                    ReportViewer1.Letterhead.SizeMode = PictureBoxSizeMode.Zoom
            End Select
        End If
    End Sub

    Private Sub LetterheadVisibleChanged(ByVal letterheadItem As ToolStripMenuItem)
        ReportViewer1.Letterhead.Visible = letterheadItem.Checked
        ElementsLetterheadToolStripMenuItem.DropDown.Enabled = letterheadItem.Checked
        LetterheadMenuStrip.Enabled = letterheadItem.Checked
    End Sub


    Private Sub LetterheadShowAll(ByVal letterheadItem As ToolStripMenuItem)
        ReportViewer1.LetterheadShowOnAllPages = letterheadItem.Checked
    End Sub

    Protected Overrides Sub OnDataSyncNotification(entityType As Type, primaryKey As Object)
        ' This event is raised by forms whenever changes are made to the database.
        ' Load any required data from the database into the LocalView.
        ' Reset any BindingSources effected.
    End Sub

    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        e.Cancel = Not ReportClose(mReport)
        MyBase.OnFormClosing(e)
    End Sub

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)
        If Me.Database IsNot Nothing Then
            BindDataSources()
            ReportViewerInitialize()
            HeaderMenuInitialize()
            ElementsMenuInitialize()
            ReportsMenuInitialize()
            RecentReportsListRefresh()
            RecentJobsListRefresh()
            ReportMenuItemsSet(mReport)
            SettingsInitialize()
            JobMenuItemsSet(mJobDetails)
        End If
    End Sub

    Private Sub PageScrollTo(menuItem As ToolStripMenuItem)
        ReportViewer1.CurrentPageIndex = ViewPagesToolStripMenuItem.DropDownItems.IndexOf(menuItem)
    End Sub

    Private Sub PrecisionChanged(ByVal menuItem As ToolStripMenuItem)
        Me.Precision = If(menuItem.Tag, Integer.Parse(menuItem.Tag.ToString()), CType(Nothing, Integer?))
        For Each item As ToolStripMenuItem In PrecisionToolStripMenuItem.DropDownItems
            If item IsNot menuItem Then item.Checked = False
        Next
    End Sub

    Private Sub RecentJobsAdd(jobDetailsId As Integer)
        ' Ensure My.Settings.RecentJobs exists.
        If My.Settings.RecentJobs Is Nothing Then
            My.Settings.RecentJobs = New Specialized.StringCollection()
        End If
        ' Add the jobDetailsId to the beginning of the list.
        If Not My.Settings.RecentJobs.Contains(jobDetailsId.ToString()) Then
            My.Settings.RecentJobs.Insert(0, jobDetailsId.ToString())
        End If
        ' If the list count exceeds the maximum, remove the last item from the list.
        If My.Settings.RecentJobs.Count > kRecentMenusCountMax Then
            My.Settings.RecentJobs.RemoveAt(My.Settings.RecentJobs.Count - 1)
        End If
        ' Save the settings and refresh our dropdown list.
        My.Settings.Save()
        RecentJobsListRefresh()
    End Sub

    Private Sub RecentJobsClear()
        If My.Settings.RecentJobs IsNot Nothing Then My.Settings.RecentJobs.Clear()
        RecentJobsListRefresh()
    End Sub

    Private Sub RecentJobsListRefresh()
        While JobsRecentToolStripMenuItem.DropDownItems(0) IsNot ToolStripSeparator15   ' Remove everything above the separator.
            JobsRecentToolStripMenuItem.DropDownItems.RemoveAt(0)
        End While
        If My.Settings.RecentJobs IsNot Nothing AndAlso My.Settings.RecentJobs.Count > 0 Then
            For Each jobDetailId As String In My.Settings.RecentJobs
                Dim jd As JobDetail = Me.Database.JobDetails.Local.FirstOrDefault(Function(j) j.Id = jobDetailId)
                Dim jobNumber As String = If(jd?.Job?.JobNumber, "").ToString()
                Dim measurement As String = If(jd IsNot Nothing,
                    Me.Database.MeasurementTypes.Local.FirstOrDefault(Function(m) m.Id = jd.MeasurementTypeId.ToString())?.MeasurementType1, "")
                ' Dropdown items consist of JobNumber followed by MeasurementType, e.g. 12345 Initial.
                Dim item As New ToolStripMenuItem(jobDetailId) With {
                    .Text = $"{jobNumber} {measurement}",
                    .Tag = jobDetailId
                }
                AddHandler item.Click, AddressOf Me.JobsRecentToolStripMenuItem_Click
                JobsRecentToolStripMenuItem.DropDownItems.Insert(0, item)
            Next
            JobsRecentClearListToolStripMenuItem.Enabled = True
        Else
            JobsRecentClearListToolStripMenuItem.Enabled = False
        End If
    End Sub

    Private Sub RecentReportsAdd(reportName As String)
        ' Ensure My.Settings.RecentReports exists.
        If My.Settings.RecentReports Is Nothing Then
            My.Settings.RecentReports = New StringCollection
        End If
        ' Add the reportName to the beginning of the list.
        If Not My.Settings.RecentReports.Contains(reportName) Then
            My.Settings.RecentReports.Insert(0, reportName)
        End If
        ' If the list count exceeds the maximum, remove the last item from the list.
        If My.Settings.RecentReports.Count > kRecentMenusCountMax Then
            My.Settings.RecentReports.RemoveAt(My.Settings.RecentReports.Count - 1)
        End If
        ' Save the settings and refresh our dropdown list.
        My.Settings.Save()
        RecentReportsListRefresh()
    End Sub

    Private Sub RecentReportsClear()
        If My.Settings.RecentReports IsNot Nothing Then My.Settings.RecentReports.Clear()
        RecentReportsListRefresh()
    End Sub

    Private Sub RecentReportsListRefresh()
        While FileRecentToolStripMenuItem.DropDownItems(0) IsNot ToolStripFileRecentSeparator   ' Remove everything above the separator.
            FileRecentToolStripMenuItem.DropDownItems.RemoveAt(0)
        End While
        ' 1. Grab the settings reference once
        Dim settings = My.Settings
        If settings IsNot Nothing Then
            ' 2. Extract the collection once to prevent repeated evaluation
            Dim reportsCollection As StringCollection = settings.RecentReports

            ' 3. Check the extracted object safely
            If reportsCollection IsNot Nothing AndAlso reportsCollection.Count > 0 Then
                For Each reportName As String In reportsCollection
                    ' Dropdown items appear as the Report name.
                    Dim item As New ToolStripMenuItem(reportName)
                    AddHandler item.Click, AddressOf FileRecentReportsToolStripMenuItem_Click
                    FileRecentToolStripMenuItem.DropDownItems.Insert(0, item)
                Next
            End If
        End If
    End Sub

    Private Sub RecentReportsReplace(ByVal oldName As String, ByVal newName As String)
        If My.Settings.RecentReports Is Nothing OrElse My.Settings.RecentReports.Count = 0 Then
            RecentReportsAdd(newName)
        End If
        If My.Settings.RecentReports.Contains(oldName) Then
            My.Settings.RecentReports.Remove(oldName)
            RecentReportsAdd(newName)
        End If
    End Sub

    Private Function ReportClose(ByRef rp As Report) As Boolean
        If rp IsNot Nothing Then
            ReportUpdate(rp)
            If Me.Database.ChangeTracker.HasChanges() Then
                Select Case MessageBox.Show(STR_PROMPT_UNSAVED_CHANGES, STR_TITLE_DEFAULT, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning)
                    Case DialogResult.Yes
                        Me.Database.SaveChanges()
                    Case DialogResult.No
                        Me.Database.ChangeTracker.Clear()
                    Case DialogResult.Cancel
                        Return False
                    Case Else
                End Select
            End If
        End If

        ReportViewer1.Reset()
        rp = Nothing
        ReportMenuItemsSet(rp)
        FormTextUpdate(rp, Me.JobDetails)
        Return True
    End Function

    Private Sub ReportEdit(ByVal menuItem As ToolStripMenuItem)

    End Sub

    Private Sub ReportElementsMenuItemsSet(ByVal rp As Report)
        ElementsLetterheadToolStripMenuItem.Checked = ReportViewer1.Letterhead.Visible
        LetterheadShowAllToolStripMenuItem.Checked = ReportViewer1.LetterheadShowOnAllPages
        Dim lhSizeMode As ToolStripMenuItem = DirectCast(LetterheadMenuStrip.Items("LetterheadSizeModeMenuItem"), ToolStripMenuItem)
        DirectCast(lhSizeMode.DropDownItems("SizeModeNormalMenuItem"), ToolStripMenuItem).Checked = ReportViewer1.Letterhead.SizeMode = PictureBoxSizeMode.Normal
        DirectCast(lhSizeMode.DropDownItems("SizeModeCenterMenuItem"), ToolStripMenuItem).Checked = ReportViewer1.Letterhead.SizeMode = PictureBoxSizeMode.CenterImage
        DirectCast(lhSizeMode.DropDownItems("SizeModeStretchMenuItem"), ToolStripMenuItem).Checked = ReportViewer1.Letterhead.SizeMode = PictureBoxSizeMode.StretchImage
        DirectCast(lhSizeMode.DropDownItems("SizeModeAutoSizeMenuItem"), ToolStripMenuItem).Checked = ReportViewer1.Letterhead.SizeMode = PictureBoxSizeMode.AutoSize
        DirectCast(lhSizeMode.DropDownItems("SizeModeZoomMenuItem"), ToolStripMenuItem).Checked = ReportViewer1.Letterhead.SizeMode = PictureBoxSizeMode.Zoom
        Dim lhBorderStyle As ToolStripMenuItem = DirectCast(LetterheadMenuStrip.Items("LetterheadBorderStyleMenuItem"), ToolStripMenuItem)
        DirectCast(lhBorderStyle.DropDownItems("LetterheadBorderStyleNoneMenuItem"), ToolStripMenuItem).Checked = ReportViewer1.Letterhead.BorderStyle = BorderStyle.None
        DirectCast(lhBorderStyle.DropDownItems("LetterheadBorderStyleFixedSingleMenuItem"), ToolStripMenuItem).Checked = ReportViewer1.Letterhead.BorderStyle = BorderStyle.FixedSingle
        DirectCast(lhBorderStyle.DropDownItems("LetterheadBorderStyleFixed3DMenuItem"), ToolStripMenuItem).Checked = ReportViewer1.Letterhead.BorderStyle = BorderStyle.Fixed3D
        ElementsHeaderToolStripMenuItem.Checked = ReportViewer1.Header.Visible
        HeaderShowAllToolStripMenuItem.Checked = ReportViewer1.HeaderShowOnAllPages
        Dim hdrBorderStyle As ToolStripMenuItem = DirectCast(HeaderMenuStrip.Items("HeaderBorderStyleMenuItem"), ToolStripMenuItem)
        DirectCast(hdrBorderStyle.DropDownItems("BorderStyleNoneMenuItem"), ToolStripMenuItem).Checked = ReportViewer1.Header.BorderStyle = BorderStyle.None
        DirectCast(hdrBorderStyle.DropDownItems("BorderStyleFixedSingleMenuItem"), ToolStripMenuItem).Checked = ReportViewer1.Header.BorderStyle = BorderStyle.FixedSingle
        DirectCast(hdrBorderStyle.DropDownItems("BorderStyleFixed3DMenuItem"), ToolStripMenuItem).Checked = ReportViewer1.Header.BorderStyle = BorderStyle.Fixed3D
        ' Check each visible Header item in our dropdown items.
        For Each item As ReportHeader.HeaderControl In ReportViewer1.Header.HeaderControls
            DirectCast(HeaderItemsToolStripMenuItem.DropDownItems.Item(item.Name), ToolStripMenuItem).Checked = item.Visible
        Next
    End Sub

    Private Sub ReportExport(ByVal menuItem As ToolStripMenuItem)

    End Sub

    Private Sub ReportImport(ByVal menuItem As ToolStripMenuItem)

    End Sub

    Private Sub ReportLoad(ByVal rp As Report)
        If rp IsNot Nothing Then
            ' TODO: Dispense with the BindingSources, as they're not necessary and can often confound
            ' "current" Report management, esp. when there isn't one, and just query the LocalView.
            ' Move the ReportsBindingSource.Position to the given Report so we can access its ReportElements.
            ReportsBindingSource.Position = ReportDataBindingSource.IndexOf(rp)
            'ReportViewer1.DataSource = HeaderDataSourceSet()
            ' Add the required number of ReportPages to the ReportViewer1.
            Try
                ReportViewer1.TransactionBegin()
                For i As Integer = 1 To rp.PageCount
                    ReportViewer1.Pages.Add(New ReportPage())
                Next

                ' Add each ReportElement's corresponding DisplayControl to the appropriate ReportPage.
                For Each re As ReportElement In rp.ReportElements
                    Dim dc As DisplayControl = DisplayControl.CreateInstance($"{Me.GetType().Namespace}.{re.ElementName}")
                    If dc IsNot Nothing Then
                        dc.Location = New Point(re.PositionX, re.PositionY)
                        DisplayControlSizeInitialize(dc, New Size(re.SizeWidth, re.SizeHeight))
                        If dc.ContextMenuStrip IsNot Nothing Then dc.ContextMenuStrip.Enabled = True
                        dc.Basis = Me.Basis
                        dc.TolClass = Me.TolClass
                        dc.Precision = Me.Precision
                        ReportViewer1.Pages(re.PageIndex).DisplayControls.Add(dc)
                        ' Check the associated Elements menu dropdown item.
                        Dim menuItem As ToolStripMenuItem = ElementsToolStripMenuItem.DropDownItems.
                        OfType(Of ToolStripMenuItem)().
                        FirstOrDefault(Function(it) it.Name.Equals(dc.GetType.Name, StringComparison.OrdinalIgnoreCase))
                        If menuItem IsNot Nothing Then menuItem.Checked = True
                    End If
                Next
                ' Set the Report data sources to the current JobDetails.
                ReportDataSet(Me.JobDetails)
                ' Set the ReportHeader and ReportLetterhead visual properties.
                ReportViewer1.Letterhead.Visible = If(rp.LetterheadVisible, False)
                ReportViewer1.Letterhead.ImageLocation = If(rp.LetterheadImage, String.Empty)
                ReportViewer1.Letterhead.BorderStyle = If(rp.LetterheadBorderStyle, BorderStyle.None)
                ReportViewer1.Letterhead.SizeMode = If(rp.LetterheadSizeMode, PictureBoxSizeMode.Normal)
                ReportViewer1.Header.Visible = If(rp.HeaderVisible, False)
                ReportViewer1.Header.BorderStyle = If(rp.HeaderBorderStyle, BorderStyle.None)
                ReportViewer1.Header.VisibleItems = If(rp.HeaderItems, String.Empty)
                'Set our ReportHeader and ReportLetterhead menu items.
                ReportElementsMenuItemsSet(rp)
                ' Add this Report to the recents list.
                RecentReportsAdd(rp.ReportName)
                ' Tell the ReportViewer it's initialized.
                ReportViewer1.Start()
                'ReportViewer1.Select()
            Finally
                ReportViewer1.TransactionEnd()
            End Try
        End If
        ' Set out form menus.
        ReportMenuItemsSet(rp)
        FormTextUpdate(rp, Me.JobDetails)
    End Sub

    Private Sub ReportMenuItemsSet(ByVal rpt As Report)
        ' Set the form and menus according to whether a Report is currently open.
        If rpt IsNot Nothing Then
            Me.Text = rpt.ReportName
            EditToolStripMenuItem.Enabled = True
            ElementsToolStripMenuItem.Enabled = True
            FileCloseToolStripMenuItem.Enabled = True
            FileSaveAsToolStripMenuItem.Enabled = True
            PrintPreviewToolStripMenuItem.Enabled = True
            PrintToolStripMenuItem.Enabled = True
            SettingsToolStripMenuItem.Enabled = True
            ViewToolStripMenuItem.Enabled = True
        Else
            Me.Text = "Reports"
            EditToolStripMenuItem.Enabled = False
            ElementsToolStripMenuItem.Enabled = False
            FileCloseToolStripMenuItem.Enabled = False
            FileSaveAsToolStripMenuItem.Enabled = False
            FileSaveToolStripMenuItem.Enabled = False
            PrintPreviewToolStripMenuItem.Enabled = False
            PrintToolStripMenuItem.Enabled = False
            SettingsToolStripMenuItem.Enabled = False
            ViewToolStripMenuItem.Enabled = False
            ElementsMenuItemsClear()
        End If

    End Sub

    Private Sub ReportNew()
        Me.Report = New Report() With {.ReportName = "New Report", .PageCount = 1}
    End Sub

    Private Sub ReportOpen(Optional ByVal item As ToolStripMenuItem = Nothing)
        Dim rpt As Report = Nothing
        If item Is Nothing Then
            Dim frm As FrmReportPicker = DirectCast(ShowFormModal(Of FrmReportPicker)(Me.ScopeFactory, Me.User), FrmReportPicker)

            If frm.ShowDialog() = DialogResult.OK Then
                rpt = Me.Database.Reports.Local.FirstOrDefault(Function(r) r.Id = frm.Current?.Id)
            End If
        Else
            rpt = ReportsBindingSource.Find(Of Report)("ReportName", item.Text)
        End If
        If rpt IsNot Nothing Then Me.Report = rpt
    End Sub

    Private Sub ReportPagesChanged(ByVal pages As ObservableCollection(Of DocumentPage))
        ' Repopulate the Pages dropdown list with the current collection of page names.
        If pages Is Nothing Then Return
        ViewPagesToolStripMenuItem.DropDownItems.Clear()
        For Each pg As DocumentPage In pages
            Dim menuItem As New ToolStripMenuItem() With {.Available = True, .Text = $"{pg.Name}"}
            Me.ViewPagesToolStripMenuItem.DropDownItems.Add(menuItem)
            AddHandler menuItem.Click, AddressOf Me.PageToolStripMenuItem_Click
        Next
    End Sub

    Private Sub ReportPagesReset()
        ' Remove the Pages dropdown list.
        ViewPagesToolStripMenuItem.DropDownItems.Clear()
    End Sub

    Private Function ReportPageCaptureImage(ByVal pg As DocumentPage) As Bitmap
        ' Returns a bitmap image of the given DocumentPage.
        Dim bmp As New Bitmap(pg.Width, pg.Height)

        pg.DrawToBitmap(bmp, New Rectangle(0, 0, pg.Width, pg.Height))

        Return bmp
    End Function

    Private Sub ReportPageSetup(sender As Object, e As EventArgs)
        ' Opens the PageSetupDialog and saves any relevant printer settings.
        Dim pageSetupDocument As New PrintDocument()

        ' Apply any existing settings to the PageSetupDialog.
        If mPrinterSettings IsNot Nothing Then pageSetupDocument.PrinterSettings = mPrinterSettings
        pageSetupDocument.OriginAtMargins = True
        Me.PageSetupDialog.Document = pageSetupDocument
        If mPageSettings IsNot Nothing Then Me.PageSetupDialog.PageSettings = mPageSettings

        ' Open the PageSetupDialog and save the printer settings so we can reuse them next time the dialog opens.
        If PageSetupDialog.ShowDialog() = DialogResult.OK Then
            Try
                Me.Cursor = Cursors.WaitCursor
                mPrinterSettings = Me.PageSetupDialog.PrinterSettings
                mPageSettings = Me.PageSetupDialog.PageSettings
                ' Set our Document property so it propagates to the ReportViewer1 and redraws the ReportPages accordingly.
                'Dim paperSize As PaperSize = PageSetupDialog.PageSettings.PaperSize
                'Dim paperMargins As Margins = PageSetupDialog.PageSettings.Margins
                Me.Document = New DocumentSettings(
                    PageSetupDialog.PageSettings.PaperSize,
                    PageSetupDialog.PageSettings.Margins,
                    PageSetupDialog.PageSettings.PrintableArea,
                    PageSetupDialog.PageSettings.PrinterResolution
                )
            Finally
                Me.Cursor = Cursors.Default
            End Try
        End If
    End Sub

    Private Sub ReportPrint(sender As Object, e As PrintPageEventArgs)
        ' Prints each DocumentPage in the ReportViewer.
        ' TODO: Hookup to hi-res GDI+ rendering.
        Dim lastPage As Integer = ReportViewer1.Pages.Count - 1

        For i As Integer = 0 To lastPage
            Dim pageBitmap As Bitmap = ReportPageCaptureImage(Me.ReportViewer1.Pages(i))
            e.Graphics.DrawImage(pageBitmap, 0, 0)
            e.HasMorePages = (i <> lastPage)
        Next
    End Sub

    Private Sub ReportPrintPreview(sender As Object, e As EventArgs)
        'Opens Current ReportViewer1 in the PrintPreviewDialog.
        PrintPreviewDialog.Document = Me.PrintDocument
        If PrintPreviewDialog.ShowDialog() = DialogResult.OK Then
            Me.PrintDocument.Print()
        End If
    End Sub

    Private Sub ReportSave(rp As Report)
        ' Save the current Report to the database.
        Me.Database.SaveChanges()
    End Sub

    Private Sub ReportSaveAs(rp As Report)
        ' Save the current Report to the database with a different name.
        Dim newName As String = InputBox("Enter a name for this report:", "Save Report As")

        ' Check if user entered something or clicked Cancel.
        If Not String.IsNullOrEmpty(newName) Then
            Dim oldName As String = Me.Report.ReportName
            Me.Report.ReportName = newName
            ReportSave(Me.Report)
            RecentReportsReplace(oldName, newName)
            ReportsMenuInitialize()
            Me.Text = newName
        End If
    End Sub

    Private Sub ReportDataSet(ByVal jd As JobDetail)
        Dim hdr As HeaderView = If(jd IsNot Nothing, New HeaderView() With {
            .Blades = jd.Job.PropellerBlades,
            .Bore = jd.Job.PropellerBore,
            .Cup = jd.Job.Cup,
            .CustomerName = jd.Job.Vessel.Customer.CustomerName,
            .Dar = jd.Job.Dar,
            .Description = jd.Description,
            .DesiredPitch = jd.Job.DesiredPitch,
            .FileName = jd.FileName,
            .Id = jd.Id,
            .InspectedByName = Me.Database.Employees.Local.FirstOrDefault(Function(emp) emp.Id = If(jd.Job.InspectedBy, kNoCurrentRecord))?.EmployeeName,
            .JobNumber = jd.Job.JobNumber,
            .ManufacturerName = jd.Job.PropellerManufacturer?.ManufacturerName,
            .MarkedDiameter = jd.Job.PropellerDiameter,
            .MarkedPitch = jd.Job.MarkedPitch,
            .Material = jd.Job.PropellerMaterial,
            .MeasuredDiameter = jd.Job.PropellerDiameter,
            .PartNumber = jd.Job.PropellerPartNumber,
            .PerformedByName = Me.Database.Employees.Local.FirstOrDefault(Function(emp) emp.Id = If(jd.PerformedBy, kNoCurrentRecord))?.EmployeeName,
            .RepairStatus = jd.MeasurementType?.MeasurementType1,
            .Rotation = jd.Job.PropellerRotation,
            .ScanDate = jd.StartDate,
            .SerialNumber = jd.Job.SerialNumber,
            .StampNumber = jd.Job.StampNumber,
            .Style = jd.Job.PropellerStyle,
            .ToleranceClass = jd.ToleranceClass,
            .VesselName = jd.Job.Vessel.VesselName,
            .WheelPitch = jd.WheelPitch
        }, Nothing)
        Dim hv As List(Of HeaderView) = If(hdr IsNot Nothing, New List(Of HeaderView) From {hdr}, Nothing)
        ReportViewer1.Header.DataSource = If(hv IsNot Nothing, New BindingList(Of HeaderView)(hv), Nothing)
        For Each dc As DisplayControl In ReportViewer1.DisplayControls
            dc.Data = jd
        Next
    End Sub

    Private Sub ReportUpdate(rp As Report)
        If rp IsNot Nothing Then
            ' If this is a new unsaved report, save it now so we get a valid ReportId.
            If rp.Id Is Nothing Then
                Me.Database.SaveChanges()
            End If

            ' Update the Report.ReportElements to contain only the currently visible DisplayControls.
            ReportUpdateElements(rp, ReportViewer1)
            ' Update the Report header properties.
            ReportUpdateHeader(rp, ReportViewer1)
            ' Update the Report letterhead properties.
            ReportUpdateLetterhead(rp, ReportViewer1)
        End If
    End Sub

    Private Sub ReportUpdateAddNewElement(ByRef elements As ICollection(Of ReportElement), dc As DisplayControl)
        elements.Add(New ReportElement() With {
            .ElementName = dc.Name,
            .PositionX = dc.BaseLocation.X,
            .PositionY = dc.BaseLocation.Y,
            .SizeWidth = dc.BaseSize.Width,
            .SizeHeight = dc.BaseSize.Height
        })
    End Sub

    Private Sub ReportUpdateUpdateElement(ByRef re As ReportElement, ByVal dc As DisplayControl, ByVal viewer As ReportViewer)
        Dim pgIndex As Integer = viewer.Pages.IndexOf(dc.Parent)
        If re.PageIndex <> pgIndex Then
            re.PageIndex = pgIndex
        End If
        If re.SizeHeight <> dc.BaseSize.Height Then
            re.SizeHeight = dc.BaseSize.Height
        End If
        If re.SizeWidth <> dc.BaseSize.Width Then
            re.SizeWidth = dc.BaseSize.Width
        End If
        If re.PositionX <> dc.BaseLocation.X Then
            re.PositionX = dc.BaseLocation.X
        End If
        If re.PositionY <> dc.BaseLocation.Y Then
            re.PositionY = dc.BaseLocation.Y
        End If
        If re.Zorder <> dc.ZOrder Then
            re.Zorder = dc.ZOrder
        End If
    End Sub

    Private Sub ReportUpdateElements(ByRef report As Report, ByVal viewer As ReportViewer)
        If report.ReportElements IsNot Nothing Then
            ' Remove any deleted elements.
            Dim toRemove As List(Of ReportElement) = report.ReportElements.
                Where(Function(re) Not viewer.DisplayControls.
                    Select(Function(dc) dc.Name).
                    ToList().
                Contains(re.ElementName)).
                    ToList()

            For Each re As ReportElement In toRemove
                report.ReportElements.Remove(re)
            Next

            'Update/add any changed/new elements.
            For Each dc As DisplayControl In viewer.DisplayControls
                Dim re As ReportElement = report.ReportElements.FirstOrDefault(Function(el) el.ElementName = dc.Name)
                If re IsNot Nothing Then
                    ReportUpdateUpdateElement(re, dc, viewer)
                Else
                    ReportUpdateAddNewElement(report.ReportElements, dc)
                End If
            Next
        End If
    End Sub

    Private Sub ReportUpdateHeader(ByRef report As Report, ByVal viewer As ReportViewer)
        If If(report.HeaderAllPages, False) <> viewer.HeaderShowOnAllPages Then
            report.HeaderAllPages = viewer.HeaderShowOnAllPages
        End If
        If If(report.HeaderBorderStyle, 0) <> viewer.Header.BorderStyle Then
            report.HeaderBorderStyle = viewer.Header.BorderStyle
        End If
        If If(report.HeaderItems, String.Empty) <> viewer.Header.VisibleItems Then
            report.HeaderItems = viewer.Header.VisibleItems
        End If
        If If(report.HeaderVisible, False) <> viewer.Header.Visible Then
            report.HeaderVisible = viewer.Header.Visible
        End If
    End Sub

    Private Sub ReportUpdateLetterhead(ByRef report As Report, viewer As ReportViewer)
        If If(report.LetterheadAllPages, False) <> viewer.LetterheadShowOnAllPages Then
            report.LetterheadAllPages = viewer.LetterheadShowOnAllPages
        End If
        If If(report.LetterheadBorderStyle, 0) <> viewer.Letterhead.BorderStyle Then
            report.LetterheadBorderStyle = viewer.Letterhead.BorderStyle
        End If
        If If(report.LetterheadImage, String.Empty) <> If(viewer.Letterhead.ImageLocation, String.Empty).Replace("""", "") Then
            report.LetterheadImage = viewer.Letterhead.ImageLocation.Replace("""", "")
        End If
        If If(report.LetterheadSizeMode, 0) <> viewer.Letterhead.SizeMode Then
            report.LetterheadSizeMode = viewer.Letterhead.SizeMode
        End If
        If If(report.LetterheadVisible, False) <> viewer.Letterhead.Visible Then
            report.LetterheadVisible = viewer.Letterhead.Visible
        End If
    End Sub

    Private Sub ReportViewerInitialize()
        ' DocumentViewer/ReportViewer are not designable, so we  design 
        ' the generic ContextMenuStrips here, in this form, and assign
        ' them to the ReportViewer.
        ReportViewer1.ContextMenuStrip = DocumentPageContextMenuStrip
        ReportViewer1.DisplayControlContextMenu = DisplayControlContextMenuStrip
        ReportViewer1.HeaderContextMenuStrip = HeaderMenuStrip
        ReportViewer1.LetterheadContextMenuStrip = LetterheadMenuStrip
        Try
            If Me.Document Is Nothing Then Me.Document = If(DocumentPrinter.IsPrinterConnected(), New DocumentSettings(New PrintDocument()), New DocumentSettings())
        Catch ex As Exception
            Me.Document = New DocumentSettings(850, 1100, 100, 100, 100, 100, New Rectangle(), New PrinterResolution())
        End Try
        AddHandler ReportViewer1.DocumentPages.CollectionChanged, AddressOf Me.ReportPages_CollectionChanged
    End Sub

    Private Sub ReportsMenuInitialize()
        ' Populate the Reports menu with available reports from the database.
        ReportsToolStripMenuItem.DropDownItems.Clear()
        For Each rpt As Report In ReportsBindingSource
            ReportsMenuItemAdd(New ToolStripMenuItem(rpt.ReportName))
        Next
    End Sub

    Private Sub ReportsMenuItemAdd(item As ToolStripMenuItem)
        ' Adds a report to the ReportsToolStripMenu
        ReportsToolStripMenuItem.DropDownItems.Add(item)
        AddHandler item.Click, AddressOf ReportsReportToolStripMenuItem_Click
    End Sub

    Private Sub ReportViewerBringToFront()
        ReportViewer1.ControlsBringToFront(ReportViewer1.SelectedControls)
    End Sub

    Private Sub ReportViewerCut()
        ReportViewer1.ControlsCut(ReportViewer1.SelectedControls.ToList())  ' Needs .ToList() because SelectedControls is modified as DisplayControls are removed.
    End Sub

    Private Sub ReportViewerCopy()
        ' This should only be enabled for copyable objects like text.
        Throw New NotImplementedException()
    End Sub

    Private Sub ReportViewerDelete()
        ReportViewer1.ControlsCut(ReportViewer1.SelectedControls.ToList())  ' Needs .ToList() because SelectedControls is modified as DisplayControls are removed.
    End Sub

    Private Sub ReportViewerPaste()
        ReportViewer1.ControlsPaste(ReportViewer1.ClipBoard)
    End Sub

    Private Sub ReportViewerSelectAll()
        ReportViewer1.ControlsSelect(ReportViewer1.DisplayControls)
    End Sub

    Private Sub ReportViewerSendToBack()
        ReportViewer1.ControlsSendToBack(ReportViewer1.SelectedControls)
    End Sub

    Private Sub ReportViewerUndo()
        ReportViewer1.ControlsUndo(ReportViewer1.UndoStack.Pop())
    End Sub

    Private Sub SettingsInitialize()
        Me.Basis = "Mean"
        Me.Precision = 2
        Me.TolClass = Me.Database.Tolerances.Local.FirstOrDefault(Function(t) t.ToleranceClass = "I".ToString())
    End Sub
#End Region
#Region "Event Handlers"
#Region "Form Events"
    Private Sub Form_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        ' Capture all keystrokes here and invoke the corresponding ReportPanel method as necessary.
        If e.Control Then
            ' Ctrl + [Key]
            Select Case e.KeyCode
                Case Keys.A
                    ReportViewerSelectAll()
                Case Keys.B
                    ReportViewerSendToBack()
                Case Keys.F
                    ReportViewerBringToFront()
                Case Keys.V
                    ReportViewerPaste()
                Case Keys.X
                    ReportViewerCut()
                Case Keys.Z
                    ReportViewerUndo()
                Case Else
                    If e.Modifiers = Keys.Control Then
                        ReportViewer1.MultiSelect = True
                    Else
                        e.Handled = False
                        Return
                    End If
            End Select
        Else
            ' Navigation Keys.
            Select Case e.KeyCode
                Case Keys.Delete
                    If e.Modifiers = Keys.None Then ReportViewerDelete()
                Case Keys.End
                    ReportViewer1.CurrentPageIndex = ReportViewer1.Pages.Count - 1
                Case Keys.Home
                    ReportViewer1.CurrentPageIndex = 0
                Case Keys.PageDown
                    ReportViewer1.CurrentPageIndex += 1
                Case Keys.PageUp
                    ReportViewer1.CurrentPageIndex -= 1
                Case Else
                    e.Handled = False
                    Return
            End Select
        End If

        e.Handled = True
    End Sub

    Private Sub Form_KeyUp(sender As Object, e As KeyEventArgs) Handles MyBase.KeyUp
        ' Capture all keystrokes here and invoke the corresponding ReportPanel method as necessary.
        If e.KeyCode = Keys.ControlKey Then
            ReportViewer1.MultiSelect = False
            e.Handled = True
        Else
            e.Handled = False
        End If
    End Sub

    Private Sub ReportPages_CollectionChanged(sender As Object, e As NotifyCollectionChangedEventArgs)
        Select Case e.Action
            Case NotifyCollectionChangedAction.Reset
                ReportPagesReset()
            Case Else
                ReportPagesChanged(ReportViewer1.Pages)
        End Select
    End Sub
#End Region
#Region "Menu Events"
    Private Sub BasisToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MeanToolStripMenuItem.Click, MarkedToolStripMenuItem.Click, DesiredToolStripMenuItem.Click
        BasisChanged(DirectCast(sender, ToolStripMenuItem))
    End Sub

    Private Sub ClassToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClassSpecialToolStripMenuItem.Click, ClassIIIToolStripMenuItem.Click, ClassIToolStripMenuItem.Click, ClasasIIToolStripMenuItem.Click
        ClassChanged(DirectCast(sender, ToolStripMenuItem))
    End Sub

    Private Sub EditCutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditCutToolStripMenuItem.Click
        ReportViewer1.ControlsCut(ReportViewer1.SelectedControls.ToList())  ' Needs .ToList() because the ControlsCut method modifies the SelectedControls collection.    
    End Sub

    Private Sub EditCopyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditCopyToolStripMenuItem.Click
        ReportViewerCopy()
    End Sub

    Private Sub EditDeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditDeleteToolStripMenuItem.Click
        ReportViewerDelete()
    End Sub

    Private Sub EditPasteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditPasteToolStripMenuItem.Click
        ReportViewerPaste()
    End Sub

    Private Sub EditSelectAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditSelectAllToolStripMenuItem.Click
        ReportViewerSelectAll()
    End Sub

    Private Sub EditUndoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditUndoToolStripMenuItem.Click
        ReportViewerUndo()
    End Sub

    Private Sub EditToolStripMenuItem_DropDownOpening(sender As Object, e As EventArgs) Handles EditToolStripMenuItem.DropDownOpening
        EditMenuDropDownOpening(DirectCast(sender, ToolStripMenuItem))
    End Sub

    Private Sub ElementsHeaderToolStripMenuItem_CheckedChanged(sender As Object, e As EventArgs) Handles ElementsHeaderToolStripMenuItem.CheckedChanged
        HeaderVisibleChanged(DirectCast(sender, ToolStripMenuItem))
    End Sub

    Private Sub ElementsLetterheadToolStripMenuItem_CheckedChanged(sender As Object, e As EventArgs) Handles ElementsLetterheadToolStripMenuItem.CheckedChanged
        LetterheadVisibleChanged(DirectCast(sender, ToolStripMenuItem))
    End Sub

    Private Sub ElementsDisplayControlToolStripMenuItem_Click(sender As Object, e As EventArgs)
        DisplayControlToggle(DirectCast(sender, ToolStripMenuItem))
    End Sub

    Private Sub FileCloseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FileCloseToolStripMenuItem.Click
        ReportClose(mReport)
    End Sub

    Private Sub FileExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FileExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub FileNewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FileNewToolStripMenuItem.Click
        ReportNew()
    End Sub

    Private Sub FileOpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FileOpenToolStripMenuItem.Click
        ReportOpen()
    End Sub

    Private Sub FileRecentReportsToolStripMenuItem_Click(sender As Object, e As EventArgs)
        ReportOpen(DirectCast(sender, ToolStripMenuItem))
    End Sub

    Private Sub FileRecentToolStripMenuItem_DropDownOpening(sender As Object, e As EventArgs) Handles FileRecentToolStripMenuItem.DropDownOpening
        FileRecentDropDownOpening(DirectCast(sender, ToolStripMenuItem))
    End Sub

    Private Sub FileSaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FileSaveToolStripMenuItem.Click
        ReportSave(mReport)
    End Sub

    Private Sub FileSaveAsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FileSaveAsToolStripMenuItem.Click
        ReportSaveAs(mReport)
    End Sub

    Private Sub FileToolStripMenuItem_DropDownOpening(sender As Object, e As EventArgs) Handles FileToolStripMenuItem.DropDownOpening
        ReportUpdate(Me.Report)
        FileSaveToolStripMenuItem.Enabled = Me.Report IsNot Nothing AndAlso Me.Database.ChangeTracker.HasChanges()
    End Sub

    Private Sub GridSizeToolStripMenuItem_DropDownClosed(sender As Object, e As EventArgs) Handles GridSizeToolStripMenuItem.DropDownClosed
        GridSizeSet(DirectCast(sender, ToolStripMenuItem), e)
    End Sub

    Private Sub GridSizeToolStripTextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles GridSizeToolStripTextBox.KeyPress
        ' Allow only numeric digits (0-9) and control keys (like Backspace).
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True ' Ignore the character.
        End If
    End Sub

    Private Sub HeaderItem_Click(sender As Object, e As EventArgs)
        HeaderItemsChanged(DirectCast(sender, ToolStripMenuItem))
    End Sub

    Private Sub HeaderShowAllToolStripMenuItem_CheckedChanged(sender As Object, e As EventArgs) Handles HeaderShowAllToolStripMenuItem.CheckedChanged
        HeaderShowAllChanged(DirectCast(sender, ToolStripMenuItem))
    End Sub

    Private Sub JobsRecentClearListToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles JobsRecentClearListToolStripMenuItem.Click
        RecentJobsClear()
    End Sub

    Private Sub JobsOpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles JobsOpenToolStripMenuItem.Click
        JobOpen(DirectCast(sender, ToolStripMenuItem), True)
    End Sub

    Private Sub JobsRecentToolStripMenuItem_Click(sender As Object, e As EventArgs)
        JobOpen(DirectCast(sender, ToolStripMenuItem), False)
    End Sub

    Private Sub JobsCloseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles JobsCloseToolStripMenuItem.Click
        JobClose(DirectCast(sender, ToolStripMenuItem))
    End Sub

    Private Sub LetterheadShowAllToolStripMenuItem_CheckedChanged(sender As Object, e As EventArgs) Handles LetterheadShowAllToolStripMenuItem.CheckedChanged
        LetterheadShowAll(DirectCast(sender, ToolStripMenuItem))
    End Sub

    Private Sub PageToolStripMenuItem_Click(sender As Object, e As EventArgs)
        PageScrollTo(DirectCast(sender, ToolStripMenuItem))
    End Sub

    Private Sub PrintToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrintToolStripMenuItem.Click
        ReportPrint(sender, e)
    End Sub

    Private Sub PrintPreviewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrintPreviewToolStripMenuItem.Click
        ReportPrintPreview(sender, e)
    End Sub

    Private Sub PageSetupToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PageSetupToolStripMenuItem.Click
        ReportPageSetup(sender, e)
    End Sub

    Private Sub PrecisionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Precision000ToolStripMenuItem.Click, Precision00ToolStripMenuItem.Click
        PrecisionChanged(DirectCast(sender, ToolStripMenuItem))
    End Sub

    Private Sub RecentReportsClearListToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RecentReportsClearListToolStripMenuItem.Click
        RecentReportsClear()
    End Sub

    Private Sub ReportsEditToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReportsEditToolStripMenuItem.Click
        ReportEdit(DirectCast(sender, ToolStripMenuItem))
    End Sub

    Private Sub ReportsExportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReportsExportToolStripMenuItem.Click
        ReportExport(DirectCast(sender, ToolStripMenuItem))
    End Sub

    Private Sub ReportsImportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReportsImportToolStripMenuItem.Click
        ReportImport(DirectCast(sender, ToolStripMenuItem))
    End Sub

    Private Sub ReportsReportToolStripMenuItem_Click(sender As Object, e As EventArgs)
        ReportOpen(DirectCast(sender, ToolStripMenuItem))
    End Sub

    Private Sub ViewActualSizeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ActualSizeToolStripMenuItem.Click
        ReportViewer1.Zoom = 1.0F
    End Sub

    Private Sub ZoomInToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ZoomInToolStripMenuItem.Click
        ReportViewer1.Zoom = ReportViewer1.Zoom + 0.1F
    End Sub

    Private Sub ZoomOutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ZoomOutToolStripMenuItem.Click
        ReportViewer1.Zoom = ReportViewer1.Zoom - 0.1F
    End Sub
#End Region
#Region "Print Events"
    Private Sub PrintDocument_BeginPrint(sender As Object, e As PrintEventArgs) Handles PrintDocument.BeginPrint
        ' Reset the starting page index at the beginning of the print/preview action.
        mPrintPageIndex = 0
    End Sub

    Private Sub PrintDocument_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PrintDocument.PrintPage
        Dim currentPage As DocumentPage = Me.ReportViewer1.Pages(mPrintPageIndex)
        ' TODO: Fix DrawContent() so the controls render at the correct position and size.
        ' 1. Move the "Zero" point to the printer's margin
        'e.Graphics.TranslateTransform(0, 0)

        ' 2. Draw directly to the printer at native resolution (Perfectly Sharp)
        'currentPage.DrawContent(e.Graphics)
        Dim pageBitmap As Bitmap = ReportPageCaptureImage(currentPage)
        e.Graphics.DrawImage(pageBitmap, 0, 0)
        ' 3. Continue to next page
        mPrintPageIndex += 1
        e.HasMorePages = (mPrintPageIndex < ReportViewer1.Pages.Count)
    End Sub


    Private Sub LetterheadImageMenuItem_Click(sender As Object, e As EventArgs) Handles LetterheadImageMenuItem.Click
        LetterheadImageSelect()
    End Sub

    Private Sub LetterheadSizeModeMenuItem_Click(sender As Object, e As EventArgs) Handles SizeModeNormalMenuItem.Click, SizeModeStretchMenuItem.Click, SizeModeAutoSizeMenuItem.Click, SizeModeCenterMenuItem.Click, SizeModeZoomMenuItem.Click
        LetterheadSizeModeChange(DirectCast(sender, ToolStripMenuItem))
    End Sub

    Private Sub HeaderBorderStyleMenuItem_Click(sender As Object, e As EventArgs) Handles BorderStyleNoneMenuItem.Click, BorderStyleFixedSingleMenuItem.Click, BorderStyleFixed3DMenuItem.Click
        HeaderBorderStyleChanged(DirectCast(sender, ToolStripMenuItem))
    End Sub

    Private Sub LetterheadBorderStyleMenuItem_Click(sender As Object, e As EventArgs) Handles LetterheadBorderStyleNoneMenuItem.Click, LetterheadBorderStyleFixedSingleMenuItem.Click, LetterheadBorderStyleFixed3DMenuItem.Click
        LetterheadBorderStyleChanged(DirectCast(sender, ToolStripMenuItem))
    End Sub
#End Region
#End Region
End Class
