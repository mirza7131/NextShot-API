using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using HMIS.Patient.Service;
using JWTAuthentication;
using CommonDTOs.ResponseDTO;
using AuthDAL.Models.Dto.ProfileDto;
using HMIS.Patient.Domain.Models.DTO.DentalDto;
using HMIS.Patient.Domain.Models.DTO.CdcDto;

namespace HMIS.Patient.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CdcController : ControllerBase
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly CdcService _cdcService;
        #endregion

        #region Constructor

        public CdcController(TokenService tokenService, CdcService cdcService)
        {
            _tokenService = tokenService;
            _cdcService = cdcService;
        }

        #endregion


        #region CUD Operations

        //[HttpPost]
        //[Route("CreateOrEdit")]
        //public async Task<IActionResult> CreateOrEdit(CreateOrEditDentalSterilizationRecordDto input)
        //{
        //    var obj = await _dentalService.CreateOrEdit(input);
        //    return Ok(new ResponseSave { data = obj });
        //}

        //[HttpPost]
        //[Route("Delete")]
        //public async Task<IActionResult> Delete(Guid Id)
        //{
        //    var obj = await _dentalService.Delete(Id);
        //    return Ok(new ResponseDelete { data = obj });
        //}

        #endregion

        #region Read Operations

        [HttpGet]
        [Route("GetAllWithPagination")]
        public async Task<IActionResult> GetAllWithPagination([FromQuery] FilterCdcDto filterCdcDto)
        {
            var response = await _cdcService.GetAllWithPagination(filterCdcDto);
            return Ok(new ResponseSuccess { data = response });
        }

        #endregion
    }
}
