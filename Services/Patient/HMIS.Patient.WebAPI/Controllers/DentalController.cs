using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using HMIS.Patient.Service;
using JWTAuthentication;
using CommonDTOs.ResponseDTO;
using AuthDAL.Models.Dto.ProfileDto;
using HMIS.Patient.Domain.Models.DTO.DentalDto;

namespace HMIS.Patient.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DentalController : ControllerBase
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly DentalService _dentalService;
        #endregion

        #region Constructor

        public DentalController(TokenService tokenService, DentalService dentalService)
        {
            _tokenService = tokenService;
            _dentalService = dentalService;
        }

        #endregion


        #region CUD Operations

        [HttpPost]
        [Route("CreateOrEdit")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditDentalSterilizationRecordDto input)
        {
            var obj = await _dentalService.CreateOrEdit(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var obj = await _dentalService.Delete(Id);
            return Ok(new ResponseDelete { data = obj });
        }

        #endregion

        #region Read Operations

        [HttpGet]
        [Route("GetAllWithPagination")]
        public async Task<IActionResult> GetAllWithPagination([FromQuery] FilterDentalSterilizationRecordDto filterDentalSterilizationRecordDto)
        {
            var response = await _dentalService.GetAllWithPagination(filterDentalSterilizationRecordDto);
            return Ok(new ResponseSuccess { data = response });
        }

        #endregion
    }
}
