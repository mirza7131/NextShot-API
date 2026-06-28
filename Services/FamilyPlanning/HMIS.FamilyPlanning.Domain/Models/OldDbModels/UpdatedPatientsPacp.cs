using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.OldDbModels;

public partial class UpdatedPatientsPacp
{
    public int? Id { get; set; }

    public string? Emrno { get; set; }

    public string? Name { get; set; }

    public string? FatherName { get; set; }

    public string? Cnic { get; set; }

    public string? Contact { get; set; }

    public string? Address { get; set; }

    public string? HealthFacility { get; set; }
}
