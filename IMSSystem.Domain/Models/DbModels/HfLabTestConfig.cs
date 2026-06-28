using System;
using System.Collections.Generic;

namespace IMSSystem.Domain.Models.DbModels;

public partial class HfLabTestConfig
{
    public Guid HfLabTestConfigId { get; set; }

    public int HealthFacilityId { get; set; }

    public int LabTestId { get; set; }

    public bool? IsPerformedPrivately { get; set; }

    public Guid? LabDepartmentProfileId { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedOn { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public byte ActionTypeId { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual User? DeletedByNavigation { get; set; }

    public virtual HealthFacility HealthFacility { get; set; } = null!;

    public virtual Profile? LabDepartmentProfile { get; set; }

    public virtual User? UpdatedByNavigation { get; set; }
}
