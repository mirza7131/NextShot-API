using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.MEAs.Domain.Models.DTO.Monitoring
{
    public class ChangeAnswerDTO
    {
        public int indicatorId { get; set; }
        public string changeValue { get; set; }
        public int MonitoringId { get; set; }
    }
}
