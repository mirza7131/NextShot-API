using System;
using System.Collections.Generic;

namespace HMIS.HCP.Domain.Models.OldDbModels;

public partial class TourTeamMember
{
    public int Id { get; set; }

    public int TourId { get; set; }

    public int? FacilityId { get; set; }

    public int TeamId { get; set; }

    public int MemberId { get; set; }

    public int MemberAreaId { get; set; }

    public int IsDeleted { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime ModifiedOn { get; set; }

    public int? ModifiedBy { get; set; }
}
