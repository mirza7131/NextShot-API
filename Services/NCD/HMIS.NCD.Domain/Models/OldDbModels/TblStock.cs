using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.OldDbModels;

public partial class TblStock
{
    public int Id { get; set; }

    public int? HospitalId { get; set; }

    public int Remaining { get; set; }
}
