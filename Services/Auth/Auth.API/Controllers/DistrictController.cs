using AuthBAL;
using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.DistrictDto;
using AuthDAL.Models.Dto.MenuDto;
using CommonDTOs.Enums;
using CommonDTOs.ResponseDTO;
using HealthFacilityBAL;
using JWTAuthentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DistrictController : ControllerBase
    {
        #region Class Fields & Propertities

        private readonly ILogger<AuthenticationController> _logger;
        private readonly TokenService _tokenService;
        private readonly DistrictService<District> _DistrictService;

        #endregion

        #region Constructor

        public DistrictController(ILogger<AuthenticationController> logger, TokenService tokenService, DistrictService<District> DistrictService)
        {
            _logger = logger;
            _tokenService = tokenService;
            _DistrictService = DistrictService;
        }

        #endregion

        #region CUD Operations

        [HttpPost]
        [Route("CreateOrEdit")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditDistrictDto input)
        {
            var obj = await _DistrictService.CreateOrEdit(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int Id)
        {
            var obj = await _DistrictService.Delete(Id);
            return Ok(new ResponseDelete { data = obj });
        }

        #endregion

        #region Read Operations

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _DistrictService.GetAll(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(int Id)
        {
            var obj = await _DistrictService.GetById(Id);
            return Ok(new ResponseSuccess { data = obj });
        }

        [HttpGet]
        [Route("GetAllByDivisionId")]
        public async Task<IActionResult> GetAllByDivisionId(int DivisionId)
        {
            var obj = await _DistrictService.GetAllByDivisionId(DivisionId);
            return Ok(new ResponseSuccess { data = obj });
        }

        #endregion

        #region Helper Methods


        #endregion
    }
}
