using System;
using System.Collections.Generic;

namespace HMIS.FamilyPlanning.Domain.Models.DbModels;

public partial class ViewSpecialityRoomNo
{
    public int HfDepartmentId { get; set; }

    public int? DepartmentLookupId { get; set; }

    public int? SectionLookupId { get; set; }

    public int? HealthFacilityId { get; set; }

    public string? VitalsFloorNo { get; set; }

    public string? VitalsRoomNo { get; set; }

    public string? DoctorFloorNo { get; set; }

    public string? DoctorRoomNo { get; set; }

    public string? PharmacyFloorNo { get; set; }

    public string? PharmacyRoomNo { get; set; }

    public string? PathalogyFloorNo { get; set; }

    public string? PathalogyRoomNo { get; set; }

    public string? AlmonerFloorNo { get; set; }

    public string? AlmonerRoomNo { get; set; }
}
