using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.NewFolder
{
    public class CreateOrEditCallDto
    {
        public Guid? CallDetailId { get; set; }

        public Guid? PatientId { get; set; }

        public Guid? PatientVisitId { get; set; }

        public Guid? PatientDiagnoseId { get; set; }

        public Guid? PatientStatusProfileId { get; set; }

        public int? HealthFacilityId { get; set; }

        public Guid? ProgramTypeProfileId { get; set; }

        public DateTime? ContactDateTime { get; set; }

        public Guid? CallReasonTypeProfileId { get; set; }

        public Guid? CallResultProfileId { get; set; }

        public string? ReasonDetail { get; set; }

        public DateTime? RevisitDateTime { get; set; }
    }
}
