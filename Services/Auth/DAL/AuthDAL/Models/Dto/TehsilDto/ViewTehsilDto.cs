using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDAL.Models.Dto.TehsilDto
{
    public class ViewTehsilDto
    {
        public int TehsilId { get; set; }

        public string? Code { get; set; }

        public string? Name { get; set; }

        public int? DistrictId { get; set; }

        public bool IsActive { get; set; }


    }
}
