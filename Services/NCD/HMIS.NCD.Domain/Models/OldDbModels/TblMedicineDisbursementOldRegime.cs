using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.OldDbModels;

public partial class TblMedicineDisbursementOldRegime
{
    public int Id { get; set; }

    public int? PatientId { get; set; }

    public string DoseDeliveredByHand { get; set; } = null!;

    public int? NoOfMedGiven { get; set; }

    public string PatientDemote { get; set; } = null!;

    public string? DemoteStatus { get; set; }

    public DateTime Created { get; set; }
}
