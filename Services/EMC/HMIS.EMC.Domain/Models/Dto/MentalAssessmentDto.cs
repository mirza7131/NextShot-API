using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.EMC.Domain.Models.Dto
{
    public class MentalAssessmentDto
    {
        public Guid? MentalAssessmentId { get; set; }
        public Guid? PsychalogicalTestProfileId { get; set; }
        public string? Answer { get; set; }
    }
}
