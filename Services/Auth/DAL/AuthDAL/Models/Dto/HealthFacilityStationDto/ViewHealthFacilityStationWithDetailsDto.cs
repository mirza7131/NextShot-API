using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDAL.Models.Dto.HealthFacilityStationDto
{
    public class ViewHealthFacilityStationWithDetailsDto
    {
        public Guid HealthFacilityStationId { get; set; }

        public Guid? StationProfileId { get; set; }

        public string? StationProfileName { get; set; }

        public short? SequenceNo { get; set; }

        public int? HealthFacilityId { get; set; }

        public string? HealthFacilityName { get; set; }

        public Guid? CreatedBy { get; set; }
        public string? CreatedByName { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid? UpdatedBy { get; set; }
        public string? UpdatedByName { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public bool IsActive { get; set; }
    }
}
