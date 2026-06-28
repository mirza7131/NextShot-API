using System;
using System.Collections.Generic;

namespace HMIS.DrugAddict.Domain.Models.DbModels;

public partial class UnionCouncil
{
    public int UnionCouncilId { get; set; }

    public string? Name { get; set; }

    public string? Code { get; set; }

    public int TehsilId { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public long? UserLogId { get; set; }

    public byte ActionTypeId { get; set; }

    public virtual ICollection<HealthFacility> HealthFacilities { get; } = new List<HealthFacility>();

    public virtual Tehsil Tehsil { get; set; } = null!;
}
