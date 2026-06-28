using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.OldDbModels;

public partial class Administration
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Designation { get; set; } = null!;

    public int Scale { get; set; }

    public string? PhoneNo { get; set; }

    public string? CellNo { get; set; }

    public int OrderBy { get; set; }

    public int IsDeleted { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime ModifiedOn { get; set; }

    public int? ModifiedBy { get; set; }
}
