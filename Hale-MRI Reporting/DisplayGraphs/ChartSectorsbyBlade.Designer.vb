<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ChartSectorsbyBlade
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
        components = New ComponentModel.Container()
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New DataVisualization.Charting.ChartArea()
        Dim Legend1 As System.Windows.Forms.DataVisualization.Charting.Legend = New DataVisualization.Charting.Legend()
        Dim Series1 As System.Windows.Forms.DataVisualization.Charting.Series = New DataVisualization.Charting.Series()
        Chart1 = New DataVisualization.Charting.Chart()
        ContextMenuStrip1 = New ContextMenuStrip(components)
        RadiusToolStripMenuItem = New ToolStripMenuItem()
        CType(Chart1, ComponentModel.ISupportInitialize).BeginInit()
        ContextMenuStrip1.SuspendLayout()
        SuspendLayout()
        ' 
        ' Chart1
        ' 
        ChartArea1.AxisY.MajorTickMark.Enabled = False
        ChartArea1.AxisY.MinorTickMark.TickMarkStyle = DataVisualization.Charting.TickMarkStyle.InsideArea
        ChartArea1.Name = "ChartArea1"
        Chart1.ChartAreas.Add(ChartArea1)
        Chart1.Dock = DockStyle.Fill
        Legend1.Alignment = StringAlignment.Center
        Legend1.Docking = DataVisualization.Charting.Docking.Top
        Legend1.LegendStyle = DataVisualization.Charting.LegendStyle.Row
        Legend1.Name = "Legend1"
        Chart1.Legends.Add(Legend1)
        Chart1.Location = New Point(2, 1)
        Chart1.Margin = New Padding(5, 4, 5, 4)
        Chart1.Name = "Chart1"
        Series1.ChartArea = "ChartArea1"
        Series1.Legend = "Legend1"
        Series1.Name = "Series1"
        Chart1.Series.Add(Series1)
        Chart1.Size = New Size(396, 198)
        Chart1.TabIndex = 0
        Chart1.TabStop = False
        Chart1.Text = "SectorsByBlade"
        ' 
        ' ContextMenuStrip1
        ' 
        ContextMenuStrip1.Items.AddRange(New ToolStripItem() {RadiusToolStripMenuItem})
        ContextMenuStrip1.Name = "ContextMenuStrip1"
        ContextMenuStrip1.Size = New Size(110, 26)
        ' 
        ' RadiusToolStripMenuItem
        ' 
        RadiusToolStripMenuItem.Name = "RadiusToolStripMenuItem"
        RadiusToolStripMenuItem.Size = New Size(109, 22)
        RadiusToolStripMenuItem.Text = "Radius"
        ' 
        ' ChartSectorsbyBlade
        ' 
        AutoScaleDimensions = New SizeF(96.0F, 96.0F)
        AutoScaleMode = AutoScaleMode.Dpi
        Controls.Add(Chart1)
        DefaultSize = New Size(400, 200)
        DisplayName = "SectorsByBlade"
        Font = New Font("Arial", 9.067472F, FontStyle.Bold)
        IsMovable = True
        IsSelectable = True
        IsSizeable = True
        Margin = New Padding(5, 4, 5, 4)
        Name = "ChartSectorsbyBlade"
        Padding = New Padding(2, 1, 2, 1)
        Size = New Size(400, 200)
        CType(Chart1, ComponentModel.ISupportInitialize).EndInit()
        ContextMenuStrip1.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents Chart1 As DataVisualization.Charting.Chart
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents RadiusToolStripMenuItem As ToolStripMenuItem

End Class
