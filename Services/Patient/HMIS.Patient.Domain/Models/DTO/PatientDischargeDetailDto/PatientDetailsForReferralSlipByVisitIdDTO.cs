using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.PatientDischargeDetailDto
{
    public class PatientDetailsForReferralSlipByVisitIdDTO
    {
        public string? Name { get; set; }
        public string? MRNo { get; set; }
        public int? Age { get; set; }
        public string? Gender { get; set; }
        public string? MobileNo { get; set; }
        public string? ReasonForReferral { get; set; }
        public string? ReferredToDepartment { get; set; }
        public string? ReferredToSection { get; set; }
        public string? ReferredBy { get; set; }
        public string? ReferredByDesignation { get; set; }
        public DateTime? ReferredOn { get; set; }
        public string? ReferToHealthFacility { get; set; }
        public string? Diseases { get; set; }
        public List<PatientVitalsDetailsForReferralSlipByVisitIdDTO>? Vitals { get; set; } = new List<PatientVitalsDetailsForReferralSlipByVisitIdDTO>();
    }
}
