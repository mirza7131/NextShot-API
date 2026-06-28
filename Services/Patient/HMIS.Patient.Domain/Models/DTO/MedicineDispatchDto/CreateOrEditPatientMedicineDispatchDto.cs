using HMIS.Patient.Domain.Models.DTO.PatientContactDetailsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.MedicineDispatchDto
{
    public class CreateOrEditPatientMedicineDispatchDto
    {
        public CreateOrEditPatientMedicineDispatchDto()
        {
            MedicineDispatches = new List<MedicineDispatchDto>();
            RiskFactors = new List<RiskFactorsDTO>();
        }

        public Guid PatientId { get; set; }

        public Guid PatientVisitId { get; set; }
        public bool? IsPatientHistory { get; set; }= false;

        public virtual ICollection<MedicineDispatchDto> MedicineDispatches { get; set; }
        public virtual ICollection<RiskFactorsDTO> RiskFactors { get; set; }

    }

    public class MedicineDispatchDto
    {
        public Guid? MedicineDispatchId { get; set; }
        public int MedicineId { get; set; }
        public string? MedicineName { get; set; }
        public string? BatchNo { get; set; }
        public int? QuantityPrescribed { get; set; }
        public int? QuantityDispatch { get; set; }
        public int? AvailableQuantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public Guid? Pharmacist { get; set; }
        public bool IsActive { get; set; }
        public bool MIMSDispatched { get; set; } = false;
        public string? Reason { get; set; }
        public Guid? PatientDiagnoseId { get; set; }
        public Guid? PatientPrescriptionId { get; set; }
        public int? WardId { get; set; }
        public Guid? MedicineResourceProfileId { get; set; }
        public Guid? MedicineTypeProfileId { get; set; }
        public decimal? TotalAmount { get; set; }
    }

    public class RiskFactorsDTO
    {
        public Guid RiskFactorProfileId { get; set; }
        public string Answer { get; set; }
    }

}
