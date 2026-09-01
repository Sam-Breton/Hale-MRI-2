<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BoreInspectTable
    Inherits DisplayControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        tlayoutboreinsp = New TableLayoutPanel()
        LabPropWeight2 = New Label()
        LabPropweight = New Label()
        Labresidual2 = New Label()
        LabResidual = New Label()
        LabBoreInsp2 = New Label()
        LabBoreInspect = New Label()
        tlayoutboreinsp.SuspendLayout()
        SuspendLayout()
        ' 
        ' tlayoutboreinsp
        ' 
        tlayoutboreinsp.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
        tlayoutboreinsp.ColumnCount = 2
        tlayoutboreinsp.ColumnStyles.Add(New ColumnStyle())
        tlayoutboreinsp.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tlayoutboreinsp.Controls.Add(LabPropWeight2, 1, 2)
        tlayoutboreinsp.Controls.Add(LabPropweight, 0, 2)
        tlayoutboreinsp.Controls.Add(Labresidual2, 1, 1)
        tlayoutboreinsp.Controls.Add(LabResidual, 0, 1)
        tlayoutboreinsp.Controls.Add(LabBoreInsp2, 1, 0)
        tlayoutboreinsp.Controls.Add(LabBoreInspect, 0, 0)
        tlayoutboreinsp.Dock = DockStyle.Fill
        tlayoutboreinsp.Location = New Point(1, 1)
        tlayoutboreinsp.Name = "tlayoutboreinsp"
        tlayoutboreinsp.RowCount = 3
        tlayoutboreinsp.RowStyles.Add(New RowStyle(SizeType.Percent, 33.3333321F))
        tlayoutboreinsp.RowStyles.Add(New RowStyle(SizeType.Percent, 33.3333321F))
        tlayoutboreinsp.RowStyles.Add(New RowStyle(SizeType.Percent, 33.3333321F))
        tlayoutboreinsp.Size = New Size(435, 75)
        tlayoutboreinsp.TabIndex = 0
        ' 
        ' LabPropWeight2
        ' 
        LabPropWeight2.AutoSize = True
        LabPropWeight2.Dock = DockStyle.Fill
        LabPropWeight2.Font = New Font("Segoe UI", 11.25F)
        LabPropWeight2.Location = New Point(246, 49)
        LabPropWeight2.Name = "LabPropWeight2"
        LabPropWeight2.Size = New Size(185, 25)
        LabPropWeight2.TabIndex = 5
        LabPropWeight2.Text = "-"
        LabPropWeight2.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' LabPropweight
        ' 
        LabPropweight.AutoSize = True
        LabPropweight.Dock = DockStyle.Fill
        LabPropweight.Font = New Font("Segoe UI", 11.25F)
        LabPropweight.Location = New Point(4, 49)
        LabPropweight.Name = "LabPropweight"
        LabPropweight.Size = New Size(235, 25)
        LabPropweight.TabIndex = 4
        LabPropweight.Text = "Propeller Weight(kg)"
        ' 
        ' Labresidual2
        ' 
        Labresidual2.AutoSize = True
        Labresidual2.Dock = DockStyle.Fill
        Labresidual2.Font = New Font("Segoe UI", 11.25F)
        Labresidual2.Location = New Point(246, 25)
        Labresidual2.Name = "Labresidual2"
        Labresidual2.Size = New Size(185, 23)
        Labresidual2.TabIndex = 3
        Labresidual2.Text = "-"
        ' 
        ' LabResidual
        ' 
        LabResidual.AutoSize = True
        LabResidual.Dock = DockStyle.Fill
        LabResidual.Font = New Font("Segoe UI", 11.25F)
        LabResidual.Location = New Point(4, 25)
        LabResidual.Name = "LabResidual"
        LabResidual.Size = New Size(235, 23)
        LabResidual.TabIndex = 2
        LabResidual.Text = "Residual Unbalance Weight(gram)"
        LabResidual.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' LabBoreInsp2
        ' 
        LabBoreInsp2.AutoSize = True
        LabBoreInsp2.Dock = DockStyle.Fill
        LabBoreInsp2.Font = New Font("Segoe UI", 11.25F)
        LabBoreInsp2.Location = New Point(246, 1)
        LabBoreInsp2.Name = "LabBoreInsp2"
        LabBoreInsp2.Size = New Size(185, 23)
        LabBoreInsp2.TabIndex = 1
        LabBoreInsp2.Text = "-"
        LabBoreInsp2.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' LabBoreInspect
        ' 
        LabBoreInspect.AutoSize = True
        LabBoreInspect.Dock = DockStyle.Fill
        LabBoreInspect.Font = New Font("Segoe UI", 11.25F)
        LabBoreInspect.Location = New Point(4, 1)
        LabBoreInspect.Name = "LabBoreInspect"
        LabBoreInspect.Size = New Size(235, 23)
        LabBoreInspect.TabIndex = 0
        LabBoreInspect.Text = "Bore/Key Inspected By"
        LabBoreInspect.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' BoreInspectTable
        ' 
        AutoScaleMode = AutoScaleMode.None
        'BaseFont = New Font("Segoe UI", 12F)
        Controls.Add(tlayoutboreinsp)
        DefaultSize = New Size(437, 77)
        DisplayName = "Bore Inspect"
        Font = New Font("Segoe UI", 9.13697052F)
        IsMovable = True
        IsSelectable = True
        IsSizeable = True
        Name = "BoreInspectTable"
        Size = New Size(437, 77)
        tlayoutboreinsp.ResumeLayout(False)
        tlayoutboreinsp.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents tlayoutboreinsp As TableLayoutPanel
    Friend WithEvents LabPropWeight2 As Label
    Friend WithEvents LabPropweight As Label
    Friend WithEvents Labresidual2 As Label
    Friend WithEvents LabResidual As Label
    Friend WithEvents LabBoreInsp2 As Label
    Friend WithEvents LabBoreInspect As Label

End Class
