
namespace HMIS.Aggregator.API.Models.MIMS
{
    public class MedicineDispenseDto
    {
        public int MedId { get; set; }
        public string? BatchNo { get; set; }
        public int Quantity { get; set; }
        public int? PortalId { get; set; } = 1; // default Portal Id 
        public string hfmisCode { get; set; }
        public int? WardId { get; set; }
        public int HealthFacilityId { get; set; } // used for HMIS internal
        public string? Reason { get; set; }
        public bool MIMSDispatched { get; set; }
        
        public class ResponseMedicineDispenseDto
        {
            public string? Message { get; set; }
            public bool Status { get; set; }
            public List<MedicineData> Data { get; set; }

        }

        public class MedicineData
        {

            public int MedId { get; set; }
            public string? Reason { get; set; }
        }
    }
}
