using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.MEAs.Domain.Models.DTO.Indicator
{
    public class IndicatorDropDownDTO
    {
        public IndicatorDropDownDTO()
        {
            Options = new List<IndicatorOptionDropDownDTO>();
        }
        public int IndicatorId { get; set; }
        public string IndicatorName { get; set; }
        public int? ParentIndicatorId { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public List<int> Shifts { get; set; }
        public List<int> HFTypes { get; set; }
        public List<IndicatorOptionDropDownDTO> Options { get; set; }
    }
    public class IndicatorOptionDropDownDTO
    {
        public int OptionId { get; set; }
        public string OptionName { get; set; }
    }
}
