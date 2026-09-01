Imports System.ComponentModel
Imports Hale_MRI.RecordNavigationBar
Imports LibDatabase
Imports LibDatabase.Contexts
Imports LibDatabase.Models
Imports LibGlobals
Imports Microsoft.EntityFrameworkCore.ChangeTracking.Internal
Imports Microsoft.Extensions.DependencyInjection

''' <summary>
''' This form provides a user interface for editing 
''' Customer records and accessing related Vessel and
''' Job records.
''' </summary>
Partial Public Class FrmCustomers
    Inherits FrmDatabaseForm

#Region "Private Members"
    Private mFilter As Object = Nothing                     ' The current form filter object, if any.
    Private mFilterOn As Boolean = False                    ' Flag indicating whether the current form filter is active.
    Private mMasterSource As BindingSource = Nothing        ' The form's "master" BindingSource.
    Private mNavigator As RecordNavigationBar = Nothing     ' The form's RecordNavigationBar.
    'Private ReadOnly mDatabase As HaleMRIContext            ' The current database context.
    'Private ReadOnly mServiceProvider As IServiceProvider   ' The current database ServiceProvider reference.
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
    ''' <summary>
    ''' Returns the currently selected Customer,
    ''' or Nothing if there is no selected record.
    ''' </summary>
    Public ReadOnly Property Current As Customer
        Get
            Return MasterSource?.Current(Of Customer)()
        End Get
    End Property

    ''' <summary>
    ''' Gets or sets the current filter object.
    ''' </summary>
    Public Property Filter As Object
        Get
            Return mFilter
        End Get
        Set(value As Object)
            mFilter = value
            If Navigator IsNot Nothing Then Navigator.Filter = mFilter
            FilterOn = mFilter IsNot Nothing
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a flag indicating whether the current filter is active.
    ''' </summary>
    Public Property FilterOn As Boolean
        Get
            Return mFilterOn
        End Get
        Set(value As Boolean)
            mFilterOn = value
            If Navigator IsNot Nothing Then Navigator.FilterOn = mFilterOn
        End Set
    End Property

    ''' <summary>
    ''' Finds the given Customer and, if found, makes it the current record.
    ''' </summary>
    ''' <param name="item">The Customer to find.</param>
    ''' <returns>The found Customer, or Nothing if not found.</returns>
    Public Function Find(item As Customer) As Customer
        Dim result As Customer = CustomerBindingSource.Find(Of Customer)("Id", item.Id)
        If result IsNot Nothing Then
            CustomerBindingSource.Position = CustomerBindingSource.IndexOf(result)
        End If
        Return result
    End Function

    '''' <summary>
    '''' Refreshes all form data bindings, including sorting the
    '''' Customers' Vessels and Jobs.
    '''' </summary>
    'Public Overrides Sub Refresh()
    '    MyBase.Refresh()
    '    FormSort(MasterSource?.DataSource)
    'End Sub
#End Region
#Region "Private Interface"
    Private Sub BindDataSources()
        ' Load required data into the LocalView.
        If Not Me.Database.Customers.Local.Any() Then
            LoadCustomers(Me.Database)
        End If
        CountryCodeBindingSource.DataSource = Me.Database.CountryCodes.Local.ToBindingList()
        EmployeeBindingSource.DataSource = Me.Database.Employees.Local.ToBindingList()
        StateCodeBindingSource.DataSource = Me.Database.StateCodes.Local.ToBindingList()
        ' Sort the data.
        Dim customersList = Me.Database.Customers.Local.OrderBy(Function(c) c.CustomerName).ToList()
        FormSort(Me.Database.Customers.Local.ToBindingList())
        CustomerBindingSource.DataSource = New BindingList(Of Customer)(customersList)
        ' Bind: Customers (master) -> Vessels (details), Vessels (master) -> Jobs (details).
        CustomerBindingSource.BindMasterDetails(VesselBindingSource, "Vessels")
        VesselBindingSource.BindMasterDetails(JobBindingSource, "Jobs")
        ' Assign DataGrid DataSources.
        DataGridCustomers.DataSource = CustomerBindingSource
        DatagridCustomerVessels.DataSource = VesselBindingSource
        DataGridVesselJobs.DataSource = JobBindingSource
    End Sub

    Private Function DeleteConfirm() As Boolean
        Dim prompt As String = If(DataGridCustomers.SelectedRows.Count = 1,
            String.Format(STR_DIALOG_DELETE_ROW, "customer", {Current?.CustomerName}),
            String.Format(STR_DIALOG_DELETE_ROWS, {DataGridCustomers.SelectedRows.Count}, "customers?"))
        Return (
            MessageBox.Show(
                prompt,
                STR_TITLE_DEFAULT,
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question) = DialogResult.OK
            )
    End Function

    Private Sub DeleteSelectedCustomers()
        ' Delete the Customer from the database and the row from the DataGrid.
        For Each row As DataGridViewRow In DataGridCustomers.SelectedRows
            Dim c As Customer = CType(row.DataBoundItem, Customer)
            If c IsNot Nothing Then
                ' We need to explicitly remove related Vessels before calling Database.SaveChanges(),
                ' otherwise we get a foreign key constraint error (probably due to multilevel
                ' Master/Details binding).
                For Each v As Vessel In c.Vessels
                    Me.Database.Remove(v)
                Next
                Me.Database.Remove(c)
            End If
            DataGridCustomers.Rows.Remove(row)
        Next
        Me.Database.SaveChanges()
    End Sub

    Private Sub FormSort(ByRef customers As BindingList(Of Customer))
        'Sort each Customer's Vessels by VesselName and each Vessel's Jobs by JobNumber.
        For Each c As Customer In customers
            If c?.Vessels IsNot Nothing Then
                If c.Vessels.Count > 1 Then
                    c.Vessels = c.Vessels.OrderBy(Function(cc) cc.VesselName).ToList()
                End If
                For Each v As Vessel In c.Vessels
                    If v?.Jobs IsNot Nothing Then
                        If v.Jobs.Count > 1 Then
                            v.Jobs = v.Jobs.OrderBy(Function(vv) vv.JobNumber).ToList()
                        End If
                    End If
                Next
            End If
        Next
    End Sub

    Private Property MasterSource As BindingSource
        Get
            Return mMasterSource
        End Get
        Set(value As BindingSource)
            mMasterSource = value
            If Navigator IsNot Nothing Then Navigator.MasterSource = mMasterSource
        End Set
    End Property

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
#End Region
#Region "Event Handlers"
    Private Sub CustomerBindingSource_AddingNew(sender As Object, e As AddingNewEventArgs) Handles CustomerBindingSource.AddingNew
        Try
            Dim newCustomer As New Customer()
            e.NewObject = newCustomer
            Me.Database.Customers.Add(newCustomer)
        Catch ex As Exception
            MessageBox.Show(String.Format(STR_ERR_ADDNEW, LCase(STR_OBJECT_CUSTOMER), ex.Message), STR_TITLE_APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DatagridCustomerVessels_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles DatagridCustomerVessels.MouseDoubleClick
        ' Open the Vessels form with the selected Vessel as the current record or,
        ' if the Customer has no Vessels, create a new Vessel for the Customer
        ' and make it the current record.
        Try
            If Me.Current IsNot Nothing Then
                Dim frm As FrmVessels = DirectCast(ShowForm(Of FrmVessels)(Me.ScopeFactory, Me.User), FrmVessels)

                If frm.Find(VesselBindingSource.Current(Of Vessel)) Is Nothing Then
                    frm.AddNew(Me.Current)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(String.Format(STR_ERR_FORM_OPEN, STR_OBJECT_VESSEL & "s", ex.Message), STR_TITLE_APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DataGridVesselJobs_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles DataGridVesselJobs.MouseDoubleClick
        ' Open the Jobs form with the selected Job as the current record or,
        ' if the Vessel has no Jobs, create a new Job for the Vessel
        ' and make it the current record.
        Try
            If VesselBindingSource.Current IsNot Nothing Then
                Dim frm As FrmJobs2 = DirectCast(ShowForm(Of FrmJobs2)(Me.ScopeFactory, Me.User), FrmJobs2)

                If frm.Find(JobBindingSource.Current(Of Job)) Is Nothing Then
                    frm.AddNew(VesselBindingSource.Current(Of Vessel))
                End If
                'frm.Show()
            End If
        Catch ex As Exception
            MessageBox.Show(String.Format(STR_ERR_FORM_OPEN, LCase(STR_OBJECT_JOB) & "s", ex.Message), STR_TITLE_APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FrmCustomers_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            DataGridCustomers.AutoGenerateColumns = False
            DatagridCustomerVessels.AutoGenerateColumns = False
            DataGridVesselJobs.AutoGenerateColumns = False
            If Me.Database IsNot Nothing Then BindDataSources()
            Navigator = RecordNavigationBar1
            If Me.Database IsNot Nothing Then Navigator.Database = Me.Database
            If Me.ServiceProvider IsNot Nothing Then Navigator.ServiceProvider = Me.ServiceProvider
            Navigator.BoundControls = New List(Of Control) From {DataGridCustomers}
            MasterSource = CustomerBindingSource
            AddHandler Navigator.NavigationEvent, AddressOf Navigator_NavigationEvent
        Catch ex As Exception
            MessageBox.Show(String.Format(STR_ERR_FORM_OPEN, LCase(STR_OBJECT_CUSTOMER) & "s", ex.Message), STR_TITLE_APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Navigator_NavigationEvent(sender As Object, e As NavigationEventArgs)
        Try
            Select Case e.EventName
                Case "Delete"
                    If DeleteConfirm() Then
                        DeleteSelectedCustomers()
                        'RefreshAll()
                    End If
                Case "FilterOff"
                Case "FilterOn"
                Case "Find"
                    Me.Find(Me.Database.Customers.Local.OrderBy(Function(c) c.CustomerName).Where(Function(c) c.CustomerName.StartsWith(e.Key)).FirstOrDefault())
                Case "GotoFirst"
                Case "GotoLast"
                Case "GotoNext"
                Case "GotoPrev"
                Case "Refresh"
                Case "Save"
                    'RefreshAll()
                Case "Undo"
                Case Else
            End Select
        Catch ex As Exception
            MessageBox.Show(String.Format(STR_ERR_NAVIGATION, ex.Message), STR_TITLE_APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region
End Class