using System;
using System.Collections.Generic;

namespace HMIS.HCP.Domain.Models.OldDbModels;

public partial class MicroScreening
{
    public int Id { get; set; }

    public string? Cnic { get; set; }

    public string? RegNo { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? MrnNo { get; set; }

    public string? SpouseName { get; set; }

    public DateTime? Dob { get; set; }

    public float? Age { get; set; }

    public string? SchoolRegistrationNo { get; set; }

    public string? SchoolName { get; set; }

    public string? PatientClass { get; set; }

    public int? Gender { get; set; }

    public string? ContactNumber { get; set; }

    public string? AltContactNumber { get; set; }

    public int? District { get; set; }

    public int? Tehsil { get; set; }

    public int? HospitalId { get; set; }

    public string IsPcrDrawn { get; set; } = null!;

    public string? TestRequired { get; set; }

    public string? SampleId { get; set; }

    public string IsFound { get; set; } = null!;

    public int? UserId { get; set; }

    public int? UserHospital { get; set; }

    public int? Created { get; set; }

    public string? IsReception { get; set; }

    public int? LabReceptionistId { get; set; }

    public string HealthType { get; set; } = null!;

    public string? CnicStatus { get; set; }

    public string? NextOfKin { get; set; }

    public string? NextOfKinCnic { get; set; }

    public string? RelContact { get; set; }

    public string? IsHbvTest { get; set; }

    public string? IsHcvTest { get; set; }

    public string? PcrOption { get; set; }

    public string? UcNumber { get; set; }

    public string? MaritalStatus { get; set; }

    public string? PcrType { get; set; }

    public int? ReferralClinic { get; set; }

    public string IsDischarge { get; set; } = null!;

    public int? Updated { get; set; }

    public string? CnicNotAvailable { get; set; }

    public int? EventId { get; set; }

    public string? Address { get; set; }

    public string? ApiDistrictName { get; set; }

    public string? TehsilName { get; set; }

    public string? UcName { get; set; }

    public string? FrmDataPlug { get; set; }

    public string? IsFirstVaccine { get; set; }

    public string? IsSampleAdd { get; set; }

    public string? IsHouseholdInfo { get; set; }

    public string? ActivityType { get; set; }

    public string? ImeiNo { get; set; }

    public string IsSoftDelete { get; set; } = null!;

    public int? SampleCollectedDate { get; set; }

    public int? ScreeningDate { get; set; }

    public string? IsPatRegInEmr { get; set; }

    public string? DistrictsReferral { get; set; }

    public string? TehsilReferralName { get; set; }

    public string? IsAlreadyVaccinated { get; set; }

    public string? Department { get; set; }

    public string IsHbvDetected { get; set; } = null!;

    public string IsHcvDetected { get; set; } = null!;

    public int? TokenNo { get; set; }

    public string IsAssesment { get; set; } = null!;

    public string? IsSecondVaccine { get; set; }

    public int? SecondVaccinationDoseDate { get; set; }

    public string? IsThirdVaccine { get; set; }

    public int? ThirdVaccinationDoseDate { get; set; }
}
