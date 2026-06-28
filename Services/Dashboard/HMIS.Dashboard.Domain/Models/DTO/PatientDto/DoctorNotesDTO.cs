using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Dashboard.Domain.Models.DTO.PatientDto
{
    public class DoctorNotesDTO
    {
        public Guid? PatientId { get; set; }

        public Guid? PatientDiagnoseId { get; set; }

        public Guid? PatientVisitId { get; set; }

        public byte? PatientDepartmentId { get; set; }

        public string? Notes { get; set; }

        public DateTime? CreatedOn { get; set; }

        public Guid? CreatedBy { get; set; }

        public byte? ActionTypeId { get; set; }
    }
}
