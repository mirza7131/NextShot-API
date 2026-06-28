using AuthBAL;
using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.FeatureDto;
using AuthDAL.Models.Dto.OfflineVersionLogDto;
using CommonDTOs.Enums;
using CommonDTOs.ResponseDTO;
using JWTAuthentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfflineVersionLogController : ControllerBase
    {
        #region Class Fields & Propertities

        private readonly ILogger<AuthenticationController> _logger;
        private readonly TokenService _tokenService;
        private readonly OfflineVersionLogService<OfflineVersionLog> _OfflineVersionLogService;

        #endregion

        #region Constructor

        public OfflineVersionLogController(ILogger<AuthenticationController> logger, TokenService tokenService, OfflineVersionLogService<OfflineVersionLog> OfflineVersionLogService)
        {
            _logger = logger;
            _tokenService = tokenService;
            _OfflineVersionLogService = OfflineVersionLogService;
        }

        #endregion

        #region CUD Operations

        [HttpPost]
        [Route("CreateOrEdit")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditOfflineVersionLogDto input)
        {
            var obj = await _OfflineVersionLogService.CreateOrEdit(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var obj = await _OfflineVersionLogService.Delete(Id);
            return Ok(new ResponseDelete { data = obj });
        }

        #endregion

        #region Read Operations

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _OfflineVersionLogService.GetAll(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted);
            return Ok(new ResponseSuccess { data = response });
        }

        [HttpGet]
        [Route("GetAllWithPagination")]
        public async Task<IActionResult> GetAllWithPagination([FromQuery] OfflineVersionLogFilterDto OfflineVersionLogFilter)
        {
            var response = await _OfflineVersionLogService.GetAllWithPagination(OfflineVersionLogFilter);
            return Ok(new ResponseSuccess { data = response });
        }


        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var obj = await _OfflineVersionLogService.GetById(Id);
            return Ok(new ResponseSuccess { data = obj });
        }

        [HttpGet]
        [Route("GetByHfId")]
        public async Task<IActionResult> GetByHfId(int HealthFacilityId)
        {
            var obj = await _OfflineVersionLogService.GetByHfId(HealthFacilityId);
            return Ok(new ResponseSuccess { data = obj });
        }

        #endregion

        #region Helper Methods


        #endregion

    }
}
