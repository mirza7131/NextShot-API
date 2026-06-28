using System;
using System.Collections.Generic;

namespace HMIS.EMC.Domain.Models.DbModels;

public partial class PatientNadraRequest
{
    public Guid PatientNadraRequestId { get; set; }

    public Guid? PatientId { get; set; }

    public string? LocationId { get; set; }

    public int? RequestId { get; set; }

    public int? RequestCode { get; set; }

    public string? FingerPrintFormate { get; set; }

    public Guid? StatusProfileId { get; set; }

    public bool? IsTransactionSaveSuccessfully { get; set; }

    public bool? IsPendingVerification { get; set; }

    public int RequestCount { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public byte? ActionTypeId { get; set; }
}
