using System;
using System.Collections.Generic;

namespace HMIS.Dashboard.Domain.Models.DbModels;

public partial class PatientContactDetail
{
    public Guid ContactId { get; set; }

    public Guid? PatientId { get; set; }

    public Guid? PatientVisitId { get; set; }

    public int? DepartmentLookupId { get; set; }

    public int? SectionLookupId { get; set; }

    public string ContactName { get; set; } = null!;

    public string ContactNo { get; set; } = null!;

    public string Relation { get; set; } = null!;

    public int Age { get; set; }

    public int? ProvinceId { get; set; }

    public int? DivisionId { get; set; }

    public int? DistrictId { get; set; }

    public int? TehsilId { get; set; }

    public bool? IsSputumCollected { get; set; }

    public Guid? CollectedBy { get; set; }

    public byte ActionTypeId { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedOn { get; set; }
}
