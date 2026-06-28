using System;
using System.Collections.Generic;

namespace HMIS.Dashboard.Domain.Models.DbModels;

public partial class TbPatientDetail
{
    public Guid TbPatientDetailsId { get; set; }

    public Guid? PatientId { get; set; }

    public Guid? PatientVisitId { get; set; }

    public Guid? PatientDiagnoseId { get; set; }

    public Guid? PatientTypeProfileId { get; set; }

    public Guid? PatientTreatmentLengthProfileId { get; set; }

    public Guid? PatientLengthOfInterruptionProfileId { get; set; }

    public int? NoOfMedicineTaken { get; set; }

    public int? PatientTreatmentCycleNo { get; set; }

    public string? PatientStatus { get; set; }

    public int? CaseNo { get; set; }

    public int? FollowupNo { get; set; }

    public bool? IsReferToDrtb { get; set; }

    public DateTime CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public Guid? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public byte? ActionTypeId { get; set; }

    public string? Drtbcenter { get; set; }

    public int? NoOfMonthsMedicineIssued { get; set; }
}
