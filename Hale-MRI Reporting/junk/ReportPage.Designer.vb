<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ReportPage
    Inherits DocumentPage

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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        HeaderMenuStrip = New ContextMenuStrip(components)
        ToolStripSeparator3 = New ToolStripSeparator()
        HeaderBorderStyleMenuItem = New ToolStripMenuItem()
        BorderStyleNoneMenuItem = New ToolStripMenuItem()
        BorderStyleFixedSingleMenuItem = New ToolStripMenuItem()
        BorderStyleFixed3DMenuItem = New ToolStripMenuItem()
        ToolStripSeparator2 = New ToolStripSeparator()
        LetterheadMenuStrip = New ContextMenuStrip(components)
        LetterheadImageMenuItem = New ToolStripMenuItem()
        LetterheadSizeModeMenuItem = New ToolStripMenuItem()
        SizeModeNormalMenuItem = New ToolStripMenuItem()
        SizeModeStretchMenuItem = New ToolStripMenuItem()
        SizeModeAutoSizeMenuItem = New ToolStripMenuItem()
        SizeModeCenterMenuItem = New ToolStripMenuItem()
        SizeModeZoomMenuItem = New ToolStripMenuItem()
        ToolStripSeparator5 = New ToolStripSeparator()
        OpenFileDialog1 = New OpenFileDialog()
        ReportHeaderControl = New ReportHeader()
        ReportLetterheadControl = New ReportLetterhead1()
        HeaderMenuStrip.SuspendLayout()
        LetterheadMenuStrip.SuspendLayout()
        SuspendLayout()
        ' 
        ' HeaderMenuStrip
        ' 
        HeaderMenuStrip.Items.AddRange(New ToolStripItem() {ToolStripSeparator3, HeaderBorderStyleMenuItem})
        HeaderMenuStrip.Name = "HeaderMenuStrip"
        HeaderMenuStrip.Size = New Size(138, 32)
        ' 
        ' ToolStripSeparator3
        ' 
        ToolStripSeparator3.Name = "ToolStripSeparator3"
        ToolStripSeparator3.Size = New Size(134, 6)
        ' 
        ' HeaderBorderStyleMenuItem
        ' 
        HeaderBorderStyleMenuItem.DropDownItems.AddRange(New ToolStripItem() {BorderStyleNoneMenuItem, BorderStyleFixedSingleMenuItem, BorderStyleFixed3DMenuItem})
        HeaderBorderStyleMenuItem.Name = "HeaderBorderStyleMenuItem"
        HeaderBorderStyleMenuItem.Size = New Size(137, 22)
        HeaderBorderStyleMenuItem.Text = "Border Style"
        ' 
        ' BorderStyleNoneMenuItem
        ' 
        BorderStyleNoneMenuItem.CheckOnClick = True
        BorderStyleNoneMenuItem.Name = "BorderStyleNoneMenuItem"
        BorderStyleNoneMenuItem.Size = New Size(136, 22)
        BorderStyleNoneMenuItem.Text = "None"
        ' 
        ' BorderStyleFixedSingleMenuItem
        ' 
        BorderStyleFixedSingleMenuItem.CheckOnClick = True
        BorderStyleFixedSingleMenuItem.Name = "BorderStyleFixedSingleMenuItem"
        BorderStyleFixedSingleMenuItem.Size = New Size(136, 22)
        BorderStyleFixedSingleMenuItem.Text = "Fixed Single"
        ' 
        ' BorderStyleFixed3DMenuItem
        ' 
        BorderStyleFixed3DMenuItem.CheckOnClick = True
        BorderStyleFixed3DMenuItem.Name = "BorderStyleFixed3DMenuItem"
        BorderStyleFixed3DMenuItem.Size = New Size(136, 22)
        BorderStyleFixed3DMenuItem.Text = "Fixed 3D"
        ' 
        ' ToolStripSeparator2
        ' 
        ToolStripSeparator2.Name = "ToolStripSeparator2"
        ToolStripSeparator2.Size = New Size(125, 6)
        ' 
        ' LetterheadMenuStrip
        ' 
        LetterheadMenuStrip.Items.AddRange(New ToolStripItem() {LetterheadImageMenuItem, ToolStripSeparator2, LetterheadSizeModeMenuItem, ToolStripSeparator5})
        LetterheadMenuStrip.Name = "LetterheadMenuStrip"
        LetterheadMenuStrip.Size = New Size(129, 60)
        ' 
        ' LetterheadImageMenuItem
        ' 
        LetterheadImageMenuItem.Name = "LetterheadImageMenuItem"
        LetterheadImageMenuItem.Size = New Size(128, 22)
        LetterheadImageMenuItem.Text = "Image"
        ' 
        ' LetterheadSizeModeMenuItem
        ' 
        LetterheadSizeModeMenuItem.DropDownItems.AddRange(New ToolStripItem() {SizeModeNormalMenuItem, SizeModeStretchMenuItem, SizeModeAutoSizeMenuItem, SizeModeCenterMenuItem, SizeModeZoomMenuItem})
        LetterheadSizeModeMenuItem.Name = "LetterheadSizeModeMenuItem"
        LetterheadSizeModeMenuItem.Size = New Size(128, 22)
        LetterheadSizeModeMenuItem.Text = "Size Mode"
        ' 
        ' SizeModeNormalMenuItem
        ' 
        SizeModeNormalMenuItem.CheckOnClick = True
        SizeModeNormalMenuItem.Name = "SizeModeNormalMenuItem"
        SizeModeNormalMenuItem.Size = New Size(180, 22)
        SizeModeNormalMenuItem.Text = "Normal"
        ' 
        ' SizeModeStretchMenuItem
        ' 
        SizeModeStretchMenuItem.CheckOnClick = True
        SizeModeStretchMenuItem.Name = "SizeModeStretchMenuItem"
        SizeModeStretchMenuItem.Size = New Size(180, 22)
        SizeModeStretchMenuItem.Text = "Stretch"
        ' 
        ' SizeModeAutoSizeMenuItem
        ' 
        SizeModeAutoSizeMenuItem.CheckOnClick = True
        SizeModeAutoSizeMenuItem.Name = "SizeModeAutoSizeMenuItem"
        SizeModeAutoSizeMenuItem.Size = New Size(180, 22)
        SizeModeAutoSizeMenuItem.Text = "AutoSize"
        ' 
        ' SizeModeCenterMenuItem
        ' 
        SizeModeCenterMenuItem.CheckOnClick = True
        SizeModeCenterMenuItem.Name = "SizeModeCenterMenuItem"
        SizeModeCenterMenuItem.Size = New Size(180, 22)
        SizeModeCenterMenuItem.Text = "Center"
        ' 
        ' SizeModeZoomMenuItem
        ' 
        SizeModeZoomMenuItem.CheckOnClick = True
        SizeModeZoomMenuItem.Name = "SizeModeZoomMenuItem"
        SizeModeZoomMenuItem.Size = New Size(180, 22)
        SizeModeZoomMenuItem.Text = "Zoom"
        ' 
        ' ToolStripSeparator5
        ' 
        ToolStripSeparator5.Name = "ToolStripSeparator5"
        ToolStripSeparator5.Size = New Size(125, 6)
        ' 
        ' OpenFileDialog1
        ' 
        OpenFileDialog1.FileName = "OpenFileDialog1"
        ' 
        ' ReportHeaderControl
        ' 
        ReportHeaderControl.BaseLocation = New Point(0, 0)
        ReportHeaderControl.BaseSize = New Size(0, 0)
        ReportHeaderControl.BorderStyle = BorderStyle.FixedSingle
        ReportHeaderControl.ContextMenuStrip = HeaderMenuStrip
        ReportHeaderControl.Location = New Point(0, 61)
        ReportHeaderControl.Margin = New Padding(0, 0, 0, 20)
        ReportHeaderControl.Name = "ReportHeaderControl"
        ReportHeaderControl.OriginalSize = New Size(0, 0)
        ReportHeaderControl.Size = New Size(711, 145)
        ReportHeaderControl.TabIndex = 3
        ReportHeaderControl.TabStop = False
        ReportHeaderControl.VerticalSeparation = 20
        ReportHeaderControl.VisibleItems = ""
        ' 
        ' ReportLetterheadControl
        ' 
        ReportLetterheadControl.BaseLocation = New Point(0, 0)
        ReportLetterheadControl.BaseSize = New Size(0, 0)
        ReportLetterheadControl.ContextMenuStrip = LetterheadMenuStrip
        ReportLetterheadControl.Location = New Point(0, 0)
        ReportLetterheadControl.Margin = New Padding(0, 0, 0, 20)
        ReportLetterheadControl.Name = "ReportLetterheadControl"
        ReportLetterheadControl.OriginalSize = New Size(0, 0)
        ReportLetterheadControl.Size = New Size(711, 50)
        ReportLetterheadControl.TabIndex = 0
        ReportLetterheadControl.TabStop = False
        ReportLetterheadControl.VerticalSeparation = 20
        ' 
        ' ReportPage
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Remove(ReportLetterheadControl)
        Controls.Add(ReportLetterheadControl)
        Controls.Add(ReportHeaderControl)
        Name = "ReportPage"
        Size = New Size(815, 335)
        HeaderMenuStrip.ResumeLayout(False)
        LetterheadMenuStrip.ResumeLayout(False)
        ResumeLayout(False)
    End Sub
    Friend WithEvents LetterheadMenuStrip As ContextMenuStrip
    Friend WithEvents LetterheadImageMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents LetterheadSizeModeMenuItem As ToolStripMenuItem
    Friend WithEvents HeaderMenuStrip As ContextMenuStrip
    Friend WithEvents SizeModeNormalMenuItem As ToolStripMenuItem
    Friend WithEvents SizeModeStretchMenuItem As ToolStripMenuItem
    Friend WithEvents SizeModeAutoSizeMenuItem As ToolStripMenuItem
    Friend WithEvents SizeModeCenterMenuItem As ToolStripMenuItem
    Friend WithEvents SizeModeZoomMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents HeaderBorderStyleMenuItem As ToolStripMenuItem
    Friend WithEvents BorderStyleNoneMenuItem As ToolStripMenuItem
    Friend WithEvents BorderStyleFixedSingleMenuItem As ToolStripMenuItem
    Friend WithEvents BorderStyleFixed3DMenuItem As ToolStripMenuItem
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents ToolStripSeparator5 As ToolStripSeparator
    Friend WithEvents ReportHeaderControl As ReportHeader
    Friend WithEvents ReportLetterheadControl As ReportLetterhead1
End Class
