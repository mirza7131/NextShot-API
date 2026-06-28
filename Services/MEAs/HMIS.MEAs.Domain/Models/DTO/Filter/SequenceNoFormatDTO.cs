using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.MEAs.Domain.Models.DTO.Filter
{
    public class SequenceNoFormatDTO
    {
        public int ApplicationTypeId { get; set; }
        public int ModuleId { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public int IndicatorId { get; set; }
        public int SequenceNo { get; set; }
    }
}
