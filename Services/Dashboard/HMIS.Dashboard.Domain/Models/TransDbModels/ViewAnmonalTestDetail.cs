using System;
using System.Collections.Generic;

namespace HMIS.Dashboard.Domain.Models.TransDbModels;

public partial class ViewAnmonalTestDetail
{
    public Guid? PatientVisitId { get; set; }

    public Guid PatientLabTestId { get; set; }

    public bool? IsSampleCollected { get; set; }

    public bool? IsReportGenerated { get; set; }

    public string? LabNo { get; set; }

    public string? SectionName { get; set; }

    public int SectionLookupId { get; set; }

    public string? TestName { get; set; }

    public decimal Price { get; set; }

    public string TestTypeShortName { get; set; } = null!;

    public bool PaidStatus { get; set; }

    public bool IsAdvisedExternally { get; set; }

    public string? LabDepartmentShortName { get; set; }

    public string LabDepartmentName { get; set; } = null!;

    public decimal? DiscountInPercentage { get; set; }

    public decimal? DiscountedPrice { get; set; }

    public Guid? DiscountedByProfileId { get; set; }

    public string? DiscountedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? CreatedBy { get; set; }

    public string? Designation { get; set; }

    public bool? IsRefunded { get; set; }

    public string? RefundReason { get; set; }

    public int? IsDiscountedByDoctor { get; set; }
}
