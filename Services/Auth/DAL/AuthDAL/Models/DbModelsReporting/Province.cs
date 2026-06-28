using System;
using System.Collections.Generic;

namespace AuthDAL.Models.DbModelsReporting;

public partial class Province
{
    public int ProvinceId { get; set; }

    public string? Name { get; set; }

    public string? Code { get; set; }

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
