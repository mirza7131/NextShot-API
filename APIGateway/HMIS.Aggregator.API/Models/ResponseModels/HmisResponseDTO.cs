
using CommonMessages;
using System.Net;

namespace HMIS.Aggregator.API.Models.ResponseModels
{
    public class HmisResponseDTO
    {
        public HttpStatusCode statusCode { get; set; }
        public bool status { get; set; } = true;
        public string message { get; set; } = CommonMessageConstant.Read;
        public Object? data { get; set; }

    }
}
