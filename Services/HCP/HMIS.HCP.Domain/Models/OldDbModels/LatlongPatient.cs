using System;
using System.Collections.Generic;

namespace HMIS.HCP.Domain.Models.OldDbModels;

public partial class LatlongPatient
{
    public int? PatientId { get; set; }

    public string? PostalAddress { get; set; }

    public string? Tehsil { get; set; }

    public string? District { get; set; }

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }
}
