using AuthDAL.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDAL.Models.Dto.RoleMenuDto
{
    public class RoleMenuAccessDto
    {
        public RoleMenuAccessDto()
        {
            ChildMenu = new List<ViewGetEditRoleMenuAccess>();
        }
        public Guid? RoleMenuId { get; set; }

        public Guid? RoleId { get; set; }

        public Guid MenuId { get; set; }

        public bool? IsModule { get; set; }

        public Guid? ModuleId { get; set; }

        public string? Name { get; set; }

        public string? DisplayName { get; set; }
        public bool HasAccess { get; set; }
        public bool IsOpened { get; set; } = false;
        public List<ViewGetEditRoleMenuAccess> ChildMenu { get; set; }
    }
}
