using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Aggregator.API.Models
{
    public class ProfileDTO
    {
        public Guid ProfileId { get; set; }

        public string Name { get; set; } = null!;

        public string? ShortName { get; set; }

        public int? SequenceNo { get; set; }

        public Guid ProfileTypeId { get; set; }

        public string? ProfileTypeName { get; set; }

        public bool IsActive { get; set; }
    }
}