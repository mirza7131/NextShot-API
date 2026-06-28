
namespace HMIS.Aggregator.API.Models.MIMS
{
    public class UnSyncIndentDto
    {
        public int IndentId { get; set; }
        public string? WardName { get; set; }
        public int WardId { get; set; }
        public DateTime? CreationDate { get; set; }
        public bool IsSync { get; set; }
        
        public class ResponseUnSyncIndentDto
        {
            public string? Message { get; set; }
            public bool Status { get; set; }
            public List<UnSyncIndentDto> Data { get; set; }

        }
    }
}
