using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.EMC.Domain.Models.Dto
{
    public class GetAllMlcSvSingleRecordDto
    {
        public Guid? Mlcid { get; set; }

        public Guid? MlctypeProfileId { get; set; }

        public Guid PatientId { get; set; }

        public Guid PatientVisitId { get; set; }

        public Guid? PatientDiagnoseId { get; set; }

        public Guid? PatientStatusProfileId { get; set; }

        public int? HealthFacilityId { get; set; }

        public Guid? DoctorId { get; set; }

        public string? Mlcno { get; set; }

        public string? BookNo { get; set; }

        public string? PoliceDistrict { get; set; }
        public string? HealthFacilityName { get; set; }
        public string? DaughterWifeOf { get; set; }

        public Guid? MlcsvinitialInfoId { get; set; }

        public string? EmergencyNo { get; set; }

        public Guid? PatientImageId { get; set; }

        public Guid? PatientFingerPrintId { get; set; }

        public Guid? PatientSignatureId { get; set; }

        public Guid? GuardianSignatureId { get; set; }

        public string? PoliceDocketOne { get; set; }

        public string? PoliceDocketTwo { get; set; }

        public string? PoliceDocketThree { get; set; }
        public Guid? PoliceSignatureId { get; set; }

        public Guid? CaseTypeProfileId { get; set; }

        public string? IncidentPlace { get; set; }

        public bool? IsFinalReport { get; set; }
        public bool? IsBroughtDead { get; set; }

        public long? ReportCounts { get; set; }
        public int? RefferToHealthFacilityId { get; set; }

        public DateTime? ArrivalDateTime { get; set; }

        public DateTime? ExaminationDateTime { get; set; }

        public string? AccompaniedBy { get; set; }

        public string? CourtOrder { get; set; }

        public string? NameOfOfficialAccompany { get; set; }

        public string? Mlcsvremark1 { get; set; }

        public string? Mlcsvremark2 { get; set; }

        public DateTime? AdmissionDateTime { get; set; }

        public DateTime? DischargeDateTime { get; set; }

        public Guid? MlcsvexaminationId { get; set; }

        public string? History { get; set; }

        public string? VictimDetail { get; set; }

        public DateTime? IncidenceDateTim { get; set; }

        public string? Location { get; set; }

        public string? RelationToVictim { get; set; }

        public string? AssaultDetail { get; set; }

        public bool? PreviousIncidence { get; set; }

        public DateTime? PreviousIncidenceDateTime { get; set; }

        public string? DetailFromOther { get; set; }

        public string? RelevantMedicalSurgicalPsychiatricHistory { get; set; }

        public string? RelevantGynecologicalHistory { get; set; }

        public string? CurrentSymptoms { get; set; }

        public string? ClothExamination { get; set; }

        public string? TypeOfCloths { get; set; }

        public string? CutsTearsHoles { get; set; }

        public string? BloodStaining { get; set; }

        public string? NonBiologicalMaterialStaining { get; set; }

        public string? GeneralPhysicalExamination { get; set; }

        public string? Physique { get; set; }

        public string? Confident { get; set; }

        public string? Confused { get; set; }

        public string? HeightWeight1 { get; set; }

        public string? CharacteristicsOfInjuries { get; set; }

        public string? Tears { get; set; }

        public string? Rupture { get; set; }

        public string? EvidenceBleed { get; set; }

        public string? EvidenceSeminal { get; set; }

        public string? Reference { get; set; }

        public Guid? MlcsvevidenceCollectedId { get; set; }

        public string? ClothsDescription { get; set; }

        public string? ClothsHandOverTo { get; set; }

        public string? BloodDescription { get; set; }

        public string? BloodHandOverTo { get; set; }

        public string? VaginalDescription { get; set; }

        public string? VaginalHandOverTo { get; set; }

        public string? OralDescription { get; set; }

        public string? OralHandOverTo { get; set; }

        public string? InvestigationAdvice { get; set; }

        public string? XrayReason { get; set; }

        public string? XrayReport { get; set; }

        public string? UltraSoundReason { get; set; }

        public string? UltraSoundReport { get; set; }

        public string? BloodReason { get; set; }

        public string? BloodReport { get; set; }

        public Guid? CounselingRefferalProfileId { get; set; }

        public Guid? MlcsvreportId { get; set; }

        public string? NatureOfInjuries { get; set; }

        public string? DurationOfInjuries { get; set; }

        public string? KindOfWeaponUse { get; set; }

        public string? Treatment { get; set; }

        public string? Notes { get; set; }

        public string? KuoinjuryNote { get; set; }

        public string? FinalOpinion { get; set; }

        public Guid? ManualReportImageId { get; set; }

        public Guid? FinalReportImageId { get; set; }
        public string? PatientImageUrl { get; set; }
        public string? PatientFingerPrintImageUrl { get; set; }
        public string? PatientSignatureImageUrl { get; set; }
        public string? GuardianSignatureImageUrl { get; set; }
        public string? PoliceSignatureImageUrl { get; set; }
        public string? ManualReportImageUrl { get; set; }
        public string? FinalReportImageUrl { get; set; }
        public string? PatientName { get; set; }
        public int? Age { get; set; }
        public string? CNIC { get; set; }
        public string? MRNo { get; set; }
        public string? MobileNo { get; set; }
    }
}
