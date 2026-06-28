using System;
using System.Collections.Generic;

namespace HMIS.HCP.Domain.Models.OldDbModels;

public partial class TblHouseholdContact
{
    public int Id { get; set; }

    public int? MicroScreenId { get; set; }

    public string? Name { get; set; }

    public string? Dob { get; set; }

    public int? GenderId { get; set; }

    public string? Cnic { get; set; }

    public string? ContactNo { get; set; }

    public int? RelationId { get; set; }

    public int? CreatedBy { get; set; }

    public int? TotalHouseheld { get; set; }
}
