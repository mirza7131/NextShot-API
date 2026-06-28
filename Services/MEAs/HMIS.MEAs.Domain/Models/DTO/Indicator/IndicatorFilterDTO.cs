using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.MEAs.Domain.Models.DTO.Indicator
{
    public class IndicatorFilterDTO
    {
        public int ApplicationType { get; set; }
        public int ModuleId { get; set; }
        public int HfTypeId { get; set; }
        public int ShiftId { get; set; }
        public string HfId { get; set; }
        public string FormId { get; set; }
        public int CategoryId { get; set; }
        public bool? IsRequired { get; set; }
        public int SubCategoryId { get; set; }
    }

    public class IndicatorFilterDTONew
    {
        public int ApplicationType { get; set; }
        public int ModuleId { get; set; }
        public int HfTypeId { get; set; }
        public int ShiftId { get; set; }
        public string HfId { get; set; }
        public string FormId { get; set; }
        public int CategoryId { get; set; }
        public bool? IsRequired { get; set; }
        public int SubCategoryId { get; set; }
    }
}
