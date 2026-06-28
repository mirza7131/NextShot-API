using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.UserRole;

namespace AuthDAL.Models.Dto.UserDto
{
    public class CreateOrEditUserDto
    {
        public Guid? UserId { get; set; }
        public int? HealthFacilityId { get; set; }
        public int? ProvinceId { get; set; }
        public int? DivisionId { get; set; }
        public int? DistrictId { get; set; }
        public int? TehsilId { get; set; }
        public int? UcId { get; set; }
        public int? DepartmentId { get; set; }
        public int? SectionId { get; set; }
        public string? Username { get; set; } = null!;
        public string? FullName { get; set; }
        public string? FatherName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; } = null!;
        public string? ContactNo { get; set; }
        public string? ProfilePic { get; set; }
        public string Cnic { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? Dob { get; set; }
        public Guid? GenderProfileId { get; set; }
        public int? HrId { get; set; }
        public Guid? DesignationProfileId { get; set; }
        public Guid? UserTypeProfileId { get; set; }
        public bool? PmisUser { get; set; }
        public virtual ICollection<CreateOrEditUserRoleDto> UserRoles { get; set; } = new List<CreateOrEditUserRoleDto>();
    }
}
