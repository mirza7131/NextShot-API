using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDAL.Models.Dto.RoleDto
{
    public class CreateOrEditRoleDto
    {
        public CreateOrEditRoleDto()
        {
            RoleMenus = new List<CreateOrEditRoleMenuDto>();
        }
        public Guid? RoleId { get; set; }
        public string Name { get; set; } = null!;
        public string? ShortName { get; set; }
        public string? RoutingUrl { get; set; }
        public bool IsActive { get; set; }
        public bool HasAccess { get; set; }
        public bool IsOpended { get; set; }

        public ICollection<CreateOrEditRoleMenuDto> RoleMenus { get; set; }

    }
}
