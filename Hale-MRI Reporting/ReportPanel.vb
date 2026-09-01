Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports LibDatabase.Models

Public Class ReportPanel
    Inherits DocumentPanel

    Private mHeader As New ReportHeader()
    Private mHeaderShowOnAllPages As Boolean = False
    Private mLetterhead As New ReportLetterhead()
    Private mLetterheadShowOnAllPages As Boolean = False

    ''' <summary>
    ''' Default constructor.
    ''' </summary>
    Public Sub New()
        MyBase.New()
        InitializeComponent()
    End Sub

    ''' <summary>
    ''' The ReportHeader's list of visible items.
    ''' </summary>
    ''' <returns></returns>
    Public Property DataSource As BindingList(Of HeaderView)

    ''' <summary>
    ''' Cast of base DocumentPage collection to ReportPage.
    ''' </summary>
    ''' <returns>IEnumerable(Of ReportPage)</returns>
    Public ReadOnly Property ReportPages As IEnumerable(Of ReportPage)
        Get
            Return Me.Pages.OfType(Of ReportPage)()
        End Get
    End Property

    Public ReadOnly Property Header As ReportHeader
        Get
            Return If(Me.Pages.Count > 0, Me.ReportPages(0).Header, mHeader)
        End Get
    End Property

    ''' <summary>
    ''' Indicates whether the ReportHeader is visible on all ReportPages.
    ''' </summary>
    ''' <returns></returns>
    Public Property HeaderShowOnAllPages As Boolean
        Get
            Return mHeaderShowOnAllPages
        End Get
        Set(value As Boolean)
            HeaderOnAllPages(value)
            mHeaderShowOnAllPages = value
        End Set
    End Property

    Public ReadOnly Property Letterhead As ReportLetterhead
        Get
            Return If(Me.Pages.Count > 0, Me.ReportPages(0).Letterhead, mLetterhead)
        End Get
    End Property

    ''' <summary>
    ''' Indicates whether the ReportLetterhead is visible on all ReportPages.
    ''' </summary>
    ''' <returns></returns>
    Public Property LetterheadShowOnAllPages As Boolean
        Get
            Return mLetterheadShowOnAllPages
        End Get
        Set(value As Boolean)
            LetterheadOnAllPages(value)
            mLetterheadShowOnAllPages = value
        End Set
    End Property

    Protected Overrides Sub DocumentPageAdded(pg As DocumentPage)
        DirectCast(pg, ReportPage).DataSource = Me.DataSource
        MyBase.DocumentPageAdded(pg)
    End Sub

    Private Sub HeaderOnAllPages(value As Boolean)
        If Me.Pages IsNot Nothing Then
            Me.SuspendLayout()
            For Each pg As ReportPage In Me.ReportPages
                pg.ContextMenuItem(Header.ContextMenuStrip, "HeaderVisibleMenuItem").Checked = value
            Next
            If Not value AndAlso Me.ReportPages.Count > 0 Then
                Me.ReportPages(0).Header.ContextMenuItem("HeaderVisibleMenuItem").Checked = True
            End If
            Me.ResumeLayout()
        End If
    End Sub

    Private Sub LetterheadOnAllPages(value As Boolean)
        If Me.Pages IsNot Nothing Then
            Me.SuspendLayout()
            For Each pg As ReportPage In Me.ReportPages
                pg.ContextMenuItem(Letterhead.ContextMenuStrip, "LetterheadVisibleMenuItem").Checked = value
            Next
            If Not value AndAlso Me.ReportPages.Count > 0 Then
                Me.ReportPages(0).Letterhead.ContextMenuItem("LetterheadVisibleMenuItem").Checked = True
            End If
            Me.ResumeLayout()
        End If
    End Sub

    Protected Overrides Sub UndoRestore(ByRef dest As ObservableCollection(Of DocumentPage), ByVal src As List(Of UndoState))
        MyBase.UndoRestore(dest, src)
    End Sub

    Protected Overrides Sub UndoSave(pgs As List(Of DocumentPage))
        MyBase.UndoSave(pgs)
    End Sub

    Protected Overrides Sub MenuAddNew_Click(sender As Object, e As EventArgs)
        ' We have to override the base handler, which adds new DocumentPages,
        ' as we need to add new ReportPages.
        Me.Pages.BeginTransaction()
        Me.Pages.Add(New ReportPage())
        Me.Pages.EndTransaction()
    End Sub
End Class
