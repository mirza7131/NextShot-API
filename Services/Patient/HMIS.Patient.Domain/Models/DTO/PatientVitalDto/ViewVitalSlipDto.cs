using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.PatientVitalDto
{
    public class ViewVitalSlipDto
    {
        public string HealthFacilityName { get; set; } = null!;
        public string TokenNo { get; set; } = null!;
        public Guid? PatientVitalId { get; set; }
        public int? VisitNo { get; set; }
        public string? vitalsFloor { get; set; }
        public string? vitalsRoom { get; set; }
        public string? DoctorFloorNo { get; set; }
        public string? DoctorRoomNo { get; set; }
        public DateTime? VisitDate { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string PatientName { get; set; } = null!;
        public string? MrNo { get; set; } = null!;
        public string VitalsBy { get; set; } = null!;
        public string VitalsByDesignation { get; set; } = null!;
        public string? Bpsystolic { get; set; }
        public string? BpdiaSystolic { get; set; }
        public string? Pulse { get; set; }
        public string? Temprature { get; set; }
        public string? Weight { get; set; }
        public string? Height { get; set; }
        public string? ResperatoryRate { get; set; }
        public string? PatientCondition { get; set; }
    }
}
