using System;
using System.Collections.Generic;

namespace HMIS.HealthCouncil.Domain.Models.DbModels;

public partial class MeetingExpendeture
{
    public Guid MeetingExpendetureId { get; set; }

    public Guid? MeetingDetailId { get; set; }

    public Guid? MeetingDisscussedCategoryId { get; set; }

    public Guid? AccountHeadId { get; set; }

    public string? ItemName { get; set; }

    public int? Quantity { get; set; }

    public decimal? PricePerUnit { get; set; }

    public decimal? EstimatedCost { get; set; }

    public int? HealthFacilityId { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public bool? IsActive { get; set; }

    public byte? ActionTypeId { get; set; }

    public virtual AccountHead? AccountHead { get; set; }
}
