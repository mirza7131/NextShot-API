using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.DrugAddict.Domain.Models.Dto.SocialWelfareFormDto
{
    public class ViewPatientDiagnoseRecordDto
    {
        public Guid PatientDiagnosisRecordId { get; set; }
        public Guid PatientVisitId { get; set; }
        public Guid PatientId { get; set; }
        public Guid PatientDiagnoseId { get; set; }
        public bool? IsDiagnoseExternally { get; set; }
        public string? SourceSystem { get; set; }
        public string? DoctorHealthFacility { get; set; }
        public string? DoctorDepartment { get; set; }
        public string? DoctorSection { get; set; }
        public string? DiagnosedBy { get; set; }
        public string? DoctorDesignation { get; set; }
        public DateTime? DiagnosedOn { get; set; }
        public string? FormType { get; set; }
        public string? Json { get; set; }
        public bool? IsActive { get; set; }
        public List<ViewMedicineDispatchDto> MedicineDispatches { get; set; }
    }
}
