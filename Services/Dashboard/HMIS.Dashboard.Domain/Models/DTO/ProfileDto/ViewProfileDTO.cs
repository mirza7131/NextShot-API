using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Dashboard.Domain.Models.DTO.ProfileDto
{
    public class ViewProfileDto
    {
        public Guid ProfileId { get; set; }

        public string Name { get; set; } = null!;

        public string? ShortName { get; set; }

        public string? Description { get; set; }

        public int? SequenceNo { get; set; }

        public Guid ProfileTypeId { get; set; }

        public string? ProfileTypeName { get; set; }

        public bool IsActive { get; set; }
    }
}
