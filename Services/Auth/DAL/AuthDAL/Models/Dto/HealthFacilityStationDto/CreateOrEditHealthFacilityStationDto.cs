using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDAL.Models.Dto.HealthFacilityStationDto
{
    public class CreateOrEditHealthFacilityStationDto
    {
        public Guid? HealthFacilityStationId { get; set; }

        public Guid? StationProfileId { get; set; }

        public short? SequenceNo { get; set; }

        public int? HealthFacilityId { get; set; }

        public bool IsActive { get; set; }
    }
}
