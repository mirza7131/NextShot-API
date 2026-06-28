using System;
using System.Collections.Generic;

namespace HMIS.HCP.Domain.Models.OldDbModels;

public partial class Tour
{
    public int Id { get; set; }

    public string TourTitle { get; set; } = null!;

    public int MonitringZoneId { get; set; }

    public int DivisionId { get; set; }

    public int DistrictId { get; set; }

    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public int VehicleId { get; set; }

    public int DriverId { get; set; }

    public int ApprovalStatus { get; set; }

    public int ApprovedBy { get; set; }

    public int TourStatus { get; set; }

    public int? OrderBy { get; set; }

    public int IsDeleted { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime ModifiedOn { get; set; }

    public int? ModifiedBy { get; set; }
}
