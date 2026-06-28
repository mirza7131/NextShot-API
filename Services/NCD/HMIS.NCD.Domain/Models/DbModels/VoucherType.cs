using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.DbModels;

public partial class VoucherType
{
    public Guid Id { get; set; }

    public int? HealthFacilityId { get; set; }

    public string? Hfmiscode { get; set; }

    public string? Name { get; set; }

    public string? ShortName { get; set; }

    public string? Remarks { get; set; }

    public string? EnableFlag { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public byte ActionTypeId { get; set; }
}
