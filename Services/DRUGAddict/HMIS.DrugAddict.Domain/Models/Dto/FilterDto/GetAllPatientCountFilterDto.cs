using HMIS.DrugAddict.Domain.Models.Dto.PaginationDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.DrugAddict.Domain.Models.Dto.FilterDto
{
    public class GetAllPatientCountFilterDto:PagerDto
    {
        //public string? searchTerm { get; set; }

        //public int? DistrictId { get; set; }
        //public int? DivisionId { get; set; }
        //public DateTime? StartDate { get; set; }
        //public DateTime? EndDate { get; set; }
        public string? searchTerm { get; set; }
    }
}
