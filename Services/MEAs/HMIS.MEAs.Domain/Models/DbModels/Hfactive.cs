using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class Hfactive
{
    public int HfActiveId { get; set; }

    public int? HfId { get; set; }

    public bool? IsActive { get; set; }

    public string? Remarks { get; set; }

    public DateTime? InActiveTill { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }
}
