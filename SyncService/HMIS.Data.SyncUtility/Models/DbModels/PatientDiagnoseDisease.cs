using System;
using System.Collections.Generic;

namespace HMIS.Data.SyncUtility.Models.DbModels;

public partial class PatientDiagnoseDisease
{
    public Guid PatientDiagnoseDiseaseId { get; set; }

    public Guid PatientDiagnoseId { get; set; }

    public Guid DiseaseProfileId { get; set; }

    public Guid? PatientId { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public long? UserLogId { get; set; }

    public byte? ActionTypeId { get; set; }

    public byte? DiagnoseTypeId { get; set; }

    public virtual Profile DiseaseProfile { get; set; } = null!;

    public virtual Patient? Patient { get; set; }

    public virtual PatientDiagnose PatientDiagnose { get; set; } = null!;
}
