Imports System.ComponentModel
Imports Hale_MRI.RecordNavigationBar
Imports LibDatabase
Imports LibDatabase.Contexts
Imports LibDatabase.Models
Imports LibGlobals
Imports Microsoft.Extensions.DependencyInjection

''' <summary>
''' This form provides a user interface for editing
''' Propeller records.
''' </summary>
Public Class FrmPropellers
    Inherits FrmDatabaseForm

#Region "Private Members"
    'Private ReadOnly mDatabase As HaleMRIContext            ' The current database context.
    Private mFilter As Object = Nothing                     ' The current form filter object, if any.
    Private mFilterOn As Boolean = False                    ' Flag indicating whether the current form filter is active.
    Private mMasterSource As BindingSource = Nothing        ' The current "master" BindingSource.
    Private mNavigator As RecordNavigationBar = Nothing     ' Derived forms' RecordNavigationBar.
    Private mNewPropeller As Propeller = Nothing            ' The new Propeller being added, if any.
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
    Public Sub AddNew(ByVal manufacturer As Manufacturer)
        mNewPropeller = New Propeller With {.Manufacturer = manufacturer}
        PropellerBindingSource.AddNew()
    End Sub
    ''' <summary>
    ''' Returns the currently selected Propeller,
    ''' or Nothing if there is no selected record.
    ''' </summary>
    Public ReadOnly Property Current As Propeller
        Get
            Return MasterSource?.Current(Of Propeller)()
        End Get
    End Property

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

    Public Property FilterOn As Boolean
        Get
            Return mFilterOn
        End Get
        Set(value As Boolean)
            mFilterOn = value
            If Navigator IsNot Nothing Then Navigator.FilterOn = mFilterOn
        End Set
    End Property

    Public Function Find(item As Propeller) As Propeller
        Dim result As Propeller = MasterSource.Find(Of Propeller)("Id", item.Id)
        If result IsNot Nothing Then
            MasterSource.Position = MasterSource.IndexOf(result)
        End If
        Return result

    End Function

    ''' <summary>
    ''' Refreshes all form data bindings, including sorting the
    ''' Customers' Vessels and Jobs.
    ''' </summary>
    'Public Overrides Sub Refresh()
    '    MyBase.Refresh()
    '    MasterSource.DataSource = FormSort(MasterSource?.DataSource)
    'End Sub
#End Region
#Region "Private Interface"
    Private Sub BindDataSources()
        ' Load required data into the LocalView.
        If Not Me.Database.Propellers.Local.Any() Then
            LoadPropellers(Me.Database)
        End If
        BladesBindingSource.DataSource = Me.Database.Blades.Local.ToBindingList()
        StylesBindingSource.DataSource = Me.Database.Styles.Local.ToBindingList()
        MaterialsBindingSource.DataSource = Me.Database.Materials.Local.ToBindingList()
        RotationsBindingSource.DataSource = Me.Database.Rotations.Local.ToBindingList()
        ManufacturersBindingSource.DataSource = New BindingList(Of Manufacturer)(Me.Database.Manufacturers.Local.OrderBy(Function(p) p.ManufacturerName).ToList())
        ' Sort the data.
        Dim propellersList = Me.Database.Propellers.Local.OrderBy(Function(p) p.Description).ToList()
        PropellerBindingSource.DataSource = FormSort(Me.Database.Propellers.Local.ToBindingList())
        ' Assign DataGrid DataSources.
        DataGridPropellers.DataSource = PropellerBindingSource
    End Sub

    Private Function DeleteConfirm() As Boolean
        Dim prompt As String = If(DataGridPropellers.SelectedRows.Count = 1,
            String.Format(STR_DIALOG_DELETE_ROW, "propeller", $"{MasterSource.Current(Of Vessel).VesselName}"),
            String.Format(STR_DIALOG_DELETE_ROWS, $"{DataGridPropellers.SelectedRows.Count}", "propellers?"))
        Return (
            MessageBox.Show(
                $"Delete {DataGridPropellers.SelectedRows.Count} row(s)?",
                STR_TITLE_DEFAULT,
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question) = DialogResult.OK
            )
    End Function

    Private Sub DeleteSelectedPropellers()
        For Each row As DataGridViewRow In DataGridPropellers.SelectedRows
            Dim p As Propeller = CType(row.DataBoundItem, Propeller)
            If p IsNot Nothing Then
                Me.Database.Remove(p)
                DataGridPropellers.Rows.Remove(row)
            End If
        Next
        Me.Database.SaveChanges()
    End Sub

    Private Function FormSort(ByVal propellers As BindingList(Of Propeller)) As BindingList(Of Propeller)
        Return New BindingList(Of Propeller)(propellers _
            .OrderBy(Function(p) p.Manufacturer?.ManufacturerName) _
            .ThenBy(Function(p) p.PartNumber).ToList())
    End Function

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
    Private Sub FrmPropellers_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            DataGridPropellers.AutoGenerateColumns = False
            If Me.Database IsNot Nothing Then BindDataSources()
            Navigator = RecordNavigationBar1
            If Me.Database IsNot Nothing Then Navigator.Database = Me.Database
            If Me.ServiceProvider IsNot Nothing Then Navigator.ServiceProvider = Me.ServiceProvider
            Navigator.BoundControls = New List(Of Control) From {DataGridPropellers}
            MasterSource = PropellerBindingSource
            AddHandler Navigator.NavigationEvent, AddressOf Navigator_NavigationEvent
        Catch ex As Exception
            MessageBox.Show(String.Format(STR_ERR_FORM_OPEN, "Propellers", ex.Message), STR_TITLE_APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Navigator_NavigationEvent(sender As Object, e As NavigationEventArgs)
        Try
            Select Case e.EventName
                Case "Delete"
                    If DeleteConfirm() Then
                        DeleteSelectedPropellers()
                        'RefreshAll()
                    End If
                Case "FilterOff"
                Case "FilterOn"
                Case "Find"
                    Me.Find(Me.Database.Propellers.Local.OrderBy(Function(p) p.PartNumber).Where(Function(p) p.PartNumber.StartsWith(e.Key)).FirstOrDefault())
                Case "GotoFirst"
                Case "GotoLast"
                Case "GotoNext"
                Case "GotoPrev"
                Case "Refresh"
                    Me.Refresh()
                Case "Save"
                    'RefreshAll()
                Case "Undo"
                Case Else
            End Select
        Catch ex As Exception
            MessageBox.Show(String.Format(STR_ERR_NAVIGATION, ex.Message), STR_TITLE_APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub PropellerBindingSource_AddingNew(sender As Object, e As AddingNewEventArgs) Handles PropellerBindingSource.AddingNew
        Try
            Dim newPropeller = If(mNewPropeller, New Propeller())
            e.NewObject = newPropeller
            Me.Database.Propellers.Add(newPropeller)
        Catch ex As Exception
            MessageBox.Show(String.Format(STR_ERR_ADDNEW, "propeller", ex.Message), STR_TITLE_APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DataGridPropellers_DefaultValuesNeeded(sender As Object, e As DataGridViewRowEventArgs) Handles DataGridPropellers.DefaultValuesNeeded
        Try
            e.Row.Cells("Manufacturer").Value = "" ' Default to "Unknown" manufacturer.
        Catch ex As Exception
            MessageBox.Show(String.Format(STR_ERR_NO_DEFAULT_VALUE, ex.Message), STR_TITLE_APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region
End Class