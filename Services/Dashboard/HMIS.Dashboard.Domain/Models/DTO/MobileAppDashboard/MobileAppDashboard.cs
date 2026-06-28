using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Dashboard.Domain.Models.DTO.MobileAppDashboard
{
    public class MobileAppDashboardDTOs
    {
        public string? searchQuery { get; set; }
        public DateTime? fromDate { get; set; }
        public DateTime? toDate { get; set; }
        public string? divisionCode { get; set; }
        public string? districtCode { get; set; }
        public string? tehsilCode { get; set; }
        public string? hfTypeCode { get; set; }
        public string? hfCode { get; set; }
        public int? hfId { get; set; }
        public string[]? filter { get; set; }
        public string? listType { get; set; }
        public string? conditionType { get; set; }
        public bool? isPaginate { get; set; } = false;
        public int? PageNumber { get; set; } = 1;
        public int? PageSize { get; set; } = 10;
        public int? TotalRecords { get; set; }

    }
    public class MobileAppDashboardFilter
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

    #region Hf Wise statistics report
    public class MobileAppHfWsieCountsDTO
    {
        public int? TotalRecords { get; set; } = 0;
        public List<MobileAppHfWsieCounts> HfWsieStatistics { get; set; }
    }
    public class MobileAppHfWsieCounts
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
        
        public int? InternalTest { get; set; }
        public int? ExternalTest { get; set; }
        public int? SampleTaken { get; set; }
        public int? SamplePending { get; set; }
        public int? ReportGenerated { get; set; }
        public int? PendingReports { get; set; }
        public int? SampleRejected { get; set; }
        public int? DentalTotal { get; set; }
        public int? DentalServed { get; set; }
        public int? PhysioTotal { get; set; }
        public int? PhysioServed { get; set; }
        public int? TbTotal { get; set; }
        public int? TbServed { get; set; }
        public int? HcpTotal { get; set; }
        public int? HcpServed { get; set; }
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
}
