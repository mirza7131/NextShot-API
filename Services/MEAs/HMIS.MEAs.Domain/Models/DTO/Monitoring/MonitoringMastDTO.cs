using HMIS.MEAs.Domain.Models.DTO.Indicator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.MEAs.Domain.Models.DTO.Monitoring
{
    public class MonitoringMastDTO
    {
        public MonitoringMastDTO()
        {
            Indicators = new List<ViewIndicatorDTO>();
        }
        public int MonitoringId { get; set; }
        public string ApplicationTypeName { get; set; }
        public string ModuleName { get; set; }
        public string FaciltyTypeName { get; set; }
        public string ShiftName { get; set; }
        public string HealthFacilityName { get; set; }
        public bool FacilityStatus { get; set; }
        public bool IllegalOccupation { get; set; }
        public string WholeOrPart { get; set; }
        public int CloseReasonId { get; set; }
        public string CloseReason { get; set; }
        public DateTime IllegalOccupationSince { get; set; }
        public string InchargeName { get; set; }
        public string InchargeDesignation { get; set; }
        public string InchargeCNIC { get; set; }
        public string InchargeMobileNo { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime SyncOn { get; set; }
        public int ApplicationTypeId { get; set; }
        public int ModuleId { get; set; }
        public int HealthFacilityTypeId { get; set; }
        public int ShiftTypeId { get; set; }
        public string HfmisCode { get; set; }
        public int HfId { get; set; }
        public string DivisionName { get; set; }
        public string DistrictName { get; set; }
        public string TehsilName { get; set; }
        public List<ViewIndicatorDTO> Indicators { get; set; }
    }
    public class MonitoringMastAllDTO
    {
        public MonitoringMastAllDTO()
        {
            Indicators = new List<ViewIndicatorDTO>();
            monitoringAttendaces = new List<MonitoringAttendace>();
            Comments = new List<MonitoringComments>();
        }
        public int MonitoringId { get; set; }
        public string ApplicationTypeName { get; set; }
        public string ModuleName { get; set; }
        public string FaciltyTypeName { get; set; }
        public string ShiftName { get; set; }
        public string HealthFacilityName { get; set; }
        public bool FacilityStatus { get; set; }
        public bool IllegalOccupation { get; set; }
        public string WholeOrPart { get; set; }
        public int CloseReasonId { get; set; }
        public string CloseReason { get; set; }
        public DateTime IllegalOccupationSince { get; set; }
        public string InchargeName { get; set; }
        public string InchargeDesignation { get; set; }
        public string InchargeCNIC { get; set; }
        public string InchargeMobileNo { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime SyncOn { get; set; }
        public int ApplicationTypeId { get; set; }
        public int ModuleId { get; set; }
        public int HealthFacilityTypeId { get; set; }
        public int ShiftTypeId { get; set; }
        public string HfmisCode { get; set; }
        public int HfId { get; set; }
        public string DivisionName { get; set; }
        public string DistrictName { get; set; }
        public string TehsilName { get; set; }
        public List<ViewIndicatorDTO> Indicators { get; set; }
        public List<MonitoringAttendace> monitoringAttendaces { get; set; }
        public List<MonitoringComments> Comments { get; set; }
    }

    public class MonitoringMastAllDTONew
    {
        public MonitoringMastAllDTONew()
        {
            Indicators = new List<ViewIndicatorDTO>();
            monitoringAttendaces = new List<MonitoringAttendace>();
            Comments = new List<MonitoringComments>();
        }
        public int MonitoringId { get; set; }
        public string ApplicationTypeName { get; set; }
        public string ModuleName { get; set; }
        public string FaciltyTypeName { get; set; }
        public string ShiftName { get; set; }
        public string HealthFacilityName { get; set; }
        public bool FacilityStatus { get; set; }
        public bool IllegalOccupation { get; set; }
        public string WholeOrPart { get; set; }
        public int CloseReasonId { get; set; }
        public string CloseReason { get; set; }
        public DateTime IllegalOccupationSince { get; set; }
        public string InchargeName { get; set; }
        public string InchargeDesignation { get; set; }
        public string InchargeCNIC { get; set; }
        public string InchargeMobileNo { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime SyncOn { get; set; }
        public int ApplicationTypeId { get; set; }
        public int ModuleId { get; set; }
        public int HealthFacilityTypeId { get; set; }
        public int ShiftTypeId { get; set; }
        public string HfmisCode { get; set; }
        public int HfId { get; set; }
        public string DivisionName { get; set; }
        public string DistrictName { get; set; }
        public string TehsilName { get; set; }
        public List<ViewIndicatorDTO> Indicators { get; set; }
        public List<MonitoringAttendace> monitoringAttendaces { get; set; }
        public List<MonitoringComments> Comments { get; set; }
    }
    public class MonitoringAttendace
    {
        public string Name { get; set; }
        public string CNIC { get; set; }
        public string DesignationName { get; set; }
        public int DesignationId { get; set; }
        public string ContactNo { get; set; }
        public string PresentStatus { get; set; }
    }
    public class MonitoringComments
    {
        public MonitoringComments()
        {
            monitoringAttachments = new List<MonitoringAttachments>();
        }
        public int FeedbackId { get; set; }
        public string FacilityIncahrgeComments { get; set; }
        public string MEAComments { get; set; }
        public List<MonitoringAttachments> monitoringAttachments { get; set; }
    }
    public class MonitoringAttachments
    {
        public string ImagePath { get; set; }
        public string ImageName { get; set; }
    }
}
