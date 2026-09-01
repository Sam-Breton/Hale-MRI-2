Imports System.ComponentModel
Imports System.Numerics
Imports LibDatabase.Models

Public Class ReportPage
    Inherits DocumentPage

    Private Const kLetterheadHeaderSpacing As Integer = 10      ' Vertical spacing between the ReportLetterhead, ReportHeader and user content.
    Private Const kLetterheadRelativeHeight As Single = 0.1!    ' Height of Letterhead as percentage of page size.
    Private Const kHeaderRelativeHeight As Single = 0.15!       ' Height of Header as percentage of page size.

#Region "Constructors"
    Public Sub New()
        MyBase.New()
        InitializeComponent()
    End Sub
#End Region
#Region "Public Interface"
    Public Property DataSource As BindingList(Of HeaderView)

    ''' <summary>
    ''' The ReportHeader Control.
    ''' </summary>
    ''' <returns>ReportHeader</returns>
    Public ReadOnly Property Header As ReportHeader
        Get
            Return Me.ReportHeader1
        End Get
    End Property

    ''' <summary>
    ''' The ReportLetterhead Control.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Letterhead As ReportLetterhead
        Get
            Return Me.ReportLetterhead1
        End Get
    End Property
#End Region
#Region "Private Interface"
    Protected Overrides Sub FitToDocument(ByVal doc As DocumentSettings)
        Me.Header.SuspendLayout()   ' TODO: Some glitching on ReportLoad()
        Me.Letterhead.SuspendLayout()
        Try
            ' The ReportLetterhead always appears below the top and between the
            ' ReportPage horizontal margins.
            Me.Letterhead.SetBounds(
                doc.MarginLeft,
                doc.MarginTop,
                doc.PaperWidth - doc.MarginLeft - doc.MarginRight,
                doc.PaperHeight * kLetterheadRelativeHeight
            )
            Me.Letterhead.BaseLocation = Me.Letterhead.Location
            Me.Letterhead.BaseSize = Me.Letterhead.Size

            ' The ReportHeader always appears between at the left 
            ' and between horizontal ReportPage margins. It's actual 
            ' location is relative to the ReportLetterhead's visibility 
            ' and thus not computed here.
            Me.Header.Size = New Size(
                doc.PaperWidth - doc.MarginLeft - doc.MarginRight,
                doc.PaperHeight * kHeaderRelativeHeight
            )
            Me.Header.BaseSize = Me.Header.Size

            ' Position the ReportHeader and set our VerticalLimit.
            LetterheadVisibleSet()
        Finally
            Me.Header.ResumeLayout(False)       ' TODO: Some glitching on ReportLoad()
            Me.Letterhead.ResumeLayout(False)
            MyBase.FitToDocument(doc)
        End Try
    End Sub

    Private Sub VLimitSet()
        ' Set our VerticalLimit according to the current ReportHeader and ReportLetterhead visibility.
        If Not (Me.Letterhead.BaseLocation.IsEmpty OrElse Me.Letterhead.BaseSize.IsEmpty) AndAlso
           Not (Me.Header.BaseLocation.IsEmpty OrElse Me.Header.BaseSize.IsEmpty) Then
            If Me.Header.Visible Then
                Me.VerticalLimit = Me.Header.BaseLocation.Y + Me.Header.BaseSize.Height + Me.Header.VerticalSeparation
            ElseIf Me.Letterhead.Visible Then
                Me.VerticalLimit = Me.Letterhead.BaseLocation.Y + Me.Letterhead.BaseSize.Height + Me.Letterhead.VerticalSeparation
            Else
                Me.VerticalLimit = Me.ClientRectangle.Top
            End If
        End If
    End Sub

    Private Sub LetterheadVisibleSet()
        ' Position the ReportHeader according to the ReportLetterhead visibility.
        If Not (Me.Letterhead.BaseLocation.IsEmpty OrElse Me.Letterhead.BaseSize.IsEmpty) Then
            Me.Header.BaseLocation = New Point(
                Me.Letterhead.BaseLocation.X,
                Me.Letterhead.BaseLocation.Y + If(Me.Letterhead.Visible, Me.Letterhead.BaseSize.Height + Me.Letterhead.VerticalSeparation, 0)
            )
            Me.Header.SetBounds(
                Me.Header.BaseLocation.X * Me.Zoom,
                Me.Header.BaseLocation.Y * Me.Zoom,
                Me.Header.BaseSize.Width * Me.Zoom,
                Me.Header.BaseSize.Height * Me.Zoom
            )
            VLimitSet()
        End If
    End Sub

    Protected Overrides Sub ZoomSet(ByVal factor As Single)
        Me.Header.SuspendLayout()       ' TODO: Some glitching on ReportLoad()
        Me.Letterhead.SuspendLayout()
        Try
            If Me.Header.BaseLocation <> Point.Empty AndAlso Me.Header.BaseSize <> Size.Empty Then
                Me.Header.SetBounds(
                    Me.Header.BaseLocation.X * factor,
                    Me.Header.BaseLocation.Y * factor,
                    Me.Header.BaseSize.Width * factor,
                    Me.Header.BaseSize.Height * factor
                )
            End If
            If Me.Letterhead.BaseLocation <> Point.Empty AndAlso Me.Letterhead.BaseSize <> Size.Empty Then
                Me.Letterhead.SetBounds(
                Me.Letterhead.BaseLocation.X * factor,
                Me.Letterhead.BaseLocation.Y * factor,
                Me.Letterhead.BaseSize.Width * factor,
                Me.Letterhead.BaseSize.Height * factor
            )
            End If
        Finally
            Me.Header.ResumeLayout(False)       ' TODO: Some glitching on ReportLoad()
            Me.Letterhead.ResumeLayout(False)
            MyBase.ZoomSet(factor)
        End Try
    End Sub
#End Region
#Region "Event Handlers"
    Private Sub ReportHeader_VisibleChanged(sender As Object, e As EventArgs) Handles ReportHeader1.VisibleChanged
        VLimitSet()
    End Sub
    Private Sub ReportLetterhead_VisibleChanged(sender As Object, e As EventArgs) Handles ReportLetterhead1.VisibleChanged
        LetterheadVisibleSet()
    End Sub
#End Region
End Class
