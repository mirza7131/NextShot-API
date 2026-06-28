using System;
using System.Collections.Generic;

namespace HMIS.Dashboard.Domain.Models.TransDbModels;

public partial class MlcsvlocalExamination
{
    public Guid MlcsvlocalExaminationId { get; set; }

    public Guid? MlcsvinitialInfoId { get; set; }

    public Guid PatientId { get; set; }

    public Guid PatientVisitId { get; set; }

    public Guid? PatientDiagnoseId { get; set; }

    public Guid? PatientStatusProfileId { get; set; }

    public int? HealthFacilityId { get; set; }

    public string? Tears { get; set; }

    public string? Rupture { get; set; }

    public string? EvidenceBleed { get; set; }

    public string? EvidenceSeminal { get; set; }

    public string? Reference { get; set; }

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
