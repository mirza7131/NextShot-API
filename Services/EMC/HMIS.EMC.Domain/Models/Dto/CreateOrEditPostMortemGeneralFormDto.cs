using HMIS.EMC.Domain.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.EMC.Domain.Models.Dto
{
    public class CreateOrEditPostMortemGeneralFormDto
    {
        public Guid? MlcpostmortemId { get; set; }

        public Guid? Mlcid { get; set; }

        public Guid PatientId { get; set; }

        public Guid PatientVisitId { get; set; }

        public Guid? PatientDiagnoseId { get; set; }

        public Guid? PatientStatusProfileId { get; set; }

        public int? HealthFacilityId { get; set; }

        public string? Pmrno { get; set; }

        public string? FatherHusbandName { get; set; }
        public Guid? McdtypeProfileId { get; set; }

        public Guid? PcdtypeProfileId { get; set; }

        public string? ConsentFile { get; set; }

        public string? IncidentPlace { get; set; }
        public string? HealthFacilityName { get; set; }
        public string? FullName { get; set; }

        public Guid? ImageTypeProfileId { get; set; }

        public Guid? PoliceSignatureTypeProfileId { get; set; }

        public DateTime? PoliceSignatureDateTime { get; set; }
        public string? PoliceInformation { get; set; }

        public long? ReportCounts { get; set; }
        public string? Radiological { get; set; }

        public string? UltraSound { get; set; }

        public bool? IsFinalReport { get; set; }
        public bool? IsReportCount { get; set; }

        public DateTime? DeathDateTime { get; set; }

        public DateTime? RecieveDateTime { get; set; }

        public DateTime? DoctorVisitDateTime { get; set; }

        public DateTime? AutopsyDateTime { get; set; }

        public string? FormType { get; set; }

        public int? DocDepartmentLookupId { get; set; }

        public int? DocSectionLookupId { get; set; }

        public List<MLEBodyIdentifierInfoDto>? BodyIdentifierInfoDtos { get; set; }

        public CreateOrEditMlcPoliceInfoDto? mlcpoliceInfos { get; set; }
        public CreateOrEditMLCDto? mlc { get; set; }
    }
}
