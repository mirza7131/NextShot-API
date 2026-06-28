using System;
using System.Collections.Generic;

namespace AuthDAL.Models.DbModelsReporting;

public partial class PatientAssessment
{
    public Guid PatientAssessmentId { get; set; }

    public Guid? PatientId { get; set; }

    public Guid? PatientDiagnoseId { get; set; }

    public Guid? PatientVisitId { get; set; }

    public bool? IsFrequentTherapeuticInjections { get; set; }

    public bool? IsConfirmedCaseOfStds { get; set; }

    public bool? IsInvasiveMedAndSurgIntervention { get; set; }

    public string? SurgeryType { get; set; }

    public DateTime? SurgeryDate { get; set; }

    public bool? IsCloseContactKnownCaseOfHcvOrHbv { get; set; }

    public bool? IsCloseContactUnderTreatment { get; set; }

    public bool? IsBloodTransfusion { get; set; }

    public DateTime? BloodTransfusionYear { get; set; }

    public string? BloodTransfusionBloodBank { get; set; }

    public bool? IsConfirmedHivpositivePersons { get; set; }

    public bool? IsEverBeenHospitalized { get; set; }

    public bool? IsHospitalizedBeenLastTwoYear { get; set; }

    public bool? IsIndividualsWithTattooing { get; set; }

    public bool? IsInjectableDrugUser { get; set; }

    public bool? IsDentalIntervention { get; set; }

    public string? DentalClinic { get; set; }

    public bool? IsHistoryofMultipleSexPartners { get; set; }

    public bool? IsTruckDriver { get; set; }

    public bool? IsEarNosePiercing { get; set; }

    public bool? IsTransgender { get; set; }

    public bool? IsSharingOfToothBrush { get; set; }

    public bool? IsSharingOfHairComb { get; set; }

    public bool? IsDarkColoredUrine { get; set; }

    public bool? IsLossOfAppetite { get; set; }

    public bool? IsLightColoredFaeces { get; set; }

    public bool? IsFatigue { get; set; }

    public bool? IsMusclePain { get; set; }

    public bool? IsNausea { get; set; }

    public bool? IsStomachAche { get; set; }

    public bool? IsRightUpperQuadrantTenderness { get; set; }

    public bool? IsGastricIrritationBurning { get; set; }

    public bool? IsUnusualUrethralDischarge { get; set; }

    public string? AdvisedSampleCollection { get; set; }

    public string? Monotes { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public long? UserLogId { get; set; }

    public byte ActionTypeId { get; set; }
}
