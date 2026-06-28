using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class Designation
{
    public int DesignationId { get; set; }

    public string DesignationName { get; set; } = null!;

    public bool IsActive { get; set; }

    public int? SequenceNo { get; set; }

    public virtual ICollection<DesignationHfT> DesignationHfTs { get; } = new List<DesignationHfT>();
}
