using HMIS.Aggregator.API.Common.EndPoints;
using HMIS.Aggregator.API.Models.ResponseModels;
using JWTAuthentication;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Aggregator.API
{
    public class PatientDiagnoseJsonService
    {
        private readonly HttpClient _client;
        private readonly string baseUrl;
        private readonly TokenService _tokenService;
        public PatientDiagnoseJsonService(HttpClient client, IWebHostEnvironment env, TokenService tokenService)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _tokenService = tokenService;
            baseUrl = env.IsDevelopment() ? PatientDiagnoseEndPoint.ProfileDevBaseUrl : PatientDiagnoseEndPoint.ProfileProBaseUrl;
        }

        public async Task<HmisResponseDTO> GetJsonObj(Guid? visitId, string formType, Guid? diagnoseId)
        {
            var token = _tokenService.GetAccessToken();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string url = baseUrl + "api/PatientDiagnose/GetJsonObj?visitId=" + visitId + "&formType=" + formType + "&diagnoseId=" + diagnoseId;
            var httpResponse = await client.GetAsync(url);

            var content = await httpResponse.Content.ReadAsStringAsync();

            var jsonDeserialize = JsonConvert.DeserializeObject<HmisResponseDTO>(content);

            return jsonDeserialize;
        }
    }
}
