using CommonDTOs.ResponseDTO;
using HMIS.HCP.Domain.Models.DbModels;
using HMIS.HCP.Domain.Models.DTO;
using HMIS.HCP.Domain.Models.DTO.PatientDiagnoseDtos;
using HMIS.HCP.Service;
using JWTAuthentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMIS.HCP.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientAssessmentController : ControllerBase
    {
        #region Class Fields & Propertities

        //private readonly ILogger<AuthenticationController> _logger;
        private readonly TokenService _tokenService;
        private readonly PatientAssessmentService<PatientAssessment> _PatientAssessmentService;

        #endregion

        #region Constructor

        public PatientAssessmentController( TokenService tokenService, PatientAssessmentService<PatientAssessment> PatientAssessmentService)
        {
            _tokenService = tokenService;
            _PatientAssessmentService = PatientAssessmentService;
        }

        #endregion

        #region CUD Operations
        //[HttpPost]
        //[Route("CreateOrEdit")]
        //public async Task<IActionResult> CreateOrEdit(CreateOrEditPatientAssessmentDto input)
        //{
        //    var obj = await _PatientAssessmentService.CreateOrEdit(input);
        //    return Ok(new ResponseSave { data = obj });
        //}

        //[HttpPost]
        //[Route("Delete")]
        //public async Task<IActionResult> Delete(Guid Id)
        //{
        //    var obj = await _PatientAssessmentService.Delete(Id);
        //    return Ok(new ResponseDelete { data = obj });
        //}

        [HttpGet]
        [Route("GetPatientPreviousAssessment")]
        public async Task<IActionResult> GetPatientPreviousAssessment(Guid PatientId)
        {
            var response = await _PatientAssessmentService.GetPatientPreviousAssessment(PatientId);
            return Ok(new ResponseSuccess { data = response });
        }
        //public int MyProperty { get; set; }
        //public int asd { get; set; }
        // This API needs to be updated there is exception in this
        [HttpGet]
        [Route("GetPatientLastFollowupDate")]
        public async Task<IActionResult> GetPatientLastFollowupDate(Guid PatientId)
        {
            var response = await _PatientAssessmentService.GetPatientLastFollowupDate(PatientId);
            return Ok(new ResponseSuccess { data = response });
        }

        [HttpGet]
        [Route("GetPatientPreviousDiagnose")]
        public async Task<IActionResult> GetPatientPreviousDiagnose(Guid PatientId)
        {
            var response = await _PatientAssessmentService.GetPatientPreviousDiagnose(PatientId);
            return Ok(new ResponseSuccess { data = response });
        }
        //public int MyProperty { get; set; }
        //public int asd { get; set; }
        [HttpPost]
        [Route("GetPatientPreviousDiagnoseByScreeningDate")]
        public async Task<IActionResult> GetPatientPreviousDiagnoseByScreeningDate(PatientPreviousDiagnoseDto input)
        {

            var response = await _PatientAssessmentService.GetPatientPreviousDiagnoseByScreeningDate(input);
            return Ok(new ResponseSuccess { data = response });
        }

        #endregion
    }
}
