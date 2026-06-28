using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.DbModels;

public partial class ViewAlmonerStat
{
    public Guid? PaymentReceivedBy { get; set; }

    public int? TestCount { get; set; }

    public decimal? PaymentReceived { get; set; }

    public int? TotalUnPaid { get; set; }
}
