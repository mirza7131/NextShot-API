using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.MEAs.Domain.Models.DTO.Settings
{
    public class HFActiveDTO
    {
        public int HfId { get; set; }
        public string Remarks { get; set; }
        public Boolean IsActive { get; set; }
        public DateTime InActiveTill { get; set; }
    }
}
