using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Dashboard.Domain.Models.DTO.PatientDiagnoseRecordDto
{
    public class CreateOrEditPatientDiagnoseRecordDto
    {
        public Guid? PatientDiagnosisRecordId { get; set; }

        public Guid PatientVisitId { get; set; }

        public Guid PatientId { get; set; }

        public Guid PatientDiagnoseId { get; set; }

        public string? FormType { get; set; }

        public string? Json { get; set; }

        public bool? IsActive { get; set; }
    }
}
