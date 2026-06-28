
namespace HMIS.Aggregator.API.Models
{
    public class HrHealthFacilityDto
    {
        public int Id { get; set; }
        public string? HFMISCode { get; set; }
        public string? Name { get; set; }
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNo { get; set; }
        public string? DivisionName { get; set; }
        public string? DivisionCode { get; set; }
        public string? DistrictName { get; set; }
        public string? DistrictCode { get; set; }
        public string? TehsilCode { get; set; }
        public string? TehsilName { get; set; }
        public string? HFTypeCode { get; set; }

        public class ResponseHrHealthFacilityDto
        {
            public string? Message { get; set; }
            public bool Success { get; set; }
            public List<HrHealthFacilityDto> Data { get; set; }

        }

    }
}
