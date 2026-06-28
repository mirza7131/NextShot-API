using HMIS.MEAs.Domain.Models.DTO.Indicator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.MEAs.Domain.Models.DTO.Monitoring
{
    public class MonitoringMasterExport
    {
        public MonitoringMasterExport()
        {
            Indicators = new List<IndicatorDTO>();
            Answers = new List<string>();
        }
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
        public string HFMISCode { get; set; }
        public string HealthFacilityName { get; set; }
        public int ShiftId { get; set; }
        public int ModuleId { get; set; }
        public string ShiftName { get; set; }
        public string FacilityStatus { get; set; }
        public string CloseReason { get; set; }
        public string MonitoredBy { get; set; }
        public string MonitoredOn { get; set; }
        public string MonitoriedTime { get; set; }
        public string SyncOn { get; set; }
        public string SyncTime { get; set; }
        public DateTime MonitoringDate { get; set; }
        public DateTime SubmissionDate { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int HealthFacilityTypeId { get; set; }
        public string FacilityInchargeComment { get; set; }
        public string MEAComment { get; set; }
        public string InchargeDesignation { get; set; }
        public string InchargeName { get; set; }
        public string InchargeCNIC { get; set; }
        public string InchargeMobileNo { get; set; }
        public Boolean IllegalOccupation { get; set; }
        public string WholeOrPart { get; set; }
        public string LegalSince { get; set; }
        public List<string> Answers { get; set; }
        public List<IndicatorDTO> Indicators { get; set; }
    }



    public class AllDataExport
    {
        public AllDataExport()
        {
            Indicators = new List<IndicatorDTO>();
            Answers = new List<string>();
        }
        public int Id { get; set; }
        public string DivisionCode { get; set; }
        public string Division { get; set; }
        public string DivisionName { get; set; }
        public string DistrictCode { get; set; }
        public string DistrictName { get; set; }
        public string TehsilCode { get; set; }
        public string TehsilName { get; set; }
        public int ZoneId { get; set; }
        public string ZoneName { get; set; }
        public string ModeName { get; set; }
        public string ModuleName { get; set; }
        // public long HFMISCode { get; set; }
        public string Zone { get; set; }
        public string HfmisCode { get; set; }
        public string DHIS_FacilityCode { get; set; }
        public string AmbulanceNo { get; set; }
        public string HealthFacilityName { get; set; }
        public int ShiftId { get; set; }
        public int ModuleId { get; set; }
        public string ShiftName { get; set; }
        public string FacilityStatus { get; set; }
        public string CloseReason { get; set; }
        public string MonitoredBy { get; set; }
        public string MonitoredOn { get; set; }
        public string MonitoriedTime { get; set; }
        public string SyncOn { get; set; }
        public string SyncTime { get; set; }
        public string CreatedOndatetime { get; set; }
        public string SyncOndatetime { get; set; }
        public DateTime VisitTime { get; set; }
        public DateTime SyncDate { get; set; }
        public DateTime SubmissionTime { get; set; }
        public DateTime SubmissionDate { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int HealthFacilityTypeId { get; set; }
        public string FacilityInchargeComment { get; set; }
        public string MEAComment { get; set; }
        public string InchargeDesignation { get; set; }
        public string InchargeName { get; set; }
        public string InchargeCNIC { get; set; }
        public string InchargeMobileNo { get; set; }
        public Boolean IllegalOccupation { get; set; }
        public string WholeOrPart { get; set; }
        public string LegalSince { get; set; }
        public string GPS { get; set; }
        public string DHISCode { get; set; }
        public List<string> Answers { get; set; }
        public List<IndicatorDTO> Indicators { get; set; }
    }
}
