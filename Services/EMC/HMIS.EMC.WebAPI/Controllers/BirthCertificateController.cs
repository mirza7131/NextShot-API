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
    public class BirthCertificateController : ControllerBase
    {
        #region Class Fields & Properties
        private readonly TokenService _tokenService;
        private readonly IBirthCertificate _birthCertificateService;

        #endregion

        #region Constructor
        public BirthCertificateController(TokenService tokenService, IBirthCertificate birthCertificate)
        {
            _tokenService = tokenService;
            _birthCertificateService = birthCertificate;
        }
        #endregion

        #region CUD

        [HttpPost]
        [Route("CreateOrEdit")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditBirthCertificateDto input)
        {
            var obj = await _birthCertificateService.CreateOrEdit(input);
            return Ok(new ResponseSave { data = obj });
        }

        #endregion

        #region Read
        [HttpGet]
        [Route("GetAllBirthCertificateRecord")]
        public async Task<IActionResult> GetAllBirthCertificateRecord([FromQuery] SearchFilterDto? filter)
        {
            var obj = await _birthCertificateService.GetAllBirthCertificateDto(filter);
            return Ok(new ResponseSuccess { data = obj });
        }

        [HttpGet]
        [Route("GetSingleBirthCertificateRecord")]
        public async Task<IActionResult> GetSingleBirthCertificateRecord(Guid PatientVisitId)
        {
            var obj = await _birthCertificateService.GetSingleBirthCertificateRecord(PatientVisitId);
            return Ok(new ResponseSuccess { data = obj });
        }
        #endregion
        //[HttpGet]
    }
}
