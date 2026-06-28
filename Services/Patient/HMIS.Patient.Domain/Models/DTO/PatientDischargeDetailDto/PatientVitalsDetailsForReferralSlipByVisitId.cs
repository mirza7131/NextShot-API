using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.PatientDischargeDetailDto
{
    public class PatientVitalsDetailsForReferralSlipByVisitIdDTO
    {
        public string? BPSystolic { get; set; }
        public string? BPDiaSystolic { get; set; }
        public string? Pulse { get; set; }
        public string? Temprature { get; set; }
        public string? Height { get; set; }
        public string? Weight { get; set; }
        public string? ResperatoryRate { get; set; }
    }
}
