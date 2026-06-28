using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.PatientPrescriptionDto
{
    public class CreateOrEditPatientPrescriptionDto
    {
        public Guid? PatientPrescriptionId { get; set; }
        public Guid? PatientDiagnoseId { get; set; }
        public Guid? PatientId { get; set; }
        public Guid? PatientVisitId { get; set; }
        public Guid? MedicineAdvisedRequisitionId { get; set; }
        public Guid? MedicineAdvisedId { get; set; }
        public int? MedicineId { get; set; }
        public string? MedicineName { get; set; }
        public Guid? MedicineTypeId { get; set; }
        public int? Days { get; set; }
        public Guid? DoseProfileId { get; set; }
        public Guid? DoseTimeProfileId { get; set; }
        public Guid? PrescribedBy { get; set; }
        public int? Quantity { get; set; }
        public int? AvailableQuantity { get; set; }
        public int? QuantityPrescribed { get; set; }
        
        public decimal? UnitPrice { get; set; }
        public string? MedicineDose { get; set; }
        public string? MedicineRoute { get; set; }
        public string? MedicineFrequency { get; set; }
        public string? MedicineInstruction { get; set; }
        public string? MedicineDuration { get; set; }
        public string? BatchNo { get; set; }
        public bool IsActive { get; set; }
        public bool? IsSMLMedicine { get; set; } = false;
        public Guid? MedicineResourceProfileId { get; set; }
        public Guid? MedicineTypeProfileId { get; set; }
    }
}
