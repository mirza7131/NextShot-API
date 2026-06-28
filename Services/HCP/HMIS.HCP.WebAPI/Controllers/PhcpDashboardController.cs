using CommonDTOs.ResponseDTO;
using HMIS.HCP.Domain.Models.OldDbModels;
using HMIS.HCP.Domain.Models.OldDto;
using HMIS.HCP.Service;
using JWTAuthentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace HMIS.HCP.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhcpDashboardController : ControllerBase
    {
        #region Class Fields & Propertities

        //private readonly ILogger<AuthenticationController> _logger;
        private readonly TokenService _tokenService;
        private readonly PhcpDashboardService<TblPatient> _PhcpDashboardService;

        #endregion

        #region Constructor

        public PhcpDashboardController(TokenService tokenService, PhcpDashboardService<TblPatient> PhcpDashboardController)
        {
            _tokenService = tokenService;
            _PhcpDashboardService = PhcpDashboardController;
        }

        #endregion

        //[HttpGet]
        //[Route("GetTotalPatientCount")]
        ////public async Task<IActionResult> GetTotalPatientCount([FromQuery] DashboardFilter filter)
        //public async Task<IActionResult> GetTotalPatientCount()
        //{
        //    var list = await _PhcpDashboardService.GetTotalPatientCount();
        //    return Ok(new ResponseSuccess { data = list });
        //}

        [HttpGet]
        [Route("GetPatientRegistrationCount")]
        public async Task<IActionResult> GetPatientRegistrationCount([FromQuery] DashboardFilter filter)
        {
            var list = await _PhcpDashboardService.GetPatientRegistrationCount(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetVaccinationAdministeredCount")]
        public async Task<IActionResult> GetVaccinationAdministeredCount([FromQuery] DashboardFilter filter)
        {
            var list = await _PhcpDashboardService.GetVaccinationAdministeredCount(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetSampleReceivedCount")]
        public async Task<IActionResult> GetSampleReceivedCount([FromQuery] DashboardFilter filter)
        {
            var list = await _PhcpDashboardService.GetSampleReceivedCount(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        //[HttpGet]
        //[Route("GetSampleReceivedInProcessCount")]
        //public async Task<IActionResult> GetSampleReceivedInProcessCount()
        //{
        //    var list = await _PhcpDashboardService.GetSampleReceivedInProcessCount();
        //    return Ok(new ResponseSuccess { data = list });
        //}

        //[HttpGet]
        //[Route("GetHCVSampleProcessDetectnNotDetect")]
        //public async Task<IActionResult> GetHCVSampleProcessDetectnNotDetect()
        //{
        //    var list = await _PhcpDashboardService.GetHCVSampleProcessDetectnNotDetect();
        //    return Ok(new ResponseSuccess { data = list });
        //}
        [HttpGet]
        [Route("GetHCVSampleProcessNotDetected")]
        public async Task<IActionResult> GetHCVSampleProcessNotDetected([FromQuery] DashboardFilter filter)
        {
            var list = await _PhcpDashboardService.GetHCVSampleProcessNotDetected(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetHBVSampleProcessed")]
        public async Task<IActionResult> GetHBVSampleProcessed([FromQuery] DashboardFilter filter)
        {
            var list = await _PhcpDashboardService.GetHBVSampleProcessed(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetHCVEnrolledInTreatment")]
        public async Task<IActionResult> GetHCVEnrolledInTreatment([FromQuery] DashboardFilter filter)
        {
            var list = await _PhcpDashboardService.GetHCVEnrolledInTreatment(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        //[HttpGet]
        //[Route("GetHCVEnrolledInTreatment_SR")]
        //public async Task<IActionResult> GetHCVEnrolledInTreatment_SR()
        //{
        //    var list = await _PhcpDashboardService.GetHCVEnrolledInTreatment_SR();
        //    return Ok(new ResponseSuccess { data = list });
        //}
        [HttpGet]
        [Route("GetRelapsednCuredCounts")]
        public async Task<IActionResult> GetRelapsednCuredCounts([FromQuery] DashboardFilter filter)
        {
            var list = await _PhcpDashboardService.GetRelapsednCuredCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetEligibleForSVRCount")]
        public async Task<IActionResult> GetEligibleForSVRCount([FromQuery] DashboardFilter filter)
        {
            var list = await _PhcpDashboardService.GetEligibleForSVRCount(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetSampleCollectedSVRCount")]
        public async Task<IActionResult> GetSampleCollectedSVRCount([FromQuery] DashboardFilter filter)
        {
            var list = await _PhcpDashboardService.GetSampleCollectedSVRCount(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetSampleProcessedSVRCount")]
        public async Task<IActionResult> GetSampleProcessedSVRCount([FromQuery] DashboardFilter filter)
        {
            var list = await _PhcpDashboardService.GetSampleProcessedSVRCount(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetHBVTelbuvidineTreatmentCount")]
        public async Task<IActionResult> GetHBVTelbuvidineTreatmentCount([FromQuery] DashboardFilter filter)
        {
            var list = await _PhcpDashboardService.GetHBVTreatmentCount(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        //[HttpGet]
        //[Route("GetHBVTenofovirTreatmentCount")]
        //public async Task<IActionResult> GetHBVTenofovirTreatmentCount()
        //{
        //    var list = await _PhcpDashboardService.GetHBVTenofovirTreatmentCount();
        //    return Ok(new ResponseSuccess { data = list });
        //}
        //[HttpGet]
        //[Route("GetHBVEntecavirTreatmentCount")]
        //public async Task<IActionResult> GetHBVEntecavirTreatmentCount()
        //{
        //    var list = await _PhcpDashboardService.GetHBVEntecavirTreatmentCount();
        //    return Ok(new ResponseSuccess { data = list });
        //}
        [HttpGet]
        [Route("GetPatientsListCountHFWise")]
        public async Task<IActionResult> GetPatientsListCountHFWise([FromQuery] DashboardFilter filter)
        {
            var list = await _PhcpDashboardService.GetPatientsListCountHFWise(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetScreeningPatientsListCountHFWise")]
        public async Task<IActionResult> GetScreeningPatientsListCountHFWise([FromQuery] DashboardFilter filter)
        {
            var list = await _PhcpDashboardService.GetScreeningPatientsListCountHFWise(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetVaccinatedPatientsListCountHFWise")]
        public async Task<IActionResult> GetVaccinatedPatientsListCountHFWise([FromQuery] DashboardFilter filter)
        {
            var list = await _PhcpDashboardService.GetVaccinatedPatientsListCountHFWise(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetSampleReceivedACCnREJListCountHFWise")]
        public async Task<IActionResult> GetSampleReceivedACCnREJListCountHFWise([FromQuery] DashboardFilter filter)
        {
            var list = await _PhcpDashboardService.GetSampleReceivedACCnREJListCountHFWise(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetHCVSampleProcessedListCountHFWise")]
        public async Task<IActionResult> GetHCVSampleProcessedListCountHFWise([FromQuery] DashboardFilter filter)
        {
            var list = await _PhcpDashboardService.GetHCVSampleProcessedListCountHFWise(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetHBVSampleProcessedListCountHFWise")]
        public async Task<IActionResult> GetHBVSampleProcessedListCountHFWise([FromQuery] DashboardFilter filter)
        {
            var list = await _PhcpDashboardService.GetHBVSampleProcessedListCountHFWise(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetSVRSampleCollectedListCountHFWise")]
        public async Task<IActionResult> GetSVRSampleCollectedListCountHFWise([FromQuery] DashboardFilter filter)
        {
            var list = await _PhcpDashboardService.GetSVRSampleCollectedListCountHFWise(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetSVRSampleProcessedListCountHFWise")]
        public async Task<IActionResult> GetSVRSampleProcessedListCountHFWise([FromQuery] DashboardFilter filter)
        {
            var list = await _PhcpDashboardService.GetSVRSampleProcessedListCountHFWise(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetCurednRelapseListCountHFWise")]
        public async Task<IActionResult> GetCurednRelapseListCountHFWise([FromQuery] DashboardFilter filter)
        {
            var list = await _PhcpDashboardService.GetCurednRelapseListCountHFWise(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetEligibleForSVRListCountHFWise")]
        public async Task<IActionResult> GetEligibleForSVRListCountHFWise([FromQuery] DashboardFilter filter)
        {
            var list = await _PhcpDashboardService.GetEligibleForSVRListCountHFWise(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetRegPatientsWithPatientTypeLL")]
        public async Task<IActionResult> GetRegPatientsWithPatientTypeLL([FromQuery] RegisteredPatientDto filter)
        {
            var list = await _PhcpDashboardService.GetRegPatientsWithPatientTypeLL(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetScreenedPatientsLL")]
        public async Task<IActionResult> GetScreenedPatientsLL([FromQuery] RegisteredPatientDto filter)
        {
            var list = await _PhcpDashboardService.GetScreenedPatientsLL(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetVaccinatedPatientsLL")]
        public async Task<IActionResult> GetVaccinatedPatientsLL([FromQuery] RegisteredPatientDto filter)
        {
            var list = await _PhcpDashboardService.GetVaccinatedPatientsLL(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetTotalSampleReceivedLL")]
        public async Task<IActionResult> GetTotalSampleReceivedLL([FromQuery] RegisteredPatientDto filter)
        {
            var list = await _PhcpDashboardService.GetTotalSampleReceivedLL(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetHCVSampleProcessedLL")]
        public async Task<IActionResult> GetHCVSampleProcessedLL([FromQuery] RegisteredPatientDto filter)
        {
            var list = await _PhcpDashboardService.GetHCVSampleProcessedLL(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetHBVSampleProcessedLL")]
        public async Task<IActionResult> GetHBVSampleProcessedLL([FromQuery] RegisteredPatientDto filter)
        {
            var list = await _PhcpDashboardService.GetHBVSampleProcessedLL(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetSVRSampleLL")]
        public async Task<IActionResult> GetSVRSampleLL([FromQuery] RegisteredPatientDto filter)
        {
            var list = await _PhcpDashboardService.GetSVRSampleLL(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetPatientHistory")]
        public async Task<IActionResult> GetPatientHistory(long PatientId)
        {
            var list = await _PhcpDashboardService.GetPatientHistory(PatientId);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetScreeningCountOfDistrict")]
        public async Task<IActionResult> GetScreeningCountOfDistrict(long DistrictId)
        {
            var list = await _PhcpDashboardService.GetScreeningCountOfDistrict(DistrictId);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetHCVEnrolledInTreatmentHfWiseCount")]
        public async Task<IActionResult> GetHCVEnrolledInTreatmentHfWiseCount([FromQuery] DashboardFilter filter)
        {
            var list = await _PhcpDashboardService.GetHCVEnrolledInTreatmentHfWiseCount(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetHBVEnrolledInTreatmentHfWiseCount")]
        public async Task<IActionResult> GetHBVEnrolledInTreatmentHfWiseCount([FromQuery] DashboardFilter filter)
        {
            var list = await _PhcpDashboardService.GetHBVEnrolledInTreatmentHfWiseCount(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetPatientVitals")]
        public async Task<IActionResult> GetPatientVitals(int PatientId)
        {
            var list = await _PhcpDashboardService.GetPatientVitals(PatientId);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetPatientAssessment")]
        public async Task<IActionResult> GetPatientAssessment(int PatientId)
        {
            var list = await _PhcpDashboardService.GetPatientAssessment(PatientId);
            return Ok(new ResponseSuccess { data = list });
        }
    }
}
