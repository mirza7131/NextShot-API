using System;
using System.Collections.Generic;

namespace HMIS.HCP.Domain.Models.OldDbModels;

public partial class TourFacility
{
    public int Id { get; set; }

    public int TourId { get; set; }

    public int FacilityId { get; set; }

    public int TeamId { get; set; }

    public int? TeamLeaderId { get; set; }

    public DateTime FacilityVisitDate { get; set; }

    public int? FacilityVisitDays { get; set; }

    public int? ComplianceStatus { get; set; }

    public DateTime? ComplianceDate { get; set; }

    public int ComplianceBy { get; set; }

    public DateTime? ComplianceSubmitDate { get; set; }

    public int IsDeleted { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime ModifiedOn { get; set; }

    public int? ModifiedBy { get; set; }
}
