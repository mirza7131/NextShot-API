using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.HealthCouncil.Domain.Models.Dto.HealthCouncil
{
    public class MeetingCategoryDto
    {
        public Guid MeetingDisscussedCategoryId { get; set; }

        public Guid? AccountHeadId { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }
    }
}
