using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels;

public partial class ViewSocialWellfarePatientDetail
{
    public Guid SocialWelfareFormId { get; set; }

    public Guid? PatientVistId { get; set; }

    public int? ReferDistrictId { get; set; }

    public string? MdrcindoorActivities { get; set; }

    public int? ReferDivisionId { get; set; }

    public string? FullName { get; set; }

    public bool? IsVisitClosed { get; set; }

    public string? Mdrcprofession { get; set; }

    public string? MdrcmonthlyIncome { get; set; }

    public string? MdrcfhrdrugAddiction { get; set; }

    public string? MdrcifYesRelation { get; set; }

    public string? MdrcfamilyAttitude { get; set; }

    public string? MdrcpatientAttitude { get; set; }

    public int? SessionNo { get; set; }

    public string? MdrcmsoprovisionReadingMaterial { get; set; }

    public string? MdrcrecreationalActivities { get; set; }

    public string? MdrcanyOtherMso { get; set; }

    public string? MobileNo { get; set; }

    public string? Mrno { get; set; }

    public string? LastName { get; set; }

    public string? FirstName { get; set; }

    public string Cnic { get; set; } = null!;

    public string? MdrcdetailOfCounsellingSessionsSesssionI { get; set; }

    public DateTime? VisitDate { get; set; }

    public DateTime? Dob { get; set; }

    public decimal? Age { get; set; }

    public string Gender { get; set; } = null!;

    public string? VisitHf { get; set; }

    public string? PatientHf { get; set; }

    public string Relation { get; set; } = null!;

    public string? PatientDivisionName { get; set; }

    public int PatientDivisionId { get; set; }

    public string? PatientProvinceName { get; set; }

    public string? PatientDistrictName { get; set; }

    public int PatientDistrctId { get; set; }

    public string? PatientTehsilName { get; set; }

    public DateTime? PatientCreatedOn { get; set; }

    public DateTime? PatientVisitCreatedOn { get; set; }

    public int? HealthFacilityProvinceId { get; set; }

    public int? HealthFacilityDivisionId { get; set; }

    public int? HealthFacilityDistrictId { get; set; }

    public int? HealthFacilityTehsilId { get; set; }

    public int HealthFacilityId { get; set; }

    public int? DepartementLookupId { get; set; }

    public int? SectionLookupId { get; set; }

    public string? PatientVisitCreatedByName { get; set; }

    public byte ActionTypeId { get; set; }

    public DateTime? CreatedOn { get; set; }
}
