using CommonMessages;
using HMIS.Dashboard.Domain.Models.DTO.PatientPrescriptionDto;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Dashboard.Domain.Models.DTO.PatientDiagnoseDto
{
    public class CreateOrEditPatientDiagnoseWithRespiratoryTherapyFormDto
    {
        public string FormType { get; set; } = CommonStringConstant.RespiratoryForm;

        public Guid? PatientDiagnoseId { get; set; }

        public Guid? PatientId { get; set; }

        public Guid? PatientVisitId { get; set; }
        public int? DepartmentId { get; set; }
        public int? SectionId { get; set; }
        public string? VisitTypeName { get; set; }
        public string? SignificantExaminationFindings { get; set; }
        public string? SignificantLabAndRadiologicalFindings { get; set; }
        public string? PresentingComplaint { get; set; }
        public bool? AnyComorbidity { get; set; }
        public string? FormData { get; set; }
        public string? ReferredDepartmentLookupId { get; set; }
        public string? ReferredSectionLookupId { get; set; }
        public string? PlanOfCareAtHome { get; set; }
        public List<string>? Diagnoses { get; set; }
        public List<string>? Comorbidity { get; set; }
        public List<string>? Modalities { get; set; }
        public DateTime? FollowupDate { get; set; }
    }


    public class CreateOrEditPatientDiagnoseWithRespiratoryTherapyFormIPDDto
    {
        public string FormType { get; set; } = CommonStringConstant.RespiratoryFormIPD;

        public Guid? PatientDiagnoseId { get; set; }

        public Guid? PatientId { get; set; }

        public Guid? PatientVisitId { get; set; }
        public int? DepartmentId { get;set;}
        public int? SectionId { get;set;}
        public string? VisitTypeName {get;set;}
        public string? SignificantExaminationFindings { get;set;}
        public string? SignificantLabAndRadiologicalFindings { get;set;}
        public string? PresentingComplaint { get;set;}
        public bool? AnyComorbidity { get;set;}
        public string? FeedingStatus {get;set;}
        public string? FormData { get; set; }
        public string? ReferredDepartmentLookupId {get;set;}
        public string? ReferredSectionLookupId {get;set;}
        public string? PatientHospitalizationDetails {get;set;}
        public string? VentilationStatus {get;set;}
        public string? Spontaneous {get;set;}
        public string? Mechanical { get; set; }
        public string? RespiratoryTherapyAfterDischarge { get; set; }
        public List<string>? Diagnoses { get; set; }
        public List<string>? Comorbidity { get; set; }
        public List<string>? Modalities { get; set; }
        public DateTime? FollowupDate { get; set; }
    }
}
