using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class HealthFacilityType
{
    public int FacilityTypeId { get; set; }

    public string? FaciltyTypeName { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<ApplicationHftype> ApplicationHftypes { get; } = new List<ApplicationHftype>();

    public virtual ICollection<CategoryHfType> CategoryHfTypes { get; } = new List<CategoryHfType>();

    public virtual ICollection<DesignationHfT> DesignationHfTs { get; } = new List<DesignationHfT>();

    public virtual ICollection<Hfshift> Hfshifts { get; } = new List<Hfshift>();

    public virtual ICollection<IndicatorHf> IndicatorHfs { get; } = new List<IndicatorHf>();
}
