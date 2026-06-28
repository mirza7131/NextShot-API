using AuthBAL;
using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.HfLabTestConfigDto;
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
    public class HfLabTestConfigController : ControllerBase
    {
        #region Class Fields & Propertities

        private readonly ILogger<AuthenticationController> _logger;
        private readonly TokenService _tokenService;
        private readonly HfLabTestConfigService<HfLabTestConfig> _HfLabTestConfigService;

        #endregion

        #region Constructor

        public HfLabTestConfigController(ILogger<AuthenticationController> logger, TokenService tokenService, HfLabTestConfigService<HfLabTestConfig> HfLabTestConfigService)
        {
            _logger = logger;
            _tokenService = tokenService;
            _HfLabTestConfigService = HfLabTestConfigService;
        }

        #endregion

        #region CUD Operations

        [HttpPost]
        [Route("CreateOrEdit")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditHfLabTestConfigListDto input)
        {
            var obj = await _HfLabTestConfigService.CreateOrEdit(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int Id)
        {
            var obj = await _HfLabTestConfigService.Delete(Id);
            return Ok(new ResponseDelete { data = obj });
        }

        #endregion

        #region Read Operations

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _HfLabTestConfigService.GetAll(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted);
            return Ok(new ResponseSuccess { data = response });
        }

        [HttpGet]
        [Route("GetAllConfigHealthFacilitiesByFilters")]
        public async Task<IActionResult> GetAllConfigHealthFacilitiesByFilters([FromQuery] FilterHfLabTestConfigDto filter)
        {
            var response = await _HfLabTestConfigService.GetAllConfigHealthFacilitiesByFilters(filter);
            return Ok(new ResponseSuccess { data = response });
        }

        [HttpGet]
        [Route("GetHfLabTestConfigByHealthFacilityId")]
        public async Task<IActionResult> GetHfLabTestConfigByHealthFacilityId(int? HealthFacilityId)
        {
            var response = await _HfLabTestConfigService.GetHfLabTestConfigByHealthFacilityId(HealthFacilityId);
            return Ok(new ResponseSuccess { data = response });
        }

        //[HttpGet]
        //[Route("GetAllWithPagination")]
        //public async Task<IActionResult> GetAllWithPagination([FromQuery] FilterHfLabTestConfigDto HfLabTestConfigDto)
        //{
        //    var response = await _HfLabTestConfigService.GetAllWithPagination(HfLabTestConfigDto);
        //    return Ok(new ResponseSuccess { data = response });
        //}


        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var obj = await _HfLabTestConfigService.GetById(Id);
            return Ok(new ResponseSuccess { data = obj });
        }

        #endregion

        #region Helper Methods


        #endregion
    }
}
