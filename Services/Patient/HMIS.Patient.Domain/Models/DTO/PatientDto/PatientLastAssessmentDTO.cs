using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.PatientDto
{
    public class PatientLastAssessmentDTO
    {
        public Guid? PatinetId { get; set; }

        public string? AssessmentType { get; set; }

        public Guid? PatientVisitId { get; set; }
        public string? TreatmentOutCome { get; set; }

        public DateTime? Createdon { get; set; }
    }
}