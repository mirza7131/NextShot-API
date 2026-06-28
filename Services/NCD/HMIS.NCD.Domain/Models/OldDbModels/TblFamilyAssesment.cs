using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.OldDbModels;

public partial class TblFamilyAssesment
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Cnic { get; set; }

    public string? Contact { get; set; }

    public string? Relation { get; set; }

    public int ParentId { get; set; }

    public string PreviousHbvTest { get; set; } = null!;

    public string PreviousHcvTest { get; set; } = null!;

    public string PcrConfirmationHbv { get; set; } = null!;

    public string PcrConfirmationHcv { get; set; } = null!;

    public DateTime? Dob { get; set; }

    public DateTime Created { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? Updated { get; set; }

    public int? UpdatedBy { get; set; }

    public string? MrnNo { get; set; }

    public int? Age { get; set; }
}
