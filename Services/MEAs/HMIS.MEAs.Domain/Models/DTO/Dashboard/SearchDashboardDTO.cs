using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.MEAs.Domain.Models.DTO.Dashboard
{
    public class SearchDashboardDTO : PagerDto
    {
        public string? DistrictCode { get; set; }
        public string? Designation { get; set; }
        public string? HfType { get; set; }
        public string? ShiftId { get; set; }
        public string? IndicatorType { get; set; }
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
        public string? GraphType { get; set; }
        public string? DistrictName { get; set; }
        public int month { get; set; }
        public int year { get; set; }
    }
}
   
