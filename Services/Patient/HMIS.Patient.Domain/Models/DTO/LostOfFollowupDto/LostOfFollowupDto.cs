using HMIS.Patient.Domain.Models.DTO.PaginationDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.LostOfFollowupDto
{
    public class LostOfFollowupDto
    {
        public Guid PatientId { get; set; }
        public string HealthFacility { get; set; }
        public string MRNo { get; set; }
        public string PatientName { get; set; }
        public string PatientMobileNo { get; set; }
        public string CNIC { get; set; }
        public DateTime LastVisitDate { get; set; }
        public int TotalNumberOfCallHistory { get; set; }
    }
}
