using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.OldDbModels;

public partial class Division
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Code { get; set; } = null!;

    public string ShortCode { get; set; } = null!;

    public string IsSpecial { get; set; } = null!;

    public int? ZoneId { get; set; }

    public int OrderBy { get; set; }

    public int? IsDeleted { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime ModifiedOn { get; set; }

    public int? ModifiedBy { get; set; }
}
