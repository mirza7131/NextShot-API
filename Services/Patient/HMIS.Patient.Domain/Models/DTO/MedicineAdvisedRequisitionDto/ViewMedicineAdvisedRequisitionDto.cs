using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.MedicineAdvisedRequisitionDto
{
    public class ViewMedicineAdvisedRequisitionDto
    {
        public Guid MedicineAdvisedRequisitionId { get; set; }

        public Guid PatientId { get; set; }

        public Guid PatientOpenVisitId { get; set; }

        public Guid? PatientDiagnoseId { get; set; }

        public byte Status { get; set; }

        public bool IsActive { get; set; }

    }
}
