using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.MEAs.Domain.Models.DTO.UMS
{
    public class UserListDTO
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string DivisionCode { get; set; }
        public string DivisionName { get; set; }
        public string DistrictCode { get; set; }
        public string DistrictName { get; set; }
        public string TehsilCode { get; set; }
        public string TehsilName { get; set; }
        public int ZoneId { get; set; }
        public string LocationCode { get; set; }
        public string ZoneName { get; set; }
        public string CNIC { get; set; }
        public string ContactNo { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}
