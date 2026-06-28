using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.MEAs.Domain.Models.DTO.Monitoring
{
    public class MonitoringIndicatorListDTO
    {
        public MonitoringIndicatorListDTO()
        {
            ChildQuestion = new List<MonitoringIndicatorListDTO>();
        }
        public int IndicatorId { get; set; }
        public string Question { get; set; }
        public int? ParentIndicatorId { get; set; }
        public List<MonitoringIndicatorListDTO> ChildQuestion { get; set; }
    }
}
