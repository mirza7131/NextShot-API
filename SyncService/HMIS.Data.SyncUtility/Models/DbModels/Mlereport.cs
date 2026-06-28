using System;
using System.Collections.Generic;

namespace HMIS.Data.SyncUtility.Models.DbModels;

public partial class Mlereport
{
    public Guid MlereportId { get; set; }

    public Guid MlebasicInfoId { get; set; }

    public Guid PatientId { get; set; }

    public Guid PatientVisitId { get; set; }

    public Guid? PatientDiagnoseId { get; set; }

    public Guid? PatientStatusProfileId { get; set; }

    public int? HealthFacilityId { get; set; }

    public long? ReportCounts { get; set; }

    public bool? IsFinalReport { get; set; }

    public bool? IsActive { get; set; }

    public Guid? MlcManualReportTypeProfileId { get; set; }

    public Guid? MlcDrawImageTypeProfileId { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public byte? ActionTypeId { get; set; }

    public virtual MlebasicInfo MlebasicInfo { get; set; } = null!;
}
