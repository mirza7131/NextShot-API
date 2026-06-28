using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels;

public partial class ViewDrugAddictCommunityDevelopment
{
    public string? FullName { get; set; }

    public string? Cnic { get; set; }

    public int? Age { get; set; }

    public DateTime? Dob { get; set; }

    public string? Mrno { get; set; }

    public string Gender { get; set; } = null!;

    public Guid PatientVisitId { get; set; }

    public DateTime VisitDate { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string RelationWithPt { get; set; } = null!;

    public string PtMedicineRoutine { get; set; } = null!;

    public string PtDoctorCheckup { get; set; } = null!;

    public string PtDailyRoutine { get; set; } = null!;

    public string PtFamilyAttitude { get; set; } = null!;

    public string? CounsellingOfPatientOrFamily { get; set; }

    public string? GuidanceProvided { get; set; }

    public string PtStatus { get; set; } = null!;

    public Guid DoctorId { get; set; }

    public string PhoneNo { get; set; } = null!;

    public int? SessionNo { get; set; }

    public string? DoctorName { get; set; }

    public string? PatientDistrictName { get; set; }
}
