using System;
using System.Collections.Generic;

namespace HMIS.Data.SyncUtility.Models.DbModels;

public partial class Division
{
    public int DivisionId { get; set; }

    public string? Code { get; set; }

    public string? Name { get; set; }

    public int? ProvinceId { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public long? UserLogId { get; set; }

    public byte ActionTypeId { get; set; }

    public virtual ICollection<District> Districts { get; } = new List<District>();

    public virtual ICollection<HealthFacility> HealthFacilities { get; } = new List<HealthFacility>();

    public virtual Province? Province { get; set; }

    public virtual ICollection<User> Users { get; } = new List<User>();
}
