using AuthBAL;
using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.LocationDto;
using AuthDAL.Models.Dto.ProvinceDto;
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
    //[Authorize]
    public class ProvinceController : ControllerBase
    {
        #region Class Fields & Propertities

        private readonly ILogger<AuthenticationController> _logger;
        private readonly TokenService _tokenService;
        private readonly ProvinceService<Province> _ProvinceService;

        #endregion

        #region Constructor

        public ProvinceController(ILogger<AuthenticationController> logger, TokenService tokenService, ProvinceService<Province> ProvinceService)
        {
            _logger = logger;
            _tokenService = tokenService;
            _ProvinceService = ProvinceService;
        }

        #endregion

        #region CUD Operations

        [HttpPost]
        [Route("CreateOrEdit")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditProvinceDto input)
        {
            var obj = await _ProvinceService.CreateOrEdit(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int Id)
        {
            var obj = await _ProvinceService.Delete(Id);
            return Ok(new ResponseDelete { data = obj });
        }

        #endregion

        #region Read Operations

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _ProvinceService.GetAll(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetAllLocations")]
        public async Task<IActionResult> GetAllLocations(bool IsHmisHf = false)
        {
            var obj = await _ProvinceService.GetAllLocations(IsHmisHf);
            return Ok(new ResponseSuccess { data = obj });
        }

        [HttpGet]
        [Route("GetAllLocationsFilters")]
        public async Task<IActionResult> GetAllLocationsFilters(bool IsHmisHf = false)
        {
            var obj = await _ProvinceService.GetAllLocationsFilters(IsHmisHf);
            return Ok(new ResponseSuccess { data = obj });
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(int Id)
        {
            var obj = await _ProvinceService.GetById(Id);
            return Ok(new ResponseSuccess { data = obj });
        }

        #endregion

        #region Helper Methods


        #endregion
    }
}
