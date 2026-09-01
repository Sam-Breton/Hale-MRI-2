Imports System.Collections.Specialized
Imports System.ComponentModel
Imports LibDatabase.Models

Public Class ReportViewer
    Inherits DocumentViewer

    Private WithEvents mHeader As ReportHeader
    Private mHeaderShowOnAllPages As Boolean = False
    Private WithEvents mLetterhead As ReportLetterhead
    Private mLetterheadShowOnAllPages As Boolean = False

    ''' <summary>
    ''' Default constructor.
    ''' </summary>
    Public Sub New()
        MyBase.New()
        InitializeComponent()
        mHeader = New ReportHeader()
        mLetterhead = New ReportLetterhead()
        AddHandler mHeader.VisibleControls.CollectionChanged, AddressOf Me.HeaderVisibleControls_CollectionChanged
        mHeader.Visible = False
        mLetterhead.Visible = False
    End Sub

    ''' <summary>
    ''' The ReportHeader's list of visible items.
    ''' </summary>
    ''' <returns></returns>
    Public Property DataSource As BindingList(Of HeaderView)

    ''' <summary>
    ''' The current ReportHeader.
    ''' </summary>
    ''' <returns>ReportHeader</returns>
    Public ReadOnly Property Header As ReportHeader
        Get
            Return mHeader
        End Get
    End Property


    Public Property HeaderContextMenuStrip As ContextMenuStrip
        Get
            Return mHeader.ContextMenuStrip
        End Get
        Set(value As ContextMenuStrip)
            HeaderContextMenuStripSet(value)
            mHeader.ContextMenuStrip = value
        End Set
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

    ''' <summary>
    ''' The current ReportLetterhead.
    ''' </summary>
    ''' <returns>ReportLetterhead</returns>
    Public ReadOnly Property Letterhead As ReportLetterhead
        Get
            'Return mLetterhead
            Return mLetterhead
        End Get
    End Property

    Public Property LetterheadContextMenuStrip As ContextMenuStrip
        Get
            Return mLetterhead.ContextMenuStrip
        End Get
        Set(value As ContextMenuStrip)
            LetterheadContextMenuStripSet(value)
            mLetterhead.ContextMenuStrip = value
        End Set
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
    ''' <summary>
    ''' Cast of base DocumentPage collection to ReportPage.
    ''' </summary>
    ''' <returns>IEnumerable(Of ReportPage)</returns>
    Public ReadOnly Property ReportPages As IList(Of ReportPage)
        Get
            Return Me.Pages.Cast(Of ReportPage)().ToList()
        End Get
    End Property

    Protected Overrides Sub DocumentPageAdded(pg As DocumentPage)
        If TypeOf pg Is ReportPage Then
            Dim rp As ReportPage = DirectCast(pg, ReportPage)
            rp.DataSource = Me.DataSource
            rp.Header.BorderStyle = mHeader.BorderStyle
            rp.Header.ContextMenuStrip = mHeader.ContextMenuStrip
            rp.Header.VisibleItems = mHeader.VisibleItems
            If Me.Pages.Count = 1 Then
                rp.Header.Visible = mHeader.Visible
            Else
                rp.Header.Visible = rp.Header.Visible And mHeaderShowOnAllPages
            End If
            rp.Letterhead.BorderStyle = mLetterhead.BorderStyle
            rp.Letterhead.ContextMenuStrip = mLetterhead.ContextMenuStrip
            rp.Letterhead.ImageLocation = mLetterhead.ImageLocation
            rp.Letterhead.SizeMode = mLetterhead.SizeMode
            If Me.Pages.Count = 1 Then
                rp.Letterhead.Visible = mLetterhead.Visible
            Else
                rp.Letterhead.Visible = rp.Letterhead.Visible And mLetterheadShowOnAllPages
            End If
        End If
        MyBase.DocumentPageAdded(pg)
    End Sub

    Protected Overrides Function DocumentPageAddNew(Optional ByVal pg As DocumentPage = Nothing) As DocumentPage
        If pg Is Nothing Then
            pg = New ReportPage()
        End If
        Return MyBase.DocumentPageAddNew(pg)
    End Function

    Private Sub HeaderContextMenuStripSet(ByVal menu As ContextMenuStrip)
        For Each rp As ReportPage In Me.ReportPages
            rp.Header.ContextMenuStrip = menu
        Next
    End Sub

    Private Sub HeaderOnAllPages(value As Boolean)
        For i As Integer = 1 To Me.Pages.Count - 1
            Dim pg As ReportPage = DirectCast(Me.Pages(i), ReportPage)
            pg.Header.BorderStyle = mHeader.BorderStyle
            pg.Header.Visible = value
        Next
    End Sub

    Private Sub LetterheadContextMenuStripSet(ByVal menu As ContextMenuStrip)
        For Each rp As ReportPage In Me.ReportPages
            rp.Letterhead.ContextMenuStrip = menu
        Next
    End Sub

    Private Sub LetterheadOnAllPages(value As Boolean)
        For i As Integer = 1 To Me.Pages.Count - 1
            Dim pg As ReportPage = DirectCast(Me.Pages(i), ReportPage)
            pg.Letterhead.BorderStyle = mLetterhead.BorderStyle
            pg.Letterhead.SizeMode = mLetterhead.SizeMode
            pg.Letterhead.Visible = value
        Next
    End Sub

    Protected Overrides Sub UndoRestore(ByVal restoreTo As IList(Of DocumentPage), ByVal restoreFrom As IList(Of PageData))
        MyBase.UndoRestore(restoreTo, restoreFrom)
    End Sub

    Protected Overrides Sub UndoSave(ByVal pgs As IList(Of DocumentPage))
        MyBase.UndoSave(pgs)
    End Sub
#Region "Event Handlers"
    Private Sub Header_DataSourceChanged(sender As Object, e As EventArgs) Handles mHeader.DataSourceChanged
        For Each rp As ReportPage In Me.ReportPages
            rp.Header.DataSource = mHeader.DataSource
        Next
    End Sub
    Private Sub Header_BorderStyleChanged(sender As Object, e As EventArgs) Handles mHeader.BorderStyleChanged
        For Each rp As ReportPage In Me.ReportPages
            rp.Header.BorderStyle = mHeader.BorderStyle
        Next
    End Sub

    Private Sub Header_VisibleChanged(sender As Object, e As EventArgs) Handles mHeader.VisibleChanged
        For Each rp As ReportPage In Me.ReportPages
            rp.Header.Visible = mHeader.Visible
        Next
    End Sub

    Private Sub HeaderVisibleControls_CollectionChanged(sender As Object, e As NotifyCollectionChangedEventArgs)
        For Each rp As ReportPage In Me.ReportPages
            rp.Header.VisibleItems = mHeader.VisibleItems   ' This works, but is probably terribly inefficient.
        Next
    End Sub

    Private Sub Letterhead_BorderStyleChanged(sender As Object, e As EventArgs) Handles mLetterhead.BorderStyleChanged
        For Each rp As ReportPage In Me.ReportPages
            rp.Letterhead.BorderStyle = mLetterhead.BorderStyle
        Next
    End Sub

    Private Sub Letterhead_ImageChanged(sender As Object, e As EventArgs) Handles mLetterhead.ImageChanged
        For Each rp As ReportPage In Me.ReportPages
            rp.Letterhead.ImageLocation = mLetterhead.ImageLocation
        Next
    End Sub

    Private Sub Letterhead_SizeModeChanged(sender As Object, e As EventArgs) Handles mLetterhead.SizeModeChanged
        For Each rp As ReportPage In Me.ReportPages
            rp.Letterhead.SizeMode = mLetterhead.SizeMode
        Next
    End Sub

    Private Sub Letterhead_VisibleChanged(sender As Object, e As EventArgs) Handles mLetterhead.VisibleChanged
        For Each rp As ReportPage In Me.ReportPages
            rp.Letterhead.Visible = mLetterhead.Visible
        Next
    End Sub
#End Region
End Class
