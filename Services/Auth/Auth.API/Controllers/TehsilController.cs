using AuthBAL;
using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.MenuDto;
using AuthDAL.Models.Dto.TehsilDto;
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
    public class TehsilController : ControllerBase
    {
        #region Class Fields & Propertities

        private readonly ILogger<AuthenticationController> _logger;
        private readonly TokenService _tokenService;
        private readonly TehsilService<Tehsil> _TehsilService;

        #endregion

        #region Constructor

        public TehsilController(ILogger<AuthenticationController> logger, TokenService tokenService, TehsilService<Tehsil> TehsilService)
        {
            _logger = logger;
            _tokenService = tokenService;
            _TehsilService = TehsilService;
        }

        #endregion

        #region CUD Operations

        [HttpPost]
        [Route("CreateOrEdit")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditTehsilDto input)
        {
            var obj = await _TehsilService.CreateOrEdit(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int Id)
        {
            var obj = await _TehsilService.Delete(Id);
            return Ok(new ResponseDelete { data = obj });
        }

        #endregion

        #region Read Operations

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _TehsilService.GetAll(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(int Id)
        {
            var obj = await _TehsilService.GetById(Id);
            return Ok(new ResponseSuccess { data = obj });
        }

        [HttpGet]
        [Route("GetAllByDistrictId")]
        public async Task<IActionResult> GetAllByDistrictId(int DistrictId)
        {
            var obj = await _TehsilService.GetAllByDistrictId(DistrictId);
            return Ok(new ResponseSuccess { data = obj });
        }

        #endregion

        #region Helper Methods


        #endregion
    }
}
