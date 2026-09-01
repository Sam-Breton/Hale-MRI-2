Imports System.ComponentModel
Imports Hale_MRI.RecordNavigationBar
Imports LibDatabase
Imports LibDatabase.Contexts
Imports LibDatabase.Models
Imports LibGlobals
Imports Microsoft.Extensions.DependencyInjection

''' <summary>
''' This form provides a user interface for editing 
''' Manufacturer records and accessing related 
''' Propeller records.
''' </summary>
Public Class FrmManufacturers
    Inherits FrmDatabaseForm

#Region "Private Members"
    'Private ReadOnly mDatabase As HaleMRIContext            ' The current database context.
    Private mFilter As Object = Nothing                     ' The current form filter object, if any.
    Private mFilterOn As Boolean = False                    ' Flag indicating whether the current form filter is active.
    Private mMasterSource As BindingSource = Nothing        ' The current "master" BindingSource.
    Private mNavigator As RecordNavigationBar = Nothing     ' Derived forms' RecordNavigationBar.
    ' Private ReadOnly mServiceProvider As IServiceProvider   ' The current database ServiceProvider reference.
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
    ''' Returns the currently selected Manufacturer,
    ''' or Nothing if there is no selected record.
    ''' </summary>
    Public ReadOnly Property Current As Manufacturer
        Get
            Return MasterSource?.Current(Of Manufacturer)()
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
    ''' Finds the given Manufacturer in the MasterSource and, if found, makes it the current record.
    ''' </summary>
    ''' <param name="item">The Manufacturer to find.</param>
    ''' <returns>The found Manufacturer, or Nothing if not found.</returns>
    Public Function Find(item As Manufacturer) As Manufacturer
        Dim result As Manufacturer = MasterSource.Find(Of Manufacturer)("Id", item.Id)
        If result IsNot Nothing Then
            MasterSource.Position = MasterSource.IndexOf(result)
        End If
        Return result
    End Function

    ''' <summary>
    ''' Refreshes the form data and sorts the Propellers of each Manufacturer by PartNumber.
    ''' </summary>
    'Public Overrides Sub Refresh()
    '    MyBase.Refresh()
    '    FormSort(MasterSource?.DataSource)
    'End Sub
#End Region
#Region "Private Interface"
    Private Sub BindDataSources()
        ' Load required data into the LocalView.
        If Not Me.Database.Manufacturers.Local.Any() Then
            LoadManufacturers(Me.Database)
        End If
        StatesBindingSource.DataSource = Me.Database.StateCodes.Local.ToBindingList()
        CountryCodesBindingSource.DataSource = Me.Database.CountryCodes.Local.ToBindingList()
        ' Sort the data.
        Dim manufacturersList = Me.Database.Manufacturers.Local.OrderBy(Function(m) m.ManufacturerName).ToList()
        FormSort(Me.Database.Manufacturers.Local.ToBindingList())
        ManufacturersBindingSource.DataSource = New BindingList(Of Manufacturer)(manufacturersList)
        ' Bind the master BindingSource (Manufacturers) to the details BindingSource (Propellers).
        ManufacturersBindingSource.BindMasterDetails(PropellersBindingSource, "Propellers")
        ' Assign DataGrid DataSources.
        DataGridManufacturers.DataSource = ManufacturersBindingSource
        DataGridPropellers.DataSource = PropellersBindingSource
    End Sub

    Private Function DeleteConfirm() As Boolean
        Dim prompt As String = If(DataGridManufacturers.SelectedRows.Count = 1,
            String.Format(STR_DIALOG_DELETE_ROW, "manufacturer", {Current?.ManufacturerName}),
            String.Format(STR_DIALOG_DELETE_ROWS, {DataGridManufacturers.SelectedRows.Count}, "manufacturers?"))
        Return (
            MessageBox.Show(
                $"Delete {DataGridManufacturers.SelectedRows.Count} row(s)?",
                STR_TITLE_DEFAULT,
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question) = DialogResult.OK
            )
    End Function

    Private Sub DeleteSelectedManufacturers()
        For Each row As DataGridViewRow In DataGridManufacturers.SelectedRows
            Dim m As Manufacturer = CType(row.DataBoundItem, Manufacturer)
            If m IsNot Nothing Then
                Me.Database.Remove(m)
                DataGridManufacturers.Rows.Remove(row)
            End If
        Next
        Me.Database.SaveChanges()
    End Sub

    Private Sub FormSort(ByRef manufacturers As BindingList(Of Manufacturer))
        For Each m In manufacturers
            If m?.Propellers IsNot Nothing AndAlso m.Propellers.Count > 1 Then
                m.Propellers = m.Propellers.OrderBy(Function(p) p.PartNumber).ToList()
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
        ' Load any required data from the database into the LocalView.
    End Sub
#End Region
#Region "Event Handlers"
    Private Sub DataGridPropeller_CellMouseDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridPropellers.CellMouseDoubleClick
        Try
            'ShowForm(gFrmPropellers, Database, User)
            'gFrmPropellers.Find(PropellersBindingSource.Current.Id)
        Catch ex As Exception
            MessageBox.Show(String.Format(STR_ERR_FORM_OPEN, "propellers", ex.Message), STR_TITLE_APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub FrmManufacturers_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            DataGridManufacturers.AutoGenerateColumns = False
            DataGridPropellers.AutoGenerateColumns = False
            If Me.Database IsNot Nothing Then BindDataSources()
            Navigator = RecordNavigationBar1
            If Me.Database IsNot Nothing Then Navigator.Database = Me.Database
            If Me.ServiceProvider IsNot Nothing Then Navigator.ServiceProvider = Me.ServiceProvider
            Navigator.BoundControls = New List(Of Control) From {DataGridManufacturers}
            MasterSource = ManufacturersBindingSource
            AddHandler Navigator.NavigationEvent, AddressOf Navigator_NavigationEvent
        Catch ex As Exception
            MessageBox.Show(String.Format(STR_ERR_FORM_OPEN, "manufacturers", ex.Message), STR_TITLE_APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ManufacturersBindingSource_AddingNew(sender As Object, e As AddingNewEventArgs) Handles ManufacturersBindingSource.AddingNew
        Try
            Dim newManufacturer As New Manufacturer()
            e.NewObject = newManufacturer
            Me.Database.Manufacturers.Add(newManufacturer)
        Catch ex As Exception
            MessageBox.Show(String.Format(STR_ERR_ADDNEW, "manufacturer", ex.Message), STR_TITLE_APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Navigator_NavigationEvent(sender As Object, e As NavigationEventArgs)
        Try
            Select Case e.EventName
                Case "Delete"
                    If DeleteConfirm() Then
                        DeleteSelectedManufacturers()
                        'RefreshAll()
                    End If
                Case "FilterOff"
                Case "FilterOn"
                Case "Find"
                    Me.Find(Me.Database.Manufacturers.Local.OrderBy(Function(m) m.ManufacturerName).Where(Function(m) m.ManufacturerName.StartsWith(e.Key)).FirstOrDefault())
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