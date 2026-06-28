using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.MEAs.Domain.Models.DTO
{
    public class UserRoleDTO
    {
        public int UserRoleId { get; set; }

        public int? RoleId { get; set; }

        public int? UserId { get; set; }

        public bool? IsActive { get; set; }
    }
}
