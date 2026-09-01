<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class RadiiAveragesTable
    Inherits DisplayControl

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
        tLayoutRABase = New TableLayoutPanel()
        SuspendLayout()
        ' 
        ' tLayoutRABase
        ' 
        tLayoutRABase.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
        tLayoutRABase.ColumnCount = 2
        tLayoutRABase.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        tLayoutRABase.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        tLayoutRABase.Dock = DockStyle.Fill
        tLayoutRABase.Location = New Point(1, 1)
        tLayoutRABase.Name = "tLayoutRABase"
        tLayoutRABase.RowCount = 2
        tLayoutRABase.RowStyles.Add(New RowStyle(SizeType.Percent, 50F))
        tLayoutRABase.RowStyles.Add(New RowStyle(SizeType.Percent, 50F))
        tLayoutRABase.Size = New Size(676, 367)
        tLayoutRABase.TabIndex = 0
        ' 
        ' RadiiAveragesTable
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(tLayoutRABase)
        IsMovable = True
        IsSelectable = True
        IsSizeable = True
        Name = "RadiiAveragesTable"
        Size = New Size(678, 369)
        ResumeLayout(False)
    End Sub

    Friend WithEvents tLayoutRABase As TableLayoutPanel

End Class
