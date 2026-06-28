using HMIS.Patient.Domain.Models.DTO.PaginationDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.MedicineAdvisedDto
{
    public class FilterMedicineAdvisedDto : PagerDto
    {
        public string? User { get; set; }
        public string? FullName { get; set; }
        public string? MobileNo { get; set; }
        public string? Cnic { get; set; }
        public string? Mrno { get; set; }
    }
}
