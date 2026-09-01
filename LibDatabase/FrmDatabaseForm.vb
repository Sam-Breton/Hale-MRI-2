Imports LibDatabase.Contexts
Imports LibDatabase.Models
Imports Microsoft.Extensions.DependencyInjection

Public Class FrmDatabaseForm
    Private ReadOnly mDatabase As HaleMRIContext = Nothing
    Private mFormLifetimeScope As IServiceScope = Nothing
    Private ReadOnly mServiceProvider As IServiceProvider = Nothing
    Private ReadOnly mScopeFactory As IServiceScopeFactory = Nothing

    ' Visual Studio Designer uses this.
    Public Sub New()
        InitializeComponent()
    End Sub

    ' DI Container uses this at runtime.
    Public Sub New(context As HaleMRIContext, serviceProvider As IServiceProvider, scopeFactory As IServiceScopeFactory)
        InitializeComponent()
        mDatabase = context
        mServiceProvider = serviceProvider
        mScopeFactory = scopeFactory
    End Sub

    ''' <summary>
    ''' The current database context.
    ''' </summary>
    ''' <returns>HaleMRIContext</returns>
    Protected ReadOnly Property Database As HaleMRIContext
        Get
            Return mDatabase
        End Get
    End Property

    ''' <summary>
    ''' Form lifetime scope.
    ''' </summary>
    ''' <returns>IServiceScope</returns>
    Public Property FormLifetimeScope As IServiceScope

    ''' <summary>
    ''' Exposes the factory to all child forms needing it.
    ''' </summary>
    ''' <returns>IServiceScopeFactory</returns>
    Protected ReadOnly Property ScopeFactory As IServiceScopeFactory
        Get
            Return mScopeFactory
        End Get
    End Property

    ''' <summary>
    ''' The current ServiceProvider.
    ''' </summary>
    ''' <returns>IServiceProvider</returns>
    Protected ReadOnly Property ServiceProvider As IServiceProvider
        Get
            Return mServiceProvider
        End Get
    End Property

    ''' <summary>
    ''' The currently logged-in application user.
    ''' </summary>
    ''' <returns>Employee</returns>
    Public Property User As Employee

    ''' <summary>
    ''' Intercepts global data changes and filters them safely.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub HandleGlobalDataChange(sender As Object, e As DataChangedEventArgs)
        ' Ignore notifications triggered by this specific form instance.
        If sender Is Me Then Return

        ' Marshall the execution safely to the UI thread if triggered by an async thread.
        If Me.InvokeRequired Then
            Me.BeginInvoke(Sub() OnDataSyncNotification(e.EntityType, e.PrimaryKey))
        Else
            OnDataSyncNotification(e.EntityType, e.PrimaryKey)
        End If
    End Sub

    ''' <summary>
    ''' Derived forms override this method to selectively reload their grids or fields.
    ''' </summary>
    Protected Overridable Sub OnDataSyncNotification(entityType As Type, primaryKey As Object)
        ' Base implementation does nothing. Derived forms implement their specific reload logic.
    End Sub

    Protected Overrides Sub OnFormClosed(e As FormClosedEventArgs)
        Try
            ' Unsubscribe to prevent memory leaks when the form closes.
            RemoveHandler DataBroadcastHub.OnDataChanged, AddressOf HandleGlobalDataChange

            ' Clean up the database connection context and private DI scope.
            If Me.Database IsNot Nothing Then Me.Database.Dispose()
            If Me.FormLifetimeScope IsNot Nothing Then Me.FormLifetimeScope.Dispose()
        Finally
            MyBase.OnFormClosed(e)
        End Try
    End Sub

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)

        ' Only subscribe to real-time events at runtime, not in the designer.
        If Me.Database IsNot Nothing Then
            AddHandler DataBroadcastHub.OnDataChanged, AddressOf HandleGlobalDataChange
        End If
    End Sub
End Class
