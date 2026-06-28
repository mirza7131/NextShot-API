using System;
using System.Collections.Generic;

namespace AuthDAL.Models.DbModelsReporting;

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

    public Guid? DentalDiseaseProfileId { get; set; }

    public Guid? ToothPositionProfileId { get; set; }

    public Guid? ToothNumberProfileId { get; set; }

    public string? ToothNumber { get; set; }

    public string? ToothPosition { get; set; }

    public Guid? AssistedBy { get; set; }

    public Guid? PatientVisitId { get; set; }
}
