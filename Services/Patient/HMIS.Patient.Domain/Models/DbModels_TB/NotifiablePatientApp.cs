using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class NotifiablePatientApp
{
    public int Id { get; set; }

    public string? MicroResultOfSputum { get; set; }

    public string? MicroResultOfFluid { get; set; }

    public string? CultureOfSputum { get; set; }

    public string? CultureOfBodyFluid { get; set; }

    public bool? GXpert { get; set; }

    public string? NameOfIncharge { get; set; }

    public string? Designation { get; set; }

    public string? Qualification { get; set; }

    public string? MedicineInUse { get; set; }

    public string? TestAddress { get; set; }

    public string? ContactPersonNo { get; set; }

    public string? Email { get; set; }

    public string? TestPersonName { get; set; }

    public DateTime? DateOfSpecimen { get; set; }

    public string? TestPersonDesignation { get; set; }

    public DateTime? DateOfSendingNotification { get; set; }

    public int? MobCreated { get; set; }

    public Guid? CreatedBy { get; set; }

    public string? Updated { get; set; }

    public Guid? UpdatedBy { get; set; }

    public string? Status { get; set; }

    public int? MobUpdated { get; set; }

    public string? Lat { get; set; }

    public string? Lng { get; set; }

    public int? UserId { get; set; }

    public string? Source { get; set; }

    public int? PatientId { get; set; }

    public bool? Cough { get; set; }

    public bool? Fever { get; set; }

    public bool? WeightLoss { get; set; }

    public bool? NightSweats { get; set; }

    public string? TestConducted { get; set; }

    public string? NameNotifying { get; set; }

    public string? DesignationNotifying { get; set; }

    public string? AddressNotifying { get; set; }

    public string? EmailReffering { get; set; }

    public DateTime? DateOfExperiencing { get; set; }

    public bool? Aids { get; set; }

    public bool? ChronicRenalDiseas { get; set; }

    public string? DiseaseSite { get; set; }

    public bool? ConformityEvidence { get; set; }

    public string? Sputum { get; set; }

    public string? IsXpert { get; set; }

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

    public DateTime? DateOfFirstVisit { get; set; }

    public DateTime? DateOfOnsetIllness { get; set; }

    public string? SEchoStatus { get; set; }

    public string? HistoryInFamily { get; set; }

    public string? CaseOfRetreat { get; set; }

    public bool? Diabetes { get; set; }

    public bool? ConfirmEvidence { get; set; }

    public string? SputumAfb { get; set; }

    public string? Fnac { get; set; }

    public string? RadioFinding { get; set; }

    public string? UnderTreatment { get; set; }

    public string? PractRegNo { get; set; }

    public string? DoctorType { get; set; }

    public virtual Patient? Patient { get; set; }
}
