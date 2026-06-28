using System;
using System.Collections.Generic;

namespace AuthDAL.Models.DbModelsReporting;

public partial class PatientScreening
{
    public Guid PatientScreeningId { get; set; }

    public Guid? PatientId { get; set; }

    public Guid? PatientDiagnoseId { get; set; }

    public Guid? PatientVisitId { get; set; }

    public bool? IsPreviouslyDiagnosedHbv { get; set; }

    public bool? HasHbvpcrconfirmation { get; set; }

    public bool? IsPreviouslyDiagnosedHcv { get; set; }

    public bool? HasHcvpcrconfirmation { get; set; }

    public string? PatientType { get; set; }

    public Guid? PatientTypeProfileId { get; set; }

    public bool? IsDiagnosedHbvrepidKit { get; set; }

    public bool? IsDiagnosedHcvrepidKit { get; set; }

    public bool? IsDialysisPatient { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public long? UserLogId { get; set; }

    public byte ActionTypeId { get; set; }
}
