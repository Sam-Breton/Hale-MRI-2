Imports System.Drawing.Printing

''' <summary>
''' Contains printer settings for WYSIWYG DocumentPage rendering.
''' </summary>
Public Class DocumentSettings
    Public PaperWidth As Integer
    Public PaperHeight As Integer
    Public MarginLeft As Integer
    Public MarginRight As Integer
    Public MarginTop As Integer
    Public MarginBottom As Integer
    Public PrintableArea As Rectangle
    Public Scale As PrinterResolution

    Public Sub New()
        Using doc As New PrintDocument()
            PaperWidth = doc.DefaultPageSettings.Bounds.Width
            PaperHeight = doc.DefaultPageSettings.Bounds.Height
            MarginLeft = doc.DefaultPageSettings.Margins.Left
            MarginRight = doc.DefaultPageSettings.Margins.Right
            MarginTop = doc.DefaultPageSettings.Margins.Top
            MarginBottom = doc.DefaultPageSettings.Margins.Bottom
            PrintableArea = Rectangle.Round(doc.DefaultPageSettings.PrintableArea)
            Scale = doc.DefaultPageSettings.PrinterResolution
        End Using
    End Sub
    ''' <summary>
    ''' Creates a new DocumentSettings object with the given optional parameters.
    ''' </summary>
    ''' <param name="width"></param>
    ''' <param name="height"></param>
    ''' <param name="left"></param>
    ''' <param name="right"></param>
    ''' <param name="top"></param>
    ''' <param name="bottom"></param>
    ''' <param name="area"></param>
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
    ''' <summary>
    ''' Creates a new DocumentSettings object from a PrintDocument.
    ''' </summary>
    ''' <param name="other"></param>
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
End Class