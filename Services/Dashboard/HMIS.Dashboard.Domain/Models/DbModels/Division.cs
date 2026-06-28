using System;
using System.Collections.Generic;

namespace HMIS.Dashboard.Domain.Models.DbModels;

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
}
