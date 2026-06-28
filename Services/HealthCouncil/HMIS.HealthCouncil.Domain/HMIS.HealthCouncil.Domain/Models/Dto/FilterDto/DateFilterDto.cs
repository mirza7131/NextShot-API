using HMIS.HealthCouncil.Domain.Models.DTO.PaginationDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.FilterDto
{
    public class DateFilterDto:PagerDto
    {
        public DateTime? StartDate { get; set; } 
        public DateTime? EndDate { get; set; }
    }
}
