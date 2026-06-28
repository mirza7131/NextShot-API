using System;
using System.Collections.Generic;

namespace HMIS.Dashboard.Domain.Models.TransDbModels;

public partial class HfDepartmentSection
{
    public int HfDepartmentSectionId { get; set; }

    public int? HfDepartmentId { get; set; }

    public int? SectionLookupId { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public long? UserLogId { get; set; }

    public byte ActionTypeId { get; set; }

    public string? VitalsFloorNo { get; set; }

    public string? VitalsRoomNo { get; set; }

    public string? DoctorFloorNo { get; set; }

    public string? DoctorRoomNo { get; set; }

    public string? PharmacyFloorNo { get; set; }

    public string? PharmacyRoomNo { get; set; }

    public string? PathalogyFloorNo { get; set; }

    public string? PathalogyRoomNo { get; set; }

    public int? BedQuantityInWard { get; set; }

    public string? AlmonerFloorNo { get; set; }

    public string? AlmonerRoomNo { get; set; }

    public virtual HfDepartment? HfDepartment { get; set; }

    public virtual SectionLookup? SectionLookup { get; set; }
}
