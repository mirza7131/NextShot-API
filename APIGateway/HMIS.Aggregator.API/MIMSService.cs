using HMIS.Aggregator.API.Models.MIMS;
using HMIS.MIMS.Domain.Models.DTO.IndentMasterDTO;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System.Text;
using static HMIS.Aggregator.API.Models.MIMS.MedicineAvailableDto;
using static HMIS.Aggregator.API.Models.MIMS.MedicineDispenseDto;
using static HMIS.Aggregator.API.Models.MIMS.UnSyncIndentDto;
using static HMIS.MIMS.Domain.Models.DTO.IndentMasterDTO.UnsyncIndentListDTO;

namespace HMIS.Aggregator.API.Services
{
    public class MIMSService
    {
        private readonly HttpClient _client;
        public MIMSService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        /*======================================================================================================
            Get Medicine Stock List of a Particular store of a particular facility
            Parameters: basUrl, string healthFacilityCode, string mimsDepartmentId
        ======================================================================================================*/
        public async Task<ResponseMedicineAvailableDto> GetMedicineAvailableQuantityByHealthFacility(string basUrl, string healthFacilityCode, string mimsDepartmentId, bool offline = false)
        {
            HttpClient client = new HttpClient();
            string url = basUrl + "api/StockApi/MedicineAvailableQuantityByHF/" + healthFacilityCode + "/" + mimsDepartmentId + "/1/" + offline;
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            HttpResponseMessage httpResponse = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            var content = await httpResponse.Content.ReadAsStringAsync();

            ResponseMedicineAvailableDto jsonDeserialize = JsonConvert.DeserializeObject<ResponseMedicineAvailableDto>(content);

            return jsonDeserialize;
        }

        public async Task<ResponseMedicineAvailableDto> GetMedicineAvailableQuantityByHealthFacilityForDashboard(string basUrl, string healthFacilityCode, string mimsDepartmentId, bool offline = false)
        {
            HttpClient client = new HttpClient();
            string url = "https://mims.pshealthpunjab.gov.pk/api/StockApi/MedicineAvailableQuantityByHF/" + healthFacilityCode + "/" + mimsDepartmentId + "/1/" + offline;
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            //var request = new HttpRequestMessage(HttpMethod.Get, basUrl + "api/StockApi/MedicineAvailableQuantityByHF/" + healthFacilityCode + "/" + mimsDepartmentId);

            HttpResponseMessage httpResponse = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            var content = await httpResponse.Content.ReadAsStringAsync();

            ResponseMedicineAvailableDto jsonDeserialize = JsonConvert.DeserializeObject<ResponseMedicineAvailableDto>(content);

            return jsonDeserialize;
        }

        public async Task<LastUpdatedDateMIMsDTO> GetLastUpdatedDateOfMIMS(string basUrl, string healthFacilityCode)
        {
            HttpClient client = new HttpClient();
            string url = "https://mims.pshealthpunjab.gov.pk//api/StockApi/GetActivityStatusByHf/" + healthFacilityCode;
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            //var request = new HttpRequestMessage(HttpMethod.Get, basUrl + "api/StockApi/MedicineAvailableQuantityByHF/" + healthFacilityCode + "/" + mimsDepartmentId);

            HttpResponseMessage httpResponse = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            var content = await httpResponse.Content.ReadAsStringAsync();

            LastUpdatedDateMIMsDTO jsonDeserialize = JsonConvert.DeserializeObject<LastUpdatedDateMIMsDTO>(content);

            return jsonDeserialize;
        }

        /*======================================================================================================
            Medicine dispnese by health facility need to dispense from MIMS as well
            Parameters: string basUrl, List<MedicineDispenseDto> medicineDispenseDto
        ======================================================================================================*/
        public async Task<ResponseMedicineDispenseDto> MedicineDespenseByHealthFacility(string basUrl, List<MedicineDispenseDto> medicineDispenseDto)
        {
            var serializedParam = JsonConvert.SerializeObject(medicineDispenseDto);
            var stringContent = new StringContent(serializedParam, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();

            var httpResponse = await client.PostAsync(basUrl + "api/StockApi/MedDespenseByHF", stringContent);
            var content = await httpResponse.Content.ReadAsStringAsync();

            ResponseMedicineDispenseDto jsonDeserialize = JsonConvert.DeserializeObject<ResponseMedicineDispenseDto>(content);

            return jsonDeserialize;

        }

        public async Task<string> GetAuthTokenAsync(string tokenUserName, string tokenPassword, string baserUrl)
        {
            var data1 = new Dictionary<string, string>
            {
                {"username", tokenUserName},
                {"password", tokenPassword},
                {"grant_type", "password"}
            };

            var url = baserUrl + "Token";

            using var client = new HttpClient();

            var httpResponse = await client.PostAsync(url, new FormUrlEncodedContent(data1));

            var token = await httpResponse.Content.ReadAsStringAsync();

            var jsonDeserialize = JsonConvert.DeserializeObject<Token>(token);

            return jsonDeserialize.access_token;

        }

        /*==================================================================================================== 
            Update sync status(i.e. it is updated IN HMIS) of Medicine Indent on MIMS side
            Parameters: string basUrl, List<string> indentList
        ======================================================================================================*/
        public async Task<LastUpdatedDateMIMsDTO> UpdateIndentSyncStatus(string basUrl, List<string> indentList)
        {
            var serializedParam = JsonConvert.SerializeObject(indentList);
            var stringContent = new StringContent(serializedParam, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();

            var httpResponse = await client.PostAsync(basUrl + "api/StockApi/UpdateIndentSyncStatusForHMIS", stringContent);
            var content = await httpResponse.Content.ReadAsStringAsync();

            LastUpdatedDateMIMsDTO jsonDeserialize = JsonConvert.DeserializeObject<LastUpdatedDateMIMsDTO>(content);

            return jsonDeserialize;

        }

        /*==================================================================================================== 
            Get Unsync Medicine Indent from MIMS side(i.e. those indents which has sync status = false in MIMS)
            Parameters: string basUrl, string healthFacilityCode
         ======================================================================================================*/
        public async Task<ResponseUnSyncIndentDto> GetUnSyncMedicineIndentFromMims(string basUrl, string healthFacilityCode, int wardId = 0)
        {
            HttpClient client = new HttpClient();
            string url = basUrl + "api/StockApi/GetUnSyncIndentListForHMIS/" + healthFacilityCode + "/" + wardId;
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            HttpResponseMessage httpResponse = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            var content = await httpResponse.Content.ReadAsStringAsync();

            ResponseUnSyncIndentDto jsonDeserialize = JsonConvert.DeserializeObject<ResponseUnSyncIndentDto>(content);

            return jsonDeserialize;
        }

        /*==================================================================================================== 
           Get Medicine Indent Detail By Indent Id
           Parameters: string basUrl, string healthFacilityCode
        ======================================================================================================*/
        public async Task<ResponseMedicineAvailableDto> GetUnSyncIndentDetailByIndentId(string basUrl, string healthFacilityCode, long indentId)
        {
            HttpClient client = new HttpClient();
            string url = basUrl + "api/StockApi/GetUnSyncIndentDetailForHMIS/" + healthFacilityCode + "/" + indentId;
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            HttpResponseMessage httpResponse = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            var content = await httpResponse.Content.ReadAsStringAsync();

            ResponseMedicineAvailableDto jsonDeserialize = JsonConvert.DeserializeObject<ResponseMedicineAvailableDto>(content);

            return jsonDeserialize;
        }



        /*==================================================================================================== 
        Get Unsync Mainpharmacy Indent from MIMS side(i.e. those indents which has sync status = false in MIMS)
        Parameters: string basUrl, string healthFacilityCode
     ======================================================================================================*/
        public async Task<ResponseUnSyncIndentListDto> GetUnSyncMainStoreMedicineIndentFromMims(string basUrl, string healthFacilityCode)
        {
            HttpClient client = new HttpClient();
            string url = basUrl + "api/StockApi/GetUnSyncMainStoreIndentListForHMIS/" + healthFacilityCode;
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            HttpResponseMessage httpResponse = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            var content = await httpResponse.Content.ReadAsStringAsync();

            ResponseUnSyncIndentListDto jsonDeserialize = JsonConvert.DeserializeObject<ResponseUnSyncIndentListDto>(content);

            return jsonDeserialize;
        }


        public async Task<List<IndentDetailDTO>> GetUnSyncMainStoreIndentDetailForHMIS(string basUrl, string healthFacilityCode, int IndentId)
        {
            HttpClient client = new HttpClient();
            string url = basUrl + "api/StockApi/GetUnSyncMainStoreIndentDetailForHMIS/" + healthFacilityCode + "/" + IndentId;
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            HttpResponseMessage httpResponse = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            var content = await httpResponse.Content.ReadAsStringAsync();

            ResponseIndentDetailDTO jsonDeserialize = JsonConvert.DeserializeObject<ResponseIndentDetailDTO>(content);

            List<IndentDetailDTO> indentList = jsonDeserialize.Data;

            return indentList;
        }

        public async Task<LastUpdatedDateMIMsDTO> UpdateMainStoreIndentStatus(string basUrl, List<int> indentList)
        {
            var serializedParam = JsonConvert.SerializeObject(indentList);
            var stringContent = new StringContent(serializedParam, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();

            var httpResponse = await client.PostAsync(basUrl + "api/StockApi/UpdateMainStoreIndentSyncStatusForHMIS", stringContent);
            var content = await httpResponse.Content.ReadAsStringAsync();

            LastUpdatedDateMIMsDTO jsonDeserialize = JsonConvert.DeserializeObject<LastUpdatedDateMIMsDTO>(content);

            return jsonDeserialize;

        }


        /*======================================================================================================
            Get Main Store Medicine Stock List particular facility
            Parameters: basUrl, string HRhealthFacilityCode, string SystemId = 1 (for HMIS), string mimsDepartmentId
        ======================================================================================================*/
        public async Task<ResponseIndentDTO> GetMainStoreMedicineOpeningStockByHealthFacility(string basUrl, string hrHfId, bool offline = false)
        {
            HttpClient client = new HttpClient();
            string url = basUrl + "api/StockApi/MainStoreMedicineAvailableQuantity/" + hrHfId + "/1/" + offline;
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            HttpResponseMessage httpResponse = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            var content = await httpResponse.Content.ReadAsStringAsync();

            ResponseIndentDetailDTO jsonDeserialize = JsonConvert.DeserializeObject<ResponseIndentDetailDTO>(content);

            ResponseIndentDTO res = new ResponseIndentDTO();
            if (jsonDeserialize!.Data.Count > 0)
            {
                res.Data = jsonDeserialize.Data;
                res.IndentIdList = jsonDeserialize.IndentIdList;
            }
            return res;
        }
    }
}
