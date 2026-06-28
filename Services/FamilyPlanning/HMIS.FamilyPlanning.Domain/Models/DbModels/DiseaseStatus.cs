using System;
using System.Collections.Generic;

namespace HMIS.FamilyPlanning.Domain.Models.DbModels;

public partial class DiseaseStatus
{
    public Guid DiseaseStatusId { get; set; }

    public Guid PatientDiagnoseDiseaseId { get; set; }

    public Guid DiseaseStatusTypeProfileId { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public int? ActionTypeId { get; set; }
}
