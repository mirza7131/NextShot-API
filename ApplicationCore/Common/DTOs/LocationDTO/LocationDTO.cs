using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonDTOs.LocationDTO
{

    public class CacheProvinceDto
    {
        public int ProvinceId { get; set; }

        public string? Name { get; set; }

        public string? Code { get; set; }

        public bool IsActive { get; set; }

        public DateTime? CreatedOn { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public Guid? UpdatedBy { get; set; }

        public DateTime? DeletedOn { get; set; }

        public Guid? DeletedBy { get; set; }

        public long? UserLogId { get; set; }

        public byte ActionTypeId { get; set; }

    }

    public class CacheDivisionDto
    {
        public int DivisionId { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public int? ProvinceId { get; set; }
        public bool IsActive { get; set; }

    }

    public class CacheDistrictDto
    {
        public int DistrictId { get; set; }

        public string? Code { get; set; }

        public string? Name { get; set; }

        public int? DivisionId { get; set; }

        public bool IsActive { get; set; }


    }

    public class CacheTehsilDto
    {
        public int TehsilId { get; set; }

        public string? Code { get; set; }

        public string? Name { get; set; }

        public int? DistrictId { get; set; }

        public bool IsActive { get; set; }


    }

    public class CacheUnionCouncilDto
    {
        public int UnionCouncilId { get; set; }

        public string? Name { get; set; }

        public string? Code { get; set; }

        public int TehsilId { get; set; }

        public bool IsActive { get; set; }
    }

    public class CacheProfileDto
    {
        public Guid ProfileId { get; set; }

        public string Name { get; set; } = null!;

        public string? ShortName { get; set; }

        public string? Description { get; set; }

        public int? SequenceNo { get; set; }

        public Guid ProfileTypeId { get; set; }

        public string? ProfileTypeName { get; set; }

        public bool IsActive { get; set; }
        public Guid? ParentProfileId { get; set; }
        public bool? IsDssDisease { get; set; }
        public virtual CacheProfileTypeDto ProfileType { get; set; } = null!;

    }

    public class CacheProfileTypeDto
    {
        public Guid ProfileTypeId { get; set; }

        public string Name { get; set; } = null!;

        public string? ShortName { get; set; }

        public bool IsActive { get; set; }
    }

    public class CacheHealthFacilityDto
    {
        public int HealthFacilityId { get; set; }

        public string? Name { get; set; }

        public string? Code { get; set; }

        public string? HealthFacilityTypeCode { get; set; }

        public int? HealthFacilityTypeId { get; set; }

        public int? ProvinceId { get; set; }

        public string? ProvinceCode { get; set; }

        public int? DivisionId { get; set; }

        public string? DivisionCode { get; set; }

        public int? DistrictId { get; set; }

        public string? DistrictCode { get; set; }

        public int? TehsilId { get; set; }

        public string? TehsilCode { get; set; }

        public int? UnionCouncilId { get; set; }

        public string? UnionCouncilCode { get; set; }

        public bool IsActive { get; set; }
        public bool? IsRunningHMIS { get; set; }


    }

    public class CacheHfDepartmentDto
    {
        public int HfDepartmentId { get; set; }
        public int? HealthFacilityId { get; set; }
        public int? DepartmentLookupId { get; set; }
        public List<int?> SectionIds { get; set; }
        public string? DepartmentName { get; set; }
        public string? SectionNames { get; set; }
        public string? HealthFacilityName { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedByName { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? UpdatedByName { get; set; }
        public Guid? UpdatedBy { get; set; }
        public bool IsActive { get; set; }
        public virtual CacheDepartmentLookupDto? DepartmentLookup { get; set; }
    }

    public class CacheDepartmentLookupDto
    {
        public int HfDepartmentId { get; set; }

        public int DepartmentLookupId { get; set; }

        public string? Name { get; set; }

        public string? DisplayName { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; }

    }
    public class CacheHfDepartmentSectionDto
    {
        public int HfDepartmentSectionId { get; set; }
        public int? HfDepartmentId { get; set; }
        public int? DepartmentLookupId { get; set; }
        public string? DepartmentName { get; set; }
        public int? SectionLookupId { get; set; }
        public string? SectionName { get; set; }
        public string? VitalsFloorNo { get; set; }
        public string? VitalsRoomNo { get; set; }
        public string? DoctorFloorNo { get; set; }
        public string? DoctorRoomNo { get; set; }
        public string? PharmacyFloorNo { get; set; }
        public string? PharmacyRoomNo { get; set; }
        public string? PathalogyFloorNo { get; set; }
        public string? PathalogyRoomNo { get; set; }

        public string? AlmonerFloorNo { get; set; }
        public string? AlmonerRoomNo { get; set; }
        public bool IsActive { get; set; }
        public virtual CacheSectionLookupDto? SectionLookup { get; set; }

    }

    public class CacheSectionLookupDto
    {
        public int SectionLookupId { get; set; }
        public int DepartmentLookupId { get; set; }
        public string? Name { get; set; }
        public string? DisplayName { get; set; }
        public string? DepartmentName { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public int? ConsultantSectionLookupId { get; set; }
        public int? SpecialityFee { get; set; }
    }

}
