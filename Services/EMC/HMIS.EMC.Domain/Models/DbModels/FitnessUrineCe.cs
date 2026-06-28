using System;
using System.Collections.Generic;

namespace HMIS.EMC.Domain.Models.DbModels;

public partial class FitnessUrineCe
{
    public Guid FitnessUrineCeid { get; set; }

    public Guid FitnessCertificateId { get; set; }

    public Guid PatientId { get; set; }

    public Guid? PatientVisitId { get; set; }

    public Guid? PatientDiagnoseId { get; set; }

    public int? HealthFacilityId { get; set; }

    public string? Color { get; set; }

    public decimal? SpecificGravity { get; set; }

    public decimal? Ph { get; set; }

    public decimal? Protien { get; set; }

    public decimal? Glucose { get; set; }

    public decimal? Ketones { get; set; }

    public decimal? Urobilinogen { get; set; }

    public decimal? PussCells { get; set; }

    public decimal? Rbcs { get; set; }

    public decimal? Crystals { get; set; }

    public decimal? EpethlialCells { get; set; }

    public decimal? Bacteria { get; set; }

    public decimal? Casts { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public byte ActionTypeId { get; set; }
}
