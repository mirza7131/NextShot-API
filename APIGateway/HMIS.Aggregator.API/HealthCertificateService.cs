using HMIS.Aggregator.API.Common.EndPoints;
using HMIS.Aggregator.API.Models.MIMS;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.Text;
using static CommonDTOs.HealthCertificateDTO.HealthCertificateDTO;
using static HMIS.Aggregator.API.Models.HrLogginUserDto;
using static HMIS.Aggregator.API.Models.MIMS.MedicineAvailableDto;
using static HMIS.Aggregator.API.Models.MIMS.MedicineDispenseDto;

namespace HMIS.Aggregator.API.Services
{
    public class HealthCertificateService
    {
        private readonly HttpClient _client;
        private readonly string baseUrl;
        public HealthCertificateService(HttpClient client, IWebHostEnvironment env)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            baseUrl = env.IsDevelopment() ? HealthCertificateEndPoint.HealthCertificateDevBaseUrl : HealthCertificateEndPoint.HealthCertificateProBaseUrl;
        }

        public async Task<ResponseHealthCertificateDTO> GetHealthCertificateCountForHMIS(string healthFacilityCode)
        {

            HttpClient client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, baseUrl + "api/HealthCertificateCountApi/GetHealthCertCountForHMIS?HFCode=" + healthFacilityCode);
            HttpResponseMessage httpResponse = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            var content = await httpResponse.Content.ReadAsStringAsync();

            ResponseHealthCertificateDTO jsonDeserialize = JsonConvert.DeserializeObject<ResponseHealthCertificateDTO>(content);

            return jsonDeserialize;
        }

    }
}
