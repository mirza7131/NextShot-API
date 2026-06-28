using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.IPDDashboardDTO
{
    #region IPD Admission DTO
    public class IPDAdmissionsDashboardDTO
    {
        public int? ActiveAdmissionsPatients { get; set; }
        public int? TodayAdmissionsPatients { get; set; }
        public int? NewAdmissionPatients { get; set; }
        public int? ReAdmissionPatients { get; set; }
        public int? DischargedPatients { get; set; }
        public int? SelfPatients { get; set; }
        public int? OtherThanSelfPatients { get; set; }
        public int? SelfMalePatients { get; set; }
        public int? SelfFemalePatients { get; set; }
        public int? SelfOthersPatients { get; set; }
    }
    #endregion

    #region IPD Vital DTO
    public class IPDVitalsDashboardDTO
    {
        public int? ActiveAdmissionsPatients { get; set; }
        public int? TodayAdmissionsPatients { get; set; }
        public int? VitalCollectedPatients { get; set; }
        public int? SelfPatients { get; set; }
        public int? OtherThanSelfPatients { get; set; }
        public int? SelfMalePatients { get; set; }
        public int? SelfFemalePatients { get; set; }
        public int? SelfOthersPatients { get; set; }
    }
    #endregion

    #region IPD Doctor Dashobard DTO
    public class IPDDoctorDashboardDTO
    {
        public int? ActiveAdmissionsPatients { get; set; }
        public int? VitalCollectedPatients { get; set; }
        public int? ServedPatients { get; set; }
        public int? PrescribedPatients { get; set; }
        public int? InternalPrescribedPatients { get; set; }
        public int? ExternalPrescribedPatients { get; set; }
        public int? InternalExternalPrescribedPatients { get; set; }
        public int? TotalLabTestRecommended { get; set; }
        public int? InternalTestRecommended { get; set; }
        public int? ExternalTestRecommended { get; set; }
        public int? InternalExternalTestRecommended { get; set; }
    }
    #endregion

    #region IPD Pharmacy Dashobard DTO
    public class IPDPharmacyDashboardDTO
    {
        public int? ActiveAdmissionsPatients { get; set; }
        public int? VitalCollectedPatients { get; set; }
        public int? ServedPatients { get; set; }
        public int? PrescribedPatients { get; set; }
        public int? InternalPrescribedPatients { get; set; }
        public int? ExternalPrescribedPatients { get; set; }
        public int? InternalExternalPrescribedPatients { get; set; }
        public int? MedicineToBeIssued { get; set; }
        public int? MedicineIssued { get; set; }
    }
    public class IPDPatientDetailDTO
    {
        public Guid PatientId { get; set; }
        public string? FullName { get; set; }
        public string? MRNo { get; set; }
        public string CNIC { get; set; } = null!;
        public int? HealthFacilityId { get; set; }
        public Guid? CreatedById { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? MobileNo { get; set; }
        public Guid PatientOpenVisitId { get; set; }
        public string? DepartmentName { get; set; }
        public string? SectionName { get; set; }

    }
    #endregion

    #region IPD Lab/Pathology Dashobard DTO
    public class IPDPathologyDashboardDTO
    {
        public int? TotalCount { get; set; }
        public int? TotalLabTestCount { get; set; }
        public int? InternalLabTestCount { get; set; }
        public int? ExternalLabTestCount { get; set; }
        public int? InternalExternalLabTestCount { get; set; }
        public int? SampleTobeCollected { get; set; }
        public int? SampleCollected { get; set; }
        public int? SamplePendingTobeCollected { get; set; }
        public int? ReportGenerated { get; set; }
        public int? PendingReports { get; set; }
        public int? SampleRejected { get; set; }
    }
    public class IPDLabDashboardFromStartTillNowAllCountsDTO
    {
        public DateTime LabStartedDate { get; set; }
        public int TotalLabTestCount { get; set; }
        public int InternalLabTestCount { get; set; }
        public int ExternalLabTestCount { get; set; }
        public int InternalExternalLabTestCount { get; set; }
        public int SampleTobeCollected { get; set; }
        public int SampleCollected { get; set; }
        public int SamplePendingTobeCollected { get; set; }
        public int ReportGenerated { get; set; }
        public int PendingReports { get; set; }
        public int SampleRejected { get; set; }
        public int PendingReportInThreeDays { get; set; }
        public int PendingReportInSevenDays { get; set; }
        public int PendingReportIn15Days { get; set; }
        public int PendingReportIn30Days { get; set; }
        public int PendingReportGreater30Days { get; set; }
        public int RejectedInThreeDays { get; set; }
        public int RejectedInSevenDays { get; set; }
        public int RejectedIn15Days { get; set; }
        public int RejectedIn30Days { get; set; }
        public int RejectedGreater30Days { get; set; }
        public int PendingSampleInThreeDays { get; set; }
        public int PendingSampleInSevenDays { get; set; }
        public int PendingSampleIn15Days { get; set; }
        public int PendingSampleIn30Days { get; set; }
        public int PendingSampleGreater30Days { get; set; }

    }

    public class IPDPatientLabDetaildto
    {
        public Guid PatientId { get; set; }

        public string? MRNo { get; set; }

        public string? FullName { get; set; }

        public string CNIC { get; set; } = null!;
        public int? HealthFacilityId { get; set; }

        public Guid? CreatedById { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string? CreatedBy { get; set; }
        public string? MobileNo { get; set; }
        public Guid PatientOpenVisitId { get; set; }
        public int? LabTestId { get; set; }
        public string? TestName { get; set; }
        public Guid? PatientLabTestId { get; set; }
        public string? DepartmentName { get; set; }
        public string? SectionName { get; set; }

    }


    #endregion
    #region Top 20 IPD Test Recommended
    public class IPDInternalExternalCountByLabTestDTO
    {
        public int? TestCount { get; set; }
        public string? LabType { get; set; }
        public string? LabTestName { get; set; }
        public int? LabTestId { get; set; }
    }
    #endregion
}
