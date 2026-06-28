using System;
using System.Collections.Generic;

namespace AuthDAL.Models.DbModelsReporting;

public partial class PsychologicalAssessment
{
    public Guid PsychologicalAssessmentId { get; set; }

    public Guid? PatientId { get; set; }

    public Guid? PatientVisitId { get; set; }

    public Guid? PatientDiagnoseId { get; set; }

    public string? MentalStatusExamination { get; set; }

    public string? Appearance { get; set; }

    public string? Orientation { get; set; }

    public string? Speech { get; set; }

    public string? ThroughProcess { get; set; }

    public string? ThroughContent { get; set; }

    public string? PerceptualProcess { get; set; }

    public string? Insight { get; set; }

    public string? Judgment { get; set; }

    public string? Mood { get; set; }

    public string? Affect { get; set; }

    public string? Memory { get; set; }

    public string? EstimatedIntellectualFunctioning { get; set; }

    public string? CognitiveDeficits { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public byte? ActionTypeId { get; set; }
}
