using System;
using System.Collections.Generic;

namespace HMIS.HCP.Domain.Models.OldDbModels;

public partial class TblPatientAssessmentLog
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

    public DateTime DateTime { get; set; }
}
