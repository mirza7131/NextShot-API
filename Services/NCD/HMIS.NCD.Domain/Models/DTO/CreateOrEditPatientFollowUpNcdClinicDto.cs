using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.NCD.Domain.Models.DTO
{
    public class CreateOrEditPatientFollowUpNcdClinicDto
    {
        public Guid? PatientFollowUpsId { get; set; }

        public Guid PatientId { get; set; }
        public Guid PatientVisitId { get; set; }
        public Guid PatientDiagnoseId { get; set; }

        public int HealthFacilityId { get; set; }

        public DateTime? NextFollowUpDate { get; set; }

        public DateTime? PreviousFollowUpdate { get; set; }

        public DateTime? LastIssueBookLetDateTime { get; set; }

        public string? LipidProfile { get; set; }

        public string? HbA1c { get; set; }

        public double? HbA1cpercent { get; set; }

        public int? CholesterolMgDl { get; set; }

        public int? TriGlycerideMgDl { get; set; }

        public int? TotalFollowUpNo { get; set; }

        public int? IsNcdFollowUps { get; set; }

        public int? LdlmgDl { get; set; }

        public bool? InProcess { get; set; }

        public bool? IsNcd { get; set; }

        public string? FormType { get; set; }

        public int? DocDepartmentLookupId { get; set; }

        public int? DocSectionLookupId { get; set; }
        public bool? IsFollowUp { get; set; }

        public virtual ICollection<CreateOrEditPatientLabTestDto>? PatientLabTests { get; set; }
        public List<CreateOrEditPatientPresCriptionDto>? PatientPrescription { get; set; }
        public List<CreateOrEditPatientDiagnoseDiseses>? PatientDiagnoseDiaeasae { get; set; }
    }
}
