using locationDTO = CommonDTOs.LocationDTO;

namespace AppCommonMethods
{
    public static class CacheData
    {
        public static List<locationDTO.CacheProfileDto> Profiles = new List<locationDTO.CacheProfileDto>();
        public static List<locationDTO.CacheProvinceDto> Provinces = new List<locationDTO.CacheProvinceDto>();
        public static List<locationDTO.CacheDivisionDto> Divisions = new List<locationDTO.CacheDivisionDto>();
        public static List<locationDTO.CacheDistrictDto> Districts = new List<locationDTO.CacheDistrictDto>();
        public static List<locationDTO.CacheTehsilDto> Tehsils = new List<locationDTO.CacheTehsilDto>();
        public static List<locationDTO.CacheHealthFacilityDto> HealthFacilities = new List<locationDTO.CacheHealthFacilityDto>();
        public static List<locationDTO.CacheUnionCouncilDto> UnionCouncils = new List<locationDTO.CacheUnionCouncilDto>();
        public static List<locationDTO.CacheHfDepartmentDto> HfDepartments = new List<locationDTO.CacheHfDepartmentDto>();
        public static List<locationDTO.CacheHfDepartmentSectionDto> HfDepartmentSections = new List<locationDTO.CacheHfDepartmentSectionDto>();
    }
}
