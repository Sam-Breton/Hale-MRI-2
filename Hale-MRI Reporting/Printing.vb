Imports System.Drawing.Printing
Imports System.Management

Public Module Printing
    Public Class Document
        Implements ICloneable

        Public PaperWidth As Integer
        Public PaperHeight As Integer
        Public MarginLeft As Integer
        Public MarginRight As Integer
        Public MarginTop As Integer
        Public MarginBottom As Integer
        Public PrintableArea As Rectangle
        Public Scale As PrinterResolution

        ''' <summary>
        ''' Default constructor. Sets all properties to their default settings.
        ''' </summary>
        Public Sub New()
            Dim settings As New PrinterSettings()
            Dim defaultPage As PageSettings = settings.DefaultPageSettings

            PaperWidth = defaultPage.Bounds.Width
            PaperHeight = defaultPage.Bounds.Height
            MarginLeft = defaultPage.Margins.Left
            MarginRight = defaultPage.Margins.Right
            MarginTop = defaultPage.Margins.Top
            MarginBottom = defaultPage.Margins.Bottom
            PrintableArea = Rectangle.Round(defaultPage.PrintableArea)
            Scale = defaultPage.PrinterResolution
        End Sub

        ''' <summary>
        ''' Copy constructor.
        ''' </summary>
        ''' <param name="other"></param>
        Public Sub New(ByVal other As Document)
            Me.PaperWidth = other.PaperWidth
            Me.PaperHeight = other.PaperHeight
            Me.MarginLeft = other.MarginLeft
            Me.MarginRight = other.MarginRight
            Me.MarginTop = other.MarginTop
            Me.MarginBottom = other.MarginBottom
            Me.PrintableArea = other.PrintableArea
            Me.Scale = other.Scale
        End Sub

        ''' <summary>
        ''' Returns a clone of this object.
        ''' </summary>
        ''' <returns></returns>
        Public Function Clone() As Object Implements ICloneable.Clone
            Return New Document(Me)
        End Function

        ''' <summary>
        ''' PrintDocument constructor. Creates a Document from a PrintDocument.
        ''' </summary>
        ''' <param name="doc"></param>
        Public Sub New(ByVal doc As PrintDocument)
            PaperWidth = doc.DefaultPageSettings.Bounds.Width
            PaperHeight = doc.DefaultPageSettings.Bounds.Height
            MarginLeft = doc.DefaultPageSettings.Margins.Left
            MarginRight = doc.DefaultPageSettings.Margins.Right
            MarginTop = doc.DefaultPageSettings.Margins.Top
            MarginBottom = doc.DefaultPageSettings.Margins.Bottom
            PrintableArea = Rectangle.Round(doc.DefaultPageSettings.PrintableArea)
            Scale = doc.DefaultPageSettings.PrinterResolution
        End Sub

        ''' <summary>
        ''' Parameter constructor. Creates a Document from the given parameters.
        ''' </summary>
        ''' <param name="width"></param>
        ''' <param name="height"></param>
        ''' <param name="left"></param>
        ''' <param name="right"></param>
        ''' <param name="top"></param>
        ''' <param name="bottom"></param>
        ''' <param name="area"></param>
        ''' <param name="scale"></param>
        Public Sub New(
            Optional ByVal width As Integer = 0,
            Optional ByVal height As Integer = 0,
            Optional ByVal left As Integer = 0,
            Optional ByVal right As Integer = 0,
            Optional ByVal top As Integer = 0,
            Optional ByVal bottom As Integer = 0,
            Optional ByVal area As Rectangle = Nothing,
            Optional ByVal scale As PrinterResolution = Nothing
        )
            Me.PaperWidth = width
            Me.PaperHeight = height
            Me.MarginLeft = left
            Me.MarginRight = right
            Me.MarginTop = top
            Me.MarginBottom = bottom
            Me.PrintableArea = area
            Me.Scale = scale
        End Sub

        ''' <summary>
        ''' Printer constructor. Creates a Document from the given printer settings.
        ''' </summary>
        ''' <param name="paperSize"></param>
        ''' <param name="paperMargins"></param>
        ''' <param name="printableArea"></param>
        ''' <param name="printerScale"></param>
        Public Sub New(
            ByVal paperSize As PaperSize,
            ByVal paperMargins As Margins,
            ByVal printableArea As RectangleF,
            ByVal printerScale As PrinterResolution
        )
            Me.PaperWidth = paperSize.Width
            Me.PaperHeight = paperSize.Height
            Me.MarginLeft = paperMargins.Left
            Me.MarginRight = paperMargins.Right
            Me.MarginTop = paperMargins.Top
            Me.MarginBottom = paperMargins.Bottom
            Me.PrintableArea = Rectangle.Round(printableArea)
            Me.Scale = printerScale
        End Sub
    End Class

    Public Class DocumentPrinter

        Public Delegate Function PrintCallback(ByVal pageIndex As Integer) As Bitmap

        Private mPageIndex As Integer = 0
        Private WithEvents mPrintDocument As PrintDocument

        Public Property Document As Document = Nothing

        Public ReadOnly Property HasMorePages As Boolean
            Get
                Return Me.PageIndex < Me.PageCount
            End Get
        End Property

        Public Property PageCount As Integer = 0

        Public ReadOnly Property PageIndex As Integer
            Get
                Return mPageIndex
            End Get
        End Property

        Public Shared Function PageSetup(ByRef dlg As PageSetupDialog, Optional ByVal prnDocument As PrintDocument = Nothing, Optional ByVal prnSettings As PrinterSettings = Nothing, Optional ByVal pgSettings As PageSettings = Nothing) As Document
            Dim doc As Document = Nothing
            prnDocument = If(prnDocument, New PrintDocument())
            If prnSettings IsNot Nothing Then prnDocument.PrinterSettings = prnSettings
            If pgSettings IsNot Nothing Then dlg.PageSettings = pgSettings
            dlg.Document = prnDocument
            If dlg.ShowDialog() = DialogResult.OK Then
                doc = New Document(
                    dlg.PageSettings.PaperSize,
                    dlg.PageSettings.Margins,
                    dlg.PageSettings.PrintableArea,
                    dlg.PageSettings.PrinterResolution
                )
            End If
            Return doc
        End Function

        Public Sub Print()
            If mPrintDocument IsNot Nothing Then
                mPrintDocument.Print()
            End If
        End Sub

        Public Property PrintCallbackProvider As PrintCallback

        Public Property PrintDocument As PrintDocument
            Get
                Return mPrintDocument
            End Get
            Set(value As PrintDocument)
                mPrintDocument = value
            End Set
        End Property

        Private Sub BeginPrint(sender As Object, e As PrintEventArgs) Handles mPrintDocument.BeginPrint
            mPageIndex = 0
        End Sub

        Public Sub PrintPreview(ByRef dlg As PrintPreviewDialog)
            dlg.Document = mPrintDocument
            dlg.ShowDialog()
        End Sub

        Private Sub PrintPage(sender As Object, e As PrintPageEventArgs) Handles mPrintDocument.PrintPage
            If PrintCallbackProvider IsNot Nothing Then
                Using bmp As Bitmap = PrintCallbackProvider.Invoke(mPageIndex)
                    If bmp IsNot Nothing Then
                        ' 1. Calculate the available space based on your Document properties
                        Dim availableWidth As Integer = Me.Document.PaperWidth - Me.Document.MarginLeft - Me.Document.MarginRight
                        Dim availableHeight As Integer = Me.Document.PaperHeight - Me.Document.MarginTop - Me.Document.MarginBottom

                        ' 2. Calculate aspect ratio scaling
                        Dim ratioX As Double = availableWidth / bmp.Width
                        Dim ratioY As Double = availableHeight / bmp.Height
                        Dim ratio As Double = Math.Min(ratioX, ratioY)

                        Dim newWidth As Integer = CInt(bmp.Width * ratio)
                        Dim newHeight As Integer = CInt(bmp.Height * ratio)

                        ' 3. Calculate centering within your margins
                        ' We use \ for integer division
                        Dim posX As Integer = Me.Document.MarginLeft + (availableWidth - newWidth) \ 2
                        Dim posY As Integer = Me.Document.MarginTop + (availableHeight - newHeight) \ 2

                        ' 4. Draw the image
                        ' Graphics.DrawImage handles the conversion from Pixels to 1/100th inch
                        e.Graphics.DrawImage(bmp, posX, posY, newWidth, newHeight)
                    End If

                    mPageIndex += 1
                    e.HasMorePages = Me.HasMorePages
                End Using
            End If
        End Sub

        Public Shared Function IsPrinterConnected(Optional ByVal printerName As String = Nothing) As Boolean
            Try
                Dim query As String = If(String.IsNullOrEmpty(printerName), "SELECT * FROM Win32_Printer", $"SELECT * FROM Win32_Printer WHERE Name = '{printerName.Replace("'", "\'")}'")
                Dim searcher As New ManagementObjectSearcher(query)

                For Each printer As ManagementObject In searcher.Get()
                    ' Check if printer is offline or inactive based on status codes
                    If Not CBool(printer("WorkOffline")) AndAlso Convert.ToInt32(printer("PrinterStatus")) > 2 Then
                        Return True
                    End If
                Next
            Catch
                Return False
            End Try
            Return False
        End Function
    End Class
End Module
