using System;
using System.Collections.Generic;

namespace HMIS.HCP.Domain.Models.OldDbModels;

public partial class TblBeauticianEvent
{
    public int Id { get; set; }

    public string? MaritalStatus { get; set; }

    public string? FullName { get; set; }

    public string? Cnic { get; set; }

    public string? SpouseName { get; set; }

    public DateTime? Dob { get; set; }

    public float? Age { get; set; }

    public int? Gender { get; set; }

    public string? ContactNumber { get; set; }

    public int? District { get; set; }

    public int? Tehsil { get; set; }

    public int? UserHospital { get; set; }

    public string IsHbvTest { get; set; } = null!;

    public string IsHcvTest { get; set; } = null!;

    public string? AltContactNumber { get; set; }

    public string? UcNumber { get; set; }

    public int? Qualification { get; set; }

    public int? Occupation { get; set; }

    public int? UserId { get; set; }

    public int? Created { get; set; }
}
