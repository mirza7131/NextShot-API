using CommonDTOs.ResponseDTO;
using HMIS.Aggregator.API.Services;
using HMIS.Patient.Domain.Models.DbModels;
using HMIS.Patient.Domain.Models.DTO.DashboardDto;
using HMIS.Patient.Domain.Models.DTO.PatientOpenVisitDto;
using HMIS.Patient.Service;
using JWTAuthentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMIS.Patient.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class IPDDashboardController : ControllerBase
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly IPDDashboardService _IPDdashboardService;
        private readonly PatientDiagnoseService<PatientDiagnose> _PatientDiagnoseService;
        private readonly bool _isDevelopment;
        #endregion

        #region Constructor

        public IPDDashboardController(TokenService tokenService, IPDDashboardService IPDDashboardService)
        {
            _tokenService = tokenService;
            _IPDdashboardService = IPDDashboardService;
        }

        #endregion

        #region IPD Admissions Dashboard 
        [HttpGet]
        [Route("getIPDAdmissionDashboardAllCounts")]
        public async Task<IActionResult> getIPDAdmissionDashboardAllCounts([FromQuery] DashboardFilter filter)
        {
            var list = await _IPDdashboardService.getIPDAdmissionDashboardAllCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("getIPDAdmissionDashboardAllList")]
        public async Task<IActionResult> getIPDAdmissionDashboardAllList([FromQuery] DashboardFilter filter)
        {
            var list = await _IPDdashboardService.getIPDAdmissionDashboardAllList(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("getIPDAdmissionDashboardIPDWardWiseCount")]
        public async Task<IActionResult> getIPDAdmissionDashboardIPDWardWiseCount([FromQuery] DashboardFilter filter)
        {
            var list = await _IPDdashboardService.getIPDAdmissionDashboardIPDWardWiseCount(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        #endregion

        #region IPD Vital Dashboard 
        [HttpGet]
        [Route("getIPDVitalDashboardAllCounts")]
        public async Task<IActionResult> getIPDVitalDashboardAllCounts([FromQuery] DashboardFilter filter)
        {
            var list = await _IPDdashboardService.getIPDVitalDashboardAllCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("getIPDVitalDashboardAllList")]
        public async Task<IActionResult> getIPDVitalDashboardAllList([FromQuery] DashboardFilter filter)
        {
            var list = await _IPDdashboardService.getIPDVitalDashboardAllList(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("getIPDVitalDashboardWardWiseCount")]
        public async Task<IActionResult> getIPDVitalDashboardWardWiseCount([FromQuery] DashboardFilter filter)
        {
            var list = await _IPDdashboardService.getIPDVitalDashboardWardWiseCount(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        #endregion

        #region IPD Doctor Dashboard 
        [HttpGet]
        [Route("getIPDDoctorDashboardPatientAllCounts")]
        public async Task<IActionResult> getIPDDoctorDashboardPatientAllCounts([FromQuery] DashboardFilter filter)
        {
            var list = await _IPDdashboardService.getIPDDoctorDashboardPatientAllCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("getIPDDoctorHFDashboardPatientAllCounts")]
        public async Task<IActionResult> getIPDDoctorHFDashboardPatientAllCounts([FromQuery] DashboardFilter filter)
        {
            var list = await _IPDdashboardService.getIPDDoctorHFDashboardPatientAllCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("getIPDDoctorDashboardPatientAllList")]
        public async Task<IActionResult> getIPDDoctorDashboardPatientAllList([FromQuery] DashboardFilter filter)
        {
            var list = await _IPDdashboardService.getIPDDoctorDashboardPatientAllList(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("getIPDDoctorHFDashboardPatientAllList")]
        public async Task<IActionResult> getIPDDoctorHFDashboardPatientAllList([FromQuery] DashboardFilter filter)
        {
            var list = await _IPDdashboardService.getIPDDoctorHFDashboardPatientAllList(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        #endregion

        #region IPD Pharmacy Dashboard 
        [HttpGet]
        [Route("getIPDPharmacyDashboardAllCounts")]
        public async Task<IActionResult> getIPDPharmacyDashboardAllCounts([FromQuery] DashboardFilter filter)
        {
            var list = await _IPDdashboardService.getIPDPharmacyDashboardAllCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("getIPDPharmacyDashboardAllList")]
        public async Task<IActionResult> getIPDPharmacyDashboardAllList([FromQuery] DashboardFilter filter)
        {
            var list = await _IPDdashboardService.getIPDPharmacyDashboardAllList(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        #endregion
        #region IPD Lab/Pathology Dashboard 
        [HttpGet]
        [Route("getIPDLabDashboardTestAllCounts")]
        public async Task<IActionResult> getIPDLabDashboardTestAllCounts([FromQuery] DashboardFilter filter)
        {
            var list = await _IPDdashboardService.getIPDLabDashboardTestAllCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("getIPDLabDashboardTestAllList")]
        public async Task<IActionResult> getIPDLabDashboardTestAllList([FromQuery] DashboardFilter filter)
        {
            var list = await _IPDdashboardService.getIPDLabDashboardTestAllList(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("getIPDLabDashboardTestTimeFromStartTillNowAllCounts")]
        public async Task<IActionResult> getIPDLabDashboardTestTimeFromStartTillNowAllCounts([FromQuery] DashboardFilter filter)
        {
            var list = await _IPDdashboardService.getIPDLabDashboardTestTimeFromStartTillNowAllCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("getIPDLabDashboardTestTimeFromStartTillNowAllList")]
        public async Task<IActionResult> getIPDLabDashboardTestTimeFromStartTillNowAllList([FromQuery] DashboardFilter filter)
        {
            var list = await _IPDdashboardService.getIPDLabDashboardTestTimeFromStartTillNowAllList(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("getIPDLabDashboardTop20LabTestRecommended")]
        public async Task<IActionResult> getLabDashboardTop20LabTestRecommended([FromQuery] DashboardFilter filter)
        {
            var list = await _IPDdashboardService.getIPDLabDashboardTop20LabTestRecommended(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        #endregion

    }
}
