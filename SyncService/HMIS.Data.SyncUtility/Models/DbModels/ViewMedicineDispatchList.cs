using System;
using System.Collections.Generic;

namespace HMIS.Data.SyncUtility.Models.DbModels;

public partial class ViewMedicineDispatchList
{
    public int ProvinceId { get; set; }

    public string? ProvinceName { get; set; }

    public int DivisionId { get; set; }

    public string? DivisionName { get; set; }

    public int DistrictId { get; set; }

    public string? DistrictName { get; set; }

    public int TehsilId { get; set; }

    public string? TehsilName { get; set; }

    public int HealthFacilityId { get; set; }

    public string? HealthFacilityName { get; set; }

    public Guid PatientVisitId { get; set; }

    public Guid? PatientId { get; set; }

    public DateTime? VisitDate { get; set; }

    public string? PatientName { get; set; }

    public string? PatientMobileNo { get; set; }

    public string? Mrno { get; set; }

    public string Cnic { get; set; } = null!;

    public Guid? CreatedBy { get; set; }

    public string? CreatedByName { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? UpdatedByName { get; set; }

    public DateTime? UpdatedOn { get; set; }
}
