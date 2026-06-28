using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDAL.Models.Dto.EventDto
{
    public class ViewEventDto
    {
        public Guid EventId { get; set; }

        public Guid? EventTypeProfileId { get; set; }

        public string? Name { get; set; }

        public DateTime? StartDateTime { get; set; }

        public DateTime? EndDateTime { get; set; }

        public int? Days { get; set; }

        public int? ProvinceId { get; set; }

        public int? DivisionId { get; set; }

        public int? DistrictId { get; set; }

        public int? TehsilId { get; set; }

        public int? HealthFacilityId { get; set; }

        public int? DepartmentLookupId { get; set; }

        public int? SectionLookupId { get; set; }

        public bool IsActive { get; set; }
    }

    public class SPResponseEventListDto
    {
        public Guid EventId { get; set; }

        public string? Name { get; set; }

        public DateTime? StartDateTime { get; set; }

        public DateTime? EndDateTime { get; set; }

        public int? Days { get; set; }

        public int? HealthFacilityId { get; set; }
        public string? HealthFacility { get; set; }

        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
    }

    public class SPResponseEventListTotalCountDto
    {
        public int TotalRecord { get; set; }

    }
}
