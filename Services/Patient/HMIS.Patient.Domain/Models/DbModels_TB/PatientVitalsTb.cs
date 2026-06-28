using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class PatientVitalsTb
{
    public int Id { get; set; }

    public decimal? Weight { get; set; }

    public bool? HivScreened { get; set; }

    public string? Hivresult { get; set; }

    public bool? ReferToArt { get; set; }

    public string? ReferredArt { get; set; }

    public int? ReferredArtid { get; set; }

    public bool? Diabetes { get; set; }

    public int? HouseHoldContacts { get; set; }

    public int? PatientId { get; set; }

    public bool? PatientWillingForHivscreening { get; set; }

    public int? ContactsUnderFive { get; set; }

    public int? VisitId { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreationDate { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public bool? RecordStatus { get; set; }

    public Guid? Guid { get; set; }

    public DateTime? PatientReportingDate { get; set; }

    public string? Reason { get; set; }

    public virtual Patient? Patient { get; set; }

    public virtual Visit? Visit { get; set; }
}
