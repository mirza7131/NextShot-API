using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.MEAs.Domain.Models.DTO.UsersModel
{
    public class ShiftDTO
    {
        public int ShiftId { get; set; }
        public string ShiftName { get; set; }
        public List<int> HFTypeId { get; set; }
    }
    public class ShiftWebDTO
    {
        public int ShiftId { get; set; }
        public string ShiftName { get; set; }
        public int HFTypeId { get; set; }
        public string HFTypeName { get; set; }
    }
}
