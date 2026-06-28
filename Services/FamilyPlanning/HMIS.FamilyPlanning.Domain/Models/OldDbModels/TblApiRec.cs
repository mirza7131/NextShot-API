using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.OldDbModels;

public partial class TblApiRec
{
    public int Id { get; set; }

    public string? TokenNo { get; set; }

    public string? UniqueId { get; set; }

    public string? FirstName { get; set; }

    public string? SwoName { get; set; }

    public string? Dob { get; set; }

    public string? MaritalStatus { get; set; }

    public string? Gender { get; set; }

    public string? ContactNo { get; set; }

    public string? Cnic { get; set; }

    public string? Occupation { get; set; }

    public string? Qualification { get; set; }

    public string? Address { get; set; }

    public string? District { get; set; }

    public string? Tehsil { get; set; }

    public string? HbvTest { get; set; }

    public string? HcvTest { get; set; }

    public string? RegDate { get; set; }

    public string? Created { get; set; }

    public string? Status { get; set; }
}
