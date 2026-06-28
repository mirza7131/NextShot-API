using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.EMC.Domain.Models.Dto
{
    public class CreateOrEditMLEBasicInfoDto
    {

        public Guid? MlebasicInfoId { get; set; }

        public Guid PatientId { get; set; }

        public Guid PatientVisitId { get; set; }

        public Guid? PatientDiagnoseId { get; set; }

        public Guid? PatientStatusProfileId { get; set; }

        public int? HealthFacilityId { get; set; }

        public string? Mlcno { get; set; }

        public string? BookNo { get; set; }

        public DateTime? Dob { get; set; }

        public string? Relation { get; set; }

        public string? PoliceDistrict { get; set; }

        public DateTime? MlcDate { get; set; }

        public int? RefferToHealthFacilityId { get; set; }

        public Guid? CaseTypeProfileId { get; set; }
        public Guid? DoctorId { get; set; }

        public string? IncidentPlace { get; set; }

        public string? MlcRemark1 { get; set; }

        public string? MlcRemark2 { get; set; }

        public string? AccompaniesBy { get; set; }

        public DateTime? ArrivalDateTime { get; set; }

        public DateTime? ExaminationDateTime { get; set; }

        public string? CourtOrder { get; set; }

        public string? PoliceConstableName { get; set; }

        public string? PoliceConstablePhoneNumber { get; set; }

        public DateTime? AdmitDateTime { get; set; }

        public DateTime? DischargeDateTime { get; set; }

        public string? Comments { get; set; }
        public string? HealthFacilityName { get; set; }
        public string? FullName { get; set; }

        public DateTime? SentDateTime { get; set; }

        public long? ReportCounts { get; set; }

        public bool? IsFinalReport { get; set; }
        public CreateOrEditMLCDto? createOrEditMLCDtos { get; set; }

        public CreateOrEditPatientImageDto? PatientImage {get;set;}
        public CreateOrEditPatientImageDto? PatientSignature {get;set;}
        public CreateOrEditPatientImageDto? PoliceSignature { get;set; }
        public CreateOrEditPatientImageDto? AdultOrUnderAgeFingerPrint { get; set; }
        //public CreateOrEditPatientImageDto? UnderAgeFingerPrint { get; set; }
        public CreateOrEditPatientImageDto? PoliceFingerPrint { get; set; }
        public Guid? PatientImageId { get; set; }
        public Guid? PoliceSignatureImageId { get; set; }
        public Guid? PatientSignatureImageId { get; set; }
        public Guid? AdultOrUnderAgeFingerPrintProfileId { get; set; }
        public Guid? PoliceFingerPrintId { get; set; }

        public string? FormType { get; set; }

        public int? DocDepartmentLookupId { get; set; }

        public int? DocSectionLookupId { get; set; }
        public bool? IsReportCount { get; set; }
    }
}
