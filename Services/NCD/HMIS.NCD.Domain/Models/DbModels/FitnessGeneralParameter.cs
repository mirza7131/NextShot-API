using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.DbModels;

public partial class FitnessGeneralParameter
{
    public Guid FitnessGeneralParameterId { get; set; }

    public Guid FitnessCertificateId { get; set; }

    public Guid PatientId { get; set; }

    public Guid? PatientVisitId { get; set; }

    public Guid? PatientDiagnoseId { get; set; }

    public int? HealthFacilityId { get; set; }

    public string? Vision { get; set; }

    public decimal? Chest { get; set; }

    public decimal? Height { get; set; }

    public decimal? Weight { get; set; }

    public string? MarkOfIdentification { get; set; }

    public Guid? PregnancyTestProfileId { get; set; }

    public string? Bsr { get; set; }

    public Guid? HivtestProfileId { get; set; }

    public Guid? HepBtestProfileId { get; set; }

    public Guid? HepCtestProfileId { get; set; }

    public string? Remarks { get; set; }

    public decimal? SalmonellaTyphiO { get; set; }

    public decimal? SalmonellaTyphiH { get; set; }

    public decimal? SalmonellaTyphiAo { get; set; }

    public decimal? SalmonellaTyphiAh { get; set; }

    public decimal? SalmonellaTyphiBo { get; set; }

    public decimal? SalmonellaTyphiBh { get; set; }

    public bool? IsWidal { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public byte ActionTypeId { get; set; }

    public decimal? BpSystolic { get; set; }

    public decimal? BpDiaSystolic { get; set; }
}
