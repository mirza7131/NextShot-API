using System;
using System.Collections.Generic;

namespace HMIS.HCP.Domain.Models.OldDbModels;

public partial class TblReferHospital
{
    public int Id { get; set; }

    public string ClinicName { get; set; } = null!;

    public string ClinicCode { get; set; } = null!;

    public string? City { get; set; }

    public int? ProvinceId { get; set; }

    public int? DistrictId { get; set; }

    public int? TehsilId { get; set; }

    public string? ClinicAddress { get; set; }

    public string? ClinicLat { get; set; }

    public string? ClinicLon { get; set; }

    public string IsPacslink { get; set; } = null!;

    public string? Status { get; set; }

    public int? Created { get; set; }

    public int? Updated { get; set; }

    public int? ClinicCodeNo { get; set; }

    public string? ClinicType { get; set; }

    public string? FacilityType { get; set; }
}
