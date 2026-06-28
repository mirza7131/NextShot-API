using HMIS.Aggregator.API.Common.EndPoints;
using HMIS.Aggregator.API.Models.NADRA;
using HMIS.Aggregator.API.Models;
using JWTAuthentication;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HMIS.Aggregator.API.Models.MIMS.MedicineAvailableDto;
using Newtonsoft.Json.Linq;

namespace HMIS.Aggregator.API
{
    public class VerifiedPatientDataFromNADRAService
    {
        private readonly HttpClient _client;
        private readonly TokenService _tokenService;
        private readonly string VerifiedPatientDataFromNADRABaseUrl;


        public VerifiedPatientDataFromNADRAService(HttpClient client, TokenService tokenService, IWebHostEnvironment env)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _tokenService = tokenService;
            VerifiedPatientDataFromNADRABaseUrl = env.IsDevelopment() ? VerifiedPatientDataFromNADRA.VerifiedPatientDataFromNADRADevBaseUrl : VerifiedPatientDataFromNADRA.VerifiedPatientDataFromNADRAProBaseUrl;
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

        public async Task<VerifiedPatientDataFromNADRADTO> GetVerifiedPatientDataFromNADRA(string transactionId)
        {
            HttpClient client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, VerifiedPatientDataFromNADRABaseUrl + "api/NadraApi/GetPatientDetailFromNadra?id=" + transactionId);
            client.DefaultRequestHeaders.Add("XApiKey", "pgH7QzFHJx4w46fI~5Uzi4RvtTwlEXX");
            HttpResponseMessage httpResponse = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            var content = await httpResponse.Content.ReadAsStringAsync();

            if(IsValidJson(content))
            {
                VerifiedPatientDataFromNADRADTO jsonDeserialize = JsonConvert.DeserializeObject<VerifiedPatientDataFromNADRADTO>(content);

                return jsonDeserialize;
            }
            else
            {
                VerifiedPatientDataFromNADRADTO nadraResponseDTO = new VerifiedPatientDataFromNADRADTO();
                nadraResponseDTO.code = "401";
                nadraResponseDTO.message = content;
                return nadraResponseDTO;
            }
    
        }
    }
}
