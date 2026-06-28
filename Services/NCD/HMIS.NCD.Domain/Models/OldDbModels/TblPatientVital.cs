using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.OldDbModels;

public partial class TblPatientVital
{
    public int Id { get; set; }

    public double? Temperature { get; set; }

    public double? Pulse { get; set; }

    public double? BpSystolic { get; set; }

    public double? BpDiastolic { get; set; }

    public double? Height { get; set; }

    public double? Weight { get; set; }

    public int? Pid { get; set; }

    public int? UserId { get; set; }

    public int? Created { get; set; }

    public int? Updated { get; set; }
}
