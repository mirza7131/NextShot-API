using System;
using System.Collections.Generic;

namespace HMIS.HCP.Domain.Models.OldDbModels;

public partial class TblStockLog
{
    public int Id { get; set; }

    public int? HospitalId { get; set; }

    public int Quantity { get; set; }

    public int? Created { get; set; }

    public int? Updated { get; set; }
}
