using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.PatientVitalDto
{
    public class ViewPatientVitalDto
    {
        public Guid? PatientVitalId { get; set; }

        public Guid? PatientId { get; set; }

        public Guid? PatientVisitId { get; set; }

        public string? Bpsystolic { get; set; }

        public string? BpdiaSystolic { get; set; }

        public string? Pulse { get; set; }

        public string? Temprature { get; set; }

        public string? Weight { get; set; }

        public string? Height { get; set; }

        public string? ResperatoryRate { get; set; }

        public int? DepartmentLookupId { get; set; }

        public int? SectionLookupId { get; set; }

        public Guid? VitalsCollectedBy { get; set; }

        public bool IsActive { get; set; }
    }
}
