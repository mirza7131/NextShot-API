using System;
using System.Collections.Generic;

namespace HMIS.FamilyPlanning.Domain.Models.DbModels;

public partial class PatientConfirmedDisease
{
    public Guid PatientConfirmedDiseaseId { get; set; }

    public string? FormType { get; set; }

    public Guid? PatientDiagnoseId { get; set; }

    public Guid? PatientId { get; set; }

    public Guid? PatientVisitId { get; set; }

    public int? SectionId { get; set; }

    public int? DepartmentId { get; set; }

    public string? Description { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public byte? ActionTypeId { get; set; }

    public bool? IsActive { get; set; }
}
