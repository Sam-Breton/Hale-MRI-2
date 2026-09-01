<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ChordLengthTable
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
        tlayoutback = New TableLayoutPanel()
        LabTitle = New Label()
        TLayoutCLBase = New TableLayoutPanel()
        tlayoutback.SuspendLayout()
        SuspendLayout()
        ' 
        ' tlayoutback
        ' 
        tlayoutback.ColumnCount = 1
        tlayoutback.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0F))
        tlayoutback.Controls.Add(LabTitle, 0, 0)
        tlayoutback.Controls.Add(TLayoutCLBase, 0, 1)
        tlayoutback.Dock = DockStyle.Fill
        tlayoutback.Location = New Point(1, 1)
        tlayoutback.Margin = New Padding(4)
        tlayoutback.Name = "tlayoutback"
        tlayoutback.RowCount = 2
        tlayoutback.RowStyles.Add(New RowStyle(SizeType.Percent, 8.0F))
        tlayoutback.RowStyles.Add(New RowStyle(SizeType.Percent, 92.0F))
        tlayoutback.Size = New Size(498, 298)
        tlayoutback.TabIndex = 0
        ' 
        ' LabTitle
        ' 
        LabTitle.AutoSize = True
        LabTitle.Dock = DockStyle.Fill
        LabTitle.Font = New Font("Segoe UI", 12.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        LabTitle.Location = New Point(4, 0)
        LabTitle.Margin = New Padding(4, 0, 4, 0)
        LabTitle.Name = "LabTitle"
        LabTitle.Size = New Size(490, 23)
        LabTitle.TabIndex = 0
        LabTitle.Text = "Title"
        LabTitle.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' TLayoutCLBase
        ' 
        TLayoutCLBase.ColumnCount = 2
        TLayoutCLBase.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50.0F))
        TLayoutCLBase.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50.0F))
        TLayoutCLBase.Dock = DockStyle.Fill
        TLayoutCLBase.Location = New Point(0, 23)
        TLayoutCLBase.Margin = New Padding(0)
        TLayoutCLBase.Name = "TLayoutCLBase"
        TLayoutCLBase.RowCount = 2
        TLayoutCLBase.RowStyles.Add(New RowStyle(SizeType.Percent, 50.0F))
        TLayoutCLBase.RowStyles.Add(New RowStyle(SizeType.Percent, 50.0F))
        TLayoutCLBase.Size = New Size(498, 275)
        TLayoutCLBase.TabIndex = 1
        ' 
        ' ChordLengthTable
        ' 
        AutoScaleDimensions = New SizeF(8.0F, 20.0F)
        AutoScaleMode = AutoScaleMode.Font
        'BaseFont = New Font("Segoe UI", 12F)
        Controls.Add(tlayoutback)
        DefaultSize = New Size(500, 300)
        Font = New Font("Segoe UI", 9.047993F)
        IsMovable = True
        IsSelectable = True
        IsSizeable = True
        Name = "ChordLengthTable"
        Size = New Size(500, 300)
        tlayoutback.ResumeLayout(False)
        tlayoutback.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents tlayoutback As TableLayoutPanel
    Friend WithEvents LabTitle As Label
    Friend WithEvents TLayoutCLBase As TableLayoutPanel

End Class
