using HMIS.Aggregator.API.Common.EndPoints;
using HMIS.Aggregator.API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.Text;
using static HMIS.Aggregator.API.Models.HrLogginUserDto;
using static HMIS.Aggregator.API.Models.MIMS.MedicineAvailableDto;
using static HMIS.Aggregator.API.Models.MIMS.MedicineDispenseDto;

namespace HMIS.Aggregator.API.Services
{
    public class BASService
    {
        private readonly HttpClient _client;
        //private readonly string mimsBaseUrl;
        public BASService(HttpClient client, IWebHostEnvironment env)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            //mimsBaseUrl =  MIMSEndPoint.MIMSDevBaseUrl;
            //mimsBaseUrl = env.IsDevelopment() ? MIMSEndPoint.MIMSDevBaseUrl : MIMSEndPoint.MIMSProBaseUrl;

        }

        public async Task<BASDashboardCountsDTO> getBasDashboardAllCounts(string basUrl, string healthFacilityCode)
        {
            HttpClient client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, basUrl + "api/DutyRoster/DutyRosterDashobardAllCounts/" + healthFacilityCode);
            HttpResponseMessage httpResponse = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            var content = await httpResponse.Content.ReadAsStringAsync();

            BASDashboardCountsDTO jsonDeserialize = JsonConvert.DeserializeObject<BASDashboardCountsDTO>(content);

            return jsonDeserialize;
        }

        public async Task<ResponseDutyRosterEmployeeListDTO> getBasDashboardAllemployeeList(string basUrl, string healthFacilityCode, string listType)
        {
            HttpClient client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, basUrl + "api/DutyRoster/EmployeeRegistrationListBR_AllDashboard_New/" + healthFacilityCode + "/"+ listType + "/Admin");
            HttpResponseMessage httpResponse = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            var content = await httpResponse.Content.ReadAsStringAsync();

            ResponseDutyRosterEmployeeListDTO jsonDeserialize = JsonConvert.DeserializeObject<ResponseDutyRosterEmployeeListDTO>(content);

            return jsonDeserialize;
        }

        public async Task<ResponseDeivcesListAndSyncCountsDTO> getBasDashboardAllDevicesList(string basUrl, string healthFacilityCode, string listType)
        {
            HttpClient client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, basUrl + "api/DutyRoster/StolenDevicesListDC_AllDashboard_New/" + healthFacilityCode + "/" + listType + "/Admin");
            HttpResponseMessage httpResponse = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            var content = await httpResponse.Content.ReadAsStringAsync();

            ResponseDeivcesListAndSyncCountsDTO jsonDeserialize = JsonConvert.DeserializeObject<ResponseDeivcesListAndSyncCountsDTO>(content);

            return jsonDeserialize;
        }
        public async Task<ResponseDeivcesListAndSyncCountsDTO> getBasDashboardAllSyncDevicesList(string basUrl, string healthFacilityCode, string listType)
        {
            HttpClient client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, basUrl + "api/DutyRoster/SyncDevicesListDC_AllDashboard_New/" + healthFacilityCode + "/" + listType + "/Admin");
            HttpResponseMessage httpResponse = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            var content = await httpResponse.Content.ReadAsStringAsync();

            ResponseDeivcesListAndSyncCountsDTO jsonDeserialize = JsonConvert.DeserializeObject<ResponseDeivcesListAndSyncCountsDTO>(content);

            return jsonDeserialize;
        }
        public async Task<ResponseDeivcesListAndAwakedCountsDTO> getBasDashboardAllOnlineOfflineDevicesList(string basUrl, string healthFacilityCode, string listType)
        {
            HttpClient client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, basUrl + "api/DutyRoster/DeviceStatusTotalDC_AllDashboard_New/" + healthFacilityCode + "/" + listType + "/Admin");
            HttpResponseMessage httpResponse = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            var content = await httpResponse.Content.ReadAsStringAsync();

            ResponseDeivcesListAndAwakedCountsDTO jsonDeserialize = JsonConvert.DeserializeObject<ResponseDeivcesListAndAwakedCountsDTO>(content);

            return jsonDeserialize;
        } 
        public async Task<List<RosterDoneUnDoneListDTO>> getBasDashboardAllRostersList(string basUrl, string healthFacilityCode, string CurrentMonth, string listType)
        {
            HttpClient client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, basUrl + "api/Dashboard/GetRosterHFListDC_AllDashboard_New/" + healthFacilityCode + "/" + CurrentMonth + "/" + listType + "/Admin");
            HttpResponseMessage httpResponse = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            var content = await httpResponse.Content.ReadAsStringAsync();

            List<RosterDoneUnDoneListDTO> jsonDeserialize = JsonConvert.DeserializeObject<List<RosterDoneUnDoneListDTO>>(content);

            return jsonDeserialize;
        }
        public async Task<ResponseAttendanceReportCountDTO> getBasDashboardAllAttendenceReportCounts(string basUrl, AttendenceObject attendence)
        {

            HttpClient client = new HttpClient();
            var request = $"{basUrl}api/DutyRoster/GetAttendanceReportCountWithTimeBR";
            var serializedParam = JsonConvert.SerializeObject(attendence);
            var stringContent = new StringContent(serializedParam, Encoding.UTF8, "application/json");


            var httpResponse = await client.PostAsync(request, stringContent);
            var content = await httpResponse.Content.ReadAsStringAsync();

            ResponseAttendanceReportCountDTO jsonDeserialize = JsonConvert.DeserializeObject<ResponseAttendanceReportCountDTO>(content);
            //string jsonDeserialize = JsonConvert.DeserializeObject<string>(content);

            return jsonDeserialize;
        }

        public async Task<ResponseDutyRosterEmployeeNotInRosterCountDTO> getBasDashboardAllNotInRosterCount(string basUrl, string healthFacilityCode, string listType)
        {
            HttpClient client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, basUrl + "api/DutyRoster/DutyRosterEmployeeNotInRosterCount/" + healthFacilityCode);
            HttpResponseMessage httpResponse = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            var content = await httpResponse.Content.ReadAsStringAsync();

            ResponseDutyRosterEmployeeNotInRosterCountDTO jsonDeserialize = JsonConvert.DeserializeObject<ResponseDutyRosterEmployeeNotInRosterCountDTO>(content);

            return jsonDeserialize;
        }
        public async Task<ResponseAttendanceListingDTO> getBasDashboardAllAttendenceReportList(string basUrl, string healthFacilityCode, string listType)
        {
            HttpClient client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, basUrl + "api/DutyRoster/AttendenceReportListBR/" + healthFacilityCode + "/" + listType);
            HttpResponseMessage httpResponse = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            var content = await httpResponse.Content.ReadAsStringAsync();

            ResponseAttendanceListingDTO jsonDeserialize = JsonConvert.DeserializeObject<ResponseAttendanceListingDTO>(content);

            return jsonDeserialize;
        }

        public async Task<ResponseAttendenceReportCalenderDTO> getBasDashboardAllAttendenceDateWiseList(string basUrl, string healthFacilityCode, string listType)
        {
            HttpClient client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, basUrl + "api/DutyRoster/AttendenceReportListBR/" + healthFacilityCode + "/" + listType);
            HttpResponseMessage httpResponse = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            var content = await httpResponse.Content.ReadAsStringAsync();

            ResponseAttendenceReportCalenderDTO jsonDeserialize = JsonConvert.DeserializeObject<ResponseAttendenceReportCalenderDTO>(content);

            return jsonDeserialize;
        }

        public async Task<DutyRosterAttendanceDownloadDTO> getBasDashboardAllAttendenceDateWiseListDownload(string basUrl, string healthFacilityCode, string listType)
        {
            HttpClient client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, basUrl + "api/DutyRoster/AttendenceReportListBR/" + healthFacilityCode + "/" + listType);
            HttpResponseMessage httpResponse = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            var content = await httpResponse.Content.ReadAsStringAsync();

            DutyRosterAttendanceDownloadDTO jsonDeserialize = JsonConvert.DeserializeObject<DutyRosterAttendanceDownloadDTO>(content);

            return jsonDeserialize;
        }
        //public async Task<ResponseMedicineDispenseDto> MedicineDespenseByHealthFacility(string basUrl, List<BASDashboardCountsDTO> medicineDispenseDto)
        //{
        //    //var data1 = new Dictionary<string, string>
        //    //{
        //    //    {"hfmisCode", medicineDispenseDto.hfmisCode},
        //    //    {"Quantity", medicineDispenseDto.Quantity.ToString()},
        //    //    {"MedId", medicineDispenseDto.MedId.ToString()}
        //    //};
        //    var serializedParam = JsonConvert.SerializeObject(medicineDispenseDto);
        //    var stringContent = new StringContent(serializedParam, Encoding.UTF8, "application/json");
        //    HttpClient client = new HttpClient();

        //    var httpResponse = await client.PostAsync(basUrl + "api/StockApi/MedDespenseByHF", stringContent);
        //    var content = await httpResponse.Content.ReadAsStringAsync();

        //    ResponseMedicineDispenseDto jsonDeserialize = JsonConvert.DeserializeObject<ResponseMedicineDispenseDto>(content);
        //    //string jsonDeserialize = JsonConvert.DeserializeObject<string>(content);

        //    return jsonDeserialize;
        //    //return null;

        //}

    }
}
