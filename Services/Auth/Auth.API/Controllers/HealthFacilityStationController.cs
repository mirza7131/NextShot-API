using AuthBAL;
using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.HealthFacilityStationDto;
using AuthDAL.Models.Dto.PaginationDto;
using CommonDTOs.Enums;
using CommonDTOs.ResponseDTO;
using JWTAuthentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HealthFacilityStationController : ControllerBase
    {
        #region Class Fields & Propertities

        private readonly ILogger<AuthenticationController> _logger;
        private readonly TokenService _tokenService;
        private readonly HealthFacilityStationService<HealthFacilityStation> _HealthFacilityStationService;

        #endregion

        #region Constructor

        public HealthFacilityStationController(ILogger<AuthenticationController> logger, TokenService tokenService, HealthFacilityStationService<HealthFacilityStation> HealthFacilityStationService)
        {
            _logger = logger;
            _tokenService = tokenService;
            _HealthFacilityStationService = HealthFacilityStationService;
        }

        #endregion

        #region CUD Operations

        [HttpPost]
        [Route("CreateOrEdit")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditHealthFacilityStationDto input)
        {
            var obj = await _HealthFacilityStationService.CreateOrEdit(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var obj = await _HealthFacilityStationService.Delete(Id);
            return Ok(new ResponseDelete { data = obj });
        }

        #endregion

        #region Read Operations

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _HealthFacilityStationService.GetAll(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(int Id)
        {
            var obj = await _HealthFacilityStationService.GetById(Id);
            return Ok(new ResponseSuccess { data = obj });
        }

        [HttpGet]
        [Route("GetAllWithDetails")]
        public async Task<IActionResult> GetAllWithDetails([FromQuery] PagerDto filter)
        {
            var list = await _HealthFacilityStationService.GetAllWithDetails(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        #endregion

        #region Helper Methods


        #endregion
    }
}
