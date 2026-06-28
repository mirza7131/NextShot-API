using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.OldDbModels;

public partial class ApprovalStatus
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public int OrderBy { get; set; }

    public int IsDeleted { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime ModifiedOn { get; set; }

    public int? ModifiedBy { get; set; }
}
