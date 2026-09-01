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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        ReportLetterhead1 = New ReportLetterhead()
        ReportHeader1 = New ReportHeader()
        CType(ReportLetterhead1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' ReportLetterhead1
        ' 
        ReportLetterhead1.BaseFont = New Font("Segoe UI", 9.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        ReportLetterhead1.BaseLocation = New Point(0, 0)
        ReportLetterhead1.BaseSize = New Size(0, 0)
        ReportLetterhead1.BorderStyle = BorderStyle.FixedSingle
        ReportLetterhead1.Image = Nothing
        ReportLetterhead1.Location = New Point(89, 3)
        ReportLetterhead1.Name = "ReportLetterhead1"
        ReportLetterhead1.Size = New Size(582, 77)
        ReportLetterhead1.SizeMode = PictureBoxSizeMode.CenterImage
        ReportLetterhead1.TabIndex = 0
        ReportLetterhead1.TabStop = False
        ReportLetterhead1.VerticalSeparation = 20
        ReportLetterhead1.Visible = False
        ' 
        ' ReportHeader1
        ' 
        ReportHeader1.BaseFont = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        ReportHeader1.BaseLocation = New Point(0, 0)
        ReportHeader1.BaseSize = New Size(0, 0)
        ReportHeader1.BorderStyle = BorderStyle.FixedSingle
        ReportHeader1.JobDetails = Nothing
        ReportHeader1.Location = New Point(43, 121)
        ReportHeader1.Margin = New Padding(0)
        ReportHeader1.Name = "ReportHeader1"
        ReportHeader1.Size = New Size(679, 195)
        ReportHeader1.TabIndex = 1
        ReportHeader1.VerticalSeparation = 20
        ReportHeader1.Visible = False
        ReportHeader1.VisibleItems = ""
        ' 
        ' ReportPage
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(ReportHeader1)
        Controls.Add(ReportLetterhead1)
        Name = "ReportPage"
        Size = New Size(850, 450)
        CType(ReportLetterhead1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub
    Friend WithEvents ReportLetterhead1 As ReportLetterhead
    Friend WithEvents ReportHeader1 As ReportHeader

End Class
