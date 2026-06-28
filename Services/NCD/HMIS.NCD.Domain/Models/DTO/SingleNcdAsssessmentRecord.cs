using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.NCD.Domain.Models.DTO
{
    public class SingleNcdAsssessmentRecord
    {
        public Guid NcdAssessmentAnswersId { get; set; }
        public Guid PatientId { get; set; }
        public Guid PatientVisitId { get; set; }
        public Guid ProfileId { get; set; }
        public int HealthFacilityId { get; set; }
        public string Answer { get; set; }
        public string ShortName { get; set; }
        public string Name { get; set; }
        public string ProfileTypeShortName { get; set; }
    }
}
