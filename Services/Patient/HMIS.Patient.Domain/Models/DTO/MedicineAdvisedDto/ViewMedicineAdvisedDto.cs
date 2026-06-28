using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.MedicineAdvisedDto
{
    public class ViewMedicineAdvisedDto
    {
        public Guid MedicineAdvisedId { get; set; }

        public Guid? PatientDiagnoseId { get; set; }

        public Guid PatientId { get; set; }

        public Guid PatientVisitId { get; set; }

        public int MedicineId { get; set; }

        public decimal? UnitPrice { get; set; }

        public Guid? MedicineTypeProfileId { get; set; }

        public int? MimsWardId { get; set; }

        public int? QuantityPrescribed { get; set; }
        public int? QuantityRequisition { get; set; }
        public int? QuantityDispatch { get; set; }
        public int? AvailableQuantity { get; set; }
        public string? MedicineName { get; set; }

        public string? BatchNo { get; set; }

        public int? Days { get; set; }

        public string? MedicineDose { get; set; }

        public string? MedicineRoute { get; set; }

        public string? MedicineFrequency { get; set; }

        public string? MedicineInstruction { get; set; }

        public string? MedicineDuration { get; set; }
        public bool IsActive { get; set; }
        public bool IsDiscontinue { get; set; }
        public bool IsSMLMedicine { get; set; }
        public bool? IsDispenced { get; set; } = false;
        public bool? IsRequested { get; set; } = false;
        public DateTime? MedicineStartDateTime { get; set; }
        public DateTime? DiscontinueDateTime { get; set; }
        public DateTime? CreatedOn { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public Guid? UpdatedBy { get; set; }

        public DateTime? DeletedOn { get; set; }

        public Guid? DeletedBy { get; set; }

        public long? UserLogId { get; set; }

        public byte ActionTypeId { get; set; }

    }
}
