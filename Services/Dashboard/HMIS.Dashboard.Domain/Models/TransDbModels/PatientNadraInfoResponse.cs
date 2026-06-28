using System;
using System.Collections.Generic;

namespace HMIS.Dashboard.Domain.Models.TransDbModels;

public partial class PatientNadraInfoResponse
{
    public Guid PatientNadraInfoResponseId { get; set; }

    public Guid? PatientNadraResponseId { get; set; }

    public Guid? PatientId { get; set; }

    public string? Name { get; set; }

    public string? CitizenNumber { get; set; }

    public string? CurrentAddress { get; set; }

    public string? PermanentAddress { get; set; }

    public string? TransactionId { get; set; }

    public int? RequestId { get; set; }

    public int? Code { get; set; }

    public string? Message { get; set; }

    public Guid? StatusProfileId { get; set; }

    public int RequestCount { get; set; }

    public bool? IsNadraResponseBack { get; set; }

    public string? ErrorResponse { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public byte? ActionTypeId { get; set; }
}
