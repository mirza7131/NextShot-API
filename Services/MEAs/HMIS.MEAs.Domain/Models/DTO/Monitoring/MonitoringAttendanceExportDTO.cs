using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.MEAs.Domain.Models.DTO.Monitoring
{
    public class MonitoringAttendanceExportDTO
    {
        public int Id { get; set; }
        public string DivisionCode { get; set; }
        public string DistrictCode { get; set; }
        public string TehsilCode { get; set; }
        public int? ZoneId { get; set; }
        public string DivisionName { get; set; }
        public string DistrictName { get; set; }
        public string TehsilName { get; set; }
        public string ZoneName { get; set; }
        public string HFMISCode { get; set; }
        public string HealthFacilityName { get; set; }
        public string ModeName { get; set; }
        public string ShiftName { get; set; }
        public string MonitoredBy { get; set; }
        public string MonitoredOn { get; set; }
        public string MonitoriedTime { get; set; }
        public string SyncOn { get; set; }
        public string SyncTime { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string ModuleName { get; set; }
        public string FacilityStatus { get; set; }
        public string CloseReason { get; set; }
        public string InchargeDesignation { get; set; }
        public string InchargeName { get; set; }
        public string InchargeCNIC { get; set; }
        public string InchargeMobileNo { get; set; }
        public Boolean IllegalOccupation { get; set; }
        public string WholeOrPart { get; set; }
        public string LegalSince { get; set; }
        public string AttandenceName { get; set; }
        public string AttendanceContactNo { get; set; }
        public string AttendanceDesignation { get; set; }
        public string AttendanceCNIC { get; set; }
        public string TypeOfAbsense { get; set; }
        public string MEAComment { get; set; }
        public string FacilityInchargeComment { get; set; }
    }


    public class MonitoringAttendanceExportDTONew
    {
        public int Id { get; set; }
        public string DivisionCode { get; set; }
        public string DistrictCode { get; set; }
        public string TehsilCode { get; set; }
        public int? ZoneId { get; set; }
        public string DivisionName { get; set; }
        public string DistrictName { get; set; }
        public string TehsilName { get; set; }
        public string ZoneName { get; set; }
        public string HFMISCode { get; set; }
        public string HealthFacilityName { get; set; }
        public string ModeName { get; set; }
        public string ShiftName { get; set; }
        public string MonitoredBy { get; set; }
        public string MonitoredOn { get; set; }
        public string MonitoriedTime { get; set; }
        public string SyncOn { get; set; }
        public string SyncTime { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string ModuleName { get; set; }
        public string FacilityStatus { get; set; }
        public string CloseReason { get; set; }
        public string InchargeDesignation { get; set; }
        public string InchargeName { get; set; }
        public string InchargeCNIC { get; set; }
        public string InchargeMobileNo { get; set; }
        public Boolean IllegalOccupation { get; set; }
        public string WholeOrPart { get; set; }
        public string LegalSince { get; set; }
        public string AttandenceName { get; set; }
        public string AttendanceContactNo { get; set; }
        public string AttendanceDesignation { get; set; }
        public string AttendanceCNIC { get; set; }
        public string TypeOfAbsense { get; set; }
        public string MEAComment { get; set; }
        public string FacilityInchargeComment { get; set; }
    }
}
