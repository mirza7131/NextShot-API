using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.OldDbModels;

public partial class TourObservationDetail
{
    public int Id { get; set; }

    public int TourObservationId { get; set; }

    public string ObservationDetails { get; set; } = null!;
}
