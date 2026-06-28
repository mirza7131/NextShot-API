using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.MEAs.Domain.Models.DTO.Settings
{
    public class MEAsUserVisitDTO
    {
        public string MEAsUserName { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string DistrictName { get; set; }
        public string ZoneName { get; set; }
        public string ContactNo { get; set; }
        public string Visit { get; set; }
        public string HealthFacilityName { get; set; }
        public string ShiftName { get; set; }
        public string ModeName { get; set; }
        public Boolean IsVisited { get; set; }
        public Boolean IsRepeat { get; set; }
        public Boolean IsSpecial { get; set; }

        public DateTime UpdatedDate1 { get; set; }

        public string UpdatedDate { get; set; }
    }
}
