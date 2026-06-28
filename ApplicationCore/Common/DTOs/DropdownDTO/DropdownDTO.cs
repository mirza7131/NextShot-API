using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonDTOs.DropdownDTO
{
    public class CacheLabTestDto
    {
        public int LabTestId { get; set; }
        public string? Name { get; set; }
        public Guid? DepartmentProfileId { get; set; }
    }

}
