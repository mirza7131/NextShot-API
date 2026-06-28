using HMIS.Patient.Domain.Models.DbModels;
using HMIS.Patient.Domain.Models.DTO.DistrictDto;
using HMIS.Patient.Domain.Models.DTO.DivisionDto;
using HMIS.Patient.Domain.Models.DTO.HealthFacilityDto;
using HMIS.Patient.Domain.Models.DTO.ProfileDto;
using HMIS.Patient.Domain.Models.DTO.ProvinceDto;
using HMIS.Patient.Domain.Models.DTO.TehsilDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.Patient
{
    public class ViewPatientDto
    {
        public Guid PatientId { get; set; }

        public string? Mrno { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? FullName { get; set; }

        public string? GuardianName { get; set; }
        public string? NameOfCnicHolder { get; set; }
        public bool? IsSelf { get; set; }
        public int? DataBankId { get; set; }
        public bool IsDataBankRecord { get; set; } = false;
        
        public string? RelationProfileName { get; set; }
        public Guid? RelationProfileId { get; set; }
        public string Cnic { get; set; } = null!;
        public int? Age { get; set; }
        public DateTime? Dob { get; set; }
        public Guid? NationalityProfileId { get; set; }
        public string? PassportNo { get; set; }
        public string? DataBankSource { get; set; }
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

        public string? HealthFacilityName { get; set; }

        public int? HealthFacilityId { get; set; }

        public string? Hfmiscode { get; set; }

        public string? ParmanentAddress { get; set; }

        public string? TemporaryAddress { get; set; }

        public Guid? ReligionProfileId { get; set; }

        public Guid? MaritialStatusProfileId { get; set; }

        public string? Domicile { get; set; }
        public Guid? PatientAdditionalInfoId { get; set; }
        //public int? DepartmentLookupId { get; set; }

        //public int? SectionLookupId { get; set; }

        public Guid? CasteTypeProfileId { get; set; }

        public Guid? AccupationTypeProfileId { get; set; }
        public string? GuardianNameInEnglisOrUrdu { get; set; }

        public string? GuardianCnic { get; set; }

        public string? GuardianAddress { get; set; }

        public string? GuardianMobileNo { get; set; }

        public DateTime? FollowupDate { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public string? UpdatedBy { get; set; }

        public virtual Profile? BloodGroupProfile { get; set; }

        public virtual Profile? CasteProfile { get; set; }

        public virtual ViewDistrictDto? District { get; set; }

        public virtual ViewDivisionDto? Division { get; set; }

        public virtual ViewProfileDto? GenderProfile { get; set; }

        public virtual Profile? MaritialStatusProfile { get; set; }

        public virtual Profile? MotherLandProfile { get; set; }

        public virtual Profile? NationalityProfile { get; set; }

        public virtual ViewProvinceDto? Province { get; set; }

        public virtual Profile? ReligionProfile { get; set; }

        public virtual ViewTehsilDto? Tehsil { get; set; }

        public virtual ViewProfileDto? RelationProfile { get; set; }
        public virtual ViewHealthFacilityDto? HealthFacility { get; set; }

    }
}
