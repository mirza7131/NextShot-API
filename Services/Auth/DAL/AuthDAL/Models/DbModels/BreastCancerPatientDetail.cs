using System;
using System.Collections.Generic;

namespace AuthDAL.Models.DbModels;

public partial class BreastCancerPatientDetail
{
    public Guid BreastCancerPatientDetailId { get; set; }

    public Guid? PatientId { get; set; }

    public Guid? PatientVisitId { get; set; }

    public Guid? PatientDiagnoseId { get; set; }

    public string? CurrentAge { get; set; }

    public int? ScoreCurrentAge { get; set; }

    public string? MenarcheAge { get; set; }

    public int? ScoreMenarche { get; set; }

    public string? FirstLiveBirthAge { get; set; }

    public int? ScoreFirstLiveBirth { get; set; }

    public string? BreastFedAge { get; set; }

    public int? ScoreBreastFed { get; set; }

    public string? Nulliparity { get; set; }

    public int? ScoreNulliparity { get; set; }

    public string? KnownFamilyBreastCancer { get; set; }

    public int? ScoreFamilyBreastCancer { get; set; }

    public string? AtypicalHyperplasia { get; set; }

    public int? ScoreAtypicalHyperplasia { get; set; }

    public string? OralHarmoneTherapy { get; set; }

    public int? ScoreHarmoneTherapy { get; set; }

    public string? OtherCancers { get; set; }

    public int? ScoreOtherCancers { get; set; }

    public string? Cbcstatus { get; set; }

    public string? Pain { get; set; }

    public string? Lump { get; set; }

    public string? NippleDischarge { get; set; }

    public string? SkinChanges { get; set; }

    public string? AxillaryLump { get; set; }

    public string? UltraSoundComments { get; set; }

    public string? ProvisionalDiagnosis { get; set; }

    public string? ReferSurgeryDepartment { get; set; }

    public string? ReferHealthFacilityId { get; set; }

    public bool? Status { get; set; }

    public bool? IsDeleted { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public string? NoBreastfed { get; set; }

    public int? ScoreNoBreastfed { get; set; }

    public int? ScoreTotal { get; set; }

    public string? RiskStatus { get; set; }

    public DateTime? AssessmentDate { get; set; }

    public DateTime? Cbedate { get; set; }

    public DateTime? UltraSoundDate { get; set; }

    public string? AssessmentHealthFacility { get; set; }

    public string? CbehealthFacility { get; set; }

    public string? UltraSoundHealthFacility { get; set; }

    public string? IsPatientReceived { get; set; }

    public DateTime? PatientReceivedDate { get; set; }

    public string? UltraSoundFinding { get; set; }

    public string? ReferForUltraSound { get; set; }

    public Guid? PatientReferId { get; set; }

    public bool? IsReferToTeritaryCareHospital { get; set; }

    public string? MaritalStatus { get; set; }

    public DateTime? FollowUpDate { get; set; }

    public string? IsPregnant { get; set; }
}
