using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.MEAs.Domain.Models.DTO.Settings
{
    public class ZoneHFDTO
    {
        public ZoneHFDTO()
        {
            HF = new List<int>();
        }
        public int ZoneId { get; set; }
        public string DivisionCode { get; set; }
        public string DistrictCode { get; set; }
        public string TehsilCode { get; set; }
        public List<int> HF { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
