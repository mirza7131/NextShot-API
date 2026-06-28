using System;
using System.Collections.Generic;

namespace HMIS.Dashboard.Domain.Models.TransDbModels;

public partial class CallDetail
{
    public Guid CallDetailId { get; set; }

    public Guid PatientId { get; set; }

    public Guid PatientVisitId { get; set; }

    public Guid? PatientDiagnoseId { get; set; }

    public Guid? PatientStatusProfileId { get; set; }

    public int? HealthFacilityId { get; set; }

    public Guid? ProgramTypeProfileId { get; set; }

    public DateTime? ContactDateTime { get; set; }

    public Guid? CallReasonTypeProfileId { get; set; }

    public Guid? CallResultProfileId { get; set; }

    public string? ReasonDetail { get; set; }

    public DateTime? RevisitDateTime { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public byte ActionTypeId { get; set; }
}
