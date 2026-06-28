using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.HealthCouncil.Domain.Models.Dto.HealthCouncil
{
    public class MeetingDetailDto
    {
        public MeetingDetailDto()
        {
            accountHeads = new List<MeetingCategoryDto>();
            expenditures = new List<Expenditures>();
        }
        public Guid? MeetingDetailId { get; set; }
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

        public virtual ICollection<MeetingCategoryDto>? accountHeads { get; set; } = new List<MeetingCategoryDto>();
        public virtual List<Expenditures>? expenditures { get; set; } = new List<Expenditures>();
    }

    public class Expenditures
    {
        public Guid? AccountHeadId { get; set; }
        public Guid? MeetingDetailId { get; set; }
        public string? Name { get; set; }
        public virtual ICollection<MeetingExpendetureDto>? Expendetures { get; set; } = new List<MeetingExpendetureDto>();
    }
}
