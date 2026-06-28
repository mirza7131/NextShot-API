using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDAL.Models.Dto.HfDepartmentDto
{
    public class CreateOrEditHfDepartmentDto
    {
        public int? HfDepartmentId { get; set; }

        public int? HealthFacilityId { get; set; }

        public int? DepartmentLookupId { get; set; }

        public List<int>? SectionIds { get; set; }

        public bool IsActive { get; set; }
    }
}
