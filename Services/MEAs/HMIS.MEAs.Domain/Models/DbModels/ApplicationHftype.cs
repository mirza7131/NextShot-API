using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class ApplicationHftype
{
    public int ApplicationHfTypeId { get; set; }

    public int? ApplicationId { get; set; }

    public int? HfTypeId { get; set; }

    public virtual ApplicationType? Application { get; set; }

    public virtual HealthFacilityType? HfType { get; set; }
}
