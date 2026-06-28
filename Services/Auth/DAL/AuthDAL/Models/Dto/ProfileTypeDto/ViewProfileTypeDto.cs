
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDAL.Models.Dto.ProfileTypeDto
{
    public class ViewProfileTypeDto
    {
        public Guid ProfileTypeId { get; set; }

        public string Name { get; set; } = null!;

        public string? ShortName { get; set; }

        public bool IsActive { get; set; }
    }
}
