using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRReports.Models.Dto
{
       #region Prescription
    public class PatientDetaildto
    {
        public Guid PatientId { get; set; }

        public string MRNo { get; set; }

        public string FullName { get; set; }

        public string CNIC { get; set; }
        public int HealthFacilityId { get; set; }

        public Guid CreatedById { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }
        public string MobileNo { get; set; }
        public Guid PatientOpenVisitId { get; set; }
        public string DepartmentName { get; set; }
        public string SectionName { get; set; }
        public string Designation { get; set; }

    }

    public class DentalProcedurePatientDetaildto
    {
        public Guid PatientId { get; set; }

        public string MRNo { get; set; }

        public string FullName { get; set; }

        public string CNIC { get; set; }
        public int HealthFacilityId { get; set; }

        public Guid CreatedById { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }
        public string MobileNo { get; set; }
        public Guid PatientOpenVisitId { get; set; }
        public string DepartmentName { get; set; }
        public string SectionName { get; set; }
        public string ProcedureTitle { get; set; }
    }

    public class PatientLabDetaildto
    {
        public Guid PatientId { get; set; }

        public string MRNo { get; set; }

        public string FullName { get; set; }

        public string CNIC { get; set; }
        public int HealthFacilityId { get; set; }

        public Guid CreatedById { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }
        public string MobileNo { get; set; }
        public Guid PatientOpenVisitId { get; set; }
        public int LabTestId { get; set; }
        public string DepartmentName { get; set; }
        public string SectionName { get; set; }

    }

    public class PatientLabReportDetaildto
    {
        public string FullName { get; set; }
        public string CNIC { get; set; }
        public string MobileNo { get; set; }
        public string MRNo { get; set; }
        public int LabTestId { get; set; }
        public string TestName { get; set; }
        public string Name { get; set; }
        public int HealthFacilityId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string DepartmentName { get; set; }
        public string SectionName { get; set; }
    }

    public class PatientDetailDrugAddictDiseasesDTO
    {
        public Guid PatientId { get; set; }

        public string MRNo { get; set; }

        public string FullName { get; set; }

        public string CNIC { get; set; } 
        public int HealthFacilityId { get; set; }

        public Guid CreatedById { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }
        public string MobileNo { get; set; }
        public Guid PatientOpenVisitId { get; set; }
        public string Diseases { get; set; }

    }

    public class PatientDetailDrugAddictSocialWelfareDTO
    {
        public Guid PatientId { get; set; }

        public string MRNo { get; set; }

        public string FullName { get; set; }

        public string CNIC { get; set; }
        public int HealthFacilityId { get; set; }

        public Guid CreatedById { get; set; }
        public string Name { get; set; }
        public string DivisionName { get; set; }
        public string DistrictName { get; set; }
        public string TehsilName { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string MobileNo { get; set; }
        public Guid PatientOpenVisitId { get; set; }

    }

    public class PatientDetailDrugAddictDTO
    {
        public Guid PatientId { get; set; }

        public string MRNo { get; set; }

        public string FullName { get; set; }

        public string CNIC { get; set; } 
        public int HealthFacilityId { get; set; }

        public Guid CreatedById { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }
        public string MobileNo { get; set; }
        public Guid PatientOpenVisitId { get; set; }

    }
    #endregion


    #region TB
    public class TbDashboardDTO
    {

        public string MRNo { get; set; }

        public string FullName { get; set; }

        public string CNIC { get; set; }
        public string DivisionName { get; set; }
        public string DistrictName { get; set; }
        public string TehsilName { get; set; }
        public string HealthFacilityName { get; set; }


        public DateTime CreatedOn { get; set; }

    }
    #endregion

    public class MlePatientsDto
    {
        public string Mrno { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Cnic { get; set; } 
        public int Age { get; set; }
        public string MobileNo { get; set; } 
        public Guid MlebasicInfoId { get; set; }

        public Guid PatientId { get; set; }

        public Guid PatientVisitId { get; set; }

        public Guid PatientDiagnoseId { get; set; }

        public Guid PatientStatusProfileId { get; set; }

        public int HealthFacilityId { get; set; }

        public string Mlcno { get; set; }

        public string BookNo { get; set; }

        //public string PatientImage { get; set; }

        public DateTime Dob { get; set; }

        public string Relation { get; set; }

        public string PoliceDistrict { get; set; }

        public DateTime MlcDate { get; set; }
        public Guid MLEReportId { get; set; }

        public string RefferedTo { get; set; }

        public string RefferedFrom { get; set; }

        public Guid CaseTypeProfileId { get; set; }

        public Guid DoctorId { get; set; }

        public string IncidentPlace { get; set; }

        public string MlcRemark1 { get; set; }

        public string MlcRemark2 { get; set; }

        public string AccompaniesBy { get; set; }

        public DateTime ArrivalDateTime { get; set; }

        public DateTime ExaminationDateTime { get; set; }

        public string CourtOrder { get; set; }

        public string PoliceConstableName { get; set; }

        public string PoliceConstablePhoneNumber { get; set; }

        public DateTime AdmitDateTime { get; set; }

        public DateTime DischargeDateTime { get; set; }

        public string Comments { get; set; }

        public DateTime SentDateTime { get; set; }

        public long ReportCounts { get; set; }

        public bool IsFinalReport { get; set; }

        public DateTime CreatedOn { get; set; }

        public Guid MleexaminationId { get; set; }

        public string History { get; set; }

        public string ClothExamination { get; set; }

        public string GeneralPhysicalExamination { get; set; }

        public string InjuriesDescription { get; set; }

        public string AdvisedInvestigate { get; set; }

        public string LaboratoryInvestigation { get; set; }

        public string OpinionSpecialistOrXrayReport { get; set; }

        public string NatureOfInjuries { get; set; }

        public string Fabrication { get; set; }

        public string DurationOfInjuries { get; set; }

        public string WeaponPoison { get; set; }

        public string KuoInjuries { get; set; }

        public string GuardianName { get; set; }
        public string GuardianCNIC { get; set; }
        public string Caste { get; set; }
        public string Gender { get; set; }
        public string Occupation { get; set; }
        public string PatientImageUrl { get; set; }
        public string PoliceSignatureImageUrl { get; set; }
        public string PatientSignatureImageUrl { get; set; }
        public string PatientUnderAgeOrAdultFingerPrint { get; set; }
        //public string PatientUnderAgeFingerPrint { get; set; }
        public string PoliceFingerPrint { get; set; }
        public string PatientManualReport { get; set; }
        public string PatientDrawImgUrl { get; set; }
        public Guid PatientImageId { get; set; }
        public Guid PoliceSignatureImageId { get; set; }
        public Guid PatientSignatureImageId { get; set; }
        public Guid AdultOrUnderAgeFingerPrintProfileId { get; set; }
        //public Guid PatientUnderAgeFingerPrintId { get; set; }
        public Guid PoliceFingerPrintId { get; set; }
        public Guid PatientManualReportId { get; set; }
        public Guid PatientDrawImgId { get; set; }
    }


    public class MleSvPatientDto
    {
        public string PatientName { get; set; }
        public string CNIC { get; set; }
        public string Age { get; set; }
        public string MobileNo { get; set; }
        public string ParmanentAddress { get; set; }
        public bool IsFinalReport { get; set; }
        public string PoliceDocketOne { get; set; }
        public string PoliceDocketTwo { get; set; }
        public string PoliceDocketThree { get; set; }
        public string DesignationName { get; set; }
        public string Caste { get; set; }
        public string MLCId { get; set; }
        public string MLCTypeProfileId { get; set; }
        public string DaughterWifeOf { get; set; }
        public Guid PatientId { get; set; }
        public Guid PatientVisitId { get; set; }
        public Guid PatientDiagnoseId { get; set; }
        public Guid PatientStatusProfileId { get; set; }
        public int HealthFacilityId { get; set; }
        public Guid DoctorId { get; set; }
        public string MLCNo { get; set; }
        public string BookNo { get; set; }
        public string PoliceDistrict { get; set; }
        public string ConsentFile { get; set; }
        public Guid ImageTypeProfileId { get; set; }
        public string EmergencyNo { get; set; }
        public string CounsellingRefferal { get; set; }
        public Guid PatientImageId { get; set; }
        public Guid PatientFingerPrintId { get; set; }
        public Guid PatientSignatureId { get; set; }
        public Guid GuardianSignatureId { get; set; }
        public Guid PoliceSignatureId { get; set; }
        public Guid CaseTypeProfileId { get; set; }
        public string IncidentPlace { get; set; }
        public string IsBroughtDead { get; set; }
        public string RefferToHealthFacilityId { get; set; }
        public string ArrivalDateTime { get; set; }
        public string ExaminationDateTime { get; set; }
        public string AccompaniedBy { get; set; }
        public string CourtOrder { get; set; }
        public string NameOfOfficialAccompany { get; set; }
        public string MLCSVRemark1 { get; set; }
        public string MLCSVRemark2 { get; set; }
        public string AdmissionDateTime { get; set; }
        public string DischargeDateTime { get; set; }
        public string MLCSVExaminationId { get; set; }
        public string MLCSVInitialInfoId { get; set; }
        public string History { get; set; }
        public string VictimDetail { get; set; }
        public string IncidenceDateTim { get; set; }
        public string Location { get; set; }
        public string RelationToVictim { get; set; }
        public string AssaultDetail { get; set; }
        public string PreviousIncidence { get; set; }
        public string PreviousIncidenceDateTime { get; set; }
        public string DetailFromOther { get; set; }
        public string RelevantMedicalSurgicalPsychiatricHistory { get; set; }
        public string RelevantGynecologicalHistory { get; set; }
        public string CurrentSymptoms { get; set; }
        public string ClothExamination { get; set; }
        public string TypeOfCloths { get; set; }
        public string CutsTearsHoles { get; set; }
        public string BloodStaining { get; set; }
        public string NonBiologicalMaterialStaining { get; set; }
        public string GeneralPhysicalExamination { get; set; }
        public string Physique { get; set; }
        public string Confident { get; set; }
        public string Confused { get; set; }
        public string HeightWeight1 { get; set; }
        public string CharacteristicsOfInjuries { get; set; }
        public string Tears { get; set; }
        public string Rupture { get; set; }
        public string EvidenceBleed { get; set; }
        public string EvidenceSeminal { get; set; }
        public string Reference { get; set; }
        public string MLCSVEvidenceCollectedId { get; set; }
        public string ClothsDescription { get; set; }
        public string ClothsHandOverTo { get; set; }
        public string BloodDescription { get; set; }
        public string BloodHandOverTo { get; set; }
        public string VaginalDescription { get; set; }
        public string VaginalHandOverTo { get; set; }
        public string OralDescription { get; set; }
        public string OralHandOverTo { get; set; }
        public string InvestigationAdvice { get; set; }
        public string XRayReason { get; set; }
        public string XRayReport { get; set; }
        public string UltraSoundReason { get; set; }
        public string UltraSoundReport { get; set; }
        public string BloodReason { get; set; }
        public string BloodReport { get; set; }
        public string CounselingRefferalProfileId { get; set; }
        public string MLCSVReportId { get; set; }
        public string NatureOfInjuries { get; set; }
        public string DurationOfInjuries { get; set; }
        public string KindOfWeaponUse { get; set; }
        public string Treatment { get; set; }
        public string Notes { get; set; }
        public string KUOInjuryNote { get; set; }
        public string FinalOpinion { get; set; }
        public string DoctorName { get; set; }
        public string PatientFingerPrintImageUrl { get; set; }
        public string ManualReportImageUrl { get; set; }
        public string FinalReportImageUrl { get; set; }
        public string HealthFacilityName { get; set; }
        public string CaseAgainst { get; set; }
        public string GuardianSignatureImageUrl { get; set; }
        public string PatientSignatureImageUrl { get; set; }
        public string QrCodeImagePath { get; set; }
        public string PatientImageUrl { get; set; }
        public string rn { get; set; }
        public string GuardianName { get; set; }

    }

    #region PostMortem Dto
    public class PostMortemDto
    {
        public List<PostMortemInitialExaminationAndReportDto> PostMortemReponseZero { get; set; }
        public List<PostMortemIdentificationDto> PostMortemReponseOne { get; set; }
    }

    public class PostMortemInitialExaminationAndReportDto
    {
        public Guid MLCId { get; set; }
        public string HealthFacilityName { get; set; }
        public string DoctorName { get; set; }
        public bool IsFinalReport { get; set; }
        public string MLCNo { get; set; }
        public string PatientName { get; set; }
        public string ParmanentAddress { get; set; }
        public string DesignationName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Caste { get; set; }
        public string SerialNo { get; set; }
        public string Relation { get; set; }
        public string BookNo { get; set; }
        public string PMRNo { get; set; }
        public string IncidentPlace { get; set; }
        public string Radiological { get; set; }
        public string UltraSound { get; set; }
        public DateTime? DeathDateTime { get; set; }
        public DateTime? RecieveDateTime { get; set; }
        public DateTime? DoctorVisitDateTime { get; set; }
        public DateTime? AutopsyDateTime { get; set; }
        public string FatherHusbandName { get; set; }
        public string PoliceInformation { get; set; }
        public string PolicePersonNameDesignation { get; set; }
        public string PoliceStationName { get; set; }
        public string PolicePeron2NameDesignation { get; set; }
        public string CommentsByPolice { get; set; }
        public string PhysicalRemarks { get; set; }
        public string ClothRemarks { get; set; }
        public string NeckRemarks { get; set; }
        public string InjuriesRemarks { get; set; }
        public string Scalp { get; set; }
        public string Skull { get; set; }
        public string Membranes { get; set; }
        public string Brain { get; set; }
        public string Vertebrae { get; set; }
        public string SpinalCord { get; set; }
        public string WallsStemum { get; set; }
        public string Pleurae { get; set; }
        public string LaryNx { get; set; }
        public string RightLung { get; set; }
        public string LeftLung { get; set; }
        public string Pencardium { get; set; }
        public string BloodVes { get; set; }
        public string Walls { get; set; }
        public string Peritoneum { get; set; }
        public string Mouth { get; set; }
        public string Diaphragm { get; set; }
        public string StomachContents { get; set; }
        public string Pancrease { get; set; }
        public string SIntestineContents { get; set; }
        public string LIntestineContents { get; set; }
        public string Liver { get; set; }
        public string Spleen { get; set; }
        public string RKidneys { get; set; }
        public string LKidneys { get; set; }

        public string Unnary { get; set; }
        public string Organs { get; set; }
        public string ULInjuries { get; set; }
        public string ULDisease { get; set; }
        public string ULFracutre { get; set; }
        public string ULDislocation { get; set; }
        public string LLInjuries { get; set; }
        public string LLDisease { get; set; }
        public string LLFracutre { get; set; }
        public string LLDislocation { get; set; }
        public string ArticalToPolice { get; set; }
        public string DoctorOpinion { get; set; }
        public string ElapsedPortableTime { get; set; }
        public string TimeBetweenInjuryAndDeath { get; set; }
        public string TimeBetweenDeathAndPostmortem { get; set; }
        public string LabExpertReport { get; set; }
        public string FinalOpinion { get; set; }
        public string PoliceOfficerName { get; set; }
        public string PoliceOfficerMobileNo { get; set; }
        public string PoliceDistrict { get; set; }
        public DateTime ReportHandoverDatetime { get; set; }
        public bool ChemicalExam { get; set; }
        public bool DNALab { get; set; }
        public bool Histopathologist { get; set; }
        public bool BallisticExpert { get; set; }

        public string PoliceFingerPrintUrl { get; set; }
        public string PatientManualReportUrl { get; set; }
        public string PatientDrawImageUrl { get; set; }
        public string MobileNo { get; set; }
        public string PoliceSignatureImageUrl { get; set; }
        public string QrCodeImagePath { get; set; }
        public string GuardianName { get; set; }
        public DateTime? CreatedOn { get; set; }

    }

    public class PostMortemIdentificationDto
    {
        public string IdentifierName { get; set; }
        public string IdentifierCNIC { get; set; }
        public string IdentifierRelation { get; set; }
        public string IdentifierComments { get; set; }

    }
    #endregion

}