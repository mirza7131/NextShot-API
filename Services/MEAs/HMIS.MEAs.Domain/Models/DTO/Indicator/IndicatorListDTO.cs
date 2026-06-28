using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.MEAs.Domain.Models.DTO.Indicator
{
    public class IndicatorListDTO
    {
        public int IndicatorId { get; set; }
        public string Question { get; set; }
        public int? ParentIndicatorId { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public bool? IsActive { get; set; }
        public int? SequenceNo { get; set; }
        public string OptionTypeName { get; set; }
        public int? OptionsCount { get; set; }
    }

    public class IndicatorListNewDTO
    {
        public int IndicatorId { get; set; }
        public int? IndicatorId1 { get; set; }
        public int? IndicatorId2 { get; set; }
        public int? IndicatorId3 { get; set; }
        public string Question { get; set; }
        public string ParentQuestion { get; set; }
        public string ChildQuestion { get; set; }

        public string ChildQuestionlvl1 { get; set; }
        public int? ParentIndicatorId { get; set; }

        public int ModuleId { get; set; }

        public string ModuleName { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public string OptionTypeName { get; set; }

        public string type { get; set; }

        public string SubCategoryName { get; set; }

        //public bool? IsActive { get; set; }
        //public int? SequenceNo { get; set; }
        //public string OptionTypeName { get; set; }
        //public int? OptionsCount { get; set; }
    }
}
