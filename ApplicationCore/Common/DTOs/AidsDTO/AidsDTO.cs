using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonDTOs.AidsDTO
{
    public class AidsDTO
    {
        public string? centerName { get; set; }
        public int? totalRegister { get; set; }

        public class ResponseAidsDTO
        {
            public string err { get; set; }
            public string? message { get; set; }
            public List<AidsDTO> table { get; set; }

        }

    }
}
