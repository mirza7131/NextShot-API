using System;
using System.Collections.Generic;

namespace AuthDAL.Models.DbModelsReporting;

public partial class ViewAlmonerStat
{
    public Guid? PaymentReceivedBy { get; set; }

    public int? TestCount { get; set; }

    public decimal? PaymentReceived { get; set; }

    public int? TotalUnPaid { get; set; }
}
