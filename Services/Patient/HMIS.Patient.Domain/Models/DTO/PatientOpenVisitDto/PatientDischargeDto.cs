using HMIS.Patient.Domain.Models.DTO.PatientDiagnoseDto;
using HMIS.Patient.Domain.Models.DTO.PatientDischargeDetailDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.PatientOpenVisitDto
{
    public class PatientDischargeDto
    {
        public PatientDischargeDto()
        {
            PatientDischargeDetails = new CreateOrEditPatientDischargeDetailDto();
            PatientDiagnose = new CreateOrEditPatientDiagnoseWithPrescriptionDto();
        }
        public Guid PatientVisitId { get; set; }
        public Guid PatientId { get; set; }
        public virtual CreateOrEditPatientDischargeDetailDto PatientDischargeDetails { get; set; }
        public virtual CreateOrEditPatientDiagnoseWithPrescriptionDto PatientDiagnose { get; set; }


    }
}
