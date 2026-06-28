using System;
using System.Collections.Generic;

namespace HMIS.FamilyPlanning.Domain.Models.DbModels;

public partial class GeoLevel
{
    public long Id { get; set; }

    public string? Pkcode { get; set; }

    public string? Fkcode { get; set; }

    public string? Code { get; set; }

    public string? Name { get; set; }

    public string? Lvl { get; set; }

    public string? EnableFlag { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreationDate { get; set; }

    public string? UpdtedBy { get; set; }

    public DateTime? UpdationDate { get; set; }

    public long? ParentId { get; set; }

    public string? LastMrnumber { get; set; }

    public DateTime? LastMrTokenUpdate { get; set; }

    public long? LastTokenNumber { get; set; }

    public DateTime? LastSyncDateTime { get; set; }

    public string? Hrpkcode { get; set; }

    public bool? IsPhfmc { get; set; }

    public bool? IsPhcipdistrict { get; set; }
}
