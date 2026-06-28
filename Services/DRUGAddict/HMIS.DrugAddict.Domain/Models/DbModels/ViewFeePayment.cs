using System;
using System.Collections.Generic;

namespace HMIS.DrugAddict.Domain.Models.DbModels;

public partial class ViewFeePayment
{
    public Guid FeePaymentId { get; set; }

    public Guid? PatientId { get; set; }

    public Guid? PatientVisitId { get; set; }

    public string? TokenNo { get; set; }

    public string Cnic { get; set; } = null!;

    public string? Mrno { get; set; }

    public string? PatientName { get; set; }

    public string? DepartmentName { get; set; }

    public string? SectionName { get; set; }

    public bool? IsPaid { get; set; }

    public int? PaidAmount { get; set; }

    public bool? IsRefund { get; set; }

    public string? RefundReason { get; set; }

    public int SectionLookupId { get; set; }

    public int? HealthFacilityId { get; set; }

    public int? SpecialityFee { get; set; }

    public DateTime? CreatedOn { get; set; }
}
