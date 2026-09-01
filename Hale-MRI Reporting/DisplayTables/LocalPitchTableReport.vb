Imports System.ComponentModel
Imports LibDatabase.Models
Imports Microsoft.EntityFrameworkCore.Metadata.Internal
Imports Windows.Devices.Display.Core

Public Class LocalPitchTableReport
    Inherits DisplayControl
#Region "Constructors"
    ''' <summary>
    ''' Default constructor.
    ''' </summary>
    Public Sub New()
        MyBase.New()
        InitializeComponent()
    End Sub
#End Region
#Region "Client Properties"
    Public Property AllowProgressivePitch As Boolean = False
    ''' <summary>
    ''' Minimums Apply
    ''' </summary>
    ''' <returns>Boolean</returns>
    Public Property MinimumsApply As Boolean = False
    Public Overrides Property Data As Object
        Get
            Return MyBase.Data
        End Get
        Set(value As Object)
            MyBase.Data = value
            mDisplayInitialized = False
            DisplayInitialize()
            DataShow()
        End Set
    End Property
#End Region
#Region "Computed Properties"
    Public ReadOnly Property TEE As Double
        Get
            Return JobDetails.Job.TeExclusion
        End Get
    End Property
    Public ReadOnly Property LEE As Double
        Get
            Return JobDetails.Job.LeExclusion
        End Get
    End Property
    Public ReadOnly Property Diameter As Double
        Get
            Return JobDetails.Job.PropellerDiameter
        End Get
    End Property
    Public ReadOnly Property Blades As Integer
        Get
            Return JobDetails.Job.PropellerBlades
        End Get
    End Property
    Public ReadOnly Property Sectors As Integer
        Get
            Return TolClass.LocalPitchSectors
        End Get
    End Property
#End Region
    ''' <summary>
    ''' Provides chart metadata.
    ''' </summary>
    ''' <returns>JobDetail</returns>
    <Browsable(False)>
    Private ReadOnly Property JobDetails As JobDetail
        Get
            Return DirectCast(Me.Data, JobDetail)
        End Get
    End Property

    ''' <summary>
    ''' Blade radius measurements.
    ''' </summary>
    ''' <returns></returns>
    <Browsable(False)>
    Private ReadOnly Property RadiusMeasurements As List(Of RadiusMeasurement)
        Get
            Return Me.JobDetails?.RadiusMeasurements
        End Get
    End Property
#Region "Members"
    Public InnerTables As New List(Of TableLayoutPanel)()
#End Region
#Region "Private Interface"
    Protected Overrides Sub DataShow()
        If JobDetails Is Nothing Or TolClass Is Nothing Then Exit Sub
        For Each tlp As TableLayoutPanel In InnerTables
            With tlp
                Dim blade As Integer = InnerTables.IndexOf(tlp) + 1
                Dim meas As List(Of RadiusMeasurement) = Me.RadiusMeasurements.Where(Function(r) r.BladeId = blade).OrderBy(Function(r) r.Radius).ToList()
                For Each rm As RadiusMeasurement In meas
                    For x As Integer = 1 To Sectors '' for each sector of each Radius measurement create a label holding that sectors
                        Dim q As Integer = Sectors - x 'inverse of x to correctly place TE to LE
                        Dim pit As Double = GetLocalPitch(rm.CellMeasurements, Sectors, x, Diameter, Math.Round(rm.Radius.Value), TEE, LEE)
                        Dim lab As New Label With {
                            .Text = pit.ToString("F2"),
                            .Dock = DockStyle.Fill,
                            .TextAlign = ContentAlignment.MiddleCenter}
                        .Controls.Add(lab, q, meas.IndexOf(rm))
                    Next
                Next
            End With
        Next
    End Sub

    Protected Overrides Sub DisplayInitialize()
        If mDisplayInitialized = True Then Exit Sub
        If JobDetails Is Nothing Or TolClass Is Nothing Then Exit Sub
        Dim meas As List(Of RadiusMeasurement) = Me.RadiusMeasurements.Where(Function(r) r.BladeId = 1).OrderBy(Function(r) r.Radius).ToList()
        Dim Radpercent As Double = 100 / meas.Count
        With tLayoutLPBase
            .ColumnCount = Blades + 1 '' need a column for each blade
            .RowCount = meas.Count + 1 '' need a row for each scanned Radius
            .ColumnStyles.Clear()
            .RowStyles.Clear()
            Dim bladepercent As Double = 100 / Blades
            For I As Integer = 0 To Blades
                If I = 0 Then
                    .ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 60))
                    Dim tb As New Label With {'' add a label in the first row and column
                        .Text = "r/R",
                        .Dock = DockStyle.Fill,
                        .TextAlign = ContentAlignment.MiddleCenter}
                    .Controls.Add(tb, I, 0)
                Else
                    .ColumnStyles.Add(New ColumnStyle(SizeType.Percent, bladepercent))
                    Dim tb As New Label With {'' set columnstyle and add label for each blade of the prop
                        .Text = "Blade " & I,
                        .Dock = DockStyle.Fill,
                        .TextAlign = ContentAlignment.MiddleCenter}
                    .Controls.Add(tb, I, 0)
                End If
            Next
            .RowStyles.Add(New RowStyle(SizeType.Percent, Radpercent)) '' set the first rowstyle
            Dim x As Integer = 1
            For Each rm As RadiusMeasurement In meas
                .RowStyles.Add(New RowStyle(SizeType.Percent, Radpercent)) '' for each scanned radii add a label and rowstyle
                Dim tb As New Label With {
                        .Text = Math.Round(rm.Radius.Value, 1).ToString("0.0"),
                        .Dock = DockStyle.Top,
                        .TextAlign = ContentAlignment.MiddleCenter}
                .Controls.Add(tb, 0, x)
                x += 1
            Next
            For I As Integer = 1 To Blades '' for each blade add a new table layout, these will be set to hold labels for each radii sector
                InnerTables.Add(New TableLayoutPanel With {
                                .Dock = DockStyle.Fill,
                                .ColumnCount = Sectors,
                                .RowCount = meas.Count,
                                .Name = "tLayoutLPBlade" & I,
                                .Margin = New Padding(0, 0, 0, 0),
                                .CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
                                })
                .Controls.Add(InnerTables.Last(), I, 1)
                InnerTables.Last().ColumnStyles.Clear()
                InnerTables.Last().RowStyles.Clear()
                Dim sectorpercent As Double = 100 / Sectors
                For J As Integer = 0 To Sectors - 1 '' ensure that the inner table layout's have correct column and row styles
                    InnerTables.Last().ColumnStyles.Add(New ColumnStyle(SizeType.Percent, sectorpercent))
                Next
                For J As Integer = 0 To meas.Count - 1
                    InnerTables.Last().RowStyles.Add(New RowStyle(SizeType.Percent, Radpercent))
                Next
                .SetRowSpan(InnerTables.Last(), .RowCount - 1)
            Next
        End With
        MyBase.DisplayInitialize()
    End Sub
#End Region
End Class
