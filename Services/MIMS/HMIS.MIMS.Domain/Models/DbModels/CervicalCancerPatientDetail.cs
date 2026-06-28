using System;
using System.Collections.Generic;

namespace HMIS.MIMS.Domain.Models.DbModels;

public partial class CervicalCancerPatientDetail
{
    public Guid CervicalCancerPatientDetailId { get; set; }

    public Guid? PatientId { get; set; }

    public Guid? PatientVisitId { get; set; }

    public Guid? PatientDiagnoseId { get; set; }

    public string? MarriedAge { get; set; }

    public int? ScoreMarriedAge { get; set; }

    public string? Noofchildren { get; set; }

    public int? ScoreNoofchildren { get; set; }

    public string? Oralcontraceptive { get; set; }

    public int? ScoreOralcontraceptive { get; set; }

    public string? Smoking { get; set; }

    public int? ScoreSmoking { get; set; }

    public string? Morethanonemarriage { get; set; }

    public int? ScoreMorethanonemarriage { get; set; }

    public string? Postcoitalbleeding { get; set; }

    public int? ScorePostcoitalbleeding { get; set; }

    public int? ScoreTotal { get; set; }

    public string? RiskStatus { get; set; }

    public string? AssessmentHealthFacility { get; set; }

    public DateTime? AssessmentDate { get; set; }

    public string? SpeculumExamination { get; set; }

    public string? VisualInspectionbyAceticAcid { get; set; }

    public string? ConsunForFpscreen { get; set; }

    public string? ExamHealthFacility { get; set; }

    public DateTime? ExamDate { get; set; }

    public bool Status { get; set; }

    public bool? IsDeleted { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public string? Referfortertiaryhospital { get; set; }

    public string? IsPatientReceived { get; set; }

    public DateTime? PatientReceivedDate { get; set; }

    public string? LocationOfAcctowhite { get; set; }

    public string? CryotherapyApplied { get; set; }

    public bool? IsPapSmearPerformed { get; set; }

    public DateTime? PapSmearPerformedDate { get; set; }

    public string? PapSmearTestResult { get; set; }

    public bool? ReferToTurtiaryHospital { get; set; }

    public DateTime? PapSmearResultDate { get; set; }

    public DateTime? CryotherapyAppliedDate { get; set; }

    public bool PapSmearReceived { get; set; }

    public DateTime? PapSmearReceivedDate { get; set; }

    public string? SpecimenType { get; set; }

    public string? SpecimenAdequacy { get; set; }

    public string? QualityIndicators { get; set; }

    public string? GeneralCate { get; set; }

    public string? Epithelialcellabnomalities { get; set; }

    public string? SampleBarcode { get; set; }

    public bool? IsActive { get; set; }

    public byte? ActionTypeId { get; set; }

    public Guid? PatientReferId { get; set; }
}
