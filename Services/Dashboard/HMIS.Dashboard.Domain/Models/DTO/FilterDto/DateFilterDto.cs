using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Dashboard.Domain.Models.DTO.FilterDto
{
    public class DateFilterDto
    {
        public DateTime? StartDate { get; set; } 
        public DateTime? EndDate { get; set; }
    }
}
