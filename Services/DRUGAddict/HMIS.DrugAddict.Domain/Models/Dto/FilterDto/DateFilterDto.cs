using HMIS.DrugAddict.Domain.Models.Dto.PaginationDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.DrugAddict.Domain.Models.Dto.FilterDto
{
    public class DateFilterDto
    {
        public DateTime? StartDate { get; set; } 
        public DateTime? EndDate { get; set; }
    }
}
