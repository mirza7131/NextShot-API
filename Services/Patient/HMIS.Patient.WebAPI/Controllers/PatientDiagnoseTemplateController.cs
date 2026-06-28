using CommonDTOs.Enums;
using CommonDTOs.FilterDTO;
using CommonDTOs.ResponseDTO;
using HMIS.Patient.Domain.Models.DbModels;
using HMIS.Patient.Domain.Models.DTO.PaginationDto;
using HMIS.Patient.Domain.Models.DTO.PatientDiagnoseTemplateDto;
using HMIS.Patient.Domain.Models.DTO.PatientVitalDto;
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
    public class PatientDiagnoseTemplateController : ControllerBase
    {
        #region Class Fields & Propertities

        private readonly ILogger<PatientVitalController> _logger;
        private readonly TokenService _tokenService;
        private readonly PatientDiagnoseTemplateService<PatientDiagnoseTemplate> _PatientDiagnoseTemplateService;
        #endregion

        #region Constructor

        public PatientDiagnoseTemplateController(ILogger<PatientVitalController> logger, TokenService tokenService, PatientDiagnoseTemplateService<PatientDiagnoseTemplate> PatientDiagnoseTemplateService)
        {
            _logger = logger;
            _tokenService = tokenService;
            _PatientDiagnoseTemplateService = PatientDiagnoseTemplateService;
        }

        #endregion

        #region CUD Operations

        [HttpPost]
        [Route("CreateOrEdit")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditPatientDiagnoseTemplateDto input)
        {
            var obj = await _PatientDiagnoseTemplateService.CreateOrEdit(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var obj = await _PatientDiagnoseTemplateService.Delete(Id);
            return Ok(new ResponseDelete { data = obj });
        }

        #endregion

        #region Read Operations

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _PatientDiagnoseTemplateService.GetAll(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted);
            return Ok(new ResponseSuccess { data = list });
        }
        //public async Task<IActionResult> GetAll([FromQuery] FilterPatientDto filterPatientDto)
        //{
        //    var response = await _PatientService.GetAll(filterPatientDto);
        //    return Ok(new ResponseSuccess { data = response });
        //}


        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var obj = await _PatientDiagnoseTemplateService.GetById(Id);
            return Ok(new ResponseSuccess { data = obj });
        }

        [HttpGet]
        [Route("GetByUserId")]
        public async Task<IActionResult> GetByUserId(Guid UserId)
        {
            var obj = await _PatientDiagnoseTemplateService.GetByUserId(UserId);
            return Ok(new ResponseSuccess { data = obj });
        }

        #endregion

        #region Helper Methods


        #endregion
    }
}
