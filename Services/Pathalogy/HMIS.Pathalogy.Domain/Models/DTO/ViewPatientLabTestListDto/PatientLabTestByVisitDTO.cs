using HMIS.Pathalogy.Domain.Models.DTO.PaginationDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Pathalogy.Domain.Models.DTO.ViewPatientLabTestListDto
{
    public class PatientLabTestByVisitDTO:PagerDto
    {
        public Guid? PatientVisitId { get; set; }
        public int? Stage { get; set; }
    }
}
