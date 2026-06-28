using HMIS.Patient.Domain.Models.DTO.PatientAdmissionDetailDto;
using HMIS.Patient.Domain.Models.DTO.PatientOpenVisitDto;
﻿using HMIS.Patient.Domain.Models.DbModels;
using HMIS.Patient.Domain.Models.DTO.CreatePatientImagesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMIS.Patient.Domain.Models.DTO.CreatePatinetFingerprintsDTO;
using HMIS.Patient.Domain.Models.DTO.CreatePatientSource;
using CommonMessages;
using HMIS.Patient.Domain.Models.DTO.Patient;

namespace HMIS.Patient.Domain.Models.DTO.PatientDto
{
    public class CreateOrEditPatientWithVisitDto
    {
        public CreateOrEditPatientWithVisitDto()
        {
            PatientOpenVisits = new List<CreateOrEditOpenVisitDto>();

            PatientAdmissionDetails = new List<CreateOrEditPatientAdmissionDetailDto>();
        }
        public string? SourceSystemShortName { get; set; }
        public byte RequestMode { get; set; }

        public bool? IsFromCallCenter { get; set; } = false;
        public bool? IsFromIPD { get; set; } = false;
        public bool? IsFromER { get; set; } = false;
        public byte? IPDSource { get; set; } // 1 for Registration, 2 for Refer
        public string? VisitFor { get; set; } // Visit Generated For
        public byte? VisitSource { get; set; } // 1 for Registration, 2 for Refer

        public int? TbVisitNo { get; set; }

        public string? CreatedByName { get; set; }
        public Guid? ReferVisitId { get; set; }
        public Guid? ReferredBy { get; set; }
        public int? ReferredByDepartmentLookupId { get; set; }
        public int? ReferredBySectionLookupId { get; set; }

        public bool IsFromPMIS { get; set; } = false;
        public bool IsDoctor { get; set; } = false;

        public Guid? Doctor { get; set; }

        public Guid? PatientId { get; set; }
        public Guid? UnknownPatientId { get; set; }

        public string? Mrno { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? FullName { get; set; }

        public string? GuardianName { get; set; }
        public string? NameOfCnicHolder { get; set; }

        public bool? IsSelf { get; set; }
        public int? DataBankId { get; set; }
        public bool IsDataBankRecord { get; set; } = false;

        public Guid? RelationProfileId { get; set; }

        public string Cnic { get; set; } = null!;

        public bool? IsAfghanCnic { get; set; } = false;

        public int? Age { get; set; }

        public DateTime? Dob { get; set; }

        public Guid? NationalityProfileId { get; set; }

        public string? PassportNo { get; set; }

        public Guid? MotherLandProfileId { get; set; }

        public Guid? CasteProfileId { get; set; }

        public Guid? GenderProfileId { get; set; }

        public Guid? BloodGroupProfileId { get; set; }

        public string? MobileNo { get; set; }

        public string? Email { get; set; }

        public string? Ntn { get; set; }

        public int? ProvinceId { get; set; }

        public int? DivisionId { get; set; }

        public int? DistrictId { get; set; }

        public int TehsilId { get; set; }

        public int? UnionCouncilId { get; set; }

        public int? HealthFacilityId { get; set; }
        public int? PatinetPrivateHeathFacilityId { get; set; }

        public int? ReferredHealthFacilityId { get; set; }

        public string? SlipNo { get; set; }

        public int? DepartmentLookupId { get; set; }
        public int? BedNo { get; set; }

        public int? SectionLookupId { get; set; }

        public string? Hfmiscode { get; set; }

        public string? ParmanentAddress { get; set; }

        public string? TemporaryAddress { get; set; }

        public Guid? ReligionProfileId { get; set; }

        public Guid? MaritialStatusProfileId { get; set; }

        public string? Domicile { get; set; }

        public DateTime? FollowupDate { get; set; }
        public DateTime? VisitDate { get; set; }

        public bool IsRegisteredExternally { get; set; } = false;

        public string? SourcePatientId { get; set; }

        public string? SourceMrno { get; set; }

        public Guid? SourceSystemId { get; set; }

        public string? SourceHealthFacilityId { get; set; }
        public string? SourceVisitId { get; set; }

        public bool? IsReferred { get; set; }
        public bool? IsReferredIpd { get; set; }

        public bool IsVitalSkip { get; set; } = false;
        public string? SourceReferredHealthFacilityId { get; set; }
        public bool? IsActive { get; set; }

        public Guid? TbPatientTypeId { get; set; }
        public Guid? TbPatientTreatmentLengthId { get; set; }
        public Guid? TbPatientLengthOfInterruptionId { get; set; }
        public int? TbPatientNoOfMedicineTaken { get; set; }
        public int? NoOfMonthsMedicineIssued { get; set; }
        public string? DRTBPatientStatus { get; set; }
        public string? ComorbidityBy { get; set; }
        public string? DateOfInocvlation { get; set; }
        public string? ConsultantName { get; set; }
        public string? StatusOfVision { get; set; }
        public string? Recovery { get; set; }
        public int? HospitalInjectedId { get; set; }
        public string? OtherHealthfacility { get; set; }
        public string? EyeInvolved { get; set; }
        public bool? isEyeBlindness { get; set; } = false;

        public virtual ICollection<CreateOrEditOpenVisitDto> PatientOpenVisits { get; set; }

        public virtual ICollection<CreateOrEditPatientAdmissionDetailDto> PatientAdmissionDetails { get; set; }

        public string? Category{ get; set; }
        public string? PatientRole{ get; set; }
        public bool? isAlive{ get; set; }
        public bool? IsEligibleForSsc { get; set; }
        public string? SscNumber { get; set; }
        public Guid? ReasonIfNotEligibleForSsc { get; set; }
        public virtual ICollection<PatientImagesDTO>? PatientImages { get; set; } = new List<PatientImagesDTO>();
        public virtual ICollection<PatientFingerprintsDTO>? PatientFingerprints { get; set; } = new List<PatientFingerprintsDTO>();
        public virtual PatientSourceDTO? PatientSource { get; set; } = new PatientSourceDTO();
        public virtual CreateOrEditAdditionalPatientDTO? CreateOrEditAdditionalPatient { get; set;} = new CreateOrEditAdditionalPatientDTO();
        public virtual TbPatientSourceDto? TbPatientSource { get; set;} = new TbPatientSourceDto();

    }


    #region Central Registration Working

    public class CreateOrEditPatientWithVisitCentrallyDto
    {
        public string? SourceSystemShortName { get; set; }
        public byte RequestMode { get; set; }

        public CreateOrEditPatientCentrallyDto? Patient { get; set; }
        public CreateOrEditOpenVisitCentrallyDto? PatientOpenVisit { get; set; }

    }

    public class CreateOrEditPatientCentrallyDto
    {
        public Guid? PatientId { get; set; }
        public Guid? UnknownPatientId { get; set; }

        public string? Mrno { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? FullName { get; set; }

        public string? GuardianName { get; set; }
        public string? NameOfCnicHolder { get; set; }

        public bool? IsSelf { get; set; }
        public int? DataBankId { get; set; }
        public bool IsDataBankRecord { get; set; } = false;

        public Guid? RelationProfileId { get; set; }

        public string Cnic { get; set; } = null!;

        public bool? IsAfghanCnic { get; set; } = false;

        public int? Age { get; set; }

        public DateTime? Dob { get; set; }

        public Guid? NationalityProfileId { get; set; }

        public string? PassportNo { get; set; }

        public Guid? MotherLandProfileId { get; set; }

        public Guid? CasteProfileId { get; set; }

        public Guid? GenderProfileId { get; set; }

        public Guid? BloodGroupProfileId { get; set; }

        public string? MobileNo { get; set; }

        public string? Email { get; set; }

        public string? Ntn { get; set; }

        public int? ProvinceId { get; set; }

        public int? DivisionId { get; set; }

        public int? DistrictId { get; set; }

        public int TehsilId { get; set; }

        public int? UnionCouncilId { get; set; }

        public int? HealthFacilityId { get; set; }
        

        public string? ParmanentAddress { get; set; }

        public string? TemporaryAddress { get; set; }

        public Guid? ReligionProfileId { get; set; }

        public Guid? MaritialStatusProfileId { get; set; }

        public string? Domicile { get; set; }

        public DateTime? FollowupDate { get; set; }

        public bool IsRegisteredExternally { get; set; } = false;

        public Guid? SourceSystemId { get; set; }
        public string? SourcePatientId { get; set; }

        public string? SourceMrno { get; set; }

        public bool? IsFromCallCenter { get; set; } = false;

        public bool? IsActive { get; set; }

        public Guid? TbPatientTypeId { get; set; }
        public Guid? TbPatientTreatmentLengthId { get; set; }
        public Guid? TbPatientLengthOfInterruptionId { get; set; }
        public int? TbPatientNoOfMedicineTaken { get; set; }
        public int? NoOfMonthsMedicineIssued { get; set; }
        public string? DRTBPatientStatus { get; set; }
        public string? ComorbidityBy { get; set; }
        public string? DateOfInocvlation { get; set; }
        public string? ConsultantName { get; set; }
        public string? StatusOfVision { get; set; }
        public string? Recovery { get; set; }
        public int? HospitalInjectedId { get; set; }
        public string? OtherHealthfacility { get; set; }
        public string? EyeInvolved { get; set; }
        public bool? isEyeBlindness { get; set; } = false;
        public string? Category { get; set; }
        public string? PatientRole { get; set; }
        public bool? isAlive { get; set; }
    }

    public class CreateOrEditOpenVisitCentrallyDto
    {
        public bool? IsFromCallCenter { get; set; } = false;
        public bool? IsFromIPD { get; set; } = false;
        public bool? IsFromER { get; set; } = false;
        public byte? IPDSource { get; set; } // 1 for Registration, 2 for Refer
        public string? VisitFor { get; set; } // Visit Generated For
        public byte? VisitSource { get; set; } // 1 for Registration, 2 for Refer

        public int? TbVisitNo { get; set; }

        public string? CreatedByName { get; set; }
        public Guid? ReferVisitId { get; set; }
        public Guid? ReferredBy { get; set; }
        public int? ReferredByDepartmentLookupId { get; set; }
        public int? ReferredBySectionLookupId { get; set; }

        public bool IsFromPMIS { get; set; } = false;
        public bool IsDoctor { get; set; } = false;

        public Guid? Doctor { get; set; }

        public int? HealthFacilityId { get; set; }

        public int? ReferredHealthFacilityId { get; set; }

        public string? SlipNo { get; set; }

        public int? DepartmentLookupId { get; set; }
        public int? BedNo { get; set; }

        public int? SectionLookupId { get; set; }

        public DateTime? VisitDate { get; set; }

        public Guid? SourceSystemId { get; set; }

        public string? SourceHealthFacilityId { get; set; }
        public string? SourceVisitId { get; set; }

        public bool? IsReferred { get; set; }
        public bool? IsReferredIpd { get; set; }

        public bool IsVitalSkip { get; set; } = false;
        public string? SourceReferredHealthFacilityId { get; set; }
        public int? PatinetPrivateHeathFacilityId { get; set; }
        public bool? IsEligibleForSsc { get; set; }
        public string? SscNumber { get; set; }
        public Guid? ReasonIfNotEligibleForSsc { get; set; }
    }

    #endregion
    public class TbPatientSourceDto
    {
        public Guid? TbPatientSource { get; set; }
        public string? Name { get; set; }
        public string? ContactNo { get; set; }
        public string? CNIC { get; set; }
        public string? NameofFacility { get; set; }

        public string? LHSName { get; set; }
        public string? LHSContactNo { get; set; }
        public string? LHSCNIC { get; set; }
    }

}
