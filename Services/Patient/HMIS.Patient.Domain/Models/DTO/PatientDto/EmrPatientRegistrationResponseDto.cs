using CommonMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.PatientDto
{
    public class EmrPatientRegistrationResponseDto
    {
        public EmrPatientRegistrationResponseDto()
        {
            //patient = new EmrPatientResponseDto();
            visit = new EmrPatientVisitResponseDto();
        }

        public string? CreatedByName { get; set; }
        public bool IsDoctor { get; set; } = false;

        public Guid? Doctor { get; set; }

        public Guid? PatientId { get; set; }

        public string? Mrno { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? FullName { get; set; }

        public string? GuardianName { get; set; }
        public string? NameOfCnicHolder { get; set; }

        public bool? IsSelf { get; set; }

        public Guid? RelationProfileId { get; set; }

        public string Cnic { get; set; } = null!;

        public bool? IsAfghanCnic { get; set; } = false;

        public int? Age { get; set; }

        public DateTime? Dob { get; set; }


        public string? PassportNo { get; set; }

        public Guid? GenderProfileId { get; set; }

        public string? MobileNo { get; set; }

        public string? Email { get; set; }

        public string? Ntn { get; set; }

        public int? ProvinceId { get; set; }

        public int? DivisionId { get; set; }

        public int? DistrictId { get; set; }

        public int TehsilId { get; set; }

        public int? UnionCouncilId { get; set; }

        public int? HealthFacilityId { get; set; }
        public int? DepartmentLookupId { get; set; }
        public int? SectionLookupId { get; set; }

        public string? ParmanentAddress { get; set; }

        public string? TemporaryAddress { get; set; }

        public Guid? MaritialStatusProfileId { get; set; }

        public bool IsRegisteredExternally { get; set; } = false;

        public bool IsVitalSkip { get; set; } = false;
        public bool? IsActive { get; set; }

        public bool? IsIPD { get; set; }
        public bool? IsEmergency { get; set; }
        public bool? LastTokenNumber { get; set; }
        public bool? LastMRNumber { get; set; }
        public byte? LastMrTokenUpdate { get; set; }
        public string? StageModel { get; set; }


        //public EmrPatientResponseDto patient { get; set; }
        public EmrPatientVisitResponseDto visit { get; set; }
    }

    public class EmrPatientVisitResponseDto
    {
        public Guid? Id { get; set; }
        public bool? IsVisitClosed { get; set; }
        public bool? CollectedVitals { get; set; }
        public bool? IsMedicineDispensed { get; set; }
        public bool? NeedLabs { get; set; }
        public bool? CompletedLabs { get; set; }
        public bool? PrescriptionAdded { get; set; }
        public bool? IsAntenatalCheck { get; set; }
        public bool? IsNeonatalCheck { get; set; }
        public bool? IsPostnatalCheck { get; set; }
        public string? Remarks { get; set; }
        public string? SignatureImagePath { get; set; }
        public DateTime? DateTimeVisitStart { get; set; }
        public DateTime? DateTimeVisitClose { get; set; }
        public Guid? DoctorId { get; set; }
        public int? HealthFacility_Id { get; set; }
        public int? UC_Id { get; set; }
        public string? Token { get; set; }
        public Guid? Patient_Id { get; set; }
        public int? MMHId { get; set; }
        public bool? IsIntegratedVisit { get; set; }
        public DateTime? DateTimeCreatedAt { get; set; }
        public DateTime? DateTimeUpdatedAt { get; set; }
        public Guid? UserIdCreatedBy { get; set; }
        public Guid? UserIdUpdatedBy { get; set; }
        public DateTime? DateTimeDeletedAt { get; set; }
        public Guid? UserIdDeletedBy { get; set; }
        public bool? IsDeleted { get; set; }
        public string? LabTest { get; set; }
        public bool? Nebulization { get; set; }
        public string? ReferToDetail { get; set; }
        public string? ReferFrom { get; set; }
        public bool? IsFpClientGeneralInfo { get; set; }
        public bool? IsFpClientCounselling { get; set; }
        public DateTime? LastVisitDate { get; set; }

    }

    public class EmrPatientResponseDto
    {
        public string? SourceSystemShortName { get; set; } = CommonStringConstant.SourceSystemEMR;
        public byte RequestMode { get; set; }
        public bool? IsFromCallCenter { get; set; } = false;
    }
}
