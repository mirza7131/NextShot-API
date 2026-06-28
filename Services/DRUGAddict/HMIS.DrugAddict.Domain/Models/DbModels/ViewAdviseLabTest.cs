using System;
using System.Collections.Generic;

namespace HMIS.DrugAddict.Domain.Models.DbModels;

public partial class ViewAdviseLabTest
{
    public string? Mrno { get; set; }

    public Guid PatientLabTestId { get; set; }

    public int VisitNo { get; set; }

    public string? FullName { get; set; }

    public string Cnic { get; set; } = null!;

    public string? MobileNo { get; set; }

    public int ProvinceId { get; set; }

    public int DivisionId { get; set; }

    public string? DivisionCode { get; set; }

    public string? DivisionName { get; set; }

    public int DistrictId { get; set; }

    public string? DistrictCode { get; set; }

    public string? DistrictName { get; set; }

    public int TehsilId { get; set; }

    public string? TehsilCode { get; set; }

    public string? TehsilName { get; set; }

    public int HealthFacilityId { get; set; }

    public string? HealthFacilityName { get; set; }

    public int? DepartementLookupId { get; set; }

    public int? SectionLookupId { get; set; }

    public string? FormType { get; set; }

    public string? TestName { get; set; }

    public bool? IsSampleCollected { get; set; }

    public bool? IsSampleRejected { get; set; }

    public bool? IsReportGenerated { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public byte ActionTypeId { get; set; }

    public bool? IsActive { get; set; }
}
