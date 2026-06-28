using System;
using System.Collections.Generic;

namespace AuthDAL.Models.DbModels;

public partial class ExaminationDetail
{
    public Guid ExaminationDetailId { get; set; }

    public Guid ExaminationId { get; set; }

    public Guid ExaminationProfileId { get; set; }

    public bool? IsActive { get; set; }

    public DateTime CreatedOn { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public long? UserLogId { get; set; }

    public byte ActionTypeId { get; set; }
}
