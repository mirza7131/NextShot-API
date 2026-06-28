using System;
using System.Collections.Generic;

namespace HMIS.HCP.Domain.Models.OldDbModels;

public partial class City
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Code { get; set; }

    public string ShortCode { get; set; } = null!;

    public int DistrictId { get; set; }

    public int OrderBy { get; set; }

    public int IsDeleted { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime ModifiedOn { get; set; }

    public int? ModifiedBy { get; set; }
}
