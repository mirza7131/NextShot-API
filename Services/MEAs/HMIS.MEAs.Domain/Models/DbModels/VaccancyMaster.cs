using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class VaccancyMaster
{
    public int MasterId { get; set; }

    public int? ApplicationTypeId { get; set; }

    public int? HealthFacilityTypeId { get; set; }

    public int? ShiftTypeId { get; set; }

    public string? HfmisCode { get; set; }

    public double? Longitude { get; set; }

    public double? Latitude { get; set; }

    public bool? FacilityStatus { get; set; }

    public bool? IllegalOccupation { get; set; }

    public string? WholeOrPart { get; set; }

    public string? CloseReason { get; set; }

    public string? Comment { get; set; }

    public DateTime? IllegalOccupationSince { get; set; }

    public bool? IsActive { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? SyncBy { get; set; }

    public DateTime? SyncOn { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public string? InchargeName { get; set; }

    public int? InchargeDesignation { get; set; }

    public string? InchargeMobileNo { get; set; }

    public string? InchargeCnic { get; set; }

    public virtual ICollection<Employess> Employesses { get; } = new List<Employess>();

    public virtual ICollection<Vacancy> Vacancies { get; } = new List<Vacancy>();
}
