using System;
using System.Collections.Generic;

namespace HMIS.FamilyPlanning.Domain.Models.DbModels;

public partial class FitnessSerology
{
    public Guid FitnessSerologyId { get; set; }

    public Guid FitnessCertificateId { get; set; }

    public Guid PatientId { get; set; }

    public Guid? PatientVisitId { get; set; }

    public Guid? PatientDiagnoseId { get; set; }

    public int? HealthFacilityId { get; set; }

    public string? SerologyTestOne { get; set; }

    public Guid? SerologyTestOneValueTypeProfileId { get; set; }

    public string? SerologyTestTwo { get; set; }

    public Guid? SerologyTestTwoValueTypeProfileId { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public byte ActionTypeId { get; set; }
}
