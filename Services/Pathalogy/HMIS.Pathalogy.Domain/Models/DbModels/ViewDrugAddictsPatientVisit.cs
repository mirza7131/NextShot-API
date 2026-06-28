using System;
using System.Collections.Generic;

namespace HMIS.Pathalogy.Domain.Models.DbModels;

public partial class ViewDrugAddictsPatientVisit
{
    public Guid PatientOpenVisitId { get; set; }

    public string? FullName { get; set; }

    public string Cnic { get; set; } = null!;

    public string Addicted { get; set; } = null!;

    public string? GuardianName { get; set; }

    public int? Age { get; set; }

    public DateTime? Dob { get; set; }

    public string Gender { get; set; } = null!;

    public string? ParmanentAddress { get; set; }

    public string? District { get; set; }

    public DateTime? AdmissionDate { get; set; }

    public DateTime? DischargeDate { get; set; }

    public string TreatmentStatus { get; set; } = null!;

    public string Rehabilitation { get; set; } = null!;

    public string? HealthFacility { get; set; }

    public string? Json { get; set; }
}
