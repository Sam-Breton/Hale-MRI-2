<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class LocalPitchTableReport
    Inherits Hale_MRI_Reporting.DisplayControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        tLayoutLPBase = New TableLayoutPanel()
        SuspendLayout()
        ' 
        ' tLayoutLPBase
        ' 
        tLayoutLPBase.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
        tLayoutLPBase.ColumnCount = 2
        tLayoutLPBase.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        tLayoutLPBase.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        tLayoutLPBase.Dock = DockStyle.Fill
        tLayoutLPBase.Location = New Point(1, 1)
        tLayoutLPBase.Margin = New Padding(4)
        tLayoutLPBase.Name = "tLayoutLPBase"
        tLayoutLPBase.RowCount = 2
        tLayoutLPBase.RowStyles.Add(New RowStyle(SizeType.Percent, 50F))
        tLayoutLPBase.RowStyles.Add(New RowStyle(SizeType.Percent, 50F))
        tLayoutLPBase.Size = New Size(776, 408)
        tLayoutLPBase.TabIndex = 0
        ' 
        ' LocalPitchTableReport
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        DefaultSize = New Size(800, 600)
        DisplayName = "Local Pitch Table"
        IsMovable = True
        IsSelectable = True
        IsSizeable = True
        Name = "LocalPitchTableReport"
        Controls.Add(tLayoutLPBase)
        Margin = New Padding(4)
        Name = "LocalPitchTableReport"
        Size = New Size(778, 410)
        ResumeLayout(False)
    End Sub

    Friend WithEvents tLayoutLPBase As TableLayoutPanel

End Class
