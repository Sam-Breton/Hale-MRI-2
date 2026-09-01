Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports LibDatabase.Contexts
Imports Microsoft.EntityFrameworkCore

Public Module BindingSourceExtensions
    ''' <summary>
    ''' Binds a master BindingSource to a details BindingSource using the given property (DataMember) value.
    ''' </summary>
    ''' <param name="master"></param>
    ''' <param name="details"></param>
    ''' <param name="value"></param>
    <Extension()>
    Public Sub BindMasterDetails(master As BindingSource, ByRef details As BindingSource, ByVal value As String)
        details.DataSource = master
        details.DataMember = value
    End Sub

    ''' <summary>
    ''' Returns the current record in the BindingSource, or Nothing if there is no current record.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="bs"></param>
    ''' <returns>T</returns>
    <Extension()>
    Public Function Current(Of T As Class)(bs As BindingSource) As T
        Return If(bs IsNot Nothing AndAlso bs.Position <> kNoCurrentRecord, DirectCast(bs.Current, T), Nothing)
    End Function


    ''' <summary>
    ''' Returns a BindingSource's underlying entity type.
    ''' </summary>
    ''' <param name="bs"></param>
    ''' <returns></returns>
    <Extension()>
    Public Function EntityType(bs As BindingSource) As Type
        Dim fi = GetType(BindingSource).GetField("_itemType", BindingFlags.NonPublic Or BindingFlags.Instance)
        Return TryCast(fi?.GetValue(bs), Type)
    End Function

    ''' <summary>
    ''' Returns an enumerable collection, IEnumerable(Of DbSet(Of T)), 
    ''' filtered according the filterParam expression. The List param
    ''' is taken from BindingSource.DataSource.List.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="list"></param>
    ''' <param name="filterParam"></param>
    ''' <returns>IEnumerable(Of T)</returns>
    <Extension()>
    Public Function Filter(Of T)(ByVal list As IEnumerable(Of T), ByVal filterParam As Func(Of T, Boolean)) As IEnumerable(Of T)
        Return list.Where(filterParam).ToList()
    End Function

    <Extension()>
    Public Function Find(Of T As Class)(bs As BindingSource, propertyName As String, key As Object) As T
        ' Returns the BindingSource item of the first record matching the given propertyName and key.
        Dim list = bs.List.Cast(Of T)()
        Dim prop As PropertyInfo = GetType(T).GetProperty(propertyName)

        If prop Is Nothing Then
            Throw New ArgumentException($"Property '{propertyName}' not found on type {GetType(T).Name}")
        End If

        Dim foundItem = list.FirstOrDefault(Function(item)
                                                Dim val = prop.GetValue(item, Nothing)
                                                Return val IsNot Nothing AndAlso val.Equals(key)
                                            End Function)

        Return foundItem
    End Function

    <Extension()>
    Public Sub Delete(bs As BindingSource, context As DbContext)
        ' 1. Grab the entity before removing it from the UI list
        Dim entityToDelete As Object = bs.Current
        If entityToDelete Is Nothing Then Return

        ' 2. Remove it from the BindingSource UI list
        bs.RemoveCurrent()
        bs.EndEdit()

        ' 3. Tell EF Core to mark this entity as Deleted
        Dim entry = context.Entry(entityToDelete)
        If entry IsNot Nothing AndAlso entry.State <> EntityState.Detached Then
            ' If it's a brand new unsaved entity, detach it; otherwise, mark for hard delete
            If entry.State = EntityState.Added Then
                entry.State = EntityState.Detached
            Else
                context.Remove(entityToDelete)
            End If
        End If

        ' 4. Safely refresh the UI for the newly focused item if one exists
        If bs.Current IsNot Nothing Then
            bs.ResetCurrentItem()
        End If
    End Sub

    '<Extension()>
    'Public Sub Remove(Of T As Class)(bs As BindingSource)
    '    bs.RemoveCurrent()
    '    bs.EndEdit()
    '    If bs.Current(Of T) IsNot Nothing Then
    '        bs.ResetCurrentItem()
    '    End If
    'End Sub

    <Extension()>
    Public Sub Save(bs As BindingSource)
        bs.EndEdit()
        bs.ResetCurrentItem()
    End Sub

    <Extension()>
    Public Sub Save(bs As BindingSource, db As HaleMRIContext)
        bs.EndEdit()
        db.SaveChanges()
        bs.ResetCurrentItem()
    End Sub

    ''' <summary>
    ''' Discards the pending changes for the current BindingSource item and restores the original values.
    ''' Supports only items implementing IEditableObject.
    ''' </summary>
    ''' <param name="bs"></param>
    <Extension()>
    Public Sub Undo(bs As BindingSource)
        ' CancelEdit automatically rolls back any unsaved changes.
        bs.CancelEdit()
        bs.ResetCurrentItem()
    End Sub

    ''' <summary>
    ''' Discards the pending changes for the current BindingSource item and restores the original values.
    ''' Supports both IEditableObject and non-IEditableObject items.
    ''' </summary>
    ''' <param name="bs"></param>
    ''' <param name="context"></param>
    <Extension()>
    Public Sub Undo(bs As BindingSource, context As DbContext)
        ' 1. Get the raw current object without casting.
        Dim currentEntity As Object = bs.Current
        If currentEntity Is Nothing Then Return

        ' 2. Locate the entity entry in the DbContext.
        Dim entry = context.Entry(currentEntity)

        ' 3. If EF is tracking it, revert to database/original values.
        If entry IsNot Nothing AndAlso entry.State <> EntityState.Detached Then
            entry.CurrentValues.SetValues(entry.OriginalValues)
            entry.State = EntityState.Unchanged

            ' 4. Refresh the UI
            bs.ResetCurrentItem()
        End If
    End Sub
End Module
