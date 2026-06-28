using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.UserDTO
{
    public class UserLoginDTO
    {
        public string? Email { get; set; }
        public string? Username { get; set; }

        [Required]
        public string? CNIC { get; set; }
        [Required]
        public string? Password { get; set; }
    }

    public class UserSignoutDto
    {
        public Guid UserId { get; set; }
    }
}
