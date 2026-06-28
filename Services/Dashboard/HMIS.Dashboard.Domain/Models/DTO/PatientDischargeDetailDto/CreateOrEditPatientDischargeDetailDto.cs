using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Dashboard.Domain.Models.DTO.PatientDischargeDetailDto
{
    public class CreateOrEditPatientDischargeDetailDto
    {
        public Guid? PatientDischargeDetailId { get; set; }

        public Guid PatientId { get; set; }

        public Guid PatientVisitId { get; set; }

        public Guid? DischargePatientDiagnoseId { get; set; }
        public DateTime? DateOfDischarge { get; set; }

        public Guid? DischargeStatusProfileId { get; set; }

        public string? Reason { get; set; }
        public string? ReferHealthFacility { get; set; }

        public bool IsActive { get; set; }
    }
}
