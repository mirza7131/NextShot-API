using System;
using System.Collections.Generic;

namespace HMIS.Dashboard.Domain.Models.DbModels;

public partial class HealthFacility
{
    public int HealthFacilityId { get; set; }

    public int? HrId { get; set; }

    public string? Name { get; set; }

    public string? Code { get; set; }

    public string? HealthFacilityTypeCode { get; set; }

    public int? HealthFacilityTypeId { get; set; }

    public int? ProvinceId { get; set; }

    public string? ProvinceCode { get; set; }

    public int? DivisionId { get; set; }

    public string? DivisionCode { get; set; }

    public int? DistrictId { get; set; }

    public string? DistrictCode { get; set; }

    public int? TehsilId { get; set; }

    public string? TehsilCode { get; set; }

    public int? UnionCouncilId { get; set; }

    public string? UnionCouncilCode { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public long? UserLogId { get; set; }

    public byte ActionTypeId { get; set; }

    public bool? IsRunningHmis { get; set; }

    public bool? IsOffline { get; set; }
}
