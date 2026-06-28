using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.DashboardDto.AdminReferedDashboard
{
    #region Secretary Dashboard Card Counts

    public class SecretaryDashboardCardCount
    {
        public int TokenIssuesCount { get; set; }
        public int RegistrationCount { get; set; }
        public int VitalCount { get; set; }
        public int FilterClinicCount { get; set; }
        public int ConsultantCount { get; set; }
        public int LabCount { get; set; }
        public int PharmacyCount { get; set; }
        public int PhysioCount { get; set; }
        public int DentalCount { get; set; }

    }
    #endregion

    #region Vital Dashboard Card Counts

    public class VitalDashboardCardCount
    {
        public int TokenIssuesCount { get; set; }
        public int RegistrationCount { get; set; }
        //public int RevisitCount { get; set; }
        public int VitalReferCount { get; set; }
        public int VitalCollectedCount { get; set; }

        //public int InQueue { get; set; }

    }
    #endregion
    #region Doctor Dashboard Card Counts
    public class DctDashboardCardCount
    {
        public int TokenIssuesCount { get; set; }
        public int RegistrationCount { get; set; }
        public int RevisitCount { get; set; }
        public int VitalReferCount { get; set; }
        public int VitalCollectedCount { get; set; }
        public int? InQueueCount { get; set; }

    }
    #endregion

    #region Patient Detail HR Dashboard
    public class PatientDetailHRDashboarddto
    {
        public Guid PatientId { get; set; }

        public string? MRNo { get; set; }

        public string? FullName { get; set; }

        public string CNIC { get; set; } = null!;
        public int? HealthFacilityId { get; set; }
        public string? HealthFacilityName { get; set; }

        public Guid? CreatedById { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string? CreatedBy { get; set; }
        public string? MobileNo { get; set; }
        public Guid PatientOpenVisitId { get; set; }
        public string? DepartmentName { get; set; }
        public string? SectionName { get; set; }

    }
    #endregion
    #region Prescription
    public class PatientDetaildto
    {
        public Guid PatientId { get; set; }

        public string? MRNo { get; set; }

        public string? FullName { get; set; }

        public string CNIC { get; set; } = null!;
        public int? HealthFacilityId { get; set; }

        public Guid? CreatedById { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string? CreatedBy { get; set; }
        public string? MobileNo { get; set; }
        public Guid PatientOpenVisitId { get; set; }
        public string? DepartmentName { get; set; }
        public string? SectionName { get; set; }

    }

    #region 
    public class PatientDetailDoctordto
    {
        public Guid PatientId { get; set; }

        public string? MRNo { get; set; }

        public string? FullName { get; set; }

        public string CNIC { get; set; } = null!;
        public int? HealthFacilityId { get; set; }

        public Guid? CreatedById { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string? CreatedBy { get; set; }
        public string? MobileNo { get; set; }
        public Guid PatientOpenVisitId { get; set; }
        public string? DepartmentName { get; set; }
        public string? SectionName { get; set; }
        public string? Designation { get; set; }

    }
    #endregion
    public class HCPpreviousDashboardMainDto
    {
        public HCPTotalSamplesDto HCPTotalSamples { get; set; }
        public HCPScreeningDto HCPScreening { get; set; }
        public HCPVaccinationDto HCPVaccination { get; set; }
        public HCPTreatmentDto HCPTreatment { get; set; }
        public HCPSvrEligibleDto HCPSvrEligible { get; set; }
    }
    public class HCPTotalSamplesDto {
        public int SampleCollected { get; set; }
        public int SampleRejected { get; set; }
        public int InProcessSamples { get; set; }
        public int HCVDetected { get; set; }
        public int HCVNotDetected { get; set; }
        public int HCVReSample { get; set; }
        public int HBVDetected { get; set; }
        public int HBVNotDetected { get; set; }
        public int HBVReSample { get; set; }
        public int SVRSampleCollected { get; set; }
        public int SVRSampleProcessed { get; set; }
        public int RelapsedPatient { get; set; }
        public int CuredPatient { get; set; }
    }

    public class HCPScreeningDto{
        public int PreDiagnosed { get; set; }
        public int NewPatients { get; set; }
        public int HCVScreenedNegative { get; set; }
        public int HBVScreenedNegative { get; set; }
        public int HCVScreenedPositive { get; set; }
        public int HBVScreenedPositive { get; set; }
    }

    public class HCPVaccinationDto{
        public int VaccinationDose1Administered { get; set; }
        public int VaccinationDose2Administered { get; set; }
        public int VaccinationDose3Administered { get; set; }
    }

    public class HCPTreatmentDto{
        public int SdHcvEnrolledInTreatment { get; set; }
        public int SvHcvEnrolledInTreatment { get; set; }
        public int SdrHcvEnrolledInTreatment { get; set; }
        public int TenofoHbvEnrolledInTreatment { get; set; }
        public int EntecaHbvEnrolledInTreatment { get; set; }
    }

    public class HCPSvrEligibleDto{
        public int EligibleForSVR { get; set; }
    }

    public class HCPpreviousDashboardIndicatorsCountsDto
    {
        public int? TotalRegistered { get; set; }
        public int? Served { get; set; }
        public int? NotServed { get; set; }
        public int? PreDiagnosed { get; set; }
        public int? NewPatients { get; set; }
        public int? HBVScreenedPositive { get; set; }
        public int? HCVScreenedPositive { get; set; }
        public int? HBVScreenedNegative { get; set; }
        public int? HCVScreenedNegative { get; set; }
        public int? VaccinationDose3Administered { get; set; }
        public int? VaccinationDose2Administered { get; set; }
        public int? VaccinationDose1Administered { get; set; }
        public int? TotalVaccinationPerformed { get; set; }
        public int? SampleCollected { get; set; }
        public int? SampleRejected { get; set; }
        public int? InProcessSamples { get; set; }
        public int? HCVReSample { get; set; }
        public int? HCVDetected { get; set; }
        public int? HCVNotDetected { get; set; }
        public int? HBVReSample { get; set; }
        public int? HBVDetected { get; set; }
        public int? HBVNotDetected { get; set; }
        public int? SdHcvEnrolledInTreatment { get; set; }
        public int? SvHcvEnrolledInTreatment { get; set; }
        public int? SdrHcvEnrolledInTreatment { get; set; }
        public int? TenofoHbvEnrolledInTreatment { get; set; }
        public int? EntecaHbvEnrolledInTreatment { get; set; }

        public int SVRSampleCollected { get; set; }
        public int SVRSampleProcessed { get; set; }
        public int CuredPatient { get; set; }
        public int RelapsedPatient { get; set; }
        public int EligibleForSVR { get; set; }

    }

    public class DentalProcedureDetaildto
    {
        public Guid PatientId { get; set; }

        public string? MRNo { get; set; }

        public string? FullName { get; set; }

        public string CNIC { get; set; } = null!;
        public int? HealthFacilityId { get; set; }

        public Guid? CreatedById { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string? CreatedBy { get; set; }
        public string? MobileNo { get; set; }
        public Guid PatientOpenVisitId { get; set; }
        public string? DepartmentName { get; set; }
        public string? SectionName { get; set; }
        public string? ProcedureTitle { get; set; }
    }
    public class PatientLabDetaildto
    {
        public Guid PatientId { get; set; }

        public string? MRNo { get; set; }

        public string? FullName { get; set; }

        public string CNIC { get; set; } = null!;
        public int? HealthFacilityId { get; set; }

        public Guid? CreatedById { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string? CreatedBy { get; set; }
        public string? MobileNo { get; set; }
        public Guid PatientOpenVisitId { get; set; }
        public int? LabTestId { get; set; }
        public string? TestName { get; set; }
        public Guid? PatientLabTestId { get; set; }
        public string? DepartmentName { get; set; }
        public string? SectionName { get; set; }

    }
    #endregion
    #region Seht sahulat
    public class SehtSahulatCardDashboardCountDTO
    {
        public int? TotalClaims { get; set; }
        public int? UnAttended { get; set; }
        public int? Pending { get; set; }
        public int? Eligible { get; set; }
        public int? NotEligible { get; set; }
        public int? ClaimSubmitted { get; set; }
        public int? ClaimRejected { get; set; }
        public int? ClaimApproved { get; set; }
        public int? ClaimReSubmitted { get; set; }
    }
    #endregion

    #region HCP Dashboard
    public class HCPOPDDashboardAllCountsDTO
    {
        public int? TotalRegistered { get; set; }
        public int? VitalCollected { get; set; }
        public int? Served { get; set; }
        public int? NotServed { get; set; }
        public int? Prescribed { get; set; }
        public int? NotPerscribed { get; set; }
        public int? InternalPatientCount { get; set; }
        public int? ExternalPatientCount { get; set; }
        public int? IntExtPatientCount { get; set; }
        public int? TotalLabVisitCount { get; set; }
        public int? InternalLabVisitCount { get; set; }
        public int? ExternalLabVisitCount { get; set; }
        public int? InternalExternalLabVisitCount { get; set; }
        public int? PCRforHCVRNA { get; set; }
        public int? PCRforHBVDNA { get; set; }
        public int? PCRforHCVRNAPositive { get; set; }
        public int? PCRforHBVDNAPositive { get; set; }
        public int? PCRforHBVDNANegative { get; set; }
        public int? PCRforHCVRNANegative { get; set; }

    }
    public class HCPOPDDashboardAllCountsUpdatedDTO
    {
        public int? TotalRegistered { get; set; }
        public int? TotalScreened { get; set; }
        public int? PreDiagnosedHBVScreenedPositive { get; set; }
        public int? PreDiagnosedHBVScreenedNegative { get; set; }
        public int? PreDiagnosedHCVScreenedPositive { get; set; }
        public int? PreDiagnosedHCVScreenedNegative { get; set; }
        public int? RapidKitHBVScreenedPositive { get; set; }
        public int? RapidKitHBVScreenedNegative { get; set; }
        public int? RapidKitHCVScreenedPositive { get; set; }
        public int? RapidKitHCVScreenedNegative { get; set; }
        public int? TotalAssessmentPerformed { get; set; }
        public int? TotalVaccinationPerformed { get; set; }
        public int? Vaccination1stDosePerformedNormal { get; set; }
        public int? Vaccination1stDosePerformedDialysis { get; set; }
        public int? Vaccination2ndDosePerformedNormal { get; set; }
        public int? Vaccination2ndDosePerformedDialysis { get; set; }
        public int? Vaccination3rdDosePerformedNormal { get; set; }
        public int? Vaccination3rdDosePerformedDialysis { get; set; }
        public int? Vaccination4thDosePerformedDialysis { get; set; }
        //public int? VitalCollected { get; set; }
        public int? Served { get; set; }
        public int? NotServed { get; set; }
        public int? Prescribed { get; set; }
        public int? NotPerscribed { get; set; }
        //public int? InternalPatientCount { get; set; }
        //public int? ExternalPatientCount { get; set; }
        //public int? IntExtPatientCount { get; set; }
        public int? TotalLabVisitCount { get; set; }
        public int? InternalLabVisitCount { get; set; }
        public int? ExternalLabVisitCount { get; set; }
        public int? InternalExternalLabVisitCount { get; set; }
        public int? PCRforHCVRNA { get; set; }
        public int? PCRforHBVDNA { get; set; }
        public int? PCRforHCVRNAPositive { get; set; }
        public int? PCRforHBVDNAPositive { get; set; }
        public int? PCRforHBVDNANegative { get; set; }
        public int? PCRforHCVRNANegative { get; set; }

    }
    #endregion

    #region Paraplegic Dashboard
    public class ParaplegicDashboardAllCountsDTO
    {
        public int? TotalRegistered { get; set; }
        public int? VitalCollected { get; set; }
        public int? Served { get; set; }
        public int? NotServed { get; set; }
        public int? TotalRegisteredSurgeryForm { get; set; }
        public int? VitalCollectedSurgeryForm { get; set; }
        public int? ServedSurgeryForm { get; set; }
        public int? NotServedSurgeryForm { get; set; }
        public int? TotalRegisteredPhysiotherapyFormOPD { get; set; }
        public int? VitalCollectedPhysiotherapyFormOPD { get; set; }
        public int? ServedPhysiotherapyFormOPD { get; set; }
        public int? NotServedPhysiotherapyFormOPD { get; set; }
        public int? TotalRegisteredPsychiatryForm { get; set; }
        public int? VitalCollectedPsychiatryForm { get; set; }
        public int? ServedPsychiatryForm { get; set; }
        public int? NotServedPsychiatryForm { get; set; }
        public int? TotalRegisteredNutritionForm { get; set; }
        public int? VitalCollectedNutritionForm { get; set; }
        public int? ServedNutritionForm { get; set; }
        public int? NotServedNutritionForm { get; set; }
        public int? TotalRegisteredSpeechTherapyForm { get; set; }
        public int? VitalCollectedSpeechTherapyForm { get; set; }
        public int? ServedSpeechTherapyForm { get; set; }
        public int? NotServedSpeechTherapyForm { get; set; }
        public int? TotalRegisteredPsychologyForm { get; set; }
        public int? VitalCollectedPsychologyForm { get; set; }
        public int? ServedPsychologyForm { get; set; }
        public int? NotServedPsychologyForm { get; set; }
        public int? TotalRegisteredOccupationalTherapyForm { get; set; }
        public int? VitalCollectedOccupationalTherapyForm { get; set; }
        public int? ServedOccupationalTherapyForm { get; set; }
        public int? NotServedOccupationalTherapyForm { get; set; }

    }
    #endregion

    #region Dental Dashboard
    public class DentalCountsAndChartDataDTO
    {
        public List<DentalDashboardAllCountsDTO> AllCounts { get; set; }
        public List<DentalChartDataDTO> DentalProcedures { get; set; }

    }
    public class DentalChartDataDTO
    {
        public string? name { get; set; }
        public int? value { get; set; }
        public string? extra { get; set; }
    }
    public class DentalDashboardAllCountsDTO
    {
        public int? TotalRegistered { get; set; }
        public int? Served { get; set; }
        public int? NotServed { get; set; }
        public int? Prescribed { get; set; }
        public int? NotPerscribed { get; set; }
        public int? MLC { get; set; }
        public int? TotalProceduresRecommended { get; set; }
        public int? ProcedurePerformed { get; set; }
        public int? ProcedureNotPerformed { get; set; }
    }

    #endregion
    #region ChartData DTO
    public class ChartDataDTO
    {
        public string? name { get; set; }
        public int? value { get; set; }
    }

    #endregion
    #region PhysioTherapy Dashboard
    public class PhysioTherapyDashboardAllCountsDTOObj
    {
        public List<PhysioTherapyDashboardAllCountsDTO> AllCounts { get; set; }
        public List<ChartPhysioDto> ExercisePlan { get; set; }
        public List<ChartPhysioDto> ModalityList { get; set; }
        public List<PhysioReportReportPatientWiseDto> ReportPatientWise { get; set; }

    }
    public class PhysioReportReportPatientWiseDto
    {
        public string? DivisionName { get; set; }
        public string? DistrictName { get; set; }
        public string? HealthFacilityName { get; set; }
        public string? AHPName { get; set; }
        public string? PatientName { get; set; }
        public string? MobileNo { get; set; }
        public string? MRNo { get; set; }
        public string? Section { get; set; }
        public string? Department { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? ClinicalDiagnosis { get; set; }
        public string? PhysiotherapyDiagnosis { get; set; }
        public string? Modalities { get; set; }
        public string? ExercisePlan { get; set; }
        public string? Comorbidity { get; set; }
        public string? Prognosis { get; set; }
        public string? PlanOfCare { get; set; }
    }
    public class ChartPhysioDto
    {
        public string? name { get; set; }
        public int? value { get; set; }
        public string? extra { get; set; }
    }
    public class PhysioTherapyDashboardAllCountsDTO
    {
        public int? TotalRegistered { get; set; }
        public int? Served { get; set; }
        public int? NotServed { get; set; }
        public int? Modalities { get; set; }
        public int? NoModalities { get; set; }
        public int? MinutesSesion10to20 { get; set; }
        public int? MinutesSesion20to30 { get; set; }
        public int? MinutesSesion30to45 { get; set; }
        public int? MinutesSesionGreaterThan45 { get; set; }
    }
    #endregion
    #region Speech Therapy Dashboard
    public class SpeechTherapyDashboardAllCountsDTO
    {
        public List<SpeechTherapyDashboardAllCounts> AllCounts { get; set; }
        public List<DetailSpeechPatientReport> SpeechPatientReport { get; set; }
    }
    public class SpeechTherapyDashboardAllCounts
    {
        public int? TotalRegistered { get; set; }
        public int? Served { get; set; }
        public int? NotServed { get; set; }
        public int? DevelopmentMileStone { get; set; }
        public int? NoDevelopmentMileStone { get; set; }
        public int? NeckHolding0to5 { get; set; }
        public int? NeckHolding6to10 { get; set; }
        public int? NeckHolding11to15 { get; set; }
        public int? NeckHoldingGreater15 { get; set; }
        public int? Sitting0to5 { get; set; }
        public int? Sitting6to10 { get; set; }
        public int? Sitting11to15 { get; set; }
        public int? SittingGreater15 { get; set; }
        public int? Standing0to5 { get; set; }
        public int? Standing6to10 { get; set; }
        public int? Standing11to15 { get; set; }
        public int? StandingGreater15 { get; set; }
        public int? Walking0to5 { get; set; }
        public int? Walking6to10 { get; set; }
        public int? Walking11to15 { get; set; }
        public int? WalkingGreater15 { get; set; }
        public int? SpeechMilestone { get; set; }
        public int? NoSpeechMilestone { get; set; }
        public int? Cooing0to5 { get; set; }
        public int? Cooing6to10 { get; set; }
        public int? Cooing11to15 { get; set; }
        public int? CooingGreater15 { get; set; }
        public int? Babbling0to5 { get; set; }
        public int? Babbling6to10 { get; set; }
        public int? Babbling11to15 { get; set; }
        public int? BabblingGreater15 { get; set; }
        public int? SingleWord0to5 { get; set; }
        public int? SingleWord6to10 { get; set; }
        public int? SingleWord11to15 { get; set; }
        public int? SingleWordGreater15 { get; set; }
        public int? SpeechLevelWord0to5 { get; set; }
        public int? SpeechLevelWord6to10 { get; set; }
        public int? SpeechLevelWord11to15 { get; set; }
        public int? SpeechLevelWordGreater15 { get; set; }
        public int? FamilyHistory { get; set; }
        public int? NoFamilyHistory { get; set; }
        public int? HearingLoss { get; set; }
        public int? NoHearingLoss { get; set; }

        public int? ArticulationSounderrors { get; set; }
        public int? Articulationintelligibility { get; set; }
        public int? ArticulationOthers { get; set; }

        public int? dysfluencyRepetitions { get; set; }
        public int? dysfluencyProlongation { get; set; }
        public int? dysfluencySilentpause { get; set; }
        public int? dysfluencyOthers { get; set; }

        public int? VoiceHoarse { get; set; }
        public int? VoiceAphonic { get; set; }
        public int? VoiceOthers { get; set; }

        public int? PitchToohigh { get; set; }
        public int? PitchToolow { get; set; }
        public int? PitchOthers { get; set; }

        public int? ResonanceNasal { get; set; }
        public int? ResonanceDenasal { get; set; }
        public int? ResonanceMixed { get; set; }

        public int? PlanOfCareSelfTalk { get; set; }
        public int? PlanOfCareRecasting { get; set; }
        public int? PlanOfCareParallel { get; set; }
        public int? PlanOfCareFocused { get; set; }
        public int? PlanOfCareOthers { get; set; }

        public int? ScheduleWeekly { get; set; }
        public int? ScheduleFortnightly { get; set; }
        public int? ScheduleMonthly { get; set; }
        public int? ScheduleOthers { get; set; }

    }
    public class DetailSpeechPatientReport
    {
        public string? DivisionName { get; set; }
        public string? DistrictName { get; set; }
        public string? HealthFacilityName { get; set; }
        public string? AHPName { get; set; }
        public string? PatientName { get; set; }
        public string? MobileNo { get; set; }
        public string? MRNo { get; set; }
        public string? Section { get; set; }
        public string? Department { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? DevelopmentMilestoneStatus { get; set; }
        public string? SpeechMilestoneStatus { get; set; }
        public string? SpeechDisorder { get; set; }
        public string? AssociatedDisorder { get; set; }
        public string? Modalities { get; set; }
    }
    #endregion

    #region DSR Dashboard

    public class DSRDashboardDataDTO
    {
        public List<DSRDashboardReportDTO> AllCounts { get; set; }
        public List<DSRDashboardTopFourMedicineDTO> TopFourMedicine { get; set; }

    }
    public class DSRDashboardReportDTO
    {
        public string? HealthFacilityName { get; set; }
        public string? DivisionName { get; set; }
        public string? DistrictName { get; set; }
        public string? TehsilName { get; set; }
        public int? TotalRegisterd { get; set; }
        public int? VitalCollected { get; set; }
        public int? Served { get; set; }
        public int? NotServed { get; set; }
        public int? Prescribed { get; set; }
        public int? NotPerscribed { get; set; }
        public int? InternalPatientCount { get; set; }
        public int? ExternalPatientCount { get; set; }
        public int? IntExtPatientCount { get; set; }
        public int? MedicineIssued { get; set; }
        public double Quantity { get; set; }
        public double MedicineCost { get; set; }
        public int? TotalMedicainePerson { get; set; }
        public int? MedicineNotIssued { get; set; }
        public int? TotalLabVisitCount { get; set; }
        public int? InternalLabVisitCount { get; set; }
        public int? ExternalLabVisitCount { get; set; }
        public int? InternalExternalLabVisitCount { get; set; }
        public int? TbServed { get; set; }
        public int? TbTotal { get; set; }
        public int? HcpTotal { get; set; }
        public int? HcpServed { get; set; }
        public int? DentalTotal { get; set; }
        public int? DentalServed { get; set; }
        public int? physioTotal { get; set; }
        public int? physioServed { get; set; }
        public int? SpeechTherapyTotal { get; set; }
        public int? SpeechTherapyServed { get; set; }
        public int? NutritionTotal { get; set; }
        public int? NutritionServed { get; set; }
        public int? PsychologyTotal { get; set; }
        public int? PsychologyServed { get; set; }


    }

    public class DSRDashboardTopFourMedicineDTO
    {
        public string? HealthFacilityName { get; set; }
        public string? DivisionName { get; set; }
        public string? DistrictName { get; set; }
        public string? TehsilName { get; set; }
        public int? HealthFacilityId { get; set; }
        public string MedicineName { get; set; }
        public double PricePerItem { get; set; }
        public double Quantity { get; set; }
        public double MedicineCost { get; set; }

    }

    #endregion
    #region Nutrition
    public class NutritionDashboardDTO
    {
        public List<NutritionDashboardCountsDTO> AllCounts { get; set; }
        public List<ChartNutritionDto> BMI { get; set; }
        public List<ChartNutritionDto> NutritionalRisk { get; set; }
        public List<ChartNutritionDto> ExaminationFindings { get; set; }
        public List<ChartNutritionDto> Comorbidity { get; set; }
        public List<ChartNutritionDto> Malnutrition { get; set; }
        public List<DetailPatientReport> PatientReport { get; set; }

    }
    public class ChartNutritionDto
    {
        public string? name { get; set; }
        public int? value { get; set; }
        public string? extra { get; set; }


    }
    public class DetailPatientReport{
        public string? DivisionName { get; set; }
        public string? DistrictName { get; set; }
        public string? HealthFacilityName { get; set; }
        public string? Nutritionist { get; set; }
        public string? PatientName { get; set; }
        public string? MobileNo { get; set; }
        public string? MRNo { get; set; }
        public string? Diagnosis { get; set; }
        public string? Section { get; set; }
        public string? Department { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? ExaminationFindings { get; set; }
        public string? Comorbidity { get; set; }
        public string? MalnutritionStatus { get; set; }
        public string? PatientBMI { get; set; }
    }
    public class NutritionDashboardCountsDTO
    {
        public int? TotalRegistered { get; set; }
        public int? Served { get; set; }
        public int? NotServed { get; set; }
        public int? ExaminationFindings { get; set; }
        public int? NoExaminationFindings { get; set; }
        public int? Comorbidity { get; set; }
        public int? NoComorbidity { get; set; }
    }
    #endregion

    #region Psychology
    public class PsychologyDashboardDTO
    {
        public List<PsychologyDashboardCountsDTO> AllCounts { get; set; }
        public List<ChartPsychologyDto> Appearance { get; set; }
        public List<ChartPsychologyDto> Orientation { get; set; }
        public List<ChartPsychologyDto> Speech { get; set; }
        public List<ChartPsychologyDto> ThroughProcess { get; set; }
        public List<ChartPsychologyDto> ThroughContent { get; set; }

        public List<ChartPsychologyDto> PerceptualProcess { get; set; }
        public List<ChartPsychologyDto> Insight { get; set; }
        public List<ChartPsychologyDto> Judgment { get; set; }
        public List<ChartPsychologyDto> Mood { get; set; }
        public List<ChartPsychologyDto> Affect { get; set; }

        public List<ChartPsychologyDto> Memory { get; set; }
        public List<ChartPsychologyDto> EstimatedIntellectualFunctioning { get; set; }
        public List<ChartPsychologyDto> CognitiveDeficits { get; set; }

    }
    public class ChartPsychologyDto
    {
        public string? name { get; set; }
        public int? value { get; set; }
        public string? extra { get; set; }


    }
    public class PsychologyDashboardCountsDTO
    {
        public int? TotalRegistered { get; set; }
        public int? Served { get; set; }
        public int? NotServed { get; set; }
        public int? PastHistory { get; set; }
        public int? NoPastHistory { get; set; }
        public int? PsychologicalTestApplied { get; set; }
        public int? NoPsychologicalTestApplied { get; set; }
    }
    #endregion

    #region New Listing Dashboards Pagination
    public class PatientDetailsWithPaginationdto
    {
        public int? TotalRecord { get; set; }
        public List<PatientDetailList> PatientList { get; set; }

    }
    public class PatientDetailList
    {
        public Guid PatientId { get; set; }
        public long? SrNo { get; set; }

        public string? MRNo { get; set; }

        public string? FullName { get; set; }

        public string CNIC { get; set; } = null!;
        public int? HealthFacilityId { get; set; }

        public Guid? CreatedById { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string? CreatedBy { get; set; }
        public string? MobileNo { get; set; }
        public Guid PatientOpenVisitId { get; set; }
        public string? DepartmentName { get; set; }
        public string? SectionName { get; set; }

    }
    #endregion
    #region Lab Dashboard Counts
    public class LabPatientDetailsWithPaginationdto
    {
        public int? TotalRecord { get; set; }
        public List<LabPatientDetail> PatientList { get; set; }

    }
    public class LabPatientDetail
    {
        public Guid PatientId { get; set; }
        public long? SrNo { get; set; }

        public string? MRNo { get; set; }

        public string? FullName { get; set; }

        public string CNIC { get; set; } = null!;
        public int? HealthFacilityId { get; set; }
        public string? HealthFacilityName { get; set; }

        public Guid? CreatedById { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string? CreatedBy { get; set; }
        public string? MobileNo { get; set; }
        public Guid PatientOpenVisitId { get; set; }
        public int? LabTestId { get; set; }
        public string? TestName { get; set; }
        public Guid? PatientLabTestId { get; set; }
        public string? DepartmentName { get; set; }
        public string? SectionName { get; set; }

    }
    #endregion

    #region New DrugAddict Dashboards Pagination
    public class PatientDetailDrugAddictsDTO
    {
        public int? TotalRecord { get; set; }
        public List<PatientDetailDrugAddictList> PatientList { get; set; }

    }
    public class PatientDetailDrugAddictList
    {
        public Guid PatientId { get; set; }
        public long? SrNo { get; set; }
        public string? MRNo { get; set; }
        public string? FullName { get; set; }
        public string CNIC { get; set; } = null!;
        public int? HealthFacilityId { get; set; }
        public Guid? CreatedById { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? MobileNo { get; set; }
        public Guid PatientOpenVisitId { get; set; }

    }
    #endregion

    #region New dental procedure Dashboards Pagination
    public class DentalProcedureDetailListingDTO
    {
        public int? TotalRecord { get; set; }
        public List<DentalProcedureDetailList> PatientList { get; set; }

    }
    public class DentalProcedureDetailList
    {
        public Guid PatientId { get; set; }
        public long? SrNo { get; set; }
        public string? MRNo { get; set; }
        public string? FullName { get; set; }
        public string CNIC { get; set; } = null!;
        public int? HealthFacilityId { get; set; }
        public Guid? CreatedById { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? MobileNo { get; set; }
        public Guid PatientOpenVisitId { get; set; }
        public string? DepartmentName { get; set; }
        public string? SectionName { get; set; }
        public string? ProcedureTitle { get; set; }
    }
    #endregion
}
