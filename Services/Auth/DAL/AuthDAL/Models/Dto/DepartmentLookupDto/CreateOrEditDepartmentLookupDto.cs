using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDAL.Models.Dto.DepartmentLookupDto
{
    public class CreateOrEditDepartmentLookupDto
    {
        public int? DepartmentLookupId { get; set; }

        public string? Name { get; set; }

        public string? DisplayName { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; }

    }
}
