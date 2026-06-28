using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.EMC.Domain.Models.Dto
{
    public class ViewStationDto
    {
        public Guid? StationProfileId { get; set; }
        public int? SequenceNo { get; set; }
        public string? ShortName { get; set; }
        public string Name { get; set; }
    }
}
