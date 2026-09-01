Imports System.ComponentModel
Imports Hale_MRI.RecordNavigationBar
Imports LibDatabase
Imports LibDatabase.Contexts
Imports LibDatabase.Models
Imports LibGlobals
Imports Microsoft.Extensions.DependencyInjection

''' <summary>
''' This form provides a user interface for editing 
''' Vessel records and accessing related Job records.
''' </summary>
Public Class FrmVessels
    Inherits FrmDatabaseForm

#Region "Private Members"
    'Private ReadOnly Me.Database As HaleMRIContext        ' The current database context.
    Private mFilter As Object = Nothing                 ' The current form filter object, if any.
    Private mFilterOn As Boolean = False                ' Flag indicating whether the current form filter is active.
    Private mMasterSource As BindingSource = Nothing    ' The form's "master" BindingSource.
    Private mNavigator As RecordNavigationBar = Nothing ' The form's RecordNavigationBar.
    Private mNewVessel As Vessel = Nothing              ' The new Vessel being added, if any.
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
    Public Sub AddNew(ByVal customer As Customer)
        mNewVessel = New Vessel With {.Customer = customer}
        VesselBindingSource.AddNew()
    End Sub

    ''' <summary>
    ''' Returns the currently selected Vessel,
    ''' or Nothing if there is no selected record.
    ''' </summary>
    Public ReadOnly Property Current As Vessel
        Get
            Return MasterSource?.Current(Of Vessel)()
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
    ''' Finds the given Vessel in the MasterSource and, if found, makes it the current record.
    ''' </summary>
    ''' <param name="item">The Vessel to find.</param>
    ''' <returns>Vessel</returns>
    Public Function Find(item As Vessel) As Vessel
        Dim result As Vessel = MasterSource.Find(Of Vessel)("Id", item.Id)
        If result IsNot Nothing Then
            VesselBindingSource.Position = VesselBindingSource.IndexOf(result)
        End If
        Return result
    End Function

    'Public Overrides Sub Refresh()
    '    MyBase.Refresh()
    '    FormSort(MasterSource?.DataSource)
    'End Sub
#End Region
#Region "Private Interface"
    Private Sub BindDataSources()
        ' Load required data into the LocalView.
        If Not Me.Database.Vessels.Local.Any() Then
            LoadVessels(Me.Database)
        End If
        CountryCodeBindingSource.DataSource = Me.Database.CountryCodes.Local.ToBindingList()
        EmployeeBindingSource.DataSource = Me.Database.Employees.Local.ToBindingList()
        VesselServiceTypeBindingSource.DataSource = Me.Database.VesselServiceTypes.Local.ToBindingList()
        CustomerBindingSource.DataSource = New BindingList(Of Customer)(Me.Database.Customers.Local.OrderBy(Function(c) c.CustomerName).ToList())
        ' Sort the data.
        Dim vesselsList = Me.Database.Vessels.Local.OrderBy(Function(v) v.VesselName).ToList()
        FormSort(Me.Database.Vessels.Local.ToBindingList())
        VesselBindingSource.DataSource = vesselsList
        ' Bind: Vessels (master) -> Jobs (details).
        VesselBindingSource.BindMasterDetails(JobsBindingSource, "Jobs")
        ' Assign DataGrid DataSources.
        DataGridVessels.DataSource = VesselBindingSource
        DataGridVesselJobs.DataSource = JobsBindingSource
    End Sub

    Private Function DeleteConfirm() As Boolean
        Dim prompt As String = If(DataGridVessels.SelectedRows.Count = 1,
            String.Format(STR_DIALOG_DELETE_ROW, LCase(STR_OBJECT_VESSEL), $"{MasterSource.Current(Of Vessel).VesselName}"),
            String.Format(STR_DIALOG_DELETE_ROWS, $"{DataGridVessels.SelectedRows.Count}", LCase(STR_OBJECT_VESSEL) & "s?"))
        Return (
            MessageBox.Show(
                prompt,
                STR_TITLE_DEFAULT,
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question) = DialogResult.OK
            )
    End Function

    Private Sub DeleteSelectedVessels()
        For Each row As DataGridViewRow In DataGridVessels.SelectedRows
            Dim v As Vessel = CType(row.DataBoundItem, Vessel)
            If v IsNot Nothing Then
                Me.Database.Remove(v)
                DataGridVessels.Rows.Remove(row)
            End If
        Next
        Me.Database.SaveChanges()
    End Sub

    Private Sub FormSort(ByRef vessels As BindingList(Of Vessel))
        For Each v In vessels
            If v?.Jobs IsNot Nothing AndAlso v.Jobs.Count > 1 Then
                v.Jobs = v.Jobs.OrderBy(Function(j) j.JobNumber).ToList()
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
            'If mNavigator IsNot Nothing Then mNavigator.Database = Me.Database
        End Set
    End Property

    Protected Overrides Sub OnDataSyncNotification(entityType As Type, primaryKey As Object)
        ' TODO: Load any entities this form manages from the database into the LocalView so they're current.
        ' BindingSource.ResetBindings(False)
    End Sub
#End Region
#Region "Event Handlers"
    Private Sub DataGridVessels_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles DataGridVessels.MouseDoubleClick
        ' Open the Customers form with the selected Customer as the current record.
        Try
            Dim frm As FrmCustomers = DirectCast(ShowForm(Of FrmCustomers)(Me.ScopeFactory, Me.User), FrmCustomers)

            frm.Find(Current?.Customer)
        Catch ex As Exception
            MessageBox.Show(String.Format(STR_ERR_FORM_OPEN, STR_OBJECT_JOB & "s", ex.Message), STR_TITLE_APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DataGridVessels_DefaultValuesNeeded(sender As Object, e As DataGridViewRowEventArgs)
        Try
            e.Row.Cells("CustomerId").Value = VesselBindingSource.Current.Customer?.Id
        Catch ex As Exception
            MessageBox.Show(String.Format(STR_ERR_NO_DEFAULT_VALUE, ex.Message), STR_TITLE_APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DataGridVesselJobs_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles DataGridVesselJobs.MouseDoubleClick
        ' Open the Jobs form with the selected Job as the current record or,
        ' if the Vessel has no Jobs, create a new Job for the Vessel
        ' and make it the current record.
        Try
            If Current IsNot Nothing Then
                Dim frm As FrmJobs2 = DirectCast(ShowForm(Of FrmJobs2)(Me.ScopeFactory, Me.User), FrmJobs2)

                frm.Find(JobsBindingSource.Current)
            End If
        Catch ex As Exception
            MessageBox.Show(String.Format(STR_ERR_FORM_OPEN, STR_OBJECT_JOB & "s", ex.Message), STR_TITLE_APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FrmVessels_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            DataGridVessels.AutoGenerateColumns = False
            DataGridVesselJobs.AutoGenerateColumns = False
            If Me.Database IsNot Nothing Then BindDataSources()
            Navigator = RecordNavigationBar1
            If Me.Database IsNot Nothing Then Navigator.Database = Me.Database
            If Me.ServiceProvider IsNot Nothing Then Navigator.ServiceProvider = Me.ServiceProvider
            Navigator.BoundControls = New List(Of Control) From {DataGridVessels}
            MasterSource = VesselBindingSource
            AddHandler Navigator.NavigationEvent, AddressOf Navigator_NavigationEvent
        Catch ex As Exception
            MessageBox.Show(String.Format(STR_ERR_FORM_OPEN, STR_OBJECT_VESSEL & "s", ex.Message), STR_TITLE_APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Navigator_NavigationEvent(sender As Object, e As NavigationEventArgs)
        Try
            Select Case e.EventName
                Case "Delete"
                    If DeleteConfirm() Then
                        DeleteSelectedVessels()
                        'RefreshAll()
                    End If
                Case "FilterOff"
                Case "FilterOn"
                Case "Find"
                    Find(Me.Database.Vessels.Local.OrderBy(Function(v) v.VesselName).Where(Function(v) v.VesselName.StartsWith(e.Key)).FirstOrDefault())
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

    Private Sub VesselBindingSource_AddingNew(sender As Object, e As AddingNewEventArgs) Handles VesselBindingSource.AddingNew
        Try
            Dim newVessel As Vessel = If(mNewVessel, New Vessel())
            e.NewObject = newVessel
            If newVessel.Customer Is Nothing Then
                newVessel.Customer = Me.Database.Customers.Local.FirstOrDefault()
            End If
            Me.Database.Vessels.Add(newVessel)
            mNewVessel = Nothing
        Catch ex As Exception
            MessageBox.Show(String.Format(STR_ERR_ADDNEW, LCase(STR_OBJECT_VESSEL), ex.Message), STR_TITLE_APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region
End Class