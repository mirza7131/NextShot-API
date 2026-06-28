using HMIS.Dashboard.Domain.Models.DbModels;
using HMIS.Dashboard.Domain.Models.DTO.PatientDiagnoseDto;
using HMIS.Dashboard.Domain.Models.DTO.PatientDiagnoseProcedureDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Dashboard.Domain.Models.DTO.PatientDto
{
    public class ViewPatientSlipDetailsDto
    {
        public ViewPatientSlipDetailsDto()
        {
            PatientDiagnosesDiseases = new List<PatientDiagnosesDiseasesDto>();
            DefinitivePatientDiagnosesDiseases = new List<PatientDiagnosesDiseasesDto>();
            PatientMedicine = new List<PatientPrescriptionDto>();
            PatientLabTests = new List<PatientLabTestDto>();
            PatientVitals = new List<PatientVitalDto>();
            PatientDiagnoseProcedures = new List<PatientDiagnoseProcedureDto>();
            MedicineDispatch = new List<MedicineDispatchDto>();
            PatientPrescriptionVisitLists = new List<ViewGetPatientPrescriptionList>();
            PatientLabTestVisitLists = new List<ViewGetPatientLabTestList>();

            //NutritionForm = new CreateOrEditPatientDiagnoseWithNutritionisttherapyFormDto();
        }
        public Guid PatientId { get; set; }
        public Guid PatientOpenVisitId { get; set; }
        public string? Mrno { get; set; }
        public string  PatientCategory { get; set; }
        public string  PatientCondition { get; set; }
        public string? Doctor { get; set; }
        public string? DoctorDesignation { get; set; }
        public string? Section { get; set; }
        public string? VitalsFloorNo { get; set; }
        public string? VitalsRoomNo { get; set; }
        public string? DoctorFloorNo { get; set; }
        public string? DoctorRoomNo { get; set; }
        public string? PharmacyFloorNo { get; set; }
        public string? PharmacyRoomNo { get; set; }
        public string? PathologyFloorNo { get; set; }
        public string? PathologyRoomNo { get; set; }
        public string? Department { get; set; }
        public int? DepartmentId { get; set; }
        public int? SectionId { get; set; }
        
        public string? PatientName { get; set; }
        public string? GurdianName { get; set; }
        public int? Age { get; set; }
        public string? Gender { get; set; }
        public string? CNIC { get; set; }
        public string? ContactNo { get; set; }
        public string? TokenNo { get; set; }
        public string? Address { get; set; }
        public DateTime? NextVisitDate { get; set; }
        public DateTime? FollowUpDate { get; set; }
        public DateTime? VisitDate { get; set; }
        public string? BloodGroupName { get; set; }
        public string? HealthFacilityName { get; set; }
        public int? HealthFacilityId { get; set; }
        public string? PresentComplaints { get; set; }
        public string? Examination { get; set; }
        public string? PatientMedicalHistory { get; set; }
        public string? AdviseGiven { get; set; }
        public Guid PatientDiagnoseId { get; set; }
        public DateTime? Dob { get; set; }
        public string? DiseasesName { get; set; }
        public int? BedNo { get; set; }

        public string? DefinitiveDiseasesName { get; set; }
        public string? ProceduresName { get; set; }
        public bool? IsWillingToBuyMedPrivately { get; set; }
        public DateTime? CreatedOn { get; set; }

        public bool? IsReferred { get; set; }
        public int? ReferredDepartmentLookupId { get; set; }
        public string? ReferredDepartmentName { get; set; }
        public int? ReferredSectionLookupId { get; set; }
        public string? ReferredSectionName { get; set; }
        public int? ReferredToDepartmentLookupId { get; set; }
        public string? ReferredToDepartmentName { get; set; }
        public int? ReferredToSectionLookupId { get; set; }
        public string? ReferredToSectionName { get; set; }
        public Guid? ReferredBy { get; set; }
        public string? ReferredByName { get; set; }

        public bool? IsAdmittedInIPD { get; set; }
        public bool? IsReferredIpd { get; set; }
        public int? IpdDepartmentLookupId { get; set; }
        public string? IpdDepartmentLookupName { get; set; }
        public int? IpdSectionLookupId { get; set; }
        public string? IpdSectionLookupName { get; set; }
        public Guid? IpdReferredBy { get; set; }
        public string? IpdReferredByName { get; set; }
        public int? IpdReferredByDepartmentLookupId { get; set; }
        public string? IpdReferredByDepartmentLookupName { get; set; }
        public int? IpdReferredBySectionLookupId { get; set; }
        public string? IpdReferredBySectionLookupName { get; set; }
        public bool? IsConfirmed { get; set; }
        public DateTime? TbStatusConfirmedDate { get; set; }
        public bool? IsFollowUp { get; set; }
        //public bool? IsOccupied { get; set; } = false;
        public int? FollowUpNo { get; set; }
        public int? noOfMonthsPassed { get; set; }
        public bool? FirstVisitTestResults { get; set; } = false;
        public bool? IsReferToDRTB { get; set; } = false;
        public bool? SSMTestResult { get; set; } = false;
        public bool? XpertTestResult { get; set; } = false;
        public bool? IsCXRTestAdvisedInLastVisit { get; set; } = false;
        public bool? HivScreeningResult { get; set; } = false;
        public bool? IssueMedicine { get; set; } = false;
        public bool? IssueTest { get; set; } = false;
        public bool? Cured { get; set; } = false;
        public bool? IsMedicinePrescribedInLastVisit { get; set; } = false;
        public bool? IsCaseNo { get; set; } = false;
        public bool? IsRegisterAgain { get; set; } = false;
        public bool? IsVitalSkip { get; set; } = false;
        public bool? IsEligibleForSsc { get; set; }
        public bool? IsMlc { get; set; }
        public bool? IsSendToCdc { get; set; }
        public string?SscNumber { get; set; }
        public bool? IsSscClaimed { get; set; }
        public Guid? ReasonIfSscNotClaimed { get; set; }
        public DateTime? SscClaimedDate { get; set; }
        public string? ReasonIfNotEligibleForSsc { get; set; }
        public Guid? CreatedBy { get; set; }
        public bool IsEdit { get; set; } = false;
        public virtual ICollection<PatientDiagnosesDiseasesDto> PatientDiagnosesDiseases { get; set; }
        public virtual ICollection<PatientDiagnosesDiseasesDto> DefinitivePatientDiagnosesDiseases { get; set; }
        public virtual ICollection<PatientVitalDto> PatientVitals { get; set; }
        public virtual ICollection<PatientPrescriptionDto> PatientMedicine { get; set; }
        public virtual ICollection<PatientLabTestDto> PatientLabTests { get; set; }
        public virtual ICollection<PatientDiagnoseProcedureDto> PatientDiagnoseProcedures { get; set; }
        public virtual ICollection<MedicineDispatchDto> MedicineDispatch { get; set; }
        public virtual ICollection<ViewGetPatientPrescriptionList> PatientPrescriptionVisitLists { get; set; }
        public virtual ICollection<ViewGetPatientLabTestList> PatientLabTestVisitLists { get; set; }

        //public virtual CreateOrEditPatientDiagnoseWithNutritionisttherapyFormDto NutritionForm { get; set; }

    }

    public class ViewPatientVitalSlipDetailsDto
    {

        public Guid PatientId { get; set; }
        public Guid PatientOpenVisitId { get; set; }
        public string? Mrno { get; set; }
        public string? Doctor { get; set; }
        public string? DoctorDesignation { get; set; }
        public string? Section { get; set; }
        public string? VitalsFloorNo { get; set; }
        public string? VitalsRoomNo { get; set; }
        public string? DoctorFloorNo { get; set; }
        public string? DoctorRoomNo { get; set; }
        public string? PharmacyFloorNo { get; set; }
        public string? PharmacyRoomNo { get; set; }
        public string? PathologyFloorNo { get; set; }
        public string? PathologyRoomNo { get; set; }
        public string? Department { get; set; }
        public int? DepartmentId { get; set; }
        public int? SectionId { get; set; }
        public string? PatientName { get; set; }
        public string? GurdianName { get; set; }
        public int? Age { get; set; }
        public string? Gender { get; set; }
        public string? CNIC { get; set; }
        public string? ContactNo { get; set; }
        public string? TokenNo { get; set; }
        public string? Address { get; set; }
        public DateTime? NextVisitDate { get; set; }
        public DateTime? VisitDate { get; set; }
        public string? BloodGroupName { get; set; }
        public string? HealthFacilityName { get; set; }
        public int? HealthFacilityId { get; set; }
        public DateTime? Dob { get; set; }
        public int? BedNo { get; set; }

        public string? ProceduresName { get; set; }
        public DateTime? CreatedOn { get; set; }

        public bool? IsVitalSkip { get; set; } = false;
        public Guid? CreatedBy { get; set; }

    }

    public class PatientVitalDto
    {
        public Guid? PatientVitalId { get; set; }

        public Guid? PatientId { get; set; }

        public Guid? PatientVisitId { get; set; }

        public bool? IsChangeBpsystolic { get; set; }
        public string? Bpsystolic { get; set; }

        public bool? IsChangeBpdiaSystolic { get; set; }
        public string? BpdiaSystolic { get; set; }

        public bool? IsChangePulse { get; set; }
        public string? Pulse { get; set; }

        public bool? IsChangeTemprature { get; set; }
        public string? Temprature { get; set; }

        public bool? IsChangeWeight { get; set; }
        public string? Weight { get; set; }

        public bool? IsChangeHeight { get; set; }
        public string? Height { get; set; }

        public bool? IsChangeResperatoryRate { get; set; }
        public string? ResperatoryRate { get; set; }

        public bool? IsChangeBMI { get; set; }
        public string? BMI { get; set; }

        public Guid? VitalsCollectedBy { get; set; }

        public string? VitalsCollectedByName { get; set; }
        public string? VitalsCollectedByDesignation { get; set; }

        public bool IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }


    }

    public class PatientDiagnosesDiseasesDto
    {
        public string? PatientDiagnoseDiseaseId { get; set; }
        public Guid DiseaseProfileId { get; set; }
        public string? DiseasesName { get; set; }
        public byte? DiagnoseTypeId { get; set; }

    }

    public class PatientDiagnoseProcedureDto
    {
        public string? PatientDiagnoseProcedureId { get; set; }

        public string? PatientDiagnoseId { get; set; }

        public Guid? PatientVisitId { get; set; }

        public int? SectionProcedureId { get; set; }

        public string? ProcedureTitle { get; set; }

        public string? ProceduresName { get; set; }

        public Guid? RecommendBy { get; set; }

        public Guid? PerformedBy { get; set; }

        public string? Feedback { get; set; }

        public bool? IsPerformed { get; set; }

        public string? ToothNumber { get; set; }

        public string? ToothPosition { get; set; }
        public Guid? AssistedBy { get; set; }
        
    }

    public class PatientPrescriptionDto
    {
        public Guid PatientPrescriptionId { get; set; }
        public int? MedicineId { get; set; }
        public string? MedicineName { get; set; }
        public int? Days { get; set; }
        public string? DoseName { get; set; }
        public string? DoseTimeName { get; set; }
        public int? Quantity { get; set; }
        public int? AvailableQuantity { get; set; }
        public string? MedicineDose { get; set; }
        public string? MedicineRoute { get; set; }
        public string? MedicineFrequency { get; set; }
        public string? MedicineInstruction { get; set; }
        public string? MedicineDuration { get; set; }
        public string? BatchNo { get; set; }
        public int? QuantityDispatch { get; set; }
        public Guid? MedicineTypeProfileId { get; set; }
        public Guid? MedicineResourceProfileId { get; set; }
        public decimal? UnitPrice { get; set; }
        public string? Status { get; set; }

        public bool? IsSMLMedicine { get; set; }
    }

    public class MedicineDispatchDto
    {
        public Guid? MedicineDispatchId { get; set; }
        public Guid? PatientDiagnoseId { get; set; }
        public Guid? PatientPrescriptionId { get; set; }
        public int? MedicineId { get; set; }
        public string? MedicineName { get; set; }
        public int? Days { get; set; }
        public string? DoseName { get; set; }
        public string? DoseTimeName { get; set; }
        public int? Quantity { get; set; }
        public int? AvailableQuantity { get; set; }
        public string? MedicineDose { get; set; }
        public string? MedicineRoute { get; set; }
        public string? MedicineFrequency { get; set; }
        public string? MedicineInstruction { get; set; }
        public string? MedicineDuration { get; set; }
        public string? BatchNo { get; set; }
        public int? QuantityPrescribed { get; set; }
        public int? QuantityDispatch { get; set; }
        public Guid? MedicineTypeProfileId { get; set; }
        public Guid? MedicineResourceProfileId { get; set; }
        public decimal? UnitPrice { get; set; }

    }

    public class PatientLabTestDto
    {
        public Guid PatientLabTestId { get; set; }
        public string? LabDepartmentName { get; set; }
        public string? DepartmentShortName { get; set; }
        public string? LabTestName { get; set; }
        public Guid? LabDepartmentProfileId { get; set; }
        public int? LabTestId { get; set; }
        public bool? IsSampleCollected { get; set; }
        public bool? IsSampleRejected { get; set; }
        public bool? IsReportGenerated { get; set; }
        public bool? IsPaid { get; set; }

        public decimal? TestPrice { get; set; }

    }
}