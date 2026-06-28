using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.MEAs.Domain.Models.DTO.UMS
{
    public class UserZoneChangeDTO
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Division { get; set; }
        public string District { get; set; }
        public string Tehsil { get; set; }
        public string Zone { get; set; }
        public int ZoneId { get; set; }
    }
}
