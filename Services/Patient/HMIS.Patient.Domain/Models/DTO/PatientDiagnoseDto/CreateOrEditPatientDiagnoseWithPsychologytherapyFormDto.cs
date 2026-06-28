using CommonMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.PatientDiagnoseDto
{
    public class CreateOrEditPatientDiagnoseWithPsychologytherapyFormDto
    {
        public string FormType { get; set; } = CommonStringConstant.NutritionForm;

        public Guid? PatientDiagnoseId { get; set; }

        public Guid? PatientId { get; set; }

        public Guid? PatientVisitId { get; set; }
        public string? VisitTypeName { get; set; }

        public List<PsychologyDisorderDto>? PsychologyDisorder { get; set; }
        public List<PsychologyAssociatedDisorderDto>? PsychologyAssociatedDisorder { get; set; }
        public List<PsychologyModalitesDto>? PsychologyModalites { get; set; }


        public string? PresentingComplaint { get; set; }
        public string? historyOfPresentingComplaint { get; set;}
        public string? IsPastPsychiatric { get; set;}
        public string? PastPsychiatricId {get; set;}
        public string? PastPsychiatric {get; set;}
        public string? AnyComorbidity {get; set;}
        public string? PersonalAndFamilyHistory {get; set;}
        public string? MentalStatusExamination {get; set;}
        public string? Appearance {get; set;}
        public string? Orientation {get; set;}
        public string? Speech {get; set;}
        public string? ThroughProcess {get; set;}
        public string? ThroughContent {get; set;}
        public string? PerceptualProcess {get; set;}
        public string? Insight {get; set;}
        public string? Judgment {get; set;}
        public string? Mood {get; set;}
        public string? Affect {get; set;}
        public string? Memory {get; set;}
        public string? EstimatedIntellectualFunctioning {get; set;}
        public string? CognitiveDeficits {get; set;}
        public string? PsychologicalTestApplied {get; set;}
        public string? BackAnxietyInventory {get; set;}
        public string? BackDepressionInventory { get; set; }
        public int ConflictResponseScore {get; set;}
        public int NeutralResponseScore {get; set;}
        public int PositiveresponseScore {get; set;}
        public int TotalScore { get; set; }
        public string? Modalities {get; set;}
        public string? ModalityName { get; set;}
        public string? ModalitiesNameOther {get; set;}
        public string? HouseTreePersonTest { get; set; }
        public string? ScheduleofPsychotherapy {get; set;}
        public string? ScheduleofPsychotherapyOther { get; set; }
        public DateTime? FollowupDate { get; set; }
    }




    public class PsychologyDisorderDto
    {
        public Guid? PsychologyDisorderProfileId { get; set; }
        public string? Name { get; set; }
    }


    public class PsychologyAssociatedDisorderDto
    {
        public Guid? PsychologyAssociatedDisorderProfileId { get; set; }
        public string? Name { get; set; }
    }

    public class PsychologyModalitesDto
    {
        public Guid? PsychologyModalitiesProfileId { get; set; }
        public string? Name { get; set; }
    }
}
