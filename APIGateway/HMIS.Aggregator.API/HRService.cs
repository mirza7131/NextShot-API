using AppCommonMethods;
using CommonExceptionHandler;
using CommonMessages;
using HMIS.Aggregator.API.Common.EndPoints;
using HMIS.Aggregator.API.Extensions;
using HMIS.Aggregator.API.Models;
using HMIS.Aggregator.API.Models.Dto.Hr;
using HMIS.Aggregator.API.Models.ResponseModels;
using JWTAuthentication;
using Newtonsoft.Json;
using System;
using System.Net.Http.Headers;
using static HMIS.Aggregator.API.Models.HrHealthFacilityDto;
using static HMIS.Aggregator.API.Models.HrLogginUserDto;
using static HMIS.Aggregator.API.Models.HrUserDto;

namespace HMIS.Aggregator.API.Services
{
    public class HRService
    {
        private readonly HttpClient _client;
        private readonly TokenService _tokenService;
        public HRService(
            HttpClient client
        )
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<ResponseHrLogginUserDto> AuthenticateHrUser(string hrTokenUsername, string hrTokenPassword, string hrBaserUrl,string userName,string password)
        {
            var token = await GetHrAuthTokenAsync(hrTokenUsername,hrTokenPassword,hrBaserUrl);

            var data1 = new Dictionary<string, string>
            {
                {"UserName", userName},
                {"Password", password}
            };

            HttpClient client = new HttpClient();

            //var httpResponse = await client.PostAsync(hrBaserUrl + "api/Account/GetAuthenticationUsers?UserName=" + userName + "&Password=" + password, new FormUrlEncodedContent(data1));
            //var content = await httpResponse.Content.ReadAsStringAsync();

            //ResponseHrLogginUserDto jsonDeserialize = JsonConvert.DeserializeObject<ResponseHrLogginUserDto>(content);


            ResponseHrLogginUserDto jsonDeserialize = new ResponseHrLogginUserDto();
            jsonDeserialize.Success = true;
            jsonDeserialize.Message = "OK";
            HrLogginUserDto Data = new HrLogginUserDto();
            Data.IsPresent = true;
            jsonDeserialize.Data = Data; 


            return jsonDeserialize;

        }

        public async Task<ResponseHrUserDto> GetHrUserByCnic(string hrTokenUsername, string hrTokenPassword,string hrBaserUrl,string cnic)
        {
            var token = await GetHrAuthTokenAsync(hrTokenUsername, hrTokenPassword, hrBaserUrl);

            HttpClient client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, hrBaserUrl + "api/Profile/GetPersonProfileByCNIC?cnic=" + cnic);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage httpResponse = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            var content = await httpResponse.Content.ReadAsStringAsync();

            ResponseHrUserDto jsonDeserialize = JsonConvert.DeserializeObject<ResponseHrUserDto>(content);

            //// Check if Login User is not Super Admin and response User are of Same Health Facility
            //if (!AppCommonMethod.IsNullObject(jsonDeserialize.Data) && !TokenService.IsSuperAdmin() && !TokenService.IsSeniorDataProcessor() && jsonDeserialize.Data.HfmisCode != TokenService.GetUserHfCode())
            //    return null;

            //if (!AppCommonMethod.IsNullObject(jsonDeserialize.Data) && TokenService.IsSeniorDataProcessor() && jsonDeserialize.Data.WorkingDivisionCode != TokenService.GetUserHfCode())
            //    return null;


            //else
            return jsonDeserialize;
        }

        public async Task<ResponseListHrUserDto> GetHealthFacilityProfilesByHfmisCodeByDesignationId(string hrTokenUsername, string hrTokenPassword, string hrBaserUrl, string hfmisCode,int? designationId)
        {
            var token = await GetHrAuthTokenAsync(hrTokenUsername, hrTokenPassword, hrBaserUrl);

            HttpClient client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, hrBaserUrl + "api/HealthFacility/GetHealthFacilityProfiles?hfmisCode=" + hfmisCode + "&designationId=" + designationId);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage httpResponse = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            var content = await httpResponse.Content.ReadAsStringAsync();

            ResponseListHrUserDto listProfiles = JsonConvert.DeserializeObject<ResponseListHrUserDto>(content);

            return listProfiles;

        }

        public async Task<ResponseHrHealthFacilityDto> GetHrFacilities(string hrTokenUsername, string hrTokenPassword, string hrBaserUrl)
        {
            var token = await GetHrAuthTokenAsync(hrTokenUsername, hrTokenPassword, hrBaserUrl);

            HttpClient client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, hrBaserUrl + "api/HealthFacility/GetDetailHFList");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage httpResponse = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            var content = await httpResponse.Content.ReadAsStringAsync();

            ResponseHrHealthFacilityDto jsonDeserialize = JsonConvert.DeserializeObject<ResponseHrHealthFacilityDto>(content);

            return jsonDeserialize;

        }

        public async Task<string> GetHrAuthTokenAsync(string hrTokenUserName,string hrTokenPassword,string hrBaserUrl)
        {
            var data1 = new Dictionary<string, string>
            {
                {"username", hrTokenUserName},
                {"password", hrTokenPassword},
                {"grant_type", "password"}
            };

            var url = hrBaserUrl + "Token";

            using var client = new HttpClient();

            var httpResponse = await client.PostAsync(url, new FormUrlEncodedContent(data1));

            var token = await httpResponse.Content.ReadAsStringAsync();

            var jsonDeserialize = JsonConvert.DeserializeObject<Token>(token);

            return jsonDeserialize.access_token;


        }


        public async Task<List<HealthFacilityWardBedsDto>> GetHealthFacilityWardBeds(int? HrId, bool? IsActive)
        {
            //var token = await GetHrAuthTokenAsync();
            var paramHf = "";

            HttpClient client = new HttpClient();
            
            if (!AppCommonMethod.IsNullorZeroInt(HrId))
                paramHf = "healthFacilityName=" + HrId+"&";

            var request = new HttpRequestMessage(HttpMethod.Get, HREndPoint.HrPhisBaseUrl + "api/HealthFacilityWardBeds/GetHealthFacilityWardBeds?"+paramHf+ "isActive="+IsActive);
            
            //request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            
            HttpResponseMessage httpResponse = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            var content = await httpResponse.Content.ReadAsStringAsync();

            //var deserializeObject = JsonConvert.DeserializeObject(content);

            ResponseHfWardBedsDto jsonDeserialize = JsonConvert.DeserializeObject<ResponseHfWardBedsDto>(content);

            return jsonDeserialize.list;
        }

        //public async Task<Model> GetUserProfile(string userName)
        //{
        //    var response = await _client.GetAsync($"/api/hr/{userName}");
        //    return await response.ReadContentAs<Model>();
        //}
    }

    public class Token
    {
        public string access_token;
    }
}
