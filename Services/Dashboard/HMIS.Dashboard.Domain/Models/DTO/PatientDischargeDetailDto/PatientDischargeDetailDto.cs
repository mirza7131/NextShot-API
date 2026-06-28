using HMIS.Dashboard.Domain.Models.DTO.PatientDiagnoseRecordDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Dashboard.Domain.Models.DTO.PatientDischargeDetailDto
{
    public class PatientDischargeDetailDto
    {
        public PatientDischargeDetailDto()
        {
            PatientDischargeDetails = new ViewPatientDischargeDetailDto();
            PatientDiagnose = new ViewPatientDiagnoseRecordDto();
        }
        public Guid PatientVisitId { get; set; }
        public Guid PatientId { get; set; }
        public string? DischargeStatusProfileName { get; set; }
        public string? DepartmentName { get; set; }
        public string? SectionName { get; set; }
        public virtual ViewPatientDischargeDetailDto PatientDischargeDetails { get; set; }
        public virtual ViewPatientDiagnoseRecordDto PatientDiagnose { get; set; }
    }
}
