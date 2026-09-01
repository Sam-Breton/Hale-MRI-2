<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ChartBladesbySector
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
        Dim Series1 As System.Windows.Forms.DataVisualization.Charting.Series = New DataVisualization.Charting.Series()
        Chart1 = New DataVisualization.Charting.Chart()
        CType(Chart1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Chart1
        ' 
        ChartArea1.AxisY.MajorTickMark.Enabled = False
        ChartArea1.AxisY.MinorTickMark.TickMarkStyle = DataVisualization.Charting.TickMarkStyle.InsideArea
        ChartArea1.Name = "ChartArea1"
        Chart1.ChartAreas.Add(ChartArea1)
        Chart1.Dock = DockStyle.Fill
        Chart1.Location = New Point(2, 2)
        Chart1.Margin = New Padding(4)
        Chart1.Name = "Chart1"
        Series1.ChartArea = "ChartArea1"
        Series1.Name = "Series1"
        Chart1.Series.Add(Series1)
        Chart1.Size = New Size(396, 196)
        Chart1.TabIndex = 0
        Chart1.TabStop = False
        Chart1.Text = "BladesBySector"
        ' 
        ' ChartBladesbySector
        ' 
        AutoScaleDimensions = New SizeF(96.0F, 96.0F)
        AutoScaleMode = AutoScaleMode.Dpi
        Controls.Add(Chart1)
        Font = New Font("Arial", 9.067472F, FontStyle.Bold)
        Name = "ChartBladesbySector"
        Padding = New Padding(2)
        Size = New Size(400, 200)
        CType(Chart1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents Chart1 As DataVisualization.Charting.Chart

End Class
