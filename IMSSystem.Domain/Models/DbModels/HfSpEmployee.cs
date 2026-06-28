using System;
using System.Collections.Generic;

namespace IMSSystem.Domain.Models.DbModels;

public partial class HfSpEmployee
{
    public Guid HfSpEmployeeId { get; set; }

    public Guid? SpId { get; set; }

    public Guid? ServiceTypeId { get; set; }

    public int? HfId { get; set; }

    public Guid? DesignationId { get; set; }

    public int? EmploymentTypeId { get; set; }

    public int? ShiftId { get; set; }

    public Guid? EmployeeId { get; set; }

    public Guid? ReplacementOf { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CraetedBy { get; set; }

    public bool? IsUpdated { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }
}
