using System;
using System.Collections.Generic;

namespace HMIS.HCP.Domain.Models.OldDbModels;

public partial class HwdPatient
{
    public int Id { get; set; }

    public string? Cnic { get; set; }

    public string? RegNo { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? SpouseName { get; set; }

    public DateTime? Dob { get; set; }

    public float? Age { get; set; }

    public int? Gender { get; set; }

    public int? MaritalStatus { get; set; }

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

    public string? PcrType { get; set; }

    public string? FormType { get; set; }

    public string? GenderApi { get; set; }

    public string? MaritalStatusApi { get; set; }

    public string? DistrictApi { get; set; }

    public string? TehsilApi { get; set; }

    public string? UniqueId { get; set; }

    public string IsTaken { get; set; } = null!;

    public string? Occupation { get; set; }

    public string? Qualification { get; set; }
}
