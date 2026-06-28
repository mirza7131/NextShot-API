using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDAL.Models.Dto.SectionLookupDto
{
    public class CreateOrEditSectionLookupDto
    {
        public int? SectionLookupId { get; set; }

        public int DepartmentLookupId { get; set; }

        public string? Name { get; set; }

        public string? DisplayName { get; set; }

        public string? Description { get; set; }
        public string? FormType { get; set; }

        public int? HrWardId { get; set; }

        public bool IsActive { get; set; }

        public bool IsConsultant { get; set; }
        public bool IsFilterClinic { get; set; }
        public int? MimsWardId { get; set; }
        public int? ConsultantSectionLookupId { get; set; }
    }
}
