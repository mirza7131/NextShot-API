using AuthBAL;
using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.HealthFacilityDto;
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
    public class HealthFacilityController : ControllerBase
    {
        #region Class Fields & Propertities

        private readonly ILogger<AuthenticationController> _logger;
        private readonly TokenService _tokenService;
        private readonly HealthFacilityService<HealthFacility> _HealthFacilityService;
        private string[] myInClause;//= new string[] {"011", "012", "068", "037" }; // Health Facilities Types Allow Only
        #endregion

        #region Constructor

        public HealthFacilityController(ILogger<AuthenticationController> logger, TokenService tokenService,
            HealthFacilityService<HealthFacility> HealthFacilityService, IConfiguration config)
        {
            _logger = logger;
            _tokenService = tokenService;
            _HealthFacilityService = HealthFacilityService;
            myInClause = config.GetSection("HealthFacility").GetSection("TypesAllowed").Get<string[]>() ?? new string[0];
        }

        #endregion

        #region CUD Operations

        [HttpPost]
        [Route("CreateOrEdit")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditHealthFacilityDto input)
        {
            var obj = await _HealthFacilityService.CreateOrEdit(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int Id)
        {
            var obj = await _HealthFacilityService.Delete(Id);
            return Ok(new ResponseDelete { data = obj });
        }

        //[HttpPost]
        //[Route("DumpHealthFacilities")]
        //public async Task<IActionResult> DumpHealthFacilities()
        //{

        //    await _HealthFacilityService.GetHrHealthFacilitesAsync();
        //    return Ok(new ResponseSave { data = null });

        //}

        [HttpPost]
        [Route("UpdateHealthFacilitiesFromHr")]
        public async Task<IActionResult> UpdateHealthFacilitiesFromHr()
        {

            await _HealthFacilityService.UpdateHealthFacilitiesFromHr();
            return Ok(new ResponseSave { data = null });

        }

        #endregion

        #region Read Operations

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _HealthFacilityService.GetAll(x => myInClause.Contains(x.HealthFacilityTypeCode) &&  x.ActionTypeId != (int)ActionTypeEnum.Deleted);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetAllHealthFacility")]
        public async Task<IActionResult> GetAllHealthFacility([FromQuery] FilterHealthFacilityDto healthFacilityDto)
        {
            var list = await _HealthFacilityService.GetAll(healthFacilityDto);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetAllForConsignment")]
        public async Task<IActionResult> GetAllForConsignment()
        {
            var list = await _HealthFacilityService.GetAllForConsignment();
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetAllWithPagination")]
        public async Task<IActionResult> GetAllWithPagination([FromQuery] FilterHealthFacilityDto healthFacilityDto)
        {
            var response = await _HealthFacilityService.GetAllWithPagination(healthFacilityDto);
            return Ok(new ResponseSuccess { data = response });
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(int Id)
        {
            var obj = await _HealthFacilityService.GetById(Id);
            return Ok(new ResponseSuccess { data = obj });
        }

        #endregion

        #region Helper Methods


        #endregion
    }
}
