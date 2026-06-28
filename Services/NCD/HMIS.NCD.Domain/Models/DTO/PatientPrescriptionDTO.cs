using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.NCD.Domain.Models.DTO
{
    public class PatientPrescriptionDTO
    {
        public PatientDiagnoseDto? patientDiagnose { get; set; }
        public List<CreateOrEditPatientPresCriptionDto>? patientPrescription { get; set; }
    }
}
