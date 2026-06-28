using CommonDTOs.Enums;
using CommonDTOs.ResponseDTO;
using HMIS.Patient.Domain.Models.DbModels;
using HMIS.Patient.Domain.Models.DTO.PatientWorkFlowLogDto;
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
    public class PatientWorkFlowLogController : ControllerBase
    {
        #region Class Fields & Propertities

        private readonly ILogger<PatientWorkFlowLogController> _logger;
        private readonly TokenService _tokenService;
        private readonly PatientWorkFlowLogService<PatientWorkFlowLog> _PatientWorkFlowLogService;

        #endregion

        #region Constructor

        public PatientWorkFlowLogController(ILogger<PatientWorkFlowLogController> logger, TokenService tokenService, PatientWorkFlowLogService<PatientWorkFlowLog> PatientWorkFlowLogService)
        {
            _logger = logger;
            _tokenService = tokenService;
            _PatientWorkFlowLogService = PatientWorkFlowLogService;
        }

        #endregion

        #region CUD Operations

        [HttpPost]
        [Route("CreateOrEdit")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditPatientWorkFlowLogDto input)
        {
            var obj = await _PatientWorkFlowLogService.CreateOrEdit(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var obj = await _PatientWorkFlowLogService.Delete(Id);
            return Ok(new ResponseDelete { data = obj });
        }

        #endregion

        #region Read Operations

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _PatientWorkFlowLogService.GetAll(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted);
            return Ok(new ResponseSuccess { data = response });
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var obj = await _PatientWorkFlowLogService.GetById(Id);
            return Ok(new ResponseSuccess { data = obj });
        }

        #endregion

        #region Helper Methods


        #endregion
    }
}
