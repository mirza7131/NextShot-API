using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class PatientsDatum
{
    public int Id { get; set; }

    public double? SrNo { get; set; }

    public string? Name { get; set; }

    public string? Cnic { get; set; }

    public string? Gender { get; set; }

    public string? PatientType { get; set; }

    public string? Address { get; set; }

    public string? HealthFacilityDivision { get; set; }

    public string? HealthFacilityDistrict { get; set; }

    public string? HealthFacilityTehsil { get; set; }

    public string? HealthFacilityName { get; set; }

    public DateTime? PatientReportingDate { get; set; }

    public string? PhoneNumber { get; set; }

    public string? EmrRegistrationNo { get; set; }

    public string? DepartmentRegistrationNo { get; set; }

    public string? CnicType { get; set; }

    public string? CnicGuardianRelation { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? City { get; set; }

    public string? Town { get; set; }

    public string? Street { get; set; }

    public string? HouseNo { get; set; }

    public string? DistrictCode { get; set; }

    public string? DivisionCode { get; set; }

    public string? TehsilCode { get; set; }

    public string? FatherName { get; set; }

    public string? Occupation { get; set; }

    public string? MaritalStatus { get; set; }

    public int? NoOfChildren { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreationDate { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public bool? RecordStatus { get; set; }

    public int? ProgramId { get; set; }

    public Guid? Guid { get; set; }

    public string? HealthFacilityCode { get; set; }

    public string? PatientSource { get; set; }

    public bool? IsHealthCardIssued { get; set; }

    public string? GuardianName { get; set; }

    public string? GuardianPhoneNumber { get; set; }

    public string? Status { get; set; }

    public int? UnionCouncilId { get; set; }

    public bool? IsBarcodeGenerated { get; set; }

    public DateTime? DataAddedOn { get; set; }

    public byte? Age { get; set; }

    public string? Latitude { get; set; }

    public string? Longitude { get; set; }

    public bool? IsHusband { get; set; }

    public string? TbType { get; set; }

    public string? EpsiteOfDisease { get; set; }

    public string? OtherSiteOfDisease { get; set; }

    public decimal? Weight { get; set; }

    public int? HouseHoldContacts { get; set; }

    public int? ContactsUnderFive { get; set; }

    public DateTime? TreatmentStartDate { get; set; }

    public string? PatientType1 { get; set; }

    public int? VisitId { get; set; }

    public DateTime? Expr10 { get; set; }

    public string? ReferredBy { get; set; }

    public decimal? BloodPressureSystolic { get; set; }

    public decimal? BloodPressureDiastolic { get; set; }

    public decimal? Temperature { get; set; }

    public decimal? RespiratoryRate { get; set; }

    public bool? IsDiagnosticInfo { get; set; }

    public string? TbregistrationNo { get; set; }

    public bool? IsXray { get; set; }

    public string? Xrayvalue { get; set; }

    public bool? IsSputum { get; set; }

    public string? MicroscopyValue { get; set; }

    public bool? IsGeneXpert { get; set; }

    public string? Tbcondition { get; set; }

    public string? EprelevantInvestigation { get; set; }

    public string? Epresult { get; set; }

    public bool? IsDiscarded { get; set; }

    public int? TreatmentId { get; set; }

    public int? ComorbidityId { get; set; }

    public string? OtherComorbidity { get; set; }

    public string? Expr13 { get; set; }

    public decimal? Expr18 { get; set; }

    public bool? HivScreened { get; set; }

    public string? Hivresult { get; set; }

    public bool? ReferToArt { get; set; }

    public string? ReferredArt { get; set; }

    public int? ReferredArtid { get; set; }

    public bool? Diabetes { get; set; }

    public int? Expr19 { get; set; }

    public bool? PatientWillingForHivscreening { get; set; }

    public int? Expr21 { get; set; }

    public int? Expr22 { get; set; }

    public DateTime? Expr31 { get; set; }

    public string? Reason { get; set; }

    public string? SampleNo { get; set; }

    public string? BarcodeNo { get; set; }

    public string? SpecimenType { get; set; }

    public DateTime? SamplingDate { get; set; }

    public Guid? SamplingBy { get; set; }

    public string? ReceivingStatus { get; set; }

    public string? ReasonForRejection { get; set; }

    public Guid? ReceivedBy { get; set; }

    public DateTime? ReceivingDate { get; set; }

    public string? LabStatus { get; set; }

    public DateTime? LabStatusUpdateDate { get; set; }

    public string? Result { get; set; }

    public Guid? ResultUpdatedBy { get; set; }

    public DateTime? ResultUpdateDate { get; set; }

    public string? VisualAppearance { get; set; }

    public string? SampleDescription { get; set; }

    public string? TestTechnique { get; set; }

    public string? RrValueTb { get; set; }

    public int? Expr33 { get; set; }

    public int? LabId { get; set; }

    public Guid? LabStatusUpdatedBy { get; set; }

    public int? Expr42 { get; set; }

    public int? TestId { get; set; }

    public DateTime? Expr44 { get; set; }

    public string? TbCondition1 { get; set; }

    public int? Month { get; set; }

    public string? SampleTransportMode { get; set; }

    public string? SampleTransportModeDescription { get; set; }

    public DateTime? SampleCollectionDate { get; set; }

    public DateTime? SamplePerformDate { get; set; }

    public string? LabNo { get; set; }

    public string? Grading { get; set; }

    public string? TbbacterialLoad { get; set; }

    public string? RifampicinResistance { get; set; }

    public string? XrayNo { get; set; }

    public string? OtherXray { get; set; }

    public string? Report { get; set; }

    public bool? SuggestForTb { get; set; }

    public string? TestName { get; set; }

    public int? PatientTreatmentProgressId { get; set; }

    public int? Ppascore { get; set; }

    public string? DivisionName { get; set; }

    public string? DistrictName { get; set; }

    public string? TehsilName { get; set; }

    public string? FullName { get; set; }
}
