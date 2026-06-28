using CommonDTOs;
using CommonMessages;
using HMIS.Aggregator.API.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Aggregator.API.Models.Dto.Auth
{
    public  class LocationDto
    {

        public LocationDto()
        {
            ProvinceDropdown = new List<ProvinceDropdownDto>();
            DivisionDropdown = new List<DDTDropdownDto>();
            DistrictDropdown = new List<DDTDropdownDto>();
            TehsilDropdown = new List<DDTDropdownDto>();
            UCDropdown = new List<DropdownDto>();
            HealthFacilityDropdown = new List<HealthFacilityDropdownDto>();
            HfDepartmentDropdown = new List<HfDepartmentDropdownDto>();
            HfDepartmentSectionDropdown = new List<DropdownSectionDto>();
        }
        public List<ProvinceDropdownDto> ProvinceDropdown { get; set; }
        public List<DDTDropdownDto> DivisionDropdown { get; set; }
        public List<DDTDropdownDto> DistrictDropdown { get; set; }
        public List<DDTDropdownDto> TehsilDropdown { get; set; }
        public List<DropdownDto> UCDropdown { get; set; }
        public List<HealthFacilityDropdownDto> HealthFacilityDropdown { get; set; }
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

    public class HfDepartmentDropdownDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? LookupId { get; set; }
        public int? ParentId { get; set; }

    }

    public class DropdownSectionDto
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
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

    }

    public class HmisLocationResponseDTO
    {
        public HttpStatusCode statusCode { get; set; }
        public bool status { get; set; } = true;
        public string message { get; set; } = CommonMessageConstant.Read;
        public LocationDto? data { get; set; }
    }


}
