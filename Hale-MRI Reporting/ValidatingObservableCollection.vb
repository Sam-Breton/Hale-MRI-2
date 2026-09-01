Imports System.Collections.ObjectModel
Imports System.Collections.Specialized
Imports System.ComponentModel

''' <summary>
''' Extends ObservableCollection(Of T) with methods to inspect and cancel changes befor they occur.
''' </summary>
''' <typeparam name="T"></typeparam>
Public Class ValidatingObservableCollection(Of T)
    Inherits ObservableCollection(Of T)

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(controls As IList(Of DisplayControl))
        Me.Controls = controls
    End Sub

    Public ReadOnly Property Controls As IList(Of DisplayControl)

    ' Custom events for previewing item addition/replacement.
    Public Event AddingItem(sender As Object, args As CancelEventArgs(Of T))
    Public Event BeginTrans(sender As Object, e As EventArgs)
    Public Event ClearingItems(sender As Object, args As CancelEventArgs(Of T))
    Public Event EndTrans(sender As Object, e As EventArgs)
    Public Event RemovingItem(sender As Object, args As CancelEventArgs(Of T))
    Public Event RemovingRange(sender As Object, args As CancelEventArgs(Of T))
    Public Event ChangingCollection(sender As Object, args As CancelEventArgs(Of T), e As NotifyCollectionChangedEventArgs)
    Public Event ChangingProperty(sender As Object, args As CancelEventArgs(Of T), e As PropertyChangedEventArgs)

    ''' <summary>
    ''' Allows undo/restore operations to inspect the event before the fact.
    ''' </summary>
    Public Sub BeginTransaction()
        RaiseEvent BeginTrans(Me, New EventArgs())
    End Sub

    ''' <summary>
    ''' Handles .Clear()
    ''' </summary>
    Protected Overrides Sub ClearItems()
        ' Raise the ClearingItems() event to allow external code to validate.
        Dim args As New CancelEventArgs(Of T)(Nothing)
        RaiseEvent ClearingItems(Me, args)

        ' If the event was not cancelled, proceed with the clearing the collection.
        If Not args.Cancel Then
            MyBase.ClearItems()
        End If
    End Sub

    ''' <summary>
    ''' Allows undo/restore operations to inspect the event after the fact.
    ''' </summary>
    Public Sub EndTransaction()
        RaiseEvent EndTrans(Me, New EventArgs())
    End Sub

    ''' <summary>
    ''' Handles .Add() and .Insert().
    ''' </summary>
    ''' <param name="index"></param>
    ''' <param name="item"></param>
    Protected Overrides Sub InsertItem(index As Integer, item As T)
        ' Raise the AddingItem() event to allow external code to validate.
        Dim args As New CancelEventArgs(Of T)(item)
        RaiseEvent AddingItem(Me, args)

        ' If the event was not cancelled, proceed with the insertion.
        If Not args.Cancel Then
            MyBase.InsertItem(index, item)
        End If
    End Sub

    ''' <summary>
    ''' Handles Remove and RemoveAt (index).
    ''' </summary>
    ''' <param name="index"></param>
    Protected Overrides Sub RemoveItem(index As Integer)
        ' Raise the RemovingItem() event to allow external code to validate.
        Dim args As New CancelEventArgs(Of T)(Nothing)
        RaiseEvent RemovingItem(Me, args)

        ' If the event was not cancelled, proceed with the deletion.
        If Not args.Cancel Then
            MyBase.RemoveItem(index)
        End If
    End Sub

    ''' <summary>
    ''' Handles (index) = value replacements.
    ''' </summary>
    ''' <param name="index"></param>
    ''' <param name="item"></param>
    Protected Overrides Sub SetItem(index As Integer, item As T)
        ' Raise the AddingItem() event to allow external code to validate.
        Dim args As New CancelEventArgs(Of T)(item)
        RaiseEvent AddingItem(Me, args)

        ' Only proceed if the event wasn't cancelled.
        If Not args.Cancel Then
            MyBase.SetItem(index, item)
        End If
    End Sub

    ''' <summary>
    ''' Handles OnCollectionChanged()
    ''' </summary>
    ''' <param name="e"></param>
    Protected Overrides Sub OnCollectionChanged(e As NotifyCollectionChangedEventArgs)
        ' Raise the ChangingCollection() event to allow external code to validate.
        Dim args As New CancelEventArgs(Of T)(Nothing)
        RaiseEvent ChangingCollection(Me, args, e)

        ' If the event was not cancelled, proceed with the changes.
        If Not args.Cancel Then
            MyBase.OnCollectionChanged(e)
        End If
    End Sub

    ''' <summary>
    ''' Handles OnPropertyChanged()
    ''' </summary>
    ''' <param name="e"></param>
    Protected Overrides Sub OnPropertyChanged(e As PropertyChangedEventArgs)
        ' Raise the ChangingCollection() event to allow external code to validate.
        Dim args As New CancelEventArgs(Of T)(Nothing)
        RaiseEvent ChangingProperty(Me, args, e)

        ' If the event was not cancelled, proceed with the insertion.
        If Not args.Cancel Then
            MyBase.OnPropertyChanged(e)
        End If
    End Sub
End Class

''' <summary>
''' Extends CancelEventArgs, encapsulating the object in question.
''' </summary>
''' <typeparam name="T"></typeparam>
Public Class CancelEventArgs(Of T)
    Inherits CancelEventArgs

    Private ReadOnly _item As T

    Public Sub New(item As T)
        _item = item
    End Sub

    Public ReadOnly Property Item As T
        Get
            Return _item
        End Get
    End Property
End Class
