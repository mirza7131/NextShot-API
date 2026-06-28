using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.HealthCouncil.Domain.Models.Dto.HealthCouncil
{
    public class MeetingDisscussedCategoriesDto
    {
        public Guid MeetingDisscussedCategoryId { get; set; }
        public Guid? MeetingDetailId { get; set; }
        public Guid? AccountHeadId { get; set; }
        public string? AccountHeadName { get; set; }
    }
}
