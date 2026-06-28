using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDAL.Models.DbModels
{
    public class CreateOrEditUserAssignableRolesDTO
    {

        public Guid? UserId { get; set; }
        public ICollection<UserAssignableRole> UserAssignableRoles { get; set; } = new List<UserAssignableRole>();
    }
}
