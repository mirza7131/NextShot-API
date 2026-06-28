using CommonDTOs.ResponseDTO;
using HMIS.Aggregator.API.Common.EndPoints;
using HMIS.Aggregator.API.Models.ResponseModels;
using JWTAuthentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Aggregator.API
{
    public class PatientCommonService
    {
        private readonly HttpClient _client;
        private readonly TokenService _tokenService;
        private readonly string patientBaseUrl;

        public PatientCommonService(
            HttpClient client,
            TokenService tokenService,
            IWebHostEnvironment env
        )
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _tokenService = tokenService;
            patientBaseUrl = env.IsDevelopment() ? PatientEndPoint.PatientDevBaseUrl : PatientEndPoint.PatientProBaseUrl;
        }

        //public async Task<ResponseDTO> CreateOrEditPatient(CreateOrEditPatientExternallyDto input)
        //{
        //    var token = _tokenService.GetAccessToken();

        //    var serializedParam = JsonConvert.SerializeObject(input);
        //    var stringContent = new StringContent(serializedParam, Encoding.UTF8, "application/json");
        //    HttpClient client = new HttpClient();
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //    var httpResponse = await client.PostAsync(patientBaseUrl + "api/Patient/CreateOrEditWithVisit", stringContent);

        //    var content = await httpResponse.Content.ReadAsStringAsync();

        //    var jsonDeserialize = JsonConvert.DeserializeObject<ResponseDTO>(content);

        //    return jsonDeserialize;
        //}

        public async Task<ResponseProfileDTO> GetProfileByShortName(string ShortName)
        {
            var token = _tokenService.GetAccessToken();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var httpResponse = await client.GetAsync(patientBaseUrl + "api/MimsMedicineData/ImportDataFromMims?shortName=" + ShortName);

            var content = await httpResponse.Content.ReadAsStringAsync();

            var jsonDeserialize = JsonConvert.DeserializeObject<ResponseProfileDTO>(content);

            return jsonDeserialize;
        }
    }
}
