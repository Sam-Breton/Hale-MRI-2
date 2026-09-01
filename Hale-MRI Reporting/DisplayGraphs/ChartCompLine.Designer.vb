<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ChartCompLine
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
        Dim Series2 As System.Windows.Forms.DataVisualization.Charting.Series = New DataVisualization.Charting.Series()
        Dim Series3 As System.Windows.Forms.DataVisualization.Charting.Series = New DataVisualization.Charting.Series()
        Dim Series4 As System.Windows.Forms.DataVisualization.Charting.Series = New DataVisualization.Charting.Series()
        Chart1 = New DataVisualization.Charting.Chart()
        CType(Chart1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Chart1
        ' 
        ChartArea1.AxisX.IntervalType = DataVisualization.Charting.DateTimeIntervalType.Number
        ChartArea1.Name = "ChartArea1"
        Chart1.ChartAreas.Add(ChartArea1)
        Chart1.Dock = DockStyle.Fill
        Legend1.Name = "Legend1"
        Chart1.Legends.Add(Legend1)
        Chart1.Location = New Point(2, 2)
        Chart1.Name = "Chart1"
        Series1.ChartArea = "ChartArea1"
        Series1.ChartType = DataVisualization.Charting.SeriesChartType.Line
        Series1.Legend = "Legend1"
        Series1.Name = "Local Height"
        Series2.ChartArea = "ChartArea1"
        Series2.ChartType = DataVisualization.Charting.SeriesChartType.Line
        Series2.Legend = "Legend1"
        Series2.Name = "Ref"
        Series3.ChartArea = "ChartArea1"
        Series3.ChartType = DataVisualization.Charting.SeriesChartType.Line
        Series3.Legend = "Legend1"
        Series3.Name = "TolHigh"
        Series4.ChartArea = "ChartArea1"
        Series4.ChartType = DataVisualization.Charting.SeriesChartType.Line
        Series4.Legend = "Legend1"
        Series4.Name = "TolLow"
        Chart1.Series.Add(Series1)
        Chart1.Series.Add(Series2)
        Chart1.Series.Add(Series3)
        Chart1.Series.Add(Series4)
        Chart1.Size = New Size(396, 196)
        Chart1.TabIndex = 0
        Chart1.TabStop = False
        Chart1.Text = "CompLine"
        ' 
        ' ChartCompLine
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        Controls.Add(Chart1)
        DefaultSize = New Size(400, 200)
        DisplayName = "CompLine"
        Font = New Font("Arial", 9.067472F, FontStyle.Bold)
        IsMovable = True
        IsSelectable = True
        IsSizeable = True
        Name = "ChartCompLine"
        Padding = New Padding(2)
        Size = New Size(400, 200)
        CType(Chart1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents Chart1 As DataVisualization.Charting.Chart

End Class
