using System;
using System.Collections.Generic;

namespace HMIS.Dashboard.Domain.Models.DbModels;

public partial class ViewDrugAddictCommunityDevelopment
{
    public string? FullName { get; set; }

    public string? Cnic { get; set; }

    public int? Age { get; set; }

    public DateTime? Dob { get; set; }

    public string? Mrno { get; set; }

    public string Gender { get; set; } = null!;

    public Guid PatientVisitId { get; set; }

    public DateTime? VisitDate { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? RelationWithPt { get; set; }

    public string? PtMedicineRoutine { get; set; }

    public string? PtDoctorCheckup { get; set; }

    public string? PtDailyRoutine { get; set; }

    public string? PtFamilyAttitude { get; set; }

    public string? CounsellingOfPatientOrFamily { get; set; }

    public string? GuidanceProvided { get; set; }

    public string? PtStatus { get; set; }

    public Guid DoctorId { get; set; }

    public string? PhoneNo { get; set; }

    public int? SessionNo { get; set; }

    public string? HomeVisited { get; set; }

    public string? PtTelephoneOrHomeVisited { get; set; }

    public string? Latitude { get; set; }

    public string? Longitude { get; set; }

    public string? DoctorName { get; set; }

    public string? PatientDistrictName { get; set; }
}
