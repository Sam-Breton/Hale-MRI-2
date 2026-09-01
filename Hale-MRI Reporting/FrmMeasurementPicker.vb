Imports System.ComponentModel
Imports LibDatabase
Imports LibDatabase.Contexts
Imports LibDatabase.Models
Imports Microsoft.Extensions.DependencyInjection

Public Class FrmMeasurementPicker
    Inherits FrmDatabaseForm

    'Private ReadOnly mDatabase As HaleMRIContext                        ' The current database context.
    'Private ReadOnly mServiceProvider As IServiceProvider               ' The current database ServiceProvider reference.
    Private mCurrent As MeasurementsView = Nothing

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


    Public ReadOnly Property Current As MeasurementsView
        Get
            Return mCurrent
        End Get
    End Property

    Private Sub BindDataSources()
        ' Make sure we have all the required data in the LocalView.
        If Not Me.Database.JobDetails.Local.Any() Then
            LoadJobDetails(Me.Database)
        End If

        ' Project to ViewModels
        Dim jobsList = Me.Database.Jobs.Local.
            Select(Function(j) New JobView() With {
                .Id = j.Id,
                .JobNumber = j.JobNumber,
                .Description = j.Description,
                .StartDate = j.StartDate,
                .InspectedByName = If(j.InspectedByNavigation IsNot Nothing, j.InspectedByNavigation.EmployeeName, ""),
                .VesselName = j.Vessel.VesselName,
                .CustomerName = j.Vessel.Customer.CustomerName,
                .Measurements = New BindingList(Of MeasurementsView)(
                    j.JobDetails.Select(Function(jd) New MeasurementsView With {
                    .Id = jd.Id,
                    .StartDate = jd.StartDate,
                    .Description = jd.Description,
                    .PerformedByName = If(jd.PerformedByNavigation IsNot Nothing, jd.PerformedByNavigation.EmployeeName, ""),
                    .MeasurementTypeName = If(jd.MeasurementType IsNot Nothing, jd.MeasurementType.MeasurementType1, "")
                    }).ToList()
                )
            })
        JobsBindingSource.DataSource = New BindingList(Of JobView)(jobsList.ToList())
    End Sub

    Protected Overrides Sub OnDataSyncNotification(entityType As Type, primaryKey As Object)
        ' This event is raised by forms whenever changes are made to the database.
        ' Load any required data from the database into the LocalView.
        ' Reset any BindingSources effected.
    End Sub

    Protected Overrides Sub OnLoad(e As EventArgs)
        DataGridJobs.AutoGenerateColumns = False
        If Me.Database IsNot Nothing Then BindDataSources()
        MyBase.OnLoad(e)
    End Sub

    Private Sub DataGridMeasurements_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles DataGridMeasurements.MouseDoubleClick
        Me.DialogResult = DialogResult.OK
    End Sub

    Private Sub DataGridMeasurements_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridMeasurements.SelectionChanged
        mCurrent = TryCast(DataGridMeasurements.CurrentRow?.DataBoundItem, MeasurementsView)
    End Sub
End Class