using HMIS.Patient.Domain.Models.DTO.PaginationDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.MedicineAdvisedRequisitionDto
{
    public class FilterMedicineAdvisedRequisitionDto : PagerDto
    {
        public Guid? PatientId { get; set; }
        public Guid? PatientVisitId { get; set; }
        public Guid? MedicineAdvisedRequisitionId { get; set; }
    }
}
