using System;
using System.Collections.Generic;

namespace HMIS.FamilyPlanning.Domain.Models.DbModels;

public partial class HfDepartment
{
    public int HfDepartmentId { get; set; }

    public int? HealthFacilityId { get; set; }

    public int? DepartmentLookupId { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public long? UserLogId { get; set; }

    public byte ActionTypeId { get; set; }

    public bool IsRequisition { get; set; }

    public virtual DepartmentLookup? DepartmentLookup { get; set; }

    public virtual HealthFacility? HealthFacility { get; set; }

    public virtual ICollection<HfDepartmentSection> HfDepartmentSections { get; } = new List<HfDepartmentSection>();
}
