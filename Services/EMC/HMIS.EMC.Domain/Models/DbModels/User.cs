using System;
using System.Collections.Generic;

namespace HMIS.EMC.Domain.Models.DbModels;

public partial class User
{
    public Guid UserId { get; set; }

    public int? ProvinceId { get; set; }

    public int? DivisionId { get; set; }

    public int? DistrictId { get; set; }

    public int? TehsilId { get; set; }

    public int? UcId { get; set; }

    public string? FullName { get; set; }

    public string? FatherName { get; set; }

    public string Username { get; set; } = null!;

    public string? Email { get; set; }

    public string Password { get; set; } = null!;

    public string? ContactNo { get; set; }

    public string? ProfilePic { get; set; }

    public string? Cnic { get; set; }

    public int? HrId { get; set; }

    public int? HealthFacilityId { get; set; }

    public int? DepartmentId { get; set; }

    public int? SectionId { get; set; }

    public string? CurrentGradeBps { get; set; }

    public DateTime? Dob { get; set; }

    public Guid? GenderProfileId { get; set; }

    public bool? IsActive { get; set; }

    public Guid? DesignationProfileId { get; set; }

    public Guid? UserTypeProfileId { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public long? UserLogId { get; set; }

    public byte ActionTypeId { get; set; }

    public bool? PmisUser { get; set; }

    public bool? IsShowRoleOnly { get; set; }

    public bool? IsUserLoginFirstTime { get; set; }

    public DateTime? PasswordChangedOn { get; set; }

    public Guid? MimsBranchId { get; set; }

    public virtual ICollection<DentalSterilizationRecord> DentalSterilizationRecords { get; } = new List<DentalSterilizationRecord>();

    public virtual DepartmentLookup? Department { get; set; }

    public virtual Profile? DesignationProfile { get; set; }

    public virtual District? District { get; set; }

    public virtual Division? Division { get; set; }

    public virtual HealthFacility? HealthFacility { get; set; }

    public virtual ICollection<HfLabTestConfig> HfLabTestConfigCreatedByNavigations { get; } = new List<HfLabTestConfig>();

    public virtual ICollection<HfLabTestConfig> HfLabTestConfigDeletedByNavigations { get; } = new List<HfLabTestConfig>();

    public virtual ICollection<HfLabTestConfig> HfLabTestConfigUpdatedByNavigations { get; } = new List<HfLabTestConfig>();

    public virtual ICollection<Patient> PatientCreatedByNavigations { get; } = new List<Patient>();

    public virtual ICollection<PatientDischargeDetail> PatientDischargeDetailCreatedByNavigations { get; } = new List<PatientDischargeDetail>();

    public virtual ICollection<PatientDischargeDetail> PatientDischargeDetailDeletedByNavigations { get; } = new List<PatientDischargeDetail>();

    public virtual ICollection<PatientDischargeDetail> PatientDischargeDetailUpdatedByNavigations { get; } = new List<PatientDischargeDetail>();

    public virtual ICollection<PatientLabTest> PatientLabTestReportGeneratedByNavigations { get; } = new List<PatientLabTest>();

    public virtual ICollection<PatientLabTest> PatientLabTestSampleCollectedByNavigations { get; } = new List<PatientLabTest>();

    public virtual ICollection<PatientLabTest> PatientLabTestSampleRejectedByNavigations { get; } = new List<PatientLabTest>();

    public virtual ICollection<PatientLabTest> PatientLabTestTestAdvisedByNavigations { get; } = new List<PatientLabTest>();

    public virtual ICollection<PatientOpenVisit> PatientOpenVisitCreatedByNavigations { get; } = new List<PatientOpenVisit>();

    public virtual ICollection<PatientOpenVisit> PatientOpenVisitIpdReferredByNavigations { get; } = new List<PatientOpenVisit>();

    public virtual ICollection<PatientOpenVisit> PatientOpenVisitOccupiedByNavigations { get; } = new List<PatientOpenVisit>();

    public virtual ICollection<PatientOpenVisit> PatientOpenVisitPharmacyAttendedByNavigations { get; } = new List<PatientOpenVisit>();

    public virtual ICollection<PatientOpenVisit> PatientOpenVisitReferredByNavigations { get; } = new List<PatientOpenVisit>();

    public virtual ICollection<PatientOpenVisit> PatientOpenVisitUpdatedByNavigations { get; } = new List<PatientOpenVisit>();

    public virtual ICollection<PatientOpenVisit> PatientOpenVisitVitalCollectedByNavigations { get; } = new List<PatientOpenVisit>();

    public virtual ICollection<Patient> PatientUpdatedByNavigations { get; } = new List<Patient>();

    public virtual Province? Province { get; set; }

    public virtual SectionLookup? Section { get; set; }

    public virtual Tehsil? Tehsil { get; set; }

    public virtual ICollection<UserMenu> UserMenus { get; } = new List<UserMenu>();

    public virtual ICollection<UserRole> UserRoles { get; } = new List<UserRole>();
}
