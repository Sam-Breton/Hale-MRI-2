<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ChartSummary
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
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New DataVisualization.Charting.ChartArea()
        Dim Legend1 As System.Windows.Forms.DataVisualization.Charting.Legend = New DataVisualization.Charting.Legend()
        Dim Series1 As System.Windows.Forms.DataVisualization.Charting.Series = New DataVisualization.Charting.Series()
        Chart1 = New DataVisualization.Charting.Chart()
        TableLayoutPanel1 = New TableLayoutPanel()
        PitchTable = New TableLayoutPanel()
        BladeTable = New TableLayoutPanel()
        CType(Chart1, ComponentModel.ISupportInitialize).BeginInit()
        TableLayoutPanel1.SuspendLayout()
        SuspendLayout()
        ' 
        ' Chart1
        ' 
        ChartArea1.AxisX.IsStartedFromZero = False
        ChartArea1.AxisX2.Enabled = DataVisualization.Charting.AxisEnabled.False
        ChartArea1.Name = "ChartArea1"
        Chart1.ChartAreas.Add(ChartArea1)
        TableLayoutPanel1.SetColumnSpan(Chart1, 2)
        Chart1.Dock = DockStyle.Fill
        Legend1.Name = "Legend1"
        Chart1.Legends.Add(Legend1)
        Chart1.Location = New Point(4, 4)
        Chart1.Name = "Chart1"
        Series1.ChartArea = "ChartArea1"
        Series1.Legend = "Legend1"
        Series1.Name = "Series1"
        Chart1.Series.Add(Series1)
        Chart1.Size = New Size(388, 199)
        Chart1.TabIndex = 0
        Chart1.TabStop = False
        Chart1.Text = "Summary"
        ' 
        ' TableLayoutPanel1
        ' 
        TableLayoutPanel1.ColumnCount = 2
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 12.0F))
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 88.0F))
        TableLayoutPanel1.Controls.Add(Chart1, 0, 0)
        TableLayoutPanel1.Controls.Add(PitchTable, 1, 1)
        TableLayoutPanel1.Controls.Add(BladeTable, 0, 1)
        TableLayoutPanel1.Dock = DockStyle.Fill
        TableLayoutPanel1.Location = New Point(2, 2)
        TableLayoutPanel1.Name = "TableLayoutPanel1"
        TableLayoutPanel1.Padding = New Padding(1)
        TableLayoutPanel1.RowCount = 2
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 70.0F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 30.0F))
        TableLayoutPanel1.Size = New Size(396, 296)
        TableLayoutPanel1.TabIndex = 1
        ' 
        ' PitchTable
        ' 
        PitchTable.ColumnCount = 2
        PitchTable.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50.0F))
        PitchTable.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50.0F))
        PitchTable.Dock = DockStyle.Fill
        PitchTable.Location = New Point(48, 206)
        PitchTable.Margin = New Padding(0)
        PitchTable.Name = "PitchTable"
        PitchTable.RowCount = 2
        PitchTable.RowStyles.Add(New RowStyle(SizeType.Percent, 50.0F))
        PitchTable.RowStyles.Add(New RowStyle(SizeType.Percent, 50.0F))
        PitchTable.Size = New Size(347, 89)
        PitchTable.TabIndex = 1
        ' 
        ' BladeTable
        ' 
        BladeTable.AutoSize = True
        BladeTable.ColumnCount = 1
        BladeTable.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0F))
        BladeTable.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 446.0F))
        BladeTable.Dock = DockStyle.Fill
        BladeTable.Location = New Point(1, 206)
        BladeTable.Margin = New Padding(0)
        BladeTable.Name = "BladeTable"
        BladeTable.RowCount = 1
        BladeTable.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0F))
        BladeTable.RowStyles.Add(New RowStyle(SizeType.Absolute, 2819.0F))
        BladeTable.Size = New Size(47, 89)
        BladeTable.TabIndex = 2
        ' 
        ' ChartSummary
        ' 
        AutoScaleDimensions = New SizeF(96.0F, 96.0F)
        AutoScaleMode = AutoScaleMode.Dpi
        Controls.Add(TableLayoutPanel1)
        DefaultSize = New Size(400, 300)
        DisplayName = "Summary"
        Font = New Font("Microsoft Sans Serif", 9.052497F)
        IsMovable = True
        IsSelectable = True
        IsSizeable = True
        Name = "ChartSummary"
        Padding = New Padding(2)
        Size = New Size(400, 300)
        CType(Chart1, ComponentModel.ISupportInitialize).EndInit()
        TableLayoutPanel1.ResumeLayout(False)
        TableLayoutPanel1.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents Chart1 As DataVisualization.Charting.Chart
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents PitchTable As TableLayoutPanel
    Friend WithEvents BladeTable As TableLayoutPanel

End Class
