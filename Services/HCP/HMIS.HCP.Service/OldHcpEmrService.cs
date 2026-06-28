using AutoMapper;
using HMIS.HCP.Domain.Models.OldDbModels;
using JWTAuthentication;
using HMIS.HCP.Domain.Repositories._UOW;
using CommonExceptionHandler;
using CommonMessages;
using AppCommonMethods;
using HMIS.HCP.Domain.Models.DTO;
using Newtonsoft.Json;

namespace HMIS.HCP.Service
{
    public class OldHcpEmrService<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        private UnitOfWork<TblPatient> _uowTblPatient;
        private UnitOfWork<TblLabSample> _uowTblLabSample;

        #endregion

        #region Constructor

        public OldHcpEmrService(TokenService tokenService, UnitOfWork<TblLabSample> uowTblLabSample, UnitOfWork<TblPatient> uowTblPatient, IMapper mapper)
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _uowTblPatient = uowTblPatient;
            _uowTblLabSample = uowTblLabSample;
        }

        #endregion

        #region Services
        public async Task<List<TblLabSample>> GetTotalSamples(string PatientCnic)
        {

            var db = new PhcpContext();
            var patientDetails = db.TblPatients.Where(x=>x.SelfCnic == PatientCnic).FirstOrDefault();

            if (!AppCommonMethod.IsNullObject(patientDetails))
            {
                var samplesList = db.TblLabSamples.Where(x=>x.Pid == patientDetails.Id).ToList();
                if (!AppCommonMethod.IsNullObject(samplesList))
                {
                    return samplesList;
                }
                else
                {
                    throw new UserFriendlyException(CommonMessageConstant.SampleNotFoundAgainstThisPatient);
                }
            }
            else
            {
                    throw new UserFriendlyException(CommonMessageConstant.PatientNotFoundinOldEMR);
            }
            //PatientScreening? responseObj = await _uowPatientScreening.Repository.GetById(input);
            //var responseObj = await _uowPatientVaccination.Repository.GetALL(x => x.PatientId == input).ToListAsync();

            //if (AppCommonMethod.IsNullObject(responseObj))
            //    throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            //return _mapper.Map<List<PatientVaccination>>(responseObj);
        }
        public async Task<List<PatientSampleDataDto>> GetTotalSamplesWithCnic(string PatientCnic)
        {
            HttpClient client = new HttpClient();
            string url = "https://phcp.pshealthpunjab.gov.pk/generate-pdf/get_total_sample_reports_for_HMIS?cnic=" + PatientCnic;
            //string url = "http://localhost/phcp-emr/generate-pdf/get_total_sample_reports_for_HMIS?cnic=" + PatientCnic;
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            //var request = new HttpRequestMessage(HttpMethod.Get, basUrl + "api/StockApi/MedicineAvailableQuantityByHF/" + healthFacilityCode + "/" + mimsDepartmentId);

            HttpResponseMessage httpResponse = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            if (httpResponse.IsSuccessStatusCode == false)
            {
                throw new UserFriendlyException(CommonMessageConstant.PatientNotFoundinOldEMR);
            }
            else
            {
                var content = await httpResponse.Content.ReadAsStringAsync();
                List<PatientSampleDataDto> jsonDeserialize = JsonConvert.DeserializeObject<List<PatientSampleDataDto>>(content);
                return jsonDeserialize;
            }


            // Make a GET request to the CodeIgniter API
            //var response = await _httpClient.GetAsync($"https://phcp.pshealthpunjab.gov.pk/generate-pdf/get_total_sample_reports/{PatientCnic}");

            //if (response.IsSuccessStatusCode)
            //{
            //    if (true)
            //    {

            //    }
            //    // Parse the response and return it
            //    var data = await response.Content.ReadAsStringAsync();
            //    return data;
            //}

            return null; // Handle errors accordingly
        }

        #endregion
    }
}
