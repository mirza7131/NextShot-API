using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.MEAs.Domain.Models.DTO.Monitoring
{
    public class MonitoringAttendanceDTO
    {
        public string Cnic { get; set; }
        public int? DesignationId { get; set; }
        public string Mobile { get; set; }
        public string Name { get; set; }
        public int? PresenceStatusId { get; set; }
    }
}
