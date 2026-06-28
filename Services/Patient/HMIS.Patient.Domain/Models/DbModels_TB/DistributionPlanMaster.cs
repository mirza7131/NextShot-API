using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class DistributionPlanMaster
{
    public int Id { get; set; }

    public string? Hfmiscode { get; set; }

    public bool? RecordStatus { get; set; }

    public string? Status { get; set; }

    public Guid? Guid { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? DeletedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<DistributionPlanDetail> DistributionPlanDetails { get; } = new List<DistributionPlanDetail>();
}
