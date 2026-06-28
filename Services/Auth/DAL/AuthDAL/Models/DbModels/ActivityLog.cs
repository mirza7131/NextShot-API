using System;
using System.Collections.Generic;

namespace AuthDAL.Models.DbModels;

public partial class ActivityLog
{
    public Guid ActivityLogId { get; set; }

    public int? HealthFacilityId { get; set; }

    public Guid? PatientId { get; set; }

    public Guid? PatientOpenVisitId { get; set; }

    public Guid? PatientDiagnoseId { get; set; }

    public string? Activity { get; set; }

    public bool? IsActive { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public byte ActionTypeId { get; set; }
}
