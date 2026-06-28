using System;
using System.Collections.Generic;

namespace IMSSystem.Domain.Models.DbModels;

public partial class EmployeesListForInvoice
{
    public Guid? Id { get; set; }

    public Guid? SpId { get; set; }

    public int? HfId { get; set; }

    public Guid? ServiceTypeId { get; set; }

    public string? Name { get; set; }

    public string? Designation { get; set; }

    public int? ShiftId { get; set; }

    public int? EmploymentTypeId { get; set; }

    public string? EmploymentType { get; set; }

    public string? Shifts { get; set; }

    public bool? IsActive { get; set; }
}
