using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.MEAs.Domain.Models.DTO.Monitoring
{
    public class MonitoringListDTO
    {
        public int Id { get; set; }
        public string DivisionCode { get; set; }
        public string DivisionName { get; set; }
        public string DistrictCode { get; set; }
        public string DistrictName { get; set; }
        public string TehsilCode { get; set; }
        public string TehsilName { get; set; }
        public int ZoneId { get; set; }
        public string ZoneName { get; set; }
        public string ModeName { get; set; }
        public string ModuleName { get; set; }
        public string HealthFacilityName { get; set; }
        public int ShiftId { get; set; }
        public string ShiftName { get; set; }
        public string MonitoredBy { get; set; }
        public DateTime MonitoringDate { get; set; }
        public DateTime SyncOn { get; set; }
        public int HealthFacilityTypeId { get; set; }
        public bool? FacilityStatus { get; set; }
        public string CloseReason { get; set; }
        public string FacilityStatusName { get; set; }
    }


    public class MonitoringListDTONew
    {
        public int Id { get; set; }
        public string DivisionCode { get; set; }
        public string DivisionName { get; set; }
        public string DistrictCode { get; set; }
        public string DistrictName { get; set; }
        public string TehsilCode { get; set; }
        public string TehsilName { get; set; }
        public int ZoneId { get; set; }
        public string ZoneName { get; set; }
        public string ModeName { get; set; }
        public string ModuleName { get; set; }
        public string HealthFacilityName { get; set; }
        public int ShiftId { get; set; }
        public string ShiftName { get; set; }
        public string MonitoredBy { get; set; }
        public DateTime MonitoringDate { get; set; }
        public DateTime SyncOn { get; set; }
        public int HealthFacilityTypeId { get; set; }
        public bool? FacilityStatus { get; set; }
        public string CloseReason { get; set; }
        public string FacilityStatusName { get; set; }
        public string modules { get; set; }
        public int type { get; set; }
    }

    //For HF Bed Survey APP

    //public class HFBedSurveyMonitoringListDTO
    //{
    //    public int Id { get; set; }
    //    public string DivisionCode { get; set; }
    //    public string DivisionName { get; set; }
    //    public string DistrictCode { get; set; }
    //    public string DistrictName { get; set; }
    //    public string TehsilCode { get; set; }
    //    public string TehsilName { get; set; }
    //    public string ModeName { get; set; }
    //    public DateTime SyncDate { get; set; }
    //    public DateTime CreatedDate { get; set; }
    //    public string IsActive { get; set; }
    //    public string FullName { get; set; }
    //    public string HealthFacilityName { get; set; }
    //    public int MasterId { get; set; }
    //    public string MEAComment { get; set; }

    //    public string WardName { get; set; }

    //}

    public class HFBedSurveyMonitoringListDTO
    {
        public int Id { get; set; }
        public int HfId { get; set; }
        public string DivisionCode { get; set; }
        public string HFMISCode { get; set; }
        public string DivisionName { get; set; }
        public string DistrictCode { get; set; }
        public string DistrictName { get; set; }
        public string TehsilCode { get; set; }
        public string TehsilName { get; set; }
        public string ModeName { get; set; }
        public DateTime SyncDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string IsActive { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string HealthFacilityName { get; set; }
        public int MasterId { get; set; }
        public string MEAComment { get; set; }
        public string WardName { get; set; }
        public string No_of_Beds { get; set; }
        public string AvailableBeds { get; set; }
        public string AdmitPatients { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }

    }

    //HrEmployeeDetailList
    public class HrEmployeeMonitoringListDTO
    {
        public int Id { get; set; }
        public int HfId { get; set; }
        public string DivisionCode { get; set; }
        public string HFMISCode { get; set; }
        public string DivisionName { get; set; }
        public string DistrictCode { get; set; }
        public string DistrictName { get; set; }
        public string TehsilCode { get; set; }
        public string TehsilName { get; set; }
        public string ModeName { get; set; }
        public DateTime SyncDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public string IsActive { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string HealthFacilityName { get; set; }
        public int MasterId { get; set; }
        public string CNIC { get; set; }
        public string VacancyTitle { get; set; }
        public string TotalEmployees { get; set; }
        public string Shift { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string Option { get; set; }
        public string Comment { get; set; }
        public string SyncOn { get; set; }
        public string OriginalPostingFacility { get; set; }
        public string OriginalPosting { get; set; }


    }



    public class tbl_BedsChildList
    {
        public int Id { get; set; }
        public string wardName { get; set; }
        public string No_of_Beds { get; set; }
        public string AvailableBeds { get; set; }
        public string AdmitPatients { get; set; }
        public int BedMaster_Id { get; set; }
        public int HFW_Id { get; set; }
        public string IsActive { get; set; }
        public string MEAComment { get; set; }

    }
}
