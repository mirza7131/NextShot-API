using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Dashboard.Domain.Models.DTO.MedicineAdvisedRequisitionDto
{
    public class ViewGetAllMedicineAdvisedRequisitionDto
    {
        public Guid MedicineAdvisedRequisitionId { get; set; }
        public Guid PatientId { get; set; }
        public Guid PatientDiagnoseId { get; set; }
        public Guid PatientVisitId { get; set; }

        public bool? IsActive { get; set; }
        public bool? IsSelf { get; set; }
        public byte? ActionTypeId { get; set; }
        public string? RequsitionBy { get; set; }

        public string? RequsitionFor { get; set; }

        public DateTime? RequsitionOn { get; set; }

        public string Status { get; set; } = null!;
    }
}
