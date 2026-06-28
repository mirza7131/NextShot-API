using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.NursingEventDto
{
    public class CreateOrEditNursingEventDto
    {
        public Guid? NursingEventsId { get; set; }

        public Guid? PatientId { get; set; }

        public Guid? PatientVisitId { get; set; }

        public Guid? PatientDiagnoseId { get; set; }

        public long? HealthFacilityId { get; set; }

        public int? DepartmentLookupId { get; set; }

        public int? SectionLookupId { get; set; }

        public string? Events { get; set; }

        public DateTime? AcknowledgedOn { get; set; }

        public Guid? AcknowledgedBy { get; set; }

        public bool IsActive { get; set; }

    }
}
