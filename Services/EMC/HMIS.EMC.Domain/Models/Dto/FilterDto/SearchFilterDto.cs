using HMIS.EMC.Domain.Models.Dto.PaginationDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.EMC.Domain.Models.Dto.FilterDto
{
    public class SearchFilterDto : PagerDto
    {
        public Guid? DoctorId { get; set; }
        public int HealthFacilityId { get; set; }
    }
}
