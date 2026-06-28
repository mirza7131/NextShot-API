using System;
using System.Collections.Generic;

namespace HMIS.MIMS.Domain.Models.DbModels;

public partial class FitnessCertificate
{
    public Guid FitnessCertificateId { get; set; }

    public Guid PatientId { get; set; }

    public Guid PatientVisitId { get; set; }

    public Guid? PatientDiagnoseId { get; set; }

    public int? HealthFacilityId { get; set; }

    public Guid? PatientImageId { get; set; }

    public Guid? RightThumbImageId { get; set; }

    public Guid? RightIndexImageId { get; set; }

    public Guid? RightMiddleImageId { get; set; }

    public Guid? RightRingImageId { get; set; }

    public Guid? RightLittleImageId { get; set; }

    public Guid? PreparedByUserId { get; set; }

    public string? RelativeName { get; set; }

    public string? EmployeeLetterNo { get; set; }

    public DateTime? LetterDateTime { get; set; }

    public string? DesignationAppliedFor { get; set; }

    public string? QrCodeImagePath { get; set; }

    public int? AgeByAppearance { get; set; }

    public string? CheckedBy { get; set; }

    public DateTime? IssueDate { get; set; }

    public string? BodilyInfirmity { get; set; }

    public string? Department { get; set; }

    public bool? IsEmployeeLetterNoGenerated { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public byte ActionTypeId { get; set; }

    public Guid? ProfessionTypeId { get; set; }

    public Guid? EducationTypeProfileId { get; set; }

    public string? TrackingId { get; set; }

    public Guid? DesignationAppliedForTypeProfileId { get; set; }

    public string? PresentJob { get; set; }

    public Guid? RecomendedOrnotTypeProfileId { get; set; }
}
