using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.PhysiotherapyFormDto
{
    public class ViewPhysiotherapyFormDto
    {
        public Guid PhysiotherapyFormId { get; set; }

        public Guid PatientDiagnoseId { get; set; }

        public string? PresentingComplaint { get; set; }

        public DateTime? ProblemSince { get; set; }

        public string? AnyComorbidity { get; set; }

        public string? DrugHistory { get; set; }

        public string? SignificantExaminationFindings { get; set; }

        public string? TotalDurationOfTreatmentSession { get; set; }

        public bool? DischargeFromPhysicalTherapyTreatment { get; set; }

        public bool? HomeExercisePlan { get; set; }

        public bool? TreatmentAtDepartment { get; set; }

        public string? Prognosis { get; set; }

        public string? ClinicalDiagnosis { get; set; }

        public string? PhysiotherapyDiagnosis { get; set; }

        public bool IsActive { get; set; }
    }
}
