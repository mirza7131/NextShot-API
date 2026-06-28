using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.PatientWorkFlowLogDto
{
    public class ViewPatientWorkFlowLogDto
    {
        public Guid PatientWorkFlowLogId { get; set; }

        public Guid? PatientId { get; set; }

        public Guid? PatientVisitId { get; set; }

        public int? HealthFacilityId { get; set; }

        public Guid? CurrentStationProfileId { get; set; }

        public Guid? NextStationProfileId { get; set; }

        public bool? IsVisitClose { get; set; }

        public string? TimeDifference { get; set; }

        public bool IsActive { get; set; }
    }
}
