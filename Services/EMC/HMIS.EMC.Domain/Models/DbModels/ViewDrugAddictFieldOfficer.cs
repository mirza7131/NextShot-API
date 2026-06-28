using System;
using System.Collections.Generic;

namespace HMIS.EMC.Domain.Models.DbModels;

public partial class ViewDrugAddictFieldOfficer
{
    public Guid PatientId { get; set; }

    public string? FullName { get; set; }

    public string Cnic { get; set; } = null!;

    public int? Age { get; set; }

    public DateTime? Dob { get; set; }

    public Guid PatientVisitId { get; set; }

    public Guid PatientVistId { get; set; }

    public string Gender { get; set; } = null!;

    public Guid DoctorId { get; set; }

    public string? DoctorName { get; set; }

    public Guid SocialWellfareFormId { get; set; }

    public DateTime? VisitDate { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? RelationWithPt { get; set; }

    public string? PtMedicineRoutine { get; set; }

    public string? PtDoctorCheckup { get; set; }

    public string? PtDailyRoutine { get; set; }

    public string? PtAttitudeWithFamily { get; set; }

    public string? PtRelativeOrOtherDetail { get; set; }

    public string? PatientDistrictName { get; set; }

    public string? MobileNo { get; set; }

    public string? Mrno { get; set; }

    public string? PtStatus { get; set; }

    public int? TotalSessions { get; set; }

    public long? RowNum { get; set; }
}
