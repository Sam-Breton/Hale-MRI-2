Imports System.ComponentModel

Public Class CustomLabel
    Inherits UserControl

    Private WithEvents LabLabel As Label

    Public Sub New()
        MyBase.New()
        InitializeComponent()

        ' Initialize the internal label
        LabLabel = New Label() With {
            .AutoSize = True,
            .Location = New Point(0, 0),
            .Text = "CustomLabel"
        }

        ' Configure the UserControl to auto-size, grow and shrink.
        Me.AutoSize = True
        Me.AutoSizeMode = AutoSizeMode.GrowAndShrink
        Me.Controls.Add(LabLabel)
    End Sub


    ' Expose the BackColor property.
    <Browsable(True)>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Overrides Property BackColor As Color
        Get
            Return MyBase.BackColor
        End Get
        Set(value As Color)
            MyBase.BackColor = value
            If LabLabel IsNot Nothing Then
                LabLabel.BackColor = value
            End If

            Me.PerformLayout()
        End Set
    End Property

    ' Expose the Text property.
    <Browsable(True)>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Overrides Property Text As String
        Get
            Return LabLabel.Text
        End Get
        Set(value As String)
            LabLabel.Text = value
            ' Force the parent UserControl to recalculate its size.
            Me.PerformLayout()
        End Set
    End Property

    ' Expose the Font property.
    <Browsable(True)>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Overrides Property Font As Font
        Get
            Return LabLabel.Font
        End Get
        Set(value As Font)
            LabLabel.Font = value
            Me.PerformLayout()
        End Set
    End Property

    Public Overrides Function GetPreferredSize(proposedSize As Size) As Size
        ' Force the UserControl to exactly match the Label's preferred dimensions.
        Return LabLabel.GetPreferredSize(proposedSize)
    End Function

    Private Sub LabLabel_SizeChanged(sender As Object, e As EventArgs) Handles LabLabel.SizeChanged
        Me.PerformLayout()
    End Sub
End Class
