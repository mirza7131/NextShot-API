using System;
using System.Collections.Generic;

namespace HMIS.HealthCouncil.Domain.Models.DbModels;

public partial class MeetingDetail
{
    public Guid MeetingDetailId { get; set; }

    public Guid? MeetingCallId { get; set; }

    public string? MeetingMembers { get; set; }

    public string? MeetingDetails { get; set; }

    public string? MeetingAgenda { get; set; }

    public string? MeetingDecision { get; set; }

    public string? MeetingNo { get; set; }

    public DateTime? MeetingDate { get; set; }

    public string? PreviousMeetingRemarks { get; set; }

    public int? HealthFacilityId { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public bool? IsActive { get; set; }

    public byte? ActionTypeId { get; set; }

    public virtual ICollection<Expense> Expenses { get; } = new List<Expense>();
}
