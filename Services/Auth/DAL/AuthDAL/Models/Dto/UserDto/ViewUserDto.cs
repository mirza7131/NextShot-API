using AuthDAL.Models.Dto.UserRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDAL.Models.Dto.UserDto
{
    public class ViewUserDto
    {
        public Guid UserId { get; set; }
        public int? ProvinceId { get; set; }
        public int? DivisionId { get; set; }
        public int? DistrictId { get; set; }
        public int? TehsilId { get; set; }
        public int? UcId { get; set; }
        public int? DepartmentId { get; set; }
        public int? SectionId { get; set; }
        public string Username { get; set; } = null!;
        public string? FatherName { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; } = null!;
        public string? ContactNo { get; set; }
        public string? ProfilePic { get; set; }
        public string? Cnic { get; set; }
        public Guid? CreatedBy { get; set; }
        public string? CreatedByName { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid? UpdatedBy { get; set; }
        public string? UpdatedByName { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? Dob { get; set; }
        public Guid? DesignationProfileId { get; set; }
        public Guid? GenderProfileId { get; set; }
        public int? UserTypeProfileId { get; set; }
        public int? HrId { get; set; }
        public int? HealthFacilityId { get; set; }
        public string? HealthFacilityName { get; set; }
        public string? Role{ get; set; }
        public bool? PmisUser { get; set; }
        public virtual ICollection<ViewUserRoleDto> UserRoles { get; set; } = new List<ViewUserRoleDto>();
    }
}
