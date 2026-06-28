using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.MEAs.Domain.Models.DTO.Indicator
{
    public class IndicatorPhysicalView
    {
        public int IndicatorId { get; set; }
        public string Question { get; set; }
        public bool IsPhysicalView { get; set; }
    }
}
