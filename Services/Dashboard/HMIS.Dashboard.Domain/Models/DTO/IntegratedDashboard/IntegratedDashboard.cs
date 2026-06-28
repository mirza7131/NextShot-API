using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Dashboard.Domain.Models.DTO.IntegratedDashboard
{
    public class IDIntegratedDashboardDTOs
    {
        public string? searchQuery { get; set; }
        public DateTime? fromDate { get; set; }
        public DateTime? toDate { get; set; }
        public string? divisionCode { get; set; }
        public string? districtCode { get; set; }
        public string? tehsilCode { get; set; }
        public string? hfTypeCode { get; set; }
        public int? hfId { get; set; }
        public int? TestId { get; set; }
        public string[]? filter { get; set; }
        public string? listType { get; set; }
        public string? conditionType { get; set; }
        public bool? isPaginate { get; set; } = false;
        public int? PageNumber { get; set; } = 1;
        public int? PageSize { get; set; } = 10;
        public int? TotalRecords { get; set; }

    }

    public class IDHFTypeWiseCountDTO
    {
        public int? TotalRecords { get; set; } = 0;
        public List<IDHFTypeWiseCount> HFTypeWiseCounts { get; set; }
    }
    public class IDHFTypeWiseCount
    {
        public string? HFTypeCode { get; set; }
        public int? HealthFacilityTypeId { get; set; }
        public string? HealthFacilityTypeName { get; set; }
        public int? Count { get; set; }
    }

    #region hftype district wise
    public class IDHFTypeWiseDistrictCountDTO
    {
        public int? TotalRecords { get; set; } = 0;
        public List<IDDistrictandHFTypeWiseCount> DistrictandHFTypeWiseCounts { get; set; }
    }
    public class IDDistrictandHFTypeWiseCount
    {
        public string? DistrictCode { get; set; }
        public string? DistrictName { get; set; }
        public string? HFTypeCode { get; set; }
        public int? HealthFacilityTypeId { get; set; }
        public string? HealthFacilityTypeName { get; set; }
        public int? Count { get; set; }
    }
    #endregion
    public class IDRegistrationDashboardAllCountsDTO
    {
        public int? Registrations { get; set; }
        public int? NewRegistrations { get; set; }
        public int? ReVisits { get; set; }
        public int? Self { get; set; }
        public int? OtherThanSelf { get; set; }
        public int? Male { get; set; }
        public int? Female { get; set; }
        public int? Others { get; set; }
    }

    public class IDHealthFacilityWiseCountForListTypeDTO
    {
        public int? TotalRecords { get; set; } = 0;
        public List<IDHealthFacilityWiseCountForList> HealthFacilityWiseCountForList { get; set; }
    }
    public class IDHealthFacilityWiseCountForList
    {
        public string? DivisionName { get; set; }
        public string? DistrictName { get; set; }
        public string? TehsilName { get; set; }
        public string? HealthFacilityName { get; set; }
        public int? HealthFacilityId { get; set; }
        public int? HealthFacilityTypeId { get; set; }
        public string? HealthFacilityCode { get; set; }
        public string? HealthFacilityTypeCode { get; set; }
        public int? Count { get; set; }
    }

    public class IDVitalDashboardAllCountsDTO
    {
        public int? TokenIssued { get; set; }
        public int? Registrations { get; set; }
        public int? VitalCollected { get; set; }
        public int? VitalNotCollected { get; set; }
        public int? RefferForvitals { get; set; }
        public int? Self { get; set; }
        public int? OtherThanSelf { get; set; }
        public int? Male { get; set; }
        public int? Female { get; set; }
        public int? Others { get; set; }
    }
    public class IDDoctorDashboardAllCountsDTO
    {
        public int? RefferedForPrescription { get; set; }
        public int? VitalCollected { get; set; }
        public int? Served { get; set; }
        public int? NotServed { get; set; }
        public int? Prescribed { get; set; }
        public int? NotPerscribed { get; set; }
        public int? InternalPatient { get; set; }
        public int? ExternalPatient { get; set; }
        public int? IntExtPatient { get; set; }
    }
    public class IDPharmacyDashboardAllCountsDTO
    {
        public int? TotalRegistered { get; set; }
        public int? Served { get; set; }
        public int? NotServed { get; set; }
        public int? Prescribed { get; set; }
        public int? NotPerscribed { get; set; }
        public int? MedicineToBeIssued { get; set; }
        public int? MedicineIssued { get; set; }
        public int? MedicineNotIssued { get; set; }
    }
    public class IDDashboardFilter
    {
        public string? User { get; set; }
        public int? HrId { get; set; }
        public string? TbPatientTypeContstant { get; set; }
        public int? ProvinceId { get; set; }
        public int? DivisionId { get; set; }
        public int? DistrictId { get; set; }
        public int? TehsilId { get; set; }
        public int? TestId { get; set; }
        public int? HealthFacilityId { get; set; }
        public int? HealthFacilityTypeId { get; set; }
        public string? HealthFacilityCode { get; set; }
        public string? HealthFacilityTypeCode { get; set; }
        public string? DivisionCode { get; set; }
        public string? DistrictCode { get; set; }
        public string? TehsilCode { get; set; }
        public bool? isPaginate { get; set; }
        public int? DepartmentId { get; set; }
        public int? SectionId { get; set; }
        public string? listType { get; set; }
        public string? conditionType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public class IDPatientLineListDTO
    {
        public int? TotalRecords { get; set; } = 0;
        public List<IDPatientLineList> PatientLineList { get; set; }
    }
    public class IDPatientLineList
    {
        public Guid PatientOpenVisitId { get; set; }
        public Guid PatientId { get; set; }
        public string? PatientName { get; set; }
        public string? MRNo { get; set; }
        public string? CNIC { get; set; }
        public string? MobileNo { get; set; }
        public int? Age { get; set; }
        public string? Gender { get; set; }
        public string? EnteredUserName { get; set; }
        public string? EnteredUserDesignation { get; set; }
        public int? HealthFacilityId { get; set; }
        public Guid? CreatedById { get; set; }
        public DateTime? Datetime { get; set; }
        public string? HealthFacilityName { get; set; }
        public string? DepartmentName { get; set; }
        public string? SectionName { get; set; }
    }
    #region HR Report with sample taken
    public class IDHRComplianceReportCountsDTO
    {
        public int? TotalRegistered { get; set; }
        public int? TotalServed { get; set; }
        public int? Prescribed { get; set; }
        public int? MedicineIssued { get; set; }
        public int? TestRecommended { get; set; }
        public int? SampleTaken { get; set; }
        public int? DentalTotal { get; set; }
        public int? DentalServed { get; set; }
        public int? PhysioTotal { get; set; }
        public int? PhysioServed { get; set; }
    }
    #endregion
    #region Report with sample taken
    public class IDComplianceReportCountsDTO
    {
        public int? TotalRegistered { get; set; }
        public int? TotalServed { get; set; }
        public int? Prescribed { get; set; }
        public int? MedicineIssued { get; set; }
        public int? TestRecommended { get; set; }
        public int? InternalTest { get; set; }
        public int? ExternalTest { get; set; }
        public int? SampleTaken { get; set; }
        public int? SamplePending { get; set; }
        public int? ReportGenerated { get; set; }
        public int? PendingReports { get; set; }
        public int? SampleRejected { get; set; }
        public int? TbTotal { get; set; }
        public int? TbServed { get; set; }
        public int? HcpTotal { get; set; }
        public int? HcpServed { get; set; }
        public int? DentalTotal { get; set; }
        public int? DentalServed { get; set; }
        public int? PhysioTotal { get; set; }
        public int? PhysioServed { get; set; }
        public int? PsychologyTotal { get; set; }
        public int? PsychologyServed { get; set; }
        public int? NutritionTotal { get; set; }
        public int? NutritionServed { get; set; }
        public int? SpeechTherapyTotal { get; set; }
        public int? SpeechTherapyServed { get; set; }
        public int? OccupationalTherapyTotal { get; set; }
        public int? OccupationalTherapyServed { get; set; }
        public int? PsychiatryTotal { get; set; }
        public int? PsychiatryServed { get; set; }
        public int? RespiratoryTotal { get; set; }
        public int? RespiratoryServed { get; set; }
        public int? DrugAddictTotal { get; set; }
        public int? DrugAddictServed { get; set; }
        public int? SocialWelfareTotal { get; set; }
        public int? SocialWelfareServed { get; set; }
    }
    #endregion
    #region Hf Wise statistics report
    public class IDHfWsieRequiredStatisticsReportCountsDTO
    {
        public int? TotalRecords { get; set; } = 0;
        public List<IDHfWsieRequiredStatisticsReportCounts> HfWsieStatistics { get; set; }
    }
    public class IDHfWsieRequiredStatisticsReportCounts
    {
        public string? DivisionName { get; set; }
        public string? DistrictName { get; set; }
        public string? TehsilName { get; set; }
        public int? HealthFacilityId { get; set; }
        public string? HealthFacilityName { get; set; }
        public int? TotalRegistered { get; set; }
        public int? TotalServed { get; set; }
        public int? Prescribed { get; set; }
        public int? MedicineIssued { get; set; }
        public int? TestRecommended { get; set; }
        public int? SampleTaken { get; set; }
        public int? DentalTotal { get; set; }
        public int? DentalServed { get; set; }
        public int? PhysioTotal { get; set; }
        public int? PhysioServed { get; set; }
    }
    #endregion
    #region Statistics Not Report Hf list
    public class IDHfWsieStatisticsNotReportingDTO
    {
        public int? TotalRecords { get; set; } = 0;
        public List<IDHfWsieStatisticsNotReporting> HfWsieStatistics { get; set; }
    }
    public class IDHfWsieStatisticsNotReporting
    {
        public string? DivisionName { get; set; }
        public string? DistrictName { get; set; }
        public string? TehsilName { get; set; }
        public int? HealthFacilityId { get; set; }
        public string? HealthFacilityName { get; set; }
    }
    #endregion
    #region Hf Wise statistics report With Served and Total For UB
    public class IDHfWsieCompleteStatisticsReportCountsDTO
    {
        public string? DivisionName { get; set; }
        public string? DistrictName { get; set; }
        public string? TehsilName { get; set; }
        public string? HealthFacilityName { get; set; }
        public int? TotalRegistered { get; set; }
        public int? TotalServed { get; set; }
        public int? Prescribed { get; set; }
        public int? MedicineIssued { get; set; }
        public int? TestRecommended { get; set; }
        public int? InternalTest { get; set; }
        public int? ExternalTest { get; set; }
        public int? SampleTaken { get; set; }
        public int? SamplePending { get; set; }
        public int? ReportGenerated { get; set; }
        public int? PendingReports { get; set; }
        public int? SampleRejected { get; set; }
        public int? TbTotal { get; set; }
        public int? TbServed { get; set; }
        public int? HcpTotal { get; set; }
        public int? HcpServed { get; set; }
        public int? DentalTotal { get; set; }
        public int? DentalServed { get; set; }
        public int? PhysioTotal { get; set; }
        public int? PhysioServed { get; set; }
        public int? PsychologyTotal { get; set; }
        public int? PsychologyServed { get; set; }
        public int? NutritionTotal { get; set; }
        public int? NutritionServed { get; set; }
        public int? SpeechTherapyTotal { get; set; }
        public int? SpeechTherapyServed { get; set; }
        public int? OccupationalTherapyTotal { get; set; }
        public int? OccupationalTherapyServed { get; set; }
        public int? PsychiatryTotal { get; set; }
        public int? PsychiatryServed { get; set; }
        public int? RespiratoryTotal { get; set; }
        public int? RespiratoryServed { get; set; }
        public int? DrugAddictTotal { get; set; }
        public int? DrugAddictServed { get; set; }
        public int? SocialWelfareTotal { get; set; }
        public int? SocialWelfareServed { get; set; }
    }
    #endregion
    #region Reporting Non Reporting hf Counts
    public class IDReportingNonReportingDTO
    {
        public string? HealthFacilityTypeName { get; set; }
        public int? HealthFacilityTypeId { get; set; }
        public string? HFTypeCode { get; set; }
        public int? Reporting { get; set; }
        public int? NonReporting { get; set; }

    }
    #endregion
    #region PAtholgogy
    #region Pathology Test Recommended Count
    public class IDPathologyMainCount
    {
        public int? TestRecommended { get; set; }

    }
    #endregion
    #region Pathology Test Recommended Count
    public class IDPathologyTestWiseCount
    {
        public int? TotalRecords { get; set; } = 0;
        public List<IDPathologyTestWiseList> TestWiseCount { get; set; }

    }
    public class IDPathologyTestWiseList
    {
        public string? TestName { get; set; }
        public int? TestId { get; set; }
        public int? TestRecommended { get; set; }

    }
    public class IDHealthFacilityWisePatholgyDTO
    {
        public int? TotalRecords { get; set; } = 0;
        public List<IDHealthFacilityWisePatholgyList> HealthFacilityWiseList { get; set; }
    }
    public class IDHealthFacilityWisePatholgyList
    {
        public string? DivisionName { get; set; }
        public string? DistrictName { get; set; }
        public string? TehsilName { get; set; }
        public string? HealthFacilityName { get; set; }
        public string? TestName { get; set; }
        public int? TestId { get; set; }
        public int? HealthFacilityId { get; set; }
        public int? HealthFacilityTypeId { get; set; }
        public string? HealthFacilityCode { get; set; }
        public string? HealthFacilityTypeCode { get; set; }
        public int? TestRecommended { get; set; }
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

    public class IDHealthFacilityTypeWisePatholgyDTO
    {
        public int? TotalRecords { get; set; } = 0;
        public List<IDHealthFacilityTypeWisePatholgyList> HealthFacilityTypeWiseList { get; set; }
    }
    public class IDHealthFacilityTypeWisePatholgyList
    {
        public string? HealthFacilityTypeName { get; set; }
        public int? HealthFacilityTypeId { get; set; }
        public string? HFTypeCode { get; set; }
        public string? TestName { get; set; }
        public int? TestId { get; set; }
        public int? TestRecommended { get; set; }
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
    #endregion

    public class PathologyPatientLineListDTO
    {
        public int? TotalRecords { get; set; } = 0;
        public List<PathologyPatientLineList> PatientLineList { get; set; }
    }
    public class PathologyPatientLineList
    {
        public Guid PatientOpenVisitId { get; set; }
        public Guid PatientId { get; set; }
        public string? PatientName { get; set; }
        public string? MRNo { get; set; }
        public string? CNIC { get; set; }
        public string? MobileNo { get; set; }
        public int? Age { get; set; }
        public string? Gender { get; set; }
        public string? EnteredUserName { get; set; }
        public string? EnteredUserDesignation { get; set; }
        public string? TestName { get; set; }
        public int? TestId { get; set; }
        public int? HealthFacilityId { get; set; }
        public Guid? CreatedById { get; set; }
        public DateTime? Datetime { get; set; }
        public string? HealthFacilityName { get; set; }
        public string? DepartmentName { get; set; }
        public string? SectionName { get; set; }
    }
    #endregion

    #region PAtholgogy
    #region Pathology Test Recommended Count
    public class IDTbMainCount
    {
        public int? OneWindowTbPatients { get; set; }

    }
    #endregion
    #endregion
    #region Hf Wise Tb Count
    public class IDHealthFacilityWiseTbDTO
    {
        public int? TotalRecords { get; set; } = 0;
        public List<IDHealthFacilityWiseTbList> HealthFacilityWiseList { get; set; }
    }
    public class IDHealthFacilityWiseTbList
    {
        public string? DivisionName { get; set; }
        public string? DistrictName { get; set; }
        public string? TehsilName { get; set; }
        public string? HealthFacilityName { get; set; }
        public int? HealthFacilityId { get; set; }
        public int? HealthFacilityTypeId { get; set; }
        public string? HealthFacilityCode { get; set; }
        public string? HealthFacilityTypeCode { get; set; }
        public int? OneWindowTbVisits { get; set; }
        public int? NewConfirmed { get; set; }
        public int? FollowupConfirmed { get; set; }
        public int? TotalConfirmed { get; set; }
        public int? TestAdvised { get; set; }
        public int? SsmAdvised { get; set; }
        public int? SsmConduct { get; set; }
        public int? SSMPositive { get; set; }
        public int? CXRAdvised { get; set; }
        public int? CXRConduct { get; set; }
        public int? CXRPositive { get; set; }
        public int? XpertAdvised { get; set; }
        public int? XpertConduct { get; set; }
        public int? XpertPositive { get; set; }
        public int? MDRDetected { get; set; }
        public int? HIVAdvised { get; set; }
        public int? HIVReactive { get; set; }
        public int? MedicineIssued { get; set; }
    }
    #endregion
    #region HfType Wise Count TB
    public class IDHealthFacilityTypeWiseTbDTO
    {
        public int? TotalRecords { get; set; } = 0;
        public List<IDHealthFacilityTypeWiseTbList> HealthFacilityTypeWiseList { get; set; }
    }
    public class IDHealthFacilityTypeWiseTbList
    {
        public string? HealthFacilityTypeName { get; set; }
        public int? HealthFacilityTypeId { get; set; }
        public string? HFTypeCode { get; set; }
        public int? OneWindowTbVisits { get; set; }
        public int? NewConfirmed { get; set; }
        public int? FollowupConfirmed { get; set; }
        public int? TotalConfirmed { get; set; }
        public int? TestAdvised { get; set; }
        public int? SsmAdvised { get; set; }
        public int? SsmConduct { get; set; }
        public int? SSMPositive { get; set; }
        public int? CXRAdvised { get; set; }
        public int? CXRConduct { get; set; }
        public int? CXRPositive { get; set; }
        public int? XpertAdvised { get; set; }
        public int? XpertConduct { get; set; }
        public int? XpertPositive { get; set; }
        public int? MDRDetected { get; set; }
        public int? HIVAdvised { get; set; }
        public int? HIVReactive { get; set; }
        public int? MedicineIssued { get; set; }
    }
    #endregion
    #region Tb Dashboard OverallCount
    public class IDTbAllCountsDTO
    {
        public int? OneWindowTbVisits { get; set; }
        public int? NewConfirmed { get; set; }
        public int? FollowupConfirmed { get; set; }
        public int? TotalConfirmed { get; set; }
        public int? TestAdvised { get; set; }
        public int? SsmAdvised { get; set; }
        public int? SsmConduct { get; set; }
        public int? SSMPositive { get; set; }
        public int? CXRAdvised { get; set; }
        public int? CXRConduct { get; set; }
        public int? CXRPositive { get; set; }
        public int? XpertAdvised { get; set; }
        public int? XpertConduct { get; set; }
        public int? XpertPositive { get; set; }
        public int? MDRDetected { get; set; }
        public int? HIVAdvised { get; set; }
        public int? HIVReactive { get; set; }
        public int? MedicineIssued { get; set; }
    }
    #endregion
    #region Tb PAtientline List 
    public class IDTbPatientLineListDTO
    {
        public int? TotalRecords { get; set; } = 0;
        public List<IDTbPatientLineList> PatientLineList { get; set; }
    }
    public class IDTbPatientLineList
    {
        public Guid PatientOpenVisitId { get; set; }
        public Guid PatientId { get; set; }
        public string? PatientName { get; set; }
        public string? MRNo { get; set; }
        public string? CNIC { get; set; }
        public string? MobileNo { get; set; }
        public int? Age { get; set; }
        public string? Gender { get; set; }
        public string? EnteredUserName { get; set; }
        public string? EnteredUserDesignation { get; set; }
        public int? HealthFacilityId { get; set; }
        public Guid? CreatedById { get; set; }
        public DateTime? Datetime { get; set; }
        public string? HealthFacilityName { get; set; }
        public string? DepartmentName { get; set; }
        public string? SectionName { get; set; }
        public int? TestId { get; set; }
        public string? TestName { get; set; }
    }
    #endregion
    #region HCP
    public class IDHCPMainCount
    {
        public int? HCPPatients { get; set; }

    }
    #endregion
    #region HCP All Count
    public class IDHCPAllCountsDTO
    {
        public int? TotalRegistered { get; set; }
        public int? TotalScreened { get; set; }
        public int? PreDiagnosedHBVScreenedPositive { get; set; }
        public int? PreDiagnosedHBVScreenedNegative { get; set; }
        public int? PreDiagnosedHCVScreenedPositive { get; set; }
        public int? PreDiagnosedHCVScreenedNegative { get; set; }
        public int? RapidKitHBVScreenedPositive { get; set; }
        public int? RapidKitHBVScreenedNegative { get; set; }
        public int? RapidKitHCVScreenedPositive { get; set; }
        public int? RapidKitHCVScreenedNegative { get; set; }
        public int? TotalAssessmentPerformed { get; set; }
        public int? TotalVaccinationPerformed { get; set; }
        public int? Vaccination1stDosePerformedNormal { get; set; }
        public int? Vaccination1stDosePerformedDialysis { get; set; }
        public int? Vaccination2ndDosePerformedNormal { get; set; }
        public int? Vaccination2ndDosePerformedDialysis { get; set; }
        public int? Vaccination3rdDosePerformedNormal { get; set; }
        public int? Vaccination3rdDosePerformedDialysis { get; set; }
        public int? Vaccination4thDosePerformedDialysis { get; set; }
        //public int? VitalCollected { get; set; }
        public int? Served { get; set; }
        public int? NotServed { get; set; }
        public int? Prescribed { get; set; }
        public int? NotPerscribed { get; set; }
        //public int? InternalPatientCount { get; set; }
        //public int? ExternalPatientCount { get; set; }
        //public int? IntExtPatientCount { get; set; }
        public int? TotalLabVisitCount { get; set; }
        public int? InternalLabVisitCount { get; set; }
        public int? ExternalLabVisitCount { get; set; }
        public int? InternalExternalLabVisitCount { get; set; }
        public int? PCRforHCVRNA { get; set; }
        public int? PCRforHBVDNA { get; set; }
        public int? PCRforHCVRNAPositive { get; set; }
        public int? PCRforHBVDNAPositive { get; set; }
        public int? PCRforHBVDNANegative { get; set; }
        public int? PCRforHCVRNANegative { get; set; }

    }
    #endregion

    #region Dental
    public class IDDentalMainCount
    {
        public int? DentalPatients { get; set; }

    }
    #region Dental All Count
    public class IDDentalAllCountsDTO
    {
        public int? DentalPatients { get; set; }
        public int? Served { get; set; }
        public int? NotServed { get; set; }
        public int? Prescribed { get; set; }
        public int? NotPrescribed { get; set; }
        public int? MLC { get; set; }
        public int? TotalProceduresRecommended { get; set; }
        public int? ProcedurePerformed { get; set; }
        public int? ProcedureNotPerformed { get; set; }
    }
    #endregion
    #region Dental PAtient line list
    public class IDDentalPatientLineListDTO
    {
        public int? TotalRecords { get; set; } = 0;
        public List<IDDentalPatientLineList> PatientLineList { get; set; }
    }
    public class IDDentalPatientLineList
    {
        public Guid PatientOpenVisitId { get; set; }
        public Guid PatientId { get; set; }
        public string? PatientName { get; set; }
        public string? MRNo { get; set; }
        public string? CNIC { get; set; }
        public string? MobileNo { get; set; }
        public int? Age { get; set; }
        public string? Gender { get; set; }
        public string? EnteredUserName { get; set; }
        public string? EnteredUserDesignation { get; set; }
        public int? HealthFacilityId { get; set; }
        public Guid? CreatedById { get; set; }
        public DateTime? Datetime { get; set; }
        public string? HealthFacilityName { get; set; }
        public string? DepartmentName { get; set; }
        public string? SectionName { get; set; }
        public string? ProcedureTitle { get; set; }
    }
    #endregion
    #region PAthology all count ID
    public class IDPathologyDashboardAllCounts
    {
        public int TestRecommended { get; set; }
        public int InternalLabTestCount { get; set; }
        public int ExternalLabTestCount { get; set; }
        public int InternalExternalLabTestCount { get; set; }
        public int SampleTobeCollected { get; set; }
        public int SampleCollected { get; set; }
        public int SamplePendingTobeCollected { get; set; }
        public int ReportGenerated { get; set; }
        public int PendingReports { get; set; }
        public int SampleRejected { get; set; }

    }
    #endregion
    #endregion
    #region Physiotherapy
    public class IDPhysiotherapyAllCountsDTO
    {
        public int? PhysioPatients { get; set; }
        public int? Served { get; set; }
        public int? NotServed { get; set; }
        public int? Modalities { get; set; }
        public int? NoModalities { get; set; }
        public int? MinutesSesion10to20 { get; set; }
        public int? MinutesSesion20to30 { get; set; }
        public int? MinutesSesion30to45 { get; set; }
        public int? MinutesSesionGreaterThan45 { get; set; }
    }
    public class IDPhysiotherapyMainCount
    {
        public int? PhysioPatients { get; set; }

    }
    #endregion
    #region Psychiatry
    public class IDPsychiatryMainCount
    {
        public int? PsychiatryPatients { get; set; }
    }
    public class IDPsychiatryAllCountsDTO
    {
        public int? PsychiatryPatients { get; set; }
        public int? VitalCollected { get; set; }
        public int? Served { get; set; }
        public int? NotServed { get; set; }
    }
    #endregion
    #region Occupational
    public class IDOccupationalMainCount
    {
        public int? OccupationalPatients { get; set; }
    }
    public class IDOccupationalAllCountsDTO
    {
        public int? OccupationalPatients { get; set; }
        public int? VitalCollected { get; set; }
        public int? Served { get; set; }
        public int? NotServed { get; set; }
    }
    #endregion
    #region Nutrition
    public class IDNutritionMainCount
    {
        public int? NutritionPatients { get; set; }
    }
    public class IDNutritionAllCountsDTO
    {
        public int? NutritionPatients { get; set; }
        public int? Served { get; set; }
        public int? NotServed { get; set; }
        public int? ExaminationFindings { get; set; }
        public int? NoExaminationFindings { get; set; }
        public int? Comorbidity { get; set; }
        public int? NoComorbidity { get; set; }
    }
    #endregion


    #region SpeechTherapy
    public class IDSpeechTherapyMainCount
    {
        public int? SpeechPatients { get; set; }
    }
    public class IDSpeechTherapyAllCountsDTO
    {
        public int? SpeechPatients { get; set; }
        public int? Served { get; set; }
        public int? NotServed { get; set; }
        public int? DevelopmentMileStone{ get; set; }
        public int? NoDevelopmentMileStone{ get; set; } 
        public int? NeckHolding0to5{ get; set; } 
        public int? NeckHolding6to10{ get; set; } 
        public int? NeckHolding11to15{ get; set; } 
        public int? NeckHoldingGreater15{ get; set; } 
        public int? Sitting0to5{ get; set; } 
        public int? Sitting6to10{ get; set; } 
        public int? Sitting11to15{ get; set; } 
        public int? SittingGreater15{ get; set; } 
        public int? Standing0to5{ get; set; } 
        public int? Standing6to10{ get; set; } 
        public int? Standing11to15{ get; set; } 
        public int? StandingGreater15{ get; set; } 
        public int? Walking0to5{ get; set; } 
        public int? Walking6to10{ get; set; } 
        public int? Walking11to15{ get; set; } 
        public int? WalkingGreater15{ get; set; } 
        public int? Cooing0to5{ get; set; } 
        public int? Cooing6to10{ get; set; } 
        public int? Cooing11to15{ get; set; } 
        public int? CooingGreater15{ get; set; } 
        public int? Babbling0to5{ get; set; } 
        public int? Babbling6to10{ get; set; } 
        public int? Babbling11to15{ get; set; } 
        public int? BabblingGreater15{ get; set; } 
        public int? SingleWord0to5{ get; set; } 
        public int? SingleWord6to10{ get; set; } 
        public int? SingleWord11to15{ get; set; } 
        public int? SingleWordGreater15{ get; set; } 
        public int? SpeechLevelWord0to5{ get; set; } 
        public int? SpeechLevelWord6to10{ get; set; } 
        public int? SpeechLevelWord11to15{ get; set; } 
        public int? SpeechLevelWordGreater15{ get; set; } 
        public int? SpeechMilestone{ get; set; } 
        public int? NoSpeechMilestone{ get; set; } 
        public int? FamilyHistory{ get; set; } 
        public int? NoFamilyHistory{ get; set; } 
        public int? HearingLoss{ get; set; } 
        public int? NoHearingLoss{ get; set; } 
        public int? ArticulationSounderrors{ get; set; } 
        public int? Articulationintelligibility{ get; set; } 
        public int? ArticulationOthers{ get; set; } 
        public int? dysfluencyRepetitions{ get; set; } 
        public int? dysfluencyProlongation{ get; set; } 
        public int? dysfluencySilentpause{ get; set; } 
        public int? dysfluencyOthers{ get; set; } 
        public int? VoiceHoarse{ get; set; } 
        public int? VoiceAphonic{ get; set; } 
        public int? VoiceOthers{ get; set; } 
        public int? PitchToohigh{ get; set; } 
        public int? PitchToolow{ get; set; } 
        public int? PitchOthers{ get; set; } 
        public int? ResonanceNasal{ get; set; } 
        public int? ResonanceDenasal{ get; set; } 
        public int? ResonanceMixed{ get; set; } 
        public int? PlanOfCareSelfTalk{ get; set; } 
        public int? PlanOfCareRecasting{ get; set; } 
        public int? PlanOfCareParallel{ get; set; } 
        public int? PlanOfCareFocused{ get; set; } 
        public int? PlanOfCareOthers{ get; set; } 
        public int? ScheduleWeekly{ get; set; } 
        public int? ScheduleFortnightly{ get; set; }
        public int? ScheduleMonthly { get; set; }
    }
    #endregion
    #region Psychology
    public class IDPsychologyMainCount
    {
        public int? PsychologyPatients { get; set; }
    }
    public class IDPsychologyAllCountsDTO
    {
        public int? PsychologyPatients { get; set; }
        public int? Served { get; set; }
        public int? NotServed { get; set; }
        public int? PastHistory { get; set; }
        public int? NoPastHistory { get; set; }
        public int? PsychologicalTestApplied { get; set; }
        public int? NoPsychologicalTestApplied { get; set; }
    }
    #endregion
}
