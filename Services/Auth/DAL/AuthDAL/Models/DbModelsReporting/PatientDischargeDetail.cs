using System;
using System.Collections.Generic;

namespace AuthDAL.Models.DbModelsReporting;

public partial class PatientDischargeDetail
{
    public Guid PatientDischargeDetailId { get; set; }

    public Guid PatientId { get; set; }

    public Guid PatientVisitId { get; set; }

    public Guid? DischargePatientDiagnoseId { get; set; }

    public DateTime? DateOfDischarge { get; set; }

    public Guid? DischargeStatusProfileId { get; set; }

    public string? Reason { get; set; }

    public bool IsActive { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public byte ActionTypeId { get; set; }

    public string? ReferHealthFacility { get; set; }
}
