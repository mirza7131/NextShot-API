using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.MimsMainPharmacyMedicineDataDTO
{
    public class CreateOrEditMimsMainPharmacyMedicineDataDto
    {
        public Guid MimsMainPharmacyMedicineDataId { get; set; }
        public int? IndentId { get; set; }
        public int? BatchNo { get; set; }
        public string? FundingSource { get; set; }
        public int? MedicineId { get; set; }
        public int? HealthFacilityId { get; set; }
        public string? MedicineName { get; set; }
        public int? MedicineTypeId { get; set; }
        public string? MedicineTypeName { get; set; }
        public DateTime? MedicineMfgDate { get; set; }
        public DateTime? MedicineExpDate { get; set; }
        public decimal? AvailableQuantity { get; set; }
        public decimal? TotalQuantity { get; set; }
        public decimal? TotalDispatchQuantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsSMLMedicine { get; set; }
    }
}
