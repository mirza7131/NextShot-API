using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.MEAs.Domain.Models.DTO.Monitoring
{
    public class MonitoringMasterDTO
    {
        public string HFMISCode { get; set; }
        public int ApplicationTypeId { get; set; }
        public string CloseReason { get; set; }
        public string Comment { get; set; }
        public DateTime? Date { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool? FacilityStatus { get; set; }
        public int FacilityType { get; set; }
        public bool? IllegalOccupation { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public int ModuleId { get; set; }
        public int? VisityType { get; set; }
        public string WholeOrPart { get; set; }
        public InchargeDTO incharge { get; set; }
        public List<MonitoringAttendanceDTO> attendanceList { get; set; }
        public List<MonitoringChildDTO> indicatorslist { get; set; }
        public MonitoringFeedbackDTO feedback { get; set; }


    }


    public class MonitoringMasterFloodDTO
    {

        public string DistrictCode { get; set; }
        public int Camp_Id { get; set; }
        public int ModuleId { get; set; }
        public string TehsilCode { get; set; }
        public string CloseReason { get; set; }
        public string Comment { get; set; }

        public bool? IsActive { get; set; }
        public DateTime? Date { get; set; }
        public DateTime CreatedOn { get; set; }

        public InchargeDTO incharge { get; set; }
        public List<FloodModule> listofModules { get; set; }


    }

    public class VaccanciesMasterDTO
    {
        public int Id { get; set; }
        public string HFMISCode { get; set; }
        public int ApplicationTypeId { get; set; }
        public string CloseReason { get; set; }
        public string Comment { get; set; }
        public DateTime? Date { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool? FacilityStatus { get; set; }
        public int FacilityType { get; set; }
        public bool? IllegalOccupation { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public int? VisityType { get; set; }
        public string WholeOrPart { get; set; }
        public string DistrictCode { get; set; }
        public string TehsilCode { get; set; }
        public bool? IsActive { get; set; }
        public InchargeDTO incharge { get; set; }
        public List<VaccancyDesig> vaccancyDsg { get; set; }
    }
    public class VaccancyDesig
    {
        public int VacancyID { get; set; }
        public string VacancyTitle { get; set; }
        public int TotalEmployess { get; set; }
        public bool? IsActive { get; set; }

        public int MasterId { get; set; }
        public List<EmployessDTO> Employee { get; set; }
    }
    public class EmployessDTO
    {
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public string CNIC { get; set; }
        public string MobileNo { get; set; }
        public string Shift { get; set; }
        public string Option { get; set; }
        public int VacancyID { get; set; }
        public string HfmisCode { get; set; }
        public bool? IsActive { get; set; }

        public int MasterId { get; set; }
        public string VacancyTitle { get; set; }
        public int? TotalEmployees { get; set; }
        public string OriginalPostingFacility { get; set; }

        public string OriginalPosting { get; set; }

    }


    public class MonitoringMasterDTONew
    {

        public string HFMISCode { get; set; }
        public int ApplicationTypeId { get; set; }
        public string CloseReason { get; set; }
        public string Comment { get; set; }
        public DateTime? Date { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool? FacilityStatus { get; set; }
        public int FacilityType { get; set; }
        public bool? IllegalOccupation { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public int ModuleId { get; set; }
        public int? VisityType { get; set; }
        public string WholeOrPart { get; set; }
        public InchargeDTO incharge { get; set; }
        public List<MonitoringAttendanceDTO> attendanceList { get; set; }
        public List<MonitoringModule> listofModules { get; set; }
        public MonitoringFeedbackDTO feedback { get; set; }


    }

    public class FloodModule
    {
        public int? ModuleId { get; set; }

        public string ModuleName { get; set; }

        public List<MonitoringChildFloodDTO> indicatorslist { get; set; }


    }

    public class MonitoringModule
    {
        public int? ModuleId { get; set; }
        public int? ApplicationTypeId { get; set; }

        public string ModuleName { get; set; }

        public List<MonitoringChildDTO> indicatorslist { get; set; }


    }
}
