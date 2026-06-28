using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMIS.MEAs.Domain.Models.DTO.Indicator;

namespace HMIS.MEAs.Domain.Models.DTO.Monitoring
{
    public class MonitoringChildDTO
    {
        public int? ApplicationType { get; set; }
        public int CaegoryId { get; set; }
        public string CategoryName { get; set; }
        public List<IndicatorDTO> indicators { get; set; }
        public int? ModuleId { get; set; }
    }


    public class MonitoringChildFloodDTO
    {
        public int CaegoryId { get; set; }
        public string CategoryName { get; set; }
        public List<FloodIndicatorDTO> indicators { get; set; }
        public int? ModuleId { get; set; }
    }
    public class MonitoringChildExportDTO
    {
        public string Question { get; set; }
        public int IndicatorId { get; set; }
        public string answer { get; set; }
        public int MonitoringMasterId { get; set; }
    }
}
