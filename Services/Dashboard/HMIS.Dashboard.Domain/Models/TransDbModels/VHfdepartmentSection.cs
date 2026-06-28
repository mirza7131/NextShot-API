using System;
using System.Collections.Generic;

namespace HMIS.Dashboard.Domain.Models.TransDbModels;

public partial class VHfdepartmentSection
{
    public int? HealthFacilityId { get; set; }

    public string? HealthFacilityName { get; set; }

    public int? HfDepartmentId { get; set; }

    public int HfDepartmentSectionId { get; set; }

    public string? DepartmentName { get; set; }

    public string? SectionName { get; set; }
}
