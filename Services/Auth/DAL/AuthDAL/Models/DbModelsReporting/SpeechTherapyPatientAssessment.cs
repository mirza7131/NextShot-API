using System;
using System.Collections.Generic;

namespace AuthDAL.Models.DbModelsReporting;

public partial class SpeechTherapyPatientAssessment
{
    public Guid SpeechTherapyPatientAssessmentId { get; set; }

    public Guid? PatientId { get; set; }

    public Guid? PatientVisitId { get; set; }

    public Guid? PatientDiagnoseId { get; set; }

    public string? ConversationSpeech { get; set; }

    public string? ConsistencySoundError { get; set; }

    public int? PattersSoundError { get; set; }

    public string? StimulabilitySoundError { get; set; }

    public string? ReceptiveLanguage { get; set; }

    public string? ExpressiveLanguage { get; set; }

    public int? Cognition { get; set; }

    public string? RelevantMedicalhistorySwallowing { get; set; }

    public int? CurrentFeedingStatus { get; set; }

    public int? Dietarylimitations { get; set; }

    public int? CognitiveStatus { get; set; }

    public string? SignificantOralMotorFinding { get; set; }

    public string? TypeOfdysfluency { get; set; }

    public string? AssociatedMotorBehavior { get; set; }

    public string? AvoidanceOfSounds { get; set; }

    public string? StimulabilityOfFluentSpeech { get; set; }

    public string? ChildVoiceQuality { get; set; }

    public string? ChildVoicePitch { get; set; }

    public string? ChildVoiceResonance { get; set; }

    public string? StimulabilityOfImprovedVoice { get; set; }

    public string? OralFacialExaminationFindings { get; set; }

    public string? Voiceimprovement { get; set; }

    public string? PreviousInterventionTherapy { get; set; }

    public string? PlanOfCare { get; set; }

    public string? ScheduleOfTherapyCarePlan { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public byte? ActionTypeId { get; set; }
}
