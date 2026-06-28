using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.DbModels;

public partial class LabTestDetail
{
    public int LabTestDetailId { get; set; }

    public int? LabTestId { get; set; }

    public string? TestName { get; set; }

    public string? TestNormalValue { get; set; }

    public string? MinValue { get; set; }

    public string? MaxValue { get; set; }

    public string? TestUnit { get; set; }

    public int? SequenceNo { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public long? UserLogId { get; set; }

    public byte ActionTypeId { get; set; }

    public Guid? TestResultDropDownTypeProfileId { get; set; }

    public string? TestResultInputType { get; set; }

    public string? TestResultInputValue { get; set; }

    public virtual LabTest? LabTest { get; set; }
}
