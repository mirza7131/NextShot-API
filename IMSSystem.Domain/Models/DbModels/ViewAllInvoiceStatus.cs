using System;
using System.Collections.Generic;

namespace IMSSystem.Domain.Models.DbModels;

public partial class ViewAllInvoiceStatus
{
    public int InvoiceId { get; set; }

    public string? InvoiceNumber { get; set; }

    public Guid? SpId { get; set; }

    public string? Logo { get; set; }

    public string? Banner { get; set; }

    public string? SpName { get; set; }

    public Guid? ServiceTypeId { get; set; }

    public string? Service { get; set; }

    public int? HfId { get; set; }

    public string? HealthFacilityName { get; set; }

    public string? HealthFacilityTypeCode { get; set; }

    public string? Hftype { get; set; }

    public string? DivisionCode { get; set; }

    public string? Division { get; set; }

    public string? DistrictCode { get; set; }

    public string? District { get; set; }

    public string? Month { get; set; }

    public DateTime? DueDate { get; set; }

    public DateTime? IssuedOn { get; set; }

    public string InvoiceStatus { get; set; } = null!;
}
