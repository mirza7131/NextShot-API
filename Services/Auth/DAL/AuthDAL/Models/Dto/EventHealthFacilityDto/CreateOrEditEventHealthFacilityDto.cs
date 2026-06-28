using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDAL.Models.Dto.EventHealthFacilityDto
{
    public class CreateOrEditEventHealthFacilityDto
    {
        public Guid? EventHealthFacilityId { get; set; }

        public int HealthFacilityId { get; set; }

        public Guid? EventId { get; set; }

        public DateTime? StartDateTime { get; set; }

        public DateTime? EndDateTime { get; set; }

        public bool IsActive { get; set; }
    }
}
