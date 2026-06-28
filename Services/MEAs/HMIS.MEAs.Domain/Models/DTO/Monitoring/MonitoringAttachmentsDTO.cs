using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.MEAs.Domain.Models.DTO.Monitoring
{
    public class MonitoringAttachmentsDTO
    {
        public int? Id { get; set; }
        public string Image { get; set; }
        public string ImageName { get; set; }
        public string Title { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
