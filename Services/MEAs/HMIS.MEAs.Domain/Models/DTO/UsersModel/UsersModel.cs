using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.MEAs.Domain.Models.DTO.UsersModel
{
    internal class UsersModel
    {

    }
    public class RegisterDTO
    {
        public int UserId { get; set; }

        public int? ProvinceId { get; set; }
        public string? DivisionCode { get; set; }
        public string? DistrictCode { get; set; }
        public string? TehsilCode { get; set; }
        public string? LocationCode { get; set; }
        public string? DepartmentName { get; set; }

        public string Username { get; set; } = null!;

        public string? FullName { get; set; }

        public string Password { get; set; } = null!;

        public string? ContactNo { get; set; }

        public string? Cnic { get; set; }

        public int? DesignationId { get; set; }
        public int? ZoneId { get; set; }
        public string? Designation { get; set; }
        public string? Email { get; set; }
        public bool? IsActive { get; set; }
        public int? UserTypeId { get; set; }
        public int? RegionId { get; set; }
        public List<UserLocationDto> UserLocations { get; set; } = new List<UserLocationDto>();
        public List<UserRoleDto> UserRoles { get; set; } = new List<UserRoleDto>();

    }
    public class UserLocationDto
    {
        public int UserLocationId { get; set; }

        public int UserId { get; set; }

        public string? LocationCode { get; set; }
    }
    public class UserRoleDto
    {
        public int UserRoleId { get; set; }

        public int? RoleId { get; set; }

        public int? UserId { get; set; }
        public bool? IsActive { get; set; }
    }
    public class UpdateDTO
    {
        public int UserId { get; set; }

        public int? ProvinceId { get; set; }

        public string? LocationCode { get; set; }

        public string? DepartmentName { get; set; }

        public string Username { get; set; } = null!;

        public string? FullName { get; set; }

        public string Password { get; set; } = null!;

        public string? ContactNo { get; set; }

        public string? Cnic { get; set; }

        public int? DesignationId { get; set; }

        public string? Designation { get; set; }
        public string? Email { get; set; }
        public bool? IsActive { get; set; }
        public int? UserTypeId { get; set; }
        public int? RegionId { get; set; }
        public List<UserRoleDto> UserRoles { get; set; } = new List<UserRoleDto>();
    }
}
