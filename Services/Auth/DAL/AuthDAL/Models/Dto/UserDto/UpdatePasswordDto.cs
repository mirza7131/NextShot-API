using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDAL.Models.Dto.UserDto
{
   public  class UpdatePasswordDto
    {
        public Guid UserId { get; set; }

        public string NewPassword { get; set; }

        public string OldPassword { get; set; }
    }
}
