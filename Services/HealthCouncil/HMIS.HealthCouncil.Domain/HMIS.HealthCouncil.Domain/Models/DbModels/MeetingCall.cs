using System;
using System.Collections.Generic;

namespace HMIS.HealthCouncil.Domain.Models.DbModels;

public partial class MeetingCall
{
    public Guid MeetingCallId { get; set; }

    public string? NotificationNo { get; set; }

    public DateTime? NotificationDate { get; set; }

    public DateTime? MeetingDate { get; set; }

    public string? MeetingAgenda { get; set; }

    public int? HealthfacilityId { get; set; }

    public string? MeetingMembers { get; set; }

    public bool? IsMeetingDone { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public bool? IsActive { get; set; }

    public byte? ActionTypeId { get; set; }
}
