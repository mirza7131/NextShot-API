using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.MEAs.Domain.Models.DTO.Visits
{
    public class UserVisitDTO
    {
        public int UserVisitId { get; set; }
        public string DivisionId { get; set; }
        public string DistrictId { get; set; }
        public string TehsilId { get; set; }
        public int ZoneId { get; set; }
        public int userId { get; set; }
        public int HfId { get; set; }
        public int ShiftId { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
    }
}
