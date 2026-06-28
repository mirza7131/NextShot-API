namespace HMIS.Aggregator.API.Models
{
    public class HrLogginUserDto
    {
        public int ProfileId { get; set; }
        public string? Userid { get; set; }
        public string? WorkingHFMISCode { get; set; }
        public string? WorkingHealthFacility_Id { get; set; }
        public bool IsPresent { get; set; } = false;

        public class ResponseHrLogginUserDto
        {
            public string? Message { get; set; }
            public bool Success { get; set; }
            public HrLogginUserDto Data { get; set; }

        }
    }
}