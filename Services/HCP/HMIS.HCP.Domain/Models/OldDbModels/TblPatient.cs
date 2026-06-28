using System;
using System.Collections.Generic;

namespace HMIS.HCP.Domain.Models.OldDbModels;

public partial class TblPatient
{
    public int Id { get; set; }

    public string? RegNo { get; set; }

    public string? OldRegNo { get; set; }

    public string? MrnNo { get; set; }

    public string? PatientName { get; set; }

    public string? Lname { get; set; }

    public string? FatherName { get; set; }

    public DateTime? PatientDob { get; set; }

    public float? PatientAge { get; set; }

    public string PatientType { get; set; } = null!;

    public string? RelationContact { get; set; }

    public string? CnicStatus { get; set; }

    public string? SelfCnic { get; set; }

    public string? Passport { get; set; }

    public string? NextOfKin { get; set; }

    public string? NextOfKinCnic { get; set; }

    public string? NoCnicReason { get; set; }

    public string? ContactNoSelf { get; set; }

    public byte[]? OtherContactno { get; set; }

    public int? Gender { get; set; }

    public string? GenderName { get; set; }

    public string? AddressAvailable { get; set; }

    public string? PostalAddress { get; set; }

    public int? Division { get; set; }

    public int? District { get; set; }

    public int? Tehsil { get; set; }

    public int? Hospital { get; set; }

    public int? Created { get; set; }

    public int? NextStatus { get; set; }

    public int? Updated { get; set; }

    public int? NextStatusUpdated { get; set; }

    public string? PreviousHbv { get; set; }

    public string? PreviousHcv { get; set; }

    public string? PcrConfirmationHbv { get; set; }

    public string? PcrConfirmationHcv { get; set; }

    public string? VacinationCompleted { get; set; }

    public int? MaritalStatus { get; set; }

    public int Occupation { get; set; }

    public int Qualification { get; set; }

    public int? UserId { get; set; }

    public int? UserHospital { get; set; }

    public string? CallcenterId { get; set; }

    public string? IsReferal { get; set; }

    public string? CompletedVacinationHbv { get; set; }

    public string IsRegister { get; set; } = null!;

    public string IsVital { get; set; } = null!;

    public string IsAssesment { get; set; } = null!;

    public string IsTreatment { get; set; } = null!;

    public string IsVacinate { get; set; } = null!;

    public string IsSample { get; set; } = null!;

    public string IsSvrSample { get; set; } = null!;

    public string IsRefered { get; set; } = null!;

    public string? Vaccinate { get; set; }

    public string? CollectSample { get; set; }

    public string? IsClosed { get; set; }

    public string? IsConseledNClosed { get; set; }

    public string? Labno { get; set; }

    public string? Pcrreq { get; set; }

    public int? CallCenterId1 { get; set; }

    public string IsTypeChange { get; set; } = null!;

    public string? IsHealthWeekPatient { get; set; }

    public string IsMedicineDelivered { get; set; } = null!;

    public string IsDischarge { get; set; } = null!;

    public string IsDoorstep { get; set; } = null!;

    public string IsHfc { get; set; } = null!;

    public string IsInvalidAddress { get; set; } = null!;

    public long? ConsignmentNo { get; set; }

    public string ConsignmentBatch { get; set; } = null!;

    public string? MedicineDeliveryStatus { get; set; }

    public string CloseCase { get; set; } = null!;

    public int NoOfMedicineDelivered { get; set; }

    public string IsLegacyData { get; set; } = null!;

    public string TcsSelected { get; set; } = null!;

    public string? IsFollowUpOn { get; set; }

    public string IsHideTcs { get; set; } = null!;

    public string? Latitude { get; set; }

    public string? Longitude { get; set; }

    public string IsHcvDemote { get; set; } = null!;

    public string IsHbvDemote { get; set; } = null!;

    public int? NoOfFollowups { get; set; }

    public int? HbvInvestigationStatus { get; set; }

    public int? XFactor { get; set; }

    public int? YFactor { get; set; }

    public int? ZFactor { get; set; }

    public int AFactor { get; set; }

    public int BFactor { get; set; }

    public int? CFactor { get; set; }

    public int? DFactor { get; set; }

    public int? EFactor { get; set; }

    public int? FFactor { get; set; }

    public string IsOldRegime { get; set; } = null!;

    public string IsOldRegimeTcs { get; set; } = null!;

    public string IsCirrhoticPatient { get; set; } = null!;

    public int? HcvInvestigationStatus { get; set; }

    public int NoOfHbvMedicineDelivered { get; set; }

    public int NoOfHbvFollowups { get; set; }

    public int? NoOfHcvFollowups { get; set; }

    public int? NoOfHcvMedicineDelivered { get; set; }

    public string? IsSvrRecommended { get; set; }

    public DateTime? SvrDate { get; set; }

    public string IsMedicineDisbursFormSubmitted { get; set; } = null!;

    public int? HcvMedicineDuration { get; set; }

    public int? HbvMedicineDuration { get; set; }

    public int? HcvFristMedicineDate { get; set; }

    public int? HbvFirstMedicineDate { get; set; }

    public string? HbvMedicineName { get; set; }

    public string? HcvMedicineName { get; set; }

    public int? HcvLastFollowup { get; set; }

    public int? HbvLastFollowup { get; set; }

    public int? NoOfHcvMedicineDelivered2 { get; set; }

    public int? NoOfHcvFollowups2 { get; set; }

    public int? HcvMedicineDuration2 { get; set; }

    public int? HcvFristMedicineDate2 { get; set; }

    public int? OldRegimeFirstBaselineDate { get; set; }

    public int? OldRegimeLastFollowupDate { get; set; }

    public string? IsSvrFormSubmitted { get; set; }

    public string? IsLegacySvr { get; set; }

    public string IsTerminate { get; set; } = null!;

    public string? PatTransitBit { get; set; }

    public string Bbl { get; set; } = null!;

    public string Cbl { get; set; } = null!;

    public string IsIgnore { get; set; } = null!;

    public string? BaselineResultType { get; set; }

    public string? BaselineLabName { get; set; }

    public string? OtherLabName { get; set; }

    public string? TreatmentHistory { get; set; }

    public string? TreatmentOptions { get; set; }

    public string? PatientFrom { get; set; }

    public int? MicroId { get; set; }

    public string? ScreeningSampleResult { get; set; }

    public string? IsEditAssessment { get; set; }

    public string IsAllMedDeliveredFrmBaseline { get; set; } = null!;

    public string IsMedRecUpdate { get; set; } = null!;

    public string? DivisionName { get; set; }

    public string? DistrictName { get; set; }

    public string? HfName { get; set; }

    public string? RapidTesting { get; set; }

    public string? EditPatientStatus { get; set; }

    public string? IsPatientTransferStatus { get; set; }

    public int? PcrSampleCollectionPendingHospitalId { get; set; }

    public int? HfSampleCollectedHospitalId { get; set; }

    public int? PrevUserHospital { get; set; }

    public int? IsSvrEligibleHospital { get; set; }

    public string IsGiReferred { get; set; } = null!;

    public string IsGiReceived { get; set; } = null!;

    public string? HbvScreeningResult { get; set; }

    public string? HcvScreeningResult { get; set; }

    public string? Vaccination { get; set; }

    public string? NoOfVaccinationDosesGiven { get; set; }

    public string? SampleCollectedNumber { get; set; }

    public int? TotalSampleCollected { get; set; }

    public string? FlagOfSvrSample { get; set; }

    public string? SampleStage { get; set; }

    public string? SampleResult { get; set; }

    public string? TreatmentCompletedHcv { get; set; }

    public string? TreatmentCompletedHbv { get; set; }

    public string? EligibleForSvr { get; set; }

    public string? SvrRecommended { get; set; }

    public string? SvrSampleCollectedNumber { get; set; }

    public string? SvrSampleResult { get; set; }

    public DateTime? SampleCollectedDate { get; set; }

    public DateTime? BatchCompletionDate { get; set; }

    public string? PatientStage { get; set; }

    public string? NextOfKinRelation { get; set; }

    public string? TehsilName { get; set; }

    public string? SampleAcceptedDate { get; set; }

    public DateTime? ScreeningDate { get; set; }

    public string? ScreeningStatus { get; set; }

    public string? SvrAlreadyDone { get; set; }

    public string? SampleTestType { get; set; }

    public string? ViralCount { get; set; }

    public string? Hemoglobin { get; set; }

    public string? Ast { get; set; }

    public string? Alt { get; set; }

    public string? Platelet { get; set; }

    public string? Tlc { get; set; }

    public string? Apri { get; set; }

    public string? Urea { get; set; }

    public string? Creatinine { get; set; }

    public string? BloodSugarRandom { get; set; }

    public string? Pulse { get; set; }

    public string? Systolic { get; set; }

    public string? Diastolic { get; set; }

    public string? Weight { get; set; }

    public int? HcvLastFollowupDate6Report { get; set; }

    public int? HcvLastFollowupDate3Report { get; set; }

    public int? NoOfDosesTaken { get; set; }

    public int? DoseEligibility { get; set; }

    public string? CurrentHospitalName { get; set; }

    public string? ExHospitalName { get; set; }

    public int? VaccinationDoseDate1 { get; set; }

    public int? VaccinationDoseDate2 { get; set; }

    public int? VaccinationDoseDate3 { get; set; }

    public string? PatientAge80 { get; set; }

    public string? FingerPrint1 { get; set; }

    public string? FingerPrint2 { get; set; }

    public string? IsPregnant { get; set; }

    public DateTime? DodDate { get; set; }

    public string? IsReRegister { get; set; }

    public int? LostFollowupId { get; set; }

    public string? IsLmpDate { get; set; }

    public string? IsBloodBankPatient { get; set; }

    public string? IsRegCompleted { get; set; }

    public int? BblUserId { get; set; }

    public string? IsPatientPrison { get; set; }

    public string? IsAnnualPcr { get; set; }

    public byte[]? FingerPrintBlob { get; set; }

    public string? PrisonTransferStatus { get; set; }

    public int? PrisonType { get; set; }

    public string? IsPrisonRelease { get; set; }

    public int? FamilyAssessmentId { get; set; }

    public string? BbScreeningMethod { get; set; }

    public int? NoOfHbvMedicineCycles { get; set; }
}
