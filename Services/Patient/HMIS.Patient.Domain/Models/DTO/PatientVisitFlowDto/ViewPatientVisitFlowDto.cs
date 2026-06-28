using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.PatientVisitFlowDto
{
    public class ViewPatientVisitFlowDto
    {
        public Guid? PatientVisitFlowId { get; set; }
        public int? PreviousDepartmentId { get; set; }
        public int? PreviousSectionId { get; set; }
        public int? CurrentDepartmentId { get; set; }
        public int? CurrentSectionId { get; set; }
        public int? HealthFacilityId { get; set; }
        public Guid? PatientVisitId { get; set; }
        public Guid? PatientDiagnoseId { get; set; }
        public bool IsActive { get; set; }
        public bool? IsVisitClose { get; set; }
        public Guid? ReferedBy { get; set; }
        public bool? IsFilterClinic { get; set; }

        public bool? IsConsultant { get; set; }

        public int? ProvinceId { get; set; }
        public int? DivisionId { get; set; }
        public int? DistrictId { get; set; }
        public int? TehsilId { get; set; }
    }
}
