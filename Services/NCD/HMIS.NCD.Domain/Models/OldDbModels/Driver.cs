using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.OldDbModels;

public partial class Driver
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string CellNo { get; set; } = null!;

    public string LicenceType { get; set; } = null!;

    public string CnicNo { get; set; } = null!;

    public int OrderBy { get; set; }

    public int IsDeleted { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime ModifiedOn { get; set; }

    public int? ModifiedBy { get; set; }
}
