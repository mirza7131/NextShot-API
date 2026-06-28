
namespace HMIS.Aggregator.API.Models.ResponseModels
{
    public class HrResponseDTO
    {
        public string? Message { get; set; }
        public bool Success { get; set; }
        public object? Data { get; set; }
    }
}
