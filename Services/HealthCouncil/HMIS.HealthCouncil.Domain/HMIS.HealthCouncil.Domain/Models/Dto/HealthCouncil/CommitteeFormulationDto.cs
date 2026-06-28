using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.HealthCouncil.Domain.Models.Dto.HealthCouncil
{
    public class CommitteeFormulationDto
    {
        public Guid? CommitteeFormulationId { get; set; }

        public string? Name { get; set; }
        public string? Cnic { get; set; }

        public string? Designation { get; set; }

        public Guid? MeetingRoleProfileId { get; set; }

        public int? HealthFacilityId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid? CreatedBy { get; set; }
    }
}
