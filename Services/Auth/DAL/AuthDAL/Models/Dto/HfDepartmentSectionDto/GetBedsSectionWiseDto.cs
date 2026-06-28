using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDAL.Models.Dto.HfDepartmentSectionDto
{
    public class GetBedsSectionWiseDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? Occupied { get; set; } = 0; 
    }
}
