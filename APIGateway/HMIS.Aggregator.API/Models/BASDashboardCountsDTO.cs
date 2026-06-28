using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Aggregator.API.Models
{
    public class BASDashboardCountsDTO
    {

        public IEnumerable<string> Data { get; set; }
        public Exception Exception { get; set; }
        public bool Succeeded { get; set; }
        public string[] Errors { get; set; }
        public List<string> AlreadyCompletedHFList { get; set; }
        public List<string> CompletedHFList { get; set; }
        public List<string> WithoutShiftHFList { get; set; }
        public List<string> AlreadyRosteredEmployeeList { get; set; }
        public string Message { get; set; }
        public int completedCount { get; set; }
        public int TotalCount { get; set; }

        public ObjDataCounts Obj { get; set; }
    }
    public class ObjDataCounts
    {
        public int Awaked { get; set; }
        public int BHURostersDone { get; set; }
        public int BHURostersNotDone { get; set; }
        public int DHQRostersDone { get; set; }
        public int DHQRostersNotDone { get; set; }
        public int NotAvailableDeviceCount { get; set; }
        public int NotAwaked { get; set; }
        public int OutOfOrderDevices { get; set; }
        public int RHCRostersDone { get; set; }
        public int RHCRostersNotDone { get; set; }
        public int Registered { get; set; }
        public int RegisteredDevice { get; set; }
        public int RosterGenerated { get; set; }
        public int RosterNotGenerated { get; set; }
        public int StolenDevices { get; set; }
        public int SyncedCount { get; set; }
        public int THQRostersDone { get; set; }
        public int THQRostersNotDone { get; set; }
        public int TotalDevices { get; set; }
        public int TotalEmployees { get; set; }
        public int UnRegistered { get; set; }
        public int UnRegisteredDevice { get; set; }
        public int UnSyncedCount { get; set; }
        public int totalDeviceAwakedSec { get; set; }
    }

    public class ResponseDutyRosterEmployeeListDTO
    {
        public int Count { get; set; } = 0;
        public string? Message { get; set; }
        public bool Success { get; set; }
        public List<DutyRosterEmployeeListDTO> Data { get; set; }

    }
    public partial class DutyRosterEmployeeListDTO
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public string Cnic { get; set; }
        public int? DesignationId { get; set; }
        public string DesignationName { get; set; }
        public string ModeName { get; set; }
        public string HftypeCode { get; set; }
        public string HfmisCode { get; set; }
        public string Division { get; set; }
        public string SubStatusName { get; set; }
        public int? SubStatusId { get; set; }
        public string District { get; set; }
        public string Tehsil { get; set; }
        public string HealthFacility { get; set; }
        public string ProfileType { get; set; }
        public string Thumb { get; set; }
    }

    public class ResponseDeivcesListAndSyncCountsDTO
    {
        public int Count { get; set; } = 0;
        public string? Message { get; set; }
        public bool Success { get; set; }
        public List<DeivcesListAndSyncCountsDTO> Data { get; set; }
    }
    public class DeivcesListAndSyncCountsDTO
    {
        public int DeviceRegId { get; set; }
        public string HfmisCode { get; set; }
        public string DivisionName { get; set; }
        public string DistrictName { get; set; }
        public string TehsilName { get; set; }
        public string FullName { get; set; }
        public string HFTypeCode { get; set; }
        public string HFCode { get; set; }
        public string DeviceCode { get; set; }
        public string IsSynced { get; set; }
        public string DeviceStatus { get; set; }
        public int DeviceStatusProfileId { get; set; }
        public long IMEI { get; set; }
        public string ModeName { get; set; }
        public string HFTypeName { get; set; }
        public DateTime? LastSynDate { get; set; }
    }
    public class ResponseDeivcesListAndAwakedCountsDTO
    {
        public int Count { get; set; } = 0;
        public string? Message { get; set; }
        public bool Success { get; set; }
        public List<DeivcesListAndAwakedCountsDTO> Data { get; set; }
    }
    public class DeivcesListAndAwakedCountsDTO
    {
        public string HfmisCode { get; set; }
        public string DivisionName { get; set; }
        public string DistrictName { get; set; }
        public string TehsilName { get; set; }
        public string FullName { get; set; }
        public string HFTypeCode { get; set; }
        public string HFCode { get; set; }
        public string DeviceCode { get; set; }
        public string IsAwaked { get; set; }
        public string DeviceStatus { get; set; }
        public string IMEI { get; set; }
        public string ModeName { get; set; }
        public string HFTypeName { get; set; }
        public DateTime? LastAwakedDate { get; set; }
    }
    public class ResponseRosterDoneUnDoneListDTO
    {
        public int Count { get; set; } = 0;
        public string? Message { get; set; }
        public bool Success { get; set; }
        public List<RosterDoneUnDoneListDTO> Data { get; set; }
    }
    public class RosterDoneUnDoneListDTO
    {
        public string HFMISCode { get; set; }
        public string DivisionName { get; set; }
        public string DistrictName { get; set; }
        public string TehsilName { get; set; }

        public string? HealthFacility { get; set; }
        public string? HFTypeCode { get; set; }
        public string Month { get; set; }
        public string RosterGenerated { get; set; }
    }

    public class ResponseAttendanceReportCountDTO
    {

        public IEnumerable<string> Data { get; set; }
        public Exception Exception { get; set; }
        public bool Succeeded { get; set; }
        public string[] Errors { get; set; }
        public List<string> AlreadyCompletedHFList { get; set; }
        public List<string> CompletedHFList { get; set; }
        public List<string> WithoutShiftHFList { get; set; }
        public List<string> AlreadyRosteredEmployeeList { get; set; }
        public string Message { get; set; }
        public int completedCount { get; set; }
        public int TotalCount { get; set; }

        public AttendanceReportCountDTO Obj { get; set; }
    }
    public class AttendanceReportCountDTO
    {
        public int ToBePresent { get; set; }
        public int Present { get; set; }
        public int Absent { get; set; }
        public int Late { get; set; }
        public int Onleave { get; set; }
        public int Holiday { get; set; }
        public int OffDay { get; set; }
        public int ThumbsNotRegistered { get; set; }
        public int UnRegisteredInRoster { get; set; }
    }
    public partial class AttendenceObject
    {
        public string HfmisCode { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public Boolean IsFilter { get; set; }
    }

    public class DutyRosterEmployeeNotInRosterCountDTO
    {
        public int TotalNotInRoster { get; set; }
    }

    public class ResponseDutyRosterEmployeeNotInRosterCountDTO
    {
        //public IEnumerable<string> Data { get; set; }
        public bool Succeeded { get; set; }
        public string[] Errors { get; set; }
        public List<string> AlreadyCompletedHFList { get; set; }
        public List<string> CompletedHFList { get; set; }
        public List<string> WithoutShiftHFList { get; set; }
        public List<string> AlreadyRosteredEmployeeList { get; set; }
        public string Message { get; set; }
        public int completedCount { get; set; }
        public int TotalCount { get; set; }

        public List<DutyRosterEmployeeNotInRosterCountDTO> Data { get; set; }
    }

    public class AttendanceListingDTO
    {
        public string HealthFacility { get; set; }
        public string HFMISCode { get; set; }
        public string DivisionName { get; set; }
        public string EmployeeName { get; set; }
        public string CNIC { get; set; }
        public string DesignationName { get; set; }
        public int EmployeeId { get; set; }
        public string Thumb { get; set; }
    }

    public class ResponseAttendanceListingDTO
    {
        public int Count { get; set; } = 0;
        public string? Message { get; set; }
        public bool Success { get; set; }
        public List<AttendanceListingDTO> Data { get; set; }
    }

    public class ResponseAttendenceReportCalenderDTO
    {
        public int Count { get; set; } = 0;
        public string? Message { get; set; }
        public bool Success { get; set; }
        public List<AttendenceReportCalenderDTO> Data { get; set; }
    }
    public class AttendenceReportCalenderDTO
    {
        public string HealthFacility { get; set; }
        public string EmployeeName { get; set; }
        public string CNIC { get; set; }
        public string DesignationName { get; set; }
        public int EmployeeId { get; set; }
        public string HFCode { get; set; }
        public List<CalenderDTO> Dates { get; set; }
    }
    public class CalenderDTO
    {
        public DateTime? DutyDate { get; set; }
        public string Status { get; set; }
    }

    public partial class DutyRosterAttendanceDownloadDTO
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string HealthFacility { get; set; }
        public string HFTypeCode { get; set; }
        public string HFTypeName { get; set; }
        public string DistrictName { get; set; }
        public string DivisionName { get; set; }
        public string TehsilName { get; set; }
        public string DesignationName { get; set; }
        public string ShiftName { get; set; }
        public string ShiftId { get; set; }
        public string HFCode { get; set; }
        public string Attendence { get; set; }
        public string Thumb { get; set; }
        public Nullable<DateTime> Date { get; set; }
        public string CNIC { get; set; }
        public string EmployeeCadre { get; set; }
        public string ProfileStatus { get; set; }
        public Nullable<DateTime> DutyDate { get; set; }
        public Nullable<DateTime> TimeIn { get; set; }
        public Nullable<DateTime> TimeOut { get; set; }
    }

}
