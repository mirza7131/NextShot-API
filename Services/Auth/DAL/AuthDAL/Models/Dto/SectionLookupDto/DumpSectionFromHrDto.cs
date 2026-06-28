using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDAL.Models.Dto.SectionLookupDto
{
    public class DumpSectionFromHrDto
    {
        public int? HrId { get; set; }
        public bool IsActive { get; set; } = true;

    }

    public class DumpHfDepartmentSection
    {
        public int? HfHrId { get; set; }
        public int DepartmentLookupId { get; set; }

    }
}
