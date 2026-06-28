using System;
using System.Collections.Generic;

namespace HMIS.FamilyPlanning.Domain.Models.DbModels;

public partial class PatientRefer
{
    public Guid PatientReferId { get; set; }

    public Guid? PatientId { get; set; }

    public Guid? PatientVisitId { get; set; }

    public Guid? PatientDiagnoseId { get; set; }

    public Guid? ReferTypeId { get; set; }

    public int? ReferHealthFacilityId { get; set; }

    public string? Reason { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public bool? IsActive { get; set; }

    public byte? ActionTypeId { get; set; }
}
