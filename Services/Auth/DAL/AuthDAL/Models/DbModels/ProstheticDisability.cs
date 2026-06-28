using System;
using System.Collections.Generic;

namespace AuthDAL.Models.DbModels;

public partial class ProstheticDisability
{
    public Guid ProstheticDisabilityId { get; set; }

    public Guid DisabilityProfileId { get; set; }

    public Guid LevelOfAmputationDisabilityId { get; set; }

    public bool IsActive { get; set; }

    public int ActionTypeId { get; set; }

    public DateTime CreatedOn { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }
}
