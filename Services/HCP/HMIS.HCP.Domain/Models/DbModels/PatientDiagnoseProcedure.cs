using System;
using System.Collections.Generic;

namespace HMIS.HCP.Domain.Models.DbModels;

public partial class PatientDiagnoseProcedure
{
    public Guid PatientDiagnoseProcedureId { get; set; }

    public Guid? PatientDiagnoseId { get; set; }

    public int? SectionProcedureId { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public byte ActionTypeId { get; set; }

    public bool? IsActive { get; set; }

    public Guid? RecommendBy { get; set; }

    public Guid? PerformedBy { get; set; }

    public string? Feedback { get; set; }

    public bool? IsPerformed { get; set; }

    public virtual PatientDiagnose? PatientDiagnose { get; set; }

    public virtual SectionProcedure? SectionProcedure { get; set; }
}
