<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ManualInspectionTable
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
        tlayoutManInsp = New TableLayoutPanel()
        LabBladeSurface = New Label()
        LabNO = New Label()
        LabYES = New Label()
        Label1 = New Label()
        LabBladeEdges = New Label()
        LabStaticBalance = New Label()
        LabThickness = New Label()
        LabBore = New Label()
        LabKeyway = New Label()
        TableLayoutPanel1 = New TableLayoutPanel()
        LabExceptions = New Label()
        LabApproval = New Label()
        LabDate = New Label()
        tlayoutManInsp.SuspendLayout()
        TableLayoutPanel1.SuspendLayout()
        SuspendLayout()
        ' 
        ' tlayoutManInsp
        ' 
        tlayoutManInsp.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
        tlayoutManInsp.ColumnCount = 4
        tlayoutManInsp.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 37.5F))
        tlayoutManInsp.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 37.5F))
        tlayoutManInsp.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 12.5F))
        tlayoutManInsp.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 12.5F))
        tlayoutManInsp.Controls.Add(LabBladeSurface, 0, 1)
        tlayoutManInsp.Controls.Add(LabNO, 3, 0)
        tlayoutManInsp.Controls.Add(LabYES, 2, 0)
        tlayoutManInsp.Controls.Add(Label1, 0, 0)
        tlayoutManInsp.Controls.Add(LabBladeEdges, 0, 2)
        tlayoutManInsp.Controls.Add(LabStaticBalance, 0, 3)
        tlayoutManInsp.Controls.Add(LabThickness, 0, 4)
        tlayoutManInsp.Controls.Add(LabBore, 0, 5)
        tlayoutManInsp.Controls.Add(LabKeyway, 0, 6)
        tlayoutManInsp.Controls.Add(TableLayoutPanel1, 0, 7)
        tlayoutManInsp.Dock = DockStyle.Fill
        tlayoutManInsp.Location = New Point(1, 1)
        tlayoutManInsp.Name = "tlayoutManInsp"
        tlayoutManInsp.RowCount = 10
        tlayoutManInsp.RowStyles.Add(New RowStyle(SizeType.Percent, 10.0F))
        tlayoutManInsp.RowStyles.Add(New RowStyle(SizeType.Percent, 10.0F))
        tlayoutManInsp.RowStyles.Add(New RowStyle(SizeType.Percent, 10.0F))
        tlayoutManInsp.RowStyles.Add(New RowStyle(SizeType.Percent, 10.0F))
        tlayoutManInsp.RowStyles.Add(New RowStyle(SizeType.Percent, 10.0F))
        tlayoutManInsp.RowStyles.Add(New RowStyle(SizeType.Percent, 10.0F))
        tlayoutManInsp.RowStyles.Add(New RowStyle(SizeType.Percent, 10.0F))
        tlayoutManInsp.RowStyles.Add(New RowStyle(SizeType.Percent, 10.0F))
        tlayoutManInsp.RowStyles.Add(New RowStyle(SizeType.Percent, 10.0F))
        tlayoutManInsp.RowStyles.Add(New RowStyle(SizeType.Percent, 10.0F))
        tlayoutManInsp.Size = New Size(366, 284)
        tlayoutManInsp.TabIndex = 0
        ' 
        ' LabBladeSurface
        ' 
        LabBladeSurface.AutoSize = True
        tlayoutManInsp.SetColumnSpan(LabBladeSurface, 2)
        LabBladeSurface.Dock = DockStyle.Fill
        LabBladeSurface.Location = New Point(4, 29)
        LabBladeSurface.Name = "LabBladeSurface"
        LabBladeSurface.Size = New Size(265, 27)
        LabBladeSurface.TabIndex = 3
        LabBladeSurface.Text = "Blade Surface"
        LabBladeSurface.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' LabNO
        ' 
        LabNO.AutoSize = True
        LabNO.Dock = DockStyle.Fill
        LabNO.Location = New Point(322, 1)
        LabNO.Name = "LabNO"
        LabNO.Size = New Size(40, 27)
        LabNO.TabIndex = 2
        LabNO.Text = "NO"
        LabNO.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabYES
        ' 
        LabYES.AutoSize = True
        LabYES.BackColor = SystemColors.Control
        LabYES.Dock = DockStyle.Fill
        LabYES.Location = New Point(276, 1)
        LabYES.Name = "LabYES"
        LabYES.Size = New Size(39, 27)
        LabYES.TabIndex = 1
        LabYES.Text = "YES"
        LabYES.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        tlayoutManInsp.SetColumnSpan(Label1, 2)
        Label1.Dock = DockStyle.Fill
        Label1.Location = New Point(4, 1)
        Label1.Name = "Label1"
        Label1.Size = New Size(265, 27)
        Label1.TabIndex = 0
        Label1.Text = "Manual Inspections: ACCEPTABLE"
        Label1.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' LabBladeEdges
        ' 
        LabBladeEdges.AutoSize = True
        tlayoutManInsp.SetColumnSpan(LabBladeEdges, 2)
        LabBladeEdges.Dock = DockStyle.Fill
        LabBladeEdges.Location = New Point(4, 57)
        LabBladeEdges.Name = "LabBladeEdges"
        LabBladeEdges.Size = New Size(265, 27)
        LabBladeEdges.TabIndex = 4
        LabBladeEdges.Text = "Blade Edges"
        LabBladeEdges.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' LabStaticBalance
        ' 
        LabStaticBalance.AutoSize = True
        tlayoutManInsp.SetColumnSpan(LabStaticBalance, 2)
        LabStaticBalance.Dock = DockStyle.Fill
        LabStaticBalance.Location = New Point(4, 85)
        LabStaticBalance.Name = "LabStaticBalance"
        LabStaticBalance.Size = New Size(265, 27)
        LabStaticBalance.TabIndex = 5
        LabStaticBalance.Text = "Static Balance"
        LabStaticBalance.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' LabThickness
        ' 
        LabThickness.AutoSize = True
        tlayoutManInsp.SetColumnSpan(LabThickness, 2)
        LabThickness.Dock = DockStyle.Fill
        LabThickness.Location = New Point(4, 113)
        LabThickness.Name = "LabThickness"
        LabThickness.Size = New Size(265, 27)
        LabThickness.TabIndex = 6
        LabThickness.Text = "Thickness"
        LabThickness.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' LabBore
        ' 
        LabBore.AutoSize = True
        tlayoutManInsp.SetColumnSpan(LabBore, 2)
        LabBore.Dock = DockStyle.Fill
        LabBore.Location = New Point(4, 141)
        LabBore.Name = "LabBore"
        LabBore.Size = New Size(265, 27)
        LabBore.TabIndex = 7
        LabBore.Text = "Bore"
        LabBore.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' LabKeyway
        ' 
        LabKeyway.AutoSize = True
        tlayoutManInsp.SetColumnSpan(LabKeyway, 2)
        LabKeyway.Dock = DockStyle.Fill
        LabKeyway.Location = New Point(4, 169)
        LabKeyway.Name = "LabKeyway"
        LabKeyway.Size = New Size(265, 27)
        LabKeyway.TabIndex = 8
        LabKeyway.Text = "Keyway"
        LabKeyway.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' TableLayoutPanel1
        ' 
        TableLayoutPanel1.ColumnCount = 2
        tlayoutManInsp.SetColumnSpan(TableLayoutPanel1, 4)
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50.0F))
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50.0F))
        TableLayoutPanel1.Controls.Add(LabExceptions, 0, 0)
        TableLayoutPanel1.Controls.Add(LabApproval, 0, 1)
        TableLayoutPanel1.Controls.Add(LabDate, 1, 1)
        TableLayoutPanel1.Dock = DockStyle.Fill
        TableLayoutPanel1.Location = New Point(2, 197)
        TableLayoutPanel1.Margin = New Padding(1, 0, 1, 1)
        TableLayoutPanel1.Name = "TableLayoutPanel1"
        TableLayoutPanel1.RowCount = 2
        tlayoutManInsp.SetRowSpan(TableLayoutPanel1, 3)
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 50.0F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 50.0F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Absolute, 20.0F))
        TableLayoutPanel1.Size = New Size(362, 85)
        TableLayoutPanel1.TabIndex = 9
        ' 
        ' LabExceptions
        ' 
        LabExceptions.AutoSize = True
        LabExceptions.Dock = DockStyle.Fill
        LabExceptions.Location = New Point(3, 0)
        LabExceptions.Name = "LabExceptions"
        LabExceptions.Size = New Size(175, 42)
        LabExceptions.TabIndex = 0
        LabExceptions.Text = "Exceptions"
        ' 
        ' LabApproval
        ' 
        LabApproval.AutoSize = True
        LabApproval.Dock = DockStyle.Bottom
        LabApproval.Location = New Point(3, 64)
        LabApproval.Name = "LabApproval"
        LabApproval.Size = New Size(175, 21)
        LabApproval.TabIndex = 1
        LabApproval.Text = "Approval"
        ' 
        ' LabDate
        ' 
        LabDate.AutoSize = True
        LabDate.Dock = DockStyle.Bottom
        LabDate.Location = New Point(184, 64)
        LabDate.Name = "LabDate"
        LabDate.Size = New Size(175, 21)
        LabDate.TabIndex = 2
        LabDate.Text = "Date"
        ' 
        ' ManualInspectionTable
        ' 
        AutoScaleMode = AutoScaleMode.None
        'BaseFont = New Font("Segoe UI", 12F)
        Controls.Add(tlayoutManInsp)
        DisplayName = "Manual Inspections"
        Font = New Font("Segoe UI", 12.0F)
        IsMovable = True
        IsSelectable = True
        IsSizeable = True
        Name = "ManualInspectionTable"
        Size = New Size(368, 286)
        tlayoutManInsp.ResumeLayout(False)
        tlayoutManInsp.PerformLayout()
        TableLayoutPanel1.ResumeLayout(False)
        TableLayoutPanel1.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents tlayoutManInsp As TableLayoutPanel
    Friend WithEvents LabBladeSurface As Label
    Friend WithEvents LabNO As Label
    Friend WithEvents LabYES As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents LabBladeEdges As Label
    Friend WithEvents LabStaticBalance As Label
    Friend WithEvents LabThickness As Label
    Friend WithEvents LabBore As Label
    Friend WithEvents LabKeyway As Label
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents LabExceptions As Label
    Friend WithEvents LabApproval As Label
    Friend WithEvents LabDate As Label

End Class
