using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.MedicineDispatchDto
{
    public class ViewMedicineDispatchListDto
    {
        public Guid MedicineDispatchId { get; set; }

        public Guid? PatientId { get; set; }

        public Guid PatientVisitId { get; set; }

        public int MedicineLookupId { get; set; }

        public int? QuantityPrescribed { get; set; }

        public int? QuantityDispatch { get; set; }

        public Guid? Pharmacist { get; set; }

        public bool IsActive { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? FullName { get; set; }
        public string? Cnic { get; set; }

        public string? MobileNo { get; set; }
        public string? Mrno { get; set; }
        public DateTime? CreatedOn { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public string? UpdatedBy { get; set; }
        public DateTime? VisitDate { get; set; }

    }
}
