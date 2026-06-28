using HMIS.Pathalogy.Domain.Models.DTO.PaginationDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Pathalogy.Domain.Models.DTO.ViewRiderLabTestListDto
{
    public class RiderPatientLabTestListDto : PagerDto
    {
        public int? Status { get; set; } = 1;
    }
}
