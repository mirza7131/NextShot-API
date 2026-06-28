using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels;

public partial class SocialWelfareTaskPerformedByCd
{
    public Guid Id { get; set; }

    public Guid PatientVisitId { get; set; }

    public Guid? SocialWelfareFormId { get; set; }

    public Guid DoctorId { get; set; }

    public string PhoneNo { get; set; } = null!;

    public DateTime VisitDate { get; set; }

    public string RelationWithPt { get; set; } = null!;

    public string PtMedicineRoutine { get; set; } = null!;

    public string PtDoctorCheckup { get; set; } = null!;

    public string PtDailyRoutine { get; set; } = null!;

    public string PtFamilyAttitude { get; set; } = null!;

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
}
