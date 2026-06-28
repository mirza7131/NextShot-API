using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class TblAmbulance
{
    public int Id { get; set; }

    public string? District { get; set; }

    public int? HfId { get; set; }

    public string? HfmisName { get; set; }

    public string? Dhisname { get; set; }

    public string? AmbulanceNo { get; set; }

    public string? Parking { get; set; }
}
