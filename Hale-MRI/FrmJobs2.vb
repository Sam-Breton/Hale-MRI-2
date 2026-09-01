Imports System.ComponentModel
Imports Hale_MRI.Layouts
Imports Hale_MRI.RecordNavigationBar
Imports LibDatabase
Imports LibDatabase.Contexts
Imports LibDatabase.Models
Imports LibGlobals
Imports Microsoft.Extensions.DependencyInjection

Public Class FrmJobs2
    Inherits FrmDatabaseForm
#Region "Types and Constants"
    Private ReadOnly kLayoutRectangle As Layouts
    Private ReadOnly kLayoutVertical As Layouts
#End Region
#Region "Private Members"
    Private mFilter As Object = Nothing                         ' The current form filter object, if any.
    Private mFilterOn As Boolean = False                        ' Flag indicating whether the current form filter is active.
    Private mLayout As Layouts = Nothing                        ' The current form layout.
    Private mMasterSource As BindingSource = Nothing            ' The form's "master" BindingSource.
    Private mMeasurementsEnabled As Boolean = True              ' Flag indicating whether measurements can be added/viewed for the current Job.
    Private mNavigator As RecordNavigationBar = Nothing         ' The form's RecordNavigationBar.
    Private mNewJob As Job = Nothing                            ' The new Job being added, if any.
    Private mRequiredFields As List(Of Control)                 ' The controls bound to required database fields.
    Private ReadOnly mThemeDefault As Themes = Nothing          ' The default form theme.
    Private ReadOnly mThemeManager As ThemeManager = Nothing    ' The form's ThemeManager.
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

        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' These need to be initialized before the Form.Load event fires. '
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        ' These Layouts and all Layout code can be removed once we decide on a final layout for this form.
        kLayoutRectangle = New Layouts(New List(Of ControlLayout) From {
            New ControlLayout(PictureBoxLogo, New Rectangle(12, 12, 309, 86)),
            New ControlLayout(PanelNavigation, New Rectangle(415, 12, 649, 59)),
            New ControlLayout(TableLayoutNavigation, New Rectangle(418, 15, 643, 53)),
            New ControlLayout(PanelSearch, New Rectangle(12, 112, 270, 116)),
            New ControlLayout(TableLayoutSearch, New Rectangle(14, 114, 265, 110)),
            New ControlLayout(PanelImport, New Rectangle(12, 241, 309, 81)),
            New ControlLayout(TableLayoutImport, New Rectangle(14, 244, 303, 75)),
            New ControlLayout(PanelPropeller, New Rectangle(415, 112, 650, 292)),
            New ControlLayout(TableLayoutPropeller, New Rectangle(418, 115, 644, 286)),
            New ControlLayout(PanelMeasurements, New Rectangle(416, 424, 650, 306)),
            New ControlLayout(TableLayoutMeasurements, New Rectangle(419, 427, 644, 300)),
            New ControlLayout(Button1, New Rectangle(12, 76, 38, 26)),
            New ControlLayout(Me, New Rectangle(100, 100, 1092, 781))
        })
        kLayoutVertical = New Layouts(New List(Of ControlLayout) From {
            New ControlLayout(PictureBoxLogo, New Rectangle(62, 13, 643, 86)),
            New ControlLayout(PanelNavigation, New Rectangle(59, 112, 649, 59)),
            New ControlLayout(TableLayoutNavigation, New Rectangle(62, 115, 643, 53)),
            New ControlLayout(PanelSearch, New Rectangle(59, 185, 291, 116)),
            New ControlLayout(TableLayoutSearch, New Rectangle(62, 188, 285, 110)),
            New ControlLayout(PanelImport, New Rectangle(409, 185, 299, 81)),
            New ControlLayout(TableLayoutImport, New Rectangle(412, 188, 293, 75)),
            New ControlLayout(PanelPropeller, New Rectangle(59, 315, 650, 289)),
            New ControlLayout(TableLayoutPropeller, New Rectangle(62, 318, 644, 283)),
            New ControlLayout(PanelMeasurements, New Rectangle(60, 617, 650, 306)),
            New ControlLayout(TableLayoutMeasurements, New Rectangle(63, 620, 644, 300)),
            New ControlLayout(Button1, New Rectangle(66, 76, 38, 26)),
            New ControlLayout(Me, New Rectangle(100, 100, 785, 909))
        })

        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' The current theme will be saved to and loaded from My.Settings. '
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' Theme using the design-time values.
        mThemeDefault = New Themes(
            SystemColors.Window,
            True,
            Me.Font,
            Me.ForeColor,
            Me.BackColor,
            False,
            Me.Font,
            Me.ForeColor,
            Me.BackColor,
            Me.Font,
            Me.ForeColor,
            Me.Text,
            Color.Transparent,
            0,
            Color.Transparent,
            Me.Font,
            Me.ForeColor,
            Color.Transparent,
            False
        )

        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' The ThemeManager needs to be initialized with all controls that participate in Themes. '
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        mThemeManager = New ThemeManager(Me, New List(Of ThemeManager.ControlGroup) From {
        New ThemeManager.ControlGroup(
            CustomLabelNavigation,
            PanelNavigation,
            New List(Of ThemeManager.GroupControl)() From {
                New ThemeManager.GroupControl(RecordNavigationBar2, True)
            },
            Nothing
        ),
        New ThemeManager.ControlGroup(
            CustomLabelSearch,
            PanelSearch,
            New List(Of ThemeManager.GroupControl)() From {
                New ThemeManager.GroupControl(ComboCustomers, True),
                New ThemeManager.GroupControl(ComboVessels, True),
                New ThemeManager.GroupControl(ComboJobs, True)
            },
            New List(Of Label) From {LabCustomer, LabVessel, LabJob}
        ),
        New ThemeManager.ControlGroup(
            CustomLabelImport,
            PanelImport,
            New List(Of ThemeManager.GroupControl)() From {
                New ThemeManager.GroupControl(TxtScanData, True)
            },
            New List(Of Label) From {LabScanData}
        ),
        New ThemeManager.ControlGroup(
            CustomLabelPropeller,
            PanelPropeller,
            New List(Of ThemeManager.GroupControl)() From {
                New ThemeManager.GroupControl(ComboManufacturer, True),
                New ThemeManager.GroupControl(TxtPartNumber, True),
                New ThemeManager.GroupControl(ComboStyle, True),
                New ThemeManager.GroupControl(ComboMaterial, True),
                New ThemeManager.GroupControl(ComboRotation, True),
                New ThemeManager.GroupControl(ComboBlades, True),
                New ThemeManager.GroupControl(TxtDiameter, True),
                New ThemeManager.GroupControl(TxtBore, True),
                New ThemeManager.GroupControl(TxtSerialNumber, True),
                New ThemeManager.GroupControl(TxtStampNumber, True),
                New ThemeManager.GroupControl(TxtMarkedPitch, True),
                New ThemeManager.GroupControl(TxtDesiredPitch, True),
                New ThemeManager.GroupControl(ComboLEExclusion, True),
                New ThemeManager.GroupControl(ComboTeExclusion, True),
                New ThemeManager.GroupControl(ComboCup, True),
                New ThemeManager.GroupControl(TxtDAR, True),
                New ThemeManager.GroupControl(ComboInspectedBy, True)
            },
            New List(Of Label) From {LabManufacturer, LabPartNumber, LabStyle, LabMaterial, LabRotation, LabBlades, LabDiameter,
            LabBore, LabSerialNumber, LabStampNumber, LabMarkedPitch, LabDesiredPitch, LabLEExclusion, LabTEExclusion, LabCup,
            LabDAR, LabelInspectedBy}
        ),
        New ThemeManager.ControlGroup(
            CustomLabelMeasurements,
            PanelMeasurements,
            New List(Of ThemeManager.GroupControl)() From {
                New ThemeManager.GroupControl(DataGridMeasurements, True)
            },
            Nothing
        )
    },,, mThemeDefault)
    End Sub
#End Region
#Region "Public Interface"
    Public Sub AddNew(ByVal vessel As Vessel)
        Me.SelectedVessel = vessel
        Me.JobsBindingSource.AddNew()
    End Sub
    ''' <summary>
    ''' Gets the currently selected Job record. 
    ''' </summary>
    ''' <returns>Job</returns>
    Public ReadOnly Property Current As Job
        Get
            Return Me.SelectedJob
        End Get
    End Property

    ''' <summary>
    ''' Gets or sets the current form filter object. The filter can be a Customer, Vessel, or Job object. 
    ''' When set, the form will display only the Jobs and/or Vessels associated with the specified filter object.
    ''' </summary>
    ''' <returns>Object</returns>
    Public Property Filter As Object
        Get
            Return mFilter
        End Get
        Set(value As Object)
            mFilter = value
            Me.Navigator.Filter = mFilter
            Me.FilterOn = mFilter IsNot Nothing
        End Set
    End Property

    ''' <summary>
    ''' Flag indicating whether the current form filter is active. 
    ''' When set to True, the form will display only the Jobs and/or Vessels associated with the current Filter object.
    ''' </summary>
    ''' <returns>Boolean</returns>
    Public Property FilterOn As Boolean
        Get
            Return mFilterOn
        End Get
        Set(value As Boolean)
            mFilterOn = value
            If mFilterOn AndAlso mFilter IsNot Nothing Then
                FiltersApply()
            Else
                FiltersRemove()
            End If
            Me.Navigator.FilterOn = mFilterOn
        End Set
    End Property


    ''' <summary>
    ''' Finds the given Job and, if found, makes it the current record.
    ''' </summary>
    ''' <param name="item">The Job to find.</param>
    ''' <returns>The found Job, or Nothing if not found.</returns>
    Public Function Find(item As Job) As Job
        ' Searches for the given Job and, if found, selects and returns it.
        Dim result As Job = Me.MasterSource.Find(Of Job)("Id", item.Id)
        If result IsNot Nothing Then
            Me.MasterSource.Position = Me.MasterSource.IndexOf(result)
            Me.SelectedJob = item
        End If
        Return result
    End Function

    ''' <summary>
    ''' Gets or sets the current WorkstationEncoders object. 
    ''' </summary>
    ''' <returns>WorkstationEncoders</returns>
    Public Property Hardware As WorkstationEncoders ' We need to pass this to the measurements form.

    ''' <summary>
    ''' Gets or sets the current form layout. The layout can be a Layouts object that defines the positions and sizes of the form's controls.
    ''' </summary>
    ''' <returns></returns>
    Public Shadows Property Layout As Layouts
        Get
            Return mLayout
        End Get
        Set(value As Layouts)
            If value IsNot Nothing Then value.ApplyTo(Me)
            mLayout = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the current form theme. The theme can be a Themes object that defines the colors and fonts of the form's controls.
    ''' </summary>
    ''' <returns></returns>
    Public Property Theme As Themes
        Get
            Return mThemeManager.Theme
        End Get
        Set(value As Themes)
            mThemeManager.Theme = If(value IsNot Nothing, value, mThemeDefault)
        End Set
    End Property
#End Region
#Region "Private Interface"
    Private Sub BindDataSources()
        If Me.Database IsNot Nothing Then
            If Me.Database.Jobs.Local.Count = 0 Then
                LoadJobs(Me.Database)
            End If
            FiltersRemove()
            ComboManufacturer.DataSource = New BindingList(Of Manufacturer)(Me.Database.Manufacturers.Local.OrderBy(Function(m) m.ManufacturerName).ToList())
            ComboStyle.DataSource = New BindingList(Of Style)(Me.Database.Styles.Local.OrderBy(Function(ps) ps.Style1).ToList())
            ComboMaterial.DataSource = New BindingList(Of Material)(Me.Database.Materials.Local.OrderBy(Function(m) m.Material1).ToList())
            ComboRotation.DataSource = New BindingList(Of Rotation)(Me.Database.Rotations.Local.OrderBy(Function(r) r.Rotation1).ToList())
            ComboBlades.DataSource = New BindingList(Of Blade)(Me.Database.Blades.Local.OrderBy(Function(b) b.BladeCount).ToList())
            ComboLEExclusion.DataSource = New BindingList(Of Exclusion)(Me.Database.Exclusions.Local.OrderBy(Function(le) le.Exclusion1).ToList())
            ComboTeExclusion.DataSource = New BindingList(Of Exclusion)(Me.Database.Exclusions.Local.OrderBy(Function(te) te.Exclusion1).ToList())
            ComboCup.DataSource = New BindingList(Of Cup)(Me.Database.Cups.Local.OrderBy(Function(c) c.Cup1).ToList())
            EmployeesBindingSource.DataSource = New BindingList(Of Employee)(Me.Database.Employees.Local.OrderBy(Function(e) e.EmployeeName).ToList())
            MeasurementTypesBindingSource.DataSource = New BindingList(Of MeasurementType)(Me.Database.MeasurementTypes.Local.OrderBy(Function(mt) mt.MeasurementType1).ToList())
            JobsBindingSource.BindMasterDetails(JobDetailsBindingSource, "JobDetails")
            DataGridMeasurements.DataSource = JobDetailsBindingSource
        End If
    End Sub

    Private Function CheckCustomerVessel() As Boolean
        Dim msg As String
        Dim newCustomer As Customer = Nothing
        Dim newVessel As Vessel = Nothing
        If Len(ComboCustomers.Text) = 0 Then
            MessageBox.Show(String.Format(STR_ERR_SELECTION_REQUIRED, LCase(STR_OBJECT_CUSTOMER)))
            Return False
        End If
        If Len(ComboVessels.Text) = 0 Then
            MessageBox.Show(String.Format(STR_ERR_SELECTION_REQUIRED, LCase(STR_OBJECT_VESSEL)))
            Return False
        End If
        If Not Me.Database.Customers.Local.Where(Function(jb) jb.CustomerName = ComboCustomers.Text).Any() AndAlso
            Not Me.Database.Vessels.Local.Where(Function(jb) jb.VesselName = ComboVessels.Text).Any() Then
            msg = String.Format(STR_DIALOG_PROMPT_NEW_CUSTOMER_VESSEL, STR_OBJECT_CUSTOMER, ComboCustomers.Text & " and ", LCase(STR_OBJECT_VESSEL), ComboVessels.Text)
            If MessageBox.Show(msg, STR_TITLE_DEFAULT, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = DialogResult.OK Then
                newCustomer = New Customer With {.CustomerName = ComboCustomers.Text}
                newVessel = New Vessel With {.Customer = newCustomer, .VesselName = ComboVessels.Text}
                Me.Database.Vessels.Add(newVessel)
            Else
                Return False
            End If
        ElseIf Not Me.Database.Customers.Local.Where(Function(jb) jb.CustomerName = ComboCustomers.Text).Any() Then
            msg = String.Format(STR_DIALOG_PROMPT_NEW_CUSTOMER_VESSEL, STR_OBJECT_CUSTOMER, ComboCustomers.Text, "", "")
            If MessageBox.Show(msg, STR_TITLE_DEFAULT, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = DialogResult.OK Then
                newCustomer = New Customer With {.CustomerName = ComboCustomers.Text}
                Me.Database.Customers.Add(newCustomer)
            Else
                Return False
            End If
        ElseIf Not Me.Database.Vessels.Local.Where(Function(jb) jb.VesselName = ComboVessels.Text).Any() Then
            msg = String.Format(STR_DIALOG_PROMPT_NEW_CUSTOMER_VESSEL, STR_OBJECT_VESSEL, ComboVessels.Text, "", "")
            If MessageBox.Show(msg, STR_TITLE_DEFAULT, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = DialogResult.OK Then
                newVessel = New Vessel With {.Customer = SelectedCustomer, .VesselName = ComboVessels.Text}
                Me.Database.Vessels.Add(newVessel)
            Else
                Return False
            End If
        End If
        If Me.Database.ChangeTracker.HasChanges Then
            Me.Database.SaveChanges()
            FiltersRemove()
            MasterSource.ResetBindings(False)
            If newVessel IsNot Nothing Then
                SelectedVessel = newVessel
            ElseIf newCustomer IsNot Nothing Then
                SelectedVessel.Customer = newCustomer
            End If
        End If
        Return True
    End Function

    Private Sub CheckRequiredFields()
        ' Controls Labels of data-bound controls attached to required database fields
        ' and enables browsing/selecting items in the Search group Controls..
        If mRequiredFields IsNot Nothing Then
            Dim lab As Label
            For Each ctrl As Control In mRequiredFields
                If TypeOf ctrl Is ComboBox Then
                    Dim cmb As ComboBox = DirectCast(ctrl, ComboBox)
                    lab = DirectCast(TableLayoutPropeller.Controls(ctrl.Tag), Label)
                    If lab IsNot Nothing Then
                        lab.ForeColor = If(cmb.SelectedItem Is Nothing, Color.Red, SystemColors.ControlText)
                    End If
                    MeasurementsEnabled = MeasurementsEnabled AndAlso cmb.SelectedItem IsNot Nothing
                ElseIf TypeOf ctrl Is TextBox Then
                    Dim txt As TextBox = DirectCast(ctrl, TextBox)
                    lab = DirectCast(TableLayoutPropeller.Controls(ctrl.Tag), Label)
                    If lab IsNot Nothing Then
                        lab.ForeColor = If(String.IsNullOrEmpty(txt.Text), Color.Red, SystemColors.ControlText)
                    End If
                    MeasurementsEnabled = MeasurementsEnabled AndAlso Not String.IsNullOrEmpty(txt.Text)
                End If
            Next
        End If
    End Sub

    Private Function CustomerNotInList(ByVal combo As ComboBox) As Boolean
        Dim notInList As Boolean = False
        Dim customerName As String = combo.Text.Trim()

        If Not String.IsNullOrEmpty(customerName) Then
            notInList = (combo.FindStringExact(customerName) <> -1)
        End If

        Return notInList
    End Function

    Private Function DeleteConfirm() As Boolean
        Return (MessageBox.Show($"Delete job {Current.JobNumber}?", STR_TITLE_DEFAULT, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = DialogResult.OK)
    End Function

    Private Sub DeleteSelectedJob()
        JobsBindingSource.Delete(Me.Database)
    End Sub

    Private Sub FiltersApply()
        Select Case True
            Case TypeOf Filter Is Customer
                FilterByCustomer()
            Case TypeOf Filter Is Vessel
                FilterByVessel()
            Case TypeOf Filter Is Job
                FilterByJob()
            Case Else
                ' Handle other filter types if necessary.
        End Select
    End Sub

    Private Sub FilterByCustomer()
        ' Filter the vessels drop down to include only the currently selected Customer's Vessels.
        Me.SelectedVessel = Nothing    ' Blank the currently selected vessel in case the current Customer has no Vessels.
        ComboVessels.DataSource = New BindingList(Of Vessel)(Me.Database.Vessels.Local.Where(Function(v) v.Customer Is Me.SelectedCustomer).ToList())
        If ComboVessels.Items.Count > 0 Then Me.SelectedVessel = CType(ComboVessels.Items(0), Vessel)
        ' The first Customer Vessel, if any, should now be selected.
        FilterByVessel()
    End Sub

    Private Sub FilterByVessel()
        ' Filter the jobs drop down to include only the currently selected Vessel's Jobs.
        Me.SelectedJob = Nothing   ' Blank the currently selected Job in case the current Vessel has no Jobs.
        JobsBindingSource.DataSource = New BindingList(Of Job)(Me.Database.Jobs.Local.Where(Function(j) j.Vessel Is Me.SelectedVessel).ToList())
        If JobsBindingSource.Count > 0 Then Me.SelectedJob = CType(JobsBindingSource(0), Job)
    End Sub

    Private Sub FilterByJob()
        ' Currently not used.
    End Sub

    Private Sub FiltersRemove()
        ' Save the currently selected Job.
        Dim displayedJob As Job = Me.SelectedJob
        ' Refresh the drop down lists.
        ComboCustomers.DataSource = New BindingList(Of Customer)(Me.Database.Customers.Local.OrderBy(Function(c) c.CustomerName).ToList())
        ComboVessels.DataSource = New BindingList(Of Vessel)(Me.Database.Vessels.Local.OrderBy(Function(v) v.VesselName).ToList())
        JobsBindingSource.DataSource = New BindingList(Of Job)(Me.Database.Jobs.Local.OrderBy(Function(j) j.JobNumber).ToList())
        ' Show the previously selected Job, if any.
        If displayedJob IsNot Nothing Then Me.SelectedJob = displayedJob
    End Sub

    Private Sub FindComboColor(ByVal clr As Color, ByVal cmb As ComboBox)
        Dim foundIndex As Integer = -1

        ' Search through the list of items for the given color.
        For i As Integer = 0 To cmb.Items.Count - 1
            Dim itemColor As Color = DirectCast(cmb.Items(i), Color)
            If itemColor.ToArgb() = clr.ToArgb() Then
                foundIndex = i
                Exit For
            End If
        Next

        ' If found, select the item.
        If foundIndex <> -1 Then
            cmb.SelectedIndex = foundIndex
        End If
    End Sub

    Private WriteOnly Property JobSelectionEnabled As Boolean
        Set(value As Boolean)
            ComboCustomers.Enabled = value
            ComboVessels.Enabled = value
            ComboJobs.Enabled = value
            ScanDataPickEnabled = value
        End Set
    End Property

    Private Property MasterSource As BindingSource
        Get
            Return mMasterSource
        End Get
        Set(value As BindingSource)
            mMasterSource = value
            If Me.Navigator IsNot Nothing Then Me.Navigator.MasterSource = mMasterSource
        End Set
    End Property

    Private Property MeasurementsEnabled As Boolean
        Get
            Return DataGridMeasurements.Enabled
        End Get
        Set(value As Boolean)
            DataGridMeasurements.Enabled = value
        End Set
    End Property

    Private Sub MeasurementsFormOpen(ByVal job As Job, Optional ByVal jobDetail As JobDetail = Nothing)
        Dim frm As FrmMeasurements = DirectCast(ShowForm(Of FrmMeasurements)(Me.ScopeFactory, Me.User), FrmMeasurements)

        frm.Hardware = Me.Hardware
        ' If the SelectedJob has no JobDetails (measurements), create and save a new one.
        If job?.JobDetails?.Count = 0 Then
            jobDetail = New JobDetail With {
                .Job = job,
                .JobId = job?.Id,
                .StartDate = Date.Now,
                .ToleranceClass = "S",
                .PerformedBy = Me.User?.Id
            }

            Me.Database.Add(jobDetail)
            Me.Database.SaveChanges()
        End If
        If jobDetail IsNot Nothing Then
            frm.JobDetails = jobDetail
        Else
            frm.Job = job
        End If
    End Sub

    Private Function NewJobCreate() As Job
        ' Returns a new Job with a unique job number, the currently selected Vessel
        ' and current StartDate.
        Return New Job With {
            .Vessel = Me.SelectedVessel,
            .StartDate = Date.Now,
            .JobNumber = If(Me.Database.Jobs.Local.Count <> 0, Me.Database.Jobs.Max(Function(job) job.JobNumber) + 1, 1)
        }
    End Function

    Private Sub NewJobUpdate()
        ' Updates the new Job's fields from the bound controls.
        Dim unused As Double
        With mNewJob
            .Cup = ComboCup.SelectedValue
            If Not String.IsNullOrEmpty(TxtDAR.Text) Then .Dar = If(Double.TryParse(TxtDAR.Text, unused), unused, Nothing)
            If Not String.IsNullOrEmpty(TxtDesiredPitch.Text) Then .DesiredPitch = If(Double.TryParse(TxtDesiredPitch.Text, unused), unused, Nothing)
            .LeExclusion = ComboLEExclusion.SelectedValue
            If Not String.IsNullOrEmpty(TxtMarkedPitch.Text) Then .MarkedPitch = If(Double.TryParse(TxtMarkedPitch.Text, unused), unused, Nothing)
            .PropellerBlades = ComboBlades.SelectedValue
            .PropellerBore = If(Not String.IsNullOrEmpty(TxtBore.Text), TxtBore.Text, Nothing)
            If Not String.IsNullOrEmpty(TxtDiameter.Text) Then .PropellerDiameter = If(Double.TryParse(TxtDiameter.Text, unused), unused, Nothing)
            .PropellerManufacturerId = ComboManufacturer.SelectedValue
            .PropellerMaterial = ComboMaterial.SelectedValue
            .PropellerPartNumber = If(Not String.IsNullOrEmpty(TxtPartNumber.Text), TxtPartNumber.Text, Nothing)
            .PropellerRotation = ComboRotation.SelectedValue
            .PropellerStyle = ComboStyle.SelectedValue
            .SerialNumber = If(Not String.IsNullOrEmpty(TxtSerialNumber.Text), TxtSerialNumber.Text, Nothing)
            .StampNumber = If(Not String.IsNullOrEmpty(TxtStampNumber.Text), TxtStampNumber.Text, Nothing)
            .TeExclusion = ComboTeExclusion.SelectedValue
        End With
    End Sub

    Private Property Navigator As RecordNavigationBar
        Get
            Return mNavigator
        End Get
        Set(value As RecordNavigationBar)
            mNavigator = value
            If mNavigator IsNot Nothing Then mNavigator.Database = Me.Database
        End Set
    End Property

    Protected Overrides Sub OnDataSyncNotification(entityType As Type, primaryKey As Object)
        ' TODO: Load any entities this form manages from the database into the LocalView so they're current.
        ' BindingSource.ResetBindings(False)
    End Sub

    ''' <summary>
    ''' Holds the Job record displayed prior to an Add New operation. This is used to restore the previous Job if the user cancels the Add New operation.
    ''' </summary>
    ''' <returns></returns>
    Private Property PreviousJob As Job = Nothing

    Private Sub ScanDataExport()
        ' TODO: Implement scan data export functionality.
    End Sub

    Private WriteOnly Property ScanDataImexEnabled As Boolean
        Set(value As Boolean)
            CmdScanDataImport.Enabled = value
            CmdScanDataExport.Enabled = value
        End Set
    End Property

    Private Sub ScanDataImport()
        ' Import scan data from a file, add it to the database and show the job data.
        Dim scandataFile As String = TxtScanData.Text
        Dim importedJob As Job = Imex.ScanDataImport(scandataFile)
        If importedJob Is Nothing Then
            ' If no job was created, show an error message.
            MessageBox.Show(STR_ERR_SCANDATA_TEXT, STR_TITLE_DEFAULT, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        ' Add the Job created from the imported scan data to the database.
        importedJob = ScanDataAdd(Me.Database, importedJob)
        ' Clear the form filters and show the imported Job.
        If Me.FilterOn Then
            Me.FilterOn = False
        Else
            FiltersRemove()
        End If
        Me.SelectedJob = importedJob
        TxtScanData.Text = scandataFile
    End Sub

    Private WriteOnly Property ScanDataPickEnabled As Boolean
        Set(value As Boolean)
            TxtScanData.Enabled = value
            CmdScanDataPick.Enabled = value
            If Not value Then Me.ScanDataImexEnabled = False
        End Set
    End Property

    Private Sub ScanDataSelect()
        Dim ofd As New OpenFileDialog With {
            .Title = STR_TITLE_SCANDATA_SELECT,
            .Filter = STR_DIALOG_FILTER_SCANDATA,
            .InitialDirectory = If(My.Settings.ApplicationDefaultFolder, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments))
        }
        If ofd.ShowDialog() = DialogResult.OK Then TxtScanData.Text = ofd.FileName
    End Sub

    Private Property SelectedCustomer As Customer
        Get
            Return CType(ComboCustomers.SelectedItem, Customer)
        End Get
        Set(value As Customer)
            ComboCustomers.SelectedItem = value
        End Set
    End Property

    Private Property SelectedJob As Job
        Get
            Return DirectCast(ComboJobs.SelectedItem, Job)
        End Get
        Set(value As Job)
            ComboJobs.SelectedItem = value
            If value IsNot Nothing Then
                Me.SelectedVessel = value.Vessel
            End If
        End Set
    End Property

    Private Property SelectedVessel As Vessel
        Get
            Return DirectCast(ComboVessels.SelectedItem, Vessel)
        End Get
        Set(value As Vessel)
            ComboVessels.SelectedItem = value
            If value IsNot Nothing Then
                SelectedCustomer = value.Customer
            End If
        End Set
    End Property
    'Private Sub ComboTextChanged(CBox As ComboBox, Text As String)
    '    For Each item As Object In CBox.Items
    '        Dim sItem As String = DirectCast(item, String)
    '        If CBox.Items.Contains(sItem) Then
    '            CBox.SelectedItem = item
    '        End If
    '    Next
    'End Sub
#End Region
#Region "Event Handlers"
    Private Sub CmdScanDataImport_Click(sender As Object, e As EventArgs)
        Try
            ScanDataImport()
        Catch ex As Exception
            MessageBox.Show(String.Format(STR_ERR_SCANDATA_IMPORT, $"{ex.Message}"), STR_TITLE_APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CmdScanDataExport_Click(sender As Object, e As EventArgs)
        Try
            ScanDataExport()
        Catch ex As Exception
            MessageBox.Show(String.Format(STR_ERR_SCANDATA_EXPORT, $"{ex.Message}"), STR_TITLE_APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CmdScanDataPick_Click(sender As Object, e As EventArgs)
        Try
            ScanDataSelect()
        Catch ex As Exception
            MessageBox.Show(String.Format(STR_ERR_SCANDATA_SELECT, $"{ex.Message}"), STR_TITLE_APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ComboCustomers_KeyDown(sender As Object, e As KeyEventArgs) Handles ComboCustomers.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            e.Handled = True
            If ComboCustomers.Text.Length > 0 Then
                Dim customerName As String = ComboCustomers.Text.Trim()

                If Not String.IsNullOrEmpty(customerName) Then
                    Dim index As Integer = ComboCustomers.FindStringExact(customerName)

                    If index <> -1 Then
                        Filter = SelectedCustomer
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub ComboCustomers_MouseClick(sender As Object, e As MouseEventArgs) Handles ComboCustomers.MouseClick
        Try
            If SelectedCustomer IsNot Nothing AndAlso ComboCustomers.DoubleClicked() Then
                Dim frm As FrmCustomers = DirectCast(ShowForm(Of FrmCustomers)(Me.ScopeFactory, Me.User), FrmCustomers)

                frm.Find(Me.SelectedCustomer)
            End If
        Catch ex As Exception
            MessageBox.Show(String.Format(STR_ERR_FORM_OPEN, STR_OBJECT_CUSTOMER & "s", $"{ex.Message}"), STR_TITLE_APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ComboCustomers_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboCustomers.SelectionChangeCommitted
        Try
            Filter = SelectedCustomer
        Catch ex As Exception
            MessageBox.Show(String.Format(STR_ERR_FILTER, $"{ex.Message}"), STR_TITLE_APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ComboJobs_KeyDown(sender As Object, e As KeyEventArgs) Handles ComboJobs.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            e.Handled = True
            If ComboJobs.Text.Length > 0 Then
                Dim jobNumber As Integer
                If Integer.TryParse(ComboJobs.Text, jobNumber) Then
                    Me.Find(Me.Database.Jobs.Local.OrderBy(Function(j) j.JobNumber).Where(Function(j) j.JobNumber = jobNumber).FirstOrDefault())
                End If
            End If
        End If
    End Sub

    Private Sub ComboJobs_MouseClick(sender As Object, e As MouseEventArgs) Handles ComboJobs.MouseClick
        ' Open the measurements form with the SelectedJob record.
        Try
            If ComboJobs.DoubleClicked() Then
                If Me.Modal Then
                    Me.DialogResult = DialogResult.OK
                Else
                    If Not mMeasurementsEnabled Then
                        MessageBox.Show(STR_ERR_BAD_OR_MISSING_REQUIRED_FIELD, STR_TITLE_DEFAULT, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    ElseIf Me.SelectedJob IsNot Nothing Then
                        MeasurementsFormOpen(Me.SelectedJob)
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(String.Format(STR_ERR_FORM_OPEN, STR_OBJECT_MEASUREMENT & "s", $"{ex.Message}"), STR_TITLE_APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ComboJobs_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboJobs.SelectionChangeCommitted

    End Sub

    Private Sub ComboManufacturer_MouseClick(sender As Object, e As MouseEventArgs) Handles ComboManufacturer.MouseClick
        Try
            If ComboManufacturer.SelectedItem IsNot Nothing AndAlso ComboManufacturer.DoubleClicked() Then
                Dim frm As FrmManufacturers = DirectCast(ShowForm(Of FrmManufacturers)(Me.ScopeFactory, Me.User), FrmManufacturers)

                frm.Find(ComboManufacturer.SelectedItem)
            End If
        Catch ex As Exception
            MessageBox.Show(String.Format(STR_ERR_FORM_OPEN, STR_OBJECT_MANUFACTURER & "s", $"{ex.Message}"), STR_TITLE_APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ComboStyle_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboStyle.SelectionChangeCommitted
        ' Automatically changes the blade count for certain propeller styles.
        If ComboStyle.SelectedItem IsNot Nothing Then
            Dim style As Style = DirectCast(ComboStyle.SelectedItem, Style)
            Select Case style.Style1
                Case "3-Blade"
                    ComboBlades.SelectedItem = Me.Database.Blades.Local.Where(Function(b) b.BladeCount = 3).FirstOrDefault()
                Case "4-Blade", "Dura Quad", "Dyna Quad", "Equi Quad"
                    ComboBlades.SelectedItem = Me.Database.Blades.Local.Where(Function(b) b.BladeCount = 4).FirstOrDefault()
                Case Else
            End Select
        End If
    End Sub

    Private Sub ComboVessels_KeyDown(sender As Object, e As KeyEventArgs) Handles ComboVessels.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            e.Handled = True
            If ComboVessels.Text.Length > 0 Then
                Dim vesselName As String = ComboVessels.Text.Trim()

                If Not String.IsNullOrEmpty(vesselName) Then
                    Dim index As Integer = ComboVessels.FindStringExact(vesselName)

                    If index <> -1 Then
                        Filter = SelectedVessel
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub ComboVessels_MouseClick(sender As Object, e As MouseEventArgs) Handles ComboVessels.MouseClick
        Try
            If SelectedVessel IsNot Nothing AndAlso ComboVessels.DoubleClicked() Then
                Dim frm As FrmVessels = DirectCast(ShowForm(Of FrmVessels)(Me.ScopeFactory, Me.User), FrmVessels)

                frm.Find(Me.SelectedVessel)
            End If
        Catch ex As Exception
            MessageBox.Show(String.Format(STR_ERR_FORM_OPEN, STR_OBJECT_VESSEL & "s", $"{ex.Message}"), STR_TITLE_APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ComboVessels_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboVessels.SelectionChangeCommitted
        Try
            Me.Filter = Me.SelectedVessel
        Catch ex As Exception
            MessageBox.Show(String.Format(STR_ERR_FILTER, $"{ex.Message}"), STR_TITLE_APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DataGridJobDetails_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles DataGridMeasurements.MouseDoubleClick
        ' Open the JobDetails (Measurements) form with the selected JobDetail as the current record or,
        ' if the current Job has no JobDetails, create a new JobDetail for the current Job
        ' and make it the current record.
        Try
            If Me.Modal Then
                Me.DialogResult = DialogResult.OK
            Else
                If Not mMeasurementsEnabled Then
                    MessageBox.Show(STR_ERR_BAD_OR_MISSING_REQUIRED_FIELD, STR_TITLE_DEFAULT, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Else
                    MeasurementsFormOpen(Me.SelectedJob, JobDetailsBindingSource.Current(Of JobDetail))
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(String.Format(STR_ERR_FORM_OPEN, STR_OBJECT_MEASUREMENT & "s", $"{ex.Message}"), STR_TITLE_APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Form_Closing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        On Error Resume Next
        DataGridMeasurements.DataSource = Nothing
    End Sub

    Private Sub Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.SuspendLayout()
        DataGridMeasurements.AutoGenerateColumns = False
        If Me.Database IsNot Nothing Then BindDataSources()
        Navigator = RecordNavigationBar2
        If Me.Database IsNot Nothing Then Navigator.Database = Me.Database
        If Me.ServiceProvider IsNot Nothing Then Navigator.ServiceProvider = Me.ServiceProvider
        ' These are the controls bound to the JobsBindingSource that the Navigator will enable automatically
        ' and notify us when any changes are made.
        Navigator.BoundControls = New List(Of Control) From {
                ComboManufacturer,
                TxtPartNumber,
                ComboStyle,
                ComboMaterial,
                ComboRotation,
                ComboBlades,
                TxtBore,
                TxtDiameter,
                TxtSerialNumber,
                TxtStampNumber,
                TxtMarkedPitch,
                TxtDesiredPitch,
                ComboLEExclusion,
                ComboTeExclusion,
                ComboCup,
                ComboInspectedBy,
                TxtDAR
            }
        mRequiredFields = New List(Of Control) From {
                ComboBlades,
                ComboRotation,
                TxtDiameter,
                TxtDesiredPitch,
                TxtMarkedPitch
            }
        Me.MasterSource = JobsBindingSource     ' The Navigator manages the Job records and notifies us when changes occur.
        Me.Navigator.NoUpdates = True           ' We handle record updates ourselves because adding new Jobs requires extra steps.
        AddHandler Navigator.NavigationEvent, AddressOf Navigator_NavigationEvent
        Me.FormBorderStyle = FormBorderStyle.Sizable
        ApplyLayout(kLayoutVertical)
        CheckRequiredFields()
        Me.ResumeLayout()
    End Sub

    Private Sub JobsBindingSource_CurrentChanged(sender As Object, e As EventArgs) Handles JobsBindingSource.CurrentChanged
        Try
            Me.SelectedJob = DirectCast(JobsBindingSource.Current, Job)
            CheckRequiredFields()
            Me.MeasurementsEnabled = Me.SelectedJob IsNot Nothing
        Catch ex As Exception
            MessageBox.Show(String.Format(STR_ERR_FILTER, $"{ex.Message}"), STR_TITLE_APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub JobsBindingSource_AddingNew(sender As Object, e As AddingNewEventArgs) Handles JobsBindingSource.AddingNew
        Try
            mNewJob = NewJobCreate()
            Me.PreviousJob = Me.SelectedJob
            e.NewObject = mNewJob
            Me.Database.Jobs.Add(mNewJob)
            JobSelectionEnabled = False
        Catch ex As Exception
            MessageBox.Show(String.Format(STR_ERR_ADDNEW, "Job", $"{ ex.Message}"), STR_TITLE_APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Navigator_NavigationEvent(sender As Object, e As NavigationEventArgs)
        Try
            Select Case e.EventName
                Case "AddNew"
                    ' This needs to occur before the Navigator calls MasterSource.AddNew.
                    If CheckCustomerVessel() Then
                        CheckRequiredFields()
                    End If
                Case "Delete"
                    If DeleteConfirm() Then
                        DeleteSelectedJob()
                    End If
                Case "Editing"
                    Me.JobSelectionEnabled = Not e.Value   ' Enable/disable the Job selection controls and the Measurements grid ...
                    Me.MeasurementsEnabled = Not e.Value   ' based on whether the user is editing a Job record or not.
                Case "FilterOff"
                    Me.FilterOn = False
                Case "FilterOn"
                    Me.FilterOn = True
                Case "Find"
                    Me.Find(Me.Database.Jobs.Local.OrderBy(Function(j) j.JobNumber).Where(Function(j) j.JobNumber.ToString().StartsWith(e.Key)).FirstOrDefault())
                Case "Save"

                    If mNewJob IsNot Nothing Then
                        NewJobUpdate()                  ' If we're adding a new Job, update the record fields from our controls.
                    End If
                    Me.MasterSource.Save(Me.Database)   ' Save changes.
                    If mNewJob IsNot Nothing Then       ' Make the added Job the current Job ...
                        SelectedJob = mNewJob
                        mNewJob = Nothing
                    End If
                    Me.PreviousJob = Me.SelectedJob     ' ... and save it for the next AddNew operation.
                    Me.JobSelectionEnabled = True       ' Enable the Job selection controls and the Measurements grid.
                    Me.MeasurementsEnabled = True
                    CheckRequiredFields()               ' Clear the required fields error indicators.   
                Case "Undo"
                    If Me.PreviousJob IsNot Nothing Then Me.SelectedJob = Me.PreviousJob    ' Make the previously displayed Job the current Job.
                    Me.JobSelectionEnabled = True                                           ' Enable the Job selection controls and the Measurements grid.
                    Me.MeasurementsEnabled = True
            End Select
        Catch ex As Exception
            Dim msg As String = "An error occurred: " & ex.Message & If(ex.InnerException IsNot Nothing, Environment.NewLine & ex.InnerException.Message, "")
            MessageBox.Show(msg, STR_TITLE_APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub TxtScanData_TextChanged(sender As Object, e As EventArgs) Handles TxtScanData.TextChanged
        Try
            ScanDataImexEnabled = TxtScanData.Text.Length > 0
        Catch ex As Exception
            'Swallow these as they're not critical and can be ignored.
        End Try
    End Sub
#End Region
#Region "Layouts and Themes"
    Private Sub ApplyLayout(layout As Layouts)
        Me.Layout = layout
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim frm As New FrmFormDesigner
        frm.FormThemeManager = mThemeManager
        frm.ShowDialog()
    End Sub

    'Private Sub ComboBox_TextChanged(sender As Object, e As EventArgs) Handles ComboRotation.TextChanged, ComboManufacturer.TextUpdate, ComboStyle.TextUpdate, ComboBlades.TextUpdate, ComboMaterial.TextUpdate, ComboLEExclusion.TextUpdate, ComboTeExclusion.TextUpdate, ComboCup.TextUpdate, ComboInspectedBy.TextUpdate
    '    Dim tCBox = DirectCast(sender, ComboBox)
    '    Dim tString = tCBox.Text
    '    ComboTextChanged(tCBox, tString)
    'End Sub
#End Region
End Class