using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.RoleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDAL.Models.Dto.IntegratedDashboardDTO
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
}
