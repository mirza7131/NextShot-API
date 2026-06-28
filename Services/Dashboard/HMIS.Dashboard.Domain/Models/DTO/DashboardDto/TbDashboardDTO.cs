using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Dashboard.Domain.Models.DTO.DashboardDto
{
    public class TbDashboardDTO
    {
 
            public int? Count { get; set; }
            public int? Index { get; set; }
            public string? Color{ get; set; }
            public string? StripColor{ get; set; }
            public string? Icon { get; set; }
            public string? Title { get; set; }
            public string? TestName { get; set; } 
            public string? Childs { get; set; }
            public int IsChild { get; set; }

    
    }



    public class DashboardDTO
    {

        public int? Count { get; set; }
        public int? Index { get; set; }
        public string? Color { get; set; }
        public string? StripColor { get; set; }
        public string? Icon { get; set; }
        public string? Title { get; set; }
        public string? Name { get; set; }
        public string? Childs { get; set; }
        public int IsChild { get; set; }


    }
}
