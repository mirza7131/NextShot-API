using CommonMessages;
using HMIS.Dashboard.Domain.Models.DTO.PatientPrescriptionDto;
using HMIS.Dashboard.Domain.Models.DTO.PhysiotherapyFormDto;
using HMIS.Dashboard.Domain.Models.DTO.PhysiotherapyModalityDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Dashboard.Domain.Models.DTO.PatientDiagnoseDto
{
    public class CreateOrEditPatientDiagnoseWithPhysiotherapyFormDto
    {
        public CreateOrEditPatientDiagnoseWithPhysiotherapyFormDto()
        {
            PhysiotherapyModalities = new List<CreateOrEditPhysiotherapyModalityDto>();
            HomeExercisePlanDropdown = new List<HomeExercisePlanDTO>();
            

            //PatientPrescriptions = new List<CreateOrEditPatientPrescriptionDto>();
        }
        public string FormType { get; set; } = CommonStringConstant.GeneralForm;

        public Guid? PatientDiagnoseId { get; set; }

        public Guid? PhysiotherapyFormId { get; set; }

        public Guid PatientId { get; set; }

        public Guid PatientVisitId { get; set; }


        public Guid? VisitTypeProfileId { get; set; }

        public int? ReferredDepartmentLookupId { get; set; }

        public int? ReferredSectionLookupId { get; set; }

        public Guid? ReferredBy { get; set; }


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

        public string? KeyTreatment { get; set; }

        public string? PlanOfCare { get; set; }

        public string? FrequencyOfExercise { get; set; }

        public string? IntensityOfExercise { get; set; }

        public string? TypeOfExercise { get; set; }

        public Guid? DischargePlanOfCareProfileId { get; set; }

        public DateTime? FollowupDate { get; set; }

        public int? DocDepartmentLookupId { get; set; }

        public int? DocSectionLookupId { get; set; }

        public Guid? DiagnosedBy { get; set; }

        public bool IsActive { get; set; }

        public bool IsMedicine { get; set; }

        public bool IsLab { get; set; }

        public bool IsRefer { get; set; }

        public bool IsVisitClose { get; set; }

        public bool IsWillingToBuyMedPrivately { get; set; }

        public virtual ICollection<CreateOrEditPhysiotherapyModalityDto> PhysiotherapyModalities { get; set; }
        public virtual ICollection<HomeExercisePlanDTO> HomeExercisePlanDropdown { get; set; }


        //public virtual ICollection<CreateOrEditPatientPrescriptionDto> PatientPrescriptions { get; set; }
    }
}

public class HomeExercisePlanDTO
{
    public Guid? HomeExersicePlanProfileId { get; set; }
    public string? Name { get; set; }
}
