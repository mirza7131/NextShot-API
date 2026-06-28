using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.MEAs.Domain.Models.DTO.Dashboard
{
    public class UserComplianceCoverageDTO
    {
        public int UserId { get; set; }
        public string? FullName { get; set; }
        public string? Username { get; set; }
        public string? DistrictName { get; set; }
        public string? ZoneName { get; set; }
        public decimal Compliance { get; set; }
        public int DaysWorked { get; set; }
        public decimal Coverage { get; set; }
    }
    public class GetMEAsCoverageAndComplianceDTO
    {
        public string? CompliancePercentage { get; set; }
        public List<UserComplianceCoverageDTO>? CoverageComplianceList { get; set; }
    }
}

