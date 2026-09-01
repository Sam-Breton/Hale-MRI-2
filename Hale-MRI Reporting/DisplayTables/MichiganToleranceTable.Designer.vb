<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MichiganToleranceTable
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
        LabSPHighLow = New Label()
        LabSPDiff = New Label()
        LabSPPerc = New Label()
        LabSP = New Label()
        LabLPHighLow = New Label()
        LabLPDiff = New Label()
        LabLPPerc = New Label()
        LabLP = New Label()
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
        tlayoutFedtol.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tlayoutFedtol.Controls.Add(tlayoutrealfedtol, 0, 1)
        tlayoutFedtol.Controls.Add(LabTitle, 0, 0)
        tlayoutFedtol.Dock = DockStyle.Fill
        tlayoutFedtol.Location = New Point(2, 2)
        tlayoutFedtol.Margin = New Padding(5, 6, 5, 6)
        tlayoutFedtol.Name = "tlayoutFedtol"
        tlayoutFedtol.RowCount = 2
        tlayoutFedtol.RowStyles.Add(New RowStyle(SizeType.Percent, 10F))
        tlayoutFedtol.RowStyles.Add(New RowStyle(SizeType.Percent, 90F))
        tlayoutFedtol.RowStyles.Add(New RowStyle(SizeType.Absolute, 39F))
        tlayoutFedtol.Size = New Size(490, 198)
        tlayoutFedtol.TabIndex = 0
        ' 
        ' tlayoutrealfedtol
        ' 
        tlayoutrealfedtol.BackColor = SystemColors.Control
        tlayoutrealfedtol.BackgroundImageLayout = ImageLayout.Stretch
        tlayoutrealfedtol.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
        tlayoutrealfedtol.ColumnCount = 4
        tlayoutrealfedtol.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25F))
        tlayoutrealfedtol.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25F))
        tlayoutrealfedtol.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25F))
        tlayoutrealfedtol.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25F))
        tlayoutrealfedtol.Controls.Add(LabPAPHighLow, 3, 4)
        tlayoutrealfedtol.Controls.Add(LabPAPDiff, 2, 4)
        tlayoutrealfedtol.Controls.Add(LabPAPPerc, 1, 4)
        tlayoutrealfedtol.Controls.Add(LabPAP, 0, 4)
        tlayoutrealfedtol.Controls.Add(LabBAPHighLow, 3, 3)
        tlayoutrealfedtol.Controls.Add(LabBAPDiff, 2, 3)
        tlayoutrealfedtol.Controls.Add(LabBAPPerc, 1, 3)
        tlayoutrealfedtol.Controls.Add(LabBAP, 0, 3)
        tlayoutrealfedtol.Controls.Add(LabSPHighLow, 3, 2)
        tlayoutrealfedtol.Controls.Add(LabSPDiff, 2, 2)
        tlayoutrealfedtol.Controls.Add(LabSPPerc, 1, 2)
        tlayoutrealfedtol.Controls.Add(LabSP, 0, 2)
        tlayoutrealfedtol.Controls.Add(LabLPHighLow, 3, 1)
        tlayoutrealfedtol.Controls.Add(LabLPDiff, 2, 1)
        tlayoutrealfedtol.Controls.Add(LabLPPerc, 1, 1)
        tlayoutrealfedtol.Controls.Add(LabLP, 0, 1)
        tlayoutrealfedtol.Controls.Add(LabRadHighLow, 3, 0)
        tlayoutrealfedtol.Controls.Add(LabRadDiff, 2, 0)
        tlayoutrealfedtol.Controls.Add(LabRadPerc, 1, 0)
        tlayoutrealfedtol.Controls.Add(LabRadius, 0, 0)
        tlayoutrealfedtol.Controls.Add(LabTrackDiff, 2, 5)
        tlayoutrealfedtol.Controls.Add(LabTrack, 0, 5)
        tlayoutrealfedtol.Dock = DockStyle.Fill
        tlayoutrealfedtol.ForeColor = SystemColors.ControlText
        tlayoutrealfedtol.Location = New Point(0, 19)
        tlayoutrealfedtol.Margin = New Padding(0)
        tlayoutrealfedtol.Name = "tlayoutrealfedtol"
        tlayoutrealfedtol.RowCount = 6
        tlayoutrealfedtol.RowStyles.Add(New RowStyle(SizeType.Percent, 16.666666F))
        tlayoutrealfedtol.RowStyles.Add(New RowStyle(SizeType.Percent, 16.666666F))
        tlayoutrealfedtol.RowStyles.Add(New RowStyle(SizeType.Percent, 16.666666F))
        tlayoutrealfedtol.RowStyles.Add(New RowStyle(SizeType.Percent, 16.666666F))
        tlayoutrealfedtol.RowStyles.Add(New RowStyle(SizeType.Percent, 16.666666F))
        tlayoutrealfedtol.RowStyles.Add(New RowStyle(SizeType.Percent, 16.666666F))
        tlayoutrealfedtol.Size = New Size(490, 179)
        tlayoutrealfedtol.TabIndex = 0
        ' 
        ' LabPAPHighLow
        ' 
        LabPAPHighLow.AutoSize = True
        LabPAPHighLow.Dock = DockStyle.Fill
        LabPAPHighLow.Location = New Point(372, 117)
        LabPAPHighLow.Margin = New Padding(5, 0, 5, 0)
        LabPAPHighLow.Name = "LabPAPHighLow"
        LabPAPHighLow.Size = New Size(112, 28)
        LabPAPHighLow.TabIndex = 19
        LabPAPHighLow.Text = "Label21"
        LabPAPHighLow.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabPAPDiff
        ' 
        LabPAPDiff.AutoSize = True
        LabPAPDiff.Dock = DockStyle.Fill
        LabPAPDiff.Location = New Point(250, 117)
        LabPAPDiff.Margin = New Padding(5, 0, 5, 0)
        LabPAPDiff.Name = "LabPAPDiff"
        LabPAPDiff.Size = New Size(111, 28)
        LabPAPDiff.TabIndex = 18
        LabPAPDiff.Text = "Label20"
        LabPAPDiff.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabPAPPerc
        ' 
        LabPAPPerc.AutoSize = True
        LabPAPPerc.Dock = DockStyle.Fill
        LabPAPPerc.Location = New Point(128, 117)
        LabPAPPerc.Margin = New Padding(5, 0, 5, 0)
        LabPAPPerc.Name = "LabPAPPerc"
        LabPAPPerc.Size = New Size(111, 28)
        LabPAPPerc.TabIndex = 17
        LabPAPPerc.Text = "1 %"
        LabPAPPerc.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabPAP
        ' 
        LabPAP.AutoSize = True
        LabPAP.Dock = DockStyle.Fill
        LabPAP.Location = New Point(6, 117)
        LabPAP.Margin = New Padding(5, 0, 5, 0)
        LabPAP.Name = "LabPAP"
        LabPAP.Size = New Size(111, 28)
        LabPAP.TabIndex = 16
        LabPAP.Text = "Prop Avg. Pitch"
        LabPAP.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabBAPHighLow
        ' 
        LabBAPHighLow.AutoSize = True
        LabBAPHighLow.Dock = DockStyle.Fill
        LabBAPHighLow.Location = New Point(372, 88)
        LabBAPHighLow.Margin = New Padding(5, 0, 5, 0)
        LabBAPHighLow.Name = "LabBAPHighLow"
        LabBAPHighLow.Size = New Size(112, 28)
        LabBAPHighLow.TabIndex = 15
        LabBAPHighLow.Text = "Label17"
        LabBAPHighLow.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabBAPDiff
        ' 
        LabBAPDiff.AutoSize = True
        LabBAPDiff.Dock = DockStyle.Fill
        LabBAPDiff.Location = New Point(250, 88)
        LabBAPDiff.Margin = New Padding(5, 0, 5, 0)
        LabBAPDiff.Name = "LabBAPDiff"
        LabBAPDiff.Size = New Size(111, 28)
        LabBAPDiff.TabIndex = 14
        LabBAPDiff.Text = "Label16"
        LabBAPDiff.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabBAPPerc
        ' 
        LabBAPPerc.AutoSize = True
        LabBAPPerc.Dock = DockStyle.Fill
        LabBAPPerc.Location = New Point(128, 88)
        LabBAPPerc.Margin = New Padding(5, 0, 5, 0)
        LabBAPPerc.Name = "LabBAPPerc"
        LabBAPPerc.Size = New Size(111, 28)
        LabBAPPerc.TabIndex = 13
        LabBAPPerc.Text = "1%"
        LabBAPPerc.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabBAP
        ' 
        LabBAP.AutoSize = True
        LabBAP.Dock = DockStyle.Fill
        LabBAP.Location = New Point(6, 88)
        LabBAP.Margin = New Padding(5, 0, 5, 0)
        LabBAP.Name = "LabBAP"
        LabBAP.Size = New Size(111, 28)
        LabBAP.TabIndex = 12
        LabBAP.Text = "Blade Avg. Pitch"
        LabBAP.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabSPHighLow
        ' 
        LabSPHighLow.AutoSize = True
        LabSPHighLow.Dock = DockStyle.Fill
        LabSPHighLow.Location = New Point(372, 59)
        LabSPHighLow.Margin = New Padding(5, 0, 5, 0)
        LabSPHighLow.Name = "LabSPHighLow"
        LabSPHighLow.Size = New Size(112, 28)
        LabSPHighLow.TabIndex = 11
        LabSPHighLow.Text = "Label13"
        LabSPHighLow.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabSPDiff
        ' 
        LabSPDiff.AutoSize = True
        LabSPDiff.Dock = DockStyle.Fill
        LabSPDiff.Location = New Point(250, 59)
        LabSPDiff.Margin = New Padding(5, 0, 5, 0)
        LabSPDiff.Name = "LabSPDiff"
        LabSPDiff.Size = New Size(111, 28)
        LabSPDiff.TabIndex = 10
        LabSPDiff.Text = "Label12"
        LabSPDiff.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabSPPerc
        ' 
        LabSPPerc.AutoSize = True
        LabSPPerc.Dock = DockStyle.Fill
        LabSPPerc.Location = New Point(128, 59)
        LabSPPerc.Margin = New Padding(5, 0, 5, 0)
        LabSPPerc.Name = "LabSPPerc"
        LabSPPerc.Size = New Size(111, 28)
        LabSPPerc.TabIndex = 9
        LabSPPerc.Text = "1.5 %"
        LabSPPerc.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabSP
        ' 
        LabSP.AutoSize = True
        LabSP.Dock = DockStyle.Fill
        LabSP.Location = New Point(6, 59)
        LabSP.Margin = New Padding(5, 0, 5, 0)
        LabSP.Name = "LabSP"
        LabSP.Size = New Size(111, 28)
        LabSP.TabIndex = 8
        LabSP.Text = "Section Pitch"
        LabSP.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabLPHighLow
        ' 
        LabLPHighLow.AutoSize = True
        LabLPHighLow.Dock = DockStyle.Fill
        LabLPHighLow.Location = New Point(372, 30)
        LabLPHighLow.Margin = New Padding(5, 0, 5, 0)
        LabLPHighLow.Name = "LabLPHighLow"
        LabLPHighLow.Size = New Size(112, 28)
        LabLPHighLow.TabIndex = 7
        LabLPHighLow.Text = "Label9"
        LabLPHighLow.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabLPDiff
        ' 
        LabLPDiff.AutoSize = True
        LabLPDiff.Dock = DockStyle.Fill
        LabLPDiff.Location = New Point(250, 30)
        LabLPDiff.Margin = New Padding(5, 0, 5, 0)
        LabLPDiff.Name = "LabLPDiff"
        LabLPDiff.Size = New Size(111, 28)
        LabLPDiff.TabIndex = 6
        LabLPDiff.Text = "Label8"
        LabLPDiff.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabLPPerc
        ' 
        LabLPPerc.AutoSize = True
        LabLPPerc.Dock = DockStyle.Fill
        LabLPPerc.Location = New Point(128, 30)
        LabLPPerc.Margin = New Padding(5, 0, 5, 0)
        LabLPPerc.Name = "LabLPPerc"
        LabLPPerc.Size = New Size(111, 28)
        LabLPPerc.TabIndex = 5
        LabLPPerc.Text = "2 %"
        LabLPPerc.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabLP
        ' 
        LabLP.AutoSize = True
        LabLP.Dock = DockStyle.Fill
        LabLP.Location = New Point(6, 30)
        LabLP.Margin = New Padding(5, 0, 5, 0)
        LabLP.Name = "LabLP"
        LabLP.Size = New Size(111, 28)
        LabLP.TabIndex = 4
        LabLP.Text = "Local Pitch"
        LabLP.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabRadHighLow
        ' 
        LabRadHighLow.AutoSize = True
        LabRadHighLow.Dock = DockStyle.Fill
        LabRadHighLow.Location = New Point(372, 1)
        LabRadHighLow.Margin = New Padding(5, 0, 5, 0)
        LabRadHighLow.Name = "LabRadHighLow"
        LabRadHighLow.Size = New Size(112, 28)
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
        LabRadDiff.Size = New Size(111, 28)
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
        LabRadPerc.Size = New Size(111, 28)
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
        LabRadius.Size = New Size(111, 28)
        LabRadius.TabIndex = 0
        LabRadius.Text = "Radius"
        LabRadius.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabTrackDiff
        ' 
        LabTrackDiff.AutoSize = True
        LabTrackDiff.Dock = DockStyle.Fill
        LabTrackDiff.Location = New Point(250, 146)
        LabTrackDiff.Margin = New Padding(5, 0, 5, 0)
        LabTrackDiff.Name = "LabTrackDiff"
        LabTrackDiff.Size = New Size(111, 32)
        LabTrackDiff.TabIndex = 20
        LabTrackDiff.Text = "Label22"
        LabTrackDiff.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabTrack
        ' 
        LabTrack.AutoSize = True
        LabTrack.Dock = DockStyle.Fill
        LabTrack.Location = New Point(4, 146)
        LabTrack.Name = "LabTrack"
        LabTrack.Size = New Size(115, 32)
        LabTrack.TabIndex = 21
        LabTrack.Text = "Track"
        LabTrack.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabTitle
        ' 
        LabTitle.AutoSize = True
        LabTitle.Dock = DockStyle.Fill
        LabTitle.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        LabTitle.Location = New Point(5, 0)
        LabTitle.Margin = New Padding(5, 0, 5, 0)
        LabTitle.Name = "LabTitle"
        LabTitle.Size = New Size(480, 19)
        LabTitle.TabIndex = 1
        LabTitle.Text = "Michigan Tolerance"
        LabTitle.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' MichiganToleranceTable
        ' 
        AutoScaleMode = AutoScaleMode.None
        Controls.Add(tlayoutFedtol)
        DefaultSize = New Size(494, 202)
        DisplayName = "Michigan Tolerance"
        Font = New Font("Segoe UI", 9.062735F)
        IsMovable = True
        IsSelectable = True
        IsSizeable = True
        Margin = New Padding(5, 6, 5, 6)
        Name = "MichiganToleranceTable"
        Padding = New Padding(2)
        Size = New Size(494, 202)
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
    Friend WithEvents LabSPHighLow As Label
    Friend WithEvents LabSPDiff As Label
    Friend WithEvents LabSPPerc As Label
    Friend WithEvents LabSP As Label
    Friend WithEvents LabLPHighLow As Label
    Friend WithEvents LabLPDiff As Label
    Friend WithEvents LabLPPerc As Label
    Friend WithEvents LabLP As Label
    Friend WithEvents LabRadHighLow As Label
    Friend WithEvents LabRadDiff As Label
    Friend WithEvents LabRadPerc As Label
    Friend WithEvents LabRadius As Label
    Friend WithEvents LabTrackDiff As Label
    Friend WithEvents LabTitle As Label
    Friend WithEvents LabTrack As Label

End Class
