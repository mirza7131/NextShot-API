using System;
using System.Collections.Generic;

namespace HMIS.EMC.Domain.Models.DbModels;

public partial class ViewPatientLastAssessment
{
    public Guid? PatinetId { get; set; }

    public string? AssessmentType { get; set; }

    public Guid? PatientVisitId { get; set; }

    public string? TreatmentOutCome { get; set; }

    public DateTime? Createdon { get; set; }
}
