using AuthBAL;
using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.HealthFacilityTypeDto;
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
    public class HealthFacilityTypeController : ControllerBase
    {
        #region Class Fields & Propertities

        private readonly ILogger<AuthenticationController> _logger;
        private readonly TokenService _tokenService;
        private readonly HealthFacilityTypeService<HealthFacilityType> _HealthFacilityTypeService;

        #endregion

        #region Constructor

        public HealthFacilityTypeController(ILogger<AuthenticationController> logger, TokenService tokenService, HealthFacilityTypeService<HealthFacilityType> HealthFacilityTypeService)
        {
            _logger = logger;
            _tokenService = tokenService;
            _HealthFacilityTypeService = HealthFacilityTypeService;
        }

        #endregion

        #region CUD Operations

        [HttpPost]
        [Route("CreateOrEdit")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditHealthFacilityTypeDto input)
        {
            var obj = await _HealthFacilityTypeService.CreateOrEdit(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int Id)
        {
            var obj = await _HealthFacilityTypeService.Delete(Id);
            return Ok(new ResponseDelete { data = obj });
        }

        #endregion

        #region Read Operations

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _HealthFacilityTypeService.GetAll(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(int Id)
        {
            var obj = await _HealthFacilityTypeService.GetById(Id);
            return Ok(new ResponseSuccess { data = obj });
        }

        #endregion

        #region Helper Methods


        #endregion
    }
}
