using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.DbModels;

public partial class MentalHealthPatientFollowup
{
    public Guid MentalHealthPatientFollowupsId { get; set; }

    public DateTime? NextFollowupDate { get; set; }

    public string? ReferredTo { get; set; }

    public Guid MentalHealthPatientId { get; set; }

    public bool Status { get; set; }

    public bool? IsDeleted { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public string? Hfmiscode { get; set; }

    public string? TreatmentOutCome { get; set; }
}
