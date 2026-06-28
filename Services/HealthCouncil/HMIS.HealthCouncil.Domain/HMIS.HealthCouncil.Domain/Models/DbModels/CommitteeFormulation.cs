using System;
using System.Collections.Generic;

namespace HMIS.HealthCouncil.Domain.Models.DbModels;

public partial class CommitteeFormulation
{
    public Guid CommitteeFormulationId { get; set; }

    public string? Name { get; set; }

    public string? Cnic { get; set; }

    public string? Designation { get; set; }

    public Guid? MeetingRoleProfileId { get; set; }

    public int? HealthFacilityId { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public bool? IsActive { get; set; }

    public byte? ActionTypeId { get; set; }
}
