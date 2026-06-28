using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class RepeatVisitPercentage
{
    public int RepeatVisitId { get; set; }

    public int? RepeatVisitPercent { get; set; }

    public string? Month { get; set; }

    public string? Year { get; set; }

    public bool? IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime? UpdatedOn { get; set; }
}
