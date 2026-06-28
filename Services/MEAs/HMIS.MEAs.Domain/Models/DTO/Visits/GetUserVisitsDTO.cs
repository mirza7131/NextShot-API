using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.MEAs.Domain.Models.DTO.Visits
{
    public class GetUserVisitsDTO
    {
        public string CurrentMonth { get; set; }
        public string CurentYear { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string ZoneName { get; set; }
        public string ContactNo { get; set; }
        public bool IsVisited { get; set; }
        public bool IsRepeat { get; set; }
        public bool IsSpecial { get; set; }
        public string HealthFacilityName { get; set; }
        public string ShiftName { get; set; }
        public string ModeName { get; set; }
    }
}
