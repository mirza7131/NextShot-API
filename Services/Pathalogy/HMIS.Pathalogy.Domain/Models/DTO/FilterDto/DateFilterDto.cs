using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Pathalogy.Domain.Models.DTO.FilterDto
{
    public class DateFilterDto
    {
        public DateTime? StartDate { get; set; } 
        public DateTime? EndDate { get; set; }
    }
}
