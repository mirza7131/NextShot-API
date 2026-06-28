using System;
using System.Collections.Generic;

namespace HMIS.Pathalogy.Domain.Models.DbModels;

public partial class ViewRejectedConsignment
{
    public string Cnic { get; set; } = null!;

    public string? Mrno { get; set; }

    public string? FullName { get; set; }

    public string? MobileNo { get; set; }

    public string? LabTestName { get; set; }

    public string? BatchNo { get; set; }
    public string? BarcodeNo { get; set; }

    public string? FromHealthFacility { get; set; }

    public string? ToHealthFacility { get; set; }

    public string? StatusReason { get; set; }

    public bool? Status { get; set; }
    public DateTime? CreatedOn { get; set; }
    public Guid? UpdatedBy { get; set; }

    public decimal? TestPrice { get; set; }
}
