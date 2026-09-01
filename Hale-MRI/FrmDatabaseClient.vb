Imports LibDatabase
Imports LibDatabase.Contexts
Imports Microsoft.Extensions.DependencyInjection

Public Class FrmDatabaseClient
    Inherits FrmDatabaseForm

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

    Protected Overrides Sub OnLoad(e As EventArgs)
        ' Do only visual/non-database settings here.
        ' Do not access the database unless our Database property is set.
        If Me.Database IsNot Nothing AndAlso Me.ServiceProvider IsNot Nothing Then
            If Me.Database.Customers.Local.Count = 0 Then
                LoadCustomers(Me.Database)
            End If
        End If
    End Sub

    Protected Overrides Sub OnDataSyncNotification(entityType As Type, primaryKey As Object)
        ' This event is raised by forms whenever changes are made to the database.
        ' Load any required data from the database into the LocalView.
        ' Reset any BindingSources effected.
    End Sub
End Class