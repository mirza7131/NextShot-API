using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.MEAs.Domain.Models.DTO.Visists
{
    public class LastVisitListDTO
    {

        public int Id { get; set; }
        public string? HFMISCode { get; set; }
        public string? HealthFacilityName { get; set; }
        public string? ShiftName { get; set; }
        public string? ZoneName { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? DivisionName { get; set; }
        public string? DistrictName { get; set; }
        public string? TehsilName { get; set; }
        public string? DivisionCode { get; set; }
        public string? DistrictCode { get; set; }
        public string? TehsilCode { get; set; }
        public string? MonitoredBy { get; set; }
        public string? FacilityStatusName { get; set; }
        public string? CloseReason { get; set; }

    }

    public class UserVisitListDTO
    {
        public int VisitId { get; set; }
        public int HfId { get; set; }
        public string? HFMISCode { get; set; }
        public string?  HealthFacilityName { get; set; }
        public int ShiftId { get; set; }
        public string? ShiftName { get; set; }
        public int UserId { get; set; }
        public string? Username { get; set; }
        public string? FullName { get; set; }
        public bool IsVisited { get; set; }
        public DateTime? VisitedDate { get; set; }
        public bool IsRepeat { get; set; }
        public bool IsSpecial { get; set; }
        public string? ModeName { get; set; }
        public bool patientExitInterviewVisit { get; set; }
        public bool HealthCouncilVisit { get; set; }
        public bool CVCVisit { get; set; }
    }
}
