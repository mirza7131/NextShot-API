using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.DbModels;

public partial class FeePayment
{
    public Guid FeePaymentId { get; set; }

    public Guid? PatientId { get; set; }

    public Guid? PatientVisitId { get; set; }

    public bool? IsPaid { get; set; }

    public int? PaidAmount { get; set; }

    public bool? IsRefund { get; set; }

    public string? RefundReason { get; set; }

    public Guid? RefundBy { get; set; }

    public DateTime? RefundOn { get; set; }

    public int? SectionLookupId { get; set; }

    public int? HealthFacilityId { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public byte? ActionTypeId { get; set; }
}
