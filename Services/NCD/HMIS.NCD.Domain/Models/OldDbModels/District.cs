using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.OldDbModels;

public partial class District
{
    public int Id { get; set; }

    public string District1 { get; set; } = null!;

    public string? Code { get; set; }

    public string ShortCode { get; set; } = null!;

    public int? DivisionId { get; set; }

    public string? CeoName { get; set; }

    public string? CeoPhone { get; set; }

    public string? CeoCell { get; set; }

    public string? CeoEmail { get; set; }

    public string? DhoName { get; set; }

    public string? DhoPhone { get; set; }

    public string? DhoCell { get; set; }

    public string? DhoEmail { get; set; }

    public int OrderBy { get; set; }

    public int? IsDeleted { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime ModifiedOn { get; set; }

    public int? ModifiedBy { get; set; }
}
