using HMIS.Aggregator.API.Common.EndPoints;
using HMIS.Aggregator.API.Models.MIMS;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.Text;
using static CommonDTOs.HealthCouncilDTO.HealthCouncilDTO;

namespace HMIS.Aggregator.API.Services
{
    public class HealthCouncilService
    {
        private readonly HttpClient _client;
        private readonly string baseUrl;
        public HealthCouncilService(HttpClient client, IWebHostEnvironment env)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            baseUrl = env.IsDevelopment() ? HealthWatchEndPoint.HealthWatchDevBaseUrl : HealthWatchEndPoint.HealthWatchProBaseUrl;
        }

        public async Task<ResponseHealthCouncilDTO> GetHealthCouncilCountForHMIS(string healthFacilityCode)
        {

            HttpClient client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, baseUrl + "api/HealthConcil/GetHealthConcilCountForHMIS?HFCode=" + healthFacilityCode);
            HttpResponseMessage httpResponse = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            var content = await httpResponse.Content.ReadAsStringAsync();

            ResponseHealthCouncilDTO jsonDeserialize = JsonConvert.DeserializeObject<ResponseHealthCouncilDTO>(content);

            return jsonDeserialize;
        }

    }
}
