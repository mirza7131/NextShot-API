using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.DbModels;

public partial class Rate
{
    public Guid RatesId { get; set; }

    public int? HealthFacilityId { get; set; }

    public string? Hfmiscode { get; set; }

    public Guid? VendorId { get; set; }

    public decimal? Local { get; set; }

    public decimal? Multinational { get; set; }

    public decimal? Surgical { get; set; }

    public decimal? SurgicalLocal { get; set; }

    public decimal? SurgicalImported { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public byte ActionTypeId { get; set; }
}
