using CommonDTOs.ResponseDTO;
using HMIS.EMC.Domain.Models.Dto;
using HMIS.EMC.Domain.Models.Dto.FilterDto;
using HMIS.EMC.Service.Interfaces;
using JWTAuthentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMIS.EMC.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MLEController : ControllerBase
    {
        #region Class Fields & Properties
        private readonly TokenService _tokenService;
        private readonly IMLE _mle;
        #endregion

        #region Constructor
        public MLEController(TokenService tokenService, IMLE mle)
        {
            _tokenService = tokenService;
            _mle = mle;
        }
        #endregion

        #region CUD
        [HttpPost]
        [Route("CreateOrEditBasicInfo")]
        public async Task<IActionResult> CreateOrEditBasicInfo(CreateOrEditMLEBasicInfoDto input)
        {
            var obj=await _mle.CreateOrEdit(input);
            if (input.IsReportCount != true)
                return Ok(new ResponseSave { data = obj });
            else
                return Ok(new ResponseSuccess { data = obj });
        }
        
        [HttpPost]
        [Route("UpdateAssignDoctor")]
        public async Task<IActionResult> UpdateAssignDoctor(UpdateAssignDoctorDto input)
        {
            var obj = await _mle.UpdateAssignDoctor(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("CreateOrEditExamination")]
        public async Task<IActionResult> CreateOrEditExamination(CreateOrEditMLEExaminationDto input)
        {
            var obj = await _mle.CreateOrEdit(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("CreateOrEditReport")]
        public async Task<IActionResult> CreateOrEditReport(CreateOrEditMleReportDto input)
        {
                        var obj=await _mle.CreateOrEdit(input);
            return Ok(new ResponseSave { data = obj }); 
        }
        #endregion

        #region Read
        [HttpGet]
        [Route("GetAllMlePatients")]
        public async Task<IActionResult> GetAllMlePatients([FromQuery] SearchFilterDto? filter) { 
            var lst=await _mle.GetAllMlePatients(filter);
            return Ok(new ResponseSuccess { data=lst});
        }
        
        [HttpGet]
        [Route("GetAllMLCsByHealthFacilityId")]
        public async Task<IActionResult> GetAllMLCsByHealthFacilityId([FromQuery] SearchFilterDto? filter) { 
            var lst=await _mle.GetAllMLCsByHealthFacilityId(filter);
            return Ok(new ResponseSuccess { data=lst});
        }

        [HttpGet]
        [Route("GetSinglePatientBasicInfo")]
        public async Task<IActionResult> GetSinglePatientBasicInfo(Guid PatientId)
        {
            var obj = await _mle.GetSinglePatientBasicInfo(PatientId);
            return Ok(new ResponseSuccess { data = obj });
        }

        [HttpGet]
        [Route("GetAllMlcDoctors")]
        public async Task<IActionResult> GetAllMlcDoctors(int HealthFacilityId)
        {
            var lst = await _mle.GetAllMlcDoctors(HealthFacilityId);
            return Ok(new ResponseSuccess { data=lst });
        }


        [HttpGet]
        [Route("GetSinglePatientMleInfo")]
        public async Task<IActionResult> GetSinglePatientMleInfo(Guid PatientId)
        {
            var obj = await _mle.GetSinglePatientMleInfo(PatientId);
            return Ok(new ResponseSuccess { data = obj });
        }

        [HttpGet]
        [Route("GetAllMLCPatientsThatAreNotCheckedYet")]
        public async Task<IActionResult> GetAllMLCDoctorsByHealthFacilityId([FromQuery]  SearchFilterDto? filter)
        {
            var obj = await _mle.GetAllMLCDoctorsByHealthFacilityId(filter);
            return Ok(new ResponseSuccess { data = obj });
        }

        [HttpGet]
        [Route("GetSingleMLCPatientsThatAreNotCheckedYet/{MlcId}")]
        public async Task<IActionResult> GetSingleMLCPatientsThatAreNotCheckedYet(Guid MlcId)
        {
            var obj = await _mle.GetSingleMLCPatientsThatAreNotCheckedYet(MlcId);
            return Ok(new ResponseSuccess { data = obj });
        }

        #endregion

        #region Helper Method

        #endregion
    }
}
