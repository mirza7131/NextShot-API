using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class ViewPatientSampleListDashboard
{
    public int PatientId { get; set; }

    public string? MrNo { get; set; }

    public string? Name { get; set; }

    public string? HealthFacilityCode { get; set; }

    public string? DistrictCode { get; set; }

    public string? DivisionCode { get; set; }

    public string? TehsilCode { get; set; }

    public string? Cnic { get; set; }

    public string? ContactNo { get; set; }

    public string? HealthFacilityDivision { get; set; }

    public string? HealthFacilityDistrict { get; set; }

    public string? HealthFacilityTehsil { get; set; }

    public string? Report { get; set; }

    public string? Result { get; set; }

    public string? Hivresult { get; set; }

    public int? TestId { get; set; }

    public DateTime? SampleCreationDate { get; set; }

    public DateTime? PatientCreationDate { get; set; }

    public DateTime? VitalCreationDate { get; set; }
}
