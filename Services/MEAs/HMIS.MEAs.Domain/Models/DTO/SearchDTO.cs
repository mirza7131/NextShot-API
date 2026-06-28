using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.MEAs.Domain.Models.DTO
{
    public class SearchDTO : PagerDto
    {
        public string? ModuleId { get; set; }
        public string? divisionId { get; set; }
        public string? districtId { get; set; }
        public string? tehsilId { get; set; }
        public string? ZoneId { get; set; }
        public string? HfTypeId { get; set; }
        public string? ShiftId { get; set; }
        public DateTime? fromDate { get; set; }
        public DateTime? toDate { get; set; }
        public string? month { get; set; }
        public string? year { get; set; }
        public string? queryString { get; set; }
        public int type { get; set; }
        public string? modename { get; set; }
        public int MasterId { get; set; }


    }
}
