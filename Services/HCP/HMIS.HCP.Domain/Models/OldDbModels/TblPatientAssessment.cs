using System;
using System.Collections.Generic;

namespace HMIS.HCP.Domain.Models.OldDbModels;

public partial class TblPatientAssessment
{
    public long Id { get; set; }

    public string FrequentTherapeuticInjections { get; set; } = null!;

    public string ConfirmedCaseOfStds { get; set; } = null!;

    public string InvasiveMedicalAndSurgicalIntervention { get; set; } = null!;

    public string CloseContactOfAKnownCaseOfHcvHbv { get; set; } = null!;

    public string BloodTransfusion { get; set; } = null!;

    public string ConfirmedHivPositivePersons { get; set; } = null!;

    public string EverBeenHospitalized { get; set; } = null!;

    public string IndividualsWithTattooingEarNosePiercing { get; set; } = null!;

    public string InjectableDrugUser { get; set; } = null!;

    public string DentalIntervention { get; set; } = null!;

    public string HistoryOfMultipleSexPartners { get; set; } = null!;

    public string TruckDriverOrTransgender { get; set; } = null!;

    public string Jaundice { get; set; } = null!;

    public string UnexplainedFever { get; set; } = null!;

    public string DarkColoredUrine { get; set; } = null!;

    public string LossOfAppetite { get; set; } = null!;

    public string LightColoredFaeces { get; set; } = null!;

    public string Fatigue { get; set; } = null!;

    public string MusclePain { get; set; } = null!;

    public string Nausea { get; set; } = null!;

    public string StomachAche { get; set; } = null!;

    public string RightUpperQuadrantTenderness { get; set; } = null!;

    public string GastricIrritationBurning { get; set; } = null!;

    public string UnusualUrethralDischarge { get; set; } = null!;

    public int Created { get; set; }

    public int? Updated { get; set; }

    public int? UserId { get; set; }

    public int? UserHospital { get; set; }

    public int? PatientId { get; set; }

    public string? Note { get; set; }

    public string? IsNewPatient { get; set; }

    public string? Pcr { get; set; }

    public string? PcrOption { get; set; }

    public string? Vaccination { get; set; }

    public string? IsHbvTest { get; set; }

    public string? IsHcvTest { get; set; }

    public string? RapidTesting { get; set; }

    public string? Counselling { get; set; }

    public string? Vacination { get; set; }

    public string? IsSampleSvr { get; set; }

    public string? IsLegacySvr { get; set; }

    public string? SurgeryType { get; set; }

    public string? SurgeryWhen { get; set; }

    public string? BloodTransfusionWhen { get; set; }

    public string? BloodBank { get; set; }

    public string? HospitalizationWithinLast2Years { get; set; }

    public string? DentalClinic { get; set; }

    public string? CloseContactIsOnTreatment { get; set; }

    public string? IsAlreadyVaccinated { get; set; }

    public int? NoOfDosesTaken { get; set; }

    public int? VacDoseDate1 { get; set; }

    public int? VacDoseDate2 { get; set; }

    public string? VacAdministered { get; set; }

    public int? DoseEligibility { get; set; }

    public string? IsTemporaryDelete { get; set; }

    public string? EarNosePirecing { get; set; }

    public string? Transgender { get; set; }

    public string? SharingToothbrush { get; set; }

    public string? SharingHairComb { get; set; }
}
