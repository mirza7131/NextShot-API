using System;
using System.Collections.Generic;

namespace HMIS.MIMS.Domain.Models.DbModels;

public partial class SampleConsignment
{
    public Guid SampleConsignmentId { get; set; }

    public string? Title { get; set; }

    public int? ToHealthFacilityId { get; set; }

    public int? FromHealthFacilityId { get; set; }

    public byte? Status { get; set; }

    public string? StatusReason { get; set; }

    public bool IsActive { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public byte? ActionTypeId { get; set; }

    public string? BatchNo { get; set; }

    public virtual ICollection<SampleConsignmentDetail> SampleConsignmentDetails { get; } = new List<SampleConsignmentDetail>();
}
