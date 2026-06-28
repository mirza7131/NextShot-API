using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.OldDbModels;

public partial class TblPatientHistory
{
    public int Id { get; set; }

    public int? PatientId { get; set; }

    public int? TransferId { get; set; }

    public int? TransferInFacility { get; set; }

    public int? TransferOutFacility { get; set; }

    public int? Created { get; set; }
}
