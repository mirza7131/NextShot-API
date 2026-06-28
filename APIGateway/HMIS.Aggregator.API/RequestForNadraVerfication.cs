using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonDTOs.ResponseDTO;
using HMIS.Aggregator.API.Common.EndPoints;
using JWTAuthentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using HMIS.Aggregator.API.Models;
using HMIS.Aggregator.API.Models.NADRA;
using System.Net.Http.Headers;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Components.Web;
using Azure;
using Newtonsoft.Json.Linq;

namespace HMIS.Aggregator.API
{
    public class RequestForNadraVerfication
    {
        private readonly HttpClient _client;
        private readonly TokenService _tokenService;
        private readonly string nadraVerificationBaseUrl;


        public RequestForNadraVerfication( HttpClient client, TokenService tokenService, IWebHostEnvironment env)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _tokenService = tokenService;
            nadraVerificationBaseUrl = env.IsDevelopment() ? NadraVerificationEndPoint.NadraVerificationDevBaseUrl : NadraVerificationEndPoint.NadraVerificationProBaseUrl;
        }

        public bool IsValidJson(string value)
        {
            try
            {
                var json = JContainer.Parse(value);
                return true;
            }
            catch
            {
                return false;
            }
        }


        public async Task<NadraResponseDTO> SentRequestForVerfication(NadraVerificationDTO input)
         {
            //var token = _tokenService.GetAccessToken();

            var serializedParam = JsonConvert.SerializeObject(input);
            var stringContent = new StringContent(serializedParam, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("XApiKey", "pgH7QzFHJx4w46fI~5Uzi4RvtTwlEXX");
            client.DefaultRequestHeaders.Add("XApiKey", "pgH7QzFHJx4w46fI~5Uzi4RvtTwlEXX");
            var httpResponse = await client.PostAsync(nadraVerificationBaseUrl + "api/NadraApi/SentRequestForVerfication", stringContent);

            var content = await httpResponse.Content.ReadAsStringAsync();
            if (IsValidJson(content))
            {
                var jsonDeserialize = JsonConvert.DeserializeObject<NadraResponseDTO>(content);

                return jsonDeserialize;
            }
            else
            {
                NadraResponseDTO nadraResponseDTO = new NadraResponseDTO();
                nadraResponseDTO.code = "401";
                nadraResponseDTO.message = content;
                return nadraResponseDTO;
            }

        }

    }
}
