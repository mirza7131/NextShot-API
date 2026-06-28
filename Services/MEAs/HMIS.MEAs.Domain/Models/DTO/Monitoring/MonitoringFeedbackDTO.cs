using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.MEAs.Domain.Models.DTO.Monitoring
{
    public class MonitoringFeedbackDTO
    {
        public string Facility_Incharge_Comment { get; set; }
        public string MeaComment { get; set; }
        public List<MonitoringAttachmentsDTO> images { get; set; }

    }
}
