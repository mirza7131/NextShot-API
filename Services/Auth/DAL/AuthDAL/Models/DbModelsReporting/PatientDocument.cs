using System;
using System.Collections.Generic;

namespace AuthDAL.Models.DbModelsReporting;

public partial class PatientDocument
{
    public Guid PatientDocumentId { get; set; }

    public Guid PatientId { get; set; }

    public Guid? PatientVisitId { get; set; }

    public Guid? PatientDocumentTypeProfileId { get; set; }

    public Guid? DocumentProfileId { get; set; }

    public string? DocumentName { get; set; }

    public string? Url { get; set; }

    public string? Base64 { get; set; }

    public byte? Status { get; set; }

    public string? StatusReason { get; set; }

    public DateTime? StatusUpdatedOn { get; set; }

    public Guid? StatusUpdatedBy { get; set; }

    public bool IsActive { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public byte ActionTypeId { get; set; }
}
