using System;
using System.Collections.Generic;

namespace HMIS.Pathalogy.Domain.Models.DbModels;

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

    public virtual District? District { get; set; }

    public virtual Division? Division { get; set; }

    public virtual ICollection<HealthFacilityStation> HealthFacilityStations { get; } = new List<HealthFacilityStation>();

    public virtual HealthFacilityType? HealthFacilityType { get; set; }

    public virtual ICollection<HfDepartment> HfDepartments { get; } = new List<HfDepartment>();

    public virtual ICollection<PatientOpenVisit> PatientOpenVisits { get; } = new List<PatientOpenVisit>();

    public virtual Province? Province { get; set; }

    public virtual Tehsil? Tehsil { get; set; }

    public virtual UnionCouncil? UnionCouncil { get; set; }

    public virtual ICollection<User> Users { get; } = new List<User>();
}
