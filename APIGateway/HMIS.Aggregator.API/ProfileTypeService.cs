using HMIS.Aggregator.API.Common.EndPoints;
using HMIS.Aggregator.API.Models;
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
//using static CommonDTOs.HealthCertificateDTO.HealthCertificateDTO;
using HMIS.Aggregator.API.Models;
using HMIS.Aggregator.API.Models.ResponseModels;

namespace HMIS.Aggregator.API
{
    public class ProfileTypeService
    {
        /// <summary>
        /// GetProfileByProfileType method get ShortName as parameter and return its all its profiles
        /// </summary>

        private readonly HttpClient _client;
        private readonly string baseUrl;
        private readonly TokenService _tokenService;
        public ProfileTypeService(HttpClient client, IWebHostEnvironment env,TokenService tokenService)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _tokenService = tokenService;
            baseUrl = env.IsDevelopment() ? ProfileTypeEndPoint.ProfileTypeDevBaseUrl : ProfileTypeEndPoint.ProfileTypeProBaseUrl;
        }


        /// <summary>
        /// This Method is used to get Profiles By Profile Type Short name 
        /// </summary>
        /// <param name="ShortName"> Profile Type Short Name  </param>
        /// <returns></returns>
        public async Task<ResponseProfileDTO> GetProfileByProfileType(string ShortName)
        {

            var token = _tokenService.GetAccessToken();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var httpResponse = await client.GetAsync(baseUrl + "api/Profile/GetProfileByProfileType?ProfileType=" + ShortName);

            var content = await httpResponse.Content.ReadAsStringAsync();

            var jsonDeserialize = JsonConvert.DeserializeObject<ResponseProfileDTO>(content);

            return jsonDeserialize;
        }
    }
}
