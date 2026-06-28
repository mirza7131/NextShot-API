using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.MimsMedicineData
{
    public class CreateOrEditMimsMedicineDataDto
    {
        public Guid MimsMedicineDataId { get; set; }
        public int? MedicineId { get; set; }
        public int? HealthFacilityId { get; set; }
        public string? MedicineName { get; set; }
        public int? MedicineTypeId { get; set; }
        public string? MedicineTypeName { get; set; }
        public int? WardId { get; set; }
        public string? WardName { get; set; }
        public decimal? AvailableQuantity { get; set; }
        public decimal? TotalQuantity { get; set; }
        public decimal? TotalDispatchQuantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsSMLMedicine { get; set; }
    }
}
