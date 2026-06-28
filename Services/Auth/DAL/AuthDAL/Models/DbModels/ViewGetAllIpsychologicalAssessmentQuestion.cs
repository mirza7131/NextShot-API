using System;
using System.Collections.Generic;

namespace AuthDAL.Models.DbModels;

public partial class ViewGetAllIpsychologicalAssessmentQuestion
{
    public string Question { get; set; } = null!;

    public string? ShortName { get; set; }

    public Guid ProfileId { get; set; }

    public string? Option1 { get; set; }

    public string? Option2 { get; set; }

    public string? Option3 { get; set; }

    public string? Option4 { get; set; }

    public string? Option5 { get; set; }

    public string? Option6 { get; set; }

    public string? Option7 { get; set; }

    public string? Option8 { get; set; }

    public string? Option9 { get; set; }
}
