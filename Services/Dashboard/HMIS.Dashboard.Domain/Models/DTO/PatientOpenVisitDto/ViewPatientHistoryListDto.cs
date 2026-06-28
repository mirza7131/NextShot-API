using HMIS.Dashboard.Domain.Models.DTO.PatientDiagnoseDiseaseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Dashboard.Domain.Models.DTO.PatientOpenVisitDto
{
    public class ViewPatientHistoryListDto
    {
        public ViewPatientHistoryListDto()
        {
            PatientDiagnoseDiseases = new List<ViewDiseaseListDto>();
        }
        public Guid PatientOpenVisitId { get; set; }

        public string? FormType { get; set; }
        public string? TokenNo { get; set; }

        public int? VisitNo { get; set; }

        public int? VisitTypeProfileId { get; set; }

        public Guid? PatientId { get; set; }
        public Guid? PatientDiagnoseId { get; set; }

        public int? HealthFacilityId { get; set; }

        public string? HealthFacilityName { get; set; }

        public int? DepartementLookupId { get; set; }
        public string? DepartementLookupName { get; set; }

        public int? SectionLookupId { get; set; }
        public string? SectionLookupName { get; set; }

        public Guid? CurrentStationProfileId { get; set; }

        public Guid? CurrentStationUserId { get; set; }

        public DateTime? VisitDate { get; set; }
        
        public DateTime? CreatedOn { get; set; }
        public string? DiagnosedBy { get; set; }
        public string? DiagnosedByDesignation { get; set; }

        public bool? IsDischarge { get; set; }
        public bool? HasLabTest { get; set; }

        public bool IsActive { get; set; }

        public string? DiseasesName { get; set; }

        public virtual IList<ViewDiseaseListDto>? PatientDiagnoseDiseases { get; set; }
    }

    public class ViewDiseaseListDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid PatientVisitId { get; set; }
        public Guid PatientDiagnoseId { get; set; }
    }
}
