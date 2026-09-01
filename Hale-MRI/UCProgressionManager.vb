Imports LibDatabase
Imports LibDatabase.Contexts
Imports LibDatabase.Models
Imports Microsoft.Extensions.DependencyInjection

Public Class UCProgressionManager
    Inherits UserControl
#Region "Members"
    Private mSections As Integer ' how many rows to make in Prog Table
    Private mCompCurr As JobDetail ' FrmComparison's Current JobDetail passed to during creation
    Private mPickerCurr As JobDetail ' JobDetail chosen from FrmMeasurementPicker when loading from file
    Private mProgRads As List(Of RadiusMeasurement) = Nothing ' list of RadiusMeasurements to be used as Progrms in Charts
    Private mProgNewPitch As Double
    Private mProgOldPitch As Double
    ' Since this class already Inherits UserControl, I added the required members and property from FrmDatabaseForm
    ' so it could open FrmMeasurementPicker using ShowForm() and handle
    Private ReadOnly mDatabase As HaleMRIContext = Nothing
    Private mFormLifetimeScope As IServiceScope = Nothing
    Private ReadOnly mServiceProvider As IServiceProvider = Nothing
    Private ReadOnly mScopeFactory As IServiceScopeFactory = Nothing
#End Region
#Region "Public Interface"
    ' Visual Studio Designer uses this.
    Public Sub New()
        MyBase.New()
        InitializeComponent()
    End Sub

    ' DI Container uses this at runtime.
    Public Sub New(context As HaleMRIContext, serviceProvider As IServiceProvider, scopeFactory As IServiceScopeFactory)
        MyBase.New()
        InitializeComponent()
        mDatabase = context
        mServiceProvider = serviceProvider
        mScopeFactory = scopeFactory
    End Sub

    Public Property CompCurrent As JobDetail
        Get
            Return mCompCurr
        End Get
        Set(value As JobDetail)
            mCompCurr = value
        End Set
    End Property

    ''' <summary>
    ''' The current database context.
    ''' </summary>
    ''' <returns>HaleMRIContext</returns>
    Protected ReadOnly Property Database As HaleMRIContext
        Get
            Return mDatabase
        End Get
    End Property

    ''' <summary>
    ''' Form lifetime scope.
    ''' </summary>
    ''' <returns>IServiceScope</returns>
    Public Property FormLifetimeScope As IServiceScope

    ''' <summary>
    ''' Exposes the factory to all child forms needing it.
    ''' </summary>
    ''' <returns>IServiceScopeFactory</returns>
    Protected ReadOnly Property ScopeFactory As IServiceScopeFactory
        Get
            Return mScopeFactory
        End Get
    End Property

    ''' <summary>
    ''' The current ServiceProvider.
    ''' </summary>
    ''' <returns>IServiceProvider</returns>
    Protected ReadOnly Property ServiceProvider As IServiceProvider
        Get
            Return mServiceProvider
        End Get
    End Property

    ''' <summary>
    ''' The currently logged-in application user.
    ''' </summary>
    ''' <returns>Employee</returns>
    Public Property User As Employee
    Public Property Sections As Integer
        Get
            Return mSections
        End Get
        Set(value As Integer)
            mSections = value
            If mSections > 10 Or mSections < 1 Then
                mSections = 10
            End If
        End Set
    End Property
    Public Property PickerCurrent As JobDetail
        Get
            Return mPickerCurr
        End Get
        Set(value As JobDetail)
            mPickerCurr = value
        End Set
    End Property
    Public Property BladeProgs As List(Of RadiusMeasurement)
        Get
            Return mProgRads
        End Get
        Set(value As List(Of RadiusMeasurement))
            mProgRads = value
        End Set
    End Property
    Public Property OldPitch As Double
        Get
            Return mProgOldPitch
        End Get
        Set(value As Double)
            mProgOldPitch = value
        End Set
    End Property
    Public Property NewPitch As Double
        Get
            Return mProgNewPitch
        End Get
        Set(value As Double)
            mProgNewPitch = value
        End Set
    End Property
#End Region
#Region "Private Interface"
    Private Sub SaveFile()
        'currently does nothing because I need to decide if it should even stay or be implemented as saving a Design in the Database
    End Sub
    Private Sub ManualFill() 'need to re visit this to make a Rad list or height list
        Dim Rads As Integer
        Dim Sect As Integer
        Dim Radsucc As Boolean
        Dim Sectsucc As Boolean
        Dim str = Interaction.InputBox("Input number of Radii.", "Fill Progression Table")
        Radsucc = Integer.TryParse(str, Rads)
        If Radsucc Then
            If Rads <= 0 Then
                MessageBox.Show("Invalid Rads: Can't be less than or equal to 0")
                Return
            End If
            str = Interaction.InputBox("Input number of Sections(1-10).", "Fill Progression Table")
            Sectsucc = Integer.TryParse(str, Sect)
            If Sectsucc Then
                If Sect > 10 Or Sect <= 0 Then
                    MessageBox.Show("Invalid Sections: Can't be less than 1 or greater than 10")
                    Return
                End If
                Dim dt As New DataTable
                Dim nRow As DataRow
                Dim col As DataColumn
                Dim x As Integer
                Dim y As Integer
                For x = 0 To Rads
                    If x = 0 Then
                        col = dt.Columns.Add("Segments", GetType(String))
                        dt.PrimaryKey = New DataColumn() {col}
                    Else
                        dt.Columns.Add("Rad" + x.ToString(), GetType(Double))
                    End If
                    For y = 0 To Sect + 1
                        If x = 0 Then
                            If y = 0 Then
                                nRow = If(dt.Rows.Find("WP"), dt.Rows.Add("WP"))
                                nRow.Item("Segments") = "WP"
                            ElseIf y = 1 Then
                                nRow = If(dt.Rows.Find("LE Seg"), dt.Rows.Add("LE Seg"))
                                nRow.Item("Segments") = "LE Seg"
                            ElseIf y = Sect And y <> 1 Then
                                nRow = If(dt.Rows.Find("TE Seg"), dt.Rows.Add("TE Seg"))
                                nRow.Item("Segments") = "TE Seg"
                            ElseIf y = Sect + 1 Then
                                nRow = If(dt.Rows.Find("Avg"), dt.Rows.Add("Avg"))
                                nRow.Item("Segments") = "Avg"
                            Else
                                nRow = If(dt.Rows.Find("Seg " + y.ToString()), dt.Rows.Add("Seg " + y.ToString()))
                                nRow.Item("Segments") = "Seg " + y.ToString()
                            End If
                        Else
                            If y = 0 Then
                                nRow = If(dt.Rows.Find("WP"), dt.Rows.Add("WP"))
                                nRow.Item("Rad" + x.ToString()) = 0
                            ElseIf y = 1 Then
                                nRow = If(dt.Rows.Find("LE Seg"), dt.Rows.Add("LE Seg"))
                                nRow.Item("Rad" + x.ToString()) = 0
                            ElseIf y = Sect And y <> 1 Then
                                nRow = If(dt.Rows.Find("TE Seg"), dt.Rows.Add("TE Seg"))
                                nRow.Item("Rad" + x.ToString()) = 0
                            ElseIf y = Sect + 1 Then
                                nRow = If(dt.Rows.Find("Avg"), dt.Rows.Add("Avg"))
                                nRow.Item("Rad" + x.ToString()) = 0
                            Else
                                nRow = If(dt.Rows.Find("Seg " + y.ToString()), dt.Rows.Add("Seg " + y.ToString()))
                                nRow.Item("Rad" + x.ToString()) = 0
                            End If
                        End If
                    Next
                Next
                DGridProgTable.DataSource = dt
            Else
                MessageBox.Show("Invalid Sections: Not a Number")
                Return
            End If
        Else
            MessageBox.Show("Invalid Rads: Not a number")
            Return
        End If
    End Sub
    Private Sub CurrentLoad()
        'Input Ref Blade
        Dim blad As Integer
        Dim bladsucc As Boolean
        Dim str = Interaction.InputBox("Input Reference Blade Number.", "Fill Progression Table")
        bladsucc = Integer.TryParse(str, blad)
        If bladsucc Then
            If blad <= 0 Or blad > CompCurrent.Job.PropellerBlades Then
                MessageBox.Show("Invalid Blade Number: Can't be less than 1 or greater than the Propeller's blade count")
                Return
            End If
            Dim dt As New DataTable
            Dim nRow As DataRow
            Dim col As DataColumn
            Dim x As Integer
            For x = 0 To Sections + 1
                If x = 0 Then
                    col = dt.Columns.Add("Segments", GetType(String))
                    dt.PrimaryKey = New DataColumn() {col}
                    nRow = If(dt.Rows.Find("WP"), dt.Rows.Add("WP"))
                    nRow.Item("Segments") = "WP"
                ElseIf x = 1 Then
                    nRow = If(dt.Rows.Find("LE Seg"), dt.Rows.Add("LE Seg"))
                    nRow.Item("Segments") = "LE Seg"
                ElseIf x = Sections Then
                    nRow = If(dt.Rows.Find("TE Seg"), dt.Rows.Add("TE Seg"))
                    nRow.Item("Segments") = "TE Seg"
                ElseIf x = Sections + 1 Then
                    nRow = If(dt.Rows.Find("Avg"), dt.Rows.Add("Avg"))
                    nRow.Item("Segments") = "Avg"
                Else
                    nRow = If(dt.Rows.Find("Seg " + x.ToString()), dt.Rows.Add("Seg " + x.ToString()))
                    nRow.Item("Segments") = "Seg " + x.ToString()
                End If
            Next
            Dim totavg As Double = 0
            ' make the rows
            For Each rm As RadiusMeasurement In CompCurrent.RadiusMeasurements.Where(Function(r) r.BladeId = blad).OrderBy(Function(r) r.Radius).ToList()
                BladeProgs.Add(rm)
                col = If(dt.Columns("Rad" + Math.Round(rm.Radius.Value).ToString()), dt.Columns.Add("Rad" + Math.Round(rm.Radius.Value).ToString()))
                Dim radavg As Double = 0
                For x = 0 To Sections + 1
                    If x = 0 Then
                        nRow = If(dt.Rows.Find("WP"), dt.Rows.Add("WP"))
                        nRow.Item(col) = Math.Round(rm.Radius.Value, 2)
                    ElseIf x = 1 Then
                        nRow = If(dt.Rows.Find("LE Seg"), dt.Rows.Add("LE Seg"))
                        Dim pitch As Double = GetLocalPitch(rm.CellMeasurements, Sections, x, rm.JobDetails.Job.PropellerDiameter, rm.Radius, rm.JobDetails.Job.TeExclusion, rm.JobDetails.Job.LeExclusion)
                        nRow.Item(col) = Math.Round(pitch, 3)
                        radavg += pitch
                    ElseIf x = Sections Then
                        nRow = If(dt.Rows.Find("TE Seg"), dt.Rows.Add("TE Seg"))
                        Dim pitch As Double = GetLocalPitch(rm.CellMeasurements, Sections, x, rm.JobDetails.Job.PropellerDiameter, rm.Radius, rm.JobDetails.Job.TeExclusion, rm.JobDetails.Job.LeExclusion)
                        nRow.Item(col) = Math.Round(pitch, 3)
                        radavg += pitch
                    ElseIf x = Sections + 1 Then
                        nRow = If(dt.Rows.Find("Avg"), dt.Rows.Add("Avg"))
                        nRow.Item(col) = Math.Round(radavg / Sections, 2)
                        totavg += radavg
                    Else
                        nRow = If(dt.Rows.Find("Seg " + x.ToString()), dt.Rows.Add("Seg " + x.ToString()))
                        Dim pitch As Double = GetLocalPitch(rm.CellMeasurements, Sections, x, rm.JobDetails.Job.PropellerDiameter, rm.Radius, rm.JobDetails.Job.TeExclusion, rm.JobDetails.Job.LeExclusion)
                        nRow.Item(col) = Math.Round(pitch, 3)
                        radavg += pitch
                    End If
                Next
            Next
            nRow = If(dt.Rows.Find("WP"), dt.Rows.Add("WP"))
            totavg /= CompCurrent.RadiusMeasurements.Where(Function(r) r.BladeId = blad).ToList().Count
            nRow.Item("Segments") = Math.Round(totavg, 3).ToString("F3")
        Else
            MessageBox.Show("Invalid Blade Number: Not a Number")
            Return
        End If
        'Populate Grid with selected blades Radii and pitch by section
    End Sub
    Private Sub PickerLoad()
        'Open FrmMeasurementPicker and select JobDetail
        Dim frm As FrmMeasurementPicker = DirectCast(ShowForm(Of FrmMeasurementPicker)(Me.ScopeFactory, Me.User), FrmMeasurementPicker)

        If frm.ShowDialog() = DialogResult.OK Then
            PickerCurrent = mDatabase.JobDetails.Local.FirstOrDefault(Function(j) j.Id = frm.Current.Id)
        Else
            Return
        End If
        Dim blad As Integer
        Dim bladsucc As Boolean
        Dim str = Interaction.InputBox("Input Reference Blade Number.", "Fill Progression Table")
        bladsucc = Integer.TryParse(str, blad)
        If bladsucc Then
            If blad <= 0 Or blad > PickerCurrent.Job.PropellerBlades Then
                MessageBox.Show("Invalid Blade Number: Can't be less than 1 or greater than the Propeller's blade count")
                Return
            End If
            Dim dt As New DataTable
            Dim nRow As DataRow
            Dim col As DataColumn
            Dim x As Integer
            For x = 0 To Sections + 1
                If x = 0 Then
                    col = dt.Columns.Add("Segments", GetType(String))
                    dt.PrimaryKey = New DataColumn() {col}
                    nRow = If(dt.Rows.Find("WP"), dt.Rows.Add("WP"))
                    nRow.Item("Segments") = "WP"
                ElseIf x = 1 Then
                    nRow = If(dt.Rows.Find("LESeg"), dt.Rows.Add("LESeg"))
                    nRow.Item("Segments") = "LESeg"
                ElseIf x = Sections Then
                    nRow = If(dt.Rows.Find("TESeg"), dt.Rows.Add("TESeg"))
                    nRow.Item("Segments") = "TESeg"
                ElseIf x = Sections + 1 Then
                    nRow = If(dt.Rows.Find("Avg"), dt.Rows.Add("Avg"))
                    nRow.Item("Segments") = "Avg"
                Else
                    nRow = If(dt.Rows.Find("Seg " + x.ToString()), dt.Rows.Add("Rad " + x.ToString()))
                    nRow.Item("Segments") = "Seg " + x.ToString()
                End If
            Next
            col = dt.Columns.Add("Segments", GetType(String))
            dt.PrimaryKey = New DataColumn() {col}
            Dim totavg As Double = 0
            ' make the rows
            For Each rm As RadiusMeasurement In PickerCurrent.RadiusMeasurements.Where(Function(r) r.BladeId = blad).OrderBy(Function(r) r.Radius).ToList()
                BladeProgs.Add(rm)
                col = If(dt.Columns("Rad" + Math.Round(rm.Radius.Value).ToString()), dt.Columns.Add("Rad" + Math.Round(rm.Radius.Value).ToString()))
                Dim radavg As Double = 0
                For x = 0 To Sections + 1
                    If x = 0 Then
                        nRow = If(dt.Rows.Find("WP"), dt.Rows.Add("WP"))
                        nRow.Item(col) = Math.Round(rm.Radius.Value, 2)
                    ElseIf x = 1 Then
                        nRow = If(dt.Rows.Find("LESeg"), dt.Rows.Add("LESeg"))
                        Dim pitch As Double = GetLocalPitch(rm.CellMeasurements, Sections, x, rm.JobDetails.Job.PropellerDiameter, rm.Radius, rm.JobDetails.Job.TeExclusion, rm.JobDetails.Job.LeExclusion)
                        nRow.Item(col) = Math.Round(pitch, 3)
                        radavg += pitch
                    ElseIf x = Sections Then
                        nRow = If(dt.Rows.Find("TESeg"), dt.Rows.Add("TESeg"))
                        Dim pitch As Double = GetLocalPitch(rm.CellMeasurements, Sections, x, rm.JobDetails.Job.PropellerDiameter, rm.Radius, rm.JobDetails.Job.TeExclusion, rm.JobDetails.Job.LeExclusion)
                        nRow.Item(col) = Math.Round(pitch, 3)
                        radavg += pitch
                    ElseIf x = Sections + 1 Then
                        nRow = If(dt.Rows.Find("Avg"), dt.Rows.Add("Avg"))
                        nRow.Item(col) = Math.Round(radavg, 2)
                        totavg += radavg
                    Else
                        nRow = If(dt.Rows.Find("Seg " + x.ToString()), dt.Rows.Add("Seg " + x.ToString()))
                        Dim pitch As Double = GetLocalPitch(rm.CellMeasurements, Sections, x, rm.JobDetails.Job.PropellerDiameter, rm.Radius, rm.JobDetails.Job.TeExclusion, rm.JobDetails.Job.LeExclusion)
                        nRow.Item(col) = Math.Round(pitch, 3)
                        radavg += pitch
                    End If
                Next
            Next
            nRow = If(dt.Rows.Find("WP"), dt.Rows.Add("WP"))
            totavg /= PickerCurrent.RadiusMeasurements.Where(Function(r) r.BladeId = blad).ToList().Count
            nRow.Item("Segments") = Math.Round(totavg, 3).ToString("F3")
        Else
            MessageBox.Show("Invalid Blade Number: Not a Number")
            Return
        End If
    End Sub
    Private Sub ScaleWheelPitch()
        Dim dt As DataTable = DGridProgTable.DataSource
        Dim newp As Double
        Dim bladsucc As Boolean
        Dim str = Interaction.InputBox("Input New Pitch.", "Scale Pitch")
        bladsucc = Double.TryParse(str, newp)
        If bladsucc Then
            If newp <= 0 Then
                MessageBox.Show("Invalid Pitch: Can't be less than 1")
                Return
            End If
            Dim oldp As Double = Double.Parse(dt.Rows(0).Item("Segments"))
            For Each Col As DataColumn In dt.Columns
                If dt.Columns.IndexOf(Col) = 0 Then
                    Continue For
                End If
                For Each row As DataRow In dt.Rows
                    If dt.Rows.IndexOf(row) = 0 Or dt.Rows.IndexOf(row) = dt.Rows.Count - 1 Then
                        Continue For
                    End If
                    Dim txtpit As Double = row.Item(Col)
                    Dim newpit As Double = txtpit * newp / oldp
                    row.Item(Col) = Math.Round(newpit, 3)
                Next
            Next
            CalcAverages()
        Else
            MessageBox.Show("Invalid Blade Number: Not a Number")
            Return
        End If
    End Sub
    Private Sub CalcAverages()
        Dim dt As DataTable = CType(DGridProgTable.DataSource, DataTable)
        Dim radavg As Double
        Dim wheelavg As Double
        For Each Col As DataColumn In dt.Columns
            If dt.Columns.IndexOf(Col) = 0 Then
                Continue For
            Else
                For Each row As DataRow In dt.Rows
                    If dt.Rows.IndexOf(row) = 0 Then
                        Continue For
                    ElseIf dt.Rows.IndexOf(row) = dt.Rows.Count - 1 Then
                        wheelavg += radavg / Sections
                        row.Item(Col) = Math.Round(radavg / Sections, 2)
                    Else
                        Dim pitch As Double = row.Item(Col)
                        radavg += pitch
                    End If
                Next
            End If
        Next
        NewPitch = Double.Parse(dt.Rows(0).Item("Segments"))
        dt.Rows(0).Item("Segments") = Math.Round(NewPitch, 3)
    End Sub
#End Region
#Region "Event Handlers"
    Private Sub CmdFillManual_Click(sender As Object, e As EventArgs) Handles CmdFillManual.Click
        ManualFill()
    End Sub

    Private Sub CmdLoadCurrent_Click(sender As Object, e As EventArgs) Handles CmdLoadCurrent.Click
        CurrentLoad()
    End Sub

    Private Sub CmdLoadFile_Click(sender As Object, e As EventArgs) Handles CmdLoadFile.Click
        PickerLoad()
    End Sub

    Private Sub CmdSavetoFile_Click(sender As Object, e As EventArgs) Handles CmdSavetoFile.Click

    End Sub

    Private Sub CmdClearDesc_Click(sender As Object, e As EventArgs) Handles CmdClearDesc.Click
        TxtDesc.Text = ""
    End Sub

    Private Sub CmdClearTable_Click(sender As Object, e As EventArgs) Handles CmdClearTable.Click
        DGridProgTable.DataSource = Nothing
    End Sub

    Private Sub CmdScalePitch_Click(sender As Object, e As EventArgs) Handles CmdScalePitch.Click
        OldPitch = Double.Parse(CType(DGridProgTable.DataSource, DataTable).Rows(0).Item("Segments"))
        ScaleWheelPitch()
    End Sub

    Private Sub CmdCalcAvg_Click(sender As Object, e As EventArgs) Handles CmdCalcAvg.Click
        CalcAverages()
    End Sub

    Private Sub UserControl_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        Try
            ' Clean up the database connection context and private DI scope.
            If Me.Database IsNot Nothing Then Me.Database.Dispose()
            If Me.FormLifetimeScope IsNot Nothing Then Me.FormLifetimeScope.Dispose()
        Catch ex As Exception

        End Try
    End Sub
#End Region
End Class
