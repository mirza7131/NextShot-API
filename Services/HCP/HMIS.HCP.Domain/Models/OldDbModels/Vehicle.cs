using System;
using System.Collections.Generic;

namespace HMIS.HCP.Domain.Models.OldDbModels;

public partial class Vehicle
{
    public int Id { get; set; }

    public string RegNo { get; set; } = null!;

    public string Make { get; set; } = null!;

    public string Modal { get; set; } = null!;

    public string Type { get; set; } = null!;

    public int OrderBy { get; set; }

    public int IsDeleted { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime ModifiedOn { get; set; }

    public int? ModifiedBy { get; set; }
}
