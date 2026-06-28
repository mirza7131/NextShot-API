using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDAL.Models.Dto.HealthFacilityTypeDto
{
    public class ViewHealthFacilityTypeDto
    {
        public int HealthFacilityTypeId { get; set; }

        public string? Name { get; set; }

        public string? Code { get; set; }

        public int? HealthFacilityCategoryId { get; set; }

        public bool IsActive { get; set; }
    }
}
