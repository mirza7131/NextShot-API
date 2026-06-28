using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDAL.Models.Dto.DistrictDto
{
    public class CreateOrEditDistrictDto
    {
        public int? DistrictId { get; set; }

        public string? Code { get; set; }

        public string? Name { get; set; }

        public int? DivisionId { get; set; }

        public bool IsActive { get; set; }

    }
}
