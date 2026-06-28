using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels;

public partial class BirthCertificate
{
    public Guid BirthCertificateId { get; set; }

    public Guid? PatientId { get; set; }

    public Guid? PatientVisitId { get; set; }

    public Guid? PatientDiagnoseId { get; set; }

    public int? HealthFacilityId { get; set; }

    public DateTime? IssueDate { get; set; }

    public string? MotherName { get; set; }

    public string? MotherCnic { get; set; }

    public string? FatherName { get; set; }

    public string? FatherCnic { get; set; }

    public Guid? OccupationTypeProfileId { get; set; }

    public Guid? FatherNationalityTypeProfileId { get; set; }

    public Guid? MotherNationalityTypeProfileId { get; set; }

    public Guid? ReligiousProfileId { get; set; }

    public string? GrandFatherName { get; set; }

    public string? ChildName { get; set; }

    public Guid? GenderProfileId { get; set; }

    public Guid? ChildBloodGroupProfileId { get; set; }

    public Guid? MotherBloodGroupProfileId { get; set; }

    public int? AnnualNumber { get; set; }

    public DateTime? Dob { get; set; }

    public string? PlaceOfBirth { get; set; }

    public string? DoctorName { get; set; }

    public string? GynaeUnit { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public byte ActionTypeId { get; set; }
}
