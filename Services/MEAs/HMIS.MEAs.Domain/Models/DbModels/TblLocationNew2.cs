using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class TblLocationNew2
{
    public int Id { get; set; }

    public string? NewHfmisCode { get; set; }

    public int? HfmisId { get; set; }

    public string? HfmisName { get; set; }

    public string? HfmisDivisionCode { get; set; }

    public string? HfmisDivisionName { get; set; }

    public string? HfmisDistrictCode { get; set; }

    public string? HfmisDistrictName { get; set; }

    public string? HfmisTehsilCode { get; set; }

    public string? HfmisTehsilName { get; set; }

    public string? DhisFacilityCode { get; set; }

    public string? HfmisFacilityType { get; set; }

    public string? ReportingFacilityType { get; set; }

    public string? PspuReportingFacilityType { get; set; }

    public string? PspuMeaZoneName { get; set; }

    public string? Phase { get; set; }

    public string? Otp { get; set; }

    public string? Ambulances { get; set; }

    public string? Comments { get; set; }
}
