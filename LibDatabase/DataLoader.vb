Imports System.ComponentModel
Imports LibDatabase.Models
Imports Microsoft.EntityFrameworkCore
Namespace Contexts
    Public Module DataLoader
        Public Sub LoadCustomers(dB As HaleMRIContext)
            dB.Customers.
                Include(Function(c) c.CountryCodeNavigation).
                Include(Function(c) c.StateNavigation).
                Include(Function(c) c.Vessels).
                    ThenInclude(Function(v) v.ServiceType).
                Include(Function(c) c.Vessels).
                    ThenInclude(Function(v) v.Jobs).
                    ThenInclude(Function(j) j.PropellerManufacturer).
                Include(Function(c) c.Vessels).
                    ThenInclude(Function(v) v.Jobs).
                    ThenInclude(Function(j) j.InspectedByNavigation).
                AsSplitQuery().
                Load()
            dB.Employees.Load()
            dB.CountryCodes.Load()
            dB.StateCodes.Load()
        End Sub

        Public Sub LoadVessels(dB As HaleMRIContext)
            dB.Vessels.
                Include(Function(v) v.Customer).
                Include(Function(v) v.ServiceType).
                Include(Function(v) v.Jobs).
                    ThenInclude(Function(j) j.InspectedByNavigation).
                Include(Function(v) v.Jobs).
                    ThenInclude(Function(j) j.JobDetails).
                        ThenInclude(Function(jd) jd.MeasurementType).
                Include(Function(v) v.Jobs).
                    ThenInclude(Function(j) j.JobDetails).
                        ThenInclude(Function(jd) jd.PerformedByNavigation).
                Include(Function(v) v.Jobs).
                    ThenInclude(Function(j) j.JobDetails).
                        ThenInclude(Function(jd) jd.ReferenceCell).
                Include(Function(v) v.Jobs).
                    ThenInclude(Function(j) j.JobDetails).
                        ThenInclude(Function(jd) jd.ToleranceClassNavigation).
                AsSplitQuery().
                Load()
            dB.Employees.Load()
            dB.VesselServiceTypes.Load()
            dB.MeasurementTypes.Load()
            dB.Tolerances.Load()
        End Sub

        Public Sub LoadJobs(dB As HaleMRIContext)
            dB.Jobs.
                Include(Function(j) j.InspectedByNavigation).
                Include(Function(j) j.Vessel).
                    ThenInclude(Function(v) v.Customer).
                Include(Function(j) j.JobDetails).
                    ThenInclude(Function(jd) jd.PerformedByNavigation).
                Include(Function(j) j.JobDetails).
                    ThenInclude(Function(jd) jd.MeasurementType).
                Load()
            dB.Blades.Load()
            dB.Cups.Load()
            dB.Employees.Load()
            dB.Exclusions.Load()
            dB.MeasurementTypes.Load()
            dB.Manufacturers.Load()
            dB.Materials.Load()
            dB.Propellers.Load()
            dB.Rotations.Load()
            dB.Styles.Load()
        End Sub

        Public Sub LoadJobDetails(dB As HaleMRIContext)
            dB.JobDetails.
                Include(Function(jd) jd.Job).
                    ThenInclude(Function(j) j.Vessel).
                        ThenInclude(Function(v) v.Customer).
                Include(Function(jd) jd.MeasurementType).
                Include(Function(jd) jd.PerformedByNavigation).
                Include(Function(jd) jd.ReferenceCell).
                Include(Function(jd) jd.ToleranceClassNavigation).
                Load()
            dB.Tolerances.Load()
        End Sub

        Public Sub LoadManufacturers(dB As HaleMRIContext)
            dB.Manufacturers.
                Include(Function(m) m.CountryCodeNavigation).
                Include(Function(m) m.StateNavigation).
                Include(Function(m) m.Jobs).
                    ThenInclude(Function(j) j.Vessel).
                        ThenInclude(Function(v) v.Customer).
                Include(Function(m) m.Propellers).
                AsSplitQuery().
                Load()
            dB.CountryCodes.Load()
            dB.StateCodes.Load()
        End Sub

        Public Sub LoadPropellers(dB As HaleMRIContext)
            dB.Propellers.
                Include(Function(p) p.Manufacturer).
                    ThenInclude(Function(m) m.CountryCodeNavigation).
                Include(Function(p) p.Manufacturer).
                    ThenInclude(Function(m) m.StateNavigation).
                AsSplitQuery().
                Load()
            dB.Blades.Load()
            dB.Materials.Load()
            dB.Rotations.Load()
            dB.Styles.Load()
        End Sub

        Public Sub LoadReports(dB As HaleMRIContext)
            dB.Reports.
                Include(Function(rpt) rpt.ReportElements).
                Include(Function(r) r.ModifiedByNavigation).
                Load()
            dB.Employees.Load()
        End Sub

        Public Function LoadMeasurements(dB As HaleMRIContext, ByVal jd As JobDetail) As List(Of RadiusMeasurement)
            Dim data = dB.RadiusMeasurements.
                Where(Function(rm) rm.JobDetailsId = jd.Id.ToString()).
                Include(Function(rm) rm.CellMeasurements).
                Include(Function(rm) rm.ExtremeMeasurements).
                AsNoTracking().
                AsSplitQuery().
                ToList()
            SortMeasurements(data)
            Return data
        End Function

        Private Sub SortMeasurements(ByRef measurements As List(Of RadiusMeasurement))
            For Each rm As RadiusMeasurement In measurements
                rm.CellMeasurements = rm.CellMeasurements.OrderBy(Function(cm) cm.Id).ToList()
                rm.ExtremeMeasurements = rm.ExtremeMeasurements.OrderBy(Function(em) em.Id).ToList()
            Next
        End Sub
    End Module
End Namespace
