<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TestDisplayControl
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
        Dim ChartArea2 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New DataVisualization.Charting.ChartArea()
        Dim Legend2 As System.Windows.Forms.DataVisualization.Charting.Legend = New DataVisualization.Charting.Legend()
        Dim Series2 As System.Windows.Forms.DataVisualization.Charting.Series = New DataVisualization.Charting.Series()
        TableLayoutPanel1 = New TableLayoutPanel()
        Chart1 = New DataVisualization.Charting.Chart()
        TxtA = New TextBox()
        TxtB = New TextBox()
        TxtC = New TextBox()
        GroupBox1 = New GroupBox()
        LabA = New Label()
        LabB = New Label()
        LabC = New Label()
        LabTitle = New Label()
        TableLayoutPanel2 = New TableLayoutPanel()
        RadioButton1 = New RadioButton()
        RadioButton2 = New RadioButton()
        RadioButton3 = New RadioButton()
        RadioButton4 = New RadioButton()
        TableLayoutPanel1.SuspendLayout()
        CType(Chart1, ComponentModel.ISupportInitialize).BeginInit()
        GroupBox1.SuspendLayout()
        TableLayoutPanel2.SuspendLayout()
        SuspendLayout()
        ' 
        ' TableLayoutPanel1
        ' 
        TableLayoutPanel1.ColumnCount = 2
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 37.03704F))
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 62.9629631F))
        TableLayoutPanel1.Controls.Add(Chart1, 1, 1)
        TableLayoutPanel1.Controls.Add(TxtA, 1, 2)
        TableLayoutPanel1.Controls.Add(TxtB, 1, 3)
        TableLayoutPanel1.Controls.Add(TxtC, 1, 4)
        TableLayoutPanel1.Controls.Add(GroupBox1, 0, 1)
        TableLayoutPanel1.Controls.Add(LabA, 0, 2)
        TableLayoutPanel1.Controls.Add(LabB, 0, 3)
        TableLayoutPanel1.Controls.Add(LabC, 0, 4)
        TableLayoutPanel1.Controls.Add(LabTitle, 0, 0)
        TableLayoutPanel1.Dock = DockStyle.Fill
        TableLayoutPanel1.Location = New Point(1, 1)
        TableLayoutPanel1.Margin = New Padding(4)
        TableLayoutPanel1.Name = "TableLayoutPanel1"
        TableLayoutPanel1.RowCount = 5
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Absolute, 28F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 66.47059F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 11.7647066F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 11.1764708F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 10.5882359F))
        TableLayoutPanel1.Size = New Size(560, 326)
        TableLayoutPanel1.TabIndex = 0
        ' 
        ' Chart1
        ' 
        ChartArea2.Name = "ChartArea1"
        Chart1.ChartAreas.Add(ChartArea2)
        Chart1.Dock = DockStyle.Fill
        Legend2.Name = "Legend1"
        Chart1.Legends.Add(Legend2)
        Chart1.Location = New Point(211, 32)
        Chart1.Margin = New Padding(4)
        Chart1.Name = "Chart1"
        Series2.ChartArea = "ChartArea1"
        Series2.Legend = "Legend1"
        Series2.Name = "Series1"
        Chart1.Series.Add(Series2)
        Chart1.Size = New Size(345, 190)
        Chart1.TabIndex = 0
        Chart1.TabStop = False
        Chart1.Text = "Chart1"
        ' 
        ' TxtA
        ' 
        TxtA.Location = New Point(211, 230)
        TxtA.Margin = New Padding(4)
        TxtA.Name = "TxtA"
        TxtA.Size = New Size(345, 24)
        TxtA.TabIndex = 1
        TxtA.TabStop = False
        ' 
        ' TxtB
        ' 
        TxtB.Location = New Point(211, 265)
        TxtB.Margin = New Padding(4)
        TxtB.Name = "TxtB"
        TxtB.Size = New Size(345, 24)
        TxtB.TabIndex = 2
        TxtB.TabStop = False
        ' 
        ' TxtC
        ' 
        TxtC.Location = New Point(211, 298)
        TxtC.Margin = New Padding(4)
        TxtC.Name = "TxtC"
        TxtC.Size = New Size(345, 24)
        TxtC.TabIndex = 3
        TxtC.TabStop = False
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Anchor = AnchorStyles.None
        GroupBox1.Controls.Add(TableLayoutPanel2)
        GroupBox1.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        GroupBox1.Location = New Point(4, 32)
        GroupBox1.Margin = New Padding(4)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Padding = New Padding(4)
        GroupBox1.Size = New Size(199, 190)
        GroupBox1.TabIndex = 4
        GroupBox1.TabStop = False
        GroupBox1.Text = "GroupBox1"
        ' 
        ' LabA
        ' 
        LabA.Anchor = AnchorStyles.Right
        LabA.AutoSize = True
        LabA.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold)
        LabA.Location = New Point(157, 236)
        LabA.Margin = New Padding(4, 0, 4, 0)
        LabA.Name = "LabA"
        LabA.Size = New Size(46, 15)
        LabA.TabIndex = 5
        LabA.Text = "Label A"
        ' 
        ' LabB
        ' 
        LabB.Anchor = AnchorStyles.Right
        LabB.AutoSize = True
        LabB.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold)
        LabB.Location = New Point(158, 270)
        LabB.Margin = New Padding(4, 0, 4, 0)
        LabB.Name = "LabB"
        LabB.Size = New Size(45, 15)
        LabB.TabIndex = 6
        LabB.Text = "Label B"
        ' 
        ' LabC
        ' 
        LabC.Anchor = AnchorStyles.Right
        LabC.AutoSize = True
        LabC.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold)
        LabC.Location = New Point(158, 302)
        LabC.Margin = New Padding(4, 0, 4, 0)
        LabC.Name = "LabC"
        LabC.Size = New Size(45, 15)
        LabC.TabIndex = 7
        LabC.Text = "Label C"
        ' 
        ' LabTitle
        ' 
        LabTitle.AutoSize = True
        TableLayoutPanel1.SetColumnSpan(LabTitle, 2)
        LabTitle.Dock = DockStyle.Fill
        LabTitle.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        LabTitle.Location = New Point(4, 0)
        LabTitle.Margin = New Padding(4, 0, 4, 0)
        LabTitle.Name = "LabTitle"
        LabTitle.Size = New Size(552, 28)
        LabTitle.TabIndex = 8
        LabTitle.Text = "Title"
        ' 
        ' TableLayoutPanel2
        ' 
        TableLayoutPanel2.ColumnCount = 1
        TableLayoutPanel2.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        TableLayoutPanel2.Controls.Add(RadioButton4, 0, 3)
        TableLayoutPanel2.Controls.Add(RadioButton3, 0, 2)
        TableLayoutPanel2.Controls.Add(RadioButton2, 0, 1)
        TableLayoutPanel2.Controls.Add(RadioButton1, 0, 0)
        TableLayoutPanel2.Dock = DockStyle.Fill
        TableLayoutPanel2.Location = New Point(4, 20)
        TableLayoutPanel2.Name = "TableLayoutPanel2"
        TableLayoutPanel2.RowCount = 4
        TableLayoutPanel2.RowStyles.Add(New RowStyle(SizeType.Percent, 25F))
        TableLayoutPanel2.RowStyles.Add(New RowStyle(SizeType.Percent, 25F))
        TableLayoutPanel2.RowStyles.Add(New RowStyle(SizeType.Percent, 25F))
        TableLayoutPanel2.RowStyles.Add(New RowStyle(SizeType.Percent, 25F))
        TableLayoutPanel2.Size = New Size(191, 166)
        TableLayoutPanel2.TabIndex = 0
        ' 
        ' RadioButton1
        ' 
        RadioButton1.Anchor = AnchorStyles.Left
        RadioButton1.AutoSize = True
        RadioButton1.Checked = True
        RadioButton1.Location = New Point(3, 11)
        RadioButton1.Name = "RadioButton1"
        RadioButton1.Size = New Size(96, 19)
        RadioButton1.TabIndex = 0
        RadioButton1.TabStop = True
        RadioButton1.Text = "RadioButton1"
        RadioButton1.UseVisualStyleBackColor = True
        ' 
        ' RadioButton2
        ' 
        RadioButton2.Anchor = AnchorStyles.Left
        RadioButton2.AutoSize = True
        RadioButton2.Location = New Point(3, 52)
        RadioButton2.Name = "RadioButton2"
        RadioButton2.Size = New Size(98, 19)
        RadioButton2.TabIndex = 1
        RadioButton2.Text = "RadioButton2"
        RadioButton2.UseVisualStyleBackColor = True
        ' 
        ' RadioButton3
        ' 
        RadioButton3.Anchor = AnchorStyles.Left
        RadioButton3.AutoSize = True
        RadioButton3.Location = New Point(3, 93)
        RadioButton3.Name = "RadioButton3"
        RadioButton3.Size = New Size(98, 19)
        RadioButton3.TabIndex = 2
        RadioButton3.Text = "RadioButton3"
        RadioButton3.UseVisualStyleBackColor = True
        ' 
        ' RadioButton4
        ' 
        RadioButton4.Anchor = AnchorStyles.Left
        RadioButton4.AutoSize = True
        RadioButton4.Location = New Point(3, 135)
        RadioButton4.Name = "RadioButton4"
        RadioButton4.Size = New Size(98, 19)
        RadioButton4.TabIndex = 3
        RadioButton4.Text = "RadioButton4"
        RadioButton4.UseVisualStyleBackColor = True
        ' 
        ' TestDisplayControl
        ' 
        AutoScaleMode = AutoScaleMode.None
        Controls.Add(TableLayoutPanel1)
        DefaultSize = New Size(562, 328)
        Font = New Font("Segoe UI", 9.043447F)
        IsMovable = True
        IsSelectable = True
        IsSizeable = True
        Margin = New Padding(4)
        Name = "TestDisplayControl"
        Size = New Size(562, 328)
        TableLayoutPanel1.ResumeLayout(False)
        TableLayoutPanel1.PerformLayout()
        CType(Chart1, ComponentModel.ISupportInitialize).EndInit()
        GroupBox1.ResumeLayout(False)
        TableLayoutPanel2.ResumeLayout(False)
        TableLayoutPanel2.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents Chart1 As DataVisualization.Charting.Chart
    Friend WithEvents TxtA As TextBox
    Friend WithEvents TxtB As TextBox
    Friend WithEvents TxtC As TextBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents LabA As Label
    Friend WithEvents LabB As Label
    Friend WithEvents LabC As Label
    Friend WithEvents LabTitle As Label
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents RadioButton4 As RadioButton
    Friend WithEvents RadioButton3 As RadioButton
    Friend WithEvents RadioButton2 As RadioButton
    Friend WithEvents RadioButton1 As RadioButton

End Class
