using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Dashboard.Domain.Models.DTO.MedicineDispatchDto
{
    public class CreateOrEditMedicineDispatchDto
    {
        public Guid? MedicineDispatchId { get; set; }

        public Guid PatientId { get; set; }

        public Guid PatientVisitId { get; set; }

        public int MedicineLookupId { get; set; }

        public int? QuantityPrescribed { get; set; }

        public int? QuantityDispatch { get; set; }

        public Guid? Pharmacist { get; set; }

        public bool IsActive { get; set; }

    }
}
