using HMIS.Patient.Domain.Models.DTO.MedicineDispatchDto;
using HMIS.Patient.Domain.Models.DTO.PatientDiagnoseDto;
using HMIS.Patient.Domain.Models.DTO.PatientVitalDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.PatientDto
{
    public class CreatePatientHistoryDTO
    {
        public CreateOrEditPatientWithVisitDto? PatientRegister { get; set; }
        public CreateOrEditPatientVitalDto? PatientVital { get; set; }
        public CreateOrEditPatientDiagnoseWithPrescriptionDto? PatientDiagnose { get; set; }
        public CreateOrEditPatientMedicineDispatchDto? PatientPharmacy { get; set; }

    }
}
