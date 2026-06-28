using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.DbModels;

public partial class Mleexamination
{
    public Guid MleexaminationId { get; set; }

    public Guid MlebasicInfoId { get; set; }

    public Guid PatientId { get; set; }

    public Guid PatientVisitId { get; set; }

    public Guid? PatientDiagnoseId { get; set; }

    public Guid? PatientStatusProfileId { get; set; }

    public int? HealthFacilityId { get; set; }

    public string? History { get; set; }

    public string? ClothExamination { get; set; }

    public string? GeneralPhysicalExamination { get; set; }

    public string? InjuriesDescription { get; set; }

    public string? AdvisedInvestigate { get; set; }

    public string? LaboratoryInvestigation { get; set; }

    public string? OpinionSpecialistOrXrayReport { get; set; }

    public string? NatureOfInjuries { get; set; }

    public string? Fabrication { get; set; }

    public string? DurationOfInjuries { get; set; }

    public string? WeaponPoison { get; set; }

    public string? KuoInjuries { get; set; }

    public long? ReportCounts { get; set; }

    public bool? IsFinalReport { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public byte? ActionTypeId { get; set; }

    public virtual MlebasicInfo MlebasicInfo { get; set; } = null!;
}
