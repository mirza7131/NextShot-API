using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Dashboard.Domain.Models.DTO.SvrPcrPendingPatientsDto
{
    public class SvrPcrPendingPatientsDto
    {
        public Guid PatientId { get; set; }
        public string PatientName { get; set; }
        public string CNIC { get; set; }
        public string PatientContact { get; set; }
        public string MRNo { get; set; }
        public string ShortName { get; set; }
        public string HealthFacility { get; set; }
        public string? ProfileName { get; set; }
        public DateTime? SvrDate { get; set; }
        public DateTime? PcrDate { get; set; }
    }
}
