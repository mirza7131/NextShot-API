using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.MEAs.Domain.Models.DTO.Monitoring
{
    public class InchargeDTO
    {
        public string name { get; set; }
        public string mobile_no { get; set; }
        public int designation { get; set; }
        public string cnic { get; set; }
        //public string vehicleNo { get; set; }
        //public string vehicleType { get; set; }
    }
}
