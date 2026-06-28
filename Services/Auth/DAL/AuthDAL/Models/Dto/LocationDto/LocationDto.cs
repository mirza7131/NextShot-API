using AuthDAL.Models.Dto.UnionCouncilDto;
using CommonDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDAL.Models.Dto.LocationDto
{
    public  class LocationDto
    {

        public LocationDto()
        {
            ProvinceDropdown = new List<ProvinceDropdownDto>();
            DivisionDropdown = new List<DDTDropdownDto>();
            DistrictDropdown = new List<DDTDropdownDto>();
            TehsilDropdown = new List<DDTDropdownDto>();
            UCDropdown = new List<ViewUcDto>();
            UnionCouncilDropdown = new List<DropdownDto>();
            HealthFacilityTypeDropdown = new List<HealthFacilityTypeDropdownDto>();
            HealthFacilityDropdown = new List<HealthFacilityDropdownDto>();
            HfDepartmentDropdown = new List<HfDepartmentDropdownDto>();
            HfDepartmentSectionDropdown = new List<DropdownSectionDto>();
        }
        public List<ProvinceDropdownDto> ProvinceDropdown { get; set; }
        public List<DDTDropdownDto> DivisionDropdown { get; set; }
        public List<DDTDropdownDto> DistrictDropdown { get; set; }
        public List<DDTDropdownDto> TehsilDropdown { get; set; }
        public List<ViewUcDto> UCDropdown { get; set; }
        public List<DropdownDto> UnionCouncilDropdown { get; set; }
        public List<HealthFacilityTypeDropdownDto> HealthFacilityTypeDropdown { get; set; }

        public List<HealthFacilityDropdownDto> HealthFacilityDropdown { get; set; }
        public List<HfDepartmentDropdownDto> HfDepartmentDropdown { get; set; }
        public List<DropdownSectionDto> HfDepartmentSectionDropdown { get; set; }

        //public IList<DropDownDTO<Guid>>? ProvinceDropdown { get; set; }
        //public List<DropDownDTO<int>> DivisionDropdown { get; set; }
        //public List<DropDownDTO<int>> DistrictDropdown { get; set; }
        //public List<DropDownDTO<int>> TehsilDropdown { get; set; }
        //public List<DropDownDTO<int>> UCDropdown { get; set; }

    }

    public class DasboardFiltersDto
    {

        public DasboardFiltersDto()
        {
            ProvinceDropdown = new List<ProvinceDropdownDto>();
            DivisionDropdown = new List<DDTDropdownDto>();
            DistrictDropdown = new List<DDTDropdownDto>();
            TehsilDropdown = new List<DDTDropdownDto>();
            UCDropdown = new List<DropdownDto>();
            HealthFacilityTypeDropdown = new List<HealthFacilityTypeDropdownDto>();
            HealthFacilityDropdown = new List<HealthFacilityDropdownDashboardFilterDto>();
            HfDepartmentDropdown = new List<HfDepartmentDropdownDto>();
            HfDepartmentSectionDropdown = new List<DropdownSectionDto>();
        }
        public List<ProvinceDropdownDto> ProvinceDropdown { get; set; }
        public List<DDTDropdownDto> DivisionDropdown { get; set; }
        public List<DDTDropdownDto> DistrictDropdown { get; set; }
        public List<DDTDropdownDto> TehsilDropdown { get; set; }
        public List<DropdownDto> UCDropdown { get; set; }
        public List<HealthFacilityTypeDropdownDto> HealthFacilityTypeDropdown { get; set; }

        public List<HealthFacilityDropdownDashboardFilterDto> HealthFacilityDropdown { get; set; }
        public List<HfDepartmentDropdownDto> HfDepartmentDropdown { get; set; }
        public List<DropdownSectionDto> HfDepartmentSectionDropdown { get; set; }

        //public IList<DropDownDTO<Guid>>? ProvinceDropdown { get; set; }
        //public List<DropDownDTO<int>> DivisionDropdown { get; set; }
        //public List<DropDownDTO<int>> DistrictDropdown { get; set; }
        //public List<DropDownDTO<int>> TehsilDropdown { get; set; }
        //public List<DropDownDTO<int>> UCDropdown { get; set; }

    }
    public class DropdownDto
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public int? LookupId { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? HfTypeCode { get; set; }
        public int? ParentLookupId { get; set; }

    }

    public class ProvinceDropdownDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
    }

    public class DDTDropdownDto
        //Division,District,Tehsil DropdownDTO
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
    }

    public class HealthFacilityDropdownDto
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? HfTypeCode { get; set; }
        public bool? IsRunningHMIS { get; set; } 
    }
    public class HealthFacilityDropdownDashboardFilterDto
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public int? TehsilId { get; set; }
        public int? DistrictId { get; set; }
        public int? DivisionId { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? HfTypeCode { get; set; }
        public bool? IsRunningHMIS { get; set; }
    }

    public class UcDropdownDto
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? TehsilCode { get; set; }
    }

    public class HealthFacilityTypeDropdownDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? HfTypeCode { get; set; }
    }
    public class HfDepartmentDropdownDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? LookupId { get; set; }
        public int? ParentId { get; set; }
        public bool IsRequisition { get; set; } = false;

    }

    public class DropdownSectionDto
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        
        public int? HealthFacilityId { get; set; }
        public int? LookupId { get; set; }
        public string? Name { get; set; }
        public int? ParentLookupId { get; set; }
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
        public byte? SpecialityRunningMode { get; set; }
        public int? SpecialityFee { get; set; }

    }


}
