using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Pathalogy.Domain.Models.DTO.ProfileDto
{
    public class CreateOrEditProfileDto
    {
        public Guid? ProfileId { get; set; }

        public string Name { get; set; } = null!;

        public string? ShortName { get; set; }
        
        public string? Description { get; set; }
        
        public int? SequenceNo { get; set; }

        public Guid ProfileTypeId { get; set; }

        public bool IsActive { get; set; }

        public Guid? ParentProfileId { get; set; }

        public bool? IsDssDisease { get; set; }
    }
}
