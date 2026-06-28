using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonDTOs.UserDTO
{
    public class UserLoggedInRolesDto
    {
        public Guid? RoleId { get; set; }
        public string Name { get; set; } = null!;
        public string? ShortName { get; set; }
    }
}
