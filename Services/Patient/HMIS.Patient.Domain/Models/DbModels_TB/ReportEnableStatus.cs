using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class ReportEnableStatus
{
    public int ReportEnableStatusId { get; set; }

    public string? ReportName { get; set; }

    public DateTime? EnableReportDateFrom { get; set; }

    public DateTime? EnableReportDateTo { get; set; }

    public bool? IsEnable { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreationDate { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public bool? RecordStatus { get; set; }

    public Guid Guid { get; set; }
}
