using HMIS.Patient.Domain.Models.DTO.PatientDiagnoseDto;
using HMIS.Patient.Domain.Models.DTO.PatientLabTestDto;
using HMIS.Patient.Domain.Models.DTO.PatientOpenVisitDto;
using HMIS.Patient.Domain.Models.DTO.PatientPrescriptionDto;
using HMIS.Patient.Domain.Models.DTO.PatientVitalDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.PatientDto
{
    public class ViewPatientDetailsDto
    {
        public Guid? PatientId { get; set; }

        public string? Mrno { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? FullName { get; set; }

        public string? GuardianName { get; set; }

        public string? NameOfCnicHolder { get; set; }

        public bool? IsSelf { get; set; }

        public Guid? RelationProfileId { get; set; }

        public string? RelationName { get; set; }

        public string Cnic { get; set; } = null!;

        public int? Age { get; set; }

        public DateTime? Dob { get; set; }

        public Guid? NationalityProfileId { get; set; }

        public string? PassportNo { get; set; }

        public Guid? MotherLandProfileId { get; set; }

        public Guid? CasteProfileId { get; set; }

        public Guid? GenderProfileId { get; set; }
        public string? GenderName { get; set; }

        public Guid? BloodGroupProfileId { get; set; }

        public string? BloodGroupName { get; set; }

        public string? MobileNo { get; set; }

        public string? Email { get; set; }

        public string? Ntn { get; set; }

        public int? ProvinceId { get; set; }

        public string? ProvinceName { get; set; }

        public int? DivisionId { get; set; }

        public string? DivisionName { get; set; }

        public int? DistrictId { get; set; }

        public string? DistrictName { get; set; }

        public int TehsilId { get; set; }

        public string? TehsilName { get; set; }

        public int? UnionCouncilId { get; set; }

        public string? UnionCouncilName { get; set; }

        public int? HealthFacilityId { get; set; }

        public string? HealthFacilityName { get; set; }

        public string? Hfmiscode { get; set; }

        public string? ParmanentAddress { get; set; }

        public string? TemporaryAddress { get; set; }

        public Guid? ReligionProfileId { get; set; }

        public Guid? MaritialStatusProfileId { get; set; }

        public string? Domicile { get; set; }

        public DateTime? FollowupDate { get; set; }

        public bool? IsActive { get; set; }

        public virtual ICollection<ViewPatientDiagnoseDto> PatientDiagnoseDiseases { get; set; } = new List<ViewPatientDiagnoseDto>();

        public virtual ICollection<ViewPatientDiagnoseDto> PatientDiagnoses { get; set; } = new List<ViewPatientDiagnoseDto>();

        public virtual ICollection<ViewPatientLabTestDto> PatientLabTests { get; set; } = new List<ViewPatientLabTestDto>();

        public virtual ICollection<ViewPatientOpenVisitDto> PatientOpenVisits { get; set; } = new List<ViewPatientOpenVisitDto>();

        public virtual ICollection<ViewPatientPrescriptionDto> PatientPrescriptions { get; set; } = new List<ViewPatientPrescriptionDto>();

        public virtual ICollection<ViewPatientVitalDto> PatientVitals { get; set; } = new List<ViewPatientVitalDto>();

    }

    public class ViewPrescriptionDto
    {
        public Guid PatientPrescriptionId { get; set; }

        public Guid? PatientId { get; set; }

        public Guid? PatientVisitId { get; set; }

        public int? MedicineId { get; set; }

        public int? MedicineName { get; set; }

        public Guid? MedicineTypeId { get; set; }

        public int? Days { get; set; }

        public Guid? DoseProfileId { get; set; }

        public string? DoseName { get; set; }

        public Guid? DoseTimeProfileId { get; set; }

        public string? DoseTimeName { get; set; }

        public Guid? PrescribedBy { get; set; }

        public String? PrescribedByName { get; set; }

        public int? Quantity { get; set; }

        public bool IsActive { get; set; }
    }
}
