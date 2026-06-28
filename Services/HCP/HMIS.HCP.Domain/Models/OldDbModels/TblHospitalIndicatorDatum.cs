using System;
using System.Collections.Generic;

namespace HMIS.HCP.Domain.Models.OldDbModels;

public partial class TblHospitalIndicatorDatum
{
    public int Id { get; set; }

    public int? Created { get; set; }

    public int? HospitalId { get; set; }

    public string? FacilityName { get; set; }

    public string? NumberOfActiveDays { get; set; }

    public string? ActivationDate { get; set; }

    public string? IsRegister { get; set; }

    public string? NewPatient { get; set; }

    public string? PreDiagnosed { get; set; }

    public string? EnrolledInTreatmentWithSrd { get; set; }

    public string? EntecavirBooked { get; set; }

    public string? EntecavirDisbursed { get; set; }

    public string? EntecavirRemaining { get; set; }

    public string? EntecavirSupplied { get; set; }

    public string? FirstMonthMedicineDelivered { get; set; }

    public string? HbvPatientsEnrolledInNewRegimen { get; set; }

    public string? HbvPatientEnrolledInOldRegimen { get; set; }

    public string? HcvPateintsCured { get; set; }

    public string? HcvPatientsEnrolledInNewRegimen { get; set; }

    public string? HcvPatientsEnrolledInOldRegimen { get; set; }

    public string? HcvPatientsTreatmentFailed { get; set; }

    public string? PatientsPendingTreatmentEnollment { get; set; }

    public string? EnrolledInTreatmentWithSr { get; set; }

    public string? EnrolledInTreatmentWithSd { get; set; }

    public string? PcrSampleAcceptedInLab { get; set; }

    public string? PcrSampleDetected { get; set; }

    public string? PcrSampleNotDetected { get; set; }

    public string? PcrSampleReceivedInLab { get; set; }

    public string? PcrSampleRejectedInLab { get; set; }

    public string? PcrSampleResample { get; set; }

    public string? SampleCollected { get; set; }

    public string? SampleCollectionPending { get; set; }

    public string? ScreenedPosBoth { get; set; }

    public string? ScreenedPosHbv { get; set; }

    public string? ScreenedPosHcv { get; set; }

    public string? ScreeningPerformed { get; set; }

    public string? SdBooked { get; set; }

    public string? SdDisbursed { get; set; }

    public string? SdRemaining { get; set; }

    public string? SdSupplied { get; set; }

    public string? SdTherapyFollowUpsDeafulted { get; set; }

    public string? SdTherapyFollowUpsDue { get; set; }

    public string? SdTherapyFollowUpsOverdue { get; set; }

    public string? SecondMonthMedicineDelivered { get; set; }

    public string? SrdTherapyFollowUpsDeafulted { get; set; }

    public string? SrdTherapyFollowUpsDue { get; set; }

    public string? SrdTherapyFollowUpsOverdue { get; set; }

    public string? SrBooked { get; set; }

    public string? SrDisbursed { get; set; }

    public string? SrRemaining { get; set; }

    public string? SrSupplied { get; set; }

    public string? SrTherapyFollowUpsDeafulted { get; set; }

    public string? SrTherapyFollowUpsDue { get; set; }

    public string? SrTherapyFollowUpsOverdue { get; set; }

    public string? StockDisbursed { get; set; }

    public string? StockInHand { get; set; }

    public string? StockMadeAvailable { get; set; }

    public string? SvrCollectedCount { get; set; }

    public string? SvrPending { get; set; }

    public string? SvrPendingCountWithGap { get; set; }

    public string? TelbuvidineBooked { get; set; }

    public string? TelbuvidineDisbursed { get; set; }

    public string? TelbuvidineRemaining { get; set; }

    public string? TelbuvidineSupplied { get; set; }

    public string? TenofovirBooked { get; set; }

    public string? TenofovirDisbursed { get; set; }

    public string? TenofovirRemaining { get; set; }

    public string? TenofovirSupplied { get; set; }

    public string? ThirdMonthMedicineDelivered { get; set; }

    public string? TreatmentWithEntecavir { get; set; }

    public string? TreatmentWithTelbuvidine { get; set; }

    public string? TreatmentWithTenofovir { get; set; }

    public string? VaccinationSecondDoseDeafulters { get; set; }

    public string? VaccinationThirdDoseDeafulters { get; set; }

    public string? VaccineFirstDose { get; set; }

    public string? VaccineFirstDosePendingCount { get; set; }

    public string? VaccineSecondDose { get; set; }

    public string? VaccineSecondDoseOverdueCount { get; set; }

    public string? VaccineThirdDose { get; set; }

    public string? VaccineThirdDoseOverdueCount { get; set; }
}
