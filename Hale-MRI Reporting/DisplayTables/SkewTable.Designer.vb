<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SkewTable
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
        TlayoutBack = New TableLayoutPanel()
        LabTitle = New Label()
        tlayoutSkewReal = New TableLayoutPanel()
        TlayoutBack.SuspendLayout()
        SuspendLayout()
        ' 
        ' TlayoutBack
        ' 
        TlayoutBack.ColumnCount = 1
        TlayoutBack.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0F))
        TlayoutBack.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 26.0F))
        TlayoutBack.Controls.Add(LabTitle, 0, 0)
        TlayoutBack.Controls.Add(tlayoutSkewReal, 0, 1)
        TlayoutBack.Dock = DockStyle.Fill
        TlayoutBack.Location = New Point(1, 1)
        TlayoutBack.Margin = New Padding(4)
        TlayoutBack.Name = "TlayoutBack"
        TlayoutBack.RowCount = 2
        TlayoutBack.RowStyles.Add(New RowStyle(SizeType.Percent, 10.0F))
        TlayoutBack.RowStyles.Add(New RowStyle(SizeType.Percent, 90.0F))
        TlayoutBack.Size = New Size(468, 223)
        TlayoutBack.TabIndex = 0
        ' 
        ' LabTitle
        ' 
        LabTitle.AutoSize = True
        LabTitle.Dock = DockStyle.Bottom
        LabTitle.Font = New Font("Segoe UI", 12.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        LabTitle.Location = New Point(4, 1)
        LabTitle.Margin = New Padding(4, 0, 4, 0)
        LabTitle.Name = "LabTitle"
        LabTitle.Size = New Size(460, 20)
        LabTitle.TabIndex = 0
        LabTitle.Text = "Skew"
        ' 
        ' tlayoutSkewReal
        ' 
        tlayoutSkewReal.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
        tlayoutSkewReal.ColumnCount = 2
        tlayoutSkewReal.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50.0F))
        tlayoutSkewReal.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50.0F))
        tlayoutSkewReal.Dock = DockStyle.Fill
        tlayoutSkewReal.Location = New Point(0, 22)
        tlayoutSkewReal.Margin = New Padding(0)
        tlayoutSkewReal.Name = "tlayoutSkewReal"
        tlayoutSkewReal.RowCount = 2
        tlayoutSkewReal.RowStyles.Add(New RowStyle(SizeType.Percent, 50.0F))
        tlayoutSkewReal.RowStyles.Add(New RowStyle(SizeType.Percent, 50.0F))
        tlayoutSkewReal.Size = New Size(468, 201)
        tlayoutSkewReal.TabIndex = 1
        ' 
        ' SkewTable
        ' 
        AutoScaleMode = AutoScaleMode.None
        'BaseFont = New Font("Segoe UI", 12F)
        Controls.Add(TlayoutBack)
        DisplayName = "Skew"
        Font = New Font("Segoe UI", 12.0F)
        Margin = New Padding(4)
        Name = "SkewTable"
        Size = New Size(470, 225)
        TlayoutBack.ResumeLayout(False)
        TlayoutBack.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents TlayoutBack As TableLayoutPanel
    Friend WithEvents LabTitle As Label
    Friend WithEvents tlayoutSkewReal As TableLayoutPanel

End Class
