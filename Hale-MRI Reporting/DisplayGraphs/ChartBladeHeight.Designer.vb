<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ChartBladeHeight
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
        components = New ComponentModel.Container()
        Dim ChartArea3 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New DataVisualization.Charting.ChartArea()
        Dim Legend3 As System.Windows.Forms.DataVisualization.Charting.Legend = New DataVisualization.Charting.Legend()
        Dim Series3 As System.Windows.Forms.DataVisualization.Charting.Series = New DataVisualization.Charting.Series()
        ContextMenuStrip1 = New ContextMenuStrip(components)
        ReferenceBladeToolStripMenuItem = New ToolStripMenuItem()
        ReferencePointToolStripMenuItem = New ToolStripMenuItem()
        LEToolStripMenuItem = New ToolStripMenuItem()
        MidToolStripMenuItem = New ToolStripMenuItem()
        TEToolStripMenuItem = New ToolStripMenuItem()
        ReferenceRadiusToolStripMenuItem = New ToolStripMenuItem()
        Chart1 = New DataVisualization.Charting.Chart()
        ContextMenuStrip1.SuspendLayout()
        CType(Chart1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' ContextMenuStrip1
        ' 
        ContextMenuStrip1.Items.AddRange(New ToolStripItem() {ReferenceBladeToolStripMenuItem, ReferencePointToolStripMenuItem, ReferenceRadiusToolStripMenuItem})
        ContextMenuStrip1.Name = "ContextMenuStrip1"
        ContextMenuStrip1.Size = New Size(165, 70)
        ' 
        ' ReferenceBladeToolStripMenuItem
        ' 
        ReferenceBladeToolStripMenuItem.Enabled = False
        ReferenceBladeToolStripMenuItem.Name = "ReferenceBladeToolStripMenuItem"
        ReferenceBladeToolStripMenuItem.Size = New Size(164, 22)
        ReferenceBladeToolStripMenuItem.Text = "Reference Blade"
        ' 
        ' ReferencePointToolStripMenuItem
        ' 
        ReferencePointToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {LEToolStripMenuItem, MidToolStripMenuItem, TEToolStripMenuItem})
        ReferencePointToolStripMenuItem.Enabled = False
        ReferencePointToolStripMenuItem.Name = "ReferencePointToolStripMenuItem"
        ReferencePointToolStripMenuItem.Size = New Size(164, 22)
        ReferencePointToolStripMenuItem.Text = "Reference Point"
        ' 
        ' LEToolStripMenuItem
        ' 
        LEToolStripMenuItem.CheckOnClick = True
        LEToolStripMenuItem.Name = "LEToolStripMenuItem"
        LEToolStripMenuItem.Size = New Size(95, 22)
        LEToolStripMenuItem.Text = "LE"
        ' 
        ' MidToolStripMenuItem
        ' 
        MidToolStripMenuItem.CheckOnClick = True
        MidToolStripMenuItem.Name = "MidToolStripMenuItem"
        MidToolStripMenuItem.Size = New Size(95, 22)
        MidToolStripMenuItem.Text = "Mid"
        ' 
        ' TEToolStripMenuItem
        ' 
        TEToolStripMenuItem.CheckOnClick = True
        TEToolStripMenuItem.Name = "TEToolStripMenuItem"
        TEToolStripMenuItem.Size = New Size(95, 22)
        TEToolStripMenuItem.Text = "TE"
        ' 
        ' ReferenceRadiusToolStripMenuItem
        ' 
        ReferenceRadiusToolStripMenuItem.Enabled = False
        ReferenceRadiusToolStripMenuItem.Name = "ReferenceRadiusToolStripMenuItem"
        ReferenceRadiusToolStripMenuItem.Size = New Size(164, 22)
        ReferenceRadiusToolStripMenuItem.Text = "Reference Radius"
        ' 
        ' Chart1
        ' 
        Chart1.BorderlineColor = Color.Transparent
        ChartArea3.Name = "ChartArea1"
        Chart1.ChartAreas.Add(ChartArea3)
        Chart1.Dock = DockStyle.Fill
        Legend3.Name = "Legend1"
        Chart1.Legends.Add(Legend3)
        Chart1.Location = New Point(2, 2)
        Chart1.Name = "Chart1"
        Series3.ChartArea = "ChartArea1"
        Series3.IsVisibleInLegend = False
        Series3.Legend = "Legend1"
        Series3.Name = "Series1"
        Chart1.Series.Add(Series3)
        Chart1.Size = New Size(396, 196)
        Chart1.TabIndex = 3
        Chart1.TabStop = False
        Chart1.Text = "BladeHeight"
        ' 
        ' ChartBladeHeight
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        Controls.Add(Chart1)
        DefaultSize = New Size(400, 200)
        DisplayName = "BladeHeight"
        Font = New Font("Arial", 9.067472F, FontStyle.Bold)
        IsMovable = True
        IsSelectable = True
        IsSizeable = True
        Name = "ChartBladeHeight"
        Padding = New Padding(2)
        Size = New Size(400, 200)
        ContextMenuStrip1.ResumeLayout(False)
        CType(Chart1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents ReferenceBladeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ReferencePointToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LEToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents MidToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TEToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ReferenceRadiusToolStripMenuItem As ToolStripMenuItem
    Public WithEvents Chart1 As DataVisualization.Charting.Chart

End Class
