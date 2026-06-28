using System;
using System.Collections.Generic;

namespace AuthDAL.Models.DbModels;

public partial class HcpMedicineDeliveryDatum
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

    public string? TypeOfPatient { get; set; }

    public string? Medicine { get; set; }

    public int? MonthsMedicineAdvised { get; set; }

    public DateTime? LastMedicineReceivedDate { get; set; }

    public int? MedicineDeliveredCount { get; set; }

    public string? VerifiedAddress { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public byte? DeliveryStatus { get; set; }

    public DateTime? SentForPackingOn { get; set; }

    public DateTime? PackingOn { get; set; }

    public DateTime? DeliveryOn { get; set; }
}
