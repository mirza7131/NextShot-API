using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class Employess
{
    public int EmployeeId { get; set; }

    public string? Name { get; set; }

    public string? Cnic { get; set; }

    public string? MobileNo { get; set; }

    public string? Shift { get; set; }

    public string? Option { get; set; }

    public int? VacancyId { get; set; }

    public string? HfmisCode { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? SyncOn { get; set; }

    public int? SyncBy { get; set; }

    public int? MasterId { get; set; }

    public string? VacancyTitle { get; set; }

    public int? TotalEmployees { get; set; }

    public string? OriginalPostingFacility { get; set; }

    public string? OriginalPosting { get; set; }

    public virtual VaccancyMaster? Master { get; set; }
}
