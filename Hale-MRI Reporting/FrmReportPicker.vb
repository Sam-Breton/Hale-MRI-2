Imports System.ComponentModel
Imports LibDatabase
Imports LibDatabase.Contexts
Imports LibDatabase.Models
Imports Microsoft.Extensions.DependencyInjection

Public Class FrmReportPicker
    Inherits FrmDatabaseForm
    'Private ReadOnly mDatabase As HaleMRIContext                        ' The current database context.
    'Private ReadOnly mServiceProvider As IServiceProvider               ' The current database ServiceProvider reference.

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


    Public ReadOnly Property Current As ReportView
        Get
            Return DirectCast(ReportsBindingSource.Current(), ReportView)
        End Get
    End Property

    Private Sub BindDataSources()
        If Not Me.Database.Reports.Local.Any() Then
            LoadReports(Me.Database)
        End If
        ' Project to ViewModels
        Dim reportsList = Me.Database.Reports.Local.
            Select(Function(r) New ReportView() With {
                .Id = r.Id,
                .ReportName = r.ReportName,
                .Description = r.Description,
                .LastModified = r.LastModifed,
                .ModifiedByName = If(r.ModifiedByNavigation IsNot Nothing, r.ModifiedByNavigation.EmployeeName, ""),
                .PageCount = r.PageCount
            })
        ReportsBindingSource.DataSource = New BindingList(Of ReportView)(reportsList.ToList())
    End Sub

    Protected Overrides Sub OnDataSyncNotification(entityType As Type, primaryKey As Object)
        ' This event is raised by forms whenever changes are made to the database.
        ' Load any required data from the database into the LocalView.
        ' Reset any BindingSources effected.
    End Sub

    Protected Overrides Sub OnLoad(e As EventArgs)
        DataGridReports.AutoGenerateColumns = False
        If Me.Database IsNot Nothing Then BindDataSources()
        MyBase.OnLoad(e)
    End Sub

    Private Sub DataGridReports_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles DataGridReports.MouseDoubleClick
        Me.DialogResult = DialogResult.OK
    End Sub
End Class