using System;
using System.Collections.Generic;

namespace HMIS.Dashboard.Domain.Models.TransDbModels;

public partial class MlcsvevidenceCollected
{
    public Guid MlcsvevidenceCollectedId { get; set; }

    public Guid? MlcsvinitialInfoId { get; set; }

    public Guid PatientId { get; set; }

    public Guid PatientVisitId { get; set; }

    public Guid? PatientDiagnoseId { get; set; }

    public Guid? PatientStatusProfileId { get; set; }

    public int? HealthFacilityId { get; set; }

    public string? ClothsDescription { get; set; }

    public string? ClothsHandOverTo { get; set; }

    public string? BloodDescription { get; set; }

    public string? BloodHandOverTo { get; set; }

    public string? VaginalDescription { get; set; }

    public string? VaginalHandOverTo { get; set; }

    public string? OralDescription { get; set; }

    public string? OralHandOverTo { get; set; }

    public string? InvestigationAdvice { get; set; }

    public string? XrayReason { get; set; }

    public string? XrayReport { get; set; }

    public string? UltraSoundReason { get; set; }

    public string? UltraSoundReport { get; set; }

    public string? BloodReason { get; set; }

    public string? BloodReport { get; set; }

    public Guid? CounselingRefferalProfileId { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public byte ActionTypeId { get; set; }

    public virtual MlcsvinitialInfo? MlcsvinitialInfo { get; set; }
}
