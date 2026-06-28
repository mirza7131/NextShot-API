using System;
using System.Collections.Generic;

namespace HMIS.DrugAddict.Domain.Models.DbModels;

public partial class Tehsil
{
    public int TehsilId { get; set; }

    public string? Code { get; set; }

    public string? Name { get; set; }

    public int? DistrictId { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public long? UserLogId { get; set; }

    public byte ActionTypeId { get; set; }

    public virtual District? District { get; set; }

    public virtual ICollection<HealthFacility> HealthFacilities { get; } = new List<HealthFacility>();

    public virtual ICollection<UnionCouncil> UnionCouncils { get; } = new List<UnionCouncil>();

    public virtual ICollection<User> Users { get; } = new List<User>();
}
