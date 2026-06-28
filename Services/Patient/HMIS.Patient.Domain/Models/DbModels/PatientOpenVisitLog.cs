using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels;

public partial class PatientOpenVisitLog
{
    public Guid PatientOpenVisitId { get; set; }

    public string? SlipNo { get; set; }

    public string? TokenNo { get; set; }

    public int? VisitNo { get; set; }

    public Guid? VisitTypeProfileId { get; set; }

    public bool? IsReferred { get; set; }

    public int? ReferredHealthFacilityId { get; set; }

    public int? ReferredDepartmentLookupId { get; set; }

    public int? ReferredSectionLookupId { get; set; }

    public Guid? ReferredBy { get; set; }

    public Guid? PatientId { get; set; }

    public int? HealthFacilityId { get; set; }

    public int? DepartementLookupId { get; set; }

    public int? SectionLookupId { get; set; }

    public Guid? CurrentStationProfileId { get; set; }

    public Guid? CurrentStationUserId { get; set; }

    public DateTime? VisitDate { get; set; }

    public Guid? VitalCollectedBy { get; set; }

    public bool IsFromPmis { get; set; }

    public Guid? AttendedBy { get; set; }

    public Guid? PharmacyAttendedBy { get; set; }

    public bool IsWillingToBuyMedPrivately { get; set; }

    public bool? IsDischarge { get; set; }

    public bool? IsVisitExternally { get; set; }

    public string? SourceVisitId { get; set; }

    public int? SourceSystemIdd { get; set; }

    public string? SourceHealthFacilityId { get; set; }

    public string? SourceReferredHealthFacilityId { get; set; }

    public bool? IsVitalSkip { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public long? UserLogId { get; set; }

    public byte ActionTypeId { get; set; }

    public DateTime ActionDate { get; set; }

    public Guid? SourceSystemId { get; set; }
    public int? Age { get; set; }

    public Guid? AgeTypeProfileId { get; set; }

}
