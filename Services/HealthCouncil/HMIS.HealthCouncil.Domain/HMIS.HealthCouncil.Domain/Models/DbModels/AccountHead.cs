using System;
using System.Collections.Generic;

namespace HMIS.HealthCouncil.Domain.Models.DbModels;

public partial class AccountHead
{
    public Guid AccountHeadId { get; set; }

    public string? Name { get; set; }

    public bool? IsActive { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public byte ActionTypeId { get; set; }

    public virtual ICollection<MeetingDisscussedCategory> MeetingDisscussedCategories { get; } = new List<MeetingDisscussedCategory>();

    public virtual ICollection<MeetingExpendeture> MeetingExpendetures { get; } = new List<MeetingExpendeture>();
}
