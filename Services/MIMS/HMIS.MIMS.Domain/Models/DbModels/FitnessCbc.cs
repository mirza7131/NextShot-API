using System;
using System.Collections.Generic;

namespace HMIS.MIMS.Domain.Models.DbModels;

public partial class FitnessCbc
{
    public Guid FitnessCbcid { get; set; }

    public Guid FitnessCertificateId { get; set; }

    public Guid PatientId { get; set; }

    public Guid? PatientVisitId { get; set; }

    public Guid? PatientDiagnoseId { get; set; }

    public int? HealthFacilityId { get; set; }

    public string? Hb { get; set; }

    public string? Mcv { get; set; }

    public string? Hcv { get; set; }

    public decimal? Tlc { get; set; }

    public decimal? Neutorphils { get; set; }

    public decimal? Lymphocytes { get; set; }

    public decimal? Eosinophils { get; set; }

    public decimal? Platelets { get; set; }

    public decimal? Esr { get; set; }

    public decimal? Monocytes { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public byte ActionTypeId { get; set; }
}
