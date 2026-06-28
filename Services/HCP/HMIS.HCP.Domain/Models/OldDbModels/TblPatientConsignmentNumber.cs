using System;
using System.Collections.Generic;

namespace HMIS.HCP.Domain.Models.OldDbModels;

public partial class TblPatientConsignmentNumber
{
    public int Id { get; set; }

    public int? PatientId { get; set; }

    public string? ConsignmentNo { get; set; }

    public int Created { get; set; }
}
