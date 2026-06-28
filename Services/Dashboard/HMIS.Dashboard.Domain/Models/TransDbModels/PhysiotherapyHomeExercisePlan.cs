using System;
using System.Collections.Generic;

namespace HMIS.Dashboard.Domain.Models.TransDbModels;

public partial class PhysiotherapyHomeExercisePlan
{
    public Guid PhysiotherapyHomeExercisePlanId { get; set; }

    public Guid? PhysiotherapyFormId { get; set; }

    public Guid? HomeExersicePlanProfileId { get; set; }

    public string? Name { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public byte? ActionTypeId { get; set; }
}
