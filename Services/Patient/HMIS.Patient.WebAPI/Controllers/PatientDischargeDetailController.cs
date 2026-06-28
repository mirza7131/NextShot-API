using CommonDTOs.Enums;
using CommonDTOs.ResponseDTO;
using HMIS.Patient.Domain.Models.DbModels;
using HMIS.Patient.Domain.Models.DTO.PatientDischargeDetailDto;
using HMIS.Patient.Domain.Models.DTO.PatientOpenVisitDto;
using HMIS.Patient.Service;
using JWTAuthentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMIS.Patient.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientDischargeDetailController : ControllerBase
    {
        #region Class Fields & Propertities

        private readonly ILogger<PatientVitalController> _logger;
        private readonly TokenService _tokenService;
        private readonly PatientDischargeDetailService<PatientDischargeDetail> _PatientDischargeDetailService;
        #endregion

        #region Constructor

        public PatientDischargeDetailController(ILogger<PatientVitalController> logger, TokenService tokenService, PatientDischargeDetailService<PatientDischargeDetail> PatientDischargeDetailService)
        {
            _logger = logger;
            _tokenService = tokenService;
            _PatientDischargeDetailService = PatientDischargeDetailService;
        }

        #endregion

        #region CUD Operations

        [HttpPost]
        [Route("CreateOrEdit")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditPatientDischargeDetailDto input)
        {
            var obj = await _PatientDischargeDetailService.CreateOrEdit(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var obj = await _PatientDischargeDetailService.Delete(Id);
            return Ok(new ResponseDelete { data = obj });
        }

        [HttpPost]
        [Route("DischargePatientVisit")]
        public async Task<IActionResult> DischargePatientVisit(PatientDischargeDto input)
        {
            var obj = await _PatientDischargeDetailService.DischargePatientVisit(input);
            return Ok(new ResponseUpdate { data = obj });
        }

        #endregion

        #region Read Operations

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _PatientDischargeDetailService.GetAll(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted);
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
            var obj = await _PatientDischargeDetailService.GetById(Id);
            return Ok(new ResponseSuccess { data = obj });
        }

        [HttpGet]
        [Route("GetDischargeDetailByVisitId")]
        public async Task<IActionResult> GetDischargeDetailByVisitId(Guid? VisitId)
        {
            var list = await _PatientDischargeDetailService.GetDischargeDetailByVisitId(VisitId);
            return Ok(new ResponseSuccess { data = list });
        }

        #endregion

        #region Helper Methods


        #endregion
    }
}
