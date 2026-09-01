Imports System.Collections.Specialized
Imports LibGlobals

Public Class DocumentPage
    Implements ICloneable

#Region "Private Members"
    Private WithEvents mDisplayControls As New ValidatingObservableCollection(Of DisplayControl)
    Private mDocumentSettings As DocumentSettings = Nothing
    Private mVerticalLimit As Integer = 0
    Private mZoom As Single = 1.0F

#End Region
#Region "Constructors"
    ''' <summary>
    ''' Default Constructor
    ''' </summary>
    Public Sub New()
        InitializeComponent()

        Me.Dock = DockStyle.None
        Me.Anchor = AnchorStyles.None ' Key for centering.
        Me.DoubleBuffered = True
        Me.Padding = New Padding(0)
        Me.Margin = New Padding(0)
    End Sub

    ''' <summary>
    ''' Copy Constructor.
    ''' </summary>
    ''' <param name="other"></param>
    Public Sub New(ByVal other As DocumentPage)
        InitializeComponent()

        Me.Document = other.Document
        Me.Name = other.Name
        Me.Dock = other.Dock
        Me.Anchor = other.Anchor
        Me.DoubleBuffered = other.DoubleBuffered
        For Each dc As DisplayControl In other.DisplayControls
            Me.DisplayControls.Add(dc.Clone())
        Next
    End Sub

    ''' <summary>
    ''' Creates a clone of this DocumentPage.
    ''' </summary>
    ''' <returns>Object</returns>
    Public Function Clone() As Object Implements ICloneable.Clone
        Return New DocumentPage(Me)
    End Function

#End Region
#Region "Public Inteface"
#Region "DocumentPage Properties and Methods"
    ''' <summary>
    ''' Current collection of DisplayControls displayed.
    ''' </summary>
    ''' <returns>ObservableCollection(Of DisplayControl)</returns>
    Public ReadOnly Property DisplayControls As IList(Of DisplayControl)
        Get
            Return mDisplayControls
        End Get
    End Property


    ''' <summary>
    ''' Current bounds set by the current printer settings.
    ''' </summary>
    ''' <returns>DocumentSettings</returns>
    Public Property Document As DocumentSettings
        Get
            Return mDocumentSettings
        End Get
        Set(value As DocumentSettings)
            Try
                DocumentSet(value)
                mDocumentSettings = value
            Finally
                ' Add any cleanup code here.
            End Try
        End Set
    End Property

    Public Property OriginalSize As Size = Size.Empty

    Public Sub TransactionBegin()
        mDisplayControls.BeginTransaction()
    End Sub

    Public Sub TransactionEnd()
        mDisplayControls.EndTransaction()
    End Sub

    ''' <summary>
    ''' Top-most location of any DisplayControl.
    ''' </summary>
    ''' <returns>Integer</returns>
    Public Property VerticalLimit As Integer
        Get
            Return mVerticalLimit * Me.Zoom
        End Get
        Set(value As Integer)
            mVerticalLimit = value
            VerticalLimitSet(value)
        End Set
    End Property

    Public Property Zoom As Single
        Get
            Return mZoom
        End Get
        Set(value As Single)
            ZoomSet(value)
            mZoom = value
        End Set
    End Property

    ''' <summary>
    ''' Current computed zoom factor.
    ''' </summary>
    ''' <returns>SizeF</returns>
    Public ReadOnly Property ZoomFactor As SizeF
        Get
            Return New SizeF(Me.Width / Me.OriginalSize.Width, Me.Height / Me.OriginalSize.Height)
        End Get
    End Property
#End Region
#End Region
#Region "Private Interface"
    Protected Overridable Sub FitToDocument(ByVal doc As DocumentSettings)

    End Sub

    Protected Overridable Sub FitToPage(ByVal controls As IList(Of DisplayControl))
        For Each dc As DisplayControl In controls
            FitToPage(dc)
        Next
    End Sub

    Protected Overridable Sub FitToPage(ByVal dc As DisplayControl)
        If dc.Parent IsNot Nothing Then
            Dim left As Integer = dc.Left
            Dim top As Integer = dc.Top
            Dim width As Integer = dc.Width
            Dim height As Integer = dc.Height
            Dim dcBounds As DisplayControl.BoundsChecks = dc.BoundsCheck(dc.Bounds)

            If dcBounds.HasFlag(DisplayControl.BoundsChecks.Width) Then width = Me.ClientRectangle.Width
            If dcBounds.HasFlag(DisplayControl.BoundsChecks.Height) Then height = Me.ClientRectangle.Height - Me.VerticalLimit
            If dcBounds.HasFlag(DisplayControl.BoundsChecks.Left) Then left = Me.ClientRectangle.Left
            If dcBounds.HasFlag(DisplayControl.BoundsChecks.Right) Then left = Me.ClientRectangle.Right - dc.Width
            If dcBounds.HasFlag(DisplayControl.BoundsChecks.Top) Then top = Me.VerticalLimit
            If dcBounds.HasFlag(DisplayControl.BoundsChecks.Bottom) Then top = Me.ClientRectangle.Bottom - dc.Height

            If dcBounds <> DisplayControl.BoundsChecks.None Then dc.SetBounds(left, top, width, height)
        End If
    End Sub

    Protected Overridable Sub DisplayControlAdded(dc As DisplayControl)
        ' 1. Suspend the control so it doesn't paint while moving/sizing
        dc.SuspendLayout()

        ' 2. Add to WinForms collection
        Me.Controls.Add(dc)

        ' 3. FORCE the control to settle its internal dimensions 
        ' (Important if dc.Zoom was set just before adding)
        dc.Update()

        ' 4. Now the page can accurately calculate its position
        FitToPage(dc)

        ' 5. Resume (False) to stay silent until the Page's EndTrans fires
        dc.ResumeLayout(False)
    End Sub

    Protected Overridable Sub DisplayControlRemoved(dc As DisplayControl)
        Me.Controls.Remove(dc)
    End Sub

    Protected Overridable Sub DocumentSet(ByVal doc As DocumentSettings)
        If doc IsNot Nothing Then
            Me.OriginalSize = New Size(doc.PaperWidth, doc.PaperHeight)
            ' FitToDocument(Me.DisplayControls, Me.OriginalSize)
            ' *** TODO: The following works, but causes a huge glitch on 
            ' screen, probably due to the Zoom call here and subsequent
            ' Zoom in DocumentViewer. Need to get FitToDocument() working 
            ' that performs the same function, squashing any out of bounds
            ' DisplayControls toward the center when the DocumentPage
            ' shrinks, but without setting Size and calling Zoom.
            Me.Size = Me.OriginalSize
            Me.Zoom = 1.0F
            FitToDocument(doc)
            FitToPage(Me.DisplayControls)
        End If
    End Sub

    Protected Overridable Sub VerticalLimitSet(ByVal limit As Integer)
        ' 1. Suspend the Page
        Me.TransactionBegin()
        Try
            ' 2. Loop through controls and shift them
            For Each dc In Me.DisplayControls
                FitToPage(dc)
            Next
        Finally
            ' 3. Resume(False). It marks the page as "dirty" but 
            ' doesn't paint because the Viewer is still suspended.
            Me.TransactionEnd()
        End Try
    End Sub

    Protected Overridable Sub ZoomSet(ByVal factor As Single)
        ' Start a nested transaction
        Me.TransactionBegin()
        Try
            ' 1. Resize the page "shell"
            Me.Size = New Size(
                CInt(Me.OriginalSize.Width * factor),
                CInt(Me.OriginalSize.Height * factor)
            )

            ' 2. Resize the children
            For Each dc As DisplayControl In Me.DisplayControls
                dc.Zoom = factor
                FitToPage(dc)
            Next
        Finally
            ' 3. This will call ResumeLayout(False) because of the event handlers.
            ' It marks the page as "needs layout" without actually doing it yet.
            Me.TransactionEnd()
        End Try
    End Sub

    Private Sub DisplayControls_BeginTrans(sender As Object, e As EventArgs) Handles mDisplayControls.BeginTrans
        Me.SuspendLayout()
    End Sub

    Private Sub DisplayControls_EndTrans(sender As Object, e As EventArgs) Handles mDisplayControls.EndTrans
        Try
            ' Resume without immediate paint
            Me.ResumeLayout(False)

            ' If you have manual logic to keep controls inside page bounds, call it here
            FitToPage(Me.DisplayControls)

            'Me.PerformLayout() ' Might be necessary in some instances.
        Catch ex As Exception
            Me.ResumeLayout(True)
        End Try
    End Sub

    Private Sub DisplayControls_CollectionChanged(sender As Object, e As NotifyCollectionChangedEventArgs) Handles mDisplayControls.CollectionChanged
        Select Case e.Action
            Case NotifyCollectionChangedAction.Add
                If e.NewItems IsNot Nothing Then
                    For Each dc As DisplayControl In e.NewItems
                        DisplayControlAdded(dc)
                    Next
                End If
            Case NotifyCollectionChangedAction.Remove
                If e.OldItems IsNot Nothing Then
                    For Each dc As DisplayControl In e.OldItems
                        DisplayControlRemoved(dc)
                    Next
                End If
            Case NotifyCollectionChangedAction.Replace
                If e.OldItems IsNot Nothing Then
                    For Each dc As DisplayControl In e.OldItems
                        DisplayControlRemoved(dc)
                    Next
                End If
                If e.NewItems IsNot Nothing Then
                    For Each dc As DisplayControl In e.NewItems
                        DisplayControlAdded(dc)
                    Next
                End If
            Case NotifyCollectionChangedAction.Reset
                Me.SuspendLayout() ' Extra safety for bulk clear.
                Try
                    ' If DisplayControlRemoved only does basic cleanup, 
                    ' it's faster to do a mass clear of the underlying Controls.
                    For i As Integer = Me.Controls.Count - 1 To 0 Step -1
                        If TypeOf Me.Controls(i) Is DisplayControl Then
                            Dim dc = DirectCast(Me.Controls(i), DisplayControl)
                            DisplayControlRemoved(dc)
                        End If
                    Next
                    ' Note: Controls.Clear() is faster, but only if the Removed logic 
                    ' doesn't need to do per-item unhooking of events.
                Finally
                    Me.ResumeLayout(False)
                End Try
        End Select
    End Sub
#End Region
End Class
