using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class ViewTodaysVisit
{
    public int? TodayVisits { get; set; }

    public int? TodayBhuvisits { get; set; }

    public int? TodayBhu247visits { get; set; }

    public int? TodayRhcvisits { get; set; }
}
