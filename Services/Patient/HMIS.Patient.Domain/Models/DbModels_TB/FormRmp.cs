using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class FormRmp
{
    public int Id { get; set; }

    public string? NameOfPatient { get; set; }

    public string? NameOfFatherHusband { get; set; }

    public DateTime? Dob { get; set; }

    public int? Age { get; set; }

    public string? Gender { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public string? Cnic { get; set; }

    public string? Occupation { get; set; }

    public bool? Cough { get; set; }

    public bool? Fever { get; set; }

    public string? Weight { get; set; }

    public bool? NightSweats { get; set; }

    public bool? Aids { get; set; }

    public bool? ChronicRenalDiseas { get; set; }

    public string? DiseaseSite { get; set; }

    public bool? ConformityEvidence { get; set; }

    public string? Sputum { get; set; }

    public bool? IsXpert { get; set; }

    public bool? MtbDetected { get; set; }

    public bool? RifResistanceDetected { get; set; }

    public bool? IsCulture { get; set; }

    public string? FluidsAfb { get; set; }

    public bool? IsRadio { get; set; }

    public string? Radiological { get; set; }

    public string? TypeOfPatient { get; set; }

    public string? TreatmentMedical { get; set; }

    public string? ProviderCode { get; set; }

    public string? HealthFacilityName { get; set; }

    public string? RefferingPhysician { get; set; }

    public string? PmdcRegistration { get; set; }

    public string? RefferingPhysicianAddress { get; set; }

    public string? ContactPersonNo { get; set; }

    public string? EmailReffering { get; set; }

    public DateTime? DateOfFirstVisit { get; set; }

    public DateTime? DateOfOnsetIllness { get; set; }

    public DateTime? DateOfSendingNotification { get; set; }

    public int? MobCreated { get; set; }

    public string? Created { get; set; }

    public string? Updated { get; set; }

    public Guid? UpdatedBy { get; set; }

    public string? Status { get; set; }

    public int? MobUpdated { get; set; }

    public string? Lat { get; set; }

    public string? Lng { get; set; }

    public int? UserId { get; set; }

    public string? Source { get; set; }

    public string? PmdcNo { get; set; }
}
