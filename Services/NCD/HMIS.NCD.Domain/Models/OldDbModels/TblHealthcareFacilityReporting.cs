using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.OldDbModels;

public partial class TblHealthcareFacilityReporting
{
    public int Id { get; set; }

    public int? FacilityId { get; set; }

    public string? FacilityName { get; set; }

    public int? PatientRegisteredCount { get; set; }

    public int? NewPatientCount { get; set; }

    public int? PrediagnosedPatientCount { get; set; }

    public int? ScreeningPerformedCount { get; set; }

    public int? ScreeningPositiveForHbvCount { get; set; }

    public int? ScreeningPositiveForHcvCount { get; set; }

    public int? ScreeningPositiveForBothCount { get; set; }

    public int? SampleCollectedCount { get; set; }

    public int? SampleCollectionPendingCount { get; set; }

    public int? PcrSampleReceivedInLabCount { get; set; }

    public int? PcrSampleAcceptedInLabCount { get; set; }

    public int? PcrSampleRejectedInLabCount { get; set; }

    public int? PcrSampleDetectedCount { get; set; }

    public int? PcrSampleNotDetectedCount { get; set; }

    public int? PcrSampleResampleCount { get; set; }

    public int? PatientsEnrolledInTreatmentCount { get; set; }

    public int? VaccineDoseAdministeredCount { get; set; }

    public int? FirstMonthMedicineDeliveredForHcvCount { get; set; }

    public int? SecondMonthMedicineDeliveredForHcvCount { get; set; }

    public int? ThirdMonthMedicineDeliveredForHcvCount { get; set; }

    public int? StockMadeAvailableCount { get; set; }

    public int? StockDisbursedCount { get; set; }

    public int? StockInHandCount { get; set; }

    public DateTime? CreatedDate { get; set; }
}
