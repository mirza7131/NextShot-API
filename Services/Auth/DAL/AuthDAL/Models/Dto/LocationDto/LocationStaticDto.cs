using AuthDAL.Models.Dto.HealthFacilityDto;
using CommonDTOs.LocationDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDAL.Models.Dto.LocationDto
{
    public static class LocationStaticDto
    {

        #region Provice
        public static List<ProvinceDropdownDto> ProvinceDropdown { get; set; } = new List<ProvinceDropdownDto>();
        public static List<ProvinceDropdownDto> GetProvinceDropdowns() { return ProvinceDropdown; }
        public static void SetProvinceDropdowns(List<ProvinceDropdownDto> input) { ProvinceDropdown = input; }
        #endregion

        #region Division
        public static List<DDTDropdownDto> DivisionDropdown { get; set; } = new List<DDTDropdownDto>();
        public static List<DDTDropdownDto> GetDivisionDropdowns() { return DivisionDropdown; }
        public static void SetDivisionDropdowns(List<DDTDropdownDto> input) { DivisionDropdown = input; }
        #endregion

        #region District

        public static List<DDTDropdownDto> DistrictDropdown { get; set; } = new List<DDTDropdownDto>();
        public static List<DDTDropdownDto> GetDistrictDropdowns() { return DistrictDropdown; }
        public static void SetDistrictDropdowns(List<DDTDropdownDto> input) { DistrictDropdown = input; }

        #endregion

        #region Tehsil

        public static List<DDTDropdownDto> TehsilDropdown { get; set; } = new List<DDTDropdownDto>();

        public static List<DDTDropdownDto> GetTehsilDropdowns() { return TehsilDropdown; }
        public static void SetTehsilDropdowns(List<DDTDropdownDto> input) { TehsilDropdown = input; }

        #endregion

        #region HealthFacility

        public static List<ViewHealthFacilityDto> HealthFacilityDropdown { get; set; } = new List<ViewHealthFacilityDto>();
        public static List<ViewHealthFacilityDto> GetHealthFacilityDropdowns() { return HealthFacilityDropdown; }
        public static void SetHealthFacilityDropdowns(List<ViewHealthFacilityDto> input) { HealthFacilityDropdown = input; }
        #endregion


        #region HfDepartment
        public static List<HfDepartmentDropdownDto> HfDepartmentDropdown { get; set; } = new List<HfDepartmentDropdownDto>();
        public static List<HfDepartmentDropdownDto> GetHfDepartmentDropdowns() { return HfDepartmentDropdown; }
        public static void SetHfDepartmentDropdowns(List<HfDepartmentDropdownDto> input) { HfDepartmentDropdown = input; }
        #endregion

        #region HfDepartmentSection

        public static List<DropdownSectionDto> HfDepartmentSectionDropdown { get; set; } = new List<DropdownSectionDto>();
        public static List<DropdownSectionDto> GetHfDepartmentSectionDropdowns() { return HfDepartmentSectionDropdown; }
        public static void SetHfDepartmentSectionDropdowns(List<DropdownSectionDto> input) { HfDepartmentSectionDropdown = input; }

        #endregion



    }
}
