using System;
using System.Collections.Generic;

namespace HMIS.Dashboard.Domain.Models.DbModels;

public partial class SocialWelfareTaskPerformedByCd
{
    public Guid Id { get; set; }

    public Guid PatientVisitId { get; set; }

    public Guid? SocialWelfareFormId { get; set; }

    public Guid DoctorId { get; set; }

    public string? PtTelephoneOrHomeVisited { get; set; }

    public DateTime? VisitDate { get; set; }

    public string? RelationWithPt { get; set; }

    public string? PtMedicineRoutine { get; set; }

    public string? PtDoctorCheckup { get; set; }

    public string? PtDailyRoutine { get; set; }

    public string? PtFamilyAttitude { get; set; }

    public string? CounsellingOfPatientOrFamily { get; set; }

    public string? GuidanceProvided { get; set; }

    public string? PtStatus { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public long? UserLogId { get; set; }

    public byte? ActionTypeId { get; set; }

    public int? SessionNo { get; set; }

    public bool? IsVisitClosed { get; set; }

    public string? PtRelativeOrOtherDetail { get; set; }

    public string? PhoneNo { get; set; }

    public string? HomeVisited { get; set; }

    public string? Latitude { get; set; }

    public string? Longitude { get; set; }

    public string? CounselingofFamily { get; set; }
}
