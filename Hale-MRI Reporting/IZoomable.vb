''' <summary>
''' Interface for absolute scaling and positioning.
''' </summary>
Public Interface IZoomable
    ''' <summary>
    ''' The 1:1 scale coordinates.
    ''' </summary>
    ''' <returns>Point</returns>
    Property BaseLocation As Point

    ''' <summary>
    ''' The 1:1 scale size.
    ''' </summary>
    ''' <returns>Size</returns>
    Property BaseSize As Size

    ''' <summary>
    ''' Method to force the UI to sync after a zoom.
    ''' </summary>
    ''' <param name="factor"></param>
    Sub ZoomSet(ByVal factor As Single)
End Interface
