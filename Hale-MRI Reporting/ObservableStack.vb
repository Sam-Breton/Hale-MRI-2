Imports System.ComponentModel

Public Class ObservableStack(Of T)
    Public Event CollectionBeforeChange(sender As Object, args As CancelEventArgs(Of T))
    Public Event CollectionChanged As EventHandler(Of StackChangedEventArgs(Of T))

    Private ReadOnly mStack As New Stack(Of T)

    Public ReadOnly Property Count As Integer
        Get
            Return mStack.Count
        End Get
    End Property

    Public Sub Push(item As T)
        Dim args As New CancelEventArgs(Of T)(Nothing)

        RaiseEvent CollectionBeforeChange(Me, args)
        If Not args.Cancel Then
            mStack.Push(item)
            RaiseEvent CollectionChanged(Me, New StackChangedEventArgs(Of T)(StackAction.Push, item))
        End If
    End Sub

    Public Function Pop() As T
        Dim args As New CancelEventArgs(Of T)(Nothing)

        RaiseEvent CollectionBeforeChange(Me, args)
        If Not args.Cancel Then
            Dim item As T = mStack.Pop()

            RaiseEvent CollectionChanged(Me, New StackChangedEventArgs(Of T)(StackAction.Pop, item))
            Return item
        End If

        Return Nothing
    End Function

    Public Sub Clear()
        Dim args As New CancelEventArgs(Of T)(Nothing)

        RaiseEvent CollectionBeforeChange(Me, args)
        If Not args.Cancel Then
            mStack.Clear()
            RaiseEvent CollectionChanged(Me, New StackChangedEventArgs(Of T)(StackAction.Clear, Nothing))
        End If
    End Sub

    Public Function Peek() As T
        Return mStack.Peek()
    End Function
End Class

Public Enum StackAction
    Push
    Pop
    Clear
End Enum

Public Class StackChangedEventArgs(Of T)
    Inherits CancelEventArgs

    Public Property Action As StackAction
    Public Property Item As T

    Public Sub New(action As StackAction, item As T)
        Me.Action = action
        Me.Item = item
    End Sub
End Class