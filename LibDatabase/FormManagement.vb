Imports LibDatabase.Models
Imports Microsoft.Extensions.DependencyInjection

Public Module FormManagement
    ' Track all open database forms explicitly in memory.
    Private ReadOnly mActiveDatabaseForms As New List(Of FrmDatabaseForm)()

    ''' <summary>
    ''' Creates and opens a single instance of a specified unbound form type, or brings it into focus if an instance is already open. 
    ''' </summary>
    ''' <typeparam name="F"></typeparam>
    ''' <param name="windowState"></param>
    Public Function ShowForm(Of F As {Form, New})(Optional ByVal windowState As FormWindowState = FormWindowState.Normal) As Form
        ' 1. Scan the global open forms collection for this specific Type
        Dim existingForm = Application.OpenForms.OfType(Of F)().FirstOrDefault()

        If existingForm IsNot Nothing AndAlso existingForm.IsHandleCreated Then
            ' 2. If it exists, bring it forward
            existingForm.WindowState = windowState
            existingForm.BringToFront()

            Return existingForm
        Else
            ' 3. If it doesn't exist, create it using its empty constructor
            Dim newForm = New F()
            newForm.WindowState = windowState
            newForm.Show()

            Return newForm
        End If
    End Function

    Public Function ShowForm(Of F As {FrmDatabaseForm, New})(ByVal scopeFactory As IServiceScopeFactory, ByVal user As Employee, Optional ByVal windowState As FormWindowState = FormWindowState.Normal) As FrmDatabaseForm
        Dim formTypeName As String = GetType(F).Name

        ' Scan our private form registry by exact type name.
        Dim existingForm As FrmDatabaseForm = mActiveDatabaseForms.
            FirstOrDefault(Function(frm) frm.GetType().Name = formTypeName)

        If existingForm IsNot Nothing AndAlso Not existingForm.IsDisposed Then
            ' If it's already open (even if minimized), bring it to the front.
            existingForm.WindowState = windowState
            existingForm.BringToFront()

            Return existingForm
        Else
            ' Create a brand new scope and form.
            Dim newScope = scopeFactory.CreateScope()
            Dim newForm = newScope.ServiceProvider.GetRequiredService(Of F)()

            newForm.FormLifetimeScope = newScope
            newForm.User = user
            newForm.WindowState = windowState

            ' Register the new form in our tracker before showing it.
            mActiveDatabaseForms.Add(newForm)

            ' Automatically remove it from our tracker when it closes.
            AddHandler newForm.FormClosed, Sub(sender As Object, e As FormClosedEventArgs)
                                               mActiveDatabaseForms.Remove(CType(sender, FrmDatabaseForm))
                                           End Sub

            newForm.Show()

            Return newForm
        End If
    End Function

    Public Function ShowFormModal(Of F As {FrmDatabaseForm, New})(ByVal scopeFactory As IServiceScopeFactory, ByVal user As Employee) As FrmDatabaseForm
        Dim formTypeName As String = GetType(F).Name

        ' Scan our private form registry by exact type name.
        Dim existingForm As FrmDatabaseForm = mActiveDatabaseForms.
            FirstOrDefault(Function(frm) frm.GetType().Name = formTypeName)

        If existingForm IsNot Nothing AndAlso Not existingForm.IsDisposed Then
            ' If it's already open close it.
            CloseForm(Of F)()
        End If
        ' Create a brand new scope and form.
        Dim newScope = scopeFactory.CreateScope()
        Dim newForm = newScope.ServiceProvider.GetRequiredService(Of F)()

        newForm.FormLifetimeScope = newScope
        newForm.User = user

        ' Register the new form in our tracker before showing it.
        mActiveDatabaseForms.Add(newForm)

        ' Automatically remove it from our tracker when it closes.
        AddHandler newForm.FormClosed, Sub(sender As Object, e As FormClosedEventArgs)
                                           mActiveDatabaseForms.Remove(CType(sender, FrmDatabaseForm))
                                       End Sub

        Return newForm
    End Function
    ''' <summary>
    ''' Finds and safely closes the single open instance of a specified form type.
    ''' </summary>
    ''' <typeparam name="F"></typeparam>
    Public Sub CloseForm(Of F As Form)()
        ' Always check the global open forms collection first.
        Dim existingForm = Application.OpenForms.OfType(Of F)().FirstOrDefault()

        If existingForm IsNot Nothing AndAlso existingForm.IsHandleCreated Then
            existingForm.Close()
            ' Exit early if we successfully found and closed the primary instance.
            Return
        End If

        ' INDEPENDENT CHECK: If it wasn't found above, check our private database forms registry.
        Dim formTypeName As String = GetType(F).Name
        Dim databaseForm = mActiveDatabaseForms.FirstOrDefault(Function(frm) frm.GetType().Name = formTypeName)

        If databaseForm IsNot Nothing AndAlso Not databaseForm.IsDisposed Then
            ' This triggers standard OnFormClosing/OnFormClosed events, 
            ' which handles database disposal and removes it from our list.
            databaseForm.Close()
        End If
    End Sub

    ''' <summary>
    ''' Closes all open forms in the application, including standard and database-bound forms.
    ''' </summary>
    Public Sub CloseAllForms()
        ' 1. Snapshot and close all standard forms tracked by Windows Forms
        '    (Exclude the main FrmHaleMRI dashboard so the app stays running)
        Dim openFormsSnapshot = Application.OpenForms.Cast(Of Form)().
        Where(Function(f) f.GetType().Name <> "FrmHaleMRI").ToList()

        For Each frm In openFormsSnapshot
            If frm.IsHandleCreated AndAlso Not frm.IsDisposed Then
                frm.Close()
            End If
        Next

        ' 2. Snapshot and close any database forms tracked in our custom list
        Dim dbFormsSnapshot = mActiveDatabaseForms.
        Where(Function(f) f.GetType().Name <> "FrmHaleMRI").ToList()

        For Each dbFrm In dbFormsSnapshot
            If Not dbFrm.IsDisposed Then
                dbFrm.Close()
            End If
        Next
    End Sub

    Public Class DataBroadcastHub
        ' Global event that fires when data changes anywhere in the app.
        Public Shared Event OnDataChanged(sender As Object, e As DataChangedEventArgs)

        ''' <summary>
        ''' Notifies all open subscribers that an item has been saved to the database.
        ''' </summary>
        Public Shared Sub BroadcastChange(sender As Object, entityType As Type, primaryKey As Object)
            RaiseEvent OnDataChanged(sender, New DataChangedEventArgs(entityType, primaryKey))
        End Sub
    End Class

    Public Class DataChangedEventArgs
        Inherits EventArgs

        Public ReadOnly Property EntityType As Type
        Public ReadOnly Property PrimaryKey As Object

        Public Sub New(type As Type, pk As Object)
            Me.EntityType = type
            Me.PrimaryKey = pk
        End Sub
    End Class
End Module
