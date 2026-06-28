using System;
using System.Collections.Generic;

namespace HMIS.MIMS.Domain.Models.DbModels;

public partial class LabSampleBatch
{
    public Guid LabSampleBatchId { get; set; }

    public string? Name { get; set; }

    public string? Number { get; set; }

    public bool? IsScannedBatch { get; set; }

    public Guid? ScannedBy { get; set; }

    public DateTime? ScannedOn { get; set; }

    public Guid? BatchCreatedBy { get; set; }

    public DateTime? BatchCreatedOn { get; set; }

    public Guid? ResultUploadedBy { get; set; }

    public DateTime? ResultUploadedOn { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public long? UserLogId { get; set; }

    public byte ActionTypeId { get; set; }
}
