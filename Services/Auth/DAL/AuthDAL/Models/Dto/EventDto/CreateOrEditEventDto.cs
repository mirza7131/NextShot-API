using AuthDAL.Models.Dto.EventHealthFacilityDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDAL.Models.Dto.EventDto
{
    public class CreateOrEditEventDto
    {
        public CreateOrEditEventDto()
        {
            EventHealthFacilities = new List<CreateOrEditEventHealthFacilityDto>();
        }
        public Guid? EventId { get; set; }

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

        public List<CreateOrEditEventHealthFacilityDto> EventHealthFacilities { get; set; }
    }

    public class DeleteEventHealthFacilityDto {
        public Guid EventId { get; set; }
        public int? HealthFacilityId { get; set; }
    }
}
