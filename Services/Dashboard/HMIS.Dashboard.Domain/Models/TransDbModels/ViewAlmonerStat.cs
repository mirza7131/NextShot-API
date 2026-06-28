using System;
using System.Collections.Generic;

namespace HMIS.Dashboard.Domain.Models.TransDbModels;

public partial class ViewAlmonerStat
{
    public Guid? PaymentReceivedBy { get; set; }

    public int? TestCount { get; set; }

    public decimal? PaymentReceived { get; set; }

    public int? TotalUnPaid { get; set; }
}
