using CommonMessages;
using HMIS.Dashboard.Domain.Models.DTO.PatientPrescriptionDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Dashboard.Domain.Models.DTO.PatientDiagnoseDto
{
    public class CreateOrEditPatientDiagnoseWithNutritionisttherapyFormDto
    {
        public CreateOrEditPatientDiagnoseWithNutritionisttherapyFormDto()
        {
            PatientPrescriptions = new List<CreateOrEditPatientPrescriptionDto>();
        }
        public string FormType { get; set; } = CommonStringConstant.NutritionForm;

        public Guid? PatientDiagnoseId { get; set; }

        public Guid? PatientId { get; set; }

        public Guid? PatientVisitId { get; set; }
        public string? VisitTypeName { get; set; }
        public string? BMI {get; set;}
        public string? IBW {get; set;}
        public string? Weight { get; set;}
        public string? Height { get; set;}
        public string? BMIName {get; set;}
        public string? PersonalHistoryProfileId {get; set;}
        public string? NutritionalRisk { get; set; }
        public string? PresentingComplaints { get; set;}
        public string? DietIntakeHistory {get; set;}
        public string? MalnutritionStatus { get; set; }
        public bool? AnyComorbidity { get; set; }
        public bool? NoSignificantFinding { get; set; }
        public List<string>? SignificantExaminationFindings { get; set; }
        public List<string>? Comorbidity { get; set; }
        public List<string>? Diagnoses { get; set; }
        public string? InvestigationAdvise { get; set; }
        public string? DietPlan { get; set; }
        public DateTime? FollowupDate { get; set; }
        public virtual ICollection<CreateOrEditPatientPrescriptionDto> PatientPrescriptions { get; set; }
    }





    public class CreateOrEditPatientDiagnoseWithNutritionisttherapyFormIPDDto
    {
        public CreateOrEditPatientDiagnoseWithNutritionisttherapyFormIPDDto()
        {
            PatientPrescriptions = new List<CreateOrEditPatientPrescriptionDto>();
        }
        public string FormType { get; set; } = CommonStringConstant.NutritionFormIPD;

        public Guid? PatientDiagnoseId { get; set; }

        public Guid? PatientId { get; set; }

        public Guid? PatientVisitId { get; set; }
        public string? AdmissionDiagnoses { get; set; }
        public string? FeedingStatus { get; set; }
        public string? BMI { get; set; }
        public string? IBW { get; set; }
        public string? BMIName { get; set; }
        public string? PersonalHistoryProfileId { get; set; }
        public string? NutrionalRiskName { get; set; }
        public string? DietIntakeHistory { get; set; }
        public string? MalnutritionStatus { get; set; }
        public string? AnyComorbidity { get; set; }
        public bool? NoSignificantFinding { get; set; }
        public List<string>? SignificantExaminationFindings { get; set; }
        public List<string>? Comorbidity { get; set; }
        public List<string>? Diagnoses { get; set; }
        public string? InvestigationAdvise { get; set; }
        public string? DietPlan { get; set; }
        public string? ReferredDepartmentLookupId { get; set; }
        public string? ReferredSectionLookupId { get; set; }
        public DateTime? FollowupDate { get; set; }
        public virtual ICollection<CreateOrEditPatientPrescriptionDto> PatientPrescriptions { get; set; }
    }




    public class Diagnosis
    {
        public string? Name { get; set; }
    }
}
