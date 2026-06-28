using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class PatientBackup7
{
    public int Id { get; set; }

    public string? DepartmentRegistrationNo { get; set; }

    public string? EmrRegistrationNo { get; set; }

    public string? Name { get; set; }

    public string? CnicType { get; set; }

    public string? CnicGuardianRelation { get; set; }

    public string? Cnic { get; set; }

    public string? PhoneNumber { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? Gender { get; set; }

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

    public string? CreatedBy { get; set; }

    public DateTime? CreationDate { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? DeletedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public bool? RecordStatus { get; set; }

    public int? ProgramId { get; set; }

    public Guid? Guid { get; set; }

    public DateTime? PatientReportingDate { get; set; }

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

    public string? Address { get; set; }

    public string? HealthFacilityDivision { get; set; }

    public string? HealthFacilityDistrict { get; set; }

    public string? HealthFacilityTehsil { get; set; }

    public string? HealthFacilityName { get; set; }

    public string? PatientType { get; set; }

    public double? SrNo { get; set; }
}
