using System;
using System.Collections.Generic;

namespace HMIS.HCP.Domain.Models.OldDbModels;

public partial class TblUser
{
    public int Id { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? FullName { get; set; }

    public int RoleId { get; set; }

    public int DivisionId { get; set; }

    public int DistrictId { get; set; }

    public int TehsilId { get; set; }

    public int? HospitalId { get; set; }

    public string? HospitalName { get; set; }

    public string? Usercnic { get; set; }

    public string? Identifier { get; set; }

    public int? StartRange { get; set; }

    public int? EndRange { get; set; }

    public int? CreatedBy { get; set; }

    public string? Status { get; set; }

    public int? Created { get; set; }

    public int? Updated { get; set; }

    public string? Imei { get; set; }

    public string? IsEventUser { get; set; }

    public string? IsLoggedIn { get; set; }

    public string? IsJailUser { get; set; }
}
