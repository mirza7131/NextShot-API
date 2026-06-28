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
    public class NCDService
    {
        private readonly HttpClient _client;
        private readonly string baseUrl;
        private readonly TokenService _tokenService;
        public NCDService(HttpClient client, IWebHostEnvironment env, TokenService tokenService)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _tokenService = tokenService;
            baseUrl = env.IsDevelopment() ? NCDEndPoint.ProfileDevBaseUrl : NCDEndPoint.ProfileProBaseUrl;
        }

        public async Task<HmisResponseDTO> GetPatientLastVisitIdForNcdAssesmentQuestions(Guid? PatinetId, string FormType)
        {
            var token = _tokenService.GetAccessToken();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string url = baseUrl + "api/AssessmentQa/GetPatientLastVisitIdForNcdAssesmentQuestions?PatientId=" + PatinetId + "&FormType=" + FormType;
            var httpResponse = await client.GetAsync(url);

            var content = await httpResponse.Content.ReadAsStringAsync();

            var jsonDeserialize = JsonConvert.DeserializeObject<HmisResponseDTO>(content);

            return jsonDeserialize;
        }
    }
}

