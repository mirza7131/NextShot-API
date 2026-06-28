using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.OldDbModels;

public partial class Observation
{
    public int Id { get; set; }

    public string Observation1 { get; set; } = null!;

    public int MonitringAreaId { get; set; }

    public int SubAreaId { get; set; }

    public int Sequence { get; set; }

    public int? OrderBy { get; set; }

    public int? IsDeleted { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime ModifiedOn { get; set; }

    public int? ModifiedBy { get; set; }
}
