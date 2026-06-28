using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class ViewMeasVisit
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string? FullName { get; set; }

    public DateTime SyncOn { get; set; }

    public string? Hfmiscode { get; set; }

    public string? HealthFacilityName { get; set; }

    public string? DivisionName { get; set; }

    public string? DistrictName { get; set; }

    public string? TehsilName { get; set; }

    public string? ShiftName { get; set; }
}
