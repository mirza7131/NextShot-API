using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Dashboard.Domain.Models.DTO.PatientDto
{
    public class ViewPatientIpdReceipt
    {
        public Guid PatientId { get; set; }
        public Guid PatientOpenVisitId { get; set; }
        public string? Mrno { get; set; }
        public string? PatientName { get; set; }
        public string? GurdianName { get; set; }
        public int? Age { get; set; }
        public DateTime? Dob { get; set; }

        public string? Gender { get; set; }
        public string? BloodGroupName { get; set; }

        public string? CNIC { get; set; }
        public string? ContactNo { get; set; }
        public string? TokenNo { get; set; }
        public string? Address { get; set; }
        public DateTime? NextVisitDate { get; set; }
        public DateTime? VisitDate { get; set; }
        public string? HealthFacilityName { get; set; }

        public string? CreatedbyName { get; set; }
        public DateTime? CreatedOn { get; set; }

        public string? UpdatedByName { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public string? DepartmentName { get; set; }
        public string? SectionName { get; set; }

    }
}
