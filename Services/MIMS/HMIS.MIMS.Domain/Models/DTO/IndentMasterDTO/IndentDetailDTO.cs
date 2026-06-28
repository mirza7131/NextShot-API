using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.MIMS.Domain.Models.DTO.IndentMasterDTO
{
    public class IndentDetailDTO
    {
        public int? IndentId { get; set; }
        public int? MedicineId { get; set; }
        public string? MedicineName { get; set; }
        public int? MedicineTypeId { get; set; }
        public string? MedicineTypeName { get; set; }
        public string? BatchNo { get; set; }
        public bool? IsSMLMedicine { get; set; }
        public DateTime? ExpDate { get; set; }
        public DateTime? MfgDate { get; set; }
        public int? FundingSourceId { get; set; }
        public string? FundingSourceName { get; set; }
        public decimal? PricePerItem { get; set; }
        public double? AvailableQuantity { get; set; }
        public int? HealthFacilityId { get; set; }
        public Guid? CreatedBy { get; set; }
    }


    public class ResponseIndentDetailDTO
    {
        public string? Message { get; set; }
        public bool Status { get; set; }
        public bool Count { get; set; }

        public bool Success { get; set; }
        public string? IndentIdList { get; set; }
        
        public List<IndentDetailDTO> Data { get; set; } = new List<IndentDetailDTO>();

    }

    public class ResponseIndentDTO
    {
        public string? IndentIdList { get; set; }
        public List<IndentDetailDTO> Data { get; set; } = new List<IndentDetailDTO>();

    }


    public class MedicineIndentDetailDTO
    {
        public int? IndentId { get; set; }
        public int? MedicineId { get; set; }
        public string? MedicineName { get; set; }
        public int? MedicineTypeId { get; set; }
        public string? MedicineTypeName { get; set; }
        public bool? IsSMLMedicine { get; set; }
        public DateTime? ExpDate { get; set; }
        public DateTime? MfgDate { get; set; }
        public int? FundingSourceId { get; set; }
        public string? FundingSourceName { get; set; }
        public decimal? PricePerItem { get; set; }
        public double? AvailableQuantity { get; set; }
        public int? HealthFacilityId { get; set; }
        public Guid? CreatedBy { get; set; }
    }

    public class ResponseCreateIndent {
        public bool Response { get; set; }

    }

    public class ResponseSPDispenceMedicine
    {
        public bool Response { get; set; }
        public int AvailableQuantity { get; set; }

    }
}
