Imports LibDatabase.Models

Public Module MRIMath
    Public Function GetLocalPitch(cm As List(Of CellMeasurement), sectors As Integer, sector As Integer, diameter As Double, radiusPercent As Double, TeExclusion As Double, LeExclusion As Double) As Double
        'Returns the local pitch of a sector based on the first and last cell measurements in that sector
        Dim startangle As Double = cm.FirstOrDefault().Angle
        Dim endangle As Double = cm.LastOrDefault().Angle
        Dim deltaangle As Double = (startangle + 360) - (360 + endangle) ' offset angles to handle negative ends
        Dim cl As Double = GetChordLength(cm, diameter, CInt(radiusPercent))
        If cl <> 0 Then
            startangle -= (deltaangle * TeExclusion / cl)
            endangle += (deltaangle * LeExclusion / cl)
        End If ' bunch of math that finds the angle bounds of the sector excluding TE and LE Exclusion zones
        Dim sectorArc As Double = (startangle - endangle) / sectors
        Dim sectorstartangle As Double = startangle - (sectorArc * (sector - 1))
        Dim sectorendangle As Double = sectorstartangle - sectorArc

        Dim sectorstartovercell As CellMeasurement
        Dim sectorstartundercell As CellMeasurement
        Dim sectorendovercell As CellMeasurement
        Dim sectorendundercell As CellMeasurement
        If TeExclusion = 0 Then
            sectorstartovercell = cm.Where(Function(c) Math.Round(c.Angle.Value, 5) >= Math.Round(sectorstartangle, 5)).LastOrDefault()
            sectorstartundercell = cm.Where(Function(c) Math.Round(c.Angle.Value, 5) <= Math.Round(sectorstartangle, 5)).FirstOrDefault()
        Else
            sectorstartovercell = cm.Where(Function(c) c.Angle.Value >= sectorstartangle).LastOrDefault()
            sectorstartundercell = cm.Where(Function(c) c.Angle.Value <= sectorstartangle).FirstOrDefault()
        End If
        If LeExclusion = 0 Then
            sectorendundercell = cm.Where(Function(c) Math.Round(c.Angle.Value, 5) <= Math.Round(sectorendangle, 2)).FirstOrDefault()
            sectorendovercell = cm.Where(Function(c) Math.Round(c.Angle.Value, 5) >= Math.Round(sectorendangle, 2)).LastOrDefault()
            If sector = sectors Then
                sectorendundercell = cm.LastOrDefault()
                sectorendovercell = cm.LastOrDefault()
            End If
        Else
            sectorendundercell = cm.Where(Function(c) c.Angle.Value <= sectorendangle).FirstOrDefault()
            sectorendovercell = cm.Where(Function(c) c.Angle.Value >= sectorendangle).LastOrDefault()
        End If
        Dim sectorstartdepth As Double = 0.0
        Dim sectorenddepth As Double = 0.0
        Dim Ratio As Double = 0.0
        If sectorstartovercell IsNot Nothing AndAlso sectorstartundercell IsNot Nothing Then
            'Linear interpolation to find the depth at the exact sector start angle
            If sectorstartangle - sectorstartundercell.Angle <> 0 Then
                If sectorstartovercell.Angle - sectorstartundercell.Angle <> 0 Then
                    Ratio = (sectorstartangle - sectorstartundercell.Angle) / (sectorstartovercell.Angle - sectorstartundercell.Angle)
                End If
            End If
            sectorstartdepth = sectorstartundercell.Depth + (Ratio * (sectorstartovercell.Depth - sectorstartundercell.Depth))
        End If
        If sectorendovercell IsNot Nothing AndAlso sectorendundercell IsNot Nothing Then
            'Linear interpolation to find the depth at the exact sector end angle
            If sectorendangle - sectorendundercell.Angle <> 0 Then
                If sectorendovercell.Angle - sectorendundercell.Angle <> 0 Then
                    Ratio = (sectorendangle - sectorendundercell.Angle) / (sectorendovercell.Angle - sectorendundercell.Angle)
                End If
            End If
            sectorenddepth = sectorendundercell.Depth + (Ratio * (sectorendovercell.Depth - sectorendundercell.Depth))
        End If
        Return GetPitch(sectorstartangle, sectorendangle, sectorstartdepth, sectorenddepth) 'sectorenddepth - sectorstartdepth) * (360 / sectorArc)
    End Function
    Public Function GetLocalHeight(cm As List(Of CellMeasurement), sectors As Integer, sector As Integer, diameter As Double, radiusPercent As Double, TeExclusion As Double, LeExclusion As Double) As Double
        'Returns the local height of a sector based on the first and last cell measurements in that sector
        Dim startangle As Double = cm.FirstOrDefault().Angle
        Dim endangle As Double = cm.LastOrDefault().Angle
        Dim deltaangle As Double = startangle - endangle
        Dim cl As Double = GetChordLength(cm, diameter, CInt(radiusPercent))
        If cl <> 0 Then
            startangle -= (deltaangle * TeExclusion / cl)
            endangle += (deltaangle * LeExclusion / cl)
        End If ' bunch of math that finds the angle bounds of the sector excluding TE and LE Exclusion zones
        Dim sectorArc As Double = (startangle - endangle) / sectors
        Dim sectorstartangle As Double = startangle - (sectorArc * (sector - 1))
        Dim sectorendangle As Double = sectorstartangle - sectorArc
        Dim sectorstartcell As CellMeasurement = cm.Where(Function(c) c.Angle >= sectorstartangle).LastOrDefault()
        Dim sectorendcell As CellMeasurement = cm.Where(Function(c) c.Angle <= sectorendangle).FirstOrDefault()
        Return Math.Abs(sectorendcell.Depth.Value - sectorstartcell.Depth.Value) ' returns the computed height of the sector
    End Function
    Public Function GetLocalHeightStartSector(cm As List(Of CellMeasurement), sectors As Integer, sector As Integer, diameter As Double, radiusPercent As Double, TeExclusion As Double, LeExclusion As Double) As Double
        'Returns the local Height of the start of a sector for use in Line Graphs
        Dim startangle As Double = cm.FirstOrDefault().Angle
        Dim endangle As Double = cm.LastOrDefault().Angle
        Dim deltaangle As Double = startangle - endangle
        Dim cl As Double = GetChordLength(cm, diameter, CInt(radiusPercent))
        If cl <> 0 Then
            startangle -= (deltaangle * TeExclusion / cl)
            endangle += (deltaangle * LeExclusion / cl)
        End If ' bunch of math that finds the angle bounds of the sector excluding TE and LE Exclusion zones
        Dim sectorArc As Double = (startangle - endangle) / sectors
        Dim sectorstartangle As Double = startangle - (sectorArc * (sector - 1))
        Dim sectorendangle As Double = sectorstartangle - sectorArc
        Dim sectorstartovercell As CellMeasurement
        Dim sectorstartundercell As CellMeasurement
        Dim sectorendovercell As CellMeasurement
        Dim sectorendundercell As CellMeasurement
        If TeExclusion = 0 Then
            sectorstartovercell = cm.Where(Function(c) Math.Round(c.Angle.Value, 5) >= Math.Round(sectorstartangle, 5)).LastOrDefault()
            sectorstartundercell = cm.Where(Function(c) Math.Round(c.Angle.Value, 5) <= Math.Round(sectorstartangle, 5)).FirstOrDefault()
        Else
            sectorstartovercell = cm.Where(Function(c) c.Angle.Value >= sectorstartangle).LastOrDefault()
            sectorstartundercell = cm.Where(Function(c) c.Angle.Value <= sectorstartangle).FirstOrDefault()
        End If
        If LeExclusion = 0 Then
            sectorendundercell = cm.Where(Function(c) Math.Round(c.Angle.Value, 5) <= Math.Round(sectorendangle, 2)).FirstOrDefault()
            sectorendovercell = cm.Where(Function(c) Math.Round(c.Angle.Value, 5) >= Math.Round(sectorendangle, 2)).LastOrDefault()
            If sector = sectors And LeExclusion Then
                sectorendundercell = cm.LastOrDefault()
                sectorendovercell = cm.LastOrDefault()
            End If
        Else
            sectorendundercell = cm.Where(Function(c) c.Angle.Value <= sectorendangle).FirstOrDefault()
            sectorendovercell = cm.Where(Function(c) c.Angle.Value >= sectorendangle).LastOrDefault()
        End If
        Dim sectorstartdepth As Double = 0.0
        Dim Ratio As Double = 0.0
        If sectorstartovercell IsNot Nothing AndAlso sectorstartundercell IsNot Nothing Then
            'Linear interpolation to find the depth at the exact sector start angle
            If sectorstartangle - sectorstartundercell.Angle <> 0 Then
                If sectorstartovercell.Angle - sectorstartundercell.Angle <> 0 Then
                    Ratio = (sectorstartangle - sectorstartundercell.Angle) / (sectorstartovercell.Angle - sectorstartundercell.Angle)
                End If
            End If
            sectorstartdepth = sectorstartundercell.Depth + (Ratio * (sectorstartovercell.Depth - sectorstartundercell.Depth))
        End If
        Return sectorstartdepth
    End Function
    Public Function GetLocalHeightEndSector(cm As List(Of CellMeasurement), sectors As Integer, sector As Integer, diameter As Double, radiusPercent As Double, TeExclusion As Double, LeExclusion As Double) As Double
        Dim startangle As Double = cm.FirstOrDefault().Angle
        Dim endangle As Double = cm.LastOrDefault().Angle
        Dim deltaangle As Double = startangle - endangle
        Dim cl As Double = GetChordLength(cm, diameter, CInt(radiusPercent))
        If cl <> 0 Then
            startangle -= (deltaangle * TeExclusion / cl)
            endangle += (deltaangle * LeExclusion / cl)
        End If ' bunch of math that finds the angle bounds of the sector excluding TE and LE Exclusion zones
        Dim sectorArc As Double = (startangle - endangle) / sectors
        Dim sectorstartangle As Double = startangle - (sectorArc * (sector - 1))
        Dim sectorendangle As Double = sectorstartangle - sectorArc

        Dim sectorstartovercell As CellMeasurement
        Dim sectorstartundercell As CellMeasurement
        Dim sectorendovercell As CellMeasurement
        Dim sectorendundercell As CellMeasurement
        If TeExclusion = 0 Then
            sectorstartovercell = cm.Where(Function(c) Math.Round(c.Angle.Value, 5) >= Math.Round(sectorstartangle, 5)).LastOrDefault()
            sectorstartundercell = cm.Where(Function(c) Math.Round(c.Angle.Value, 5) <= Math.Round(sectorstartangle, 5)).FirstOrDefault()
        Else
            sectorstartovercell = cm.Where(Function(c) c.Angle.Value >= sectorstartangle).LastOrDefault()
            sectorstartundercell = cm.Where(Function(c) c.Angle.Value <= sectorstartangle).FirstOrDefault()
        End If
        If LeExclusion = 0 Then
            sectorendundercell = cm.Where(Function(c) Math.Round(c.Angle.Value, 5) <= Math.Round(sectorendangle, 2)).FirstOrDefault()
            sectorendovercell = cm.Where(Function(c) Math.Round(c.Angle.Value, 5) >= Math.Round(sectorendangle, 2)).LastOrDefault()
            If sector = sectors Then
                sectorendundercell = cm.LastOrDefault()
                sectorendovercell = cm.LastOrDefault()
            End If
        Else
            sectorendundercell = cm.Where(Function(c) c.Angle.Value <= sectorendangle).FirstOrDefault()
            sectorendovercell = cm.Where(Function(c) c.Angle.Value >= sectorendangle).LastOrDefault()
        End If
        Dim sectorenddepth As Double = 0.0
        Dim Ratio As Double = 0.0
        If sectorendovercell IsNot Nothing AndAlso sectorendundercell IsNot Nothing Then
            'Linear interpolation to find the depth at the exact sector end angle
            If sectorendangle - sectorendundercell.Angle <> 0 Then
                If sectorendovercell.Angle - sectorendundercell.Angle <> 0 Then
                    Ratio = (sectorendangle - sectorendundercell.Angle) / (sectorendovercell.Angle - sectorendundercell.Angle)
                End If
            End If
            sectorenddepth = sectorendundercell.Depth + (Ratio * (sectorendovercell.Depth - sectorendundercell.Depth))
        End If
        Return sectorenddepth
    End Function
    Public Function GetRefHeightsHighTol(center As Boolean, entirescan As Boolean, refpitch As Double, Tolclass As Tolerance, cm As List(Of CellMeasurement)) As List(Of Double)
        'Returns a list of height values that are already modified based on ref point for use in Line graphs
        Dim Numbers As New List(Of Double)
        refpitch += refpitch * (Tolclass.LocalPitchPercent / 100)
        Dim LEE As Double = cm.FirstOrDefault().RadiusMeasurement.JobDetails.Job.LeExclusion
        Dim TEE As Double = cm.FirstOrDefault().RadiusMeasurement.JobDetails.Job.TeExclusion
        Dim StartAngle As Double = cm.FirstOrDefault().Angle
        Dim EndAngle As Double = cm.LastOrDefault().Angle
        Dim TotAngle As Double
        If EndAngle < 0 Then
            TotAngle = Math.Abs(StartAngle + 360) - (EndAngle + 360)
        Else
            TotAngle = Math.Abs(StartAngle - EndAngle)
        End If
        If entirescan Then
            Dim cl As Double = GetChordLength(cm, cm(0).RadiusMeasurement.JobDetails.Job.PropellerDiameter, Math.Round(cm(0).RadiusMeasurement.Radius.Value))
            If cl <> 0 Then
                StartAngle -= (TotAngle * TEE / cl)
                EndAngle += (TotAngle * LEE / cl)
            End If ' bunch of math that finds the angle bounds of the sector excluding TE and LE Exclusion zones
            If EndAngle < 0 Then
                TotAngle = Math.Abs(StartAngle + 360) - (EndAngle + 360)
            Else
                TotAngle = Math.Abs(StartAngle - EndAngle)
            End If
        End If
        Dim anglediffbetweenpoints As Double = TotAngle / 20
        Dim heightdiffbetweenpoints As Double = (refpitch * anglediffbetweenpoints) / 360
        Dim x As Integer
        For x = 0 To 20
            If x = 0 Then
                Numbers.Add(GetLocalHeightStartSector(cm, 20, 1, cm(0).RadiusMeasurement.JobDetails.Job.PropellerDiameter, cm(0).RadiusMeasurement.Radius, cm(0).RadiusMeasurement.JobDetails.Job.TeExclusion, cm(0).RadiusMeasurement.JobDetails.Job.LeExclusion))
            Else
                Numbers.Add(GetLocalHeightEndSector(cm, 20, x, cm(0).RadiusMeasurement.JobDetails.Job.PropellerDiameter, cm(0).RadiusMeasurement.Radius, cm(0).RadiusMeasurement.JobDetails.Job.TeExclusion, cm(0).RadiusMeasurement.JobDetails.Job.LeExclusion))
            End If
        Next
        If center Then
            For x = 0 To 20
                Numbers.Item(x) -= Numbers.Item(10)
                Numbers.Item(x) += heightdiffbetweenpoints * (10 - x)
            Next
        Else
            For x = 0 To 20
                Numbers.Item(x) -= Numbers.Item(0)
                Numbers.Item(x) += heightdiffbetweenpoints * x
            Next
        End If
        Return Numbers
    End Function
    Public Function GetRefHeightsLowTol(center As Boolean, entirescan As Boolean, refpitch As Double, Tolclass As Tolerance, cm As List(Of CellMeasurement)) As List(Of Double)
        'Returns a list of height values that are already modified based on ref point for use in Line graphs
        Dim Numbers As New List(Of Double)
        refpitch -= refpitch * (Tolclass.LocalPitchPercent / 100)
        Dim LEE As Double = cm.FirstOrDefault().RadiusMeasurement.JobDetails.Job.LeExclusion
        Dim TEE As Double = cm.FirstOrDefault().RadiusMeasurement.JobDetails.Job.TeExclusion
        Dim StartAngle As Double = cm.FirstOrDefault().Angle
        Dim EndAngle As Double = cm.LastOrDefault().Angle
        Dim TotAngle As Double
        If EndAngle < 0 Then
            TotAngle = Math.Abs(StartAngle + 360) - (EndAngle + 360)
        Else
            TotAngle = Math.Abs(StartAngle - EndAngle)
        End If
        If entirescan Then
            Dim cl As Double = GetChordLength(cm, cm(0).RadiusMeasurement.JobDetails.Job.PropellerDiameter, Math.Round(cm(0).RadiusMeasurement.Radius.Value))
            If cl <> 0 Then
                StartAngle -= (TotAngle * TEE / cl)
                EndAngle += (TotAngle * LEE / cl)
            End If ' bunch of math that finds the angle bounds of the sector excluding TE and LE Exclusion zones
            If EndAngle < 0 Then
                TotAngle = Math.Abs(StartAngle + 360) - (EndAngle + 360)
            Else
                TotAngle = Math.Abs(StartAngle - EndAngle)
            End If
        End If
        Dim anglediffbetweenpoints As Double = TotAngle / 20
        Dim heightdiffbetweenpoints As Double = (refpitch * anglediffbetweenpoints) / 360
        Dim x As Integer
        For x = 0 To 20
            If x = 0 Then
                Numbers.Add(GetLocalHeightStartSector(cm, 20, 1, cm(0).RadiusMeasurement.JobDetails.Job.PropellerDiameter, cm(0).RadiusMeasurement.Radius, cm(0).RadiusMeasurement.JobDetails.Job.TeExclusion, cm(0).RadiusMeasurement.JobDetails.Job.LeExclusion))
            Else
                Numbers.Add(GetLocalHeightEndSector(cm, 20, x, cm(0).RadiusMeasurement.JobDetails.Job.PropellerDiameter, cm(0).RadiusMeasurement.Radius, cm(0).RadiusMeasurement.JobDetails.Job.TeExclusion, cm(0).RadiusMeasurement.JobDetails.Job.LeExclusion))
            End If
        Next
        If center Then
            For x = 0 To 20
                Numbers.Item(x) -= Numbers.Item(10)
                Numbers.Item(x) += heightdiffbetweenpoints * (10 - x)
            Next
        Else
            For x = 0 To 20
                Numbers.Item(x) -= Numbers.Item(0)
                Numbers.Item(x) += heightdiffbetweenpoints * x
            Next
        End If
        Return Numbers
    End Function
    Public Function GetRefHeightsStraight(center As Boolean, entirescan As Boolean, refpitch As Double, cm As List(Of CellMeasurement)) As List(Of Double)
        'Returns a list of height values for use in line graphs
        'Changed this to calculate with Exclusions and only use the actual angle of the scan instead of 360 / PropBlades' something in here is skewing values higher than they should be
        Dim numbers As New List(Of Double)
        Dim x As Integer
        Dim LEE As Double = cm.FirstOrDefault().RadiusMeasurement.JobDetails.Job.LeExclusion
        Dim TEE As Double = cm.FirstOrDefault().RadiusMeasurement.JobDetails.Job.TeExclusion
        Dim StartAngle As Double = cm.FirstOrDefault().Angle
        Dim EndAngle As Double = cm.LastOrDefault().Angle
        Dim TotAngle As Double
        If EndAngle < 0 Then
            TotAngle = Math.Abs(StartAngle + 360) - (EndAngle + 360)
        Else
            TotAngle = Math.Abs(StartAngle - EndAngle)
        End If
        If Not entirescan Then
            Dim cl As Double = GetChordLength(cm, cm(0).RadiusMeasurement.JobDetails.Job.PropellerDiameter, Math.Round(cm(0).RadiusMeasurement.Radius.Value))
            If cl <> 0 Then
                StartAngle -= (TotAngle * TEE / cl)
                EndAngle += (TotAngle * LEE / cl)
            End If ' bunch of math that finds the angle bounds of the sector excluding TE and LE Exclusion zones
            If EndAngle < 0 Then
                TotAngle = Math.Abs(StartAngle + 360) - (EndAngle + 360)
            Else
                TotAngle = Math.Abs(StartAngle - EndAngle)
            End If
        End If
        Dim anglediffbetweenpoints As Double = TotAngle / 20
        Dim heightdiffbetweenpoints As Double = (refpitch * anglediffbetweenpoints) / 360
        If center Then
            For x = 0 To 20
                Dim q = 20 - x
                numbers.Add(heightdiffbetweenpoints * (q - 10))
            Next
        Else
            For x = 0 To 20
                Dim q = 20 - x
                numbers.Add(heightdiffbetweenpoints * q)
            Next
        End If
        Return numbers
    End Function
    Public Function GetChordMidAngle(cm As List(Of CellMeasurement)) As Double
        Dim startangle As Double = cm.FirstOrDefault().Angle
        Dim endangle As Double = cm.LastOrDefault().Angle
        Dim deltaangle As Double = 0.0
        Dim negativeend As Boolean = False
        If endangle < 0 Then
            deltaangle = Math.Abs((startangle + 360) - (endangle + 360))
            negativeend = True
        Else
            deltaangle = Math.Abs(startangle - endangle)
        End If
        If startangle > endangle Or negativeend Then
            Return startangle - (deltaangle / 2) ' returns the computed midpoint angle
        Else
            Return endangle - (deltaangle / 2) ' returns the computed midpoint angle - here for compatibility with old scans
        End If
    End Function
    Public Function GetChordMidDepth(cm As List(Of CellMeasurement)) As Double
        'Dim angle As Double = GetChordMidAngle(cm)
        'Dim endDepthcell As CellMeasurement = cm.Where(Function(c) c.Angle >= angle).FirstOrDefault()
        'Dim startDepthcell As CellMeasurement = cm.Where(Function(c) c.Angle <= angle).LastOrDefault()
        'Dim deltaDepth As Double = Math.Abs(startDepthcell.Depth.Value - endDepthcell.Depth.Value)
        'Dim Ratio As Double = (angle - startDepthcell.Angle.Value) / (endDepthcell.Angle.Value - startDepthcell.Angle.Value)
        'Dim interdepth = startDepthcell.Depth.Value + (deltaDepth * Ratio)
        'Return interdepth ' returns the computed midpoint angle
        Dim startDepth As Double = cm.FirstOrDefault().Depth
        Dim endDepth As Double = cm.LastOrDefault().Depth
        Dim deltaDepth As Double = 0.0
        Dim negativeend As Boolean = False
        If endDepth < 0 Then
            deltaDepth = Math.Abs((startDepth + 360) - (endDepth + 360))
            negativeend = True
        Else
            deltaDepth = Math.Abs(startDepth - endDepth)
        End If
        If startDepth > endDepth Or negativeend Then
            Return startDepth - (deltaDepth / 2) ' returns the computed midpoint Depth
        Else
            Return endDepth - (deltaDepth / 2) ' returns the computed midpoint Depth - here for compatibility with old scans
        End If
    End Function
    Public Function GetPitch(firstangle As Double, secondangle As Double, firstdepth As Double, seconddepth As Double) As Double
        'Pitch = (360 * Change in Depth) / Change in Angle
        ' Can be used to get local pitch between two cellmeasurements,
        If firstangle < 0 Or secondangle < 0 Then
            firstangle += 360
            secondangle += 360
        End If
        If firstdepth < 0 Or secondangle < 0 Then
            firstdepth += 1000
            seconddepth += 1000
        End If
        Dim deltaangle = secondangle - firstangle
        Dim deltadepth = seconddepth - firstdepth
        Return If(deltaangle <> 0.0, Math.Abs((360.0 * deltadepth) / deltaangle), 0.0)
    End Function
    Public Function GetChordLength(cm As List(Of CellMeasurement), diameter As Double, radperc As Integer) As Double
        'ChordLength = sqrt((Change in Depth)^2 + ((Diameter * Radius Percent) * PI *(Change in Angle / 360))^2)
        'used to get the chord length between two cell measurements in inches
        Dim deltaangle As Double = cm.LastOrDefault().Angle - cm.FirstOrDefault().Angle 'Total change in angle on a radius of one blade
        Dim deltadepth As Double = cm.LastOrDefault().Depth - cm.FirstOrDefault().Depth 'Total change in depth on a radius of one blade

        Dim adjusteddiameter As Double = diameter * (radperc / 100) 'Gets the value side of a radius measurement from a radius percent needed for an arc length calculation

        Dim arclength = adjusteddiameter * Math.PI * deltaangle / 360 'Gets the length of the arc/flat of the radial chord

        Dim squared = Math.Pow(deltadepth, 2) + Math.Pow(arclength, 2)
        Dim chordlength = Math.Sqrt(squared) 'Pythagorean theorem to get chord length from change in depth and arc length

        Return chordlength
    End Function
    Public Function GetChordLength(startAngle As Double, endAngle As Double, startDepth As Double, endDepth As Double, diameter As Double, radperc As Integer) As Double
        'ChordLength = sqrt((Change in Depth)^2 + ((Diameter * Radius Percent) * PI *(Change in Angle / 360))^2)
        'used to get the chord length between two cell measurements in inches
        Dim deltaangle As Double = endAngle - startAngle 'Total change in angle on a radius of one blade
        Dim deltadepth As Double = endDepth - startDepth 'Total change in depth on a radius of one blade

        Dim adjusteddiameter As Double = diameter * (radperc / 100) 'Gets the value side of a radius measurement from a radius percent needed for an arc length calculation

        Dim arclength = adjusteddiameter * Math.PI * deltaangle / 360 'Gets the length of the arc/flat of the radial chord

        Dim squared = Math.Pow(deltadepth, 2) + Math.Pow(arclength, 2)
        Dim chordlength = Math.Sqrt(squared) 'Pythagorean theorem to get chord length from change in depth and arc length

        Return chordlength
    End Function
    Public Function GetBladeNumber(Angle As Double, Blades As Integer) As Integer
        'CurrentBlade = Blades - Math.Ceiling(Angle/(360/Blades))
        ' Return CInt(Math.Ceiling(Angle / (360 / Blades)))
        Return If(Blades <> 0, CInt(Math.Ceiling(Angle / (360 / Blades))), 1)
    End Function
    Public Function GetRadiusMeasurementPitch(ByVal cellMeasurements As ICollection(Of CellMeasurement), TeExclusion As Double, LeExclusion As Double) As Double ' Changed this due to terms written in the ISO standard of how to measure average pitch of a radial section
        Dim avgPitch = GetLocalPitch(cellMeasurements, 1, 1, cellMeasurements.FirstOrDefault().RadiusMeasurement.JobDetails.Job.PropellerDiameter, cellMeasurements.FirstOrDefault().RadiusMeasurement.Radius, TeExclusion, LeExclusion)
        Return avgPitch
    End Function
    Public Function GetBladeAveragePitch(BladeData As List(Of RadiusMeasurement))
        Dim LEE As Double = BladeData.FirstOrDefault().JobDetails.Job.LeExclusion
        Dim TEE As Double = BladeData.FirstOrDefault().JobDetails.Job.TeExclusion
        Dim avgpitch As Double = 0.0
        Dim pitchcount = BladeData.Count
        For Each rm In BladeData
            'Dim pitch = GetRadiusMeasurementPitch(rm.CellMeasurements.ToList(), TEE, LEE)
            Dim pitch = GetRadiusMeasurementPitch(rm.CellMeasurements, TEE, LEE)
            avgpitch += pitch
        Next
        If pitchcount <> 0 Then
            avgpitch /= pitchcount
        End If
        Return avgpitch
    End Function
    Public Function PolarToCartesian(radius As Double, angleDegrees As Double) As (x As Double, y As Double)
        Dim angleRadians As Double = angleDegrees * (Math.PI / 180.0)
        Dim x As Double = radius * Math.Cos(angleRadians)
        Dim y As Double = radius * Math.Sin(angleRadians)
        Return (x, y)
    End Function
End Module
