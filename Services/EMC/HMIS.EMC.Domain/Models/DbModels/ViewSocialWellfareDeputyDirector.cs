using System;
using System.Collections.Generic;

namespace HMIS.EMC.Domain.Models.DbModels;

public partial class ViewSocialWellfareDeputyDirector
{
    public Guid PatientId { get; set; }

    public string? FullName { get; set; }

    public string Gender { get; set; } = null!;

    public Guid SocialWellfareFormId { get; set; }

    public bool? IsDrugAddict { get; set; }

    public string Cnic { get; set; } = null!;

    public string? Mrno { get; set; }

    public string? LastName { get; set; }

    public decimal? Age { get; set; }

    public Guid? PatientVistId { get; set; }

    public string? MobileNo { get; set; }

    public int PatientDistrctId { get; set; }

    public string? PatientDivisionName { get; set; }

    public int PatientDivisionId { get; set; }

    public DateTime? VisitDate { get; set; }

    public int HealthFacilityId { get; set; }

    public string? VisitHf { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? Dob { get; set; }

    public int TehsilId { get; set; }

    public string? PatientDistrictName { get; set; }

    public bool? IsAssignDoctor { get; set; }

    public int? DivisionId { get; set; }

    public int? DistrictId { get; set; }

    public int? ReferDistrictId { get; set; }

    public bool? IsVisitClosed { get; set; }

    public string? AssignedDoctor { get; set; }

    public Guid? DoctorId { get; set; }

    public int? SessionNo { get; set; }

    public string? PtStatus { get; set; }
}
