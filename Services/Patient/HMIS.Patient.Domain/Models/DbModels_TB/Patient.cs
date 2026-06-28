using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class Patient
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

    public DateTime? StatusUpdatedDate { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public bool? RecordStatus { get; set; }

    public int? ProgramId { get; set; }

    public Guid? Guid { get; set; }

    public string? HealthFacilityCode { get; set; }

    public string? PatientSource { get; set; }

    public string? FingerPrint1 { get; set; }

    public string? FingerPrint2 { get; set; }

    public string? FingerPrint3 { get; set; }

    public string? FingerPrint4 { get; set; }

    public string? FingerPrint5 { get; set; }

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

    public bool? IsSmooker { get; set; }

    public virtual ICollection<FollowUpTb> FollowUpTbs { get; } = new List<FollowUpTb>();

    public virtual ICollection<NotifiablePatientApp> NotifiablePatientApps { get; } = new List<NotifiablePatientApp>();

    public virtual ICollection<PatientContact> PatientContacts { get; } = new List<PatientContact>();

    public virtual ICollection<PatientContactsStatistic> PatientContactsStatistics { get; } = new List<PatientContactsStatistic>();

    public virtual ICollection<PatientMedicineHistory> PatientMedicineHistories { get; } = new List<PatientMedicineHistory>();

    public virtual ICollection<PatientMedicineHistoryLog> PatientMedicineHistoryLogs { get; } = new List<PatientMedicineHistoryLog>();

    public virtual ICollection<PatientOutcome> PatientOutcomes { get; } = new List<PatientOutcome>();

    public virtual ICollection<PatientTransferLog> PatientTransferLogs { get; } = new List<PatientTransferLog>();

    public virtual ICollection<PatientTreatmentInfoTb> PatientTreatmentInfoTbs { get; } = new List<PatientTreatmentInfoTb>();

    public virtual ICollection<PatientVitalsTb> PatientVitalsTbs { get; } = new List<PatientVitalsTb>();

    public virtual ICollection<TransferHistory> TransferHistories { get; } = new List<TransferHistory>();
}
