using System;
using System.Collections.Generic;

namespace AuthDAL.Models.DbModels;

public partial class MentalHealthAssessment
{
    public Guid MentalHealthAssessmentId { get; set; }

    public Guid? PatinetId { get; set; }

    public Guid? PatientVisitId { get; set; }

    public string? Question { get; set; }

    public string? Answer { get; set; }

    public bool? Status { get; set; }

    public string? AssessmentType { get; set; }

    public string? MentalType { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public byte? ActionTypeId { get; set; }
}
