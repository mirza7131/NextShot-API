using System;
using System.Collections.Generic;

namespace AuthDAL.Models.DbModels;

public partial class FitnessStoolExamination
{
    public Guid FitnessStoolExaminationId { get; set; }

    public Guid FitnessCertificateId { get; set; }

    public Guid PatientId { get; set; }

    public Guid? PatientVisitId { get; set; }

    public Guid? PatientDiagnoseId { get; set; }

    public int? HealthFacilityId { get; set; }

    public string? Colour { get; set; }

    public string? Consistency { get; set; }

    public decimal? Mucus { get; set; }

    public decimal? Blood { get; set; }

    public decimal? PussCells { get; set; }

    public decimal? Ova { get; set; }

    public decimal? Rbcs { get; set; }

    public decimal? VegetativeForms { get; set; }

    public decimal? OccultBlood { get; set; }

    public string? XrayChestPaview { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public byte ActionTypeId { get; set; }
}
