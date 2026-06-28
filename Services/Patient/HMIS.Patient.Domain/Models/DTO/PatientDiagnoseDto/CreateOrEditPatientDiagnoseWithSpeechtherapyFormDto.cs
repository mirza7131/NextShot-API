using CommonMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.PatientDiagnoseDto
{
    public class CreateOrEditPatientDiagnoseWithSpeechtherapyFormDto
    {
        public string FormType { get; set; } = CommonStringConstant.SpeechTherapyForm;

        public Guid? PatientDiagnoseId { get; set; }

        public Guid? PatientId { get; set; }

        public Guid? PatientVisitId { get; set; }
        public List<SpeechDisorderDto>? SpeechDisorder { get; set; }
        public List<AssociatedDisorderDto>? AssociatedDisorder { get; set; }
        public List<ModalitiesDto>? Modalities { get; set; }


        public string? VisitTypeName { get; set; }
        public string? PresentingComplaint { get; set; }

        public int? ReferredDepartmentLookupId { get; set; }

        public string? SpeechMilstoneNormalOrDelayed { get; set; }
        public string? DevelopmentMilstoneNormalOrDelayed { get; set; }
        public int? ReferredSectionLookupId { get; set; }
        public int? NeckHolding { get; set; }
        public int? Siting { get; set; }
        public int? Standing { get; set; }
        public int? Walking { get; set; }
        public int? Cooing { get; set; }
        public int? Babbling { get; set; }
        public int? SingleWord { get; set; }
        public int? PresentSpeechLevel { get; set; }
        public int? AgeOfWearingHearingAid { get; set; }
        public int? LevelOfHearingLoss { get; set; }
        public string? RelevantMedicalhistorySwallowing { get; set; }
        public string? NoDevelopmentMilestone { get; set; }
        public string? NoSpeechMilestone { get; set; } 
        public string? DisabilityInfamily { get; set; }
        public string? DisabilityName { get; set; }
        public string? ChildSchool { get; set; }
        public string? SchoolType { get; set; }
        public string? MotherLanguage { get; set; }
        public string? LanguageSpokenAtHome { get; set; }
        public string? LanguageSpokenAtSchool { get; set; }
        public string? ChildCommunicates { get; set; }
        public string? ChildCommunicatesOther { get; set; }
        public string? OtherDisabilityInfamily { get; set; }
        public string? LanguageProblem { get; set; }
        public string? LanguageProblemOther { get; set; }
        public string? OtherBehavioralProblem { get; set; }
        public string? ChildSchoolGrade { get; set; }
        public string? ChildVisitProfessional { get; set; }
        public string? ChildVisitProfessionalOther { get; set; }
        public string? ChildInteraction { get; set; }
        public string? ChildInteractionOther { get; set; }
        public string? HearingLoss { get; set; }
        public string? TypeOfHearingLoss { get; set; }
        public string? NatureOfHearingLoss { get; set; }
        public string? UseOfHearingAid { get; set; }
        public string? ConversationSpeech { get; set; }
        public string? ConversationSpeechOther { get; set; }
        public string? ReceptiveLanguage { get; set; }
        public string? ExpressiveLanguage { get; set; }
        public string? RelevantMedicalhistory { get; set; }
        public int? ConsistencySoundError { get; set; }
        public int? PattersSoundError { get; set; }
        public int? StimulabilitySoundError { get; set; }
        public int? Cognition { get; set; }
        public int? CurrentFeedingStatus { get; set; }
        public int? Dietarylimitations { get; set; }
        public int? CognitiveStatus { get; set; }
        public string? SignificantOralMotorFinding { get; set; }
        public string? TypeOfdysfluency { get; set; }
        public string? TypeOfdysfluencyOther { get; set; }
        public string? AssociatedMotorBehavior { get; set; }
        public string? AssociatedMotorBehaviorOther { get; set; }
        public string? AvoidanceOfSounds { get; set; }
        public string? StimulabilityOfFluentSpeech { get; set; }
        public string? ChildVoiceQuality { get; set; }
        public string? ChildVoiceQualityOther { get; set; }
        public string? ChildVoicePitch { get; set; }
        public string? ChildVoicePitchOther { get; set; }
        public string? ChildVoiceResonance { get; set; }
        public string? StimulabilityOfImprovedVoice { get; set; }
        public string? OralFacialExaminationFindings { get; set; }
        public string? Voiceimprovement { get; set; }
        public string? PreviousInterventionTherapy { get; set; }
        public string? PlanOfCare { get; set; }
        public string? PlanOfCareOther { get; set; }
        public string? ScheduleOfTherapyCarePlan { get; set; }
        public string? ScheduleOfTherapyCarePlanOther { get; set; }
        public DateTime? FollowupDate { get; set; }
        public string? SpeechTheripistName { get; set; }
    }


    public class Diagnoses
    {
        public string? Name{ get; set; }
    }

    public class SpeechDisorderDto
    {
       public Guid? SpeechDisorderProfileId { get; set; }
        public string? Name { get; set; }
    }


    public class AssociatedDisorderDto
    {
        public Guid? AssociatedDisorderProfileId { get; set; }
        public string? Name { get; set; }
    }

        public class ModalitiesDto
    {
        public Guid? SpeechModalitiesProfileId { get; set; }
        public string? Name { get; set; }
    }
}
