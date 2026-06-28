using CommonDTOs.ResponseDTO;
using HMIS.EMC.Domain.Models.DbModels;
using HMIS.EMC.Domain.Models.Dto;
using HMIS.EMC.Domain.Models.Dto.FilterDto;
using HMIS.EMC.Service;
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
    public class IcvCertificateController : ControllerBase
    {
        #region Class Fields & Propertities

        //private readonly ILogger<AuthenticationController> _logger;
        private readonly TokenService _tokenService;
        private readonly IicvCertificate _icvCertificateService;

        #endregion

        #region Constructor
        //ILogger<AuthenticationController> logger
        public IcvCertificateController(TokenService tokenService, IicvCertificate icvCertificateService)
        {
            // _logger = logger;
            _tokenService = tokenService;
            _icvCertificateService = icvCertificateService;
        }

        #endregion

        #region CUD

        [HttpPost]
        [Route("CreateOrEdit")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditIcvCertificateDto input)
        {
            var obj = await _icvCertificateService.CreateOrEdit(input);
            return Ok(new ResponseSave { data = obj });
        }

        #endregion

        #region READ    
        [HttpGet]
        [Route("GetIcvPatientsList")]
        public async Task<IActionResult> GetIcvPatientsList([FromQuery] SearchFilterDto? filter)
        {
            var list = await _icvCertificateService.GetIcvPatientsList(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetSingleIcvPatientByPatientId")]
        public async Task<IActionResult> GetSingleIcvPatientByPatientId(Guid PatientId) {
            var obj = await _icvCertificateService.GetSingleIcvPatientByPatientId(PatientId);
            return Ok(new ResponseSuccess { data = obj });  
        }
        #endregion
    }
}
