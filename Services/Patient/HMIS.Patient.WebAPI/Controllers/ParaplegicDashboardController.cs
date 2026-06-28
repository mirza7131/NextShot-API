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
    public class ParaplegicDashboardController : ControllerBase
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly ParaplegicDashboardService _ParaplegicDashboardService;
        private readonly PatientDiagnoseService<PatientDiagnose> _PatientDiagnoseService;
        private readonly bool _isDevelopment;
        #endregion

        #region Constructor

        public ParaplegicDashboardController(TokenService tokenService, ParaplegicDashboardService ParaplegicDashboardService)
        {
            _tokenService = tokenService;
            _ParaplegicDashboardService = ParaplegicDashboardService;
        }

        #endregion


        #region Patient Registration Dashboard New  
        [HttpGet]
        [Route("getParaplegicRegistrationDashboardAllCounts")]
        public async Task<IActionResult> getParaplegicRegistrationDashboardAllCounts([FromQuery] DashboardFilter filter)
        {
            var list = await _ParaplegicDashboardService.getParaplegicRegistrationDashboardAllCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("getParaplegicRegistrationDashboardAllList")]
        public async Task<IActionResult> getParaplegicRegistrationDashboardAllList([FromQuery] DashboardFilter filter)
        {
            var list = await _ParaplegicDashboardService.getParaplegicRegistrationDashboardAllList(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("getParaplegicRegDashboardOPDSectionWiseTokenCount")]
        public async Task<IActionResult> getParaplegicRegDashboardOPDSectionWiseTokenCount([FromQuery] DashboardFilter filter)
        {
            var list = await _ParaplegicDashboardService.getParaplegicRegDashboardOPDSectionWiseTokenCount(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("getParaplegicPatientVisitCountByUser")]
        public async Task<IActionResult> getParaplegicPatientVisitCountByUser([FromQuery] DashboardFilter filter)
        {
            var list = await _ParaplegicDashboardService.getParaplegicPatientVisitCountByUser(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        #endregion

        #region Doctor Dashboard
        [HttpGet]
        [Route("getParaplegicDoctorDashboardPatientAllCounts")]
        public async Task<IActionResult> getParaplegicDoctorDashboardPatientAllCounts([FromQuery] DashboardFilter filter)
        {
            var list = await _ParaplegicDashboardService.getParaplegicDoctorDashboardPatientAllCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("getParaplegicDoctorDashboardHFPatientAllCounts")]
        public async Task<IActionResult> getParaplegicDoctorDashboardHFPatientAllCounts([FromQuery] DashboardFilter filter)
        {
            var list = await _ParaplegicDashboardService.getParaplegicDoctorDashboardHFPatientAllCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("getParaplegicDoctorDashboardPatientAllList")]
        public async Task<IActionResult> getParaplegicDoctorDashboardPatientAllList([FromQuery] DashboardFilter filter)
        {
            var list = await _ParaplegicDashboardService.getParaplegicDoctorDashboardPatientAllList(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("getParaplegicDoctorDashboardHFPatientAllList")]
        public async Task<IActionResult> getParaplegicDoctorDashboardHFPatientAllList([FromQuery] DashboardFilter filter)
        {
            var list = await _ParaplegicDashboardService.getParaplegicDoctorDashboardHFPatientAllList(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        #endregion

        #region Patient Vital Dashboard New  
        [HttpGet]
        [Route("getParaplegicVitalDashboardAllCounts")]
        public async Task<IActionResult> getParaplegicVitalDashboardAllCounts([FromQuery] DashboardFilter filter)
        {
            var list = await _ParaplegicDashboardService.getParaplegicVitalDashboardAllCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("getParaplegicVitalDashboardAllList")]
        public async Task<IActionResult> getParaplegicVitalDashboardAllList([FromQuery] DashboardFilter filter)
        {
            var list = await _ParaplegicDashboardService.getParaplegicVitalDashboardAllList(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("getParaplegicPatientVisitVitalCountByUser")]
        public async Task<IActionResult> getParaplegicPatientVisitVitalCountByUser([FromQuery] DashboardFilter filter)
        {
            var list = await _ParaplegicDashboardService.getParaplegicPatientVisitVitalCountByUser(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        #endregion

        #region Pharmacy New
        [HttpGet]
        [Route("getParaplegicPharmacyDashboardAllCounts")]
        public async Task<IActionResult> getPharmacyDashboardAllCounts([FromQuery] DashboardFilter filter)
        {
            var list = await _ParaplegicDashboardService.getParaplegicPharmacyDashboardAllCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("getParaplegicPharmacyDashboardAllList")]
        public async Task<IActionResult> getParaplegicPharmacyDashboardAllList([FromQuery] DashboardFilter filter)
        {
            var list = await _ParaplegicDashboardService.getParaplegicPharmacyDashboardAllList(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetParaplegicPharmacyDashboardMedcineIssuedReport")]
        public async Task<IActionResult> GetParaplegicPharmacyDashboardMedcineIssuedReport([FromQuery] DashboardFilter filter)
        {
            var list = await _ParaplegicDashboardService.GetParaplegicPharmacyDashboardMedcineIssuedReport(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("getParaplegicPharmacyDashboardMedcineIssuedPatientDetailList")]
        public async Task<IActionResult> getParaplegicPharmacyDashboardMedcineIssuedPatientDetailList([FromQuery] MedicineIssuedFilter filter)
        {
            var list = await _ParaplegicDashboardService.getParaplegicPharmacyDashboardMedcineIssuedPatientDetailList(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetParaplegicMIMSCountForHMISByHealthFacilityCode")]
        public async Task<IActionResult> GetParaplegicMIMSCountForHMISByHealthFacilityCode([FromQuery] DashboardFilter filter)
        {
            var list = await _ParaplegicDashboardService.GetParaplegicMIMSCountForHMISByHealthFacilityCode(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("getParaplegicPharmacyInternalExternalAccumulateQuantityPatientWiseReport")]
        public async Task<IActionResult> getParaplegicPharmacyInternalExternalAccumulateQuantityPatientWiseReport([FromQuery] DashboardFilter filter)
        {
            var list = await _ParaplegicDashboardService.getParaplegicPharmacyInternalExternalAccumulateQuantityPatientWiseReport(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        #endregion

        #region Pathology NEw
        [HttpGet]
        [Route("getParaplegicLabDashboardTestAllCounts")]
        public async Task<IActionResult> getParaplegicLabDashboardTestAllCounts([FromQuery] DashboardFilter filter)
        {
            var list = await _ParaplegicDashboardService.getParaplegicLabDashboardTestAllCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("getParaplegicLabDashboardTestTimeFromStartTillNowAllCounts")]
        public async Task<IActionResult> getParaplegicLabDashboardTestTimeFromStartTillNowAllCounts([FromQuery] DashboardFilter filter)
        {
            var list = await _ParaplegicDashboardService.getParaplegicLabDashboardTestTimeFromStartTillNowAllCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("getParaplegicLabDashboardTestAllList")]
        public async Task<IActionResult> getParaplegicLabDashboardTestAllList([FromQuery] DashboardFilter filter)
        {
            var list = await _ParaplegicDashboardService.getParaplegicLabDashboardTestAllList(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("getParaplegicLabDashboardTestTimeFromStartTillNowAllList")]
        public async Task<IActionResult> getParaplegicLabDashboardTestTimeFromStartTillNowAllList([FromQuery] DashboardFilter filter)
        {
            var list = await _ParaplegicDashboardService.getParaplegicLabDashboardTestTimeFromStartTillNowAllList(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("getParaplegicLabDashboardTop20LabTestRecommended")]
        public async Task<IActionResult> getParaplegicLabDashboardTop20LabTestRecommended([FromQuery] DashboardFilter filter)
        {
            var list = await _ParaplegicDashboardService.getParaplegicLabDashboardTop20LabTestRecommended(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        #endregion
        #region Paraplegic Dashboard 
        [HttpGet]
        [Route("getParaplegicDashboardAllCounts")]
        public async Task<IActionResult> getParaplegicDashboardAllCounts([FromQuery] DashboardFilter filter)
        {
            var list = await _ParaplegicDashboardService.getParaplegicDashboardAllCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("getParaplegicDashboardAllList")]
        public async Task<IActionResult> getParaplegicDashboardAllList([FromQuery] DashboardFilter filter)
        {
            var list = await _ParaplegicDashboardService.getParaplegicDashboardAllList(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        #endregion
        #region  Indivisual Opd Dashboard 
        [HttpGet]
        [Route("getIndivisualOpdDashboardAllCounts")]
        public async Task<IActionResult> getIndivisualOpdDashboardAllCounts([FromQuery] DashboardFilter filter)
        {
            var list = await _ParaplegicDashboardService.getIndivisualOpdDashboardAllCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("getIndivisualOpdDashboardAllList")]
        public async Task<IActionResult> getIndivisualOpdDashboardAllList([FromQuery] DashboardFilter filter)
        {
            var list = await _ParaplegicDashboardService.getIndivisualOpdDashboardAllList(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        #endregion

    }
}
