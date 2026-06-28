using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDAL.Models.Dto.ProvinceDto
{
    public class ViewProvinceDto
    {
        public int ProvinceId { get; set; }

        public string? Name { get; set; }

        public string? Code { get; set; }

        public bool IsActive { get; set; }
    }

}
