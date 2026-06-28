using CommonDTOs.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.UserDTO
{
    public class UserLoggedInfoDTO
    {
        public UserLoggedInfoDTO()
        {
            UserRoleList = new List<UserRoleDto>();
        }
        public Guid UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? ContactNo { get; set; }
        public string? Username { get; set; }
        public string? Key { get; set; }
        public string? Cnic { get; set; }
        public string? MobileNo { get; set; }
        public int? ProvinceId { get; set; }
        public int? DivisionId { get; set; }
        public int? DistrictId { get; set; }
        public int? TehsilId { get; set; }
        public int? HealthFacilityId { get; set; }
        public string? HealthFacilityName { get; set; }
        public string? HealthFacilityCode { get; set; }
        public string? DesignationName { get; set; }
        public int? DepartmentId { get; set; }
        public DateTime? PasswordChangedOn { get; set; }
        public string? DepartmentName { get; set; }
        public int? SectionId { get; set; }
        public string? SectionName { get; set; }
        public string FormType { get; set; }
        public int? ConsultantSectionId { get; set; }
        public bool IsSuperAdmin { get; set; }

        public bool IsAdmin { get; set; }
        
        public bool IsSeniorDataProcessor { get; set; }
        public bool? IsShowRoleOnly { get; set; }
        public bool? IsUserLoginFirstTime { get; set; }
        public bool IsPMIS { get; set; }
        public bool IsDoctor { get; set; }
        public bool? IsConsultant { get; set; }
        public bool? IsFreeLabTest { get; set; }
        public bool? IsSkipAlmoner { get; set; }

        public Guid? MimsBranchId { get; set; }

        public int UserLevel { get; set; }
        public int? UcId { get; set; }
        public int? HfHrId { get; set; }
        public string? UserLevelName { get; set; }
        public bool AuthViaHR { get; set; }
        public bool UserCreationWithoutHR { get; set; }
        public string? UserRoles { get; set; }
        public string? Token { get; set; }

        public bool? status { get; set; } = true;
        
        public IList<UserRoleDto> UserRoleList { get; set; }
        public int? MimsDepartmentId { get; set; }
        public bool IsOffline { get; set; }
        public bool IsPaidProcedures { get; set; }
        public Guid? EventId { get; set; }
    }

    public class ResponseDTO
    {
        public int StatusCode { get; set; } = 200;
        public string Message { get; set; }
        public Boolean Error { get; set; } = false;
        public Object Data { get; set; }
    }
    public partial class UserRoleDto
    {
        public Guid RoleId { get; set; }

        public string Name { get; set; } = null!;
        public string? ShortName { get; set; } = null!;
        public string? RoutingUrl { get; set; } = null!;
        public string? RoleType { get; set; } = null!;
    }
}
