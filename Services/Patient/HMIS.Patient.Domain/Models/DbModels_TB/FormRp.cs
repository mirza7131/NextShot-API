using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class FormRp
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

    public string? SEchoStatus { get; set; }

    public string? HistoryInFamily { get; set; }

    public string? CaseOfRetreat { get; set; }

    public bool? Cough { get; set; }

    public bool? Fever { get; set; }

    public bool? WeightLoss { get; set; }

    public bool? NightSweats { get; set; }

    public bool? Aids { get; set; }

    public bool? Diabetes { get; set; }

    public string? DiseaseSite { get; set; }

    public bool? ConfirmEvidence { get; set; }

    public string? SputumAfb { get; set; }

    public bool? GXpert { get; set; }

    public string? Fnac { get; set; }

    public string? RadioFinding { get; set; }

    public string? TypeOfPatient { get; set; }

    public string? MedicineInUse { get; set; }

    public string? UnderTreatment { get; set; }

    public string? HealthFacilityName { get; set; }

    public string? PractRegNo { get; set; }

    public string? ContactPersonNo { get; set; }

    public string? Email { get; set; }

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
}
