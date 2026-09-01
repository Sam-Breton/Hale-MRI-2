<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ReportLetterhead1
    Inherits System.Windows.Forms.UserControl

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
        CustomPanel1 = New CustomPanel()
        PictureLetterhead = New PictureBox()
        CustomPanel1.SuspendLayout()
        CType(PictureLetterhead, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' CustomPanel1
        ' 
        CustomPanel1.BorderColor = Color.Empty
        CustomPanel1.BorderWidth = 1
        CustomPanel1.Controls.Add(PictureLetterhead)
        CustomPanel1.DashPattern = New Single() {0F, 0F}
        CustomPanel1.DashStyle = Drawing2D.DashStyle.DashDot
        CustomPanel1.Dock = DockStyle.Fill
        CustomPanel1.Location = New Point(0, 0)
        CustomPanel1.Name = "CustomPanel1"
        CustomPanel1.Size = New Size(693, 150)
        CustomPanel1.TabIndex = 0
        ' 
        ' PictureLetterhead
        ' 
        PictureLetterhead.Dock = DockStyle.Fill
        PictureLetterhead.ImageLocation = "C:\Users\super\source\repos\Hale-MRI\Hale-MRI\Resources\Borgnine1.png"
        PictureLetterhead.Location = New Point(0, 0)
        PictureLetterhead.Name = "PictureLetterhead"
        PictureLetterhead.Size = New Size(693, 150)
        PictureLetterhead.SizeMode = PictureBoxSizeMode.StretchImage
        PictureLetterhead.TabIndex = 0
        PictureLetterhead.TabStop = False
        ' 
        ' ReportLetterhead1
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(CustomPanel1)
        Name = "ReportLetterhead1"
        Size = New Size(693, 150)
        CustomPanel1.ResumeLayout(False)
        CType(PictureLetterhead, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents CustomPanel1 As CustomPanel
    Friend WithEvents PictureLetterhead As PictureBox

End Class
