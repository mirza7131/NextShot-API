using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class Vacancy
{
    public int VacancyId { get; set; }

    public string? VacancyTitle { get; set; }

    public int? TotalEmployess { get; set; }

    public bool? IsActive { get; set; }

    public int? MasterId { get; set; }

    public virtual VaccancyMaster? Master { get; set; }
}
