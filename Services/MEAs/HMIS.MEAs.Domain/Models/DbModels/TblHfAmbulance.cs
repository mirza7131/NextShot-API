using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class TblHfAmbulance
{
    public int Id { get; set; }

    public int? HfId { get; set; }

    public string? Hfmiscode { get; set; }

    public string? District { get; set; }

    public string? HealthFacilityName { get; set; }

    public string? ModeName { get; set; }

    public string? Lvl { get; set; }

    public string? AmbulanceNo { get; set; }

    public string? Dhiscode { get; set; }
}
