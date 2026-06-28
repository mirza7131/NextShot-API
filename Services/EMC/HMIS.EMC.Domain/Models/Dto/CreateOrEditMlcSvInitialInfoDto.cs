using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.EMC.Domain.Models.Dto
{
    public class CreateOrEditMlcSvInitialInfoDto
    {
        public Guid? MlcsvinitialInfoId { get; set; }

        public Guid? Mlcid { get; set; }

        public Guid PatientId { get; set; }

        public Guid PatientVisitId { get; set; }

        public Guid? PatientDiagnoseId { get; set; }

        public Guid? PatientStatusProfileId { get; set; }

        public int? HealthFacilityId { get; set; }
        public int? ReportCounts { get; set; }

        public string? EmergencyNo { get; set; }
        public string? HealthFacilityName { get; set; }
        public string? PatientName { get; set; }

        public string? DaughterWifeOf { get; set; }

        public Guid? PatientImageId { get; set; }

        public Guid? PatientFingerPrintId { get; set; }

        public Guid? PatientSignatureId { get; set; }

        public Guid? GuardianSignatureId { get; set; }

        public Guid? PoliceSignatureId { get; set; }

        public bool? IsBroughtDead { get; set; }
        public bool? IsReportCount { get; set; }

        public int? RefferToHealthFacilityId { get; set; }

        public DateTime? ArrivalDateTime { get; set; }
        public Guid? CaseTypeProfileId { get; set; }

        public string? IncidentPlace { get; set; }
        public DateTime? ExaminationDateTime { get; set; }

        public string? AccompaniedBy { get; set; }

        public string? CourtOrder { get; set; }

        public string? NameOfOfficialAccompany { get; set; }

        public string? Mlcsvremark1 { get; set; }

        public string? Mlcsvremark2 { get; set; }
        public string? FormType { get; set; }
        public int? DocDepartmentLookupId { get; set; }

        public int? DocSectionLookupId { get; set; }

        public DateTime? AdmissionDateTime { get; set; }

        public DateTime? DischargeDateTime { get; set; }

        public bool? IsFinalReport { get; set; }

        public virtual CreateOrEditMLCDto? createOrEditMLCDtos { get; set; }

        public CreateOrEditPatientImageDto? PatientImage { get; set; }
        public CreateOrEditPatientImageDto? PatientFingerPrint { get; set; }
        public CreateOrEditPatientImageDto? PatientSignature { get; set; }
        public CreateOrEditPatientImageDto? GuardianSignature { get; set; }
        public CreateOrEditPatientImageDto? PoliceSignature { get; set; }
    }
}
