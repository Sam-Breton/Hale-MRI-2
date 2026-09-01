Public Class Layouts
    Public Class ControlLayout
        Public Control As Control
        Public Bounds As Rectangle
        Public Visible As Boolean

        Public Sub New(ctrl As Control, Optional bounds As Rectangle = Nothing, Optional visible As Boolean = True)
            Me.Control = ctrl
            If Not bounds.IsEmpty Then
                Me.Bounds = bounds
            Else
                Me.Bounds = ctrl.Bounds
            End If
            Me.Visible = visible
        End Sub
    End Class

    Public Property Controls As List(Of ControlLayout)

    Public Sub New(controls As List(Of ControlLayout))
        Me.Controls = controls
    End Sub

    Public Sub ApplyTo(ByVal frm As Form)
        frm.SuspendLayout()
        For Each ctrlLayout As ControlLayout In Me.Controls
            If ctrlLayout.Control IsNot Nothing Then
                ctrlLayout.Control.Bounds = ctrlLayout.Bounds
                ctrlLayout.Control.Visible = ctrlLayout.Visible
            End If
        Next
        frm.ResumeLayout()
    End Sub
End Class

Public Class LayoutManager
    Public Property Layouts As List(Of Layouts)
    Public Property Current As Layouts

    Public Sub New(layouts As List(Of Layouts))
        Me.Layouts = layouts
    End Sub
End Class
