using System;
using System.Collections.Generic;

namespace HMIS.DrugAddict.Domain.Models.DbModels;

public partial class DaSocialWelfareForm
{
    public Guid Id { get; set; }

    public Guid FormTypeProfileId { get; set; }

    public string JsonBody { get; set; } = null!;

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public long UserLogId { get; set; }

    public byte ActionTypeId { get; set; }
}
