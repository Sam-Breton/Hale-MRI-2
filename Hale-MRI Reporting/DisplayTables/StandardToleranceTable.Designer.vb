<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class StandardToleranceTable
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
        tlayoutFedtol = New TableLayoutPanel()
        tlayoutrealfedtol = New TableLayoutPanel()
        LabPAPHighLow = New Label()
        LabPAPDiff = New Label()
        LabPAPPerc = New Label()
        LabPAP = New Label()
        LabBAPHighLow = New Label()
        LabBAPDiff = New Label()
        LabBAPPerc = New Label()
        LabBAP = New Label()
        LabRadHighLow = New Label()
        LabRadDiff = New Label()
        LabRadPerc = New Label()
        LabRadius = New Label()
        LabTrackDiff = New Label()
        LabTrack = New Label()
        LabTitle = New Label()
        tlayoutFedtol.SuspendLayout()
        tlayoutrealfedtol.SuspendLayout()
        SuspendLayout()
        ' 
        ' tlayoutFedtol
        ' 
        tlayoutFedtol.ColumnCount = 1
        tlayoutFedtol.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0F))
        tlayoutFedtol.Controls.Add(tlayoutrealfedtol, 0, 1)
        tlayoutFedtol.Controls.Add(LabTitle, 0, 0)
        tlayoutFedtol.Dock = DockStyle.Fill
        tlayoutFedtol.Location = New Point(1, 1)
        tlayoutFedtol.Margin = New Padding(5, 6, 5, 6)
        tlayoutFedtol.Name = "tlayoutFedtol"
        tlayoutFedtol.RowCount = 2
        tlayoutFedtol.RowStyles.Add(New RowStyle(SizeType.Percent, 20.0F))
        tlayoutFedtol.RowStyles.Add(New RowStyle(SizeType.Percent, 80.0F))
        tlayoutFedtol.Size = New Size(492, 153)
        tlayoutFedtol.TabIndex = 0
        ' 
        ' tlayoutrealfedtol
        ' 
        tlayoutrealfedtol.BackColor = SystemColors.Control
        tlayoutrealfedtol.BackgroundImageLayout = ImageLayout.Stretch
        tlayoutrealfedtol.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
        tlayoutrealfedtol.ColumnCount = 4
        tlayoutrealfedtol.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25.0F))
        tlayoutrealfedtol.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25.0F))
        tlayoutrealfedtol.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25.0F))
        tlayoutrealfedtol.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25.0F))
        tlayoutrealfedtol.Controls.Add(LabPAPHighLow, 3, 2)
        tlayoutrealfedtol.Controls.Add(LabPAPDiff, 2, 2)
        tlayoutrealfedtol.Controls.Add(LabPAPPerc, 1, 2)
        tlayoutrealfedtol.Controls.Add(LabPAP, 0, 2)
        tlayoutrealfedtol.Controls.Add(LabBAPHighLow, 3, 1)
        tlayoutrealfedtol.Controls.Add(LabBAPDiff, 2, 1)
        tlayoutrealfedtol.Controls.Add(LabBAPPerc, 1, 1)
        tlayoutrealfedtol.Controls.Add(LabBAP, 0, 1)
        tlayoutrealfedtol.Controls.Add(LabRadHighLow, 3, 0)
        tlayoutrealfedtol.Controls.Add(LabRadDiff, 2, 0)
        tlayoutrealfedtol.Controls.Add(LabRadPerc, 1, 0)
        tlayoutrealfedtol.Controls.Add(LabRadius, 0, 0)
        tlayoutrealfedtol.Controls.Add(LabTrackDiff, 2, 3)
        tlayoutrealfedtol.Controls.Add(LabTrack, 0, 3)
        tlayoutrealfedtol.Dock = DockStyle.Fill
        tlayoutrealfedtol.ForeColor = SystemColors.ControlText
        tlayoutrealfedtol.Location = New Point(0, 30)
        tlayoutrealfedtol.Margin = New Padding(0)
        tlayoutrealfedtol.Name = "tlayoutrealfedtol"
        tlayoutrealfedtol.RowCount = 4
        tlayoutrealfedtol.RowStyles.Add(New RowStyle(SizeType.Percent, 25.0000038F))
        tlayoutrealfedtol.RowStyles.Add(New RowStyle(SizeType.Percent, 24.9999981F))
        tlayoutrealfedtol.RowStyles.Add(New RowStyle(SizeType.Percent, 24.9999981F))
        tlayoutrealfedtol.RowStyles.Add(New RowStyle(SizeType.Percent, 25.0000038F))
        tlayoutrealfedtol.RowStyles.Add(New RowStyle(SizeType.Absolute, 20.0F))
        tlayoutrealfedtol.RowStyles.Add(New RowStyle(SizeType.Absolute, 20.0F))
        tlayoutrealfedtol.Size = New Size(492, 123)
        tlayoutrealfedtol.TabIndex = 0
        ' 
        ' LabPAPHighLow
        ' 
        LabPAPHighLow.AutoSize = True
        LabPAPHighLow.Dock = DockStyle.Fill
        LabPAPHighLow.Location = New Point(372, 61)
        LabPAPHighLow.Margin = New Padding(5, 0, 5, 0)
        LabPAPHighLow.Name = "LabPAPHighLow"
        LabPAPHighLow.Size = New Size(114, 29)
        LabPAPHighLow.TabIndex = 19
        LabPAPHighLow.Text = "Label21"
        LabPAPHighLow.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabPAPDiff
        ' 
        LabPAPDiff.AutoSize = True
        LabPAPDiff.Dock = DockStyle.Fill
        LabPAPDiff.Location = New Point(250, 61)
        LabPAPDiff.Margin = New Padding(5, 0, 5, 0)
        LabPAPDiff.Name = "LabPAPDiff"
        LabPAPDiff.Size = New Size(111, 29)
        LabPAPDiff.TabIndex = 18
        LabPAPDiff.Text = "Label20"
        LabPAPDiff.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabPAPPerc
        ' 
        LabPAPPerc.AutoSize = True
        LabPAPPerc.Dock = DockStyle.Fill
        LabPAPPerc.Location = New Point(128, 61)
        LabPAPPerc.Margin = New Padding(5, 0, 5, 0)
        LabPAPPerc.Name = "LabPAPPerc"
        LabPAPPerc.Size = New Size(111, 29)
        LabPAPPerc.TabIndex = 17
        LabPAPPerc.Text = "1 %"
        LabPAPPerc.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabPAP
        ' 
        LabPAP.AutoSize = True
        LabPAP.Dock = DockStyle.Fill
        LabPAP.Location = New Point(6, 61)
        LabPAP.Margin = New Padding(5, 0, 5, 0)
        LabPAP.Name = "LabPAP"
        LabPAP.Size = New Size(111, 29)
        LabPAP.TabIndex = 16
        LabPAP.Text = "Prop Avg. Pitch"
        LabPAP.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabBAPHighLow
        ' 
        LabBAPHighLow.AutoSize = True
        LabBAPHighLow.Dock = DockStyle.Fill
        LabBAPHighLow.Location = New Point(372, 31)
        LabBAPHighLow.Margin = New Padding(5, 0, 5, 0)
        LabBAPHighLow.Name = "LabBAPHighLow"
        LabBAPHighLow.Size = New Size(114, 29)
        LabBAPHighLow.TabIndex = 15
        LabBAPHighLow.Text = "Label17"
        LabBAPHighLow.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabBAPDiff
        ' 
        LabBAPDiff.AutoSize = True
        LabBAPDiff.Dock = DockStyle.Fill
        LabBAPDiff.Location = New Point(250, 31)
        LabBAPDiff.Margin = New Padding(5, 0, 5, 0)
        LabBAPDiff.Name = "LabBAPDiff"
        LabBAPDiff.Size = New Size(111, 29)
        LabBAPDiff.TabIndex = 14
        LabBAPDiff.Text = "Label16"
        LabBAPDiff.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabBAPPerc
        ' 
        LabBAPPerc.AutoSize = True
        LabBAPPerc.Dock = DockStyle.Fill
        LabBAPPerc.Location = New Point(128, 31)
        LabBAPPerc.Margin = New Padding(5, 0, 5, 0)
        LabBAPPerc.Name = "LabBAPPerc"
        LabBAPPerc.Size = New Size(111, 29)
        LabBAPPerc.TabIndex = 13
        LabBAPPerc.Text = "1%"
        LabBAPPerc.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabBAP
        ' 
        LabBAP.AutoSize = True
        LabBAP.Dock = DockStyle.Fill
        LabBAP.Location = New Point(6, 31)
        LabBAP.Margin = New Padding(5, 0, 5, 0)
        LabBAP.Name = "LabBAP"
        LabBAP.Size = New Size(111, 29)
        LabBAP.TabIndex = 12
        LabBAP.Text = "Blade Avg. Pitch"
        LabBAP.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabRadHighLow
        ' 
        LabRadHighLow.AutoSize = True
        LabRadHighLow.Dock = DockStyle.Fill
        LabRadHighLow.Location = New Point(372, 1)
        LabRadHighLow.Margin = New Padding(5, 0, 5, 0)
        LabRadHighLow.Name = "LabRadHighLow"
        LabRadHighLow.Size = New Size(114, 29)
        LabRadHighLow.TabIndex = 3
        LabRadHighLow.Text = "Label5"
        LabRadHighLow.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabRadDiff
        ' 
        LabRadDiff.AutoSize = True
        LabRadDiff.Dock = DockStyle.Fill
        LabRadDiff.Location = New Point(250, 1)
        LabRadDiff.Margin = New Padding(5, 0, 5, 0)
        LabRadDiff.Name = "LabRadDiff"
        LabRadDiff.Size = New Size(111, 29)
        LabRadDiff.TabIndex = 2
        LabRadDiff.Text = "Label4"
        LabRadDiff.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabRadPerc
        ' 
        LabRadPerc.AutoSize = True
        LabRadPerc.Dock = DockStyle.Fill
        LabRadPerc.Location = New Point(128, 1)
        LabRadPerc.Margin = New Padding(5, 0, 5, 0)
        LabRadPerc.Name = "LabRadPerc"
        LabRadPerc.Size = New Size(111, 29)
        LabRadPerc.TabIndex = 1
        LabRadPerc.Text = "0.3 %"
        LabRadPerc.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabRadius
        ' 
        LabRadius.AutoSize = True
        LabRadius.Dock = DockStyle.Fill
        LabRadius.Location = New Point(6, 1)
        LabRadius.Margin = New Padding(5, 0, 5, 0)
        LabRadius.Name = "LabRadius"
        LabRadius.Size = New Size(111, 29)
        LabRadius.TabIndex = 0
        LabRadius.Text = "Radius"
        LabRadius.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabTrackDiff
        ' 
        LabTrackDiff.AutoSize = True
        LabTrackDiff.Dock = DockStyle.Fill
        LabTrackDiff.Location = New Point(250, 91)
        LabTrackDiff.Margin = New Padding(5, 0, 5, 0)
        LabTrackDiff.Name = "LabTrackDiff"
        LabTrackDiff.Size = New Size(111, 31)
        LabTrackDiff.TabIndex = 20
        LabTrackDiff.Text = "Label22"
        LabTrackDiff.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabTrack
        ' 
        LabTrack.AutoSize = True
        LabTrack.Dock = DockStyle.Fill
        LabTrack.Location = New Point(4, 91)
        LabTrack.Name = "LabTrack"
        LabTrack.Size = New Size(115, 31)
        LabTrack.TabIndex = 21
        LabTrack.Text = "Track"
        LabTrack.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabTitle
        ' 
        LabTitle.AutoSize = True
        LabTitle.Dock = DockStyle.Fill
        LabTitle.Font = New Font("Segoe UI", 12.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        LabTitle.Location = New Point(5, 0)
        LabTitle.Margin = New Padding(5, 0, 5, 0)
        LabTitle.Name = "LabTitle"
        LabTitle.Size = New Size(482, 30)
        LabTitle.TabIndex = 1
        LabTitle.Text = "Label1"
        LabTitle.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' StandardToleranceTable
        ' 
        AutoScaleMode = AutoScaleMode.None
        'BaseFont = New Font("Segoe UI", 12F)
        Controls.Add(tlayoutFedtol)
        DefaultSize = New Size(494, 155)
        DisplayName = "Standard Tolerance"
        Font = New Font("Segoe UI", 12.0F)
        IsMovable = True
        IsSelectable = True
        IsSizeable = True
        Margin = New Padding(5, 6, 5, 6)
        Name = "StandardToleranceTable"
        Size = New Size(494, 155)
        tlayoutFedtol.ResumeLayout(False)
        tlayoutFedtol.PerformLayout()
        tlayoutrealfedtol.ResumeLayout(False)
        tlayoutrealfedtol.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents tlayoutFedtol As TableLayoutPanel
    Friend WithEvents tlayoutrealfedtol As TableLayoutPanel
    Friend WithEvents LabPAPHighLow As Label
    Friend WithEvents LabPAPDiff As Label
    Friend WithEvents LabPAPPerc As Label
    Friend WithEvents LabPAP As Label
    Friend WithEvents LabBAPHighLow As Label
    Friend WithEvents LabBAPDiff As Label
    Friend WithEvents LabBAPPerc As Label
    Friend WithEvents LabBAP As Label
    Friend WithEvents LabRadHighLow As Label
    Friend WithEvents LabRadDiff As Label
    Friend WithEvents LabRadPerc As Label
    Friend WithEvents LabRadius As Label
    Friend WithEvents LabTrackDiff As Label
    Friend WithEvents LabTitle As Label
    Friend WithEvents LabTrack As Label

End Class
