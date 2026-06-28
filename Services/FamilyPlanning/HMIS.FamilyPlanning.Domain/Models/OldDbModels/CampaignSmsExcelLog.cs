using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.OldDbModels;

public partial class CampaignSmsExcelLog
{
    public int Id { get; set; }

    public string? SmsType { get; set; }

    public string? Status { get; set; }

    public int? PatientId { get; set; }

    public string? MrnNo { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? MonbileNo { get; set; }

    public string? Response { get; set; }

    public string? Message { get; set; }
}
