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
    public class MlcSvController : ControllerBase
    {
        #region Class Fields & Properties
        private readonly TokenService _tokenService;
        private readonly IMLESvForm _mlesv;
        #endregion

        #region Constructor
        public MlcSvController(TokenService tokenService, IMLESvForm mle)
        {
            _tokenService = tokenService;
            _mlesv = mle;
        }
        #endregion

        #region CUD
        [HttpPost]
        [Route("CreateOrEditMlcSvInitialInfo")]
        public async Task<IActionResult> CreateOrEditMlcSvInitialInfo(CreateOrEditMlcSvInitialInfoDto input)
        {
            var obj = await _mlesv.CreateOrEdit(input);
            if (input.IsReportCount != true)
                return Ok(new ResponseSave { data = obj });                            
            else
                return Ok(new ResponseSuccess { data = obj });  
        }

        [HttpPost]
        [Route("CreateOrEditMlcSvExamination")]
        public async Task<IActionResult> CreateOrEditMlcSvExamination(CreateOrEditMlcSvExaminationDto input)
        {
            var obj = await _mlesv.CreateOrEdit(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("CreateOrEditMlcSvEvidenceCollected")]
        public async Task<IActionResult> CreateOrEditMlcSvEvidenceCollected(CreateOrEditMlcSvEvidenceCollectedDto input)
        {
            var obj = await _mlesv.CreateOrEdit(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("CreateOrEditMlcSvReport")]
        public async Task<IActionResult> CreateOrEditMlcSvReport(CreateOrEditMlcSvReportDto input)
        {
            var obj = await _mlesv.CreateOrEdit(input);
            return Ok(new ResponseSave { data = obj });
        }
        #endregion

        #region Read
        [HttpGet]
        [Route("GetSingleMlcSvRecordByPatientId")]
        public async Task<IActionResult> GetSingleMlcSvRecordByPatientId(Guid PatientId) 
        {
            var lst =await _mlesv.GetSingleMlcSvRecordByPatientId(PatientId);
            return Ok(new ResponseSuccess { data =  lst }); 
        }

        [HttpGet]
        [Route("GetAllMlcSvRecord")]
        public async Task<IActionResult> GetAllMlcSvRecord([FromQuery] SearchFilterDto? filter)
        {
            var lst = await _mlesv.GetAllMlcSvRecord(filter);
            return Ok(new ResponseSuccess { data = lst });
        }
        #endregion

    }
}
