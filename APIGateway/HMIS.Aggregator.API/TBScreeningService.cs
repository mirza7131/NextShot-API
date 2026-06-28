using CommonDTOs.TBScreeningDTO;
using HMIS.Aggregator.API.Common.EndPoints;
using HMIS.Aggregator.API.Models.MIMS;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Text;

using System.Text.Json;
using static CommonDTOs.HealthCouncilDTO.HealthCouncilDTO;

using static HMIS.Aggregator.API.Models.MIMS.MedicineDispenseDto;

namespace HMIS.Aggregator.API.Services
{
    public class TBScreeningService
    {
        private readonly HttpClient _client;
        private readonly string baseUrl;
        public TBScreeningService(HttpClient client, IWebHostEnvironment env)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            baseUrl = env.IsDevelopment() ? TBScreeningEndPoint.TBScreeningDevBaseUrl : TBScreeningEndPoint.TBScreeningProBaseUrl;
        }

        public async Task<ResponseTBScreeningDto> GetEMRProgressReport(EMRProgressReportDto emrprogressReportDto)
        {

            HttpClient client = new HttpClient();
            var request = $"{baseUrl}api/TBScreeningApi/GetEMRProgressReport";
            var serializedParam = JsonConvert.SerializeObject(emrprogressReportDto);
            var stringContent = new StringContent(serializedParam, Encoding.UTF8, "application/json");
          

            var httpResponse = await client.PostAsync(request, stringContent);
            var content = await httpResponse.Content.ReadAsStringAsync();

            ResponseTBScreeningDto jsonDeserialize = JsonConvert.DeserializeObject<ResponseTBScreeningDto>(content);
            //string jsonDeserialize = JsonConvert.DeserializeObject<string>(content);

            return jsonDeserialize;




        }

    }
}
