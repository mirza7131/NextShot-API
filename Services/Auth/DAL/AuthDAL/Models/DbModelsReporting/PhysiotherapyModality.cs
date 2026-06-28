using System;
using System.Collections.Generic;

namespace AuthDAL.Models.DbModelsReporting;

public partial class PhysiotherapyModality
{
    public Guid PhysiotherapyModalitiesId { get; set; }

    public Guid PhysiotherapyFormId { get; set; }

    public int? DepartmentLookupId { get; set; }

    public Guid? ModalitiesProfileId { get; set; }

    public string? Name { get; set; }

    public string? Value { get; set; }

    public bool IsActive { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public byte ActionTypeId { get; set; }
}
