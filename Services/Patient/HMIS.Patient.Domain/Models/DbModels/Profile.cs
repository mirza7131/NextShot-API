using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels;

public partial class Profile
{
    public Guid ProfileId { get; set; }

    public int? ProfileAutoId { get; set; }

    public string Name { get; set; } = null!;

    public string? ShortName { get; set; }

    public int? SequenceNo { get; set; }

    public string? Description { get; set; }

    public Guid ProfileTypeId { get; set; }

    public Guid? ParentProfileId { get; set; }

    public bool? IsDssDisease { get; set; }

    public int? ChartPieSequenceNo { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public long? UserLogId { get; set; }

    public byte ActionTypeId { get; set; }

    public virtual ICollection<DentalSterilizationRecord> DentalSterilizationRecords { get; } = new List<DentalSterilizationRecord>();

    public virtual ICollection<HealthFacilityStation> HealthFacilityStations { get; } = new List<HealthFacilityStation>();

    public virtual ICollection<LabTest> LabTestDepartmentProfiles { get; } = new List<LabTest>();

    public virtual ICollection<LabTestDetail> LabTestDetails { get; } = new List<LabTestDetail>();

    public virtual ICollection<LabTest> LabTestLabTestCategoryProfiles { get; } = new List<LabTest>();

    public virtual ICollection<LabTest> LabTestLabTestTypeProfiles { get; } = new List<LabTest>();

    public virtual ICollection<PatientAdmissionDetail> PatientAdmissionDetailPatientLevelProfiles { get; } = new List<PatientAdmissionDetail>();

    public virtual ICollection<PatientAdmissionDetail> PatientAdmissionDetailPatientStatusProfiles { get; } = new List<PatientAdmissionDetail>();

    public virtual ICollection<Patient> PatientBloodGroupProfiles { get; } = new List<Patient>();

    public virtual ICollection<Patient> PatientCasteProfiles { get; } = new List<Patient>();

    public virtual ICollection<PatientDiagnoseDisease> PatientDiagnoseDiseases { get; } = new List<PatientDiagnoseDisease>();

    public virtual ICollection<PatientDiagnose> PatientDiagnoseMedicineRegimeProfiles { get; } = new List<PatientDiagnose>();

    public virtual ICollection<PatientDiagnose> PatientDiagnoseOutcomeStatusProfiles { get; } = new List<PatientDiagnose>();

    public virtual ICollection<PatientDischargeDetail> PatientDischargeDetails { get; } = new List<PatientDischargeDetail>();

    // public virtual ICollection<PatientDocument> PatientDocumentDocumentProfiles { get; } = new List<PatientDocument>();

    // public virtual ICollection<PatientDocument> PatientDocumentPatientDocumentTypeProfiles { get; } = new List<PatientDocument>();

    public virtual ICollection<Patient> PatientGenderProfiles { get; } = new List<Patient>();

    public virtual ICollection<PatientLabTest> PatientLabTests { get; } = new List<PatientLabTest>();

    public virtual ICollection<Patient> PatientMaritialStatusProfiles { get; } = new List<Patient>();

    public virtual ICollection<Patient> PatientMotherLandProfiles { get; } = new List<Patient>();

    public virtual ICollection<Patient> PatientNationalityProfiles { get; } = new List<Patient>();

    public virtual ICollection<PatientOpenVisit> PatientOpenVisitCurrentStationProfiles { get; } = new List<PatientOpenVisit>();

    public virtual ICollection<PatientOpenVisit> PatientOpenVisitReasonIfNotEligibleForSscNavigations { get; } = new List<PatientOpenVisit>();

    public virtual ICollection<PatientOpenVisit> PatientOpenVisitVisitTypeProfiles { get; } = new List<PatientOpenVisit>();

    public virtual ICollection<PatientPrescription> PatientPrescriptionDoseProfiles { get; } = new List<PatientPrescription>();

    public virtual ICollection<PatientPrescription> PatientPrescriptionDoseTimeProfiles { get; } = new List<PatientPrescription>();

    public virtual ICollection<Patient> PatientRelationProfiles { get; } = new List<Patient>();

    public virtual ICollection<Patient> PatientReligionProfiles { get; } = new List<Patient>();

    public virtual ICollection<PatientVaccination> PatientVaccinationVaccinationProfiles { get; } = new List<PatientVaccination>();

    public virtual ICollection<PatientVaccination> PatientVaccinationVaccinationTypeProfiles { get; } = new List<PatientVaccination>();

    public virtual ICollection<PhysiotherapyForm> PhysiotherapyForms { get; } = new List<PhysiotherapyForm>();

    public virtual ICollection<PhysiotherapyModality> PhysiotherapyModalities { get; } = new List<PhysiotherapyModality>();

    public virtual ProfileType ProfileType { get; set; } = null!;

    public virtual ICollection<User> Users { get; } = new List<User>();
}