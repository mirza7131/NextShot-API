using System;
using System.Collections.Generic;

namespace HMIS.HCP.Domain.Models.OldDbModels;

public partial class TblCallcenterStat
{
    public int Id { get; set; }

    public int? NumberOfCalls { get; set; }

    public int? AnsweredCalls { get; set; }

    public int? AbandonedCalls { get; set; }

    public int? Hepatitis { get; set; }

    public int? TotalRegistration { get; set; }

    public int? Samplereceived { get; set; }

    public int? Date { get; set; }

    public int? UserId { get; set; }

    public int? Created { get; set; }
}
