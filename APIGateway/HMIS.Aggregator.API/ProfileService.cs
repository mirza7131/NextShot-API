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

namespace HMIS.Aggregator.API
{
    public class ProfileService
    {
        /// <summary>
        /// GetProfileByShortName method get ShortName as parameter and return its Profile
        /// </summary>

        private readonly HttpClient _client;
        private readonly string baseUrl;
        private readonly TokenService _tokenService;
        public ProfileService(HttpClient client, IWebHostEnvironment env, TokenService tokenService)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _tokenService = tokenService;
            baseUrl = env.IsDevelopment() ? ProfileEndPoint.ProfileDevBaseUrl : ProfileEndPoint.ProfileProBaseUrl;
        }

        public async Task<ResponseProfileDTO> GetProfileByShortName(string ShortName)
        {
            var token = _tokenService.GetAccessToken();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var httpResponse = await client.GetAsync(baseUrl + "api/Profile/GetProfileByShortName?shortName=" + ShortName);

            var content = await httpResponse.Content.ReadAsStringAsync();

            var jsonDeserialize = JsonConvert.DeserializeObject<ResponseProfileDTO>(content);

            return jsonDeserialize;
        }
    }
}
