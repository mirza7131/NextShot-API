using System;
using System.Collections.Generic;

namespace IMSSystem.Domain.Models.DbModels;

public partial class ServiceProvider
{
    public Guid Id { get; set; }

    public Guid? StakeHolderTypeId { get; set; }

    public string? Name { get; set; }

    public string? Logo { get; set; }

    public string? Banner { get; set; }

    public string? OwnerName { get; set; }

    public string? OwneCnic { get; set; }

    public string? OwnerMob { get; set; }

    public string? DivisionCde { get; set; }

    public string? DistrictCode { get; set; }

    public string? Address { get; set; }

    public bool? IsActive { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public virtual ICollection<SpService> SpServices { get; set; } = new List<SpService>();
}
