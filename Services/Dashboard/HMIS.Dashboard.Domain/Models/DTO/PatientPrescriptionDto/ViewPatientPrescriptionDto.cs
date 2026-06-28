using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Dashboard.Domain.Models.DTO.PatientPrescriptionDto
{
    public class ViewPatientPrescriptionDto
    {
        public Guid PatientPrescriptionId { get; set; }

        public Guid? PatientId { get; set; }

        public Guid? PatientVisitId { get; set; }

        public int? MedicineId { get; set; }

        public Guid? MedicineTypeId { get; set; }

        public int? Days { get; set; }

        public Guid? DoseProfileId { get; set; }

        public Guid? DoseTimeProfileId { get; set; }

        public Guid? PrescribedBy { get; set; }

        public int? Quantity { get; set; }

        public bool IsActive { get; set; }
    }
}