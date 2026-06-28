using AuthBAL;
using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.HealthFacilityCategoryDto;
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
    public class HealthFacilityCategoryController : ControllerBase
    {
        #region Class Fields & Propertities

        private readonly ILogger<AuthenticationController> _logger;
        private readonly TokenService _tokenService;
        private readonly HealthFacilityCategoryService<HealthFacilityCategory> _HealthFacilityCategoryService;

        #endregion

        #region Constructor

        public HealthFacilityCategoryController(ILogger<AuthenticationController> logger, TokenService tokenService, HealthFacilityCategoryService<HealthFacilityCategory> HealthFacilityCategoryService)
        {
            _logger = logger;
            _tokenService = tokenService;
            _HealthFacilityCategoryService = HealthFacilityCategoryService;
        }

        #endregion

        #region CUD Operations

        [HttpPost]
        [Route("CreateOrEdit")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditHealthFacilityCategoryDto input)
        {
            var obj = await _HealthFacilityCategoryService.CreateOrEdit(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int Id)
        {
            var obj = await _HealthFacilityCategoryService.Delete(Id);
            return Ok(new ResponseDelete { data = obj });
        }

        #endregion

        #region Read Operations

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _HealthFacilityCategoryService.GetAll(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(int Id)
        {
            var obj = await _HealthFacilityCategoryService.GetById(Id);
            return Ok(new ResponseSuccess { data = obj });
        }

        #endregion

        #region Helper Methods


        #endregion
    }
}
