Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports LibDatabase.Models
Public Module UndoData
    <Serializable>
    Public Class ControlData
        Public Property ID As Guid ' Unique way to track which control is which
        Public Property BaseLocation As Point
        Public Property Basis As String
        Public Property BaseSize As Size
        Public Property Bounds As Rectangle
        Public Property ControlType As Type
        Public Property DisplayName As String
        Public Property DragEdgeSize As Integer
        Public Property IsMovable As Boolean
        Public Property IsSelectable As Boolean
        Public Property IsSizeable As Boolean
        Public Property LastPosition As Point
        Public Property LastSize As Size
        Public Property MaxSize As Size
        Public Property MinSize As Size
        Public Property Name As String
        Public Property PageIndex As Integer = -1
        Public Property Precision As Integer?
        Public Property Selected As Boolean
        Public Property SelectionBorderColor As Color
        Public Property SelectionBorderSize As Integer
        Public Property TolClass As Tolerance
        Public Property ZOrder As Integer
    End Class

    <Serializable()>
    Public Class PageData
        Public Property Controls As New List(Of ControlData)
        Public Property Name As String
        Public Property OriginalSize As Size
        Public Property VerticalLimit As Integer
    End Class

    Public Function CapturePages(state As IList(Of DocumentPage)) As List(Of PageData)
        Dim snapshot As New List(Of PageData)

        For Each pg As DocumentPage In state
            Dim pData As New PageData With {
                .Name = pg.Name,
                .OriginalSize = pg.OriginalSize,
                .VerticalLimit = pg.VerticalLimit
            }

            For Each dc As DisplayControl In pg.DisplayControls
                pData.Controls.Add(CaptureControl(dc))
            Next
            snapshot.Add(pData)
        Next

        Return snapshot
    End Function

    Public Function CaptureControl(ByVal dc As DisplayControl) As ControlData
        Dim cd As New ControlData With {
            .ID = dc.Id,
            .BaseLocation = dc.BaseLocation,
            .Basis = dc.Basis,
            .BaseSize = dc.BaseSize,
            .Bounds = dc.Bounds,
            .ControlType = dc.GetType(),
            .DisplayName = dc.DisplayName,
            .DragEdgeSize = dc.DragEdgeSize,
            .IsMovable = dc.IsMovable,
            .IsSelectable = dc.IsSelectable,
            .IsSizeable = dc.IsSizeable,
            .LastPosition = dc.LastPosition,
            .LastSize = dc.LastSize,
            .MaxSize = dc.MaxSize,
            .MinSize = dc.MinSize,
            .Name = dc.Name,
            .Precision = dc.Precision,
            .Selected = dc.Selected,
            .SelectionBorderColor = dc.SelectionBorderColor,
            .SelectionBorderSize = dc.SelectionBorderSize,
            .TolClass = dc.TolClass,
            .ZOrder = dc.ZOrder
        }

        Return cd
    End Function
End Module
