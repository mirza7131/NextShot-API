using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMIS.MEAs.Domain.Models.DTO.UsersModel;

namespace HMIS.MEAs.Domain.Models.DTO.Categories
{
    public class CategoryAddDTO
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int ApplicationTypeId { get; set; }
        public int ModuleId { get; set; }
        public bool isActive { get; set; }
        public List<ShiftDTO> Shifts { get; set; }
        public List<HFTypeDTO> HfTypes { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int SequenceNo { get; set; }
    }
}
