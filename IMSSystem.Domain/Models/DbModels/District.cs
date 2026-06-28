using System;
using System.Collections.Generic;

namespace IMSSystem.Domain.Models.DbModels;

public partial class District
{
    public int DistrictId { get; set; }

    public string? Code { get; set; }

    public string? Name { get; set; }

    public int? DivisionId { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public long? UserLogId { get; set; }

    public byte ActionTypeId { get; set; }

    public virtual Division? Division { get; set; }

    public virtual ICollection<HealthFacility> HealthFacilities { get; set; } = new List<HealthFacility>();

    public virtual ICollection<Tehsil> Tehsils { get; set; } = new List<Tehsil>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
