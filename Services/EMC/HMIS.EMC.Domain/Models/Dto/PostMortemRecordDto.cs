using HMIS.EMC.Domain.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.EMC.Domain.Models.Dto
{
    public class PostMortemRecordDto
    {
        // Mlc
        public Guid Mlcid { get; set; }

        public string? MobileNo { get; set; }

        public string? FullName { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid? MlctypeProfileId { get; set; }

        public Guid PatientId { get; set; }
        public long? ReportCounts { get; set; }

        public Guid PatientVisitId { get; set; }

        public Guid? PatientDiagnoseId { get; set; }

        public int? HealthFacilityId { get; set; }

        public Guid? DoctorId { get; set; }

        public string? Mlcno { get; set; }
        public string? HealthFacilityName { get; set; }

        public string? BookNo { get; set; }

        // Police Info
        public Guid MlcpoliceInfoId { get; set; }
        public string? PolicePersonNameDesignation { get; set; }

        public string? PoliceStationName { get; set; }

        public string? PolicePeron2NameDesignation { get; set; }
        public string? PoliceInformation { get; set; }
        public string? CommentsByPolice { get; set; }

        // Mlc Postmortem

        public Guid MlcpostmortemId { get; set; }

        public string? Pmrno { get; set; }

        public string? IncidentPlace { get; set; }

        public string? Radiological { get; set; }

        public string? UltraSound { get; set; }

        public DateTime? DeathDateTime { get; set; }

        public DateTime? RecieveDateTime { get; set; }

        public string? FatherHusbandName { get; set; }
        public DateTime? DoctorVisitDateTime { get; set; }

        public DateTime? AutopsyDateTime { get; set; }

        // Body Identifier
        public Guid MlcbodyIdentifierInfoId { get; set; }

        public string? IdentifierName { get; set; }

        public string? IdentifierCnic { get; set; }

        public Guid? IdentifierRelationTypeProfileId { get; set; }

        public string? IdentifierComments { get; set; }
        // ExternalExamination
        public Guid PostmortemExternalExaminationId { get; set; }

        public string? PhysicalRemarks { get; set; }

        public string? ClothRemarks { get; set; }

        public string? NeckRemarks { get; set; }

        public string? InjuriesRemarks { get; set; }
        // Internal Examination
        public Guid PostMortemInternalExaminationId { get; set; }

        public string? Scalp { get; set; }

        public string? Skull { get; set; }

        public string? Membranes { get; set; }

        public string? Brain { get; set; }

        public string? Vertebrae { get; set; }

        public string? SpinalCord { get; set; }

        public string? WallsStemum { get; set; }

        public string? Pleurae { get; set; }

        public string? LaryNx { get; set; }

        public string? RightLung { get; set; }

        public string? LeftLung { get; set; }

        public string? Pencardium { get; set; }

        public string? BloodVes { get; set; }

        public string? Walls { get; set; }

        public string? Peritoneum { get; set; }

        public string? Mouth { get; set; }

        public string? Diaphragm { get; set; }

        public string? StomachContents { get; set; }

        public string? Pancrease { get; set; }

        public string? SintestineContents { get; set; }

        public string? LintestineContents { get; set; }

        public string? Liver { get; set; }

        public string? Spleen { get; set; }

        public string? Rkidneys { get; set; }

        public string? Lkidneys { get; set; }

        public string? Unnary { get; set; }

        public string? Organs { get; set; }

        public string? Ulinjuries { get; set; }

        public string? Uldisease { get; set; }

        public string? Ulfracutre { get; set; }

        public string? Uldislocation { get; set; }

        public string? Llinjuries { get; set; }

        public string? Lldisease { get; set; }

        public string? Llfracutre { get; set; }

        public string? Lldislocation { get; set; }

        // Report

        public Guid PostmortemReportId { get; set; }

        public string? ArticalToPolice { get; set; }

        public string? DoctorOpinion { get; set; }

        public string? ElapsedPortableTime { get; set; }

        public string? TimeBetweenInjuryAndDeath { get; set; }

        public string? TimeBetweenDeathAndPostmortem { get; set; }

        public string? LabExpertReport { get; set; }

        public string? FinalOpinion { get; set; }

        public string? PoliceOfficerName { get; set; }

        public string? PoliceOfficerMobileNo { get; set; }

        //public string? PoliceOfficerDistrict { get; set; }
        public string? PoliceDistrict { get; set; }

        public DateTime? ReportHandoverDatetime { get; set; }

        public Guid? PoliceSignatureImageId { get; set; }

        public Guid? PoliceFingerPrintId { get; set; }

        public Guid? PatientManualReportId { get; set; }

        public Guid? PatientDrawImgId { get; set; }

        public bool? ChemicalExam { get; set; }

        public bool? Dnalab { get; set; }

        public bool? Histopathologist { get; set; }

        public bool? BallisticExpert { get; set; }

        public string? PoliceSignatureUrl { get; set; }
        public string? PoliceFingerPrintUrl { get; set; }
        public string? PatientManualReportUrl { get; set; }
        public string? PatientDrawImageUrl { get; set; }

        public bool? IsFinalReport { get; set; }

    }
}
