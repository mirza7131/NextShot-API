using System;
using System.Collections.Generic;

namespace HMIS.EMC.Domain.Models.DbModels;

public partial class Mlcsvreport
{
    public Guid MlcsvreportId { get; set; }

    public Guid? MlcsvinitialInfoId { get; set; }

    public Guid PatientId { get; set; }

    public Guid PatientVisitId { get; set; }

    public Guid? PatientDiagnoseId { get; set; }

    public Guid? PatientStatusProfileId { get; set; }

    public int? HealthFacilityId { get; set; }

    public string? NatureOfInjuries { get; set; }

    public string? DurationOfInjuries { get; set; }

    public string? KindOfWeaponUse { get; set; }

    public string? Treatment { get; set; }

    public string? Notes { get; set; }

    public string? KuoinjuryNote { get; set; }

    public string? FinalOpinion { get; set; }

    public Guid? ManualReportImageId { get; set; }

    public Guid? FinalReportImageId { get; set; }

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
