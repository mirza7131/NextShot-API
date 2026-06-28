using System;
using System.Collections.Generic;

namespace HMIS.MIMS.Domain.Models.DbModels;

public partial class ViewPatientOpenVisitCountByDeptBySecByMonth
{
    public string? DepartmentName { get; set; }

    public int DepartmentId { get; set; }

    public string? SectionName { get; set; }

    public int SectionId { get; set; }

    public int? VisitCount { get; set; }

    public int? HealthFacilityId { get; set; }

    public int? VisitMonth { get; set; }
}
