using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.NCD.Domain.Models.DTO
{
    public class CreateOrEditPatientDiagnoseDiseses
    {
        public Guid? PatientDiagnoseDiseaseId { get; set; }
        public Guid DiseaseProfileId { get; set; }
        public long? UserLogId { get; set; }
        public byte? DiagnoseTypeId { get; set; }
        // It is for DiseaseStatus Entry
        public Guid? DiseaseStatusId { get; set; }
        public Guid DiseaseStatusTypeProfileId { get; set; }
    }
}
