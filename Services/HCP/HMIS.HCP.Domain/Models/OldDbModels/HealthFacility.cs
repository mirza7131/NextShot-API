using System;
using System.Collections.Generic;

namespace HMIS.HCP.Domain.Models.OldDbModels;

public partial class HealthFacility
{
    public int Id { get; set; }

    public int FacilityId { get; set; }

    public string Title { get; set; } = null!;

    public int? TypeId { get; set; }

    public int DistrictId { get; set; }

    public string? Address { get; set; }

    public string? PhoneNo { get; set; }

    public string? CellNo { get; set; }

    public string? SmoName { get; set; }

    public string? SmoDesignation { get; set; }

    public string? SmoPhoneNo { get; set; }

    public string? SmoCellNo { get; set; }

    public string? SmoEmail { get; set; }

    public int? OrderBy { get; set; }

    public int? IsDeleted { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime ModifiedOn { get; set; }

    public int? ModifiedBy { get; set; }
}
