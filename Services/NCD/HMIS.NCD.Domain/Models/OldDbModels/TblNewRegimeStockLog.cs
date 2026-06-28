using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.OldDbModels;

public partial class TblNewRegimeStockLog
{
    public int Id { get; set; }

    public string? MedicineId { get; set; }

    public int? DistrictId { get; set; }

    public int? TehsilId { get; set; }

    public int? HospitalId { get; set; }

    public int? Quantity { get; set; }

    public int? Pid { get; set; }

    public int? Created { get; set; }

    public int? CreatedBy { get; set; }
}
