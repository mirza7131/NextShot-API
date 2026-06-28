using System;
using System.Collections.Generic;

namespace AuthDAL.Models.DbModelsReporting;

public partial class Profile
{
    public Guid ProfileId { get; set; }

    public int? ProfileAutoId { get; set; }

    public string Name { get; set; } = null!;

    public string? ShortName { get; set; }

    public int? SequenceNo { get; set; }

    public string? Description { get; set; }

    public Guid ProfileTypeId { get; set; }

    public bool? IsDssDisease { get; set; }

    public Guid? ParentProfileId { get; set; }

    public int? ChartPieSequenceNo { get; set; }

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
