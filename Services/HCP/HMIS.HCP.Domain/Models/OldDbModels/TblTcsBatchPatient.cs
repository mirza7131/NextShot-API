using System;
using System.Collections.Generic;

namespace HMIS.HCP.Domain.Models.OldDbModels;

public partial class TblTcsBatchPatient
{
    public string? BatchId { get; set; }

    public int? PatientId { get; set; }

    public long? ConsignmentNo { get; set; }

    public int? NoOfDosage { get; set; }
}
