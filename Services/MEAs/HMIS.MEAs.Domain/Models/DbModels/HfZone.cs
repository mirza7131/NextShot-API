using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class HfZone
{
    public int HfZoneId { get; set; }

    public int? HfId { get; set; }

    public int? ZoneId { get; set; }

    public bool? IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public bool? FacilityActive { get; set; }

    public virtual Zone? Zone { get; set; }
}
