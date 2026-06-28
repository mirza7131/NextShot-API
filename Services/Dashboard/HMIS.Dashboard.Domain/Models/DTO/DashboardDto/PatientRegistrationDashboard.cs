using HMIS.Dashboard.Domain.Models.DTO.DashboardDto.AdminReferedDashboard;
using HMIS.Dashboard.Domain.Models.DTO.FilterDto;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Dashboard.Domain.Models.DTO.DashboardDto.PatientRegistrationDashboard
{

    #region Dashboard DataSyncUtility
    public class DashboardDataSyncUtilityLogDto
    {
        public Guid DataSyncUtilityLogId { get; set; }

        public int HealthFacilityId { get; set; }
        public string? HealthFacilityName { get; set; }

        public DateTime? MorningSynced { get; set; }

        public DateTime? NightSynced { get; set; }

        public string FileName { get; set; } = null!;

        public string? ServerType { get; set; }
        public string? FileSize { get; set; }
        public string? FileStatus { get; set; }

        public int Status { get; set; }
        public string? StatusName { get; set; }

        public DateTime StatusUpdatedOn { get; set; }

        public DateTime? UploadedOn { get; set; }

        public DateTime? ProcessedOn { get; set; }

        public DateTime? CompletedOn { get; set; }

        public string? Message { get; set; }
    }
    #endregion

    #region HRDashboard
    public class HRHealthDashboardCountDTO
    {
        public int? TotalRegistered { get; set; }
        public List<HRHealthDashboardHFTypeCountDTO> HfTypeWiseCount { get; set; }
    }
    //public class HRHealthDashboardCountDTO
    //{
    //    public int? TotalRegistered { get; set; }
    //}
    public class HRHealthDashboardHFTypeCountDTO
    {
        public string? HealthFacilityTypeName { get; set; }
        public string? HFTypeCode { get; set; }
        public int? Registered { get; set; }
    }
    public class HRHRSectionWiseDTO
    {
        public int? TotalRegistered { get; set; }
        public int? Prescribed { get; set; }
        public int? VitalCollected { get; set; }
        public int? MedicineIssued { get; set; }
        public int? TestRecommended { get; set; }
        public int? TbTotal { get; set; }
        public int? HcpTotal { get; set; }
        public int? DentalTotal { get; set; }
        public int? PhysioTotal { get; set; }
        public int? PsychologyTotal { get; set; }
        public int? NutritionTotal { get; set; }
        public int? SpeechTherapyTotal { get; set; }
        public int? OccupationalTherapyTotal { get; set; }
        public int? PsychiatryTotal { get; set; }
        public int? RespiratoryTotal { get; set; }
        public int? DrugAddictTotal { get; set; }
        public int? SocialWelfareTotal { get; set; }
    }
    #endregion
    #region RegistrationLogin user Counts 
    public class RegistrationDashboardLoginUserAllCountsDTO
    {
        public int? TokenIssued { get; set; }
        public int? Registrations { get; set; }
        public int? NewRegistrations { get; set; }
        public int? ReVisits { get; set; }
    }
    #endregion
    #region Registration Dashboard DTo
    public class RegistrationDashboardAllCountsDTO
    {
        public List<RegistrationDashboardAllCounts> AllCounts { get; set; }
        public List<AgeWisePatientCount> AgeWisePatient { get; set; }
    }

    #endregion

    #region AgeWisePatientCount
    public class AgeWisePatientCount
    {
        public string? DistrictName { get; set; }
        public string? HealthFacilityName { get; set; }
        public int? Male0to5Age { get; set; }
        public int? Female0to5Age { get; set; }
        public int? Male6to10Age { get; set; }
        public int? Female6to10Age { get; set; }
        public int? Male11to15Age { get; set; }
        public int? Female11to15Age { get; set; }
        public int? Male16to20Age { get; set; }
        public int? Female16to20Age { get; set; }
        public int? Male21to30Age { get; set; }
        public int? Female21to30Age { get; set; }
        public int? Male31to40Age { get; set; }
        public int? Female31to40Age { get; set; }
        public int? Male41to50Age { get; set; }
        public int? Female51to60Age { get; set; }
        public int? Male51to60Age { get; set; }
        public int? Female41to50Age { get; set; }
        public int? Male61to70Age { get; set; }
        public int? Female61to70Age { get; set; }
        public int? Male70plusAge { get; set; }
        public int? Female70plusAge { get; set; }
        public int? OtherGender { get; set; }
    }
    #endregion
    #region Dashboard New Counts 
    public class RegistrationDashboardAllCounts
    {
        public int? TokenIssued { get; set; }
        public int? Registrations { get; set; }
        public int? NewRegistrations { get; set; }
        public int? ReVisits { get; set; }
        public int? SelfPatients { get; set; }
        public int? OtherThanSelfPatients { get; set; }
        public int? SelfMalePatients { get; set; }
        public int? SelfFemalePatients { get; set; }
        public int? SelfOthersPatients { get; set; }
    }
    #endregion
    #region Patient Visit Count By Health Facility By PMIS Vs HMIS
    public class GetPatientVisitCountByHealthFacilityByPMIS
    {
        public string? name { get; set; }
        public int? hmisValue { get; set; }
        public int? pmisValue { get; set; }
    }

    #endregion

    #region Patient Visit Count By Health Facility
    public class PatientVisitCountByHealthFacility
    {
        public int? extra { get; set; }
        public string? name { get; set; }
        public int? value { get; set; }
    }

    #endregion


    #region Patient Visit Count By User
    public class PatientVisitCountByUser
    {
        public Guid? extra { get; set; }
        public string? name { get; set; }
        public int? value { get; set; }
    }
    #endregion
    #region Patient Visit Count By User Export DEO
    public class PatientVisitCountByDEO
    {
        public Guid? extra { get; set; }
        public string? DistrictName { get; set; }
        public string? HealthFacilityName { get; set; }
        public string? RegisteredBy { get; set; }
        public string? Designation { get; set; }
        public int? PatientCount { get; set; }
        public string? name { get; set; }
        public int? value { get; set; }
    }
    #endregion
    

    #region Patient Visit Count By Province
    public class PatientVisitCountByProvince
    {
        public PatientVisitCountByProvince()
        {
            extra = new PatientDashboardExtra();
        }
        public string? name { get; set; }
        public int? value { get; set; }
        public PatientDashboardExtra extra { get; set; }
    }

    public class PatientDashboardExtra
    {
        public int? patientProvinceId { get; set; }
        public Guid? createdById { get; set; }
    }

    #endregion

    #region Patient Registered Count By Province
    public class PatientRegisteredCountByProvince
    {
        public string? name { get; set; }
        public int? value { get; set; }
        public PatientDashboardExtra extra { get; set; }
    }

    #endregion

    #region Patient New Registration Vs Revisit
    public class PatientNewRegisteredVsRevisit
    {
        public int? NewRegisteredCount { get; set; }
        public int? ReVisitCount { get; set; }
    }

    public class GetTokenNoDto
    {
        public string? TokenNo { get; set; }
    }

    #endregion

    #region Patient Visit Count By Province By Gender
    public class PatientVisitCountByProvinceByGender
    {
        public PatientVisitCountByProvinceByGender()
        {
            series = new List<PatientVisitCountByProvinceByGenderSeries>();
        }
        public string? name { get; set; }

        public List<PatientVisitCountByProvinceByGenderSeries> series { get; set; }
    }

    public class PatientVisitCountByProvinceByGenderSeries
    {
        public string? name { get; set; }
        public int? value { get; set; }
    }

    #endregion

    #region Patient Visit Count Self Vs Others
    public class PatientVisitCountSelfVsOthers
    {
        public string? name { get; set; }
        public int? value { get; set; }
    }
    #endregion

    #region Patient Visit Count By Gender By Self Vs Others
    public class PatientVisitCountByGenderBySelfVsOthers
    {
        public PatientVisitCountByGenderBySelfVsOthers()
        {
            series = new List<PatientVisitCountByGender>();
        }

        public string? relation { get; set; }
        public List<PatientVisitCountByGender> series { get; set; }
    }

    public class PatientVisitCountByGender
    {
        public string? name { get; set; }
        public int? value { get; set; }
        public string? extra { get; set; }
        public int? seqNo { get; set; }
    }



    #endregion

    #region Patient Visit Count By Gender By Age Range
    public class PatientVisitCountByAgeRangeByGender
    {

        public string? Gender { get; set; }
        public Guid? GenderId { get; set; }
        public int? Under18 { get; set; }
        public int? Range18to40 { get; set; }
        public int? Range41to60 { get; set; }
        public int? Range61to80 { get; set; }
        public int? Range81to100 { get; set; }
        public int? Above100 { get; set; }
    }
    #endregion

    #region Patient Visit Count By Department By Section By Month
    public class PatientVisitCountByDeptBySecByMonth
    {
        public int? VisitCount { get; set; }
        //public string? DepartmentName { get; set; }
        //public int? DepartmentId { get; set; }
        public int? SectionId { get; set; }
        public string? SectionName { get; set; }
        //public int? HealthFacilityId { get; set; }
        public int? VisitMonth { get; set; }

    }
    #endregion

    #region Patient Visit Count By Deptartment By Section By Date
    public class PatientVisitCountByDeptBySecByDate
    {
        public int? VisitCount { get; set; }

        public string? DepartmentName { get; set; }
        public int? DepartmentId { get; set; }
        public int? SectionId { get; set; }
        public string? SectionName { get; set; }

    }
    public class PatientVisitCountByDeptBySecWithouthfIdByDate
    {
        public int? VisitCount { get; set; }

        public string? DepartmentName { get; set; }
        public int? DepartmentId { get; set; }
        public int? SectionId { get; set; }
        public string? SectionName { get; set; }
    

    }
    
    #endregion

    public class PatientRegistrationDashboardCardCount
    {
        public int TokenIssuesCount { get; set; }
        public int OverAllRegistration { get; set; }
        public int NewRegistrationCount { get; set; }
        
        public int ReVisitCount { get; set; }
    }

    #region Patient Reffered Count By User
    public class VisitCountByDeptBySec
    {
       
        public string? name { get; set; }
        public int? value { get; set; }
    }

    public class OPDSectionWiseTokenCountDTO
    {
        public string? name { get; set; }
        public int? extra { get; set; }
        public int? value { get; set; }
    }

    #endregion

}
