using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class Zone
{
    public int ZoneId { get; set; }

    public string? ZoneName { get; set; }

    public bool? IsActive { get; set; }

    public string? DivisonCode { get; set; }

    public string? DistrictCode { get; set; }

    public string? TehsilCode { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? ApplicationTypeId { get; set; }

    public virtual ApplicationType? ApplicationType { get; set; }

    public virtual ICollection<HfZone> HfZones { get; } = new List<HfZone>();

    public virtual ICollection<User> Users { get; } = new List<User>();
}
