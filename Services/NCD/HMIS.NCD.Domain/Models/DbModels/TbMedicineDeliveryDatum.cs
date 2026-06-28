using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.DbModels;

public partial class TbMedicineDeliveryDatum
{
    public int Id { get; set; }

    public string? MrNo { get; set; }

    public DateTime? RegistrationDate { get; set; }

    public string? HealthFacilityDistrict { get; set; }

    public string? HealthFacilityTehsil { get; set; }

    public string? HealthFacility { get; set; }

    public string? PatientName { get; set; }

    public string? FatherName { get; set; }

    public string? Cnic { get; set; }

    public string? Gender { get; set; }

    public decimal? Age { get; set; }

    public string? PatientDivision { get; set; }

    public string? PatientDistrict { get; set; }

    public string? PatientTehsil { get; set; }

    public string? PatientAddress { get; set; }

    public string? PhoneNo1 { get; set; }

    public string? PhoneNo2 { get; set; }

    public string? Weight { get; set; }

    public string? DiseaseSite { get; set; }

    public string? TypeOfPatient { get; set; }

    public DateTime? DateOfExamination { get; set; }

    public string? TreatmentPhase { get; set; }

    public string? RegimenType { get; set; }

    public string? NameOfMedicineAdvised { get; set; }

    public string? AdditionalMedicineAdvised { get; set; }

    public string? Dose { get; set; }

    public string? Frequency { get; set; }

    public string? DurationInWeeks { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public byte? DeliveryStatus { get; set; }

    public DateTime? SentForPackingOn { get; set; }

    public DateTime? PackingOn { get; set; }

    public DateTime? DeliveryOn { get; set; }

    public int? OrderBy { get; set; }
}
