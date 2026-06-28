using HMIS.Patient.Domain.Models.DTO.Patient;
using HMIS.Patient.Domain.Models.DTO.PatientPrescriptionDto;
using HMIS.Patient.Domain.Models.DTO.PatientVitalDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.PatientOpenVisitDto
{
    public class ViewPatientOpenVisitDto
    {
        public Guid PatientOpenVisitId { get; set; }

        public string? TokenNo { get; set; }

        public int? VisitNo { get; set; }

        public int? VisitTypeProfileId { get; set; }

        public Guid? PatientId { get; set; }

        public int? HealthFacilityId { get; set; }

        public int? DepartementLookupId { get; set; }

        public int? SectionLookupId { get; set; }

        public Guid? CurrentStationProfileId { get; set; }

        public Guid? CurrentStationUserId { get; set; }

        public DateTime? VisitDate { get; set; }

        public bool? IsDischarge { get; set; }

        public bool IsActive { get; set; }
        public virtual List<ViewPatientVitalDto> PatientVitals { get; set; }
        public virtual List<ViewPatientPrescriptionDto> PatientPrescriptions { get; set; }

    }
}
