using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.PatientOpenVisitDto
{
    public class VitalsPatientQueDto
    {
        public Guid? PatientVisitId { get; set; }
        public Guid? PatientId { get; set; }
        public string? Mrno { get; set; }
        public string? FirstName { get; set; }
        public string? TokenNo { get; set; }
    }
}
