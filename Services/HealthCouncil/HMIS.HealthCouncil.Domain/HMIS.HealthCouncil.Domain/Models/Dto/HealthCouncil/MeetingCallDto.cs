using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.HealthCouncil.Domain.Models.Dto.HealthCouncil
{
    public class MeetingCallDto
    {
        public Guid? MeetingCallId { get; set; }

        public string? NotificationNo { get; set; }
        public string? MeetingAgenda { get; set; }

        public DateTime? NotificationDate { get; set; }

        public DateTime? MeetingDate { get; set; }

        public int? HealthFacilityId { get; set; }

        public string? MeetingMembers { get; set; }
        public bool? IsMeetingDone { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }
    }
}
