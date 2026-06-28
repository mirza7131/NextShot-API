
namespace HMIS.Aggregator.API.Models.MIMS
{
    public class MedicineAvailableDto
    {
        public int MedicineId { get; set; }
        public string? MedicineName { get; set; }
        public int WardId { get; set; }
        public string? WardName { get; set; }
        public int MedicineTypeId { get; set; }
        public string? MedicineTypeName { get; set; }
        public string? AvailableQuantity { get; set; }
        public string? BatchNo { get; set; }
        public decimal PricePerItem { get; set; }
        public decimal? UnitPrice { get; set; }

        public bool? IsSMLMedicine { get; set; }

        public class ResponseMedicineAvailableDto
        {
            public int Count { get; set; } = 0;
            public string? Message { get; set; }
            public bool Success { get; set; }
            public List<MedicineAvailableDto> Data { get; set; }
            public string? IndentIdList { get; set; }
            public bool Status { get; set; }
        }

    }
    public class LastUpdatedDateMIMsDTO
    {
        public string? Message { get; set; }
        public bool Status { get; set; }
        public string? Data { get; set; }

    }
}