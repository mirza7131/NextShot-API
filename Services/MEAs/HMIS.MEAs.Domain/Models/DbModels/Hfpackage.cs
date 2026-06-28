using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class Hfpackage
{
    public int Id { get; set; }

    public int? BundleId { get; set; }

    public int? PackageId { get; set; }

    public string? PackName { get; set; }

    public int? UserId { get; set; }

    public string? Name { get; set; }

    public int? Hfid { get; set; }

    public string? Hfname { get; set; }

    public string? HftypeCode { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsDelete { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? UpdatedBy { get; set; }
}
