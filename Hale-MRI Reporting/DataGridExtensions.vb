Imports System.Runtime.CompilerServices

Public Module DataGridExtensions
    <Extension()>
    Public Sub IsEnabled(dataGrid As DataGridView, value As Boolean)
        dataGrid.Enabled = value
        If dataGrid.Enabled Then
            dataGrid.DefaultCellStyle.BackColor = SystemColors.Window
            dataGrid.DefaultCellStyle.ForeColor = SystemColors.ControlText
            dataGrid.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.Window
            dataGrid.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText
            dataGrid.EnableHeadersVisualStyles = True
        Else
            dataGrid.DefaultCellStyle.BackColor = SystemColors.Control
            dataGrid.DefaultCellStyle.ForeColor = SystemColors.GrayText
            dataGrid.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.Control
            dataGrid.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.GrayText
            dataGrid.CurrentCell = Nothing
            dataGrid.EnableHeadersVisualStyles = False
        End If
        dataGrid.Refresh()
    End Sub
End Module
