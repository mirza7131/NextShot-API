using System;
using System.Collections.Generic;

namespace AuthDAL.Models.DbModelsReporting;

public partial class ViewHealthFacilityDepartmentList
{
    public int HfDepartmentId { get; set; }

    public int ProvinceId { get; set; }

    public int DivisionId { get; set; }

    public int DistrictId { get; set; }

    public int TehsilId { get; set; }

    public int? HealthFacilityId { get; set; }

    public string? HealthFacilityName { get; set; }

    public int? DepartmentLookupId { get; set; }

    public string? DepartmentName { get; set; }

    public Guid? CreatedBy { get; set; }

    public string? CreatedByName { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public string? UpdatedByName { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public bool IsActive { get; set; }
}
