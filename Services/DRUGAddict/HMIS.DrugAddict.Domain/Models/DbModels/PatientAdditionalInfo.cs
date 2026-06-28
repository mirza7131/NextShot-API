using System;
using System.Collections.Generic;

namespace HMIS.DrugAddict.Domain.Models.DbModels;

public partial class PatientAdditionalInfo
{
    public Guid PatientAdditionalInfoId { get; set; }

    public Guid? PatientId { get; set; }

    public Guid? PatientStatusProfileId { get; set; }

    public int? HealthFacilityId { get; set; }

    public int? DepartmentLookupId { get; set; }

    public int? SectionLookupId { get; set; }

    public Guid? CasteTypeProfileId { get; set; }

    public Guid? AccupationTypeProfileId { get; set; }

    public string? GuardianName { get; set; }

    public string? GuardianCnic { get; set; }

    public string? GuardianAddress { get; set; }

    public string? GuardianMobileNo { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public byte ActionTypeId { get; set; }

    public virtual Patient? Patient { get; set; }
}
