using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDAL.Models.Dto.RoleDto
{
    public class ViewRoleMenuDto
    {
        public Guid RoleMenuId { get; set; }

        public Guid MenuId { get; set; }

        public Guid RoleId { get; set; }

        public bool CanRead { get; set; }

        public bool CanWrite { get; set; }

        public bool CanEdit { get; set; }

        public bool CanDelete { get; set; }

        public bool? HasAccess { get; set; }
    }
}
