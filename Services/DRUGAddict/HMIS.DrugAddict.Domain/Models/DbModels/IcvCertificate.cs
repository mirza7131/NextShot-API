using System;
using System.Collections.Generic;

namespace HMIS.DrugAddict.Domain.Models.DbModels;

public partial class IcvCertificate
{
    public Guid IcvCertificateId { get; set; }

    public Guid? PatientId { get; set; }

    public Guid? PatientVisitId { get; set; }

    public Guid? PatientDiagnoseId { get; set; }

    public int? HealthFacilityId { get; set; }

    public Guid? DoctorId { get; set; }

    public string? OpdNo { get; set; }

    public DateTime? IssueDate { get; set; }

    public string? Name { get; set; }

    public string? RelativeName { get; set; }

    public string? Cnic { get; set; }

    public Guid? NationalityTypeProfileId { get; set; }

    public string? PassportNo { get; set; }

    public string? Condition { get; set; }

    public string? Vaccination { get; set; }

    public string? QrCodeImagePath { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? NameOfDisease { get; set; }

    public DateTime? VaccinationDate { get; set; }

    public bool? IsOpdNoGenerated { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public byte ActionTypeId { get; set; }
}
