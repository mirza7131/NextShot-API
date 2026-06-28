using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class CategoriesFlood
{
    public int CategoryId { get; set; }

    public int? ApplicationTypeId { get; set; }

    public int? ModuleId { get; set; }

    public int? SequenceNo { get; set; }

    public string? CategoryName { get; set; }

    public bool? IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public bool? IsRequired { get; set; }
}
