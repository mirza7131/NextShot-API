using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.MEAs.Domain.Models.DTO.Categories
{
    public class CategoryListDTO
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int? ModuleId { get; set; } = 0;
        public int SequenceNo { get; set; }
        public bool? IsRequired { get; set; }
    }
}
