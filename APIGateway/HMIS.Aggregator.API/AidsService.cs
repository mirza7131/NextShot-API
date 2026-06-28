using HMIS.Aggregator.API.Common.EndPoints;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using static CommonDTOs.AidsDTO.AidsDTO;
using static CommonDTOs.MedicoLegalDTO.MedicoLegalDTO;

namespace HMIS.Aggregator.API.Services
{
    public class AidsService
    {
        private readonly HttpClient _client;
        private readonly string baseUrl;
        public AidsService(HttpClient client, IWebHostEnvironment env)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            baseUrl = env.IsDevelopment() ? AidsEndPoint.AidsDevBaseUrl : AidsEndPoint.AidsProBaseUrl;
        }

        public async Task<ResponseAidsDTO> GetAidsCountForHMISByDateRange(DateTime? startDate, DateTime? endDate)
        {

            HttpClient client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, baseUrl + "api/AIDSApi/GetAIDSDATA?fromdate=" + startDate?.Date.ToString("yyyy-MM-dd") + "&todate=" + endDate?.Date.ToString("yyyy-MM-dd"));
            HttpResponseMessage httpResponse = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            var content = await httpResponse.Content.ReadAsStringAsync();

            ResponseAidsDTO jsonDeserialize = JsonConvert.DeserializeObject<ResponseAidsDTO>(content);

            return jsonDeserialize;
        }

    }
}
