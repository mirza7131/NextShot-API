using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class Sheet2
{
    public double? Sr { get; set; }

    public DateTime? PatientRegistrationDate { get; set; }

    public string? Name { get; set; }

    public string? Cnic { get; set; }

    public string? CnicUpdated { get; set; }

    public double? Age { get; set; }

    public string? DateOfBirth { get; set; }

    public string? Gender { get; set; }

    public string? PatientMobileNo { get; set; }

    public string? Patient { get; set; }

    public string? Mobile { get; set; }

    public string? PatientMobile { get; set; }

    public string? PatientType { get; set; }

    public DateTime? MedicineDeliveredDates { get; set; }

    public string? PatientDivisionName { get; set; }

    public string? PatientDistrictName { get; set; }

    public string? PatientTehsilName { get; set; }

    public string? PatientAddress { get; set; }

    public string? HealthFacilityDivision { get; set; }

    public string? HealthFacilityDistrict { get; set; }

    public string? HealthFacilityTehsil { get; set; }

    public string? HealthFacilityName { get; set; }
}
