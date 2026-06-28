using CommonDTOs.ResponseDTO;
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
    public class DeathCertificateFormController : ControllerBase
    {
        #region Class Fields & Properties
        private readonly TokenService _tokenService;
        private readonly IDeathCertificate _deathCertificateService;

        #endregion

        #region Constructor
        public DeathCertificateFormController(TokenService tokenService, IDeathCertificate deathCertificate)
        {
            _tokenService = tokenService;
            _deathCertificateService = deathCertificate;
        }
        #endregion

        #region CUD
        [HttpPost]
        [Route("CreateOrEdit")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditDeathCertificateDto input)
        {
            var obj = await _deathCertificateService.CreateOrEdit(input);
            return Ok(new ResponseSave { data = obj });
        }
        #endregion

        #region Read
        [HttpGet]
        [Route("GetDeathCertificatePatientsList")]
        public async Task<IActionResult> GetDeathCertificatePatientsList([FromQuery] SearchFilterDto? filter)
        {
            var obj = await _deathCertificateService.GetDeathCertificatePatientsList(filter);
            return Ok(new ResponseSuccess { data = obj });
        }

        [HttpGet]
        [Route("GetSingleDeathCertificatePatients")]
        public async Task<IActionResult> GetDeathCertificatePatientsList(Guid PatientVisitId)
        {
            var obj = await _deathCertificateService.GetSingleDeathCertificatePatient(PatientVisitId);
            return Ok(new ResponseSuccess { data = obj });
        }
        #endregion
    }
}
