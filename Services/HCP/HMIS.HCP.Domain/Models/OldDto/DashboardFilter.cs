using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.HCP.Domain.Models.OldDto
{
    public class DashboardFilter : UserLevelFilterDto
    {
        public string? User { get; set; }
        public int? DepartmentId { get; set; }
        public int? SectionId { get; set; }

    }
}
