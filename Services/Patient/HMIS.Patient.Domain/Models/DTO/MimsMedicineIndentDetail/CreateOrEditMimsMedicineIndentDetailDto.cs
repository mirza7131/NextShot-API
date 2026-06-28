using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.MimsMedicineIndentDetail
{
    public class CreateOrEditMimsMedicineIndentDetailDto
    {
        public Guid MimsMedicineIndentDetailId { get; set; }
        public long? IndentId { get; set; }
        public int? MedicineId { get; set; }
        public string? MedicineName { get; set; }
        public int? MedicineTypeId { get; set; }
        public string? MedicineTypeName { get; set; }
        public int? WardId { get; set; }
        public string? WardName { get; set; }
        public decimal? AvailableQuantity { get; set; }
        public int? HealthFacilityId { get; set; }
        public decimal? PricePerItem { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public Guid? DeletedBy { get; set; }
        public byte? ActionTypeId { get; set; }

    }

}
