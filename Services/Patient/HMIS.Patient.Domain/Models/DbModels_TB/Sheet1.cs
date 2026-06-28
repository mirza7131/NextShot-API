using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class Sheet1
{
    public double? Id { get; set; }

    public string? PatientName { get; set; }

    public string? MrnNo { get; set; }

    public double? Age { get; set; }

    public string? GuardianName { get; set; }

    public double? Gender { get; set; }

    public string? Occupation { get; set; }

    public string? ContactNumber { get; set; }

    public double? Weight { get; set; }

    public string? Hiv { get; set; }

    public string? Result { get; set; }

    public double? MaritalStatus { get; set; }

    public string? CnicStatus { get; set; }

    public string? Cnic { get; set; }

    public string? GuardianCnic { get; set; }

    public double? HouseHoldContacts { get; set; }

    public double? UnderFiveContacts { get; set; }

    public string? DiabetesKnown { get; set; }

    public string? Relation { get; set; }

    public string? RelationContact { get; set; }

    public double? Division { get; set; }

    public double? Districts { get; set; }

    public double? Tehsil { get; set; }

    public double? Hospital { get; set; }

    public string? Address { get; set; }

    public string? ReferredBy { get; set; }

    public string? MedCatType { get; set; }

    public string? IsMedCatTypeChange { get; set; }

    public string? MedCatTypeChangeDate { get; set; }

    public string? PatientType { get; set; }

    public string? IsSsm { get; set; }

    public string? IsXPert { get; set; }

    public string? IsCxr { get; set; }

    public string? IsCulture { get; set; }

    public double? WeightResult { get; set; }

    public double? MedicineResult { get; set; }

    public double? MedCount { get; set; }

    public double? CurrentMedCount { get; set; }

    public double? CurrentFollowupCount { get; set; }

    public double? TotalMedCount { get; set; }

    public string? IsFollowup { get; set; }

    public DateTime? LastFollowupDate { get; set; }

    public string? IsCultureRecieved { get; set; }

    public string? CultureRecieveDate { get; set; }

    public double? PatientStatus { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? UpdatedAt { get; set; }

    public string? CronDate { get; set; }
}
