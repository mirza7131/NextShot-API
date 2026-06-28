
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Pathalogy.Domain.Models.DTO.ProfileTypeDto
{
    public class ViewProfileTypeDto
    {
        public Guid ProfileTypeId { get; set; }

        public string Name { get; set; } = null!;

        public string? ShortName { get; set; }

        public bool IsActive { get; set; }
    }
}
