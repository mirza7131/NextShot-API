using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDAL.Models.Dto.RoleDto
{
    public class ViewRoleDto
    {
        public Guid RoleId { get; set; }

        public string Name { get; set; } = null!;

        public string? ShortName { get; set; }

        public bool IsActive { get; set; }

        public ICollection<CreateOrEditRoleMenuDto> RoleMenus { get; set; } = new List<CreateOrEditRoleMenuDto>();

    }

   
}
