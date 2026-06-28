using System;
using System.Collections.Generic;

namespace AuthDAL.Models.DbModels;

public partial class PatientOpenVisitDetail
{
    public string? FullName { get; set; }

    public string? LastName { get; set; }

    public string? FirstName { get; set; }

    public DateTime? VisitDate { get; set; }

    public DateTime? Dob { get; set; }

    public decimal? Age { get; set; }

    public string Gender { get; set; } = null!;

    public string? VisitHf { get; set; }

    public string? PatientHf { get; set; }

    public string Relation { get; set; } = null!;

    public string? PatientDivisionName { get; set; }

    public string? PatientProvinceName { get; set; }

    public string? PatientDistrictName { get; set; }

    public string? PatientTehsilName { get; set; }

    public DateTime? PatientCreatedOn { get; set; }

    public DateTime? PatientVisitCreatedOn { get; set; }
}
