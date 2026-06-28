using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDAL.Models.Dto.UserDto
{
    public class CreateOrEditHrUserDto
    {
        public int HrId { get; set; }
        public string ProvinceCode { get; set; }
        public string? DivisionCode { get; set; }
        public string? DistrictCode { get; set; }
        public string? TehsilCode { get; set; }
        public string? HealthFacilityCode { get; set; }
        public string? Username { get; set; } = null!;
        public string Cnic { get; set; }
        public string? Password { get; set; } = null!;
        public string? FullName { get; set; }
        public string? FatherName { get; set; }
        public string? Email { get; set; }
        public string? ContactNo { get; set; }
        public string? ProfilePic { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? Dob { get; set; }
        public string? Gender { get; set; }
        public string? Designation { get; set; }
        public string? RoleShortName { get; set; }
    }
}
