''' <summary>
''' Modification of IZoomable for DisplayControls.
''' </summary>
Public Interface IDisplayControl
    ''' <summary>
    ''' The 1:1 control coordinates.
    ''' </summary>
    ''' <returns>Point</returns>
    Property BaseLocation As Point

    ''' <summary>
    ''' The 1:1 control dimensions.
    ''' </summary>
    ''' <returns></returns>
    Property BaseSize As Size

    ''' <summary>
    ''' The dpi scaling factor.
    ''' </summary>
    ''' <returns></returns>
    Property ScaleSize As SizeF

    ''' <summary>
    ''' Method to update 1:1 bounds after a resize or move operation.
    ''' </summary>
    Sub ApplyResizeMove()

    ''' <summary>
    ''' Methods to scale control dpi.
    ''' </summary>
    ''' <param name="factor"></param>
    Sub ApplyScale(ByVal scale As SizeF)

    ''' <summary>
    ''' Methods to apply a zoom factor.
    ''' </summary>
    ''' <param name="factor"></param>
    Sub ApplyZoom(ByVal factor As SizeF)
End Interface
