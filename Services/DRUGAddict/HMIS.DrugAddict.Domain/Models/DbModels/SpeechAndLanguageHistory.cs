using System;
using System.Collections.Generic;

namespace HMIS.DrugAddict.Domain.Models.DbModels;

public partial class SpeechAndLanguageHistory
{
    public Guid SpeechAndLanguageHistoryId { get; set; }

    public Guid? PatientId { get; set; }

    public Guid? PatientVisitId { get; set; }

    public Guid? PatientDiagnoseId { get; set; }

    public string? MotherLanguage { get; set; }

    public string? LanguageSpokenAtHome { get; set; }

    public string? LanguageSpokenAtSchool { get; set; }

    public string? ChildCommunicates { get; set; }

    public string? LanguageProblem { get; set; }

    public string? ChildVisitProfessional { get; set; }

    public string? ChildInteraction { get; set; }

    public string? OtherBehavioralProblem { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public byte? ActionTypeId { get; set; }
}
