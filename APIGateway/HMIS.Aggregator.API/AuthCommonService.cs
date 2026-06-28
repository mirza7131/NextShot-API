using AppCommonMethods;
using HMIS.Aggregator.API.Common.EndPoints;
using HMIS.Aggregator.API.Models.Dto.DataSyncToOffline;
using HMIS.Aggregator.API.Models.Dto.DataSyncUtilityLog;
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
using static HMIS.Aggregator.API.Models.MIMS.MedicineDispenseDto;

namespace HMIS.Aggregator.API
{
    public class AuthCommonService
    {
        private readonly HttpClient _client;
        private readonly string baseUrl;
        private readonly TokenService _tokenService;
        public AuthCommonService(HttpClient client, IWebHostEnvironment env, TokenService tokenService)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _tokenService = tokenService;
            baseUrl = env.IsDevelopment() ? AuthEndPoints.DevBaseUrl : AuthEndPoints.ProBaseUrl;
        }


        //public async Task<ResponseProfileDTO> GetProfileByShortName(string ShortName)
        //{
        //    var token = _tokenService.GetAccessToken();

        //    HttpClient client = new HttpClient();
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //    var httpResponse = await client.GetAsync(baseUrl + "api/Profile/CreateOrEdit?shortName=" + ShortName);

        //    var content = await httpResponse.Content.ReadAsStringAsync();

        //    var jsonDeserialize = JsonConvert.DeserializeObject<ResponseProfileDTO>(content);

        //    return jsonDeserialize;
        //}

        public async Task<bool> CreateDataSyncUtilityLog(CreateDataSyncUtilityLog input)
        {
            //var token = _tokenService.GetAccessToken();

            var serializedParam = JsonConvert.SerializeObject(input);
            var stringContent = new StringContent(serializedParam, Encoding.UTF8, "application/json");
            //HttpClient client = new HttpClient();

            //_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var httpResponse = await _client.PostAsync(baseUrl + "api/DataSyncUtilityLog/CreateDataSyncLogInfo", stringContent);
            await httpResponse.Content.ReadAsStringAsync();

            if(!AppCommonMethod.IsNullObject(httpResponse.IsSuccessStatusCode) && httpResponse.IsSuccessStatusCode)
                return true;
            else
                return false;
            //var jsonDeserialize = JsonConvert.DeserializeObject<ResponseMedicineDispenseDto>(content);
            //string jsonDeserialize = JsonConvert.DeserializeObject<string>(content);
        }

        public async Task<string[]> GetDataToSync(FilterSyncToOfflineDto input)
        {

            //var token = _tokenService.GetAccessToken();

            var serializedParam = JsonConvert.SerializeObject(input);
            var stringContent = new StringContent(serializedParam, Encoding.UTF8, "application/json");
            //HttpClient client = new HttpClient();

            //_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var httpResponse = await _client.PostAsync(baseUrl + "api/DataSyncToOffline/GetDataToSync", stringContent);
            var content = await httpResponse.Content.ReadAsStringAsync();

            //if (!AppCommonMethod.IsNullObject(httpResponse.IsSuccessStatusCode) && httpResponse.IsSuccessStatusCode)
            //    return true;
            //else
            //    return false;
            var jsonDeserialize = JsonConvert.DeserializeObject<ResponseOfflineSyncDto>(content);
            //string jsonDeserialize = JsonConvert.DeserializeObject<string>(content);
            return jsonDeserialize!.Data.JsonData;
        }
    }
}
