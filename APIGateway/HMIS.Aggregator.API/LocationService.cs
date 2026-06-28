using CommonMessages;
using HMIS.Aggregator.API.Common.EndPoints;
using HMIS.Aggregator.API.Models.Dto.Auth;
using HMIS.Aggregator.API.Models.Dto.PaginationDto;
using HMIS.Aggregator.API.Models.ResponseModels;
using JWTAuthentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Aggregator.API
{
    public class LocationService
    {
        private readonly HttpClient _client;
        private readonly TokenService _tokenService;
        private readonly string patientBaseUrl;

        public LocationService(
            HttpClient client,
            TokenService tokenService
        )
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _tokenService = tokenService;
        }

        public async Task<HmisLocationResponseDTO> GetAllLocations(string baseUrl)
        {
            var token = _tokenService.GetAccessToken();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var httpResponse = await client.GetAsync(baseUrl + "api/Province/GetAllLocations");

            var content = await httpResponse.Content.ReadAsStringAsync();

            var jsonDeserialize = JsonConvert.DeserializeObject<HmisLocationResponseDTO>(content);

            return jsonDeserialize;
        }

        public async Task<HmisHealthFacilityPaginationResponseDTO> GetAllHealthFacilityWithPagination(string baseUrl, int? divisionId,int? districtId, int? tehsilId,int? healthFacilityId, int pageSize = 10,int pageNumber = 1)
        {
            var token = _tokenService.GetAccessToken();


            var queryParameters = new Dictionary<string, string>
            {
                { "DivisionId", Convert.ToString(divisionId) },
                { "DistrictId", Convert.ToString(districtId) },
                { "TehsilId", Convert.ToString(tehsilId) },
                { "HealthFacilityId", Convert.ToString(healthFacilityId) },
                { "PageSize", Convert.ToString(pageSize) },
                { "PageNumber", Convert.ToString(pageNumber)  }
            };

            var dictFormUrlEncoded = new FormUrlEncodedContent(queryParameters);
            var queryString = await dictFormUrlEncoded.ReadAsStringAsync();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var httpResponse = await client.GetAsync($"{baseUrl}api/HealthFacility/GetAllWithPagination?{queryString}");

            var content = await httpResponse.Content.ReadAsStringAsync();

            var jsonDeserialize = JsonConvert.DeserializeObject<HmisHealthFacilityPaginationResponseDTO>(content);

            return jsonDeserialize;
        }

        public async Task<HmisHealthFacilityResponseDTO> GetAllHealthFacilities(string baseUrl, int? divisionId, int? districtId, int? tehsilId, int? healthFacilityId)
        {
            var token = _tokenService.GetAccessToken();

            var queryParameters = new Dictionary<string, string>
            {
                { "DivisionId", Convert.ToString(divisionId) },
                { "DistrictId", Convert.ToString(districtId) },
                { "TehsilId", Convert.ToString(tehsilId) },
                { "HealthFacilityId", Convert.ToString(healthFacilityId) }
            };

            var dictFormUrlEncoded = new FormUrlEncodedContent(queryParameters);
            var queryString = await dictFormUrlEncoded.ReadAsStringAsync();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var httpResponse = await client.GetAsync($"{baseUrl}api/HealthFacility/GetAllHealthFacility?{queryString}");

            var content = await httpResponse.Content.ReadAsStringAsync();

            var jsonDeserialize = JsonConvert.DeserializeObject<HmisHealthFacilityResponseDTO>(content);

            return jsonDeserialize;
        }

        public async Task<HmisHealthFacilityTypeResponseDTO> GetAllHealthFacilityTypes(string baseUrl)
        {
            var token = _tokenService.GetAccessToken();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var httpResponse = await client.GetAsync(baseUrl + "api/HealthFacilityType/GetAll");

            var content = await httpResponse.Content.ReadAsStringAsync();

            var jsonDeserialize = JsonConvert.DeserializeObject<HmisHealthFacilityTypeResponseDTO>(content);

            return jsonDeserialize;
        }


    }
}
