using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Dashboard.Domain.Models.DTO.TBDSR
{
    public class TBDSRDashboardReportDTO
    {
        public string? DivisionName { get; set; }
        public string? DistrictName { get; set; }
        public string? TehsilName { get; set; }
        public string? HealthFacilityTypeName { get; set; }
        public string? HealthFacility { get; set; }
        public int? HealthFacilityId { get; set; }
        public int? HealthFacilityTypeId { get; set; }
        public int? OneWindowTbVisits { get; set; }
        public int? Adults { get; set; }
        public int? Childs { get; set; }
        public int? NewConfirmed { get; set; }
        public int? FollowupConfirmed { get; set; }
        public int? TestAdvised { get; set; }
        public int? SsmAdvised { get; set; }
        public int? SsmConduct { get; set; }
        public int? SsmPositive { get; set; }
        public int? CXRAdvised { get; set; }
        public int? CXRConduct { get; set; }
        public int? CXRPositive { get; set; }
        public int? XpertAdvised { get; set; }
        public int? XpertConduct { get; set; }
        public int? XpertPositive { get; set; }
        public int? MDRDetected { get; set; }
        public int? HIVAdvised { get; set; }
        public int? HIVConduct { get; set; }
        public int? HIVReactive { get; set; }
        public int? MedicineIssued { get; set; }
    }
    public class GetProfilesByShortNameDto
    {
        public string? ProfileName { get; set;}
        public string? ShortName { get; set; }
    }
}
