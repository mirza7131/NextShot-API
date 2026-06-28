using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.HCP.Domain.Models.DTO.PatientDiagnoseDtos
{
    public class PatientPreviousDiagnoseDto
    {
        public Guid? PatientId { get; set; }
        public DateTime? PreviousScreeningDate { get; set; }
    }
}
