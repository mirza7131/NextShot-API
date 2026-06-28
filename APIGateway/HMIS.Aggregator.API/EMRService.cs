using CommonDTOs.ResponseDTO;
using HMIS.Aggregator.API.Common.EndPoints;
using HMIS.Aggregator.API.Models.Dto.EMR;
using JWTAuthentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Aggregator.API
{
    public class EMRService
    {
        private readonly HttpClient _client;
        private readonly TokenService _tokenService;
        private readonly string emrBaseUrl;

        public EMRService(
            HttpClient client, TokenService tokenService, IWebHostEnvironment env)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _tokenService = tokenService;
            //this.emrBaseUrl = emrBaseUrl;
            emrBaseUrl = env.IsDevelopment() ? EMREndPoint.EMRDevBaseUrl : EMREndPoint.EMRProBaseUrl;
        }

        public async Task<ResponseDTO> UpdateSampleCollectedStatus(UpdateLabTestSampleCollectedDto input)
        {
            //var token = _tokenService.GetAccessToken();

            var serializedParam = JsonConvert.SerializeObject(input);
            var stringContent = new StringContent(serializedParam, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var httpResponse = await client.PostAsync(emrBaseUrl + "api/PatientTestSample/UpdateSampleReceivingData", stringContent);

            var content = await httpResponse.Content.ReadAsStringAsync();

            var jsonDeserialize = JsonConvert.DeserializeObject<ResponseDTO>(content);

            return jsonDeserialize;
        }

        //public async Task<ResponseDTO> UpdateSampleRejectedStatus(UpdateLabTestSampleCollectedDto input)
        //{
        //    //var token = _tokenService.GetAccessToken();

        //    var serializedParam = JsonConvert.SerializeObject(input);
        //    var stringContent = new StringContent(serializedParam, Encoding.UTF8, "application/json");
        //    HttpClient client = new HttpClient();
        //    //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //    var httpResponse = await client.PostAsync(emrBaseUrl + "api/PatientTestSample/UpdateSampleReceivingData", stringContent);

        //    var content = await httpResponse.Content.ReadAsStringAsync();

        //    var jsonDeserialize = JsonConvert.DeserializeObject<ResponseDTO>(content);

        //    return jsonDeserialize;
        //}

        public async Task<ResponseDTO> UpdateResultStatus(UpdateLabTestSampleCollectedDto input)
        {
            //var token = _tokenService.GetAccessToken();

            var serializedParam = JsonConvert.SerializeObject(input);
            var stringContent = new StringContent(serializedParam, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var httpResponse = await client.PostAsync(emrBaseUrl + "api/PatientTestSample/UpdateSampleResultData", stringContent);

            var content = await httpResponse.Content.ReadAsStringAsync();

            var jsonDeserialize = JsonConvert.DeserializeObject<ResponseDTO>(content);

            return jsonDeserialize;
        }

        public async Task<ResponseDTO> EMRRequestForSuspectedPatient(EMRRequestForSuspectedPatientDto input)
        {
            //var token = _tokenService.GetAccessToken();

            var serializedParam = JsonConvert.SerializeObject(input);
            var stringContent = new StringContent(serializedParam, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var httpResponse = await client.PostAsync(emrBaseUrl + "api/CaseInvestigationSyncApi/AddorUpdateSuspectedPatient", stringContent);

            var content = await httpResponse.Content.ReadAsStringAsync();

            var jsonDeserialize = JsonConvert.DeserializeObject<ResponseDTO>(content);

            return jsonDeserialize;
        }
    }
}
